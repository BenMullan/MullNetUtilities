using System.Linq; using DocScript.CompilerExtentions; using DocScript.Language.Instructions;
using DocScript.Language.Instructions.Statements; using DocScript.Language.Variables; using DocScript.Runtime;

namespace WinMorph32 {
    
    /// <summary>For injecting the custom functions into the DocScript runtime...</summary>
    public static class WMDSExecutionContext {

        public static ExecutionContext @WinMorphExeCxt {
            get {
                return new ExecutionContext(
                   _ID:                 "WinMorph32_CLI_ExeCxt",
                   _RootFolder:         new System.IO.DirectoryInfo(System.IO.Directory.GetCurrentDirectory()),
                   _InputDelegate:      ExecutionContext.CLIDefault.InputDelegate,
                   _OutputDelegate:     ExecutionContext.CLIDefault.OutputDelegate,
                   _BuiltInFunctions:   ExecutionContext.AllStandardBuiltInFunctions.Concat(WMDSExecutionContext.WinMorphBIFs).ToArray()
                );
            }
        }

        public static BuiltInFunction[] @WinMorphBIFs {
            get {
                return new BuiltInFunction[] {
                    WM_EnumWindowTitles_, WM_EnumWindowTitles_VisibleOnly_, WM_EnumTopLevelWindowsXE_, WM_MoveWindowBy_,
                    WM_SetWindowText_, WM_GetWindowText_, WM_SetWindowVisibility_, WM_GetWindowVisibility_, WM_SetWindowEnabled_,
                    WM_GetWindowEnabled_, WM_SetWindowSize_, WM_GetWindowSize_, WM_SetWindowPosition_, WM_GetWindowPosition_,
                    WM_GetChildWindowsXE_, WM_GetChildWindows_RecursiveXE_,
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
                                global::WinMorph32.WindowManipulationMethods.EnumWindowTitles().Select<System.String, DSString>(
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
                                global::WinMorph32.WindowManipulationMethods.EnumWindowTitles_VisibleOnly().Select<System.String, DSString>(
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
                    _ReturnType:            typeof(DSArray<DSString>),
                    _ExpectedParameters:    (new DSFunction.Parameter[] {}),
                    _Action:                new BuiltInFunction.BuiltInFunctionDelegate(
                        (SymbolTablesSnapshot _SymTbls, IDataValue[] _Arguments) => {

                            var _ExeRes = ExecutionResult.New_AndStartExecutionTimer(@"WM-BIF\" + _BifName);

                            _ExeRes.ReturnStatus.BuiltInFunction_ReturnValue = new DSArray<DSString>(
                                global::WinMorph32.WindowManipulationMethods.EnumTopLevelWindows().Select<WindowManipulationMethods.TLWindowInfo, DSString>(
                                    (WindowManipulationMethods.TLWindowInfo _TLWinInfo) => new DSString(
                                        WinMorph32.ExeCxtUtilityMethods.SerializeXElement_WithDoubleBackticks(
                                            WinMorph32.ExeCxtUtilityMethods.ToXElement<WindowManipulationMethods.TLWindowInfo>(_TLWinInfo)
                                        )
                                    )
                                ).ToArray()
                            );

                            return _ExeRes.StopExecutionTimer_AndFinaliseObject(ref _SymTbls);

                        }
                    )
                ) {
                    Description = "Returns (ds-double-backtick-)serialised XElements for {HWND, ClassName, Text, ProcessName, PID, IsVisible, IsEnabled} of all top-level windows. Syntax eg <TLWindowInfo Attr=``Value`` ... />."
                };

            }
        }

        private static BuiltInFunction WM_GetChildWindowsXE_ {
            get {

                System.String _BifName = "WM_GetChildWindowsXE";

                return new BuiltInFunction(
                    _Identifier: _BifName,
                    _ReturnType: typeof(DSArray<DSString>),
                    _ExpectedParameters: (
                        new DSFunction.Parameter[] {
                            new DSFunction.Parameter("_ParentHWnd", typeof(DSString))
                        }
                    ),
                    _Action: new BuiltInFunction.BuiltInFunctionDelegate(
                        (SymbolTablesSnapshot _SymTbls, IDataValue[] _Arguments) => {

                            var _ExeRes = ExecutionResult.New_AndStartExecutionTimer(@"WM-BIF\" + _BifName);

                            // _ParentHWnd, eg: "0x000100EE"
                            System.String _ParentHWnd = _Arguments[0].Coerce<DSString>().Value;

                            WindowManipulationMethods.ChildWindowInfo[] _ChildWindows = global::WinMorph32.WindowManipulationMethods.EnumChildWindows(
                                WinMorph32.WindowManipulationMethods.GetHwnd_FromHexString(_ParentHWnd)
                            );

                            _ExeRes.ReturnStatus.BuiltInFunction_ReturnValue = new DSArray<DSString>(
                                _ChildWindows.Select<WindowManipulationMethods.ChildWindowInfo, DSString>(
                                    (_ChildWinInfo) => new DSString(
                                        WinMorph32.ExeCxtUtilityMethods.SerializeXElement_WithDoubleBackticks(
                                            WinMorph32.ExeCxtUtilityMethods.ToXElement<WindowManipulationMethods.ChildWindowInfo>(_ChildWinInfo)
                                        )
                                    )
                                ).ToArray()
                            );

                            return _ExeRes.StopExecutionTimer_AndFinaliseObject(ref _SymTbls);

                        }
                    )
                ) {
                    Description = "Returns (ds-double-backtick-)serialised XElements for all direct child windows of the specified parent HWND. Syntax eg <ChildWindowInfo Attr=``Value`` ... />."
                };

            }
        }

        private static BuiltInFunction WM_GetChildWindows_RecursiveXE_ {
            get {

                System.String _BifName = "WM_GetChildWindows_RecursiveXE";

                return new BuiltInFunction(
                    _Identifier: _BifName,
                    _ReturnType: typeof(DSArray<DSString>),
                    _ExpectedParameters: (
                        new DSFunction.Parameter[] {
                            new DSFunction.Parameter("_ParentHWnd", typeof(DSString))
                        }
                    ),
                    _Action: new BuiltInFunction.BuiltInFunctionDelegate(
                        (SymbolTablesSnapshot _SymTbls, IDataValue[] _Arguments) => {

                            var _ExeRes = ExecutionResult.New_AndStartExecutionTimer(@"WM-BIF\" + _BifName);

                            // _ParentHWnd, eg: "0x000100EE"
                            System.String _ParentHWnd = _Arguments[0].Coerce<DSString>().Value;

                            WindowManipulationMethods.ChildWindowInfo[] _ChildWindows = global::WinMorph32.WindowManipulationMethods.EnumChildWindows_Recursive(
                                WinMorph32.WindowManipulationMethods.GetHwnd_FromHexString(_ParentHWnd)
                            );

                            _ExeRes.ReturnStatus.BuiltInFunction_ReturnValue = new DSArray<DSString>(
                                _ChildWindows.Select<WindowManipulationMethods.ChildWindowInfo, DSString>(
                                    (_ChildWinInfo) => new DSString(
                                        WinMorph32.ExeCxtUtilityMethods.SerializeXElement_WithDoubleBackticks(
                                            WinMorph32.ExeCxtUtilityMethods.ToXElement<WindowManipulationMethods.ChildWindowInfo>(_ChildWinInfo)
                                        )
                                    )
                                ).ToArray()
                            );

                            return _ExeRes.StopExecutionTimer_AndFinaliseObject(ref _SymTbls);

                        }
                    )
                ) {
                    Description = "Returns (ds-double-backtick-)serialised XElements for all descendant child windows (recursive) of the specified parent HWND. Syntax eg <ChildWindowInfo Attr=``Value`` ... />."
                };

            }
        }

        private static BuiltInFunction WM_SetWindowPosition_ {
            get {

                System.String _BifName = "WM_SetWindowPosition";

                return new BuiltInFunction(
                    _Identifier: _BifName,
                    _ReturnType: typeof(@Void),
                    _ExpectedParameters: (
                        new DSFunction.Parameter[] {
                            new DSFunction.Parameter("_HWnd", typeof(DSString)),
                            new DSFunction.Parameter("_TopLeftX", typeof(DSNumber)),
                            new DSFunction.Parameter("_TopLeftY", typeof(DSNumber))
                        }
                    ),
                    _Action: new BuiltInFunction.BuiltInFunctionDelegate(
                        (SymbolTablesSnapshot _SymTbls, IDataValue[] _Arguments) => {

                            var _ExeRes = ExecutionResult.New_AndStartExecutionTimer(@"WM-BIF\" + _BifName);

                            // _HWnd, eg: "0x000100EE"
                            System.String _HWnd = _Arguments[0].Coerce<DSString>().Value;

                            // _TopLeftX, eg: 100
                            System.Int32 _TopLeftX = (System.Int32)_Arguments[1].Coerce<DSNumber>().Value;

                            // _TopLeftY, eg: 200
                            System.Int32 _TopLeftY = (System.Int32)_Arguments[2].Coerce<DSNumber>().Value;

                            global::WinMorph32.WindowManipulationMethods.SetWindowPosition(
                                _hWnd: WinMorph32.WindowManipulationMethods.GetHwnd_FromHexString(_HWnd),
                                _TopLeftX: _TopLeftX,
                                _TopLeftY: _TopLeftY
                            );

                            return _ExeRes.StopExecutionTimer_AndFinaliseObject(ref _SymTbls);

                        }
                    )
                ) {
                    Description = "Sets the window's top-left position (screen coordinates) to _TopLeftX, _TopLeftY, retaining current width and height."
                };

            }
        }

        private static BuiltInFunction WM_GetWindowPosition_ {
            get {

                System.String _BifName = "WM_GetWindowPosition";

                return new BuiltInFunction(
                    _Identifier: _BifName,
                    _ReturnType: typeof(DSArray<DSNumber>),
                    _ExpectedParameters: (
                        new DSFunction.Parameter[] {
                            new DSFunction.Parameter("_HWnd", typeof(DSString))
                        }
                    ),
                    _Action: new BuiltInFunction.BuiltInFunctionDelegate(
                        (SymbolTablesSnapshot _SymTbls, IDataValue[] _Arguments) => {

                            var _ExeRes = ExecutionResult.New_AndStartExecutionTimer(@"WM-BIF\" + _BifName);

                            // _HWnd, eg: "0x000100EE"
                            System.String _HWnd = _Arguments[0].Coerce<DSString>().Value;

                            System.Drawing.Point _WindowPos = global::WinMorph32.WindowManipulationMethods.GetWindowPosition(
                                _hWnd: WinMorph32.WindowManipulationMethods.GetHwnd_FromHexString(_HWnd)
                            );

                            _ExeRes.ReturnStatus.BuiltInFunction_ReturnValue = new DSArray<DSNumber>(
                                new DSNumber[] { new DSNumber(_WindowPos.X), new DSNumber(_WindowPos.Y) }
                            );

                            return _ExeRes.StopExecutionTimer_AndFinaliseObject(ref _SymTbls);

                        }
                    )
                ) {
                    Description = "Returns [0] the top-left-X, and [1] the top-left-Y screen coordinates of the window."
                };

            }
        }
        
        private static BuiltInFunction WM_MoveWindowBy_ {
            get {

                System.String _BifName = "WM_MoveWindowBy";

                return new BuiltInFunction(
                    _Identifier: _BifName,
                    _ReturnType: typeof(@Void),
                    _ExpectedParameters: (
                        new DSFunction.Parameter[] {
                            new DSFunction.Parameter("_HWnd", typeof(DSString)),
                            new DSFunction.Parameter("_PixelsToTheRight", typeof(DSNumber)),
                            new DSFunction.Parameter("_PixelsDownwards", typeof(DSNumber))
                        }
                    ),
                    _Action: new BuiltInFunction.BuiltInFunctionDelegate(
                        (SymbolTablesSnapshot _SymTbls, IDataValue[] _Arguments) => {

                            var _ExeRes = ExecutionResult.New_AndStartExecutionTimer(@"WM-BIF\" + _BifName);

                            // _HWnd, eg: "0x000100EE"
                            System.String _HWnd = _Arguments[0].Coerce<DSString>().Value;

                            // _PixelsToTheRight, eg: 50
                            System.Int32 _PixelsToTheRight = (System.Int32)_Arguments[1].Coerce<DSNumber>().Value;

                            // _PixelsDownwards, eg: -600
                            System.Int32 _PixelsDownwards = (System.Int32)_Arguments[2].Coerce<DSNumber>().Value;

                            global::WinMorph32.WindowManipulationMethods.MoveWindowBy(
                               _hWnd: WinMorph32.WindowManipulationMethods.GetHwnd_FromHexString(_HWnd),
                               _PixelsToTheRight: _PixelsToTheRight,
                               _PixelsDownwards: _PixelsDownwards
                            );

                            return _ExeRes.StopExecutionTimer_AndFinaliseObject(ref _SymTbls);

                        }
                    )
                ) {
                    Description = "Moves the window pointed-to by the _HWnd, _PixelsToTheRight right, and _PixelsDownwards down."
                };

            }
        }

        private static BuiltInFunction WM_SetWindowText_ {
            get {

                System.String _BifName = "WM_SetWindowText";

                return new BuiltInFunction(
                    _Identifier: _BifName,
                    _ReturnType: typeof(@Void),
                    _ExpectedParameters: (
                        new DSFunction.Parameter[] {
                            new DSFunction.Parameter("_HWnd", typeof(DSString)),
                            new DSFunction.Parameter("_NewText", typeof(DSString))
                        }
                    ),
                    _Action: new BuiltInFunction.BuiltInFunctionDelegate(
                        (SymbolTablesSnapshot _SymTbls, IDataValue[] _Arguments) => {

                            var _ExeRes = ExecutionResult.New_AndStartExecutionTimer(@"WM-BIF\" + _BifName);

                            // _HWnd, eg: "0x000100EE"
                            System.String _HWnd = _Arguments[0].Coerce<DSString>().Value;

                            // _NewText, eg: "Title Here"
                            System.String _NewText = _Arguments[1].Coerce<DSString>().Value;

                            global::WinMorph32.RawWin32Methods.SetWindowText(
                               hWnd: WinMorph32.WindowManipulationMethods.GetHwnd_FromHexString(_HWnd),
                               lpString: _NewText
                            );

                            return _ExeRes.StopExecutionTimer_AndFinaliseObject(ref _SymTbls);

                        }
                    )
                ) {
                    Description = "Changes the window's text (eg title, button-text, label-text, etc) to the _NewText."
                };

            }
        }

        private static BuiltInFunction WM_GetWindowText_ {
            get {

                System.String _BifName = "WM_GetWindowText";

                return new BuiltInFunction(
                    _Identifier: _BifName,
                    _ReturnType: typeof(DSString),
                    _ExpectedParameters: (
                        new DSFunction.Parameter[] {
                            new DSFunction.Parameter("_HWnd", typeof(DSString))
                        }
                    ),
                    _Action: new BuiltInFunction.BuiltInFunctionDelegate(
                        (SymbolTablesSnapshot _SymTbls, IDataValue[] _Arguments) => {

                            var _ExeRes = ExecutionResult.New_AndStartExecutionTimer(@"WM-BIF\" + _BifName);

                            // _HWnd, eg: "0x000100EE"
                            System.String _HWnd = _Arguments[0].Coerce<DSString>().Value;

                            _ExeRes.ReturnStatus.BuiltInFunction_ReturnValue = new DSString(
                                global::WinMorph32.WindowManipulationMethods.GetWindowText(
                                    _hWnd: WinMorph32.WindowManipulationMethods.GetHwnd_FromHexString(_HWnd)
                                )
                            );

                            return _ExeRes.StopExecutionTimer_AndFinaliseObject(ref _SymTbls);

                        }
                    )
                ) {
                    Description = "Returns the text (title, caption, etc) of the window pointed-to by the _HWnd."
                };

            }
        }

        private static BuiltInFunction WM_SetWindowVisibility_ {
            get {

                System.String _BifName = "WM_SetWindowVisibility";

                return new BuiltInFunction(
                    _Identifier: _BifName,
                    _ReturnType: typeof(@Void),
                    _ExpectedParameters: (
                        new DSFunction.Parameter[] {
                            new DSFunction.Parameter("_HWnd", typeof(DSString)),
                            new DSFunction.Parameter("_MakeWindowVisible", typeof(DSBoolean))
                        }
                    ),
                    _Action: new BuiltInFunction.BuiltInFunctionDelegate(
                        (SymbolTablesSnapshot _SymTbls, IDataValue[] _Arguments) => {

                            var _ExeRes = ExecutionResult.New_AndStartExecutionTimer(@"WM-BIF\" + _BifName);

                            // _HWnd, eg: "0x000100EE"
                            System.String _HWnd = _Arguments[0].Coerce<DSString>().Value;

                            // _MakeWindowVisible, eg: False
                            System.Boolean _MakeWindowVisible = _Arguments[1].Coerce<DSBoolean>().Value;

                            global::WinMorph32.WindowManipulationMethods.SetWindowVisibility(
                               _hWnd: WinMorph32.WindowManipulationMethods.GetHwnd_FromHexString(_HWnd),
                               _MakeWindowVisible: _MakeWindowVisible
                            );

                            return _ExeRes.StopExecutionTimer_AndFinaliseObject(ref _SymTbls);

                        }
                    )
                ) {
                    Description = "Makes the window pointed-to by the _HWnd visible/invisible, in accordance with _MakeWindowVisible."
                };

            }
        }

        private static BuiltInFunction WM_GetWindowVisibility_ {
            get {

                System.String _BifName = "WM_GetWindowVisibility";

                return new BuiltInFunction(
                    _Identifier: _BifName,
                    _ReturnType: typeof(DSBoolean),
                    _ExpectedParameters: (
                        new DSFunction.Parameter[] {
                            new DSFunction.Parameter("_HWnd", typeof(DSString))
                        }
                    ),
                    _Action: new BuiltInFunction.BuiltInFunctionDelegate(
                        (SymbolTablesSnapshot _SymTbls, IDataValue[] _Arguments) => {

                            var _ExeRes = ExecutionResult.New_AndStartExecutionTimer(@"WM-BIF\" + _BifName);

                            // _HWnd, eg: "0x000100EE"
                            System.String _HWnd = _Arguments[0].Coerce<DSString>().Value;

                            _ExeRes.ReturnStatus.BuiltInFunction_ReturnValue = new DSBoolean(
                                global::WinMorph32.RawWin32Methods.IsWindowVisible(
                                    hWnd: WinMorph32.WindowManipulationMethods.GetHwnd_FromHexString(_HWnd)
                                )
                            );

                            return _ExeRes.StopExecutionTimer_AndFinaliseObject(ref _SymTbls);

                        }
                    )
                ) {
                    Description = "Returns whether or not the window pointed-to by the _HWnd is visible."
                };

            }
        }

        private static BuiltInFunction WM_SetWindowEnabled_ {
            get {

                System.String _BifName = "WM_SetWindowEnabled";

                return new BuiltInFunction(
                    _Identifier: _BifName,
                    _ReturnType: typeof(@Void),
                    _ExpectedParameters: (
                        new DSFunction.Parameter[] {
                            new DSFunction.Parameter("_HWnd", typeof(DSString)),
                            new DSFunction.Parameter("_MakeWindowEnabled", typeof(DSBoolean))
                        }
                    ),
                    _Action: new BuiltInFunction.BuiltInFunctionDelegate(
                        (SymbolTablesSnapshot _SymTbls, IDataValue[] _Arguments) => {

                            var _ExeRes = ExecutionResult.New_AndStartExecutionTimer(@"WM-BIF\" + _BifName);

                            // _HWnd, eg: "0x000100EE"
                            System.String _HWnd = _Arguments[0].Coerce<DSString>().Value;

                            // _MakeWindowEnabled, eg: False
                            System.Boolean _MakeWindowEnabled = _Arguments[1].Coerce<DSBoolean>().Value;

                            global::WinMorph32.WindowManipulationMethods.SetWindowEnabled(
                               _hWnd: WinMorph32.WindowManipulationMethods.GetHwnd_FromHexString(_HWnd),
                               _MakeWindowEnabled: _MakeWindowEnabled
                            );

                            return _ExeRes.StopExecutionTimer_AndFinaliseObject(ref _SymTbls);

                        }
                    )
                ) {
                    Description = "Makes the window pointed-to by the _HWnd enabled/disabled, in accordance with _MakeWindowEnabled."
                };

            }
        }

        private static BuiltInFunction WM_GetWindowEnabled_ {
            get {

                System.String _BifName = "WM_GetWindowEnabled";

                return new BuiltInFunction(
                    _Identifier: _BifName,
                    _ReturnType: typeof(DSBoolean),
                    _ExpectedParameters: (
                        new DSFunction.Parameter[] {
                            new DSFunction.Parameter("_HWnd", typeof(DSString))
                        }
                    ),
                    _Action: new BuiltInFunction.BuiltInFunctionDelegate(
                        (SymbolTablesSnapshot _SymTbls, IDataValue[] _Arguments) => {

                            var _ExeRes = ExecutionResult.New_AndStartExecutionTimer(@"WM-BIF\" + _BifName);

                            // _HWnd, eg: "0x000100EE"
                            System.String _HWnd = _Arguments[0].Coerce<DSString>().Value;

                            _ExeRes.ReturnStatus.BuiltInFunction_ReturnValue = new DSBoolean(
                                global::WinMorph32.RawWin32Methods.IsWindowEnabled(
                                    hWnd: WinMorph32.WindowManipulationMethods.GetHwnd_FromHexString(_HWnd)
                                )
                            );

                            return _ExeRes.StopExecutionTimer_AndFinaliseObject(ref _SymTbls);

                        }
                    )
                ) {
                    Description = "Returns whether or not the window pointed-to by the _HWnd is enabled."
                };

            }
        }

        private static BuiltInFunction WM_SetWindowSize_ {
            get {

                System.String _BifName = "WM_SetWindowSize";

                return new BuiltInFunction(
                    _Identifier: _BifName,
                    _ReturnType: typeof(@Void),
                    _ExpectedParameters: (
                        new DSFunction.Parameter[] {
                            new DSFunction.Parameter("_HWnd", typeof(DSString)),
                            new DSFunction.Parameter("_Width", typeof(DSNumber)),
                            new DSFunction.Parameter("_Height", typeof(DSNumber))
                        }
                    ),
                    _Action: new BuiltInFunction.BuiltInFunctionDelegate(
                        (SymbolTablesSnapshot _SymTbls, IDataValue[] _Arguments) => {

                            var _ExeRes = ExecutionResult.New_AndStartExecutionTimer(@"WM-BIF\" + _BifName);

                            // _HWnd, eg: "0x000100EE"
                            System.String _HWnd = _Arguments[0].Coerce<DSString>().Value;

                            // _Width, eg: 600
                            System.Int32 _Width = (System.Int32)_Arguments[1].Coerce<DSNumber>().Value;

                            // _Height, eg: 400
                            System.Int32 _Height = (System.Int32)_Arguments[2].Coerce<DSNumber>().Value;

                            global::WinMorph32.WindowManipulationMethods.SetWindowSize(
                               _hWnd:   WinMorph32.WindowManipulationMethods.GetHwnd_FromHexString(_HWnd),
                               _Width:  _Width,
                               _Height: _Height
                            );

                            return _ExeRes.StopExecutionTimer_AndFinaliseObject(ref _SymTbls);

                        }
                    )
                ) {
                    Description = "Sets [the window pointed-to by the _HWnd]'s width and height, retaining the current top-left position."
                };

            }
        }

        private static BuiltInFunction WM_GetWindowSize_ {
            get {

                System.String _BifName = "WM_GetWindowSize";

                return new BuiltInFunction(
                    _Identifier: _BifName,
                    _ReturnType: typeof(DSArray<DSNumber>),
                    _ExpectedParameters: (
                        new DSFunction.Parameter[] {
                            new DSFunction.Parameter("_HWnd", typeof(DSString))
                        }
                    ),
                    _Action: new BuiltInFunction.BuiltInFunctionDelegate(
                        (SymbolTablesSnapshot _SymTbls, IDataValue[] _Arguments) => {

                            var _ExeRes = ExecutionResult.New_AndStartExecutionTimer(@"WM-BIF\" + _BifName);

                            // _HWnd, eg: "0x000100EE"
                            System.String _HWnd = _Arguments[0].Coerce<DSString>().Value;

                            System.Drawing.Size _WindowSize = global::WinMorph32.WindowManipulationMethods.GetWindowSize(
                                _hWnd: WinMorph32.WindowManipulationMethods.GetHwnd_FromHexString(_HWnd)
                            );

                            _ExeRes.ReturnStatus.BuiltInFunction_ReturnValue = new DSArray<DSNumber>(
                                new DSNumber[] { new DSNumber(_WindowSize.Width), new DSNumber(_WindowSize.Height) }
                            );

                            return _ExeRes.StopExecutionTimer_AndFinaliseObject(ref _SymTbls);

                        }
                    )
                ) {
                    Description = "Returns [0] the width, and [1] the height of the window."
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