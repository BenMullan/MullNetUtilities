Public Class SampleQueriesWindow

	Public Shared ReadOnly SQLSampleQueries$() = {
  "SELECT ""Name"", ""Database_ID"", ""Create_Date"" FROM [Sys].[Databases];"
 }

	Public TargetMainWindow As SQLMaster.MainWindow

	Public Sub AddQueriesTListBox() Handles Me.Shown
		Me.SampleQueriesListBox.Items.AddRange(SampleQueriesWindow.SQLSampleQueries)
	End Sub

	Public Sub New(ByRef _SQLMasterWindow_IntoWhoseQueryTextBox_TheQueryShouldBeInserted As SQLMaster.MainWindow)
		InitializeComponent()
		Me.TargetMainWindow = _SQLMasterWindow_IntoWhoseQueryTextBox_TheQueryShouldBeInserted
	End Sub

	Public Sub InsertSelected() Handles InsertSelected_Button.Click
		If Me.SampleQueriesListBox.SelectedItems.Count = 0 Then MsgBox("A Query must firstly be selected from the ListBox", MsgBoxStyle.Critical) Else Me.TargetMainWindow.SQLQuery_TextBox.Text = Me.SampleQueriesListBox.SelectedItem.ToString()
		Me.DialogResult = Windows.Forms.DialogResult.OK
		Me.Close()
	End Sub

End Class