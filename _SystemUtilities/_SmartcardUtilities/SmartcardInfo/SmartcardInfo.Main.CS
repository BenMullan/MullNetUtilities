namespace MullNet;

/// <summary>A small command-line utility to display useful information for EMV Smartcards</summary>
public static class SmartcardInfo {

	internal enum ActionWhenReady : Byte {
		Unresolved			= 0,
		ListReaders			= 1,
		AwaitCardInsertion	= 2,
		AwaitCardRemoval	= 3,
		GetCardInfo			= 4,
	}

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

		void _OutputSmartcardInfo(SmartcardUtils.SmartcardDataSnapshot _SmartcardData) {

			if (_OutputAsXML) {

				(
					new XElement(
						"SmartcardInfo",
						new XAttribute("CardholderName", _SmartcardData.CardholderName),
						new XAttribute("CardProvider", _SmartcardData.CardProviderName),
						new XAttribute("CardLanguageID", _SmartcardData.CardLanguageID),
						new XAttribute("PaymentSystemEnvironment", _SmartcardData.PaymentSystemEnvironment),
						_SmartcardData.HeirarchicalData.ToXML()
					)
				).WriteToConsoleWithXMLColourFormatting();

			} else {

				const UInt16 _ValueWidth = 32;
				
				Console.Out.Write(
$@"
┌──────────────{'─'.Repeat(_ValueWidth).JoinAsStrings("")}─┐
│  $ EMV Smartcard {' '.Repeat(_ValueWidth - 4).JoinAsStrings("")} │▒
│  ^^^^^^^^^^^^{'^'.Repeat(_ValueWidth).JoinAsStrings("")} │▒
│              {' '.Repeat(_ValueWidth).JoinAsStrings("")} │▒
│  Cardholder: {_SmartcardData.CardholderName.PadRight(totalWidth: _ValueWidth)} │▒
│  Provider:   {_SmartcardData.CardProviderName.PadRight(totalWidth: _ValueWidth)} │▒
│              {' '.Repeat(_ValueWidth).JoinAsStrings("")} │▒
│  Lang:       {_SmartcardData.CardLanguageID.PadRight(totalWidth: _ValueWidth)} │▒
│  PSE:        {_SmartcardData.PaymentSystemEnvironment.PadRight(totalWidth: _ValueWidth)} │▒
│              {' '.Repeat(_ValueWidth).JoinAsStrings("")} │▒
└──────────────{'─'.Repeat(_ValueWidth).JoinAsStrings("")}─┘▒
 ▒▒▒▒▒▒▒▒▒▒▒▒▒▒{'▒'.Repeat(_ValueWidth).JoinAsStrings("")}▒▒▒
"
				);

				// Output all raw card data as plaintext + xml...
				//
				//Console.Out.Write(
				//	_SmartcardData.HeirarchicalData.AsFlatHeirarchicalData
				//		.ToArray()
				//		.Select<KeyValuePair<String, String>, String>(
				//			(KeyValuePair<String, String> _KVP)
				//				=> $"{_KVP.Key.Take(100).JoinAsStrings(""),-102} = {( _KVP.Value.SubstituteByteStrings_WithHumanReadableStrings().OrThisValueIfEmpty(_KVP.Value) ).Take(70).JoinAsStrings("")}"
				//		).JoinAsStrings("\r\n")
				//);
				//Console.Out.Write("\r\n".Repeat(10).JoinAsStrings(""));
				//_SmartcardData.HeirarchicalData.ToXML().WriteToConsoleWithXMLColourFormatting();

			}

		}

		#endregion

