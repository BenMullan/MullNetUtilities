namespace MullNet;

public static class SmartcardInfo {

	internal enum ActionWhenReady : Byte {
		Unresolved  = 0,
		ListReaders = 1,
	}

	public static readonly PCSC.PCSCReader SmartcardReader = new();

	public static Int32 Main (String[] _CLAs) {

		ActionWhenReady _ActionWhenReady = ActionWhenReady.Unresolved;

		try {

			CLAManager _CLAManager = new("Displays basic information for an inserted EMV Smartcard") {
				CLAHelpDictionary_Examples = [
					"SmartcardInfo /ListReaders"
				]
			};

			_CLAManager.KeyToActionMappings.AddRange(
				[
					(new CLAManager.CLAKeyToActionMapping("ListReaders", (_Value) => _ActionWhenReady = ActionWhenReady.ListReaders) { MustBeSpecified = false, MustHaveValue = false, Description = "[Action] Lists PCSC readers installed on the system" })
				]
			);

			_CLAManager.RunForTheseCLAs(ref _CLAs);

		} catch (Exception _CLAsEx) {
			UsefulMethods.ConsoleErrorWriteLineInColour($"Command-line Arguments' Error: {_CLAsEx.Message}", ConsoleColor.Red);
			return 2;
		}

		try {

			switch (_ActionWhenReady) {
				case ActionWhenReady.ListReaders: {

					global::System.Console.Out.Write(
						String.Join(
							"\r\n",
							SmartcardInfo.SmartcardReader.AvaliableReaders.Select<String, String>(
								(String _ReaderName, Int32 _Index) => $"[{_Index}] \t {_ReaderName}"
							)
						)
					);

					break;
				}
				default: { throw new Exception($"Unresolved Application Action `{_ActionWhenReady:G}`"); }
			}

		} catch (Exception _RuntimeEx) {
			UsefulMethods.ConsoleErrorWriteLineInColour($"Runtime Error: {_RuntimeEx.Message}", ConsoleColor.Red);
			return 1;
		}

		return 0;

	}

}