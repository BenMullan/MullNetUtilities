'╔═══════════════════════════════════════════════════════════════════════════╗
'║                      MN Utilities \ CLA-Manager (v2)                      ║
'╚═══════════════════════════════════════════════════════════════════════════╝
'
'Features:
'---------
'	- Handles [/Key:Value] and [/Key] Pairs with [Action(Of String)]s
'	- Automatically recognises the /? switch and displays a CLA Help Dictionary

'Required Syntax: (Example)
'--------------------------
'	Program.exe /Key1:Value /Key2:"Value with Spaces" /Key3
'	In other words, the KVP must be in one OS-defined (space-delimited) CLA

'Example Usage:
'--------------
'	Dim _CLAManager As New MullNet.MetaUtilities.CLAManager("MullNet SilentControlToolset - SendKeys.exe" & vbCrLf & "Sends Keystrokes on the current Session to the active Window.") With {
'	  .CLAHelpDictionary_Examples = {
'	  "SendKeys /Keys:""HELLO""",
'	  "SendKeys /Keys:""Ctrl+A,Ctrl+C""",
'	  "SendKeys /Keys:""This is some lovley text!"" /ForegroundWindow:0x8c24a5 /SendWait"
'	  }
'	}
'
'	_CLAManager.KeyToActionMappings.AddRange({
'	   (New CLAManager.CLAKeyToActionMapping("Keys", Sub(_Value$) SendKeys.SpecifiedKeysSequence = _Value) With {.MustBeSpecified = True, .MustHaveValue = True, .Description = "[Datum] The [keys] argument for SendKeys.Send() or SendKeys.SendWait()"}),
'	   (New CLAManager.CLAKeyToActionMapping("Delay", Sub(_Value$) SendKeys.MSToWaitAfterSettingForegroundWindow = _Value.Extract_UInt64()) With {.MustBeSpecified = False, .MustHaveValue = True, .Description = "[Datum] The number of MilliSeconds to wait, after setting the ForegroundWindow (if one is specified), but before sending the Keystrokes."}),
'	   (New CLAManager.CLAKeyToActionMapping("ForegroundWindow", Sub(_Value$) SendKeys.TargetWindow_IntPtr = New IntPtr(_Value.Extract_UInt32())) With {.MustBeSpecified = False, .MustHaveValue = True, .Description = "[Datum] The hexadecimal HWND value, of the Window to Focus before sending the KeyStrokes."}),
'	   (New CLAManager.CLAKeyToActionMapping("NoWait", Sub(_Value$) SendKeys.UseSendWait = False) With {.MustBeSpecified = False, .MustHaveValue = False, .Description = "[Flag] Indicates that SendKeys.Send() should be used, instead of SendKeys.SendWait()"})
'	})
'
'	_CLAManager.OutputDelegate = Sub(_String$) MsgBox(_String, MsgBoxStyle.Information, "MN-SCT Command-Line")
'	_CLAManager.UseCommandLineFormatting_ForHelpDictionary = False
'	_CLAManager.RunForTheseCLAs(_CLAs)

