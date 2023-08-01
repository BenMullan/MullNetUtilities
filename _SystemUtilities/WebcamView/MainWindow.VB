Public Class MainWindow

	Public Sub SelectCameraAndInitialiseVideoStream() Handles MyBase.Shown
		Try

			'Choose a Camera Device
			Dim _CameraDeviceProvider As New Global.Camera_NET.CameraChoice()
			_CameraDeviceProvider.UpdateDeviceList()
			If (_CameraDeviceProvider.Devices.Count = 0) Then : MsgBox("No Camera Devices were detected", MsgBoxStyle.Critical) : Application.Exit() : End If

			Dim _ChooseCameraDialog As New MullNet.MetaUtilities.Dialogs.ChooseFromListDialog(Of DirectShowLib.DsDevice)(_CameraDeviceProvider.Devices.ToArray(), "Camera Device", Function(_Camera As DirectShowLib.DsDevice) _Camera.Name)

			'User could have clicked [Cancel] or pressed [Esc]
			If Not ((_ChooseCameraDialog.ShowDialog() = (Windows.Forms.[DialogResult]).OK) AndAlso (_ChooseCameraDialog.ChosenItem IsNot Nothing)) Then Application.Exit()

			'Use the Maximum Resolution
			Dim _MaxResolution_ForChosenCamera As Camera_NET.Resolution = MainWindow.GetHighestResolutionForCamera_(_ChooseCameraDialog.ChosenItem.Mon)
			Me.TheCameraControl.SetCamera(moniker:=_ChooseCameraDialog.ChosenItem.Mon, resolution:=_MaxResolution_ForChosenCamera)
			Me.Text = _ChooseCameraDialog.ChosenItem.Name & If(_ChooseCameraDialog.ChosenItem.DevicePath.ToUpper().Contains("USB"), " (USB)", "") & " @ " & _MaxResolution_ForChosenCamera.ToString()

			'Scale the Window, so that the Width is 500 (as set by the Designer), and the Height corrosponds according to the resolution of the camera
			Me.Width = Me.Width
			Me.Height = CInt(CInt(_MaxResolution_ForChosenCamera.Height * (Me.Width / _MaxResolution_ForChosenCamera.Width)) * 1.09)
			Me.Enabled = True

			'Release the Camera on Exit
			AddHandler Me.FormClosed, Sub() Me.TheCameraControl.CloseCamera()

		Catch _Ex As Exception
			MsgBox(_Ex.Message, MsgBoxStyle.Critical, "Could not initialise Video Stream")
			Application.Exit()
		End Try
	End Sub

	Private Shared Function GetHighestResolutionForCamera_(ByRef _CameraDevice_Moniker As System.Runtime.InteropServices.ComTypes.IMoniker) As Camera_NET.Resolution

		Dim _AllSupportedResolutions_ForSpecifiedCamera As Camera_NET.ResolutionList = Camera_NET.Camera.GetResolutionList(_CameraDevice_Moniker)
		Dim _TotalPixelCounts_ForEachSupportedResolution As Int32() = _AllSupportedResolutions_ForSpecifiedCamera.Select(Of Int32)(Function(_Resolution As Camera_NET.Resolution) _Resolution.Width * _Resolution.Height).ToArray()

		Return _AllSupportedResolutions_ForSpecifiedCamera.ElementAt(
		 _TotalPixelCounts_ForEachSupportedResolution.ToList().IndexOf(
		  _TotalPixelCounts_ForEachSupportedResolution.Max()
		 )
		)

	End Function

#Region "Close the form on pressing [Esc]"

	Protected Sub Register_CloseOnEscKey_Handler() Handles Me.Load
		Me.KeyPreview = True
		AddHandler Me.KeyDown, Sub(_Sender As Object, _KeyEventArgs As Windows.Forms.KeyEventArgs) If _KeyEventArgs.KeyCode = Windows.Forms.Keys.Escape Then Me.Close()
	End Sub

	Protected Overrides Function ProcessDialogKey(keyData As System.Windows.Forms.Keys) As Boolean
		If (Windows.Forms.Form.ModifierKeys = Windows.Forms.Keys.None) AndAlso (keyData = Windows.Forms.Keys.Escape) Then
			Me.Close() : Return True
		End If
		Return MyBase.ProcessDialogKey(keyData)
	End Function

#End Region

End Class