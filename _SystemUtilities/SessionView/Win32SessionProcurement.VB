Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Runtime.InteropServices

Public Module Win32SessionProcurement

	<DllImport("wtsapi32.dll")>
	Private Function WTSOpenServer(
 <MarshalAs(UnmanagedType.LPStr)> ByVal pServerName As String) As IntPtr
	End Function

	<DllImport("wtsapi32.dll")>
	Private Sub WTSCloseServer(ByVal hServer As IntPtr)
	End Sub

	<DllImport("wtsapi32.dll")>
	Private Function WTSEnumerateSessions(ByVal hServer As IntPtr,
 <MarshalAs(UnmanagedType.U4)> ByVal Reserved As Int32,
 <MarshalAs(UnmanagedType.U4)> ByVal Version As Int32, ByRef ppSessionInfo As IntPtr,
 <MarshalAs(UnmanagedType.U4)> ByRef pCount As Int32) As Int32
	End Function

	<DllImport("wtsapi32.dll")>
	Private Sub WTSFreeMemory(ByVal pMemory As IntPtr)
	End Sub

	<DllImport("Wtsapi32.dll")>
	Private Function WTSQuerySessionInformation(ByVal hServer As System.IntPtr, ByVal sessionId As Integer, ByVal wtsInfoClass As WTS_INFO_CLASS, <Out()> ByRef ppBuffer As System.IntPtr, <Out()> ByRef pBytesReturned As UInteger) As Boolean
	End Function

	<StructLayout(LayoutKind.Sequential)>
	Private Structure WTS_SESSION_INFO
		Public SessionID As Int32
		<MarshalAs(UnmanagedType.LPStr)>
		Public pWinStationName As String
		Public State As WTS_CONNECTSTATE_CLASS
	End Structure

	Public Enum WTS_INFO_CLASS
		WTSInitialProgram
		WTSApplicationName
		WTSWorkingDirectory
		WTSOEMId
		WTSSessionId
		WTSUserName
		WTSWinStationName
		WTSDomainName
		WTSConnectState
		WTSClientBuildNumber
		WTSClientName
		WTSClientDirectory
		WTSClientProductId
		WTSClientHardwareId
		WTSClientAddress
		WTSClientDisplay
		WTSClientProtocolType
	End Enum

	Public Enum WTS_CONNECTSTATE_CLASS
		WTSActive
		WTSConnected
		WTSConnectQuery
		WTSShadow
		WTSDisconnected
		WTSIdle
		WTSListen
		WTSReset
		WTSDown
		WTSInit
	End Enum

	Public Function OpenServer(ByVal Name As String) As IntPtr
		Dim server As IntPtr = WTSOpenServer(Name)
		Return server
	End Function

	Public Sub CloseServer(ByVal ServerHandle As IntPtr)
		WTSCloseServer(ServerHandle)
	End Sub

	Public Structure Win32Session
		Public Domain$, Username$, ClientName$, SessionID% ', WinStationName$, State$
	End Structure

	Public Function GetSessionsForServer(ByVal ServerName$) As Win32Session()

		Dim _Sessions_ToReturn As New List(Of Win32Session)()

		Dim serverHandle As IntPtr = IntPtr.Zero
		Dim resultList As List(Of String) = New List(Of String)()
		serverHandle = OpenServer(ServerName)

		Try
			Dim SessionInfoPtr As IntPtr = IntPtr.Zero
			Dim userPtr As IntPtr = IntPtr.Zero
			Dim domainPtr As IntPtr = IntPtr.Zero
			Dim clientNamePtr As IntPtr = IntPtr.Zero
			Dim sessionCount As Int32 = 0
			Dim retVal As Int32 = WTSEnumerateSessions(serverHandle, 0, 1, SessionInfoPtr, sessionCount)
			Dim dataSize As Int32 = Marshal.SizeOf(GetType(WTS_SESSION_INFO))
			Dim currentSession As Int32 = CInt(SessionInfoPtr)
			Dim bytes As UInteger = 0

			If retVal <> 0 Then

				For i As Integer = 0 To sessionCount - 1
					Dim si As WTS_SESSION_INFO = CType(Marshal.PtrToStructure(CType(currentSession, System.IntPtr), GetType(WTS_SESSION_INFO)), WTS_SESSION_INFO)
					currentSession += dataSize
					WTSQuerySessionInformation(serverHandle, si.SessionID, WTS_INFO_CLASS.WTSUserName, userPtr, bytes)
					WTSQuerySessionInformation(serverHandle, si.SessionID, WTS_INFO_CLASS.WTSDomainName, domainPtr, bytes)
					WTSQuerySessionInformation(serverHandle, si.SessionID, WTS_INFO_CLASS.WTSClientName, clientNamePtr, bytes)

					If Marshal.PtrToStringAnsi(domainPtr).Length > 0 AndAlso Marshal.PtrToStringAnsi(userPtr).Length > 0 Then

						If Marshal.PtrToStringAnsi(clientNamePtr).Length < 1 Then
							_Sessions_ToReturn.Add(New Win32Session() With {.Domain = Marshal.PtrToStringAnsi(domainPtr), .Username = Marshal.PtrToStringAnsi(userPtr), .SessionID = si.SessionID})
							'Console.WriteLine(Marshal.PtrToStringAnsi(domainPtr) & "\" & Marshal.PtrToStringAnsi(userPtr) & vbTab & "SessionID: " & si.SessionID & vbTab & "ClientName: n/a")
						Else
							'Console.WriteLine(Marshal.PtrToStringAnsi(domainPtr) & "\" & Marshal.PtrToStringAnsi(userPtr) & vbTab & "SessionID: " & si.SessionID & vbTab & "ClientName: " & Marshal.PtrToStringAnsi(clientNamePtr))
							_Sessions_ToReturn.Add(New Win32Session() With {.Domain = Marshal.PtrToStringAnsi(domainPtr), .Username = Marshal.PtrToStringAnsi(userPtr), .SessionID = si.SessionID, .ClientName = Marshal.PtrToStringAnsi(clientNamePtr)})
						End If
					End If

					WTSFreeMemory(clientNamePtr)
					WTSFreeMemory(userPtr)
					WTSFreeMemory(domainPtr)
				Next

				WTSFreeMemory(SessionInfoPtr)
			End If

		Catch ex As Exception
			Throw New Exception("@GetSessionsForServer: " & ex.Message)
		Finally
			CloseServer(serverHandle)
		End Try

		Return _Sessions_ToReturn.ToArray()

	End Function

End Module