REM -------------------------------------
REM Ben Mullan 2019. All Rights Reserved.
REM -------------------------------------

Public Class CustomAccount

	Private Sub CustomAccount_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
		EMailAddressTextBox.Select()
	End Sub

	Private Sub ShutDown(Source As Object, e As FormClosingEventArgs) Handles Me.Closing
		e.Cancel = False
	End Sub

	Private Sub PortTextBox_TextChanged(sender As Object, e As System.EventArgs) Handles PortTextBox.TextChanged
		Dim allowed As String = ("0123456789")
		For Each c As Char In PortTextBox.Text
			If allowed.Contains(c) = False Then
				PortTextBox.Text = PortTextBox.Text.Remove(PortTextBox.SelectionStart - 1, 1)
				PortTextBox.Select(PortTextBox.Text.Count, 0)
				Beep()
			End If
		Next
	End Sub

	Private Sub SMTPServerTextBox_TextChanged(sender As System.Object, e As System.EventArgs) Handles SMTPServerTextBox.TextChanged
		Dim allowed As String = ("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789.-")
		For Each c As Char In PortTextBox.Text
			If allowed.Contains(c) = False Then
				PortTextBox.Text = PortTextBox.Text.Remove(PortTextBox.SelectionStart - 1, 1)
				PortTextBox.Select(PortTextBox.Text.Count, 0)
				Beep()
			End If
		Next
	End Sub

	Private Sub EMailAddressTextBox_Leave(sender As Object, e As System.EventArgs) Handles EMailAddressTextBox.Leave ', EMailAddressTextBox.LostFocus
		If Not Home.IsValidEMailAddress(EMailAddressTextBox.Text) Then
			MsgBox("Please specify a valid EMail Address", MsgBoxStyle.Information, "AutoEMailSend")
			EMailAddressTextBox.Text = ""
			EMailAddressTextBox.Select()
		End If
	End Sub

	Private Sub OkButton_Click(sender As System.Object, e As System.EventArgs) Handles OkButton.Click

		'Check that all the feilds have been filled out
		If EMailAddressTextBox.Text = "" Or SMTPServerTextBox.Text = "" Or PortTextBox.Text = "" Then
			MsgBox("Please specify an EMail Address, SMTP Server, and Port", MsgBoxStyle.Information, "Custom Account")
		Else
			'Set the Text of the Custom Selection (Label) on the main Account Form
			Account.SetCustomAccountLabelText()

			'Close this Dialog
			Me.Hide()
		End If

	End Sub
End Class