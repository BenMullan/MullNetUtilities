<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
		Me.ListFoldersButton = New System.Windows.Forms.Button()
		Me.SearchDirTextBox = New System.Windows.Forms.TextBox()
		Me.FoundFolders_ListBox = New System.Windows.Forms.ListBox()
		Me.DeleteAllNotSelectedButton = New System.Windows.Forms.Button()
		Me.BrowseButton = New System.Windows.Forms.Button()
		Me.TheFolderBrowserDialog = New System.Windows.Forms.FolderBrowserDialog()
		Me.Label1 = New System.Windows.Forms.Label()
		Me.Label2 = New System.Windows.Forms.Label()
		Me.AreEmptyCheckBox = New System.Windows.Forms.CheckBox()
		Me.Label3 = New System.Windows.Forms.Label()
		Me.SearchGroupBox = New System.Windows.Forms.GroupBox()
		Me.RegExTextBox = New System.Windows.Forms.TextBox()
		Me.NamesMatchRegExCheckBox = New System.Windows.Forms.CheckBox()
		Me.ResultsGroupBox = New System.Windows.Forms.GroupBox()
		Me.ShowSelectedInExplorerButton = New System.Windows.Forms.LinkLabel()
		Me.FolderSearch_ErrorTextLabel = New System.Windows.Forms.Label()
		Me.Label4 = New System.Windows.Forms.Label()
		Me.SearchGroupBox.SuspendLayout()
		Me.ResultsGroupBox.SuspendLayout()
		Me.SuspendLayout()
		'
		'ListFoldersButton
		'
		Me.ListFoldersButton.Location = New System.Drawing.Point(239, 118)
		Me.ListFoldersButton.Name = "ListFoldersButton"
		Me.ListFoldersButton.Size = New System.Drawing.Size(83, 23)
		Me.ListFoldersButton.TabIndex = 0
		Me.ListFoldersButton.Text = "&List Folders"
		Me.ListFoldersButton.UseVisualStyleBackColor = True
		'
		'SearchDirTextBox
		'
		Me.SearchDirTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
				  Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.SearchDirTextBox.Location = New System.Drawing.Point(76, 19)
		Me.SearchDirTextBox.Name = "SearchDirTextBox"
		Me.SearchDirTextBox.Size = New System.Drawing.Size(445, 20)
		Me.SearchDirTextBox.TabIndex = 1
		Me.SearchDirTextBox.Text = "{No Dir Chosen}"
		'
		'FoundFolders_ListBox
		'
		Me.FoundFolders_ListBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
				  Or System.Windows.Forms.AnchorStyles.Left) _
				  Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.FoundFolders_ListBox.FormattingEnabled = True
		Me.FoundFolders_ListBox.HorizontalScrollbar = True
		Me.FoundFolders_ListBox.Location = New System.Drawing.Point(6, 41)
		Me.FoundFolders_ListBox.Name = "FoundFolders_ListBox"
		Me.FoundFolders_ListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
		Me.FoundFolders_ListBox.Size = New System.Drawing.Size(546, 303)
		Me.FoundFolders_ListBox.TabIndex = 2
		'
		'DeleteAllNotSelectedButton
		'
		Me.DeleteAllNotSelectedButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.DeleteAllNotSelectedButton.Location = New System.Drawing.Point(448, 347)
		Me.DeleteAllNotSelectedButton.Name = "DeleteAllNotSelectedButton"
		Me.DeleteAllNotSelectedButton.Size = New System.Drawing.Size(104, 23)
		Me.DeleteAllNotSelectedButton.TabIndex = 4
		Me.DeleteAllNotSelectedButton.Text = "&Delete Unselected"
		Me.DeleteAllNotSelectedButton.UseVisualStyleBackColor = True
		'
		'BrowseButton
		'
		Me.BrowseButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.BrowseButton.BackgroundImage = Global.EmptyFolderDel.My.Resources.Resources.folder_yellow
		Me.BrowseButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
		Me.BrowseButton.FlatAppearance.BorderColor = System.Drawing.Color.Gray
		Me.BrowseButton.FlatAppearance.BorderSize = 2
		Me.BrowseButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup
		Me.BrowseButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.BrowseButton.Location = New System.Drawing.Point(527, 19)
		Me.BrowseButton.Name = "BrowseButton"
		Me.BrowseButton.Size = New System.Drawing.Size(27, 20)
		Me.BrowseButton.TabIndex = 5
		Me.BrowseButton.UseVisualStyleBackColor = True
		'
		'TheFolderBrowserDialog
		'
		Me.TheFolderBrowserDialog.Description = "Select a Folder to search for Empty Directories in..."
		Me.TheFolderBrowserDialog.RootFolder = System.Environment.SpecialFolder.MyComputer
		Me.TheFolderBrowserDialog.ShowNewFolderButton = False
		'
		'Label1
		'
		Me.Label1.AutoSize = True
		Me.Label1.Location = New System.Drawing.Point(15, 22)
		Me.Label1.Name = "Label1"
		Me.Label1.Size = New System.Drawing.Size(60, 13)
		Me.Label1.TabIndex = 7
		Me.Label1.Text = "Search Dir:"
		'
		'Label2
		'
		Me.Label2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
				  Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.Label2.AutoSize = True
		Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label2.Location = New System.Drawing.Point(6, 23)
		Me.Label2.Name = "Label2"
		Me.Label2.Size = New System.Drawing.Size(164, 13)
		Me.Label2.TabIndex = 8
		Me.Label2.Text = "Select all folders to be kept"
		'
		'AreEmptyCheckBox
		'
		Me.AreEmptyCheckBox.AutoSize = True
		Me.AreEmptyCheckBox.Checked = True
		Me.AreEmptyCheckBox.CheckState = System.Windows.Forms.CheckState.Checked
		Me.AreEmptyCheckBox.Location = New System.Drawing.Point(28, 65)
		Me.AreEmptyCheckBox.Name = "AreEmptyCheckBox"
		Me.AreEmptyCheckBox.Size = New System.Drawing.Size(81, 17)
		Me.AreEmptyCheckBox.TabIndex = 9
		Me.AreEmptyCheckBox.Text = "...are &empty"
		Me.AreEmptyCheckBox.UseVisualStyleBackColor = True
		'
		'Label3
		'
		Me.Label3.AutoSize = True
		Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label3.Location = New System.Drawing.Point(13, 49)
		Me.Label3.Name = "Label3"
		Me.Label3.Size = New System.Drawing.Size(138, 13)
		Me.Label3.TabIndex = 8
		Me.Label3.Text = "List all Folders which..."
		'
		'SearchGroupBox
		'
		Me.SearchGroupBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
				  Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.SearchGroupBox.Controls.Add(Me.RegExTextBox)
		Me.SearchGroupBox.Controls.Add(Me.ListFoldersButton)
		Me.SearchGroupBox.Controls.Add(Me.BrowseButton)
		Me.SearchGroupBox.Controls.Add(Me.Label3)
		Me.SearchGroupBox.Controls.Add(Me.NamesMatchRegExCheckBox)
		Me.SearchGroupBox.Controls.Add(Me.AreEmptyCheckBox)
		Me.SearchGroupBox.Controls.Add(Me.SearchDirTextBox)
		Me.SearchGroupBox.Controls.Add(Me.Label1)
		Me.SearchGroupBox.Location = New System.Drawing.Point(12, 12)
		Me.SearchGroupBox.Name = "SearchGroupBox"
		Me.SearchGroupBox.Size = New System.Drawing.Size(560, 156)
		Me.SearchGroupBox.TabIndex = 10
		Me.SearchGroupBox.TabStop = False
		Me.SearchGroupBox.Text = "Search"
		'
		'RegExTextBox
		'
		Me.RegExTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
				  Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.RegExTextBox.Enabled = False
		Me.RegExTextBox.Location = New System.Drawing.Point(220, 80)
		Me.RegExTextBox.Name = "RegExTextBox"
		Me.RegExTextBox.Size = New System.Drawing.Size(219, 20)
		Me.RegExTextBox.TabIndex = 11
		Me.RegExTextBox.Text = "^\w+\.tmp$"
		'
		'NamesMatchRegExCheckBox
		'
		Me.NamesMatchRegExCheckBox.AutoSize = True
		Me.NamesMatchRegExCheckBox.Location = New System.Drawing.Point(28, 82)
		Me.NamesMatchRegExCheckBox.Name = "NamesMatchRegExCheckBox"
		Me.NamesMatchRegExCheckBox.Size = New System.Drawing.Size(196, 17)
		Me.NamesMatchRegExCheckBox.TabIndex = 9
		Me.NamesMatchRegExCheckBox.Text = "...have names matching this &RegEx:"
		Me.NamesMatchRegExCheckBox.UseVisualStyleBackColor = True
		'
		'ResultsGroupBox
		'
		Me.ResultsGroupBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
				  Or System.Windows.Forms.AnchorStyles.Left) _
				  Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.ResultsGroupBox.Controls.Add(Me.ShowSelectedInExplorerButton)
		Me.ResultsGroupBox.Controls.Add(Me.FolderSearch_ErrorTextLabel)
		Me.ResultsGroupBox.Controls.Add(Me.FoundFolders_ListBox)
		Me.ResultsGroupBox.Controls.Add(Me.DeleteAllNotSelectedButton)
		Me.ResultsGroupBox.Controls.Add(Me.Label4)
		Me.ResultsGroupBox.Controls.Add(Me.Label2)
		Me.ResultsGroupBox.Enabled = False
		Me.ResultsGroupBox.Location = New System.Drawing.Point(12, 174)
		Me.ResultsGroupBox.Name = "ResultsGroupBox"
		Me.ResultsGroupBox.Size = New System.Drawing.Size(560, 376)
		Me.ResultsGroupBox.TabIndex = 11
		Me.ResultsGroupBox.TabStop = False
		Me.ResultsGroupBox.Text = "Results"
		'
		'ShowSelectedInExplorerButton
		'
		Me.ShowSelectedInExplorerButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.ShowSelectedInExplorerButton.AutoSize = True
		Me.ShowSelectedInExplorerButton.Location = New System.Drawing.Point(413, 23)
		Me.ShowSelectedInExplorerButton.Name = "ShowSelectedInExplorerButton"
		Me.ShowSelectedInExplorerButton.Size = New System.Drawing.Size(140, 13)
		Me.ShowSelectedInExplorerButton.TabIndex = 11
		Me.ShowSelectedInExplorerButton.TabStop = True
		Me.ShowSelectedInExplorerButton.Text = "Show selected, in explorer..."
		'
		'FolderSearch_ErrorTextLabel
		'
		Me.FolderSearch_ErrorTextLabel.AutoSize = True
		Me.FolderSearch_ErrorTextLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.FolderSearch_ErrorTextLabel.ForeColor = System.Drawing.Color.Firebrick
		Me.FolderSearch_ErrorTextLabel.Location = New System.Drawing.Point(8, 352)
		Me.FolderSearch_ErrorTextLabel.Name = "FolderSearch_ErrorTextLabel"
		Me.FolderSearch_ErrorTextLabel.Size = New System.Drawing.Size(0, 13)
		Me.FolderSearch_ErrorTextLabel.TabIndex = 9
		'
		'Label4
		'
		Me.Label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.Label4.AutoSize = True
		Me.Label4.Location = New System.Drawing.Point(202, 23)
		Me.Label4.Name = "Label4"
		Me.Label4.Size = New System.Drawing.Size(169, 13)
		Me.Label4.TabIndex = 8
		Me.Label4.Text = "(Use Ctrl , Shift, Home, End, etc...)"
		'
		'Form1
		'
		Me.AcceptButton = Me.ListFoldersButton
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(584, 562)
		Me.Controls.Add(Me.ResultsGroupBox)
		Me.Controls.Add(Me.SearchGroupBox)
		Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
		Me.MaximizeBox = False
		Me.MinimizeBox = False
		Me.MinimumSize = New System.Drawing.Size(400, 400)
		Me.Name = "Form1"
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
		Me.Text = "EmptyFolderDel (v2)"
		Me.SearchGroupBox.ResumeLayout(False)
		Me.SearchGroupBox.PerformLayout()
		Me.ResultsGroupBox.ResumeLayout(False)
		Me.ResultsGroupBox.PerformLayout()
		Me.ResumeLayout(False)

	End Sub
	Friend WithEvents ListFoldersButton As System.Windows.Forms.Button
	Friend WithEvents SearchDirTextBox As System.Windows.Forms.TextBox
	Friend WithEvents FoundFolders_ListBox As System.Windows.Forms.ListBox
	Friend WithEvents DeleteAllNotSelectedButton As System.Windows.Forms.Button
	Friend WithEvents BrowseButton As System.Windows.Forms.Button
	Public WithEvents TheFolderBrowserDialog As System.Windows.Forms.FolderBrowserDialog
	Friend WithEvents Label1 As System.Windows.Forms.Label
	Friend WithEvents Label2 As System.Windows.Forms.Label
	Friend WithEvents AreEmptyCheckBox As System.Windows.Forms.CheckBox
	Friend WithEvents Label3 As System.Windows.Forms.Label
	Friend WithEvents SearchGroupBox As System.Windows.Forms.GroupBox
	Friend WithEvents NamesMatchRegExCheckBox As System.Windows.Forms.CheckBox
	Friend WithEvents RegExTextBox As System.Windows.Forms.TextBox
	Friend WithEvents ResultsGroupBox As System.Windows.Forms.GroupBox
	Friend WithEvents FolderSearch_ErrorTextLabel As System.Windows.Forms.Label
	Friend WithEvents ShowSelectedInExplorerButton As System.Windows.Forms.LinkLabel
	Friend WithEvents Label4 As System.Windows.Forms.Label

End Class
