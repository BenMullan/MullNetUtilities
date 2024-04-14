REM File:		Doorman.Main.VB - The entire implementation of Doorman (command-line IO-relayer)
REM Compile:	(Build Solution)
REM Author:		Ben Mullan 2024

Public Module Doorman

	REM Example: echo Ben | dm python .\HelloName.py
	'	[PYTHON.EXE\args.0] .\HelloName.py
	'	[PYTHON.EXE\launch] PID=18164, Session=1
	'	[PYTHON.EXE\stdout] Enter name: Hello, Ben
	'	[PYTHON.EXE\exited] ExitCode=0, RanFor=0.0363765s

	REM Example: dm claoutputter "hello world"
	'	[CLAOUTPUTTER.EXE\args.0] hello world
	'	[CLAOUTPUTTER.EXE\launch] PID=8036, Session=1
	'	[CLAOUTPUTTER.EXE\stdout] ------------------------- Start -------------------------
	'	[CLAOUTPUTTER.EXE\stdout] hello world
	'	[CLAOUTPUTTER.EXE\stdout] -------------------------- End --------------------------
	'	[CLAOUTPUTTER.EXE\exited] ExitCode=0, RanFor=3.1450084s

#Region "Important Objects"

	REM E.g. `python` and `claoutputter` in the examples above
	REM Declared outside the method, as it's WithEvents
	Public WithEvents InnerProcess As New System.Diagnostics.Process()

	REM Points to the executable image file, for the `InnerProcess`
	Public InnerProcess_TargetExecutableFile As IO.FileInfo = Nothing

	REM The Command-line Arguments, fed into the InnerProcess
	Public InnerProcess_CLAs$() = {}

#End Region

#Region "Main()"

	Public Function Main%(ByVal _CLAs$())
		Try

			REM We need at least 1 CLA; the InnerProcess_TargetExecutableFile
			If Not _CLAs.Length >= 1 Then Throw New Exception("No Program-to-run was specified.")

			Try REM Resolve the full file path of the InnerProcess_TargetExecutableFile, from the CLAs

				'These statements may fall into the catch block because:
				'	- _CLAs.ElementAt(0) contains illegal path chars
				'	- _CLAs.ElementAt(0) is a path for a non-existant file

				Doorman.InnerProcess_TargetExecutableFile = New IO.FileInfo(_CLAs.ElementAt(0))
				If Not Doorman.InnerProcess_TargetExecutableFile.Exists Then Throw New Exception("["c & Doorman.InnerProcess_TargetExecutableFile.FullName & "] does not exit in the current working directory")

			Catch _Ex As Exception
				System.Diagnostics.Debug.WriteLine("Using WHERE.EXE program-resolution method because " & _Ex.Message)
				Doorman.InnerProcess_TargetExecutableFile = MullNet.MetaUtilities.Processes.ResolveFullFilePath_FromFileName(_CLAs.ElementAt(0))
			End Try

			REM **BugFix 03042024; Corrected quotes CLAs not being passed to InnerProcess atomically
			REM Collect any CLAs specified *for* the `InnerProcess_TargetExecutableFile`
			'	Wrap each one in quotes, because e.g.
			'		If this was the original command: dm net user "John Smith"
			'		...then Windows(TM) strips the quotes...
			'		...so `_CLAs` looks like: {"net", "user", "John Smith"}
			'		...and if we just Join() 'em by SPACE, that'd be: "net user John Smith"
			'		...and we've now no way of telling that "John" and "Smith" were part of the same original CLA
			'		...so the InnerProcess would receive this CL: net user John Smith
			'
			'		So instead, do e.g.
			'			Original command: dm net user "John Smith"
			'			...then wrap each item in `_CLAs` in quotes...
			'			...resulting in: {"""net""", """user""", """John Smith"""}
			'			...so that the InnerProcess receives CL: "net" "user" "John Smith"
			'
			'	Keep them unquoted for now, so that PrintoutHandlerFor_CLAs dosen't print them with quotes the user didn't put there.
			'	Then when serialising them though, in preperation for assignment to `.Arguments` of the `InnerProcess`, add the quotes.

			Doorman.InnerProcess_CLAs = _CLAs.Skip(1).ToArray()
			Doorman.PrintoutHandlerFor_CLAs.Invoke(InnerProcess_CLAs)

			REM Configure && Start() the `InnerProcess`
			With Doorman.InnerProcess

				.EnableRaisingEvents = True

				With .StartInfo

					.UseShellExecute = False
					.FileName = Doorman.InnerProcess_TargetExecutableFile.FullName
					.Arguments = [String].Join(" "c, Doorman.InnerProcess_CLAs.Select(Of [String])(Function(_CLA$) [String].Format("""{0}""", {_CLA})))

					.RedirectStandardOutput = True
					.RedirectStandardError = True
					.RedirectStandardInput = False

				End With

				AddHandler .OutputDataReceived, Doorman.PrintoutHandlerFor_StdOut
				AddHandler .ErrorDataReceived, Doorman.PrintoutHandlerFor_StdErr

				.Start() : Doorman.PrintoutHandlerFor_StartInfo.Invoke(Doorman.InnerProcess)
				.BeginOutputReadLine() : .BeginErrorReadLine()
				.WaitForExit() : Doorman.PrintoutHandlerFor_ExitInfo.Invoke(Doorman.InnerProcess)

				Return .ExitCode

			End With

		Catch _Ex As Exception

			ConsoleWriteLineInColour("Exception: " & _Ex.Message, ConsoleColor.Red)

			UsefulMethods.ConsoleWriteLineInColour(
			 <String>
Doorman; an IO relayer for command-line programs. (v<%= System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString() %>)
Usage: Specify the Program to run as the first command-line argument, with arguments therefor, thereafter.
Doorman's exit code is that of the inner process.

Examples:
<%= System.Reflection.Assembly.GetExecutingAssembly().GetName().Name %> whoami
<%= System.Reflection.Assembly.GetExecutingAssembly().GetName().Name %> net user "John Smith"
echo Ben | <%= System.Reflection.Assembly.GetExecutingAssembly().GetName().Name %> python .\HelloName.py
			  </String>.Value.Trim(),
			 ConsoleColor.Blue
			)

			Return -1

		End Try
	End Function

