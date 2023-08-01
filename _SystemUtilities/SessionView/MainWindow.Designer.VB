<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MainWindow
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
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainWindow))
		Me.ScreenshotPictureBox = New System.Windows.Forms.PictureBox()
		Me.GroupBox1 = New System.Windows.Forms.GroupBox()
		Me.OpenWorkingDirectoryLinkLabel = New System.Windows.Forms.LinkLabel()
		Me.BrowseForWorkingDirectoryButton = New System.Windows.Forms.Button()
		Me.TargetHostnameTextBox = New System.Windows.Forms.TextBox()
		Me.WorkingDirectoryTextBox = New System.Windows.Forms.TextBox()
		Me.AvaliableSessionsComboBox = New System.Windows.Forms.ComboBox()
		Me.Label4 = New System.Windows.Forms.Label()
		Me.Label2 = New System.Windows.Forms.Label()
		Me.GroupBox2 = New System.Windows.Forms.GroupBox()
		Me.TakeScreenshotButton = New System.Windows.Forms.Button()
		Me.Label1 = New System.Windows.Forms.Label()
		Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
		Me.ToolStripStatusLabel1 = New System.Windows.Forms.ToolStripStatusLabel()
		CType(Me.ScreenshotPictureBox, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.GroupBox1.SuspendLayout()
		Me.GroupBox2.SuspendLayout()
		Me.StatusStrip1.SuspendLayout()
		Me.SuspendLayout()
		'
		'ScreenshotPictureBox
		'
		Me.ScreenshotPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		Me.ScreenshotPictureBox.Location = New System.Drawing.Point(6, 102)
		Me.ScreenshotPictureBox.Name = "ScreenshotPictureBox"
		Me.ScreenshotPictureBox.Size = New System.Drawing.Size(515, 281)
		Me.ScreenshotPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
		Me.ScreenshotPictureBox.TabIndex = 0
		Me.ScreenshotPictureBox.TabStop = False
		'
		'GroupBox1
		'
		Me.GroupBox1.Controls.Add(Me.OpenWorkingDirectoryLinkLabel)
		Me.GroupBox1.Controls.Add(Me.BrowseForWorkingDirectoryButton)
		Me.GroupBox1.Controls.Add(Me.TargetHostnameTextBox)
		Me.GroupBox1.Controls.Add(Me.WorkingDirectoryTextBox)
		Me.GroupBox1.Controls.Add(Me.AvaliableSessionsComboBox)
		Me.GroupBox1.Controls.Add(Me.Label4)
		Me.GroupBox1.Controls.Add(Me.Label2)
		Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
		Me.GroupBox1.Name = "GroupBox1"
		Me.GroupBox1.Size = New System.Drawing.Size(527, 100)
		Me.GroupBox1.TabIndex = 1
		Me.GroupBox1.TabStop = False
		Me.GroupBox1.Text = "Control"
		'
		'OpenWorkingDirectoryLinkLabel
		'
		Me.OpenWorkingDirectoryLinkLabel.AutoSize = True
		Me.OpenWorkingDirectoryLinkLabel.Location = New System.Drawing.Point(12, 57)
		Me.OpenWorkingDirectoryLinkLabel.Name = "OpenWorkingDirectoryLinkLabel"
		Me.OpenWorkingDirectoryLinkLabel.Size = New System.Drawing.Size(95, 13)
		Me.OpenWorkingDirectoryLinkLabel.TabIndex = 5
		Me.OpenWorkingDirectoryLinkLabel.TabStop = True
		Me.OpenWorkingDirectoryLinkLabel.Text = "Working Directory:"
		'
		'BrowseForWorkingDirectoryButton
		'
		Me.BrowseForWorkingDirectoryButton.Location = New System.Drawing.Point(491, 53)
		Me.BrowseForWorkingDirectoryButton.Name = "BrowseForWorkingDirectoryButton"
		Me.BrowseForWorkingDirectoryButton.Size = New System.Drawing.Size(30, 20)
		Me.BrowseForWorkingDirectoryButton.TabIndex = 4
		Me.BrowseForWorkingDirectoryButton.Text = "..."
		Me.BrowseForWorkingDirectoryButton.UseVisualStyleBackColor = True
		'
		'TargetHostnameTextBox
		'
		Me.TargetHostnameTextBox.Location = New System.Drawing.Point(312, 26)
		Me.TargetHostnameTextBox.Name = "TargetHostnameTextBox"
		Me.TargetHostnameTextBox.Size = New System.Drawing.Size(173, 20)
		Me.TargetHostnameTextBox.TabIndex = 3
		'
		'WorkingDirectoryTextBox
		'
		Me.WorkingDirectoryTextBox.Location = New System.Drawing.Point(113, 53)
		Me.WorkingDirectoryTextBox.Name = "WorkingDirectoryTextBox"
		Me.WorkingDirectoryTextBox.Size = New System.Drawing.Size(372, 20)
		Me.WorkingDirectoryTextBox.TabIndex = 3
		'
		'AvaliableSessionsComboBox
		'
		Me.AvaliableSessionsComboBox.FormattingEnabled = True
		Me.AvaliableSessionsComboBox.Location = New System.Drawing.Point(113, 26)
		Me.AvaliableSessionsComboBox.Name = "AvaliableSessionsComboBox"
		Me.AvaliableSessionsComboBox.Size = New System.Drawing.Size(168, 21)
		Me.AvaliableSessionsComboBox.TabIndex = 1
		'
		'Label4
		'
		Me.Label4.AutoSize = True
		Me.Label4.Location = New System.Drawing.Point(287, 29)
		Me.Label4.Name = "Label4"
		Me.Label4.Size = New System.Drawing.Size(19, 13)
		Me.Label4.TabIndex = 0
		Me.Label4.Text = "on"
		'
		'Label2
		'
		Me.Label2.AutoSize = True
		Me.Label2.Location = New System.Drawing.Point(12, 29)
		Me.Label2.Name = "Label2"
		Me.Label2.Size = New System.Drawing.Size(81, 13)
		Me.Label2.TabIndex = 0
		Me.Label2.Text = "Target Session:"
		'
		'GroupBox2
		'
		Me.GroupBox2.Controls.Add(Me.TakeScreenshotButton)
		Me.GroupBox2.Controls.Add(Me.Label1)
		Me.GroupBox2.Controls.Add(Me.ScreenshotPictureBox)
		Me.GroupBox2.Location = New System.Drawing.Point(12, 118)
		Me.GroupBox2.Name = "GroupBox2"
		Me.GroupBox2.Size = New System.Drawing.Size(527, 389)
		Me.GroupBox2.TabIndex = 1
		Me.GroupBox2.TabStop = False
		Me.GroupBox2.Text = "Target Session"
		'
		'TakeScreenshotButton
		'
		Me.TakeScreenshotButton.Location = New System.Drawing.Point(377, 73)
		Me.TakeScreenshotButton.Name = "TakeScreenshotButton"
		Me.TakeScreenshotButton.Size = New System.Drawing.Size(144, 23)
		Me.TakeScreenshotButton.TabIndex = 2
		Me.TakeScreenshotButton.Text = "Take Screenshot"
		Me.TakeScreenshotButton.UseVisualStyleBackColor = True
		'
		'Label1
		'
		Me.Label1.AutoSize = True
		Me.Label1.Location = New System.Drawing.Point(12, 78)
		Me.Label1.Name = "Label1"
		Me.Label1.Size = New System.Drawing.Size(235, 13)
		Me.Label1.TabIndex = 1
		Me.Label1.Text = "Most-recent Screenshot: (No screenshot Taken)"
		'
		'StatusStrip1
		'
		Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatusLabel1})
		Me.StatusStrip1.Location = New System.Drawing.Point(0, 523)
		Me.StatusStrip1.Name = "StatusStrip1"
		Me.StatusStrip1.Size = New System.Drawing.Size(553, 22)
		Me.StatusStrip1.TabIndex = 2
		Me.StatusStrip1.Text = "StatusStrip1"
		'
		'ToolStripStatusLabel1
		'
		Me.ToolStripStatusLabel1.Name = "ToolStripStatusLabel1"
		Me.ToolStripStatusLabel1.Size = New System.Drawing.Size(64, 17)
		Me.ToolStripStatusLabel1.Text = "Status: Idle"
		'
		'MainWindow
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(553, 545)
		Me.Controls.Add(Me.StatusStrip1)
		Me.Controls.Add(Me.GroupBox2)
		Me.Controls.Add(Me.GroupBox1)
		Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
		Me.Name = "MainWindow"
		Me.Text = "SessionView"
		CType(Me.ScreenshotPictureBox, System.ComponentModel.ISupportInitialize).EndInit()
		Me.GroupBox1.ResumeLayout(False)
		Me.GroupBox1.PerformLayout()
		Me.GroupBox2.ResumeLayout(False)
		Me.GroupBox2.PerformLayout()
		Me.StatusStrip1.ResumeLayout(False)
		Me.StatusStrip1.PerformLayout()
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub
	Friend WithEvents ScreenshotPictureBox As System.Windows.Forms.PictureBox
	Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
	Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
	Friend WithEvents Label1 As System.Windows.Forms.Label
	Friend WithEvents AvaliableSessionsComboBox As System.Windows.Forms.ComboBox
	Friend WithEvents Label2 As System.Windows.Forms.Label
	Friend WithEvents WorkingDirectoryTextBox As System.Windows.Forms.TextBox
	Friend WithEvents TakeScreenshotButton As System.Windows.Forms.Button
	Friend WithEvents BrowseForWorkingDirectoryButton As System.Windows.Forms.Button
	Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
	Friend WithEvents ToolStripStatusLabel1 As System.Windows.Forms.ToolStripStatusLabel
	Friend WithEvents Label4 As System.Windows.Forms.Label
	Friend WithEvents TargetHostnameTextBox As System.Windows.Forms.TextBox
	Friend WithEvents OpenWorkingDirectoryLinkLabel As System.Windows.Forms.LinkLabel
End Class
