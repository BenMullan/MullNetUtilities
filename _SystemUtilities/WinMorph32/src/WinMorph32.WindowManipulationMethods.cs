using System.Linq;

namespace WinMorph32 {
    public static class WindowManipulationMethods {

        /// <summary>Represents a top-level window with a {HWND, Title, ClassName, ProcessName, PID, IsVisible, IsEnabled}.</summary>
        public struct TLWindowInfo { public System.String HWND, Title, ClassName, ProcessName; public System.UInt32 PID; public System.Boolean IsVisible, IsEnabled; }

        /// <summary>Eg: 0xA3F88E9B3</summary>
        public const System.String WinMorph_SerialisedHwndPrefix = "0x";

        /// /// <summary>Eg: (HWND) → "0xA3F88E9B3"</summary>
        public static System.String ConvertHwnd_ToHexString(System.IntPtr _hWnd) {
            return (
                (System.IntPtr.Size == 4)
                ? WinMorph_SerialisedHwndPrefix + _hWnd.ToInt32().ToString("X8")  /* 32-bit */
                : WinMorph_SerialisedHwndPrefix + _hWnd.ToInt64().ToString("X16") /* 64-bit */
            );
        }

        /// <summary>Eg: "0xA3F88E9B3" → (HWND)</summary>
        public static System.IntPtr GetHwnd_FromHexString(System.String _HexString) {

            // _HexString should look like eg: 0xA3F88E9B3
            // ; strip the 1st 7 chars...

            System.String _CleanedHex = _HexString.Substring(WinMorph_SerialisedHwndPrefix.Length);

            return (
                (System.IntPtr.Size == 4)
                ? (new System.IntPtr(System.Convert.ToInt32(_CleanedHex, 16))) /* 32-bit */
                : (new System.IntPtr(System.Convert.ToInt64(_CleanedHex, 16))) /* 64-bit */
            );

        }

        #region ImplementationsOf-Win32Methods

        /// <summary>Returns only the non-empty window-titles. Special entry at end of array for #titleless-windows.</summary>
        public static System.String[] EnumWindowTitles() {

            System.Collections.Generic.List<System.String> _WindowTitles = new System.Collections.Generic.List<System.String>();
            System.UInt32 _TitlelessWindows_Count = 0;

            RawWin32Methods.EnumWindows(
                (System.IntPtr _hWnd, System.IntPtr _lParam) => {

                    System.Int32 _TitleLength = RawWin32Methods.GetWindowTextLength(_hWnd);
                    if (_TitleLength < 1) { _TitlelessWindows_Count += 1; return true; }

                    System.Text.StringBuilder _StringBuilder = new System.Text.StringBuilder(capacity: _TitleLength + 1);
                    RawWin32Methods.GetWindowText(_hWnd, _StringBuilder, _StringBuilder.Capacity);
                    _WindowTitles.Add(_StringBuilder.ToString());

                    // continue enumerating...
                    return true;

                },
                System.IntPtr.Zero
            );

            return _WindowTitles.Concat(new System.String[] { "<<< ...and " + _TitlelessWindows_Count.ToString() + " titleless windows >>>" }).ToArray();

        }

        /// <summary>Returns only the non-empty window-titles, of windows which ARE visible.</summary>
        public static System.String[] EnumWindowTitles_VisibleOnly() {

            System.Collections.Generic.List<System.String> _WindowTitles = new System.Collections.Generic.List<System.String>();

            RawWin32Methods.EnumWindows(
                (System.IntPtr _hWnd, System.IntPtr _lParam) => {
                    
                    if (!RawWin32Methods.IsWindowVisible(_hWnd)) { return true; }
                    
                    System.Int32 _TitleLength = RawWin32Methods.GetWindowTextLength(_hWnd);
                    if (_TitleLength < 1) { return true; }

                    System.Text.StringBuilder _StringBuilder = new System.Text.StringBuilder(capacity: _TitleLength + 1);
                    RawWin32Methods.GetWindowText(_hWnd, _StringBuilder, _StringBuilder.Capacity);
                    _WindowTitles.Add(_StringBuilder.ToString());

                    // continue enumerating...
                    return true;

                },
                System.IntPtr.Zero
            );

            return _WindowTitles.ToArray();

        }

