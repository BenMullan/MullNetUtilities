Public Module GetNTPTime

	Public TargetNTPServer As [String] = Nothing
	Public DisplayAsRawResponse As [Boolean] = False

	Public Function Main(ByVal _CLAs As [String]()) As Int32
		Try

			Dim _CLAManager As New MullNet.MetaUtilities.CLAManager("MullNet SysUtils - GetNTPTime.exe" & vbCrLf & "Queries the specified NTP Server for the current time, optionally displaying the response in raw data.") With {
			 .CLAHelpDictionary_Examples = {
			  "PlayWav /NTPServer:""time.windows.com""",
			  "PlayWav /NTPServer:time.windows.com /Raw"
			 }
			}

			_CLAManager.KeyToActionMappings.AddRange({
			   (New CLAManager.CLAKeyToActionMapping("NTPServer", Sub(_Value$) GetNTPTime.TargetNTPServer = _Value) With {.MustBeSpecified = True, .MustHaveValue = True, .Description = "[Datum] The NTP Server to query."}),
			   (New CLAManager.CLAKeyToActionMapping("Raw", Sub(_Value$) GetNTPTime.DisplayAsRawResponse = True) With {.MustBeSpecified = False, .MustHaveValue = False, .Description = "[Flag] Indicates that the NTP Server's response should be provided as unformatted raw data; the bytes received from the NTP Server are encoded to UTF8, and then written to the standard output stream."})
			})

			_CLAManager.OutputDelegate = AddressOf Console.WriteLine
			_CLAManager.UseCommandLineFormatting_ForHelpDictionary = True
			_CLAManager.RunForTheseCLAs(_CLAs)

			REM </CLAManagment> <ApplicationFunction>

			Dim _RawNTPData As Byte() = MullNet.MetaUtilities.NTPServerUtils.GetNTPServerResponseBytes(GetNTPTime.TargetNTPServer.MustNotBeNothing("The Target-NTP-Server had no value"))

			Console.Out.WriteLine(
			 If(
			  GetNTPTime.DisplayAsRawResponse,
			  System.Text.Encoding.UTF8.GetString(_RawNTPData),
			  MullNet.MetaUtilities.NTPServerUtils.GetDataTime_FromNTPServerResponseBytes(_RawNTPData).ToString()
			 )
			)

			Return Global.[MullNet].MetaUtilities.CMDApplicationUtilities.ExitCode_RanSuccessfully

		Catch _Ex As Exception
			UsefulMethods.ConsoleErrorWriteLineInColour("GetNTPTime Exception: " & _Ex.Message, ConsoleColor.Red)
			Return -1
		End Try
	End Function

End Module