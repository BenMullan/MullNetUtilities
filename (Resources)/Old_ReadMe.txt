╔═════════════════════════════════════════════════════════════╗
║                      MullNet Utilities                      ║
╚═════════════════════════════════════════════════════════════╝

[July 2023: Still very much a work-in-progress]
A set of system, mathematical, and miscellaneous utilities for Windows 7+, written for .NET-4 and Win32/C++.

Notes:
- All the .NET binaries which take command-line arguments, can be called with /? to produce a help-dictionary.
- The SilentControlToolset .NET binaries will never show any windows, apart from if /? is called.
- Any runtime exceptions from the .NET binaries of the SilentControlToolset, are written to a *hidden* file called "_SCT_Errors.log"
- Release Builds are output into the _ReleaseBinaries Folders
- Binaries ending in "32" are non-.NET, pure Win32 C/C++ programs.
- It is useful to show the exit-codes of the SCT processes, by using e.g.: ShowExitCode PlayWav /Wav:"C:\ProgramData\WinaeroTweaker\StartupSoundDefault.wav"
- The MullNet.MetaUtilities.dll project is for utilities-for-the-utilities; it contains e.g. the CLA Manager and compiler extentions.
- The .NET-binaries which reference the MetaUtilities DLL, can be IL-Merged (into just one .exe) using .\ILMergeAll.ps1

↓ Example Command-line Arguments (CLAs)... ↓

System Utilities:
-----------------
AutoEMail.exe			
EmptyFolderDel.exe		
ShowExitCode.exe		rundll32.exe dsquery.dll,OpenQueryWindow
WebcamView.exe

Silent Control Toolset:
-----------------------
ClickAt.exe				/Point:400,500 /DoubleClick (/RightClick or /LeftClick or /MiddleClick)
MaxVol32.exe
MouseGone32.exe
SendKeys.exe			/Keys:"H,Ctrl+" /String:"Hello, World!"
PlayWav.exe				/Wav:"X:\Music.wav" /Repeat:9




ToDo-26022023:
	SessionView:
		New version of SCT\SaveScreenshot
		RunHidden.exe







MullNet Silent Control Toolset (MNSCT.Zip)
------------------------------------------

#Exit Codes:
	0	Ran Successfully
	1	Command-Line Argument Error, successfully logged in .\_SCT_Errors.log
	2	Runtime Error, successfully logged in .\_SCT_Errors.log
	3	An Error occured, but could not be logged in .\_SCT_Errors.log

# 0 is used to mean "forever", or "no limit", or "infinity"

ClipboardText.exe /Set:"Some Text"
ClipboardText.exe /Get:"OutputFile.txt"

MouseGone32.exe # Pure Win32 C; not .NET

Screenshotter.exe /SaveAs:"OutputFile.PNG"

IntervalScreenshotter.exe /OutputFolder:"Screenshots\" /Frequency:5 /OverwriteAfter:30
IntervalScreenshotter.exe /OutputFolder:"Screenshots\" /Frequency:5 /OverwriteAfter:0

SetVol.exe /To:44
SetVol.exe /Mute
SetVol.exe /Unmute

PlayWav.exe /Wav:"BBC.Wav"
PlayWav.exe /Wav:"BBC.Wav" /Repeat:2
PlayWav.exe /Wav:"BBC.Wav" /Repeat:0

Click.exe /At:"500,500"
Click.exe /At:"500,500" /DoubleClick
Click.exe /At:"500,500" /DoubleClick /RightClick
Click.exe /At:"500,500" /DoubleClick /MiddleClick

TypeKeys.exe /String:"Hello, World!"
TypeKeys.exe /Keys:"44,19,86"							# From the Windows.Forms.Keys Enum
TypeKeys.exe /Keys:"B,A,T,RightCtrl+S"
TypeKeys.exe /Keys:"22+44"								# + for Modifier Keys
TypeKeys.exe /Keys:"Alt+F" /ForegroundWindow:0x45687	# HWnd

FullscreenImg.exe /Image:"BSOD.PNG"
FullscreenImg.exe /Image:"BSOD.PNG" /ShowFor:5

FreezeScreen.exe /For:5
FreezeScreen.exe /For:0

WinSeizure.exe /All /KeyLights /Screen /Audio /DVDEdject

#An observation tool; not for active control
DumpWindowBitmaps.exe	#Saves a .bmp of each open Window to a File



















						'Dim _TakeScreenshotThread As New Threading.Thread(
						' Sub()
						'	 MullNet.MetaUtilities.Screenshotting.TakeAndSaveScreenshot(_CurrentShot_FullPath$)
						' End Sub)
						'_TakeScreenshotThread.Start() : _TakeScreenshotThread.Join()









					REM This action is a Loop; it will never end the process itself

					SaveScreenshot.OutputFolder.MustNotBeNothing("An /OutputFolder must be specified for /Interval mode.")
					SaveScreenshot.ScreenshotFrequency.MustNotBeNothing("A /Frequency must be specified for /Interval mode.")

					If Not SaveScreenshot.OutputFolder.Exists Then SaveScreenshot.OutputFolder.Create()
					Dim _NumScreenshotsTaken As UInt64 = 0

					Dim _TakeScreenshotTimer As New System.Timers.Timer(interval:=SaveScreenshot.ScreenshotFrequency.TotalMilliseconds)

					AddHandler _TakeScreenshotTimer.Elapsed, _
					 Sub()
						 REM Determine whether the Maximum Number of Screenshots exists in the folder (0 = never-overwrite)
						 If (SaveScreenshot.MaxScreenshotsToKeepInOutputFolder > 0) Then If (_NumScreenshotsTaken > (SaveScreenshot.MaxScreenshotsToKeepInOutputFolder - 1)) Then _NumScreenshotsTaken = 0

						 REM Take and Save() the Screenshot
						 Dim _CurrentShot_FullPath$ = String.Format("{0}\{1}.PNG", SaveScreenshot.OutputFolder.FullName, _NumScreenshotsTaken.ToString())
						 If IO.File.Exists(_CurrentShot_FullPath$) Then IO.File.Delete(_CurrentShot_FullPath$)

						 Dim _Bitmap As Drawing.Bitmap = MullNet.MetaUtilities.Screenshotting.GetScreenshot()
						 _Bitmap.Save(_CurrentShot_FullPath$)
						 _Bitmap.Dispose()

						 UsefulMethods.ConsoleWriteLineInColour("Successfully saved screenshot as: " & _CurrentShot_FullPath$, ConsoleColor.Blue)

						 REM Wait, until the next Screenshot should be captured
						 Threading.Thread.Sleep(SaveScreenshot.ScreenshotFrequency)
						 _NumScreenshotsTaken += 1UL
					 End Sub