        /// <summary>Returns {HWND, Title, ClassName, ProcessName, PID, IsVisible, IsEnabled} for ALL top-level windows.</summary>
        public static WindowManipulationMethods.TLWindowInfo[] EnumTopLevelWindows() {

            System.Collections.Generic.List<WindowManipulationMethods.TLWindowInfo> _TLWindowInfos =
                new System.Collections.Generic.List<WindowManipulationMethods.TLWindowInfo>()
            ;

            RawWin32Methods.EnumWindows(
                (System.IntPtr _hWnd, System.IntPtr _lParam) => {

                    WindowManipulationMethods.TLWindowInfo _TLWindowInfo = new WindowManipulationMethods.TLWindowInfo();

                    // HWND
                    _TLWindowInfo.HWND = WindowManipulationMethods.ConvertHwnd_ToHexString(_hWnd);

                    // Title
                    System.Int32 _TitleLength = RawWin32Methods.GetWindowTextLength(_hWnd);
                    System.Text.StringBuilder _TitleStringBuilder = new System.Text.StringBuilder(capacity: _TitleLength + 1);
                    RawWin32Methods.GetWindowText(_hWnd, _TitleStringBuilder, _TitleStringBuilder.Capacity);
                    _TLWindowInfo.Title = _TitleStringBuilder.ToString();

                    // ClassName
                    System.Text.StringBuilder _ClassNameStringBuilder = new System.Text.StringBuilder(capacity: 256);
                    RawWin32Methods.GetClassName(_hWnd, _ClassNameStringBuilder, _ClassNameStringBuilder.Capacity);
                    _TLWindowInfo.ClassName = _ClassNameStringBuilder.ToString();

                    // PID
                    RawWin32Methods.GetWindowThreadProcessId(_hWnd, out _TLWindowInfo.PID);

                    // ProcessName
                    _TLWindowInfo.ProcessName = "(process exited)";
                    try { _TLWindowInfo.ProcessName = System.Diagnostics.Process.GetProcessById((System.Int32)_TLWindowInfo.PID).ProcessName; } catch {}

                    // IsVisible
                    _TLWindowInfo.IsVisible = RawWin32Methods.IsWindowVisible(_hWnd);

                    // IsEnabled
                    _TLWindowInfo.IsEnabled = RawWin32Methods.IsWindowEnabled(_hWnd);

                    _TLWindowInfos.Add(_TLWindowInfo);

                    // continue enumerating...
                    return true;

                },
                System.IntPtr.Zero
            );

            return _TLWindowInfos.ToArray();

        }

        /// <summary>Gets the window's title/caption text.</summary>
        public static System.String GetWindowText(System.IntPtr _hWnd) {

            System.Int32 _TitleLength = RawWin32Methods.GetWindowTextLength(_hWnd);
            if (_TitleLength < 1) { return System.String.Empty; }

            System.Text.StringBuilder _StringBuilder = new System.Text.StringBuilder(capacity: _TitleLength + 1);
            RawWin32Methods.GetWindowText(_hWnd, _StringBuilder, _StringBuilder.Capacity);

            return _StringBuilder.ToString();

        }

        /// <summary>Moves the specified window by the given pixel offsets (relative to its current position).</summary>
        public static void MoveWindowBy(System.IntPtr _hWnd, System.Int32 _PixelsToTheRight, System.Int32 _PixelsDownwards) {

            RawWin32Methods.RECT _Rect;
            if (!RawWin32Methods.GetWindowRect(_hWnd, out _Rect)) { throw new System.Exception("Couldn't GetWindowRect() for (HWND)" + _hWnd); }

            System.Int32 _NewX = _Rect.Left + _PixelsToTheRight;
            System.Int32 _NewY = _Rect.Top + _PixelsDownwards;
            System.Int32 _Width = _Rect.Right - _Rect.Left;
            System.Int32 _Height = _Rect.Bottom - _Rect.Top;

            if (!RawWin32Methods.MoveWindow(_hWnd, _NewX, _NewY, _Width, _Height, bRepaint: true)) { throw new System.Exception("Couldn't MoveWindow() for (HWND)" + _hWnd); }

        }

        /// <summary>Shows/hides the specified window.</summary>
        public static void SetWindowVisibility(System.IntPtr _hWnd, System.Boolean _MakeWindowVisible) {

            if (
                !RawWin32Methods.ShowWindow(
                    _hWnd,
                    _MakeWindowVisible ? RawWin32Methods.SW_SHOW : RawWin32Methods.SW_HIDE
                )
            ) { throw new System.Exception("Couldn't ShowWindow() for (HWND)" + _hWnd); }

        }

        /// <summary>Enables/disables the specified window.</summary>
        public static void SetWindowEnabled(System.IntPtr _hWnd, System.Boolean _MakeWindowEnabled) {

            if (!RawWin32Methods.EnableWindow(_hWnd, _MakeWindowEnabled)) {
                throw new System.Exception("Couldn't EnableWindow() for (HWND)" + _hWnd);
            }

        }

        /// <summary>Gets the window's width &amp; height.</summary>
        public static System.Drawing.Size GetWindowSize(System.IntPtr _hWnd) {

            RawWin32Methods.RECT _Rect;
            if (!RawWin32Methods.GetWindowRect(_hWnd, out _Rect)) {
                throw new System.Exception("Couldn't GetWindowRect() for (HWND)" + _hWnd);
            }

            System.Int32 _Width = _Rect.Right - _Rect.Left;
            System.Int32 _Height = _Rect.Bottom - _Rect.Top;

            return new System.Drawing.Size(_Width, _Height);

        }

        /// <summary>Sets the window's width &amp; height (...retaining the current top-left position).</summary>
        public static void SetWindowSize(System.IntPtr _hWnd, System.Int32 _Width, System.Int32 _Height) {

            RawWin32Methods.RECT _Rect;
            if (!RawWin32Methods.GetWindowRect(_hWnd, out _Rect)) {
                throw new System.Exception("Couldn't GetWindowRect() for (HWND)" + _hWnd);
            }

            if (!RawWin32Methods.MoveWindow(_hWnd, X: _Rect.Left, Y: _Rect.Top, nWidth: _Width, nHeight: _Height, bRepaint: true)) {
                throw new System.Exception("Couldn't MoveWindow() for (HWND)" + _hWnd);
            }

        }

        #endregion

    }
}