using System.Linq; using DocScript.CompilerExtentions; using DocScript.Runtime; using DocScript.Language.Variables;
using DocScript.Language.Instructions; using DocScript.Language.Instructions.Statements;

namespace WinMorph32 {
    
    /// <summary>For injecting the custom functions into the DocScript runtime...</summary>
    public static class WinMorphDSExecutionContext {

        public static ExecutionContext @WinMorphExeCxt {
            get {
                return new ExecutionContext(
                   _ID:                 "WinMorph32_CLI_ExeCxt",
                   _RootFolder:         new System.IO.DirectoryInfo(System.IO.Directory.GetCurrentDirectory()),
                   _InputDelegate:      ExecutionContext.CLIDefault.InputDelegate,
                   _OutputDelegate:     ExecutionContext.CLIDefault.OutputDelegate,
                   _BuiltInFunctions:   ExecutionContext.AllStandardBuiltInFunctions.Concat(WinMorphDSExecutionContext.WinMorphBIFs).ToArray()
                );
            }
        }

        public static BuiltInFunction[] @WinMorphBIFs {
            get {
                return new BuiltInFunction[] {
                    WM_EnumWindowTitles_, WM_EnumWindowTitles_VisibleOnly_, WM_EnumTopLevelWindowsXE_,
                    WMUtil_XElement_GetAttribute_
                };
            }
        }

        #region WinMorph-BuiltInFunction-Declarations

        private static BuiltInFunction WM_EnumWindowTitles_ {
            get {

                System.String _BifName = "WM_EnumWindowTitles";

                return new BuiltInFunction(
                    _Identifier:            _BifName,
                    _ExpectedParameters:    (new DSFunction.Parameter[] { }),
                    _ReturnType:            typeof(DSArray<DSString>),
                    _Action:                new BuiltInFunction.BuiltInFunctionDelegate(
                        (SymbolTablesSnapshot _SymTbls, IDataValue[] _Arguments) => {

                            var _ExeRes = ExecutionResult.New_AndStartExecutionTimer(@"WM-BIF\" + _BifName);

                            _ExeRes.ReturnStatus.BuiltInFunction_ReturnValue = new DSArray<DSString>(
                                global::WinMorph32.Win32Functions.EnumWindowTitles().Select<System.String, DSString>(
                                    (System.String _WindowTitle) => new DSString(_WindowTitle)
                                ).ToArray()
                            );

                            return _ExeRes.StopExecutionTimer_AndFinaliseObject(ref _SymTbls);

                        }
                    )
                ) {
                    Description = "Returns the titles of all (visible + invisible) top-level windows."
                };

            }
        }

        private static BuiltInFunction WM_EnumWindowTitles_VisibleOnly_ {
            get {

                System.String _BifName = "WM_EnumWindowTitles_VisibleOnly";

                return new BuiltInFunction(
                    _Identifier:            _BifName,
                    _ExpectedParameters:    (new DSFunction.Parameter[] { }),
                    _ReturnType:            typeof(DSArray<DSString>),
                    _Action:                new BuiltInFunction.BuiltInFunctionDelegate(
                        (SymbolTablesSnapshot _SymTbls, IDataValue[] _Arguments) => {

                            var _ExeRes = ExecutionResult.New_AndStartExecutionTimer(@"WM-BIF\" + _BifName);

                            _ExeRes.ReturnStatus.BuiltInFunction_ReturnValue = new DSArray<DSString>(
                                global::WinMorph32.Win32Functions.EnumWindowTitles_VisibleOnly().Select<System.String, DSString>(
                                    (System.String _WindowTitle) => new DSString(_WindowTitle)
                                ).ToArray()
                            );

                            return _ExeRes.StopExecutionTimer_AndFinaliseObject(ref _SymTbls);

                        }
                    )
                ) {
                    Description = "Returns the titles of all currently-visible top-level windows."
                };

            }
        }

        private static BuiltInFunction WM_EnumTopLevelWindowsXE_ {
            get {

                System.String _BifName = "WM_EnumTopLevelWindowsXE";

                return new BuiltInFunction(
                    _Identifier:            _BifName,
                    _ExpectedParameters:    (new DSFunction.Parameter[] { }),
                    _ReturnType:            typeof(DSArray<DSString>),
                    _Action:                new BuiltInFunction.BuiltInFunctionDelegate(
                        (SymbolTablesSnapshot _SymTbls, IDataValue[] _Arguments) => {

                            var _ExeRes = ExecutionResult.New_AndStartExecutionTimer(@"WM-BIF\" + _BifName);

                            _ExeRes.ReturnStatus.BuiltInFunction_ReturnValue = new DSArray<DSString>(
                                global::WinMorph32.Win32Functions.EnumTopLevelWindows().Select<Win32Functions.TLWindowInfo, DSString>(
                                    (Win32Functions.TLWindowInfo _TLWinInfo) => new DSString(
                                        WinMorph32.ExeCxtUtilityMethods.SerializeXElement_WithDoubleBackticks(
                                            WinMorph32.ExeCxtUtilityMethods.ToXElement<Win32Functions.TLWindowInfo>(_TLWinInfo)
                                        )
                                    )
                                ).ToArray()
                            );

                            return _ExeRes.StopExecutionTimer_AndFinaliseObject(ref _SymTbls);

                        }
                    )
                ) {
                    Description = "Returns (ds-double-backtick-)serialised XElements for the {HWND, ClassName, Title, ProcessName, and PID} of all windows. Eg <TLWindowInfo Attr=``Value`` ... />."
                };

            }
        }

        #region WinMorph-UtilityBIFs

        private static BuiltInFunction WMUtil_XElement_GetAttribute_ {
            get {

                System.String _BifName = "WMUtil_XElement_GetAttribute";

                return new BuiltInFunction(
                    _Identifier:            _BifName,
                    _ExpectedParameters:    (new DSFunction.Parameter[] { new DSFunction.Parameter("DBT_XElement", typeof(DSString)), new DSFunction.Parameter("AttributeName", typeof(DSString)) }),
                    _ReturnType:            typeof(DSString),
                    _Action:                new BuiltInFunction.BuiltInFunctionDelegate(
                        (SymbolTablesSnapshot _SymTbls, IDataValue[] _Arguments) => {

                            var _ExeRes = ExecutionResult.New_AndStartExecutionTimer(@"WM-BIF\" + _BifName);

                            // eg: <Person Name=``Ben`` />
                            System.String _DBT_XElement = _Arguments[0].Coerce<DSString>().Value;
                            System.Xml.Linq.XElement _XElement = ExeCxtUtilityMethods.DeserializeXElement_FromDoubleBackticks(_DBT_XElement);

                            // eg: Name
                            System.String _AttributeName = _Arguments[1].Coerce<DSString>().Value;

                            _ExeRes.ReturnStatus.BuiltInFunction_ReturnValue = new DSString(
                                global::WinMorph32.ExeCxtUtilityMethods.XElement_GetAttribute(_XElement, _AttributeName)
                            );

                            return _ExeRes.StopExecutionTimer_AndFinaliseObject(ref _SymTbls);

                        }
                    )
                ) {
                    Description = "From a (ds-double-backtick-)serialised XElement, plucks the value of the attribute named _AttributeName, eg <Person MyAttr=``Value`` />."
                };

            }
        }

        #endregion

        #endregion

    }

}