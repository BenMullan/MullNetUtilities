Public Class MainWindow

	Public Const MSToWaitAfterSettingForegroundWindow% = 100

	Private Sub InitialiseGUI() Handles MyBase.Shown

		With Me.TheToolTipProvider
			.SetToolTip(Me, "SendKeys GUI: Send Keystrokes to a target Window")

			.SetToolTip(Me.TargetGroupBox, "Specify the intended target for the Keystrokes")
			.SetToolTip(Me.Target_Unspecified_RadioButton, "No window will be brought to the Foreground before sending the Keystrokes")
			.SetToolTip(Me.Target_ThisWindow_RadioButton, "The selected window will be brought to the Foreground before sending the Keystrokes")
			.SetToolTip(Me.AvaliableWindowsComboBox, "Select a currently-open Window" & vbCrLf & "The Windows' Handles are shown in the form *HANDLE")
			.SetToolTip(Me.SelectWindowByPositionButton, "Use the Mouse Cursor to select a Window..." & vbCrLf & vbCrLf & "Note: One Desktop Window may contain several Win32 Windows;" & vbCrLf & "place the mouse accurately over precisely the part of the Window" & vbCrLf & "to which the KeyStrokes should be sent.")
			.SetToolTip(Me.RescanOpenWindowsButton, "Refresh the list of currently-open Windows")

			.SetToolTip(Me.KeystrokesGroupBox, "Specify the Keystrokes to send to the target")
			.SetToolTip(Me.ViewExpansionsLinkLabel, "View the SendKeys Expansions list, and see Examples...")
			.SetToolTip(Me.KeysToSendTextBox, "Specify the Keystrokes to send to the target. See ""Expansions..."" for examples...")

			.SetToolTip(Me.OptionsGroupBox, "Specify parameters regarding the SendKeys operation")
			.SetToolTip(Me.DelayBeforeStartingNumericUpDown, "1 Second = 1000 MS")
			.SetToolTip(Me.DelayBeforeStartingCheckBox, "Check to enable pausing for *ms before sending the Keystrokes to the target")
			.SetToolTip(Me.SendWaitCheckBox, "If checked, SendKeys.SendWait() will be called instead of SendKeys.Send()")

			.SetToolTip(Me.SendKeystrokesButton, "Alt + S")
			.SetToolTip(Me.TheStatusStrip, "The Status will change when [performing a delay], [Scanning or selecting Windows], or [Sending Keystrokes]")
			.SetToolTip(Me.GenerateVBSFileLinkLabel, "Generates a self-contained .VBS File which, when executed, performs the same SendKeys operation as the program currently would")
		End With

		Me.PopulateAvaliableWindowsComboBox()

	End Sub

	Public Sub PopulateAvaliableWindowsComboBox()
		Try
			Me.TheToolStripStatusLabel.Text = "Status: Scanning Open Windows..." : Me.Refresh()
			Me.AvaliableWindowsComboBox.Items.Clear()

			For Each _OpenWindow As KeyValuePair(Of IntPtr, String) In OpenWindowGetter.GetOpenWindows()
				Me.AvaliableWindowsComboBox.Items.Add(_OpenWindow.Value & " *" & _OpenWindow.Key.ToString()) 'Untitled - Notepad *123456
			Next

			REM Prevent the visiable text of the ComboBox from still being Blank
			If Me.AvaliableWindowsComboBox.Items.Count > 0 Then Me.AvaliableWindowsComboBox.SelectedIndex = 0

			Me.TheToolStripStatusLabel.Text = "Status: Idle" : Me.Refresh()
		Catch _Ex As Exception : MsgBox("During OpenWindow Scanning:" & vbCrLf & vbCrLf & _Ex.Message, MsgBoxStyle.Critical, "SendKeysGUI - Error") : End Try
	End Sub

	Public Sub SendKeystrokes() Handles SendKeystrokesButton.Click
		Try

			REM Set the Foreground window if instructed to
			If Me.Target_ThisWindow_RadioButton.Checked Then
				Me.TheToolStripStatusLabel.Text = "Status: Setting Foreground Window..." : Me.Refresh()
				SetForegroundWindow(New Global.System.IntPtr(Convert.ToInt32(Me.AvaliableWindowsComboBox.SelectedItem.ToString().Split("*"c).Last())))	'Untitled - Notepad *123456
				Threading.Thread.Sleep(MSToWaitAfterSettingForegroundWindow)
			End If

			REM Delay if instructed to
			If Me.DelayBeforeStartingCheckBox.Checked Then
				Me.TheToolStripStatusLabel.Text = "Status: Waiting " & (Me.DelayBeforeStartingNumericUpDown.Value / 1000).ToString() & " seconds..." : Me.Refresh()
				Threading.Thread.Sleep(Convert.ToInt32(Me.DelayBeforeStartingNumericUpDown.Value))
			End If

			Me.TheToolStripStatusLabel.Text = "Status: Sending Keystrokes..." : Me.Refresh()

			REM Call the appropriate SendKeys Function
			If Me.SendWaitCheckBox.Checked Then : SendKeys.SendWait(Me.KeysToSendTextBox.Text)
			Else : SendKeys.Send(Me.KeysToSendTextBox.Text) : End If

			Me.TheToolStripStatusLabel.Text = "Status: Idle" : Me.Refresh()

		Catch _Ex As Exception : MsgBox("During Keystroke Sending:" & vbCrLf & vbCrLf & _Ex.Message, MsgBoxStyle.Critical, "SendKeysGUI - Error") : End Try
	End Sub

	Public Sub GenerateAndSave_VBSFile() Handles GenerateVBSFileLinkLabel.LinkClicked
		Try

			Me.TheToolStripStatusLabel.Text = "Status: Generating VBSFile..." : Me.Refresh()
			Dim _VBSFile_Contents$ = Me.GetVBScriptFileContents_FromCurrentState()

			If Me.VBSFileSaveFileDialog.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
				My.Computer.FileSystem.WriteAllText(Me.VBSFileSaveFileDialog.FileName, _VBSFile_Contents, False, System.Text.Encoding.ASCII)
				If MsgBox("The File was successfully saved to [" & Me.VBSFileSaveFileDialog.FileName & "]." & vbCrLf & vbCrLf & "Click Ok to show the file in Explorer...", MsgBoxStyle.Information, "SendKeys GUI - Export Success") = MsgBoxResult.Ok Then Process.Start("explorer.exe", "/select,""" & Me.VBSFileSaveFileDialog.FileName & """")
			End If

			Me.TheToolStripStatusLabel.Text = "Status: Idle" : Me.Refresh()

		Catch _Ex As Exception : MsgBox("During VBScript File Generation and Saving:" & vbCrLf & vbCrLf & _Ex.Message, MsgBoxStyle.Critical, "SendKeysGUI - Error") : End Try
	End Sub

	Public Function GetVBScriptFileContents_FromCurrentState() As String

		Dim _VBSFileContents$ = _
		 "REM Automatically generated by [SendKeys GUI] @" & DateTime.Now.ToString() & vbCrLf & _
		 vbCrLf & _
		 "'Options, Target, and Setup" & vbCrLf & _
		 "Dim WSShell : Set WSShell = CreateObject(""WScript.Shell"")" & vbCrLf

		REM Delay if instructed to
		If Me.DelayBeforeStartingCheckBox.Checked Then _VBSFileContents &= _
		 "WScript.Sleep(" & Me.DelayBeforeStartingNumericUpDown.Value & ")" & vbCrLf

		REM Set the Foreground window if instructed to
		If Me.Target_ThisWindow_RadioButton.Checked Then

			Dim _TargetWindow_TitleAndHWnd_Split$() = Me.AvaliableWindowsComboBox.SelectedItem.ToString().Split("*"c) 'Untitled - Notepad *123456
			Dim _TargetWindow_JustTitle_WithSpaceOnEnd$ = String.Join("", _TargetWindow_TitleAndHWnd_Split.Take(_TargetWindow_TitleAndHWnd_Split.Length - 1))
			Dim _TargetWindow_JustTitle$ = _TargetWindow_JustTitle_WithSpaceOnEnd.Remove(_TargetWindow_JustTitle_WithSpaceOnEnd.Length - 1)

			_VBSFileContents &= _
			 "WSShell.AppActivate(""" & _TargetWindow_JustTitle & """) :  WScript.Sleep(" & MSToWaitAfterSettingForegroundWindow.ToString() & ")" & vbCrLf

		End If

		REM Call the SendKeys Function; there is no SendWait in VBScript
		_VBSFileContents &= _
		 vbCrLf & _
		 "'Keystrokes" & vbCrLf & _
		 "WSShell.SendKeys """ & Me.KeysToSendTextBox.Text.Replace(vbCrLf, "~").Replace(""""c, "'"c) & """"

		Return _VBSFileContents

	End Function

#Region "Misc. Event Handlers"

	Private Sub ShowExpansionsWindow() Handles ViewExpansionsLinkLabel.LinkClicked
		ExpansionsWindow.Show()
	End Sub

	Private Sub RescanOpenWindows() Handles RescanOpenWindowsButton.Click
		Me.PopulateAvaliableWindowsComboBox()
	End Sub

	Private Sub DelayBeforeStartingCheckBox_CheckedChanged() Handles DelayBeforeStartingCheckBox.CheckedChanged
		Me.DelayBeforeStartingNumericUpDown.Enabled = Me.DelayBeforeStartingCheckBox.Checked
	End Sub

	Private Sub Target_RadioButtons_CheckedChanged() Handles Target_ThisWindow_RadioButton.CheckedChanged, Target_Unspecified_RadioButton.CheckedChanged
		Me.AvaliableWindowsComboBox.Enabled = Me.Target_ThisWindow_RadioButton.Checked
		Me.RescanOpenWindowsButton.Enabled = Me.Target_ThisWindow_RadioButton.Checked
	End Sub

#End Region

#Region "SelectWindowByPosition Logic"

	<Global.System.Runtime.InteropServices.DllImport("User32.dll")> _
	Private Shared Function WindowFromPoint(ByVal p As System.Drawing.Point) As System.IntPtr
	End Function

	Public Sub SelectWindowByPosition() Handles SelectWindowByPositionButton.Click
		Try
			Me.TheToolStripStatusLabel.Text = "Status: Selecting Window by Cursor Position..." : Me.Refresh()

			REM Clicking the CrossHair Selection Tool means that the user must want to Target a specific Window
			Me.Target_ThisWindow_RadioButton.Checked = True

			Me.Enabled = False
			OverlayTimerWindow.ShowNew(3, "Position the Mouse over the Target Window", "Capturing Mouse Position in...")
			Dim _HWnd_OfTargetWindow As System.IntPtr = WindowFromPoint(Cursor.Position)
			Me.TheToolStripStatusLabel.Text = "Status: Selected Target Window *" & _HWnd_OfTargetWindow.ToString() : Me.Refresh()
			Me.Enabled = True
			Me.Activate()

			REM The selected Window may or may not be an Item in the ComboBox; don't bother checking - just remove all items thence, and add the selected HWnd
			Me.AvaliableWindowsComboBox.Items.Clear()
			Me.AvaliableWindowsComboBox.Items.Add("(Cursor-selected Window) *" & _HWnd_OfTargetWindow.ToString()) 'Untitled - Notepad *123456
			Me.AvaliableWindowsComboBox.SelectedIndex = 0

			'REM Attempt to select the same Window from the ComboBox
			'Dim _AvaliableWindows_HWndStrings As New List(Of String)()
			'For Each _AvaliableWindowString$ In Me.AvaliableWindowsComboBox.Items : _AvaliableWindows_HWndStrings.Add(_AvaliableWindowString.Split("*"c)(1)) : Next
			'If Not _AvaliableWindows_HWndStrings.Any(Function(_HWndString$) _HWndString = _HWnd_OfTargetWindow.ToString()) Then Throw New Exception("The cursor-captured Window *" & _HWnd_OfTargetWindow.ToString() & " is not a valid SendKeys Target")

			'REM If we're here, then the ComboBox contains an Entry for the HWnd we just selected
			'Me.AvaliableWindowsComboBox.SelectedIndex = _AvaliableWindows_HWndStrings.TakeWhile(Function(_HWndString$) Not (_HWndString = _HWnd_OfTargetWindow.ToString())).Count()

			Me.TheToolStripStatusLabel.Text = "Status: Idle" : Me.Refresh()
		Catch _Ex As Exception : MsgBox("During Window Selection by Cursor Position:" & vbCrLf & vbCrLf & _Ex.Message, MsgBoxStyle.Critical, "SendKeysGUI - Error") : End Try
	End Sub

#End Region

End Class