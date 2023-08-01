Namespace MetaUtilities
	Public Module Screenshotting

		''' <summary>WARNING: Tends to cause an Exception after ~40+ calls, because of a bug in GDI+.</summary>
		Public Function GetScreenshot() As System.Drawing.Bitmap

			REM Using the VirtualScreen, accounts for all monitors.

			Dim _ScalingFactor As [Decimal] = Screenshotting.GetScalingFactor()
			Dim _ScreenSize As New Size(width:=CInt(Windows.Forms.SystemInformation.VirtualScreen.Width * _ScalingFactor), height:=CInt(Windows.Forms.SystemInformation.VirtualScreen.Height * _ScalingFactor))
			Dim _ScreenGrab As New Bitmap(width:=_ScreenSize.Width, height:=_ScreenSize.Height)
			Dim _Graphics As Drawing.Graphics = Graphics.FromImage(_ScreenGrab)

			_Graphics.CopyFromScreen( _
			 upperLeftSource:=New Point(Windows.Forms.SystemInformation.VirtualScreen.Left, Windows.Forms.SystemInformation.VirtualScreen.Top), _
			 upperLeftDestination:=New Point(0, 0), _
			 blockRegionSize:=_ScreenSize _
			)

			Return _ScreenGrab

		End Function

		''' <summary>Does not cause a bug after some time, because the objects are properly disposed of, in each call.</summary>
		Public Sub TakeAndSaveScreenshot(ByVal _SaveAsPath$)

			REM Using the VirtualScreen, accounts for all monitors.

			Dim _ScalingFactor As [Decimal] = GetScalingFactor()
			Dim _ScreenSize As New Size(CInt(Windows.Forms.SystemInformation.VirtualScreen.Width * _ScalingFactor), CInt(Windows.Forms.SystemInformation.VirtualScreen.Height * _ScalingFactor))
			Dim _ScreenGrab As New Bitmap(_ScreenSize.Width, _ScreenSize.Height)

			Dim _Graphics As Graphics = Graphics.FromImage(_ScreenGrab)
			_Graphics.CopyFromScreen( _
			 upperLeftSource:=New Point(Windows.Forms.SystemInformation.VirtualScreen.Left, Windows.Forms.SystemInformation.VirtualScreen.Top), _
			 upperLeftDestination:=New Point(0, 0), _
			 blockRegionSize:=_ScreenSize _
			)

			_ScreenGrab.Save(_SaveAsPath, ImageFormat.Png)

			_ScreenGrab.Dispose() : _Graphics.Dispose()

		End Sub

#Region "ScalingFactor-Handling"

		<System.Runtime.InteropServices.DllImport("gdi32.dll")>
		Private Function GetDeviceCaps(ByVal hdc As IntPtr, ByVal nIndex As Int32) As Int32
		End Function

		Public Enum DeviceCap
			VERTRES = 10
			DESKTOPVERTRES = 117
		End Enum

		Public Function GetScalingFactor() As [Decimal]
			Try

				Dim _Graphics As Graphics = Graphics.FromHwnd(IntPtr.Zero)
				Dim _Desktop As IntPtr = _Graphics.GetHdc()
				Dim LogicalScreenHeight% = GetDeviceCaps(_Desktop, CInt(DeviceCap.VERTRES))
				Dim PhysicalScreenHeight% = GetDeviceCaps(_Desktop, CInt(DeviceCap.DESKTOPVERTRES))

				Dim _ScreenScalingFactor As [Decimal] = CDec(PhysicalScreenHeight) / CDec(LogicalScreenHeight)
				Return _ScreenScalingFactor	' 1.25 = 125%

			Catch _Ex As Exception
				Debug.WriteLine("Couldn't resolve ScalingFactor: " & _Ex.Message)
				Return 1
			End Try

		End Function

#End Region

	End Module
End Namespace