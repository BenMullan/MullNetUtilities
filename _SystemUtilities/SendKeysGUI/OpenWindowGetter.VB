Imports System.Runtime.InteropServices
Imports HWND = System.IntPtr

Module OpenWindowGetter

	Function GetOpenWindows() As IDictionary(Of HWND, String)

		Dim shellWindow As HWND = GetShellWindow()
		Dim windows As New Dictionary(Of HWND, String)()

		EnumWindows(
		 Function(hWnd As HWND, lParam As Integer) As Boolean

			 If hWnd = shellWindow Then Return True
			 If Not IsWindowVisible(hWnd) Then Return True

			 Dim length As Integer = GetWindowTextLength(hWnd) : If length = 0 Then Return True

			 Dim builder As New System.Text.StringBuilder(length)
			 GetWindowText(hWnd, builder, length + 1)
			 windows(hWnd) = builder.ToString()

			 Return True

		 End Function,
		 0
		 )

		Return windows

	End Function

	Private Delegate Function EnumWindowsProc(ByVal hWnd As HWND, ByVal lParam As Integer) As Boolean

	<DllImport("USER32.DLL")>
	Private Function EnumWindows(ByVal enumFunc As EnumWindowsProc, ByVal lParam As Integer) As Boolean
	End Function

	<DllImport("USER32.DLL")>
	Private Function GetWindowText(ByVal hWnd As HWND, ByVal lpString As System.Text.StringBuilder, ByVal nMaxCount As Integer) As Integer
	End Function

	<DllImport("USER32.DLL")>
	Private Function GetWindowTextLength(ByVal hWnd As HWND) As Integer
	End Function

	<DllImport("USER32.DLL")>
	Private Function IsWindowVisible(ByVal hWnd As HWND) As Boolean
	End Function

	<DllImport("USER32.DLL")>
	Private Function GetShellWindow() As IntPtr
	End Function

	''' <summary>
	'''     Brings the thread that created the specified window into the foreground and activates the window. Keyboard input is
	'''     directed to the window, and various visual cues are changed for the user. The system assigns a slightly higher
	'''     priority to the thread that created the foreground window than it does to other threads.
	'''     <para>See for https://msdn.microsoft.com/en-us/library/windows/desktop/ms633539%28v=vs.85%29.aspx more information.</para>
	'''  </summary>
	''' <param name="hWnd">
	'''     C++ ( hWnd [in]. Type: HWND )<br />A handle to the window that should be activated and brought to the foreground.
	''' </param>
	''' <returns>
	'''     <c>true</c> or nonzero if the window was brought to the foreground, <c>false</c> or zero If the window was not
	'''     brought to the foreground.
	''' </returns>
	''' <remarks>
	'''     The system restricts which processes can set the foreground window. A process can set the foreground window only if
	'''     one of the following conditions is true:
	'''     <list type="bullet">
	'''     <listheader>
	'''         <term>Conditions</term><description></description>
	'''     </listheader>
	'''     <item>The process is the foreground process.</item>
	'''     <item>The process was started by the foreground process.</item>
	'''     <item>The process received the last input event.</item>
	'''     <item>There is no foreground process.</item>
	'''     <item>The process is being debugged.</item>
	'''     <item>The foreground process is not a Modern Application or the Start Screen.</item>
	'''     <item>The foreground is not locked (see LockSetForegroundWindow).</item>
	'''     <item>The foreground lock time-out has expired (see SPI_GETFOREGROUNDLOCKTIMEOUT in SystemParametersInfo).</item>
	'''     <item>No menus are active.</item>
	'''     </list>
	'''     <para>
	'''     An application cannot force a window to the foreground while the user is working with another window.
	'''     Instead, Windows flashes the taskbar button of the window to notify the user.
	'''     </para>
	'''     <para>
	'''     A process that can set the foreground window can enable another process to set the foreground window by
	'''     calling the AllowSetForegroundWindow function. The process specified by dwProcessId loses the ability to set
	'''     the foreground window the next time the user generates input, unless the input is directed at that process, or
	'''     the next time a process calls AllowSetForegroundWindow, unless that process is specified.
	'''     </para>
	'''     <para>
	'''     The foreground process can disable calls to SetForegroundWindow by calling the LockSetForegroundWindow
	'''     function.
	'''     </para>
	''' </remarks>
	<DllImport("USER32.DLL")> _
	Public Function SetForegroundWindow(ByVal hWnd As IntPtr) As <MarshalAs(UnmanagedType.Bool)> Boolean
	End Function

End Module