Module ShowExitCode

	Public Function Main(ByVal _CLAs$()) As Int32
		Try

			REM ↓ Attempt to find the target Binary Image (for the process), simply by treating CLAs[0] as a file path relative to the current directory.
			REM ↓ If this fails to produce an existant file, then run WHERE.EXE to resolve the location of the exe. This could be in e.g. the %PATH%.

			If Not _CLAs.Length >= 1 Then Throw New Exception("No Program-to-run was specified.")
			Dim _ProgramToRun As IO.FileInfo
			Try
				_ProgramToRun = New IO.FileInfo(_CLAs.ElementAt(0))
				If Not _ProgramToRun.Exists Then Throw New Exception("The Program File [" & _ProgramToRun.FullName & "] does not exit or is invalid")
			Catch _Ex As Exception
				_ProgramToRun = MullNet.MetaUtilities.Processes.ResolveFullFilePath_FromFileName(_CLAs.ElementAt(0))
			End Try

			Dim _ProgramArguments$ = ""
			If _CLAs.Length > 1 Then
				Dim _ArgumentCLAs As New List(Of [String])()
				For _ArgumentCLA_Index% = 1 To (_CLAs.Length - 1) Step +1
					_ArgumentCLAs.Add(_CLAs.ElementAt(_ArgumentCLA_Index))
				Next
				_ProgramArguments = String.Join(" "c, _ArgumentCLAs.ToArray())
			End If

			With New Process()
				.StartInfo.UseShellExecute = False
				.StartInfo.FileName = _ProgramToRun.FullName
				.StartInfo.Arguments = _ProgramArguments
				.Start()
				Dim _SessionID$ = .SessionId.ToString()
				.WaitForExit()
				Dim _ExitCodeString$ = String.Format("""{0}"" exited with code {1} at {2} after {6}s. The PID was {3} of session {4} on machine ""{5}"".", _ProgramToRun.Name, .ExitCode.ToString(), .ExitTime.TimeOfDay.ToString(), .Id.ToString(), _SessionID, If(.MachineName = ".", Environment.MachineName, .MachineName), (.ExitTime - .StartTime).TotalSeconds.ToString())
				ConsoleWriteLineInColour(_ExitCodeString, ConsoleColor.DarkGreen)
				Return .ExitCode
			End With

		Catch _Ex As Exception
			ConsoleWriteLineInColour("ShowExitCode Exception: " & _Ex.Message & vbCrLf, ConsoleColor.Red)
			ConsoleWriteLineInColour("Usage: Specify the Program to run as the first command-line argument, with arguments therefor, thereafter" & vbCrLf & _
			  "Example: ShowExitCode WhoAmI" & vbCrLf & _
			  "Example: ShowExitCode.exe rundll32.exe dsquery.dll,OpenQueryWindow",
			  ConsoleColor.Blue)
			ConsoleWriteLineInColour("ShowExitCode.exe returns the same Exit-Code as the process it runs.", ConsoleColor.Magenta)
			Return -1
		End Try
	End Function

End Module