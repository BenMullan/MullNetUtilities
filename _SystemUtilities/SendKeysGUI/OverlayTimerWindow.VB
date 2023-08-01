Public Class OverlayTimerWindow

	''' <summary>
	''' Returns once a New instance of the Window has been shown for _SecondsToShowFor
	''' </summary>
	''' <param name="_SecondsToShowFor">E.g. 3</param>
	''' <param name="_InstructionMessage">E.g. "Position the Mouse over the Target Window"</param>
	''' <param name="_ActionAfterTimeMessage">E.g. "Capturing MousePosition in..."</param>
	Public Shared Sub ShowNew(ByVal _SecondsToShowFor As UInt16, ByVal _InstructionMessage$, ByVal _ActionAfterTimeMessage$)

		Dim _NewOverlayTimeWindow As New OverlayTimerWindow()
		_NewOverlayTimeWindow.InstructionLabel.Text = _InstructionMessage
		_NewOverlayTimeWindow.ActionAfterTimeLabel.Text = _ActionAfterTimeMessage
		_NewOverlayTimeWindow.SecondsLeftLabel.Text = _SecondsToShowFor.ToString()

		_NewOverlayTimeWindow.Show() : _NewOverlayTimeWindow.Refresh()

		For _SecondsLeft As Int32 = (_SecondsToShowFor) To (1) Step (-1)
			_NewOverlayTimeWindow.SecondsLeftLabel.Text = _SecondsLeft.ToString() : _NewOverlayTimeWindow.Refresh()
			Threading.Thread.Sleep(1000)
		Next

		_NewOverlayTimeWindow.Hide()

	End Sub

End Class