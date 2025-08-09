using System.Linq;

namespace WinMorph32 {
    public static class Win32Functions {

        public static class RawWin32Methods {

            public const System.Int32 SW_HIDE = 0;
            public const System.Int32 SW_SHOW = 5;

            public delegate System.Boolean EnumWindowsProc(System.IntPtr hWnd, System.IntPtr lParam);

            [System.Runtime.InteropServices.DllImport("user32.dll")]
            public static extern System.Boolean EnumWindows(EnumWindowsProc lpEnumFunc, System.IntPtr lParam);

            [System.Runtime.InteropServices.DllImport("user32.dll")]
            public static extern System.Boolean EnumChildWindows(System.IntPtr hwndParent, EnumWindowsProc lpEnumFunc, System.IntPtr lParam);

            [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
            public static extern System.Boolean SetWindowText(System.IntPtr hWnd, System.String lpString);

            [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Unicode)]
            public static extern int GetWindowText(System.IntPtr hWnd, System.Text.StringBuilder lpString, int nMaxCount);

            [System.Runtime.InteropServices.DllImport("user32.dll")]
            public static extern int GetWindowTextLength(System.IntPtr hWnd);

            [System.Runtime.InteropServices.DllImport("user32.dll")]
            public static extern System.Boolean EnableWindow(System.IntPtr hWnd, System.Boolean bEnable);

            [System.Runtime.InteropServices.DllImport("user32.dll")]
            public static extern System.Boolean ShowWindow(System.IntPtr hWnd, System.Int32 nCmdShow);

            [System.Runtime.InteropServices.DllImport("user32.dll")]
            public static extern System.Boolean IsWindowVisible(System.IntPtr hWnd);

            [System.Runtime.InteropServices.DllImport("user32.dll")]
            public static extern System.Boolean IsWindowEnabled(System.IntPtr hWnd);

            [System.Runtime.InteropServices.DllImport("user32.dll")]
            public static extern System.UInt32 GetWindowThreadProcessId(System.IntPtr hWnd, out System.UInt32 processId);

            [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Unicode)]
            public static extern System.Int32 GetClassName(System.IntPtr hWnd, System.Text.StringBuilder lpClassName, int nMaxCount);

        }

        /// <summary>Eg: $WMH-0xA3F88E9B3</summary>
        public const System.String WinMorph_SerialisedHwndPrefix = "$WMH-0x";

        /// <summary>Represents a top-level window with a {HWND, Title, ClassName, ProcessName, and PID}.</summary>
        public struct TLWindowInfo { public System.String HWND, Title, ClassName, ProcessName; public System.UInt32 PID; }

        private static System.String HwndToWMHexString_(System.IntPtr _hWnd) {
            if (System.IntPtr.Size == 4)    { return WinMorph_SerialisedHwndPrefix + _hWnd.ToInt32().ToString("X8");  /* 32-bit */ }
            else                            { return WinMorph_SerialisedHwndPrefix + _hWnd.ToInt64().ToString("X16"); /* 64-bit */ }
        }

        private static System.IntPtr HwndFromWMHexString_(System.String _HexString) {

            // _HexString should look like eg: $WMH-0xA3F88E9B3
            // ; strip the 1st 7 chars...

            System.String _CleanedHex = _HexString.Substring(WinMorph_SerialisedHwndPrefix.Length);

            if (System.IntPtr.Size == 4)    { return new System.IntPtr(System.Convert.ToInt32(_CleanedHex, 16)); /* 32-bit */ }
            else                            { return new System.IntPtr(System.Convert.ToInt64(_CleanedHex, 16)); /* 64-bit */ }

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

        /// <summary>Returns the {HWND, Title, ClassName, ProcessName, and PID} for all top-level windows.</summary>
        public static Win32Functions.TLWindowInfo[] EnumTopLevelWindows() {

            System.Collections.Generic.List<Win32Functions.TLWindowInfo> _TLWindowInfos =
                new System.Collections.Generic.List<Win32Functions.TLWindowInfo>()
            ;

            RawWin32Methods.EnumWindows(
                (System.IntPtr _hWnd, System.IntPtr _lParam) => {

                    Win32Functions.TLWindowInfo _TLWindowInfo = new Win32Functions.TLWindowInfo();

                    // HWND
                    _TLWindowInfo.HWND = Win32Functions.HwndToWMHexString_(_hWnd);

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

                    _TLWindowInfos.Add(_TLWindowInfo);

                    // continue enumerating...
                    return true;

                },
                System.IntPtr.Zero
            );

            return _TLWindowInfos.ToArray();

        }

        #endregion

        /**/

        // RECT for sizing
        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
        public static extern System.Boolean GetWindowRect(System.IntPtr hWnd, out RECT lpRect);
        public struct RECT { public System.Int32 Left, Top, Right, Bottom; }


        // Move & Position
        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
        public static extern System.Boolean MoveWindow(System.IntPtr hWnd, int X, int Y, int nWidth, int nHeight, System.Boolean bRepaint);
            
        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
        public static extern System.Boolean SetWindowPos(
            System.IntPtr hWnd, System.IntPtr hWndInsertAfter,
            int X, int Y, int cx, int cy, uint uFlags
        );

        public const uint SWP_NOMOVE       = 0x0002;
        public const uint SWP_NOSIZE       = 0x0001;
        public const uint SWP_NOZORDER     = 0x0004;
        public const uint SWP_FRAMECHANGED = 0x0020;


        // Styles
        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
        public static extern int GetWindowLong(System.IntPtr hWnd, int nIndex);
        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
        public static extern int SetWindowLong(System.IntPtr hWnd, int nIndex, int dwNewLong);
        public const int GWL_STYLE   = -16;
        public const int WS_BORDER   = 0x00800000;
        public const int GWL_EXSTYLE = -20;
        public const int WS_EX_LAYERED = 0x80000;


        // Transparency
        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
        public static extern System.Boolean SetLayeredWindowAttributes(System.IntPtr hwnd, uint crKey, byte bAlpha, uint dwFlags);
        public const int LWA_ALPHA = 0x2;

        // Font
        [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        public static extern System.IntPtr SendMessage(System.IntPtr hWnd, uint Msg, System.IntPtr wParam, System.IntPtr lParam);
        public const uint WM_SETFONT = 0x0030;

        [System.Runtime.InteropServices.DllImport("gdi32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        public static extern System.IntPtr CreateFont(
            int cHeight, int cWidth, int cEscapement, int cOrientation,
            int cWeight, uint bItalic, uint bUnderline, uint bStrikeOut,
            uint iCharSet, uint iOutPrecision, uint iClipPrecision,
            uint iQuality, uint iPitchAndFamily, string pszFaceName
        );


        // Background Colour
        [System.Runtime.InteropServices.DllImport("gdi32.dll", SetLastError = true)]
        public static extern System.IntPtr CreateSolidBrush(uint crColor);

        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
        public static extern System.IntPtr SetClassLongPtr(System.IntPtr hWnd, int nIndex, System.IntPtr dwNewLong);
        public const int GCLP_HBRBACKGROUND = -10;

        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
        public static extern System.Boolean InvalidateRect(
        System.IntPtr hWnd, System.IntPtr lpRect, System.Boolean bErase);

    }
}