		#region Understand the command-line arguments (CLAs)
		try {

			String _CurrentExeName = System.Diagnostics.Process.GetCurrentProcess().ProcessName ?? throw new Exception("Unresolvable current assembly (exe) name");

			MetaUtilities.CLAManager _CLAManager = new("Displays useful information for an inserted EMV Smartcard, including the Bank- & Cardholder-Name") {
				CLAHelpDictionary_Examples = [
					$"{_CurrentExeName} /ListReaders",
					$"{_CurrentExeName} /GetCardInfo",
					$"{_CurrentExeName} /GetCardInfo /TargetReaderIndex:0",
					$"{_CurrentExeName} /GetCardInfo /TargetReaderIndex:0 /AsXML",
					$"{_CurrentExeName} /AwaitCardInsertion",
					$"{_CurrentExeName} /AwaitCardInsertion /TargetReaderIndex:1",
					$"{_CurrentExeName} /AwaitCardRemoval",
					$"(in cmd.exe) {_CurrentExeName} /AwaitCardInsertion /TargetReaderIndex:2 && {_CurrentExeName} /GetCardInfo /TargetReaderIndex:2",
				]
			};

			_CLAManager.KeyToActionMappings.AddRange(
				[
					(new CLAManager.CLAKeyToActionMapping("ListReaders",        (String _Value) => _ActionWhenReady = ActionWhenReady.ListReaders)          { MustBeSpecified = false, MustHaveValue = false, Description = "[Action] Lists PCSC readers installed on the system." }),
					(new CLAManager.CLAKeyToActionMapping("AwaitCardInsertion", (String _Value) => _ActionWhenReady = ActionWhenReady.AwaitCardInsertion)   { MustBeSpecified = false, MustHaveValue = false, Description = "[Action] Waits for a Smartcard to be inserted (into the reader at /TargetReaderIndex if specified, otherwise any installed reader), then prints the card's Answer-To-Reset and exits." }),
					(new CLAManager.CLAKeyToActionMapping("AwaitCardRemoval",   (String _Value) => _ActionWhenReady = ActionWhenReady.AwaitCardRemoval)     { MustBeSpecified = false, MustHaveValue = false, Description = "[Action] Waits for a Smartcard to be removed (from the reader at /TargetReaderIndex if specified, otherwise any installed reader), then exits." }),
					(new CLAManager.CLAKeyToActionMapping("GetCardInfo",        (String _Value) => _ActionWhenReady = ActionWhenReady.GetCardInfo)          { MustBeSpecified = false, MustHaveValue = false, Description = "[Action] Reads & displays information including the Bank- & Cardholder-Name for an EMV smartcard (from the reader at /TargetReaderIndex if specified, otherwise any installed reader)." }),
					(new CLAManager.CLAKeyToActionMapping("TargetReaderIndex",  (String _Value) => _TargetPCSCReader_Index = Convert.ToByte(_Value))        { MustBeSpecified = false, MustHaveValue = true,  Description = "[Datum]  Specifies the zero-based index of a PCSC Smartcard reader (as shown by /ListReaders), to use for the /GetCardInfo, /AwaitCardInsertion, or /AwaitCardRemoval actions." }),
					(new CLAManager.CLAKeyToActionMapping("AsXML",              (String _Value) => _OutputAsXML = true)                                     { MustBeSpecified = false, MustHaveValue = false, Description = "[Flag]   Indicates that Actions' stdout will be in syntax-highlighted XML (instead of plaintext or ASCII-Art)." }),
				]
			);

			_CLAManager.RunForTheseCLAs(ref _CLAs);

		} catch (Exception _CLAsEx) {
			if (!_OutputAsXML) { UsefulMethods.ConsoleErrorWriteLineInColour($"Command-line Arguments' Error: {_CLAsEx.Message}", ConsoleColor.Red); }
			else { (new System.Xml.Linq.XElement("Error", new System.Xml.Linq.XAttribute("Message", $"Command-line Arguments' Error: {_CLAsEx.Message}"))).WriteToConsoleWithXMLColourFormatting(); }
			return 2;
		}
		#endregion

		#region Perform what the CLAs indicated
		try {

			///**	_ActionWhenReady switch {
			///**		ActionWhenReady.ListReaders			=> _ListReaders(),
			///**		ActionWhenReady.AwaitCardInsertion	=> ,
			///**		_									=> throw new Exception($"No recognised /Action specified in the command-line arguments. Choose from: {{{System.Enum.GetNames(typeof(SmartcardInfo.ActionWhenReady)).JoinAsStrings()}}}"),
			///**	}

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

					Byte __TargetPCSCReader_Index = _TargetPCSCReader_Index ?? throw new MetaUtilities.CLAManager.CLANotSpecifiedException("No /TargetReaderIndex was specified.");

					if ( ! Enumerable.Range(0, MetaUtilities.SmartcardUtils.AvaliableSmartcardReaders.Length).Contains(__TargetPCSCReader_Index) ) {
						throw new Exception("The specified /TargetReaderIndex represents a non-existent smartcard-reader.");
					}

					MetaUtilities.SmartcardUtils.SmartcardDataSnapshot _SmartcardData =
						MetaUtilities.SmartcardUtils.ReadSmartcard(__TargetPCSCReader_Index)
					;

					_OutputSmartcardInfo(_SmartcardData);

					break;

				}
				default: {
					throw new Exception($"No recognised /Action specified in the command-line arguments. Choose from: {{{ System.Enum.GetNames(typeof(SmartcardInfo.ActionWhenReady)).JoinAsStrings() }}}");
				}
			}

		} catch (Exception _RuntimeEx) {
			if (!_OutputAsXML) { UsefulMethods.ConsoleErrorWriteLineInColour($"Runtime Error: {_RuntimeEx.Message}", ConsoleColor.Red); }
			else { (new System.Xml.Linq.XElement("Error", new System.Xml.Linq.XAttribute("Message", $"Runtime Error: {_RuntimeEx.Message}"))).WriteToConsoleWithXMLColourFormatting(); }
			return 1;
		}

		return 0;

	}
	#endregion

}