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
		Me.components = New System.ComponentModel.Container()
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainWindow))
		Me.TargetGroupBox = New System.Windows.Forms.GroupBox()
		Me.RescanOpenWindowsButton = New System.Windows.Forms.Button()
		Me.AvaliableWindowsComboBox = New System.Windows.Forms.ComboBox()
		Me.Target_ThisWindow_RadioButton = New System.Windows.Forms.RadioButton()
		Me.Target_Unspecified_RadioButton = New System.Windows.Forms.RadioButton()
		Me.KeystrokesGroupBox = New System.Windows.Forms.GroupBox()
		Me.ViewExpansionsLinkLabel = New System.Windows.Forms.LinkLabel()
		Me.KeysToSendTextBox = New System.Windows.Forms.TextBox()
		Me.SendKeystrokesButton = New System.Windows.Forms.Button()
		Me.DelayBeforeStartingCheckBox = New System.Windows.Forms.CheckBox()
		Me.TheToolTipProvider = New System.Windows.Forms.ToolTip(Me.components)
		Me.DelayBeforeStartingNumericUpDown = New System.Windows.Forms.NumericUpDown()
		Me.MSLabel = New System.Windows.Forms.Label()
		Me.GenerateVBSFileLinkLabel = New System.Windows.Forms.LinkLabel()
		Me.TheStatusStrip = New System.Windows.Forms.StatusStrip()
		Me.TheToolStripStatusLabel = New System.Windows.Forms.ToolStripStatusLabel()
		Me.SendWaitCheckBox = New System.Windows.Forms.CheckBox()
		Me.OptionsGroupBox = New System.Windows.Forms.GroupBox()
		Me.VBSFileSaveFileDialog = New System.Windows.Forms.SaveFileDialog()
		Me.SelectWindowByPositionButton = New System.Windows.Forms.PictureBox()
		Me.TargetGroupBox.SuspendLayout()
		Me.KeystrokesGroupBox.SuspendLayout()
		CType(Me.DelayBeforeStartingNumericUpDown, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.TheStatusStrip.SuspendLayout()
		Me.OptionsGroupBox.SuspendLayout()
		CType(Me.SelectWindowByPositionButton, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SuspendLayout()
		'
		'TargetGroupBox
		'
		Me.TargetGroupBox.Controls.Add(Me.SelectWindowByPositionButton)
		Me.TargetGroupBox.Controls.Add(Me.RescanOpenWindowsButton)
		Me.TargetGroupBox.Controls.Add(Me.AvaliableWindowsComboBox)
		Me.TargetGroupBox.Controls.Add(Me.Target_ThisWindow_RadioButton)
		Me.TargetGroupBox.Controls.Add(Me.Target_Unspecified_RadioButton)
		Me.TargetGroupBox.Location = New System.Drawing.Point(12, 12)
		Me.TargetGroupBox.Name = "TargetGroupBox"
		Me.TargetGroupBox.Size = New System.Drawing.Size(482, 77)
		Me.TargetGroupBox.TabIndex = 1
		Me.TargetGroupBox.TabStop = False
		Me.TargetGroupBox.Text = "Target"
		'
		'RescanOpenWindowsButton
		'
		Me.RescanOpenWindowsButton.Location = New System.Drawing.Point(415, 40)
		Me.RescanOpenWindowsButton.Name = "RescanOpenWindowsButton"
		Me.RescanOpenWindowsButton.Size = New System.Drawing.Size(52, 23)
		Me.RescanOpenWindowsButton.TabIndex = 4
		Me.RescanOpenWindowsButton.Text = "&Rescan"
		Me.RescanOpenWindowsButton.UseVisualStyleBackColor = True
		'
		'AvaliableWindowsComboBox
		'
		Me.AvaliableWindowsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.AvaliableWindowsComboBox.FormattingEnabled = True
		Me.AvaliableWindowsComboBox.Location = New System.Drawing.Point(105, 42)
		Me.AvaliableWindowsComboBox.Name = "AvaliableWindowsComboBox"
		Me.AvaliableWindowsComboBox.Size = New System.Drawing.Size(279, 21)
		Me.AvaliableWindowsComboBox.TabIndex = 3
		'
		'Target_ThisWindow_RadioButton
		'
		Me.Target_ThisWindow_RadioButton.AutoSize = True
		Me.Target_ThisWindow_RadioButton.Location = New System.Drawing.Point(14, 43)
		Me.Target_ThisWindow_RadioButton.Name = "Target_ThisWindow_RadioButton"
		Me.Target_ThisWindow_RadioButton.Size = New System.Drawing.Size(90, 17)
		Me.Target_ThisWindow_RadioButton.TabIndex = 2
		Me.Target_ThisWindow_RadioButton.Text = "&This Window:"
		Me.Target_ThisWindow_RadioButton.UseVisualStyleBackColor = True
		'
		'Target_Unspecified_RadioButton
		'
		Me.Target_Unspecified_RadioButton.AutoSize = True
		Me.Target_Unspecified_RadioButton.Checked = True
		Me.Target_Unspecified_RadioButton.Location = New System.Drawing.Point(14, 20)
		Me.Target_Unspecified_RadioButton.Name = "Target_Unspecified_RadioButton"
		Me.Target_Unspecified_RadioButton.Size = New System.Drawing.Size(81, 17)
		Me.Target_Unspecified_RadioButton.TabIndex = 1
		Me.Target_Unspecified_RadioButton.TabStop = True
		Me.Target_Unspecified_RadioButton.Text = "&Unspecified"
		Me.Target_Unspecified_RadioButton.UseVisualStyleBackColor = True
		'
		'KeystrokesGroupBox
		'
		Me.KeystrokesGroupBox.Controls.Add(Me.ViewExpansionsLinkLabel)
		Me.KeystrokesGroupBox.Controls.Add(Me.KeysToSendTextBox)
		Me.KeystrokesGroupBox.Location = New System.Drawing.Point(12, 95)
		Me.KeystrokesGroupBox.Name = "KeystrokesGroupBox"
		Me.KeystrokesGroupBox.Size = New System.Drawing.Size(482, 174)
		Me.KeystrokesGroupBox.TabIndex = 1
		Me.KeystrokesGroupBox.TabStop = False
		Me.KeystrokesGroupBox.Text = "Keystrokes"
		'
		'ViewExpansionsLinkLabel
		'
		Me.ViewExpansionsLinkLabel.AutoSize = True
		Me.ViewExpansionsLinkLabel.Location = New System.Drawing.Point(6, 16)
		Me.ViewExpansionsLinkLabel.Name = "ViewExpansionsLinkLabel"
		Me.ViewExpansionsLinkLabel.Size = New System.Drawing.Size(70, 13)
		Me.ViewExpansionsLinkLabel.TabIndex = 2
		Me.ViewExpansionsLinkLabel.TabStop = True
		Me.ViewExpansionsLinkLabel.Text = "Expansions..."
		'
		'KeysToSendTextBox
		'
		Me.KeysToSendTextBox.Location = New System.Drawing.Point(6, 32)
		Me.KeysToSendTextBox.Multiline = True
		Me.KeysToSendTextBox.Name = "KeysToSendTextBox"
		Me.KeysToSendTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both
		Me.KeysToSendTextBox.Size = New System.Drawing.Size(470, 136)
		Me.KeysToSendTextBox.TabIndex = 1
		'
		'SendKeystrokesButton
		'
		Me.SendKeystrokesButton.Location = New System.Drawing.Point(201, 358)
		Me.SendKeystrokesButton.Name = "SendKeystrokesButton"
		Me.SendKeystrokesButton.Size = New System.Drawing.Size(103, 23)
		Me.SendKeystrokesButton.TabIndex = 2
		Me.SendKeystrokesButton.Text = "&Send Keystrokes"
		Me.SendKeystrokesButton.UseVisualStyleBackColor = True
		'
		'DelayBeforeStartingCheckBox
		'
		Me.DelayBeforeStartingCheckBox.AutoSize = True
		Me.DelayBeforeStartingCheckBox.Location = New System.Drawing.Point(14, 23)
		Me.DelayBeforeStartingCheckBox.Name = "DelayBeforeStartingCheckBox"
		Me.DelayBeforeStartingCheckBox.Size = New System.Drawing.Size(126, 17)
		Me.DelayBeforeStartingCheckBox.TabIndex = 3
		Me.DelayBeforeStartingCheckBox.Text = "&Delay before starting:"
		Me.DelayBeforeStartingCheckBox.UseVisualStyleBackColor = True
		'
		'TheToolTipProvider
		'
		Me.TheToolTipProvider.AutoPopDelay = 20000
		Me.TheToolTipProvider.InitialDelay = 10
		Me.TheToolTipProvider.ReshowDelay = 10
		Me.TheToolTipProvider.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info
		Me.TheToolTipProvider.ToolTipTitle = "Info"
		'
		'DelayBeforeStartingNumericUpDown
		'
		Me.DelayBeforeStartingNumericUpDown.Enabled = False
		Me.DelayBeforeStartingNumericUpDown.Location = New System.Drawing.Point(140, 22)
		Me.DelayBeforeStartingNumericUpDown.Maximum = New Decimal(New Integer() {10000000, 0, 0, 0})
		Me.DelayBeforeStartingNumericUpDown.Name = "DelayBeforeStartingNumericUpDown"
		Me.DelayBeforeStartingNumericUpDown.Size = New System.Drawing.Size(73, 20)
		Me.DelayBeforeStartingNumericUpDown.TabIndex = 4
		Me.DelayBeforeStartingNumericUpDown.Value = New Decimal(New Integer() {3000, 0, 0, 0})
		'
		'MSLabel
		'
		Me.MSLabel.AutoSize = True
		Me.MSLabel.Location = New System.Drawing.Point(213, 24)
		Me.MSLabel.Name = "MSLabel"
		Me.MSLabel.Size = New System.Drawing.Size(20, 13)
		Me.MSLabel.TabIndex = 0
		Me.MSLabel.Text = "ms"
		'
		'GenerateVBSFileLinkLabel
		'
		Me.GenerateVBSFileLinkLabel.AutoSize = True
		Me.GenerateVBSFileLinkLabel.Location = New System.Drawing.Point(399, 394)
		Me.GenerateVBSFileLinkLabel.Name = "GenerateVBSFileLinkLabel"
		Me.GenerateVBSFileLinkLabel.Size = New System.Drawing.Size(102, 13)
		Me.GenerateVBSFileLinkLabel.TabIndex = 2
		Me.GenerateVBSFileLinkLabel.TabStop = True
		Me.GenerateVBSFileLinkLabel.Text = "Export to VBScript..."
		'
		'TheStatusStrip
		'
		Me.TheStatusStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TheToolStripStatusLabel})
		Me.TheStatusStrip.Location = New System.Drawing.Point(0, 390)
		Me.TheStatusStrip.Name = "TheStatusStrip"
		Me.TheStatusStrip.Size = New System.Drawing.Size(504, 22)
		Me.TheStatusStrip.SizingGrip = False
		Me.TheStatusStrip.TabIndex = 5
		Me.TheStatusStrip.Text = "TheStatusStrip"
		'
		'TheToolStripStatusLabel
		'
		Me.TheToolStripStatusLabel.Name = "TheToolStripStatusLabel"
		Me.TheToolStripStatusLabel.Size = New System.Drawing.Size(64, 17)
		Me.TheToolStripStatusLabel.Text = "Status: Idle"
		'
		'SendWaitCheckBox
		'
		Me.SendWaitCheckBox.AutoSize = True
		Me.SendWaitCheckBox.Checked = True
		Me.SendWaitCheckBox.CheckState = System.Windows.Forms.CheckState.Checked
		Me.SendWaitCheckBox.Location = New System.Drawing.Point(14, 46)
		Me.SendWaitCheckBox.Name = "SendWaitCheckBox"
		Me.SendWaitCheckBox.Size = New System.Drawing.Size(295, 17)
		Me.SendWaitCheckBox.TabIndex = 3
		Me.SendWaitCheckBox.Text = "&Wait for Keystrokes to be processed by target application"
		Me.SendWaitCheckBox.UseVisualStyleBackColor = True
		'
		'OptionsGroupBox
		'
		Me.OptionsGroupBox.Controls.Add(Me.DelayBeforeStartingCheckBox)
		Me.OptionsGroupBox.Controls.Add(Me.MSLabel)
		Me.OptionsGroupBox.Controls.Add(Me.DelayBeforeStartingNumericUpDown)
		Me.OptionsGroupBox.Controls.Add(Me.SendWaitCheckBox)
		Me.OptionsGroupBox.Location = New System.Drawing.Point(12, 275)
		Me.OptionsGroupBox.Name = "OptionsGroupBox"
		Me.OptionsGroupBox.Size = New System.Drawing.Size(482, 72)
		Me.OptionsGroupBox.TabIndex = 6
		Me.OptionsGroupBox.TabStop = False
		Me.OptionsGroupBox.Text = "Options"
		'
		'VBSFileSaveFileDialog
		'
		Me.VBSFileSaveFileDialog.DefaultExt = "VBS"
		Me.VBSFileSaveFileDialog.Filter = "VBScript Files|*.vbs|All Files|*.*"
		Me.VBSFileSaveFileDialog.InitialDirectory = "\"
		Me.VBSFileSaveFileDialog.Title = "SendKeys GUI - Export to VBScript..."
		'
		'SelectWindowByPositionButton
		'
		Me.SelectWindowByPositionButton.Cursor = System.Windows.Forms.Cursors.Hand
		Me.SelectWindowByPositionButton.Image = Global.SendKeysGUI.My.Resources.Resources.ProcExp_WindowSelection_TargetIcon
		Me.SelectWindowByPositionButton.Location = New System.Drawing.Point(388, 40)
		Me.SelectWindowByPositionButton.Margin = New System.Windows.Forms.Padding(1)
		Me.SelectWindowByPositionButton.Name = "SelectWindowByPositionButton"
		Me.SelectWindowByPositionButton.Size = New System.Drawing.Size(23, 23)
		Me.SelectWindowByPositionButton.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
		Me.SelectWindowByPositionButton.TabIndex = 5
		Me.SelectWindowByPositionButton.TabStop = False
		'
		'MainWindow
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(504, 412)
		Me.Controls.Add(Me.OptionsGroupBox)
		Me.Controls.Add(Me.GenerateVBSFileLinkLabel)
		Me.Controls.Add(Me.TheStatusStrip)
		Me.Controls.Add(Me.SendKeystrokesButton)
		Me.Controls.Add(Me.KeystrokesGroupBox)
		Me.Controls.Add(Me.TargetGroupBox)
		Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
		Me.MaximizeBox = False
		Me.MaximumSize = New System.Drawing.Size(520, 450)
		Me.MinimizeBox = False
		Me.MinimumSize = New System.Drawing.Size(520, 450)
		Me.Name = "MainWindow"
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
		Me.Text = "SendKeys GUI"
		Me.TargetGroupBox.ResumeLayout(False)
		Me.TargetGroupBox.PerformLayout()
		Me.KeystrokesGroupBox.ResumeLayout(False)
		Me.KeystrokesGroupBox.PerformLayout()
		CType(Me.DelayBeforeStartingNumericUpDown, System.ComponentModel.ISupportInitialize).EndInit()
		Me.TheStatusStrip.ResumeLayout(False)
		Me.TheStatusStrip.PerformLayout()
		Me.OptionsGroupBox.ResumeLayout(False)
		Me.OptionsGroupBox.PerformLayout()
		CType(Me.SelectWindowByPositionButton, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub
	Friend WithEvents TargetGroupBox As System.Windows.Forms.GroupBox
	Friend WithEvents KeystrokesGroupBox As System.Windows.Forms.GroupBox
	Friend WithEvents KeysToSendTextBox As System.Windows.Forms.TextBox
	Friend WithEvents SendKeystrokesButton As System.Windows.Forms.Button
	Friend WithEvents DelayBeforeStartingCheckBox As System.Windows.Forms.CheckBox
	Friend WithEvents TheToolTipProvider As System.Windows.Forms.ToolTip
	Friend WithEvents DelayBeforeStartingNumericUpDown As System.Windows.Forms.NumericUpDown
	Friend WithEvents MSLabel As System.Windows.Forms.Label
	Friend WithEvents ViewExpansionsLinkLabel As System.Windows.Forms.LinkLabel
	Friend WithEvents AvaliableWindowsComboBox As System.Windows.Forms.ComboBox
	Friend WithEvents Target_ThisWindow_RadioButton As System.Windows.Forms.RadioButton
	Friend WithEvents Target_Unspecified_RadioButton As System.Windows.Forms.RadioButton
	Friend WithEvents GenerateVBSFileLinkLabel As System.Windows.Forms.LinkLabel
	Friend WithEvents RescanOpenWindowsButton As System.Windows.Forms.Button
	Friend WithEvents TheStatusStrip As System.Windows.Forms.StatusStrip
	Friend WithEvents TheToolStripStatusLabel As System.Windows.Forms.ToolStripStatusLabel
	Friend WithEvents SendWaitCheckBox As System.Windows.Forms.CheckBox
	Friend WithEvents OptionsGroupBox As System.Windows.Forms.GroupBox
	Friend WithEvents VBSFileSaveFileDialog As System.Windows.Forms.SaveFileDialog
	Friend WithEvents SelectWindowByPositionButton As System.Windows.Forms.PictureBox
End Class
