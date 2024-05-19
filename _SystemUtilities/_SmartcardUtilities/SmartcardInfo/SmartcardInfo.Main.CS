namespace MullNet;

/// <summary>A small command-line utility to display useful information for EMV Smartcards</summary>
public static class SmartcardInfo {

	internal enum ActionWhenReady : Byte {
		Unresolved          = 0,
		ListReaders         = 1,
		AwaitCardInsertion  = 2,
		AwaitCardRemoval    = 3,
		GetCardInfo         = 4,
	}

	[System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0047:Remove unnecessary parentheses", Justification = "I want my extra brackets, thank you very much.")]
	public static Int32 Main(String[] _CLAs) {

		ActionWhenReady _ActionWhenReady            = ActionWhenReady.Unresolved;
		Byte?           _TargetPCSCReader_Index     = null;
		Boolean         _OutputAsXML                = false;

		#region Declare methods needed for the possible Actions

		void _ListReaders() {

			if (_OutputAsXML) {

				(
					new XElement(
						"AvaliableSmartcardReaders",
						MetaUtilities.SmartcardUtils.AvaliableSmartcardReaders.Select<String, XElement>(
							(String _ReaderName, Int32 _Index) => new XElement(
								"SmartcardReader",
								new XAttribute("Index", _Index),
								new XAttribute("Name", _ReaderName)
							)
						)
					)
				).WriteToConsoleWithXMLColourFormatting();

			} else {

				global::System.Console.Out.Write(
					String.Join(
						"\r\n",
						MetaUtilities.SmartcardUtils.AvaliableSmartcardReaders.Select<String, String>(
							(String _ReaderName, Int32 _Index) => $"[{_Index}] \t {_ReaderName}"
						)
					)
				);

			}

		}

		void _HandleSmartcardInsertion(String _ReaderName, Byte[] _ATR) {

			if (_TargetPCSCReader_Index is null || Convert.ToInt32(_ReaderName) == _TargetPCSCReader_Index) {

				if (_OutputAsXML) {

					(
						new XElement(
							"SmartcardInsertion",
							new XAttribute("AnswerToReset", System.BitConverter.ToString(_ATR)),
							(
								new XElement(
									"SmartcardReader",
									new XAttribute("Index", MetaUtilities.SmartcardUtils.AvaliableSmartcardReaders.ToList().IndexOf(_ReaderName)),
									new XAttribute("Name", _ReaderName)
								)
							).WrapIn("TargetReader")
						)
					).WriteToConsoleWithXMLColourFormatting();

				} else {

					global::System.Console.Out.Write(
						$@"Smartcard inserted into reader [{MetaUtilities.SmartcardUtils.AvaliableSmartcardReaders.ToList().IndexOf(_ReaderName)}] ""{_ReaderName}"", producing Answer-To-Reset ""{System.BitConverter.ToString(_ATR)}"""
					);

				}

				Environment.Exit(0);

			}

		}

		void _HandleSmartcardRemoval(String _ReaderName) {

			if (_OutputAsXML) {

				(
					new XElement(
						"SmartcardRemoval",
						(
							new XElement(
								"SmartcardReader",
								new XAttribute("Index", MetaUtilities.SmartcardUtils.AvaliableSmartcardReaders.ToList().IndexOf(_ReaderName)),
								new XAttribute("Name", _ReaderName)
							)
						).WrapIn("TargetReader")
					)
				).WriteToConsoleWithXMLColourFormatting();

			} else {

				global::System.Console.Out.Write(
					$@"Smartcard removed from reader [{MetaUtilities.SmartcardUtils.AvaliableSmartcardReaders.ToList().IndexOf(_ReaderName)}] ""{_ReaderName}"""
				);

			}

			Environment.Exit(0);

		}

		void _OutputSmartcardInfo() {

			if (_OutputAsXML) {

				(
					new XElement(
						"SmartcardInfo",
						new XAttribute("", "")
					)
				).WriteToConsoleWithXMLColourFormatting();

			} else {

				Console.Out.Write(
$@"
┌─────────────────────────────┐ 
│  Bank:       {"",-10}│▒
│  Cardholder: {2}            │▒
│                             │▒
│  ATR:        {2}            │▒
│  Hash:       {2}            │▒
│                             │▒
└─────────────────────────────┘▒
 ▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒
"
				);

			}

		}

		#endregion

		#region Understand the command-line arguments (CLAs)
		try {

			String _CurrentExeName = System.Diagnostics.Process.GetCurrentProcess().ProcessName ?? throw new Exception("Unresolvable current assembly (exe) name");

			CLAManager _CLAManager = new("Displays useful information for an inserted EMV Smartcard, including the Bank- & Cardholder-Name") {
				CLAHelpDictionary_Examples = [
					$"{_CurrentExeName} /ListReaders",
					$"{_CurrentExeName} /GetCardInfo",
					$"{_CurrentExeName} /GetCardInfo /TargetReaderIndex:0",
					$"{_CurrentExeName} /AwaitCardInsertion",
					$"{_CurrentExeName} /AwaitCardInsertion /TargetReaderIndex:1",
					$"{_CurrentExeName} /AwaitCardInsertion /TargetReaderIndex:1 /GetCardInfo /AsXML",
				]
			};

			_CLAManager.KeyToActionMappings.AddRange(
				[
					(new CLAManager.CLAKeyToActionMapping("ListReaders",        (String _Value) => _ActionWhenReady = ActionWhenReady.ListReaders)          { MustBeSpecified = false, MustHaveValue = false, Description = "[Action] Lists PCSC readers installed on the system." }),
					(new CLAManager.CLAKeyToActionMapping("AwaitCardInsertion", (String _Value) => _ActionWhenReady = ActionWhenReady.AwaitCardInsertion)   { MustBeSpecified = false, MustHaveValue = false, Description = "[Action] Waits for a Smartcard to be inserted (into the reader at /TargetReaderIndex if specified, otherwise any installed reader), then prints the card's Answer-To-Reset and exits." }),
					(new CLAManager.CLAKeyToActionMapping("AwaitCardRemoval",   (String _Value) => _ActionWhenReady = ActionWhenReady.AwaitCardRemoval)     { MustBeSpecified = false, MustHaveValue = false, Description = "[Action] Waits for a Smartcard to be removed (from the reader at /TargetReaderIndex if specified, otherwise any installed reader), then exits." }),
					(new CLAManager.CLAKeyToActionMapping("GetCardInfo",        (String _Value) => _ActionWhenReady = ActionWhenReady.GetCardInfo)          { MustBeSpecified = false, MustHaveValue = false, Description = "[Action] Awaits insertion of a smartcard into any installed reader (unless a smartcard is already inserted), then reads & displays information including the Bank- & Cardholder-Name." }),
					(new CLAManager.CLAKeyToActionMapping("TargetReaderIndex",  (String _Value) => _TargetPCSCReader_Index = Convert.ToByte(_Value))        { MustBeSpecified = false, MustHaveValue = true,  Description = "[Datum]  Specifies the index of a PCSC Smartcard reader (as shown by /ListReaders), to use for the /GetCardInfo, /AwaitCardInsertion, or /AwaitCardRemoval actions." }),
					(new CLAManager.CLAKeyToActionMapping("AsXML",              (String _Value) => _OutputAsXML = true)                                     { MustBeSpecified = false, MustHaveValue = false, Description = "[Flag]   Indicates that Actions' stdout output will be in syntax-highlighted XML (instead of plaintext or ASCII art)." }),
				]
			);

			_CLAManager.RunForTheseCLAs(ref _CLAs);

		} catch (Exception _CLAsEx) {
			UsefulMethods.ConsoleErrorWriteLineInColour($"Command-line Arguments' Error: {_CLAsEx.Message}", ConsoleColor.Red);
			return 2;
		}
		#endregion

		#region Perform what the CLAs indicated
		try {

			switch (_ActionWhenReady) {
				case ActionWhenReady.ListReaders: {

					_ListReaders();
					break;

				}
				case ActionWhenReady.AwaitCardInsertion: {

					global::LibPCSC.PCSCReader _SmartcardReader = new();

					_SmartcardReader.CardInserted += _HandleSmartcardInsertion;

					// Hang around until a smartcard is inserted.
					// The above delegate will then run on a different thread.
					while (true) { Thread.Sleep(100); }

				}
				case ActionWhenReady.AwaitCardRemoval: {

					global::LibPCSC.PCSCReader _SmartcardReader = new();

					_SmartcardReader.CardRemoved += _HandleSmartcardRemoval;

					// Hang around until a smartcard is removed.
					// The above delegate will then run on a different thread.
					while (true) { Thread.Sleep(100); }

				}
				case ActionWhenReady.GetCardInfo: {

					MetaUtilities.SmartcardUtils.SmartcardDataSnapshot _CardData = MetaUtilities.SmartcardUtils.ReadSmartcard(
						_TargetPCSCReader_Index ?? throw new MetaUtilities.CLAManager.CLANotSpecifiedException("No /TargetReaderIndex was specified.")
					);

					_OutputSmartcardInfo();

					break;

				}
				default: { throw new Exception($"No recognised /Action specified in the command-line arguments. Choose from: {{{System.Enum.GetNames(typeof(SmartcardInfo.ActionWhenReady)).JoinAsStrings()}}}"); }
			}

		} catch (Exception _RuntimeEx) {
			UsefulMethods.ConsoleErrorWriteLineInColour($"Runtime Error: {_RuntimeEx.Message}", ConsoleColor.Red);
			return 1;
		}

		return 0;

	}
	#endregion

}