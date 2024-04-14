Public Class ConnectToServerWindow

	REM Me.AuthenticationMode_ComboBox
	REM		.SelectedIndex = 0		WINDOWS AUTHENTICATION
	REM		.SelectedIndex = 1		SQL SERVER AUTHENTICATION

	''' <summary>Returns Nothing, if there is no Server Name specified</summary>
	Protected ReadOnly Property SpecifiedSQLServerConnection_ As MetaUtilities.SQLQueryRunner
		Get
			If String.IsNullOrEmpty(Me.ServerNameAndInstance_TextBox.Text) Then Return Nothing
			If (Me.AuthenticationMode_ComboBox.SelectedIndex = 0) Then Return New MetaUtilities.SQLQueryRunner(_SQLServerName:=Me.ServerNameAndInstance_TextBox.Text)
			If (Me.AuthenticationMode_ComboBox.SelectedIndex = 1) Then Return New MetaUtilities.SQLQueryRunner(_SQLServerName:=Me.ServerNameAndInstance_TextBox.Text, _SQLServerUserName:=Me.AuthUsername_TextBox.Text, _SQLServerUserPassword:=Me.AuthPassword_TextBox.Text)
			Throw New Exception("The Authentication Mode of the ComboBox was un-accounted-for.")
		End Get
	End Property

	''' <summary>Returns Nothing, if [Esc] is pressed, or the form is closed with [x] instead of [Ok]</summary>
	Public Shared Function RequestSQLServerConnectionDetails() As MullNet.MetaUtilities.SQLQueryRunner

		REM Show a New ConnectToServerWindow()
		Dim _ConnectToServerWindow As New ConnectToServerWindow()
		_ConnectToServerWindow.ShowDialog()

		Return _ConnectToServerWindow.SpecifiedSQLServerConnection_

	End Function

	Public Sub WindowShown() Handles Me.Shown
		Try
			Me.AuthenticationMode_ComboBox.SelectedIndex = 0
			Me.AuthUsername_TextBox.Text = System.Security.Principal.WindowsIdentity.GetCurrent().Name
			Me.ServerNameAndInstance_TextBox.Text = MetaUtilities.SQLQueryRunner.LocalSQLServerInstances(_ReplaceMSSQLSERVERInstances_WithHostnameOnly:=True).FirstOrDefault()
		Catch _Ex As Exception : MsgBox(_Ex.Message, MsgBoxStyle.Critical) : End Try
	End Sub

	REM ↓ This also occurs when the Form is first shown
	Protected Sub UpdateUsernameAndPasswordTextboxesReadOnlyStatus_() Handles AuthenticationMode_ComboBox.SelectedIndexChanged
		Call {Me.AuthUsername_TextBox, Me.AuthPassword_TextBox}.ToList().ForEach(Sub(_Textbox As TextBox) _Textbox.Enabled = Not (Me.AuthenticationMode_ComboBox.SelectedIndex = 0))
	End Sub

	Private Sub OkButton_Click() Handles OkButton.Click
		Try

			Dim _StringsWhichMustBeNotNothing As New List(Of [String])() From {Me.ServerNameAndInstance_TextBox.Text}
			If Me.AuthenticationMode_ComboBox.SelectedIndex = 1 Then _StringsWhichMustBeNotNothing.AddRange({Me.AuthUsername_TextBox.Text, Me.AuthPassword_TextBox.Text})

			REM Permit the Form to be closed, if the required fields have been populated
			If _StringsWhichMustBeNotNothing.Any(AddressOf System.[String].IsNullOrEmpty) Then
				MsgBox("At least one required field is unpopulated.", MsgBoxStyle.Critical)
			Else
				Me.DialogResult = Windows.Forms.DialogResult.OK
				Me.Close()
			End If

		Catch _Ex As Exception : MsgBox(_Ex.Message, MsgBoxStyle.Critical) : End Try
	End Sub

	Public Sub ShowLocalInstances_InMsgBox() Handles ListLocalInstances_LinkLabel.LinkClicked
		Try
			MsgBox("The SQL Server Instances avaliable on the local machine are:" & vbCrLf & vbCrLf & [String].Join(vbCrLf, MetaUtilities.SQLQueryRunner.LocalSQLServerInstances), MsgBoxStyle.Information)
		Catch _Ex As Exception : MsgBox(_Ex.Message, MsgBoxStyle.Critical) : End Try
	End Sub

#Region "Close the form on pressing [Esc]"

	Protected Sub Register_CloseOnEscKey_Handler() Handles Me.Load
		Me.KeyPreview = True
		AddHandler Me.KeyDown, Sub(_Sender As [Object], _KeyEventArgs As Windows.Forms.KeyEventArgs) If _KeyEventArgs.KeyCode = Windows.Forms.Keys.Escape Then Me.Close()
	End Sub

	Protected Overrides Function ProcessDialogKey(keyData As System.Windows.Forms.Keys) As [Boolean]
		If (Windows.Forms.Form.ModifierKeys = Windows.Forms.Keys.None) AndAlso (keyData = Windows.Forms.Keys.Escape) Then
			Me.Close() : Return True
		End If
		Return MyBase.ProcessDialogKey(keyData)
	End Function

#End Region

End Class