Namespace MetaUtilities

	''' <summary>
	''' Manages the mappings between CLA Keys, and Actions-To-Run if a given Key is specified.
	''' The expected Command-line Argument Syntax is /Key or /Key:Value or /Key:"Value"
	''' Automatically generates and displays a Help-Dictionary when /? is specified.
	''' </summary>
	Partial Public Class CLAManager

#Region "Sub-Classes"

		''' <summary>
		''' Represents a configuration for a single CLA key, and what to do if it's specified.
		''' (Specified in the Syntax /Key or /Key:Value or /Key:"Value")
		''' NOTE: CLA Keys are not case-sensitive; /Name is the same as /NAmE
		''' </summary>
		Public Class CLAKeyToActionMapping

			''' <summary>The CLA Key E.g. "ShowLog". The forward slash / is not to be included in this string. NOTE: CLA Keys are not case-sensitive; /Name is the same as /NAmE</summary>
			Public ReadOnly Key As [String]

			''' <summary>The Action to run if the Key is specified. The String Argument is the Value corrosponding to the Key, which could be [Nothing] (null) {nullptr} &lt;undefined&gt; if there is no Value corrosponding to the Key</summary>
			Public ReadOnly ActionIfSpecified As Action(Of [String])

			''' <summary>The Description used in the CLA Help Dictionary ( /? ). Default: "(No Description)"</summary>
			Public Description As [String] = "(No Description)"

			''' <summary>Indicates that the Key must be specified in the Command-Line Arguments provided to the CLAManager, otherwise an Exception is Thrown. Default: False</summary>
			Public MustBeSpecified As [Boolean] = False

			''' <summary>Indicates that if the Key is specified, then a corrosponding Value must also be specified, otherwise an Exception is Thrown. Default: False</summary>
			Public MustHaveValue As [Boolean] = False

			''' <summary>Constructs a new Object to represent the desired configuration for a single CLA Key (Specified in the Syntax /Key or /Key:Value or /Key:"Value")</summary>
			''' <param name="_Key">The CLA Key E.g. "ShowLog". The forward slash / is not to be included in this string. NOTE: CLA Keys are not case-sensitive; /Name is the same as /NAmE</param>
			''' <param name="_ActionIfSpecified">The Action to run if the Key is specified. The String Argument is the Value corrosponding to the Key, which could be [Nothing] (null) {nullptr} &lt;undefined&gt; if there is no Value corrosponding to the Key in the actual Command-Line Arguments.</param>
			Public Sub New(ByVal _Key$, ByVal _ActionIfSpecified As Action(Of [String]))

				If _Key.Contains(CLAManager.CLAKeyStartChar_) OrElse _Key.Contains(CLAManager.CLAValueStartChar_) Then Throw New Exception("A Command-Line Argument Key in a CLAKeyToActionMapping cannot contain the CLAKeyStartChar " & CLAManager.CLAKeyStartChar_ & " or the CLAValueStartChar " & CLAManager.CLAValueStartChar_)
				If _Key.Contains(" "c) Then Throw New Exception("A Command-Line Argument Key in a CLAKeyToActionMapping cannot contain a space [ ], because this character is used by the Operating System to seperate Command-Line arguments, and therfore using it would lead to ambigous Command-Line syntacies")

				Me.Key = _Key
				Me.ActionIfSpecified = _ActionIfSpecified

			End Sub

		End Class

		''' <summary>Thrown when a Command-Line Argument is specified, but without a mandatory associated Value in the form /Key:Value or /Key:"Value".</summary>
		Public Class CLAValueNotSpecifiedException : Inherits System.Exception

			Public Sub New(ByVal _Message$)
				MyBase.New("[DSCLAValueNotSpecifiedException] " & _Message)
			End Sub

		End Class

		''' <summary>Thrown when a Command-Line Argument is mandatory, but not specified.</summary>
		Public Class CLANotSpecifiedException : Inherits System.Exception

			Public Sub New(ByVal _Message$)
				MyBase.New("[DSCLANotSpecifiedException] " & _Message)
			End Sub

		End Class

