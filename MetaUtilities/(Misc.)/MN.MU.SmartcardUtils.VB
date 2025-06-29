Namespace MetaUtilities

	''' <summary>Contains logic for interfacing with PCSC Smartcards and Readers</summary>
	Public Module SmartcardUtils

		''' <summary>An immutable representation of data read from an EMV Smartcard; like an ISO file of a smartcard.</summary>
		Public Class SmartcardDataSnapshot

			Public ReadOnly CardholderName$, CardProviderName$, CardLanguageID$, PaymentSystemEnvironment$

			Protected ReadOnly FlatData_ As New List(Of KeyValuePair(Of [String], [String]))()
			''' <summary>Serialise using e.g. `_SmartcardData.FlatData.ToArray().GetDictionaryPrintout()`</summary>
			Public ReadOnly Property FlatData As ObjectModel.ReadOnlyCollection(Of KeyValuePair(Of [String], [String]))
				Get
					Return New ObjectModel.ReadOnlyCollection(Of KeyValuePair(Of [String], [String]))(Me.FlatData_)
				End Get
			End Property

			Protected ReadOnly HeirarchicalData_ As MullNet.MetaUtilities.BasicDataStructures.TreeNode
			''' <summary>Includes all ASN1 data as child `TreeNode`s</summary>
			Public ReadOnly Property HeirarchicalData As MullNet.MetaUtilities.BasicDataStructures.TreeNode
				Get
					Return Me.HeirarchicalData_
				End Get
			End Property

			Friend Sub New(ByRef _CollectedSmartcardData As MullNet.MetaUtilities.BasicDataStructures.TreeNode)

				Me.HeirarchicalData_ = _CollectedSmartcardData
				Me.FlatData_ = _CollectedSmartcardData.AsFlatData.ToList()

				Dim _ScrapeDecodedStringValues_OfTheseKeys As System.Func(Of [String](), [String]) = (
					Function(_Keys As [String]()) Me.FlatData.Where(
						Function(_KVP As KeyValuePair(Of [String], [String])) _Keys.Contains(_KVP.Key.ToUpper())
					).Select(Of [String])(
						Function(_KVP As KeyValuePair(Of [String], [String])) _KVP.Value.SubstituteByteStrings_WithHumanReadableStrings("").Split({" "c}, StringSplitOptions.RemoveEmptyEntries).JoinAsStrings(" ")
					).Distinct().Where(
						Function(_DecodedValue As [String]) Not (System.Text.RegularExpressions.Regex.Replace(_DecodedValue, "[\?\(\)]", "") = "")
					).JoinAsStrings(", ")
				)

				Me.CardholderName = _ScrapeDecodedStringValues_OfTheseKeys({"5F-20"})
				Me.CardProviderName = _ScrapeDecodedStringValues_OfTheseKeys({"50"})            'Because this doesn't seem to work: 9F-12
				Me.CardLanguageID = _ScrapeDecodedStringValues_OfTheseKeys({"5F-2D"})
				Me.PaymentSystemEnvironment = _ScrapeDecodedStringValues_OfTheseKeys({"84"})

			End Sub

		End Class

		''' <summary>When a LibPCSC.PCSCReader is instanciated, it starts a card-insertion-listening thread. If this encounters an exception, here's a handler for it.</summary>
		Private ReadOnly Default_SmartcardInsertionException_Handler_ As Action(Of Exception) _
			= Sub(_SmartcardInsertionEx As Exception)
				  UsefulMethods.ConsoleErrorWriteLineInColour($"Smartcard Insertion Error: {_SmartcardInsertionEx.Message}", ConsoleColor.Red)
				  System.Environment.Exit(exitCode:=2)
			  End Sub

		''' <summary>Returns the names of all installed PCSC Smartcard Readers on the current system</summary>
		Public ReadOnly Property AvaliableSmartcardReaders As [String]()
			Get
				Dim _SmartcardReader As New LibPCSC.PCSCReader() With {.CardInsertionException_Handler = SmartcardUtils.Default_SmartcardInsertionException_Handler_}
				Return _SmartcardReader.AvaliableReaders.ToArray()
			End Get
		End Property

		''' <summary>Sends a series of APDU-Commands to the Smartcard in the specified reader, to build a SmartcardDataSnapshot</summary>
		Public Function ReadSmartcard(ByVal _TargetReader_Index As [Byte]) As SmartcardUtils.SmartcardDataSnapshot

			Dim _SmartcardReader As New LibPCSC.PCSCReader() With {.CardInsertionException_Handler = SmartcardUtils.Default_SmartcardInsertionException_Handler_}

			REM EMV Smartcard Reading Process:
			'	- Reset the card (done for us; we receive Answer-To-Reset)
			'	- Send APDU {0x00, 0xA4, 0x04, 0x00, ...} to query Smartcard's avaliable Applications
			'	- For each of these Applications:
			'		- ...

			Dim _CollectedSCData As New BasicDataStructures.TreeNode() With {.Name = "Smartcard"}

			Dim _TargetSCReader_Name$ = _SmartcardReader.AvaliableReaders.ElementAt(Convert.ToInt32(_TargetReader_Index))
			_SmartcardReader.Connect(_TargetSCReader_Name)

			Dim _PaymentSystemEnvironment_IDs As List(Of [Byte]()) =
				{"1PAY.SYS.DDF01", "2PAY.SYS.DDF01"} _
				.Select(Of [Byte]())(AddressOf New System.Text.ASCIIEncoding().GetBytes) _
				.ToList()

			Dim _Application_IDs As New List(Of [Byte]())()
			Dim _MatchingPSE_HasBeenFound As [Boolean] = False

			REM 1) Identify all Payment-System-Environments on the card
			For Each _PSE_ID As [Byte]() In _PaymentSystemEnvironment_IDs

				REM 1.1) Send an APDU saying "Select", "Directory by Name", {Current PSE}
				Dim _PSEExistenceQuery_Response As LibPCSC.APDUResponse _
					= _SmartcardReader.Transmit(New LibPCSC.APDUCommand(&H0, &HA4, &H4, &H0, _PSE_ID, Convert.ToByte(_PSE_ID.Length)))

				REM 1.2) If the Response's Switch-1 is 0x61, this means "Success, but go back and read an additional {Switch-2} bytes"
				If _PSEExistenceQuery_Response.SW1 = &H61 Then _PSEExistenceQuery_Response _
					= _SmartcardReader.Transmit(New LibPCSC.APDUCommand(&H0, &HC0, &H0, &H0, Nothing, _PSEExistenceQuery_Response.SW2))

				REM 1.3) If we've received a {0x90, 0x00} (Command Seccessfully Executed (OK)) response, this PSE exists on the Smartcard.
				If _PSEExistenceQuery_Response.SwitchesAre("90-00") Then

					_MatchingPSE_HasBeenFound = True

					'Here, we've identified a PSE present on the Smartcard, and have successfully retreived an AIP containing a list of supported Applications from that PSE
					'Next, collect each of these Applications' IDs (?)

					Dim _FileControlInfo As New LibPCSC.ASN1DataStructure(data:=_PSEExistenceQuery_Response.Data)
					_CollectedSCData.Children.Add(New TreeNode() With {.Name = "File-Control-Information for Payment-System-Environment " & New System.Text.ASCIIEncoding().GetString(_PSE_ID), .Children = {_FileControlInfo.ToTreeNode_()}.ToList()})

					Dim _ShortFileIdentifier As [Byte] = _FileControlInfo.Find(&H88).Value.ElementAt(0)
					_CollectedSCData.Children.Add(New TreeNode() With {.Name = "Elementry-File-Identifier; Short-File-Identifier", .Value = System.BitConverter.ToString({_ShortFileIdentifier})})

					REM +++
					REM Next, go through records until no more records are found
					REM +++

					Dim _RecordNumber As [Byte] = 1
					Dim _Param2_SFI As [Byte] = Convert.ToByte((_ShortFileIdentifier << 3) Or 4)
					'Dim _ReadRecord_Response As LibPCSC.APDUResponse = _PSEExistenceQuery_Response

					'Forever, incrementing _RecordNumber, until the response is "Record not found"
					While True

						REM Send a "Read record" command
						Dim _ReadRecord_Response As LibPCSC.APDUResponse _
						= _SmartcardReader.Transmit(New LibPCSC.APDUCommand(&H0, &HB2, _RecordNumber, _Param2_SFI, Nothing, &H0))

						REM If the response was "Record not found", then the previous _RecordNumber was the final-avaliable-record
						If _ReadRecord_Response.SwitchesAre("6A-83") Then Exit While

						REM If we've received "Wrong length Le", re-issue the command with the correct Le (in SW2)
						If _ReadRecord_Response.SW1 = &H6C Then _ReadRecord_Response _
						= _SmartcardReader.Transmit(New LibPCSC.APDUCommand(&H0, &HB2, _RecordNumber, _Param2_SFI, Nothing, _ReadRecord_Response.SW2))

						REM If the Response's Switch-1 is 0x61, this means "Success, but go back and read an additional {Switch-2} bytes"
						If _ReadRecord_Response.SW1 = &H61 Then _ReadRecord_Response _
						 = _SmartcardReader.Transmit(New LibPCSC.APDUCommand(&H0, &HC0, &H0, &H0, Nothing, _ReadRecord_Response.SW2))

						If _ReadRecord_Response.Data IsNot Nothing Then

							_CollectedSCData.Children.Add(New TreeNode() With {.Name = "Record #" & _RecordNumber.ToString()})

							REM Extract the Application Elementry File(s)
							Dim _ApplicationElementryFile As New LibPCSC.ASN1DataStructure(data:=_ReadRecord_Response.Data)
							_CollectedSCData.Children.Add(New TreeNode() With {.Name = "Application-Elementry-File", .Children = {_ApplicationElementryFile.ToTreeNode_()}.ToList()})

							REM Add all Sub-ASN1s which "are really application templates"
							_Application_IDs.AddRange(
							_ApplicationElementryFile.Children _
								.Where(Function(_AppTemplate As LibPCSC.ASN1DataStructure) _AppTemplate.Tag.ElementAt(0) = &H61) _
								.Select(Of [Byte]())(Function(_AppTemplate As LibPCSC.ASN1DataStructure) _AppTemplate.Find(&H4F).Value)
						)

						End If

						_RecordNumber += Convert.ToByte(1)

					End While

				End If

				If _MatchingPSE_HasBeenFound Then Exit For

			Next




			REM 2) Process all of the Applications we've found by ID
			For Each _Application_ID As [Byte]() In _Application_IDs

				Dim _Application_FileLocators As New List(Of LibPCSC.ApplicationFileLocator)()

				REM Select the Application
				Dim _ApplicationSelection_Response As LibPCSC.APDUResponse _
					= _SmartcardReader.Transmit(New LibPCSC.APDUCommand(&H0, &HA4, &H4, &H0, _Application_ID, Convert.ToByte(_Application_ID.Length)))

				'Received "Response bytes still available"? Go back and read an additional {Switch-2} bytes...
				If _ApplicationSelection_Response.SW1 = &H61 Then _ApplicationSelection_Response _
					= _SmartcardReader.Transmit(New LibPCSC.APDUCommand(&H0, &HC0, &H0, &H0, Nothing, _ApplicationSelection_Response.SW2))

				'Received "File not found"? Skip to next Application...
				If _ApplicationSelection_Response.SwitchesAre("6A-82") Then Continue For

				'Received "Command successfully executed (OK)"? Extract Application's contents...
				If _ApplicationSelection_Response.SwitchesAre("90-00") Then

					Dim _ApplicationTreeNode As New TreeNode() With {.Name = System.BitConverter.ToString(_Application_ID)}


					Dim _FileControlInfo As New LibPCSC.ASN1DataStructure(data:=_ApplicationSelection_Response.Data)
					_ApplicationTreeNode.Children.Add(New TreeNode() With {.Name = "File-Control-Information", .Children = {_FileControlInfo.ToTreeNode_()}.ToList()})



					'Send a "PDOL", to retreive AIP with list of avaliable Applications
					Dim _PDOL_Response As LibPCSC.APDUResponse = _SmartcardReader.Transmit(New LibPCSC.APDUCommand(&H80, &HA8, &H0, &H0, {&H83, &H0}, &H2))

					'Received "Response bytes still available"? Go back and read an additional {Switch-2} bytes...
					If _PDOL_Response.SW1 = &H61 Then _PDOL_Response _
						= _SmartcardReader.Transmit(New LibPCSC.APDUCommand(&H0, &HC0, &H0, &H0, Nothing, _PDOL_Response.SW2))

					'Received "Command successfully executed (OK)"?
					If _PDOL_Response.SwitchesAre("90-00") Then

						Dim _Template As New LibPCSC.ASN1DataStructure(_PDOL_Response.Data)
						Dim _ApplicationInterchangeProfile, _ApplicationFileLocator As LibPCSC.ASN1DataStructure

						Select Case _Template.Tag.ElementAt(0)

							Case &H80 ' "Primative response (Template Format 1)"

								REM Get the AIP...

								Dim _TempAIP As [Byte]() = New [Byte](1) {}

								System.Buffer.BlockCopy(
									src:=_Template.Value, srcOffset:=0,
									dst:=_TempAIP, dstOffset:=0,
									count:=2
								)

								_ApplicationInterchangeProfile = New LibPCSC.ASN1DataStructure(tag:=&H82, data:=_TempAIP)

								REM Get the AFL...

								Dim _TempAFL As [Byte]() = New [Byte](_Template.Length - 3) {}

								System.Buffer.BlockCopy(
									src:=_Template.Value, srcOffset:=2,
									dst:=_TempAFL, dstOffset:=0,
									count:=_Template.Length - 2
								)

								_ApplicationFileLocator = New LibPCSC.ASN1DataStructure(tag:=&H94, data:=_TempAFL)

							Case &H77 ' "Constructed _CardData object response (Template Format 2)"

								_ApplicationInterchangeProfile = _Template.Find(&H82)
								_ApplicationFileLocator = _Template.Find(&H94)

							Case Else : Throw New Exception("Un-accounted-for Template Format of " & System.BitConverter.ToString({_Template.Tag.ElementAt(0)}))

						End Select

						_ApplicationTreeNode.Children.Add(New TreeNode() With {.Name = "Application-Interchange-Profile", .Children = {_ApplicationInterchangeProfile.ToTreeNode_()}.ToList()})
						_ApplicationTreeNode.Children.Add(New TreeNode() With {.Name = "Application-File-Locator", .Children = {_ApplicationFileLocator.ToTreeNode_()}.ToList()})



						REM Chop the Application-File-Locators up...


						Dim _AFL_Child_Index% = 0I
						While _AFL_Child_Index < _ApplicationFileLocator.Length

							Dim _AFL As [Byte]() = New [Byte](3) {}

							System.Buffer.BlockCopy(
								src:=_ApplicationFileLocator.Value,
								srcOffset:=_AFL_Child_Index,
								dst:=_AFL,
								dstOffset:=0,
								count:=4
							)

							_Application_FileLocators.Add(New LibPCSC.ApplicationFileLocator(_AFL))

							_AFL_Child_Index += 4I

						End While

						_ApplicationTreeNode.Children.Add(New TreeNode() With {.Name = "Application-Interchange-Profile; Application-File-Locator", .Children = {New LibPCSC.ASN1DataStructure(_PDOL_Response.Data).ToTreeNode_()}.ToList()})


						REM Iterate through each Application
						For Each _AppFileLocator As LibPCSC.ApplicationFileLocator In _Application_FileLocators

							Dim _RecordIndex As [Byte] = _AppFileLocator.FirstRecord
							Dim _FinalRecordIndex As [Byte] = _AppFileLocator.LastRecord
							Dim _Param2_SFI As [Byte] = Convert.ToByte((_AppFileLocator.SFI << 3) Or 4)

							Dim _ElementryFileTreeNode As New TreeNode() With {.Name = "Elementry-File #" & _AppFileLocator.SFI.ToString()}

							While _RecordIndex <= _FinalRecordIndex

								'Issue a "READ RECORD" command
								Dim _ReadRecord_Response As LibPCSC.APDUResponse _
									= _SmartcardReader.Transmit(New LibPCSC.APDUCommand(&H0, &HB2, _RecordIndex, _Param2_SFI, Nothing, &H0))

								'Received "Wrong length Le"? Re-issue the command with the correct Le (in SW2)...
								If _ReadRecord_Response.SW1 = &H6C Then _ReadRecord_Response _
									= _SmartcardReader.Transmit(New LibPCSC.APDUCommand(&H0, &HB2, _RecordIndex, _Param2_SFI, Nothing, _ReadRecord_Response.SW2))

								_ElementryFileTreeNode.Children.Add(New TreeNode() With {.Name = "Record #" & _RecordIndex, .Children = {New LibPCSC.ASN1DataStructure(_ReadRecord_Response.Data).ToTreeNode_()}.ToList()})
								_RecordIndex += Convert.ToByte(1)

							End While

							_ApplicationTreeNode.Children.Add(_ElementryFileTreeNode)

						Next


						REM Issue closing commands for "APPLICATION TRANSACTION COUNTER"
						_SmartcardReader.Transmit(New LibPCSC.APDUCommand(&H80, &HCA, &H9F, &H13, Nothing, 0))
						_SmartcardReader.Transmit(New LibPCSC.APDUCommand(&H80, &HCA, &H9F, &H17, Nothing, 0))
						_SmartcardReader.Transmit(New LibPCSC.APDUCommand(&H80, &HCA, &H9F, &H36, Nothing, 0))



					Else : Debug.WriteLine("Unexpected GET-PROCESSING-OPTIONS Response: " & _PDOL_Response.SwitchesToString())
					End If

					_CollectedSCData.Children.Add(_ApplicationTreeNode)

				Else : Debug.WriteLine("Unexpected SELECT Response: " & _ApplicationSelection_Response.SwitchesToString())
				End If

			Next

			_SmartcardReader.Disconnect()
			Return New SmartcardDataSnapshot(_CollectedSCData)

		End Function

		<System.Runtime.CompilerServices.Extension()>
		Private Function ToTreeNode_(ByRef _ASN1 As Global.LibPCSC.ASN1DataStructure) As BasicDataStructures.TreeNode

			Return New BasicDataStructures.TreeNode() With {
				.Name = System.BitConverter.ToString(_ASN1.Tag),
				.Value = System.BitConverter.ToString(_ASN1.Value),
				.Children = _ASN1.Children.Select(Of TreeNode)(Function(_Child As LibPCSC.ASN1DataStructure) _Child.ToTreeNode_()).ToList()
			}

		End Function

	End Module

End Namespace