Public Class MainWindow

	Protected Cached_AvaliableDatabases_$() = {}

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
		ExecuteSQLQuery()
		Me.TheStatusLabel.Text = "Listed avaliable Databases"

		Try

			Cached_AvaliableDatabases_ = _
			   Me.TheOutputDataGridView.Rows.Cast(Of DataGridViewRow)() _
			   .Select(Of [String])(Function(_Row As DataGridViewRow) If(_Row.Cells.Item(0).Value IsNot Nothing, _Row.Cells.Item(0).Value.ToString(), [String].Empty)) _
			   .Where(Function(_DBName$) Not [String].IsNullOrWhiteSpace(_DBName)) _
			   .ToArray()

		Catch _Ex As Exception
			MsgBox("The Database names could not be cached for the Table-listing function, because " & _Ex.Message, MsgBoxStyle.Exclamation)
		End Try

	End Sub

	Private Sub ListAllTablesInDatabaseToolStripMenuItem_Click_() Handles ListAllTablesInDatabaseToolStripMenuItem.Click

		Dim _ChooseDatabaseDialog As New MullNet.MetaUtilities.Dialogs.ChooseFromListDialog(Of [String])(Cached_AvaliableDatabases_.Concat({"(Manually type Table Name)"}).ToArray(), "Database")
		If Not ((_ChooseDatabaseDialog.ShowDialog() = (Windows.Forms.[DialogResult]).OK) AndAlso (_ChooseDatabaseDialog.ChosenItem IsNot Nothing)) Then Return

		Me.SQLQuery_TextBox.Text = "SELECT ""TABLE_NAME"" FROM [" & _ChooseDatabaseDialog.ChosenItem & "].[INFORMATION_SCHEMA].[TABLES] WHERE ""TABLE_TYPE"" = 'BASE TABLE';"
		ExecuteSQLQuery()
		Me.TheStatusLabel.Text = "Listed Tables in " & _ChooseDatabaseDialog.ChosenItem

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