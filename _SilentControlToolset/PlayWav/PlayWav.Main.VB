Module PlayWav

	Public TargetWavFile As IO.FileInfo = Nothing
	Public NumberOfTimesToPlayWav As UInt32 = 1, PlayForever As [Boolean] = False
	Public DelayBeforeEachPlaybackMS As UInt32 = 0

	Public Function Main(ByVal _CLAs As [String]()) As Int32
		Try	'...To understand the Command-Line Arguments

			Dim _CLAManager As New MullNet.MetaUtilities.CLAManager("MullNet SilentControlToolset - PlayWav.exe" & vbCrLf & "Plays a .wav file in the background, optionally pausing and looping a specified number of times.") With {
			  .CLAHelpDictionary_Examples = {
			  "PlayWav /Wav:""Wombles.wav""",
			  "PlayWav /Wav:""X:\Wombles.wav"" /Repeat:3",
			  "PlayWav /Wav:""C:\Users\Ryan\Music\Lion.Wav"" /Repeat:Forever",
			  "PlayWav /Wav:""HH.WAV"" /Repeat:9 /DelayBeforeEachPlayback:5000"
			  }
			}

			_CLAManager.KeyToActionMappings.AddRange({
			   (New CLAManager.CLAKeyToActionMapping("Wav", Sub(_Value$) PlayWav.TargetWavFile = New IO.FileInfo(_Value)) With {.MustBeSpecified = True, .MustHaveValue = True, .Description = "[Datum] The target .wav File to play."}),
			   (New CLAManager.CLAKeyToActionMapping("Repeat", Sub(_Value$) If _Value.Trim().ToUpper() = "FOREVER" Then PlayWav.PlayForever = True Else PlayWav.NumberOfTimesToPlayWav = _Value.Extract_UInt32()) With {.MustBeSpecified = False, .MustHaveValue = True, .Description = "[Datum] The number of times to play the File. Can either be ""Forever"", or a positive integer. Default: 1."}),
			   (New CLAManager.CLAKeyToActionMapping("DelayBeforeEachPlayback", Sub(_Value$) PlayWav.DelayBeforeEachPlaybackMS = _Value.Extract_UInt32()) With {.MustBeSpecified = False, .MustHaveValue = True, .Description = "[Datum] The number of MilliSeconds to wait for, before each playback of the target .wav File. Default: 0."})
			})

			CMDApplicationUtilities.ConfigureCLAManager_ForSCTOutputDelegate(_CLAManager)
			_CLAManager.RunForTheseCLAs(_CLAs)

		Catch _CLAsException As Exception : MullNet.MetaUtilities.CMDApplicationUtilities.WriteCLAsExeptionToSCTErrorsFile_AndEndProcess(GetType(PlayWav).Name, _CLAsException) : End Try

		Try	'...To perform the program's function

			If Not PlayWav.TargetWavFile.Exists Then Throw New IO.FileNotFoundException("The Wav File was not found.", PlayWav.TargetWavFile.FullName)
			Dim _SoundPlayer As New Media.SoundPlayer(PlayWav.TargetWavFile.FullName)

			For _PlaybackCount As UInt32 = 0 To (PlayWav.NumberOfTimesToPlayWav - 1UI) Step If(PlayWav.PlayForever, +0UI, +1US)
				Threading.Thread.Sleep(CInt(PlayWav.DelayBeforeEachPlaybackMS))
				_SoundPlayer.PlaySync()
			Next

			Return Global.[MullNet].MetaUtilities.CMDApplicationUtilities.ExitCode_RanSuccessfully

		Catch _RuntimeException As Exception : MullNet.MetaUtilities.CMDApplicationUtilities.WriteRuntimeExeptionToSCTErrorsFile_AndEndProcess(GetType(PlayWav).Name, _RuntimeException) : Throw New Exception("This is never Throw; the previous method call ends the process") : End Try
	End Function

End Module