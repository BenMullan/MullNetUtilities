namespace WinMorph32 {
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
        public static extern System.Int32 GetWindowText(System.IntPtr hWnd, System.Text.StringBuilder lpString, System.Int32 nMaxCount);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern System.Int32 GetWindowTextLength(System.IntPtr hWnd);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern System.Boolean EnableWindow(System.IntPtr hWnd, System.Boolean bEnable);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern System.Boolean ShowWindow(System.IntPtr hWnd, System.Int32 nCmdShow);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.Bool)]
        public static extern System.Boolean IsWindowVisible(System.IntPtr hWnd);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.Bool)]
        public static extern System.Boolean IsWindowEnabled(System.IntPtr hWnd);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern System.UInt32 GetWindowThreadProcessId(System.IntPtr hWnd, out System.UInt32 processId);

        [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Unicode)]
        public static extern System.Int32 GetClassName(System.IntPtr hWnd, System.Text.StringBuilder lpClassName, System.Int32 nMaxCount);

        /**/

        /*
         * - WindowState (maximised, normal, minimised)
         * - GetWindowTextSafe() (cross-process, use WM_GETTEXT instead of GetWindowText)
         * - UIAutomation?
         */

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern System.IntPtr GetForegroundWindow();

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern System.Boolean SetForegroundWindow(System.IntPtr hWnd);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern System.IntPtr GetParent(System.IntPtr hWnd);

        [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        public static extern System.IntPtr SendMessage(System.IntPtr hWnd, System.UInt32 Msg, System.IntPtr wParam, System.IntPtr lParam);

        [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        public static extern System.Boolean PostMessage(System.IntPtr hWnd, System.UInt32 Msg, System.IntPtr wParam, System.IntPtr lParam);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern System.Boolean DestroyWindow(System.IntPtr hWnd);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern System.Boolean FlashWindow(System.IntPtr hWnd, System.Boolean bInvert);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern System.IntPtr GetDesktopWindow();

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern System.Boolean UpdateWindow(System.IntPtr hWnd);

        /**/

        // RECT for sizing
        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
        public static extern System.Boolean GetWindowRect(System.IntPtr hWnd, out RECT lpRect);

        [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
        public struct RECT { public System.Int32 Left, Top, Right, Bottom; }


        // Move & Position
        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
        public static extern System.Boolean MoveWindow(System.IntPtr hWnd, System.Int32 X, System.Int32 Y, System.Int32 nWidth, System.Int32 nHeight, System.Boolean bRepaint);

        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
        public static extern System.Boolean SetWindowPos(
            System.IntPtr hWnd, System.IntPtr hWndInsertAfter,
            System.Int32 X, System.Int32 Y, System.Int32 cx, System.Int32 cy, System.UInt32 uFlags
        );

        public const System.UInt32 SWP_NOMOVE       = 0x0002;
        public const System.UInt32 SWP_NOSIZE       = 0x0001;
        public const System.UInt32 SWP_NOZORDER     = 0x0004;
        public const System.UInt32 SWP_FRAMECHANGED = 0x0020;


        // Styles
        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
        public static extern System.Int32 GetWindowLong(System.IntPtr hWnd, System.Int32 nIndex);
        
        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
        public static extern System.Int32 SetWindowLong(System.IntPtr hWnd, System.Int32 nIndex, System.Int32 dwNewLong);

        public const System.Int32 GWL_STYLE   = -16;
        public const System.Int32 WS_BORDER   = 0x00800000;
        public const System.Int32 GWL_EXSTYLE = -20;
        public const System.Int32 WS_EX_LAYERED = 0x80000;


        // Transparency
        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
        public static extern System.Boolean SetLayeredWindowAttributes(System.IntPtr hwnd, System.UInt32 crKey, byte bAlpha, System.UInt32 dwFlags);
        
        public const System.Int32 LWA_ALPHA = 0x2;


        // Font
        public const System.UInt32 WM_SETFONT = 0x0030;

        [System.Runtime.InteropServices.DllImport("gdi32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        public static extern System.IntPtr CreateFont(
            System.Int32 cHeight, System.Int32 cWidth, System.Int32 cEscapement, System.Int32 cOrientation,
            System.Int32 cWeight, System.UInt32 bItalic, System.UInt32 bUnderline, System.UInt32 bStrikeOut,
            System.UInt32 iCharSet, System.UInt32 iOutPrecision, System.UInt32 iClipPrecision,
            System.UInt32 iQuality, System.UInt32 iPitchAndFamily, string pszFaceName
        );


        // Background Colour
        [System.Runtime.InteropServices.DllImport("gdi32.dll", SetLastError = true)]
        public static extern System.IntPtr CreateSolidBrush(System.UInt32 crColor);

        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
        public static extern System.IntPtr SetClassLongPtr(System.IntPtr hWnd, System.Int32 nIndex, System.IntPtr dwNewLong);
        
        public const System.Int32 GCLP_HBRBACKGROUND = -10;

        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
        public static extern System.Boolean InvalidateRect(System.IntPtr hWnd, System.IntPtr lpRect, System.Boolean bErase);

        /**/

        // ShowWindow() commands
        public const System.Int32 SW_MINIMIZE      = 6;
        public const System.Int32 SW_MAXIMIZE      = 3;
        public const System.Int32 SW_RESTORE       = 9;

        // SetWindowPos() special HWND values
        public static readonly System.IntPtr HWND_TOP       = new System.IntPtr(0);
        public static readonly System.IntPtr HWND_BOTTOM    = new System.IntPtr(1);
        public static readonly System.IntPtr HWND_TOPMOST   = new System.IntPtr(-1);
        public static readonly System.IntPtr HWND_NOTOPMOST = new System.IntPtr(-2);

        // SetWindowPos() flags
        public const System.UInt32 SWP_SHOWWINDOW   = 0x0040;
        public const System.UInt32 SWP_HIDEWINDOW   = 0x0080;

        // Window styles
        public const System.Int32 WS_OVERLAPPEDWINDOW = 0x00CF0000;
        public const System.Int32 WS_POPUP            = unchecked((int)0x80000000);
        public const System.Int32 WS_VISIBLE          = 0x10000000;
        public const System.Int32 WS_DISABLED         = 0x08000000;
        public const System.Int32 WS_MINIMIZE         = 0x20000000;
        public const System.Int32 WS_MAXIMIZE         = 0x01000000;

        // Extended window styles
        public const System.Int32 WS_EX_TOPMOST      = 0x00000008;
        public const System.Int32 WS_EX_TOOLWINDOW   = 0x00000080;
        public const System.Int32 WS_EX_APPWINDOW    = 0x00040000;

        // Window messages
        public const System.UInt32 WM_CLOSE          = 0x0010;
        public const System.UInt32 WM_SETTEXT        = 0x000C;
        public const System.UInt32 WM_GETTEXT        = 0x000D;
        public const System.UInt32 WM_GETTEXTLENGTH  = 0x000E;

    }
}