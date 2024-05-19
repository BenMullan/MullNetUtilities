Public Module SaveScreenshot

	Public Enum ActionWhenReady As [Byte]
		Unspecified
		TakeSingleScreenshot
		TakeScreenshotsAtInterval
	End Enum

	''' <summary>The Process will exit after the Number of failed Screenshots bypasses this threshold</summary>
	Public Const MaxErrorCount_BeforeExit As [Byte] = 30US

	Public WhenReadyAction As ActionWhenReady = ActionWhenReady.Unspecified
	Public OutputFile As IO.FileInfo = Nothing
	Public OutputFolder As IO.DirectoryInfo = Nothing
	Public ScreenshotFrequency As TimeSpan = Nothing
	Public MaxScreenshotsToKeepInOutputFolder As UInt64 = 0

	Public Function Main(ByVal _CLAs As [String]()) As Int32
		Try '...To understand the Command-Line Arguments

			Dim _CLAManager As New MullNet.MetaUtilities.CLAManager("MullNet SilentControlToolset - SaveScreenshot.exe" & vbCrLf & "Saves a screenshot to disk, optionally repeating the capture every few seconds.") With {
			  .CLAHelpDictionary_Examples = {
			  "SaveScreenshot /Single /OutputFile:""Shot.PNG""",
			  "SaveScreenshot /Interval /OutputFolder:""Shots"" /Frequency:1 /OverwriteAfter:30",
			  "SaveScreenshot /Interval /OutputFolder:""F:\Output\Shots"" /Frequency:60 /OverwriteAfter:0"
			  }
			} 'Possibly add /ExitAfter:* in the future.

			_CLAManager.KeyToActionMappings.AddRange({
				New CLAManager.CLAKeyToActionMapping("Single", Sub(_Value$) SaveScreenshot.WhenReadyAction = ActionWhenReady.TakeSingleScreenshot) With {.MustBeSpecified = False, .MustHaveValue = False, .Description = "[Action] Indicates that just one Screenshot it to be saved, under /OutputFile."},
				New CLAManager.CLAKeyToActionMapping("Interval", Sub(_Value$) SaveScreenshot.WhenReadyAction = ActionWhenReady.TakeScreenshotsAtInterval) With {.MustBeSpecified = False, .MustHaveValue = False, .Description = "[Action] Indicates that a Screenshot is to be taken every /Frequency seconds, and saved in the /OutputFolder."},
				New CLAManager.CLAKeyToActionMapping("OutputFile", Sub(_Value$) SaveScreenshot.OutputFile = New IO.FileInfo(_Value)) With {.MustBeSpecified = False, .MustHaveValue = True, .Description = "[Datum] (Single-Mode) The File name under which to save a singular Screenshot."},
				New CLAManager.CLAKeyToActionMapping("OutputFolder", Sub(_Value$) SaveScreenshot.OutputFolder = New IO.DirectoryInfo(If(_Value.EndsWith("\"c), _Value.Substring(startIndex:=0, length:=_Value.Length - 1), _Value))) With {.MustBeSpecified = False, .MustHaveValue = True, .Description = "[Datum] (Interval-Mode) The Path to a directory, into which to save multiple screenshots. WARNING: Windows treats \"" as a command-line escape sequence for including a backslash inside quotes, so this datum should be specified in the form /OutputFolder:""Shots"", instead of /OutputFolder:""Shots\""."},
				New CLAManager.CLAKeyToActionMapping("Frequency", Sub(_Value$) SaveScreenshot.ScreenshotFrequency = TimeSpan.FromSeconds(Convert.ToDouble(_Value))) With {.MustBeSpecified = False, .MustHaveValue = True, .Description = "[Datum] (Interval-Mode) The Frequency in seconds, at which to take a screenshot, and save it under the /OutputFolder."},
				New CLAManager.CLAKeyToActionMapping("OverwriteAfter", Sub(_Value$) SaveScreenshot.MaxScreenshotsToKeepInOutputFolder = Convert.ToUInt64(_Value)) With {.MustBeSpecified = False, .MustHaveValue = True, .Description = "[Datum] (Interval-Mode) The number of Screenshots to save into the OutputFolder, before overwriting the oldest one with each new screenshot. The default of ""0"" means ""Never Overwrite""."}
			})

			_CLAManager.OutputDelegate = AddressOf Console.WriteLine
			_CLAManager.UseCommandLineFormatting_ForHelpDictionary = True
			_CLAManager.RunForTheseCLAs(_CLAs)

		Catch _CLAsException As Exception : CMDApplicationUtilities.WriteCLAsExeptionToConsole_AndEndProcess(_CLAsException) : End Try

		Try '...To perform the program's function

			If SaveScreenshot.WhenReadyAction = ActionWhenReady.Unspecified Then Throw New Exception("An Action must be specified. Use /Single or /Interval.")

			Select Case SaveScreenshot.WhenReadyAction
				Case ActionWhenReady.TakeSingleScreenshot

					If Not SaveScreenshot.OutputFile.MustNotBeNothing("An /OutputFile must be specified for /Single mode.").Directory.Exists Then SaveScreenshot.OutputFile.Directory.Create()
					MullNet.MetaUtilities.Screenshotting.GetScreenshot().Save(SaveScreenshot.OutputFile.FullName)
					UsefulMethods.ConsoleWriteLineInColour("Successfully saved screenshot as: " & SaveScreenshot.OutputFile.FullName, ConsoleColor.Blue)

				Case ActionWhenReady.TakeScreenshotsAtInterval

					REM This action is a Loop; it will never end the process itself

					SaveScreenshot.OutputFolder.MustNotBeNothing("An /OutputFolder must be specified for /Interval mode.")
					SaveScreenshot.ScreenshotFrequency.MustNotBeNothing("A /Frequency must be specified for /Interval mode.")

					If Not SaveScreenshot.OutputFolder.Exists Then SaveScreenshot.OutputFolder.Create()
					Dim _NumScreenshotsTaken As UInt64 = 0
					Dim _ConsecutiveScreenshotErrors As New List(Of Exception)()

					Do
						Try

							REM Determine whether the Maximum Number of Screenshots exists in the folder (0 = never-overwrite)
							If SaveScreenshot.MaxScreenshotsToKeepInOutputFolder > 0 Then If _NumScreenshotsTaken > (SaveScreenshot.MaxScreenshotsToKeepInOutputFolder - 1) Then _NumScreenshotsTaken = 0

							REM Take and Save() the Screenshot
							Dim _CurrentShot_FullPath$ = String.Format("{0}\{1}.PNG", SaveScreenshot.OutputFolder.FullName, (_NumScreenshotsTaken + 1).ToString())
							If IO.File.Exists(_CurrentShot_FullPath$) Then IO.File.Delete(_CurrentShot_FullPath$)
							MullNet.MetaUtilities.Screenshotting.TakeAndSaveScreenshot(_CurrentShot_FullPath$)
							UsefulMethods.ConsoleWriteLineInColour("Successfully saved screenshot as: " & _CurrentShot_FullPath$, ConsoleColor.Blue)

							REM If we've just run all of those lines successfully, then there was no error
							_ConsecutiveScreenshotErrors.Clear()
							_NumScreenshotsTaken += 1UL

						Catch _ScreenshotException As Exception
							_ConsecutiveScreenshotErrors.Add(_ScreenshotException)
							UsefulMethods.ConsoleErrorWriteLineInColour("Screenshot Error (" & _ConsecutiveScreenshotErrors.Count.ToString() & "): " & _ConsecutiveScreenshotErrors.Last().ToString() & vbCrLf, ConsoleColor.Red)
							If _ConsecutiveScreenshotErrors.Count > (MaxErrorCount_BeforeExit - 1) Then CMDApplicationUtilities.WriteRuntimeExeptionToConsole_AndEndProcess(New Exception("The Maximum permitted number of consecutive screenshot errors (" & MaxErrorCount_BeforeExit.ToString() & ") has been exceeded; the process has been ended. The final error to be caught was: " & _ScreenshotException.ToString(), _ScreenshotException))
						End Try

						REM Wait, until the next Screenshot should be captured (even if there was an Exception during the previous capture)
						Threading.Thread.Sleep(SaveScreenshot.ScreenshotFrequency)

					Loop

				Case Else : Throw New Exception("Unaccounted-for Action: " & SaveScreenshot.WhenReadyAction.ToString())
			End Select

			Return Global.[MullNet].MetaUtilities.CMDApplicationUtilities.ExitCode_RanSuccessfully

		Catch _RuntimeException As Exception : CMDApplicationUtilities.WriteRuntimeExeptionToConsole_AndEndProcess(_RuntimeException) : End Try
		Throw New Exception("This line is never hit, because the process is ended beforehand; it's just here to keep VS happy.")
	End Function

End Module