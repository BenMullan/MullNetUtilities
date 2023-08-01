REM -------------------------------------
REM Ben Mullan 2019. All Rights Reserved.
REM -------------------------------------

Public Class Account

	Private Sub Account_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
		EMailAccountComboBox.SelectedIndex = 0
		OkButton.Select()
	End Sub

	Public Sub SetCustomAccountLabelText()
		'Resolve the existance of the Custom Account
		If CustomAccount.EMailAddressTextBox.Text = "" Then
			'One isn't set
			CustomAccountLabel.Text = "The Custom Account is currently not set"
		Else
			'One is set
			CustomAccountLabel.Text = "Custom Account: " & CustomAccount.EMailAddressTextBox.Text
		End If

		'Centre the CustomAccountLabel
		CustomAccountLabel.Left = (CustomAccountLabel.Parent.Width \ 2) - (CustomAccountLabel.Width \ 2)
	End Sub

	Private Sub EMailAccountComboBox_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles EMailAccountComboBox.SelectedIndexChanged
		If EMailAccountComboBox.SelectedText = "Custom..." Or EMailAccountComboBox.SelectedIndex = 1 Then
			CustomAccount.ShowDialog()
		End If
	End Sub

	Private Sub ShutDown(Source As Object, e As FormClosingEventArgs) Handles Me.Closing
		e.Cancel = True
	End Sub

	Private Sub OkButton_Click(sender As System.Object, e As System.EventArgs) Handles OkButton.Click

		'Check that [if they have chosen "Custom..." in the ComboBox], then a Custom Account has actually been specified
		If EMailAccountComboBox.SelectedIndex = 1 And CustomAccount.EMailAddressTextBox.Text = "" Then
			'We have a problem; They need to specify a Custom EMail Account, because they have selected that they want to use one
			MsgBox("No Custom Account has been specified." & vbCrLf & "You must either specify one, or use the Default Account.", MsgBoxStyle.Information, "Custom EMail Account")
		Else
			'This form has the final say on which account to use, so it sets the Public Variables now
			If EMailAccountComboBox.SelectedIndex = 0 Then
				'This is the Default Account
				Home.SenderEMailAddress = Home.Default_SenderEMailAddress
				Home.SenderEMailPassword = Home.Default_SenderEMailPassword
				Home.SMTPServer = Home.Default_SMTPServer
				Home.SMTPServerUsesSSL = Home.Default_SMTPServerUsesSSL
				Home.SMTPServerPort = Home.Default_SMTPServerPort
			Else
				'This is the Custom Account
				Home.SenderEMailAddress = CustomAccount.EMailAddressTextBox.Text
				Home.SenderEMailPassword = CustomAccount.PasswordTextBox.Text
				Home.SMTPServer = CustomAccount.SMTPServerTextBox.Text
				Home.SMTPServerUsesSSL = CustomAccount.UsesSSLCheckBox.Checked
				Home.SMTPServerPort = CInt(CustomAccount.PortTextBox.Text)
			End If

			'Close this Dialog
			Me.Hide()

		End If

	End Sub
End Class