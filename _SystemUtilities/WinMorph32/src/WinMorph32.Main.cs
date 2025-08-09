using System.Linq;

namespace WinMorph32 {
    static class Program {

        public static System.Int32 @Main(System.String[] _CLAs) {
            try {

                if ( (_CLAs.Length < 01) || !(new System.IO.FileInfo(_CLAs[00])).Exists ) {
                    throw new System.Exception(
                        "\nWinMorph - Win32 window text-, -size, and -position modifier utility."
                        + "\n   Usage: WinMorph32.exe [path-to-wmds-script] [...script-args]"
                        + "\n   Specify at-least 1 command-line argument; the path to an existent *.wmds script file."
                    );
                }

                global::DocScript.Runtime.ExecutionContext _ExecutionContext =
                    WinMorph32.WMDSExecutionContext.WinMorphExeCxt
                ;

                global::DocScript.Runtime.Program _WMProgram = global::DocScript.Runtime.Program.FromSource(
                    _Source: System.IO.File.ReadAllText(_CLAs[0]),
                    _ExeCxt: ref _ExecutionContext
                );

                return _WMProgram.Run(_CLAs.Skip(1).ToArray()).ReturnStatus.Program_ExitCode ?? -1;

            } catch (System.Exception _Ex) {

                global::DocScript.CompilerExtentions.UsefulMethods.ConsoleErrorWriteLineInColour(
                    "WinMorph :: runtime exception :: " + _Ex.Message,
                    System.ConsoleColor.Red
                );

                return 1;

            }
        }

    }
}