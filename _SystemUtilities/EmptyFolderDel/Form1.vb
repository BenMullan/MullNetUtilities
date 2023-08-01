Public Class Form1

	Sub ListDirsMatchingConditions() Handles ListFoldersButton.Click
		Try

			UILoadingMode_TurnOn()
			Me.ClearListBox()
			EFDLogic.AccessDenied_OrOtherError_DuringFolderSearch_Count = 0UI

			Dim _FoundFolders As New List(Of IO.DirectoryInfo)()

			Dim _FolderFinderThread As New Threading.Thread(Sub() _FoundFolders = GetSubFoldersWhere(_Folder:=New IO.DirectoryInfo(Me.SearchDirTextBox.Text), _SubFolderMustBeEmpty:=Me.AreEmptyCheckBox.Checked, _SubFolderNameRegEx:=If(Me.NamesMatchRegExCheckBox.Checked, Me.RegExTextBox.Text, ".*")).ToList())
			_FolderFinderThread.Start() : _FolderFinderThread.Join()

			If _FoundFolders.Count = 0 Then
				MsgBox("No Sub-Folders matching the specified conditions exist inside the specified Parent Directory", MsgBoxStyle.Information)
			Else

				Me.FoundFolders_ListBox.Items.AddRange(( _
				   From _EmptyDir _
				   As IO.DirectoryInfo _
				   In _FoundFolders _
				   Select MakeSureThereIsBackslashOnEnd(_EmptyDir.FullName.Replace(If(Me.SearchDirTextBox.Text.EndsWith("\"), Me.SearchDirTextBox.Text.Substring(0, Me.SearchDirTextBox.Text.Length - 1), Me.SearchDirTextBox.Text), ""))
				).ToArray())

			End If

			Me.FolderSearch_ErrorTextLabel.Text = If(EFDLogic.AccessDenied_OrOtherError_DuringFolderSearch_Count > 0, "Access was denied for " & EFDLogic.AccessDenied_OrOtherError_DuringFolderSearch_Count.ToString() & " Sub-Folder(s)", "")
			ResultsGroupBox.Enabled = True

		Catch _Ex As Exception When True
			MsgBox("Whilst searching for Sub-Folders: " & _Ex.Message, MsgBoxStyle.Critical)
		Finally
			UILoadingMode_TurnOff()
		End Try
	End Sub

	Sub DeleteAllNotSelected() Handles DeleteAllNotSelectedButton.Click
		Try
			UILoadingMode_TurnOn()

			REM Ensure that there are some directories to delete
			'If Not (Me.FoundFolders_ListBox.Items.Count >= 1) Then : MsgBox("There are doch no directories to delete.", MsgBoxStyle.Information) : Exit Sub : End If
			If (Me.FoundFolders_ListBox.Items.Count - Me.FoundFolders_ListBox.SelectedItems.Count) < 1 Then
				MsgBox("There are no unselected Folders to delete." & vbCrLf & "Only unselected (non-blue) Folders will be deleted. Run the [List Folders] operation first.", MsgBoxStyle.Information)
				Exit Sub
			End If

			REM Get a list of the DirectoryInfos to Delete
			Dim _FoldersToDelete As New List(Of IO.DirectoryInfo)()
			For _ItemIndex = 0 To Me.FoundFolders_ListBox.Items.Count - 1 Step +1
				If Not Me.FoundFolders_ListBox.GetSelected(_ItemIndex) Then
					_FoldersToDelete.Add(New IO.DirectoryInfo((MakeSureThereIsBackslashOnEnd(Me.SearchDirTextBox.Text).Substring(0, MakeSureThereIsBackslashOnEnd(Me.SearchDirTextBox.Text).Length - 1) & Me.FoundFolders_ListBox.Items.Item(_ItemIndex).ToString())))
				End If
			Next

			REM Request confirmation
			If Not MessageBox.Show(
			  _FoldersToDelete.Count.ToString() & " Folder(s) will be deleted." & vbCrLf & vbCrLf & _
			  "The first of these is:" & vbCrLf & _FoldersToDelete.First().FullName & vbCrLf & vbCrLf & _
			  "Press OK to continue...", _
			  "Confirm Deletion", MessageBoxButtons.OKCancel, MessageBoxIcon.Question
			 ) = Windows.Forms.DialogResult.OK Then Return

			For Each _FolderToDelete As IO.DirectoryInfo In _FoldersToDelete
				Try
					_FolderToDelete.Delete()
				Catch _Ex As Exception
					Throw New Exception("Error Deleting this Folder: " & _FolderToDelete.FullName & vbCrLf & _Ex.Message)
				End Try
			Next

			ClearListBox() : ListDirsMatchingConditions()

		Catch _Ex As Exception
			MsgBox("The following Exception was thrown upon attempting to Delete all non-selected folders: " & _Ex.Message, MsgBoxStyle.Critical)
		Finally
			UILoadingMode_TurnOff()
		End Try

	End Sub

	Sub UILoadingMode_TurnOn()
		Loading.Show() : Me.Enabled = False : Cursor = Cursors.WaitCursor
	End Sub

	Sub UILoadingMode_TurnOff()
		Loading.Hide() : Me.Enabled = True : Cursor = Cursors.Default : Me.Activate()
	End Sub

	Sub ClearListBox()
		Me.FoundFolders_ListBox.Items.Clear()
	End Sub

	'Show the OpenFileDialog...
	Public Sub BrowseForParentDir() Handles BrowseButton.Click
		If Me.TheFolderBrowserDialog.ShowDialog() = Windows.Forms.DialogResult.OK Then
			Me.SearchDirTextBox.Text = Me.TheFolderBrowserDialog.SelectedPath
			Me.FoundFolders_ListBox.Items.Clear()
		End If
	End Sub

	Sub LoadAppDataDirIntoTextBox() Handles Me.Shown
		Me.SearchDirTextBox.Text = MakeSureThereIsBackslashOnEnd(Global.System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData))
	End Sub

	Sub SearchDirTextBox_TextChanged() Handles SearchDirTextBox.TextChanged
		Me.ClearListBox()
	End Sub

	Private Sub NamesMatchRegExCheckBox_CheckedChanged() Handles NamesMatchRegExCheckBox.CheckedChanged
		Me.RegExTextBox.Enabled = Me.NamesMatchRegExCheckBox.Checked
	End Sub

	Private Sub OpenSelectedButton_Click() Handles ShowSelectedInExplorerButton.Click

		For _ItemIndex = 0 To Me.FoundFolders_ListBox.Items.Count - 1 Step +1
			Try
				If Me.FoundFolders_ListBox.GetSelected(_ItemIndex) Then

					Dim _Folder As New IO.DirectoryInfo((MakeSureThereIsBackslashOnEnd(Me.SearchDirTextBox.Text).Substring(0, MakeSureThereIsBackslashOnEnd(Me.SearchDirTextBox.Text).Length - 1) & Me.FoundFolders_ListBox.Items.Item(_ItemIndex).ToString()))
					Process.Start("explorer.exe", "/select,""" & _Folder.FullName & """")

				End If
			Catch _Ex As Exception
				MsgBox("Error Opening Folder: " & Me.FoundFolders_ListBox.Items.Item(_ItemIndex).ToString() & vbCrLf & _Ex.Message, MsgBoxStyle.Exclamation, _Ex.GetType.FullName)
			End Try
		Next

	End Sub

End Class