#End Region

		Protected Const ExitCode_ForWhenCLAHelpDictionaryIsDisplayed_% = -1

		Protected Const CLAKeyStartChar_ As Char = "/"c     'E.g.	/ShowLog
		Protected Const CLAValueStartChar_ As Char = ":"c   'E.g.	/Name:Ben
		Protected Const HelpCLA_$ = CLAKeyStartChar_ & "?"c 'E.g.	/?

		''' <summary>The Sub to be called when the Help Dictionary is to be displayed. Use MessageBox.Show for a GUI Application. Default: AddressOf Console.Writeline</summary>
		Public OutputDelegate As Action(Of [String]) = AddressOf Console.WriteLine

		''' <summary>The connections between registered Keys e.g /Name, and what to do when they are specified e.g. Sub (_Value$) Program.ProvidedName = _Value</summary>
		Public KeyToActionMappings As New List(Of CLAKeyToActionMapping)()

		''' <summary>The Text displayed at the start of the CLA Help Dictionary, when it is displayed via the presence of the /? CLA</summary>
		Public CLAHelpDictionary_Description As [String]

		''' <summary>Example Calls to the Binary executable with CLAs. E.g. DSCLI.EXE /RunSourceFile:"X:\Programming\DocScript\HelloWorld.DS"</summary>
		Public CLAHelpDictionary_Examples As [String]()

		''' <summary>
		''' Indicates that the Help Dictionary will have the Linebreaks and padding-whitespace automatically inserted.
		''' Use True, if this is a Console Application;
		''' If MsgBox()-ing out the /? Dictionary, then use False.
		''' Default: True
		''' </summary>
		Public UseCommandLineFormatting_ForHelpDictionary As [Boolean] = True

		''' <summary>
		''' <para>Constructs a Command-Line Argument Manager, but doesn't parse the arguments until RunForTheseCLAs() is called.</para>
		''' <para>If the Help Dictionary is displayed, then no other CLA Actions will be dealt with or run, and Environment.Exit(EXITCODE:=-1) is called.</para>
		''' <para>NOTE: CLA Keys are not case-sensitive; /Name is the same as /NAmE</para>
		''' Make sure to set the [UseCommandLineFormatting_ForHelpDictionary] Property correctly, if writing a GUI Application.
		''' </summary>
		''' <param name="_CLAHelpDictionary_Description">The Text displayed at the start of the CLA Help Dictionary, when it is displayed via the presence of the /? CLA</param>
		''' <remarks></remarks>
		Public Sub New(ByVal _CLAHelpDictionary_Description$)
			Me.CLAHelpDictionary_Description = _CLAHelpDictionary_Description
		End Sub

		''' <summary>
		''' Executes the [CLAKeyToActionMapping]s for the specified [_CommandLineArguments] argument. It is at this point, that any missing CLAs (whose MustBeSpecified Flags were set to True) cause an Exception to be Thrown.
		''' </summary>
		''' <param name="_CommandLineArguments">The space-delimited arguments provided to the process, by the Operating System. 4E.g. My.Application.CommandLineArgs.ToArray() or an "args"/"_CLAs" Parameter</param>
		Public Sub RunForTheseCLAs(ByRef _CommandLineArguments As [String]())
			Try

				REM Ensure all Keys are Unique...
				If Not Me.KeyToActionMappings.Select(Of [String])(Function(_CLAKeyToActionMapping As CLAKeyToActionMapping) _CLAKeyToActionMapping.Key.ToUpper()).ToArray().AllElementsAreUnique() _
				 Then Throw New Exception("The KeyToActionMappings Keys were not all Unique.")

				REM Determine weather to Display the Help Dictionary and exit, or to run the Actions for the Specified-CLAs...
				If _CommandLineArguments.Contains(HelpCLA_) Then
					Me.DisplayHelpDictionary_() : Global.System.Environment.Exit(exitCode:=CLAManager.ExitCode_ForWhenCLAHelpDictionaryIsDisplayed_)
				Else
					'↓ Ensures mandatory keys are specified, and executes the relevant [ActionIfSpecified]s
					Me.ExecuteActionsForSpecifiedCLAs_(_CommandLineArguments)
				End If

			Catch _Ex As Exception When True : Throw New Exception("Command-Line Argument Manager: " & _Ex.Message, _Ex) : End Try
		End Sub

