Namespace MetaUtilities
	Public Module CMDApplicationUtilities

		REM Common Process Exit Codes
		Public Const ExitCode_GenericFatalError As Int32 = -1
		Public Const ExitCode_RanSuccessfully As Int32 = 0

		'!!!!!!!!!!!!!!!!!!!!!!!!!! Remove these; we're now using the RunHidden.exe mechanism !!!!!!!!!!!!!!!!!!!!!!!!
		REM Specific to the MN-SCT
		Public Const ExitCode_CLAsError As Int32 = 1
		Public Const ExitCode_RuntimeError As Int32 = 2
		Public Const ExitCode_Error_Unlogged As Int32 = 3

		REM The MN-SilentControlToolset Errors File:
		Public Const MNSCT_ErrorsFile_Name$ = "_SCT_Errors.log"

		''' <summary>Writes the Exception to the Console in Red, and then Ends the Process with ExitCode -1</summary>
		Public Sub WriteFatalExceptionToConsole_AndEndProcess(ByRef _Exception As System.Exception)
			UsefulMethods.ConsoleErrorWriteLineInColour("Exception: " & _Exception.Message, ConsoleColor.Red)
			System.Environment.Exit(exitCode:=CMDApplicationUtilities.ExitCode_GenericFatalError)
		End Sub

#Region "Remove these - They shouldn't be used anymore..."

		''' <summary>
		''' Writes the Exception - along with the _MNSCTBinaryName and a Timestamp - to _SCT_Errors.log, before ending the process with ExitCode 2 or 3
		''' </summary>
		''' <param name="_MNSCTBinaryName">E.g. "PlayWav.exe"</param>
		''' <param name="_Exception"></param>
		''' <remarks></remarks>
		Public Sub WriteRuntimeExeptionToSCTErrorsFile_AndEndProcess(ByVal _MNSCTBinaryName$, ByRef _Exception As System.Exception)
			Try
				Dim _NewText_ForErrorLog$ = _MNSCTBinaryName.PadRight(20) & DateTime.Now.ToString() & " Runtime: " & _Exception.GetType().Name.InSquares() & " "c & _Exception.Message & vbCrLf & vbCrLf
				My.Computer.FileSystem.WriteAllText(CMDApplicationUtilities.MNSCT_ErrorsFile_Name, _NewText_ForErrorLog, append:=True)
				System.Environment.Exit(exitCode:=CMDApplicationUtilities.ExitCode_RuntimeError)
			Catch
				'The _Exception couldn't even be logged
				System.Environment.Exit(exitCode:=CMDApplicationUtilities.ExitCode_Error_Unlogged)
			End Try
		End Sub

		''' <summary>
		''' Writes the Exception - along with the _MNSCTBinaryName and a Timestamp - to _SCT_Errors.log, before ending the process with ExitCode 1 or 3
		''' </summary>
		''' <param name="_MNSCTBinaryName">E.g. "PlayWav.exe"</param>
		''' <param name="_Exception"></param>
		''' <remarks></remarks>
		Public Sub WriteCLAsExeptionToSCTErrorsFile_AndEndProcess(ByVal _MNSCTBinaryName$, ByRef _Exception As System.Exception)
			Try
				Dim _NewText_ForErrorLog$ = _MNSCTBinaryName.PadRight(20) & DateTime.Now.ToString() & " Command-line Arguments: " & _Exception.GetType().Name.InSquares() & " "c & _Exception.Message & vbCrLf & vbCrLf
				My.Computer.FileSystem.WriteAllText(CMDApplicationUtilities.MNSCT_ErrorsFile_Name, _NewText_ForErrorLog, append:=True)
				System.Environment.Exit(exitCode:=CMDApplicationUtilities.ExitCode_CLAsError)
			Catch
				'The _Exception couldn't even be logged
				System.Environment.Exit(exitCode:=CMDApplicationUtilities.ExitCode_Error_Unlogged)
			End Try
		End Sub

#End Region

		''' <summary>Writes the Exception - along with a Timestamp - to the Console Error, before ending the process with ExitCode 1</summary>
		Public Sub WriteCLAsExeptionToConsole_AndEndProcess(ByRef _Exception As System.Exception)
			UsefulMethods.ConsoleErrorWriteLineInColour(DateTime.Now.ToString() & " Command-Line Arguments Exception: " & _Exception.ToString(), ConsoleColor.Red)
			System.Environment.Exit(exitCode:=CMDApplicationUtilities.ExitCode_CLAsError)
		End Sub

		''' <summary>Writes the Exception - along with a Timestamp - to the Console Error, before ending the process with ExitCode 2</summary>
		Public Sub WriteRuntimeExeptionToConsole_AndEndProcess(ByRef _Exception As System.Exception)
			UsefulMethods.ConsoleErrorWriteLineInColour(DateTime.Now.ToString() & " Runtime Exception: " & _Exception.ToString(), ConsoleColor.Red)
			System.Environment.Exit(exitCode:=CMDApplicationUtilities.ExitCode_RuntimeError)
		End Sub

		''' <summary>
		''' Sets the .OutputDelegate, and .UseCommandLineFormatting_ForHelpDictionary
		''' </summary>
		''' <param name="_CLAManager"></param>
		''' <remarks></remarks>
		Public Sub ConfigureCLAManager_ForSCTOutputDelegate(ByRef _CLAManager As CLAManager)
			_CLAManager.OutputDelegate = Sub(_String$) MsgBox(_String, MsgBoxStyle.Information, "MN-SCT Command-Line")
			_CLAManager.UseCommandLineFormatting_ForHelpDictionary = False
			Return
		End Sub

	End Module
End Namespace