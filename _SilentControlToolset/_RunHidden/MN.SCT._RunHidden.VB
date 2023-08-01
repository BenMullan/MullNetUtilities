REM Name:			_RunHidden.exe		(The Score is there, because it's a MetaUtility; a Utility for the other MN-SCT Utilities)
REM Example CLAs:	_RunHidden.exe SaveScreenshot.exe /Single /OutputFile:Shot.PNG
REM Description:	Runs the rest of the command-line Arguments as a new Process, without producing a Window for that process. Any STD-OUT or STD-ERR text is written to "_RunHidden_ProcessOutputText.log". The ExitCode of the _RunHidden process is that of the Child Process.
REM Date:			08-03-2023 (Snowing, sat in Mrs. Ali's Classroom)

Public Module _RunHidden

	Public Const StdOut_AndStdErr_OutputTextFileName$ = "_RunHidden_ProcessOutputText.log"

	REM The ExitCode hereof, is that of the Child Process, if this was run successfully.
	Public Function Main(ByVal _CLAs As [String]()) As Int32
		Try

			REM ↓ Attempt to find the target Binary Image (for the process), simply by treating CLAs[0] as a file path relative to the current directory.
			REM ↓ If this fails to produce an existant file, then run WHERE.EXE to resolve the location of the exe. This could be in e.g. the %PATH%.

			If Not _CLAs.Length >= 1 Then Throw New Exception("No Program-to-run was specified.")
			Dim _ProgramToRun As IO.FileInfo
			Try
				_ProgramToRun = New IO.FileInfo(_CLAs.ElementAt(0))
				If Not _ProgramToRun.Exists Then Throw New Exception("The Program File [" & _ProgramToRun.FullName & "] does not exit or is invalid")
			Catch _Ex As Exception
				_ProgramToRun = MullNet.MetaUtilities.Processes.ResolveFilePath_FromFileName(_CLAs.ElementAt(0))
			End Try

			REM ↓ Gather the CLAs for the Child Process

			Dim _ProgramArguments$ = ""
			If _CLAs.Length > 1 Then
				Dim _ArgumentCLAs As New List(Of String)()
				For _ArgumentCLA_Index% = 1 To (_CLAs.Length - 1) Step +1 : _ArgumentCLAs.Add(_CLAs.ElementAt(_ArgumentCLA_Index)) : Next
				_ProgramArguments = String.Join(" "c, _ArgumentCLAs.ToArray())
			End If

			REM ↓ Instanciate the Process, hiding any Window.
			REM ↓ Log information about the process in the Output file too.
			REM ↓ Return the ExitCode of the Child Process

			With New System.Diagnostics.Process()

				REM ↓ Setup
				.StartInfo.UseShellExecute = False
				.StartInfo.CreateNoWindow = True
				.StartInfo.ErrorDialog = False
				.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
				.StartInfo.FileName = _ProgramToRun.FullName
				.StartInfo.Arguments = _ProgramArguments
				.StartInfo.RedirectStandardOutput = True
				.StartInfo.RedirectStandardError = True

				REM ↓ Run the child Process
				.Start()
				_RunHidden.WriteText_ToChildProcessOutputFile("Info: Started " & _ProgramToRun.FullName.InSquares() & " with arguments ↓" & vbCrLf & String.Join(" "c, _ProgramArguments))
				Dim _SessionID$ = .SessionId.ToString()
				Dim _ProcessOutput$ = .StandardOutput.ReadToEnd()
				Dim _ErrorOutput$ = .StandardError.ReadToEnd()

				REM ↓ Await its exiting
				.WaitForExit()
				Dim _ExitCodeString$ = String.Format("""{0}"" exited with code {1} at {2} after {6}s. The PID was {3} of session {4} on machine ""{5}"".", _ProgramToRun.Name, .ExitCode.ToString(), .ExitTime.TimeOfDay.ToString(), .Id.ToString(), _SessionID, If(.MachineName = ".", Environment.MachineName, .MachineName), (.ExitTime - .StartTime).TotalSeconds.ToString())
				_RunHidden.WriteText_ToChildProcessOutputFile("Info: " & _ExitCodeString)

				REM ↓ Write its Output to the TextFile
				Dim _Process_StdOut_AndOr_StdErr$
				If String.IsNullOrEmpty(_ProcessOutput) AndAlso (Not String.IsNullOrEmpty(_ErrorOutput)) Then : _Process_StdOut_AndOr_StdErr$ = _ErrorOutput
				ElseIf String.IsNullOrEmpty(_ErrorOutput) AndAlso (Not String.IsNullOrEmpty(_ProcessOutput)) Then : _Process_StdOut_AndOr_StdErr$ = _ProcessOutput
				ElseIf (Not String.IsNullOrEmpty(_ErrorOutput)) AndAlso (Not String.IsNullOrEmpty(_ProcessOutput)) Then : _Process_StdOut_AndOr_StdErr$ = _ProcessOutput & vbCrLf & _ErrorOutput
				ElseIf String.IsNullOrEmpty(_ErrorOutput) AndAlso String.IsNullOrEmpty(_ProcessOutput) Then : _Process_StdOut_AndOr_StdErr$ = String.Empty
				Else : Throw New Exception("Unaccounted-for Process Error/Output eventuality")
				End If
				_RunHidden.WriteText_ToChildProcessOutputFile(_ProgramToRun.Name & " Output ↓" & vbCrLf & _Process_StdOut_AndOr_StdErr)

				REM ↓ Exit this _RunHidden.exe Process, with the ExitCode of the Child Process
				Return .ExitCode

			End With

		Catch _Ex As Exception

			REM ↓ Write an Exception Message to the TextFile

			Dim _ExceptionMessageText$ = "_RunHidden Exception: " & _Ex.Message & vbCrLf & _
			"Usage: Specify the Program to run as the first command-line argument, with arguments therefor, thereafter" & vbCrLf & _
			  "Example: _RunHidden WhoAmI" & vbCrLf & _
			  "Example: _RunHidden.exe SaveScreenshot.exe /Single /OutputFile:Shot.PNG" & vbCrLf & _
			"NOTE: _RunHidden.exe returns the same Exit-Code as the process it runs."
			_RunHidden.WriteText_ToChildProcessOutputFile("_RunHidden Error: " & _ExceptionMessageText)

			Return -1

		End Try
	End Function

	''' <summary>Automatically appends two {LineBreak}s. Saves in the same same directory as the _RunHidden.exe Binary.</summary>
	Public Sub WriteText_ToChildProcessOutputFile(ByVal _Text$)
		My.Computer.FileSystem.WriteAllText(IO.Path.Combine((New System.IO.FileInfo(System.Environment.GetCommandLineArgs().ElementAt(0))).DirectoryName, _RunHidden.StdOut_AndStdErr_OutputTextFileName), DateTime.Now.ToString().InSquares() & " ↓" & vbCrLf & _Text & vbCrLf & vbCrLf, append:=True)
	End Sub

End Module