#Region "Internal, Protected Utility Methods"

		REM Also ensures that Mandatory CLAs have been specified
		Protected Sub ExecuteActionsForSpecifiedCLAs_(ByRef _CommandLineArguments As [String]())

			REM Ensure the Mandatory Keys have been specified, and execute the Actions on derivable <Value>s
			For Each _CLAKeyToActionMapping As CLAKeyToActionMapping In Me.KeyToActionMappings

				If _CLAKeyToActionMapping.MustBeSpecified _
					Then If Not CLAsContainsKey_(_CommandLineArguments, _CLAKeyToActionMapping.Key) _
						Then Throw New CLANotSpecifiedException("The mandatory Command-Line Argument with Key """ & _CLAKeyToActionMapping.Key & """ was not specified. Command-Line Arguments must be provided in the syntax  /Key  or  /Key:Value  or  /Key:""Value"" . NOTE: CLA Keys are not case-sensitive;  /Name  is the same as  /NAmE .")

				'Here, we know that *if* the _CLAKeyToActionMapping was mandatory, then it has been specified
				'If it hasn't been specified, then we don't need to do anything about it
				If Not CLAsContainsKey_(_CommandLineArguments, _CLAKeyToActionMapping.Key) Then Continue For

				'If we're here, then the CLA Has been specified, so ensure it's value is present if it needs one
				Dim _CorrospondingValue$ = Nothing

				If _CLAKeyToActionMapping.MustHaveValue Then

					Dim _LambdaCopyOf_CLAKeyToActionMapping As CLAKeyToActionMapping = _CLAKeyToActionMapping

					Dim _CLAWithMatchingKey_Resolver As Func(Of [String], [Boolean]) _
						= Function(_CLA$) _CLA.ToUpper().StartsWith((CLAKeyStartChar_ & _LambdaCopyOf_CLAKeyToActionMapping.Key & CLAValueStartChar_).ToUpper()) AndAlso (_CLA.Length > (CLAKeyStartChar_ & _LambdaCopyOf_CLAKeyToActionMapping.Key & CLAValueStartChar_).Length)

					'Ensure that there is a Value
					If Not _CommandLineArguments.Where(_CLAWithMatchingKey_Resolver).Count() = 1 _
						Then Throw New CLAValueNotSpecifiedException("The Command-Line Argument with Key """ & _CLAKeyToActionMapping.Key & """ declared that a corrosponding Value was mandatory, but no recognisable value was provided in the Syntax /Key:Value or /Key:""Value"".")

					'Extract the :Value or :"Value"
					_CorrospondingValue = New [String](_CommandLineArguments.First(_CLAWithMatchingKey_Resolver).ToCharArray().Skip(_CommandLineArguments.First(_CLAWithMatchingKey_Resolver).ToCharArray().ToList().IndexOf(CLAValueStartChar_) + 1).ToArray())

				End If

				REM Now run the Action, using the derived <Value>
				_CLAKeyToActionMapping.ActionIfSpecified.Invoke(_CorrospondingValue)

			Next

		End Sub

		REM Handles the presence of Keys in the form /Key and /Key:Value and /Key:"Value"
		Protected Shared Function CLAsContainsKey_(ByVal _CLAs$(), ByVal _Key$) As Boolean

			'													/Key																	/Key: {...}
			Return _CLAs.Any(Function(_CLA$) (_CLA.ToUpper() = (CLAKeyStartChar_ & _Key).ToUpper()) OrElse _CLA.ToUpper().StartsWith((CLAKeyStartChar_ & _Key & CLAValueStartChar_).ToUpper()))
			REM Parenthetically: We couldn't have just checked whether the _CLA StartsWith() [/Key], because you could have another Key like /KeyTwo

		End Function

		REM Writes the CLA Help Dictionary to the OutputDelegate if the /? CLA is specified
		Protected Sub DisplayHelpDictionary_()

			Dim _CLAHelpDictionaryText$

			If Me.UseCommandLineFormatting_ForHelpDictionary Then

				REM CLA Help Dictionary Format: CommandLineFormatting=True

				'Description:
				'----------------------------------------
				'{Description}
				'
				'Examples:
				'----------------------------------------
				'{Examples}
				'
				'Argument Usage:
				'----------------------------------------
				'/RunFile:<Value>		The File to Execute (*.DS)
				'/Debug					(Optional) Shows Debugging Logging Messages. More text can go here, bu
				'						t it wraps around after 80 columns (in total)
				'/ShowLog				(Optional) Shows the Log during Interpretation


				REM ↓ The number of chars wide the console is treated as
				REM ↓ If the process isn't being run from a console,
				REM   (e.g. because it's being run through Doorman or VS-Immediate-Window)
				REM   then there is no Handle to a Console, so Console.WindowWidth raises an IOException
				Dim _ConsoleWidth_InChars As UInt16
				Try : _ConsoleWidth_InChars = Convert.ToUInt16(System.Console.WindowWidth)
				Catch _IOEx As System.IO.IOException : _ConsoleWidth_InChars = 80US : End Try

				_CLAHelpDictionaryText$ =
<String>
	Description:
	<%= "-"c.Repeat(_ConsoleWidth_InChars) & vbCrLf & vbCrLf %>
	<%= Me.CLAHelpDictionary_Description %>

	Examples:
	<%= "-"c.Repeat(_ConsoleWidth_InChars) & vbCrLf %>
	<%= Me.CLAHelpDictionary_Examples.JoinAsStrings(vbCrLf) %>

	Argument Usage: (Keys are case-insensitive)
	<%= "-"c.Repeat(_ConsoleWidth_InChars) %>
</String>.Value.Split({vbLf}, StringSplitOptions.None).Select(Of [String])(Function(_Line$) _Line.Trim()).ToArray().JoinAsStrings(vbCrLf)


				For Each _CLAKeyToActionMapping As CLAKeyToActionMapping In Me.KeyToActionMappings

					Const _KeyAndValuePart_TargetLength As UInt16 = 30US
					Dim _DescriptionPart_MaxColumnWidth As UInt16 = _ConsoleWidth_InChars - _KeyAndValuePart_TargetLength

					'E.g. /RunFile:<Value>
					Dim _KeyAndValuePart$ = CLAKeyStartChar_ & _CLAKeyToActionMapping.Key & If(_CLAKeyToActionMapping.MustHaveValue, CLAValueStartChar_ & "<Value>", String.Empty)

					'E.g. (Optional) Shows the Log during Interpretation
					Dim _DescriptionPart$ = If(Not _CLAKeyToActionMapping.MustBeSpecified, "(Optional) ", "") & _CLAKeyToActionMapping.Description

					REM Start the Descriptions 30 Chars in, and put a {LineBreak} and {30 Spaces} every 50 chars.
					_CLAHelpDictionaryText &=
						 _KeyAndValuePart.PadRight(_KeyAndValuePart_TargetLength, " "c) &
						_DescriptionPart.ToCharArray().Intersperse((vbCrLf & " "c.Repeat(_KeyAndValuePart_TargetLength)).ToCharArray(), _DescriptionPart_MaxColumnWidth) &
						vbCrLf

				Next

			Else

				REM CLA Help Dictionary Format: CommandLineFormatting=False

				'Description: {Description}
				'
				'Examples:
				'{Examples...}
				'
				'Argument Usage:
				'/RunFile:<Value> → The File to Execute (*.DS)
				'/Debug → (Optional) Shows Debugging Logging Messages. More text can go here, but it wraps around after 80 columns (in total)
				'/ShowLog → (Optional) Shows the Log during Interpretation

				_CLAHelpDictionaryText$ =
				 "Description:" & vbCrLf & Me.CLAHelpDictionary_Description & vbCrLf & vbCrLf &
				 "Examples:" & vbCrLf

				_CLAHelpDictionaryText &= Me.CLAHelpDictionary_Examples.JoinAsStrings(vbCrLf)

				_CLAHelpDictionaryText &= vbCrLf &
				 "Argument Usage: (Keys are case-insensitive)" & vbCrLf

				For Each _CLAKeyToActionMapping As CLAKeyToActionMapping In Me.KeyToActionMappings

					'E.g. /RunFile:<Value>
					Dim _KeyAndValuePart$ = CLAKeyStartChar_ & _CLAKeyToActionMapping.Key & If(_CLAKeyToActionMapping.MustHaveValue, CLAValueStartChar_ & "<Value>", String.Empty)

					'E.g. (Optional) Shows the Log during Interpretation
					Dim _DescriptionPart$ = If(Not _CLAKeyToActionMapping.MustBeSpecified, "(Optional) ", String.Empty) & _CLAKeyToActionMapping.Description

					_CLAHelpDictionaryText &= _KeyAndValuePart & " → " & _DescriptionPart & vbCrLf

				Next

			End If

			REM The OutputDelegate will usually be `AddressOf Console.WriteLine`, to write the CLAs' HD to stdout.
			REM Sometimes though, e.g. in the case of a Windows Application, it might be e.g. `AddressOf MessageBox.Show`.
			Me.OutputDelegate.Invoke(_CLAHelpDictionaryText)

			Return

		End Sub

#End Region

	End Class

End Namespace