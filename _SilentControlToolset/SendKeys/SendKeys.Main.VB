Public Module SendKeys

	Public Const MSToWaitAfterSettingForegroundWindow As Int32 = 100%

	Public SpecifiedKeysSequence As [String] = Nothing
	Public DelayBeforeSendingKeystrokesMS As UInt32 = 0
	Public TargetWindow_IntPtr As System.IntPtr? = Nothing
	Public UseSendWait As [Boolean] = True

	Public Function Main(ByVal _CLAs As [String]()) As Int32
		Try	'...To understand the Command-Line Arguments

			Dim _CLAManager As New MullNet.MetaUtilities.CLAManager("MullNet SilentControlToolset - SendKeys.exe" & vbCrLf & "Sends Keystrokes on the current Session to the active Window.") With {
			  .CLAHelpDictionary_Examples = {
			  "SendKeys /Keys:""HELLO""",
			  "SendKeys /Keys:""^+C""",
			  "SendKeys /Keys:""This is some lovley text!"" /ForegroundWindow:0x8c24a5 /SendWait"
			  }
			}

			_CLAManager.KeyToActionMappings.AddRange({
			   (New CLAManager.CLAKeyToActionMapping("Keys", Sub(_Value$) SendKeys.SpecifiedKeysSequence = _Value) With {.MustBeSpecified = True, .MustHaveValue = True, .Description = "[Datum] The [keys] argument for SendKeys.Send() or SendKeys.SendWait()"}),
			   (New CLAManager.CLAKeyToActionMapping("Delay", Sub(_Value$) SendKeys.DelayBeforeSendingKeystrokesMS = _Value.Extract_UInt32()) With {.MustBeSpecified = False, .MustHaveValue = True, .Description = "[Datum] The number of MilliSeconds to wait, after setting the ForegroundWindow (if one is specified), but before sending the Keystrokes."}),
			   (New CLAManager.CLAKeyToActionMapping("ForegroundWindow", Sub(_Value$) SendKeys.TargetWindow_IntPtr = New IntPtr(_Value.Extract_UInt32())) With {.MustBeSpecified = False, .MustHaveValue = True, .Description = "[Datum] The hexadecimal HWND value, of the Window to Focus before sending the KeyStrokes. Use ListWindows.exe of the MN-SCT, or SendKeysGUI.exe of the MN-SysUtils, to view avaliable Windows from their Titles."}),
			   (New CLAManager.CLAKeyToActionMapping("NoWait", Sub(_Value$) SendKeys.UseSendWait = False) With {.MustBeSpecified = False, .MustHaveValue = False, .Description = "[Flag] Indicates that SendKeys.Send() should be used, instead of SendKeys.SendWait()"})
			})

			CMDApplicationUtilities.ConfigureCLAManager_ForSCTOutputDelegate(_CLAManager)
			_CLAManager.RunForTheseCLAs(_CLAs)

		Catch _CLAsException As Exception : MullNet.MetaUtilities.CMDApplicationUtilities.WriteCLAsExeptionToSCTErrorsFile_AndEndProcess(GetType(SendKeys).Name, _CLAsException) : End Try

		Try	'...To perform the program's function

			REM Set the Foreground window if instructed to
			If SendKeys.TargetWindow_IntPtr IsNot Nothing Then
				UsefulMethods.SetForegroundWindow(SendKeys.TargetWindow_IntPtr.Value)
				Threading.Thread.Sleep(MSToWaitAfterSettingForegroundWindow)
			End If

			REM Wait {Delay} MS (which is 0, unless reassigned by the CLA)
			Threading.Thread.Sleep(CInt(SendKeys.DelayBeforeSendingKeystrokesMS))

			REM Call the appropriate SendKeys Function
			If SendKeys.UseSendWait Then : Windows.Forms.SendKeys.SendWait(SendKeys.SpecifiedKeysSequence)
			Else : Windows.Forms.SendKeys.Send(SendKeys.SpecifiedKeysSequence) : End If

			Return Global.[MullNet].MetaUtilities.CMDApplicationUtilities.ExitCode_RanSuccessfully

		Catch _RuntimeException As Exception : MullNet.MetaUtilities.CMDApplicationUtilities.WriteRuntimeExeptionToSCTErrorsFile_AndEndProcess(GetType(SendKeys).Name, _RuntimeException) : Throw New Exception("This is never Throw, because the previous method call ends the process") : End Try
	End Function

End Module