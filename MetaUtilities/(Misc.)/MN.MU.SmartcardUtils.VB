Namespace MetaUtilities

	''' <summary>Contains logic for interfacing with PCSC Smartcards and Readers</summary>
	Public Module SmartcardUtils

		''' <summary>An immutable representation of data read from an EMV Smartcard; like an ISO file of a smartcard.</summary>
		Public Class SmartcardDataSnapshot

			Public ReadOnly CardholderName$, BankName$



		End Class

		''' <summary>When a LibPCSC.PCSCReader is instanciated, it starts a card-insertion-listening thread. If this encounters an exception, here's a handler for it.</summary>
		Private ReadOnly SmartcardInsertionException_Handler_ As Action(Of Exception) _
			= Sub(_SmartcardInsertionEx As Exception)
				  UsefulMethods.ConsoleErrorWriteLineInColour($"Smartcard Insertion Error: {_SmartcardInsertionEx.Message}", ConsoleColor.Red)
				  System.Environment.Exit(exitCode:=2)
			  End Sub

		''' <summary>Returns the names of all installed PCSC Smartcard Readers on the current system</summary>
		Public ReadOnly Property AvaliableSmartcardReaders As [String]()
			Get
				Dim _SmartcardReader As New LibPCSC.PCSCReader() With {.CardInsertionException_Handler = SmartcardUtils.SmartcardInsertionException_Handler_}
				Return _SmartcardReader.AvaliableReaders.ToArray()
			End Get
		End Property

		''' <summary>Sends a series of APDU-Commands to the Smartcard in the specified reader, to build a SmartcardDataSnapshot</summary>
		Public Function ReadSmartcard(ByVal _TargetReader_Index As [Byte]) As SmartcardUtils.SmartcardDataSnapshot

			Dim _SmartcardReader As New LibPCSC.PCSCReader() With {.CardInsertionException_Handler = SmartcardUtils.SmartcardInsertionException_Handler_}

			Return Nothing

		End Function

	End Module

End Namespace