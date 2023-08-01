Public Class MainWindow

	Protected Property CurrentSQLConnection_ As MullNet.MetaUtilities.SQLQueryRunner = Nothing
	Public Property CurrentSQLConnection As MullNet.MetaUtilities.SQLQueryRunner
		Get
			Return Me.CurrentSQLConnection_
		End Get
		Set(_NewValue As MullNet.MetaUtilities.SQLQueryRunner)
			Me.CurrentSQLConnection_ = _NewValue
			Me.Text = "SQL Master - " & If(_NewValue Is Nothing, "Not Connected", "Connected to " & Me.CurrentSQLConnection_.SQLServerName)
		End Set
	End Property

	Public Sub ConnectToServer() Handles ConnectToServerToolStripMenuItem.Click, Me.Shown
		Try
			Me.CurrentSQLConnection = MullNet.SystemUtilities.SQLMaster.ConnectToServerWindow.RequestSQLServerConnectionDetails()
		Catch _Ex As Exception : MsgBox(_Ex.Message, MsgBoxStyle.Critical) : End Try
	End Sub

	Public Sub ExecuteSQLQuery() Handles ExecuteSQLQuery_Button.Click
		Try

			If Me.CurrentSQLConnection Is Nothing Then Throw New NullReferenceException("There is no current SQL Server Connection")

			Me.EngageUILoadingMode_("Executing SQL Query...")
			Me.CurrentSQLConnection.ExecuteQuery(Me.SQLQuery_TextBox.Text)
			Me.TheOutputDataGridView.DataSource = Me.CurrentSQLConnection.QueryOutputDataTable

		Catch _Ex As Exception : MsgBox(_Ex.Message, MsgBoxStyle.Critical) : Finally : Me.DisengageUILoadingMode_() : End Try
	End Sub

#Region "UI Utilities"

	Protected Sub EngageUILoadingMode_(ByVal _StatusText$)
		Me.Enabled = False
		Me.TheStatusLabel.Text = "Status: " & _StatusText
		Cursor.Current = Cursors.WaitCursor
		Me.Refresh()
	End Sub

	Protected Sub DisengageUILoadingMode_()
		Me.Enabled = True
		Me.TheStatusLabel.Text = "Status: Idle"
		Cursor.Current = Cursors.Default
		Me.Refresh()
	End Sub

	Private Sub SQLQuery_TextBox_GotFocus_() Handles SQLQuery_TextBox.GotFocus
		Me.SQLQuery_TextBox.SelectAll()
	End Sub

#End Region

	Private Sub ListDatabasesOnServerToolStripMenuItem_Click_() Handles ListDatabasesOnServerToolStripMenuItem.Click
		Me.SQLQuery_TextBox.Text = "SELECT ""Name"", ""Database_ID"" FROM [Sys].[Databases];"
		Me.TheStatusLabel.Text = "Do Now: Execute the SQL Query to fetch avaliable Databases..."
	End Sub

	Private Sub ListAllTablesInDatabaseToolStripMenuItem_Click_() Handles ListAllTablesInDatabaseToolStripMenuItem.Click
		Me.SQLQuery_TextBox.Text = "SELECT ""TABLE_NAME"" FROM [DatabaseName].[INFORMATION_SCHEMA].[TABLES] WHERE ""TABLE_TYPE"" = 'BASE TABLE';"
		Me.TheStatusLabel.Text = "Do Now: Replace ""DatabaseName"", then execute the SQL Query..."
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