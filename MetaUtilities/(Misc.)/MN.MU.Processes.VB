Namespace MetaUtilities
	Public Module Processes

		''' <summary>Uses WHERE.EXE to resolve the path</summary>
		Public Function ResolveFilePath_FromFileName(ByVal _FileName$) As IO.FileInfo

			REM If there are multiple instances of the file in-scope, they'll be returned in the format:
			REM		C:\Windows\System32\hh.exe {vbCrLf} C:\Windows\hh.exe  

			Dim _WhereExe_Output$ = Global.MullNet.MetaUtilities.Processes.RunProcess_AndReturnOutput(Environment.GetEnvironmentVariable("WINDIR") & "\System32\WHERE.EXE", _FileName).Trim()

			If _WhereExe_Output.StartsWith("INFO") OrElse _WhereExe_Output.StartsWith("ERROR") Then Throw New Exception("WHERE.EXE reported that the file does not exist or is invalid")
			If _WhereExe_Output.Contains(vbCrLf) Then _WhereExe_Output = _WhereExe_Output.Split({vbCrLf}, StringSplitOptions.RemoveEmptyEntries).ElementAt(0).Trim()

			Return (New IO.FileInfo(_WhereExe_Output))
		End Function

		''' <summary>Returns both the STDOUT and STDERR</summary>
		Public Function RunProcess_AndReturnOutput(ByVal _Process_FileName$, Optional ByVal _Process_CLAs$ = "") As String
			With New System.Diagnostics.Process()

				.StartInfo.FileName = _Process_FileName
				.StartInfo.Arguments = _Process_CLAs
				.StartInfo.UseShellExecute = False
				.StartInfo.RedirectStandardOutput = True
				.StartInfo.RedirectStandardError = True

				.Start()
				Dim _ProcessOutput$ = .StandardOutput.ReadToEnd()
				Dim _ErrorOutput$ = .StandardError.ReadToEnd()
				.WaitForExit()

				If String.IsNullOrEmpty(_ProcessOutput) AndAlso (Not String.IsNullOrEmpty(_ErrorOutput)) Then : Return _ErrorOutput
				ElseIf String.IsNullOrEmpty(_ErrorOutput) AndAlso (Not String.IsNullOrEmpty(_ProcessOutput)) Then : Return _ProcessOutput
				ElseIf (Not String.IsNullOrEmpty(_ErrorOutput)) AndAlso (Not String.IsNullOrEmpty(_ProcessOutput)) Then : Return _ProcessOutput & vbCrLf & _ErrorOutput
				ElseIf String.IsNullOrEmpty(_ErrorOutput) AndAlso String.IsNullOrEmpty(_ProcessOutput) Then : Return String.Empty
				Else : Throw New Exception("Unaccounted-for Process Error/Output eventuality")
				End If

			End With
		End Function

		''' <summary>The ExitCode can be resolved from the input Process Object</summary>
		Public Function RunProcess_AndReturnOutput(ByRef _InputProcess As Process) As [String]
			With _InputProcess

				.StartInfo.UseShellExecute = False
				.StartInfo.RedirectStandardOutput = True
				.StartInfo.RedirectStandardError = True

				.Start()
				Dim _ProcessOutput$ = .StandardOutput.ReadToEnd()
				Dim _ErrorOutput$ = .StandardError.ReadToEnd()
				.WaitForExit()

				Dim _Process_StdOut_AndStdErr$
				If String.IsNullOrEmpty(_ProcessOutput) AndAlso (Not String.IsNullOrEmpty(_ErrorOutput)) Then : _Process_StdOut_AndStdErr$ = _ErrorOutput
				ElseIf String.IsNullOrEmpty(_ErrorOutput) AndAlso (Not String.IsNullOrEmpty(_ProcessOutput)) Then : _Process_StdOut_AndStdErr$ = _ProcessOutput
				ElseIf (Not String.IsNullOrEmpty(_ErrorOutput)) AndAlso (Not String.IsNullOrEmpty(_ProcessOutput)) Then : _Process_StdOut_AndStdErr$ = _ProcessOutput & vbCrLf & _ErrorOutput
				ElseIf String.IsNullOrEmpty(_ErrorOutput) AndAlso String.IsNullOrEmpty(_ProcessOutput) Then : _Process_StdOut_AndStdErr$ = String.Empty
				Else : Throw New Exception("Unaccounted-for Process Error/Output eventuality")
				End If

				Return _Process_StdOut_AndStdErr

			End With
		End Function

	End Module
End Namespace