/*
	MullNet System Utilities \ MicroHTTP.exe
	Ben Mullan (c) 2023
	A lightweight HTTP Server, which can serve either a constant String, single File, or a file from a specified Directory.
*/

public static class MicroHTTP {

	// To be set via CLA-Actions
	public static MullNet.MetaUtilities.MicroHTTPServer TheMicroHTTPServer = null;
	public static System.UInt16 Port = System.Convert.ToUInt16((new System.Random(System.DateTime.Now.Millisecond)).Next(minValue: 500, maxValue: 9000));
	public static System.String VirtualPath = System.String.Empty;
	public static System.String MIMEType = System.String.Empty;
	public static System.Boolean LaunchInBrowser = false;

	public static System.Int32 Main(System.String[] _CLAs) {

		// <CLAManagment>
		try {

			global::MullNet.MetaUtilities.CLAManager _CLAManager =
				new MullNet.MetaUtilities.CLAManager(
					_CLAHelpDictionary_Description: "MullNet SysUtils - MicroHTTP.exe\r\nA lightweight HTTP Server, which can serve either a constant String, single File, or files from a specified Directory.\r\nThe following MIME-Types are supported by File-Extension: " + MullNet.MetaUtilities.MicroHTTPServer.SerialisedMIMETypesDictionary
				) {
					CLAHelpDictionary_Examples = new System.String[] {
						@"MicroHTTP /String:""Same For All URLs!""",
						@"MicroHTTP /SingleFile:""Page.HTM""",
						@"MicroHTTP /Directory:""F:\"" /Port:8090",
						@"MicroHTTP /SingleFile:""C:\Me\Page.HTM"" /MIMEType:""text/html"" /VirtualPath:""Help""",
						@"Type SomeFile.txt | MicroHTTP /StringFromSTDIN /LaunchInBrowser"
					}
				};

			_CLAManager.KeyToActionMappings.AddRange(
				new MullNet.MetaUtilities.CLAManager.CLAKeyToActionMapping[] {
					(new MullNet.MetaUtilities.CLAManager.CLAKeyToActionMapping("Port", _Value => MicroHTTP.Port = System.Convert.ToUInt16(_Value) ) { MustBeSpecified = false, MustHaveValue = true, Description = @"[Datum] Specifies a specific port from which to run the server. If not specified, a random port number is chosen." }),
					(new MullNet.MetaUtilities.CLAManager.CLAKeyToActionMapping("VirtualPath", _Value => MicroHTTP.VirtualPath = _Value ) { MustBeSpecified = false, MustHaveValue = true, Description = @"[Datum] Specifies an optional virtual URL path. E.g. /VirtualPath:""Help"" and /Port:9001 would produce the URL http://localhost:9001/Help/." }),
					(new MullNet.MetaUtilities.CLAManager.CLAKeyToActionMapping("MIMEType", _Value => MicroHTTP.MIMEType = _Value ) { MustBeSpecified = false, MustHaveValue = true, Description = @"[Datum] Specifies an optional HTTP MIME-Type for the response. E.g. ""text/html"". If not specified, the server will resolve the MIME-Type from a file-extension, where possible." }),
					(new MullNet.MetaUtilities.CLAManager.CLAKeyToActionMapping("String", _Value => MicroHTTP.TheMicroHTTPServer = new MullNet.MetaUtilities.MicroHTTPServer(_Port:MicroHTTP.Port, _ResponseContentString:_Value, _ResponseMIMEType:MicroHTTP.MIMEType, _VirtualPath:MicroHTTP.VirtualPath) ) { MustBeSpecified = false, MustHaveValue = true, Description = @"[Action+Datum] Indicates that the server should serve the specified string, for all URLs. Can be used in combination with the /MIMEType and /VirtualPath CLAs." }),
					(new MullNet.MetaUtilities.CLAManager.CLAKeyToActionMapping("SingleFile", _Value => MicroHTTP.TheMicroHTTPServer = new MullNet.MetaUtilities.MicroHTTPServer(_Port:MicroHTTP.Port, _FileToServe:new System.IO.FileInfo(_Value), _ResponseMIMEType:MicroHTTP.MIMEType, _VirtualPath:MicroHTTP.VirtualPath) ) { MustBeSpecified = false, MustHaveValue = true, Description = @"[Action+Datum] Indicates that the server should serve the specified file, for all URLs." }),
					(new MullNet.MetaUtilities.CLAManager.CLAKeyToActionMapping("Directory", _Value => MicroHTTP.TheMicroHTTPServer = new MullNet.MetaUtilities.MicroHTTPServer(_Port:MicroHTTP.Port, _DirectoryToServeFilesFrom:new System.IO.DirectoryInfo(_Value), _ResponseMIMEType:MicroHTTP.MIMEType, _VirtualPath:MicroHTTP.VirtualPath) ) { MustBeSpecified = false, MustHaveValue = true, Description = @"[Action+Datum] Indicates that the server should serve a file from the specified directory, dependent on the file path of the URL." }),
					(new MullNet.MetaUtilities.CLAManager.CLAKeyToActionMapping("StringFromSTDIN", _Value => { System.Console.Out.WriteLine("Reading from StdIn...");  System.String _StdInput = System.Console.In.ReadToEnd(); MicroHTTP.TheMicroHTTPServer = new MullNet.MetaUtilities.MicroHTTPServer(_Port:MicroHTTP.Port, _ResponseContentString:_StdInput, _ResponseMIMEType:MicroHTTP.MIMEType, _VirtualPath:MicroHTTP.VirtualPath); } ) { MustBeSpecified = false, MustHaveValue = false, Description = @"[Action] Indicates that the process should read a string from the Standard (console) Input Stream. If typing this in, use Ctrl+Z and Enter to terminate. This string is then served for all URLs. Can be used in combination with the /MIMEType and /VirtualPath CLAs." }),
					(new MullNet.MetaUtilities.CLAManager.CLAKeyToActionMapping("LaunchInBrowser", _Value => MicroHTTP.LaunchInBrowser = true ) { MustBeSpecified = false, MustHaveValue = false, Description = @"[Flag] Automatically opens the URL via the Shell, just before starting the HTTP Server." })
				}
			);

			_CLAManager.OutputDelegate = System.Console.Out.WriteLine;
			_CLAManager.UseCommandLineFormatting_ForHelpDictionary = true;
			_CLAManager.RunForTheseCLAs(ref _CLAs);

		} catch (System.Exception _Ex) {
			MullNet.CompilerExtentions.UsefulMethods.ConsoleErrorWriteLineInColour("MicroHTTP Command-line Arguments: " + _Ex.Message, System.ConsoleColor.Red);
			return MullNet.MetaUtilities.CMDApplicationUtilities.ExitCode_CLAsError;
		}
		// </CLAManagment>

		// <ApplicationFunction>
		try {

			//If the _MicroServer is still [null], then an Action hasn't been specified as a CLA.
			if (MicroHTTP.TheMicroHTTPServer == null) { throw new System.Exception(@"An action must be specified, such as /SingleFile, /Directory, or /String."); }

			//Register an EventHandler for when Ctrl+C is pressed
			MicroHTTP.ProcessShutdownHandling.SetConsoleCtrlHandler(
				new MicroHTTP.ProcessShutdownHandling.HandlerRoutine(
					(MicroHTTP.ProcessShutdownHandling.CtrlTypes _ProcessExitReason) => {

						//Stop running the HTTP Server
						MullNet.CompilerExtentions.UsefulMethods.ConsoleWriteLineInColour("MicroHTTP received process-termination signal of type " + _ProcessExitReason.ToString("G") + "; shutting down...", System.ConsoleColor.Magenta);
						MicroHTTP.TheMicroHTTPServer.StopRunning();

						//Exit this process
						System.Environment.Exit(exitCode: MullNet.MetaUtilities.CMDApplicationUtilities.ExitCode_RanSuccessfully);
						return true;

					}
				),
				true
			);

			//Write to the console, whenever a Request is successfully handled
			MicroHTTP.TheMicroHTTPServer.RequestHandledSuccessfully += (MullNet.MetaUtilities.MicroHTTPServer.HTTPRequestSnapshot _Request) => {
				MullNet.CompilerExtentions.UsefulMethods.ConsoleWriteInColour("Incomming Request from " + _Request.UserHostAddress + "... ", System.ConsoleColor.Blue);
			};

			//Write to the console, whenever a Request is successfully handled
			MicroHTTP.TheMicroHTTPServer.RequestHandledSuccessfully += (MullNet.MetaUtilities.MicroHTTPServer.HTTPRequestSnapshot _Request) => {
				MullNet.CompilerExtentions.UsefulMethods.ConsoleWriteLineInColour("Served " + _Request.URL, System.ConsoleColor.Green);
			};

			//Write to the console, whenever a Request is UNsuccessfully handled
			MicroHTTP.TheMicroHTTPServer.RequestHandledUnsuccessfully += (System.String _ErrorMsg) => {
				MullNet.CompilerExtentions.UsefulMethods.ConsoleErrorWriteLineInColour("Request Error: " + _ErrorMsg, System.ConsoleColor.Red);
			};

			//Run the Server...
			if (MicroHTTP.LaunchInBrowser) { MullNet.CompilerExtentions.UsefulMethods.ConsoleWriteLineInColour("Launching Browser...", System.ConsoleColor.Blue); System.Diagnostics.Process.Start(MicroHTTP.TheMicroHTTPServer.URL); }
			MullNet.CompilerExtentions.UsefulMethods.ConsoleWriteLineInColour("MicroHTTP listening on " + MicroHTTP.TheMicroHTTPServer.URL + "\r\n", System.ConsoleColor.Blue);
			TheMicroHTTPServer.Run();

			//This line should never be hit, because the exit code should be produced by the above delegate
			return MullNet.MetaUtilities.CMDApplicationUtilities.ExitCode_GenericFatalError;

		} catch (System.Exception _Ex) {
			MullNet.CompilerExtentions.UsefulMethods.ConsoleErrorWriteLineInColour("MicroHTTP Runtime Error: " + _Ex.Message, System.ConsoleColor.Red);
			return MullNet.MetaUtilities.CMDApplicationUtilities.ExitCode_RuntimeError;
		}
		// </ApplicationFunction>

	}

	/// <summary>Provides logic for orchestrating [Ctrl+C]-initiated process shutdowns, etc.</summary>
	public static class ProcessShutdownHandling {

		public enum CtrlTypes {
			CTRL_C_EVENT = 0,
			CTRL_BREAK_EVENT,
			CTRL_CLOSE_EVENT,
			CTRL_LOGOFF_EVENT = 5,
			CTRL_SHUTDOWN_EVENT
		}

		public delegate System.Boolean HandlerRoutine(CtrlTypes CtrlType);

		[global::System.Runtime.InteropServices.DllImport("Kernel32.dll")]
		public static extern System.Boolean SetConsoleCtrlHandler(ProcessShutdownHandling.HandlerRoutine Handler, System.Boolean Add);

	}

}