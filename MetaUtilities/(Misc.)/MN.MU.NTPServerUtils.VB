Namespace MetaUtilities
	Public Module NTPServerUtils

		Public Const NTPPort As Int32 = 123UI

		Public Function GetDataTime_FromNTPServerResponseBytes(ByVal _NTPServerResponseBytes As Byte()) As DateTime

			Const _OffsetTransmitTime As Byte = 40
			Dim _IntPart As UInt64 = 0
			Dim _FractionPart As UInt64 = 0

			For _Index% = 0 To 3 Step +1 : _IntPart = Convert.ToUInt64((256 * _IntPart) + _NTPServerResponseBytes.ElementAt(_OffsetTransmitTime + _Index)) : Next
			For _Index% = 4 To 7 : _FractionPart = Convert.ToUInt64((256 * _FractionPart) + _NTPServerResponseBytes.ElementAt(_OffsetTransmitTime + _Index)) : Next

			Dim _Milliseconds As UInt64 = Convert.ToUInt64((_IntPart * 1000) + (_FractionPart * 1000 / &H100000000L))
			Dim _TimeSpan As TimeSpan = TimeSpan.FromTicks(Convert.ToInt64(_Milliseconds) * TimeSpan.TicksPerMillisecond)
			Dim _DateTime As New DateTime(1900, 1, 1) : _DateTime += _TimeSpan
			Dim _Offset As TimeSpan = TimeZone.CurrentTimeZone.GetUtcOffset(_DateTime)

			Return _DateTime + _Offset

		End Function

		Public Function GetNTPServerResponseBytes(ByVal _NTPServer As String) As Byte()

			REM Get an IP Address
			Dim _NTPServer_IPAddresses As System.Net.IPAddress() = System.Net.Dns.GetHostEntry(_NTPServer).AddressList
			If (_NTPServer_IPAddresses Is Nothing) OrElse (_NTPServer_IPAddresses.Length = 0) Then Throw New Exception("An IP Address for the NTP Server " & _NTPServer.InSquares() & " could not be resolved")

			REM Get a Socket to the NTP EndPoint
			Dim _IPEndPoint As New System.Net.IPEndPoint(address:=_NTPServer_IPAddresses.ElementAt(0), port:=NTPServerUtils.NTPPort)
			Dim _Socket As New System.Net.Sockets.Socket(System.Net.Sockets.AddressFamily.InterNetwork, System.Net.Sockets.SocketType.Dgram, System.Net.Sockets.ProtocolType.Udp)
			_Socket.Connect(_IPEndPoint)

			REM Construct the Response Bytes
			Dim _NTPRequest_Buffer As Byte() = New [Byte](47) {}
			_NTPRequest_Buffer(0) = &H1B
			For _Index% = 1 To 48 - 1 Step +1 : _NTPRequest_Buffer(_Index) = 0 : Next

			_Socket.Send(_NTPRequest_Buffer)
			_Socket.Receive(_NTPRequest_Buffer)
			_Socket.Close()

			Return _NTPRequest_Buffer

		End Function

	End Module
End Namespace