#End Region

#Region "Print-out Handlers"

	Public PrintoutHandlerFor_StartInfo As System.Action(Of Diagnostics.Process) = _
	  Sub(_Started_InnerProcess As Diagnostics.Process)

		  REM If the process has already ended, the PID and SessionID will not be avaliable
		  Dim _PID$ = "(Could not resolve)", _Session$ = "(Could not resolve)", _StartInfoResolutionErrorMsg$ = ""
		  Try : _PID = _Started_InnerProcess.Id.ToString() : _Session = _Started_InnerProcess.SessionId.ToString()
		  Catch _Ex As Exception When True : _StartInfoResolutionErrorMsg = " " & _Ex.Message.InPointies() : End Try

		  UsefulMethods.ConsoleWriteLineInColour([String].Format("[{0}\launch] PID={1}, Session={2}{3}", {InnerProcess_TargetExecutableFile.Name, _PID, _Session, _StartInfoResolutionErrorMsg}), ConsoleColor.Blue)

	  End Sub

	Public PrintoutHandlerFor_CLAs As System.Action(Of [String]()) =
	 Sub(_InnerProcess_CLAs As [String]())
		 For _CLA_Index As Int16 = 0 To Convert.ToInt16(_InnerProcess_CLAs.Length - 1) Step +1
			 UsefulMethods.ConsoleWriteLineInColour([String].Format("[{0}\args.{1}] {2}", {InnerProcess_TargetExecutableFile.Name, _CLA_Index.ToString(), _InnerProcess_CLAs.ElementAt(_CLA_Index)}), ConsoleColor.Magenta)
		 Next
	 End Sub

	Public PrintoutHandlerFor_StdOut As New DataReceivedEventHandler(
	 Sub(_Sender As [Object], _DataReceivedEA As System.Diagnostics.DataReceivedEventArgs)
		 If _DataReceivedEA.Data Is Nothing Then : Return
		 Else
			 UsefulMethods.ConsoleWriteInColour([String].Format("[{0}\stdout] ", {InnerProcess_TargetExecutableFile.Name}), ConsoleColor.Green)
			 Global.System.Console.Out.Write([String].Format("{0}{1}", {_DataReceivedEA.Data, If(Not _DataReceivedEA.Data.EndsWith(vbCrLf), vbCrLf, [String].Empty)}))
		 End If
	 End Sub
	)

	Public PrintoutHandlerFor_StdErr As New DataReceivedEventHandler(
	 Sub(_Sender As [Object], _DataReceivedEA As System.Diagnostics.DataReceivedEventArgs)
		 If _DataReceivedEA.Data Is Nothing Then : Return
		 Else
			 UsefulMethods.ConsoleErrorWriteInColour([String].Format("[{0}\stderr] ", {InnerProcess_TargetExecutableFile.Name}), ConsoleColor.Red)
			 Global.System.Console.Error.Write([String].Format("{0}{1}", {_DataReceivedEA.Data, If(Not _DataReceivedEA.Data.EndsWith(vbCrLf), vbCrLf, [String].Empty)}))
		 End If
	 End Sub
	)

	Public PrintoutHandlerFor_StdIn As System.Func(Of [String]) = _
		Function() As [String]
			Throw New NotImplementedException()
		End Function

	Public PrintoutHandlerFor_ExitInfo As System.Action(Of Diagnostics.Process) = _
		Sub(_Finished_InnerProcess As Diagnostics.Process)
			UsefulMethods.ConsoleWriteLineInColour([String].Format("[{0}\exited] ExitCode={1}, RanFor={2}s", {InnerProcess_TargetExecutableFile.Name, _Finished_InnerProcess.ExitCode.ToString(), (_Finished_InnerProcess.ExitTime - _Finished_InnerProcess.StartTime).TotalSeconds.ToString()}), ConsoleColor.Blue)
		End Sub

#End Region

End Module