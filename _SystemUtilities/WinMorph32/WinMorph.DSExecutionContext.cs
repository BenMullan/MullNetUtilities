using System.Linq;
using DocScript.CompilerExtentions;
using DocScript.Language.Instructions;
using DocScript.Language.Instructions.Statements;
using DocScript.Language.Variables;
using DocScript.Runtime;

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
                    WMUtil_XElement_New_, WMUtil_XElement_GetAttribute_, WMUtil_XElement_SetAttribute_, WMUtil_XElement_HasAttribute_,
                    WMUtil_XElement_GetAttrKeys_, WMUtil_XElement_RemoveAttribute_, WMUtil_XElement_IsValid_, WMUtil_XElement_Merge_
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

        #region XElement-BIFs

        private static BuiltInFunction WMUtil_XElement_New_ {
            get {

                System.String _BifName = "WMUtil_XElement_New";

                return new BuiltInFunction(
                    _Identifier:            _BifName,
                    _ReturnType:            typeof(DSString),
                    _ExpectedParameters:    (new DSFunction.Parameter[] { new DSFunction.Parameter("_ElementName", typeof(DSString)) }),
                    _Action:                new BuiltInFunction.BuiltInFunctionDelegate(
                        (SymbolTablesSnapshot _SymTbls, IDataValue[] _Arguments) => {

                            var _ExeRes = ExecutionResult.New_AndStartExecutionTimer(@"WM-BIF\" + _BifName);

                            // _ElementName, eg: Person
                            System.String _ElementName = _Arguments[0].Coerce<DSString>().Value;

                            _ExeRes.ReturnStatus.BuiltInFunction_ReturnValue = new DSString(
                                global::WinMorph32.ExeCxtUtilityMethods.SerializeXElement_WithDoubleBackticks(
                                    new System.Xml.Linq.XElement(name:_ElementName)
                                )
                            );

                            return _ExeRes.StopExecutionTimer_AndFinaliseObject(ref _SymTbls);

                        }
                    )
                ) {
                    Description = "Returns an empty (ds-double-backtick-)serialised XElement, with tag-name _ElementName. Eg <Person />."
                };

            }
        }
        
        private static BuiltInFunction WMUtil_XElement_GetAttribute_ {
            get {

                System.String _BifName = "WMUtil_XElement_GetAttribute";

                return new BuiltInFunction(
                    _Identifier:            _BifName,
                    _ReturnType:            typeof(DSString),
                    _ExpectedParameters:    (new DSFunction.Parameter[] { new DSFunction.Parameter("_DBT_XElement", typeof(DSString)), new DSFunction.Parameter("_AttributeName", typeof(DSString)) }),
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
                    Description = "From a (ds-double-backtick-)serialised XElement, plucks the value of the attribute named _AttributeName. Syntax eg <Person MyAttr=``Value`` />."
                };

            }
        }

        private static BuiltInFunction WMUtil_XElement_SetAttribute_ {
            get {

                System.String _BifName = "WMUtil_XElement_SetAttribute";

                return new BuiltInFunction(
                    _Identifier: _BifName,
                    _ReturnType: typeof(DSString),
                    _ExpectedParameters: (
                        new DSFunction.Parameter[] {
                            new DSFunction.Parameter("_DBT_XElement", typeof(DSString)),
                            new DSFunction.Parameter("_AttributeName", typeof(DSString)),
                            new DSFunction.Parameter("_NewAttrValue", typeof(DSString))
                        }
                    ),
                    _Action: new BuiltInFunction.BuiltInFunctionDelegate(
                        (SymbolTablesSnapshot _SymTbls, IDataValue[] _Arguments) => {

                            var _ExeRes = ExecutionResult.New_AndStartExecutionTimer(@"WM-BIF\" + _BifName);

                            // _DBT_XElement, eg: <Person Name=``Ben`` />
                            System.String _DBT_XElement = _Arguments[0].Coerce<DSString>().Value;
                            System.Xml.Linq.XElement _XElement = ExeCxtUtilityMethods.DeserializeXElement_FromDoubleBackticks(_DBT_XElement);

                            // _AttributeName, eg: Name
                            System.String _AttributeName = _Arguments[1].Coerce<DSString>().Value;

                            // _NewAttrValue, eg: Luke
                            System.String _NewAttrValue = _Arguments[2].Coerce<DSString>().Value;

                            _ExeRes.ReturnStatus.BuiltInFunction_ReturnValue = new DSString(
                                global::WinMorph32.ExeCxtUtilityMethods.SerializeXElement_WithDoubleBackticks(
                                    ExeCxtUtilityMethods.XElement_SetAttribute(_XElement, _AttributeName, _NewAttrValue)
                                )
                            );

                            return _ExeRes.StopExecutionTimer_AndFinaliseObject(ref _SymTbls);

                        }
                    )
                ) {
                    Description = "From a (ds-double-backtick-)serialised XElement, returns a new mutated XElement, with the _NewAttrValue. Syntax eg <Person MyAttr=``Value`` />."
                };

            }
        }

        private static BuiltInFunction WMUtil_XElement_RemoveAttribute_ {
            get {

                System.String _BifName = "WMUtil_XElement_RemoveAttribute";

                return new BuiltInFunction(
                    _Identifier: _BifName,
                    _ReturnType: typeof(DSString),
                    _ExpectedParameters: (
                        new DSFunction.Parameter[] {
                            new DSFunction.Parameter("_DBT_XElement", typeof(DSString)),
                            new DSFunction.Parameter("_AttributeName", typeof(DSString))
                        }
                    ),
                    _Action: new BuiltInFunction.BuiltInFunctionDelegate(
                        (SymbolTablesSnapshot _SymTbls, IDataValue[] _Arguments) => {

                            var _ExeRes = ExecutionResult.New_AndStartExecutionTimer(@"WM-BIF\" + _BifName);

                            // _DBT_XElement, eg: <Person Name=``Ben`` />
                            System.String _DBT_XElement = _Arguments[0].Coerce<DSString>().Value;
                            System.Xml.Linq.XElement _XElement = ExeCxtUtilityMethods.DeserializeXElement_FromDoubleBackticks(_DBT_XElement);

                            // _AttributeName, eg: Name
                            System.String _AttributeName = _Arguments[1].Coerce<DSString>().Value;

                            _XElement.SetAttributeValue(_AttributeName, null);

                            _ExeRes.ReturnStatus.BuiltInFunction_ReturnValue = new DSString(
                                global::WinMorph32.ExeCxtUtilityMethods.SerializeXElement_WithDoubleBackticks(_XElement)
                            );

                            return _ExeRes.StopExecutionTimer_AndFinaliseObject(ref _SymTbls);

                        }
                    )
                ) {
                    Description = "From a (ds-double-backtick-)serialised XElement, returns a new mutated XElement, WITHOUT the specified attribute. Syntax eg <Person MyAttr=``Value`` />."
                };

            }
        }

        private static BuiltInFunction WMUtil_XElement_HasAttribute_ {
            get {

                System.String _BifName = "WMUtil_XElement_HasAttribute";

                return new BuiltInFunction(
                    _Identifier: _BifName,
                    _ReturnType: typeof(DSBoolean),
                    _ExpectedParameters: (
                        new DSFunction.Parameter[] {
                            new DSFunction.Parameter("_DBT_XElement", typeof(DSString)),
                            new DSFunction.Parameter("_AttributeName", typeof(DSString))
                        }
                    ),
                    _Action: new BuiltInFunction.BuiltInFunctionDelegate(
                        (SymbolTablesSnapshot _SymTbls, IDataValue[] _Arguments) => {

                            var _ExeRes = ExecutionResult.New_AndStartExecutionTimer(@"WM-BIF\" + _BifName);

                            // _DBT_XElement, eg: <Person Name=``Ben`` />
                            System.String _DBT_XElement = _Arguments[0].Coerce<DSString>().Value;
                            System.Xml.Linq.XElement _XElement = ExeCxtUtilityMethods.DeserializeXElement_FromDoubleBackticks(_DBT_XElement);

                            // _AttributeName, eg: Name
                            System.String _AttributeName = _Arguments[1].Coerce<DSString>().Value;

                            _ExeRes.ReturnStatus.BuiltInFunction_ReturnValue = new DSBoolean(
                                _XElement.Attribute(name: _AttributeName) != null
                            );

                            return _ExeRes.StopExecutionTimer_AndFinaliseObject(ref _SymTbls);

                        }
                    )
                ) {
                    Description = "Returns whether or not the (ds-double-backtick-)serialised XElement has an Attr with name _AttributeName. Syntax eg <Person Name=``Ben`` />."
                };

            }
        }

        private static BuiltInFunction WMUtil_XElement_GetAttrKeys_ {
            get {

                System.String _BifName = "WMUtil_XElement_GetAttrKeys";

                return new BuiltInFunction(
                    _Identifier: _BifName,
                    _ReturnType: typeof(DSArray<DSString>),
                    _ExpectedParameters: (
                        new DSFunction.Parameter[] {
                            new DSFunction.Parameter("_DBT_XElement", typeof(DSString))
                        }
                    ),
                    _Action: new BuiltInFunction.BuiltInFunctionDelegate(
                        (SymbolTablesSnapshot _SymTbls, IDataValue[] _Arguments) => {

                            var _ExeRes = ExecutionResult.New_AndStartExecutionTimer(@"WM-BIF\" + _BifName);

                            // _DBT_XElement, eg: <Person Name=``Ben`` />
                            System.String _DBT_XElement = _Arguments[0].Coerce<DSString>().Value;
                            System.Xml.Linq.XElement _XElement = ExeCxtUtilityMethods.DeserializeXElement_FromDoubleBackticks(_DBT_XElement);

                            _ExeRes.ReturnStatus.BuiltInFunction_ReturnValue = new DSArray<DSString>(
                                _XElement.Attributes().Select<System.Xml.Linq.XAttribute, DSString>(
                                    (System.Xml.Linq.XAttribute _Attr) => new DSString(_Attr.Name.LocalName)
                                ).ToArray()
                            );

                            return _ExeRes.StopExecutionTimer_AndFinaliseObject(ref _SymTbls);

                        }
                    )
                ) {
                    Description = "Returns all attribute names from the (ds-double-backtick-)serialised XElement. Syntax eg <Person Name=``Ben`` />."
                };

            }
        }

        private static BuiltInFunction WMUtil_XElement_IsValid_ {
            get {

                System.String _BifName = "WMUtil_XElement_IsValid";

                return new BuiltInFunction(
                    _Identifier: _BifName,
                    _ReturnType: typeof(DSBoolean),
                    _ExpectedParameters: (
                        new DSFunction.Parameter[] {
                            new DSFunction.Parameter("_DBT_XElement", typeof(DSString))
                        }
                    ),
                    _Action: new BuiltInFunction.BuiltInFunctionDelegate(
                        (SymbolTablesSnapshot _SymTbls, IDataValue[] _Arguments) => {

                            var _ExeRes = ExecutionResult.New_AndStartExecutionTimer(@"WM-BIF\" + _BifName);

                            // _DBT_XElement, eg: <Person Name=``Ben`` />
                            System.String _DBT_XElement = _Arguments[0].Coerce<DSString>().Value;

                            try {

                                System.Xml.Linq.XElement _XElement =
                                    ExeCxtUtilityMethods.DeserializeXElement_FromDoubleBackticks(_DBT_XElement)
                                ;

                                _ExeRes.ReturnStatus.BuiltInFunction_ReturnValue = new DSBoolean(true);

                            } catch {

                                _ExeRes.ReturnStatus.BuiltInFunction_ReturnValue = new DSBoolean(false);

                            }

                            return _ExeRes.StopExecutionTimer_AndFinaliseObject(ref _SymTbls);

                        }
                    )
                ) {
                    Description = "Returns whether or not the (ds-double-backtick-)serialised XElement is syntactically valid. Syntax eg <Person Name=``Ben`` />."
                };

            }
        }

        private static BuiltInFunction WMUtil_XElement_Merge_ {
            get {

                System.String _BifName = "WMUtil_XElement_Merge";

                return new BuiltInFunction(
                    _Identifier: _BifName,
                    _ReturnType: typeof(DSString),
                    _ExpectedParameters: (
                        new DSFunction.Parameter[] {
                            new DSFunction.Parameter("_DBT_XElementOne", typeof(DSString)),
                            new DSFunction.Parameter("_DBT_XElementTwo", typeof(DSString))
                        }
                    ),
                    _Action: new BuiltInFunction.BuiltInFunctionDelegate(
                        (SymbolTablesSnapshot _SymTbls, IDataValue[] _Arguments) => {

                            var _ExeRes = ExecutionResult.New_AndStartExecutionTimer(@"WM-BIF\" + _BifName);

                            // _DBT_XElementOne, eg: <Person Name=``Ben`` />
                            System.String _DBT_XElementOne = _Arguments[0].Coerce<DSString>().Value;
                            System.Xml.Linq.XElement _XElementOne = ExeCxtUtilityMethods.DeserializeXElement_FromDoubleBackticks(_DBT_XElementOne);

                            // _DBT_XElementTwo, eg: <Person Name=``Ben`` />
                            System.String _DBT_XElementTwo = _Arguments[1].Coerce<DSString>().Value;
                            System.Xml.Linq.XElement _XElementTwo = ExeCxtUtilityMethods.DeserializeXElement_FromDoubleBackticks(_DBT_XElementTwo);

                            System.Xml.Linq.XElement _NewMergedXElement = new System.Xml.Linq.XElement(name: _XElementOne.Name);
                            foreach (System.Xml.Linq.XAttribute _Attr in _XElementOne.Attributes()) { _NewMergedXElement.SetAttributeValue(_Attr.Name, _Attr.Value); }
                            foreach (System.Xml.Linq.XAttribute _Attr in _XElementTwo.Attributes()) { _NewMergedXElement.SetAttributeValue(_Attr.Name, _Attr.Value); }

                            _ExeRes.ReturnStatus.BuiltInFunction_ReturnValue = new DSString(
                                global::WinMorph32.ExeCxtUtilityMethods.SerializeXElement_WithDoubleBackticks(
                                    _NewMergedXElement
                                )
                            );

                            return _ExeRes.StopExecutionTimer_AndFinaliseObject(ref _SymTbls);

                        }
                    )
                ) {
                    Description = "Takes _DBT_XElementOne, but adds-to/overwrites its attributes, with those of _DBT_XElementTwo. Syntax eg <Person MyAttr=``Value`` />."
                };

            }
        }

        #endregion

        #endregion

    }

}