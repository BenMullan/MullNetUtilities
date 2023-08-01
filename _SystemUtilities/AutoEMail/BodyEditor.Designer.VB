<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class BodyEditor
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(BodyEditor))
        Me.OkButton1 = New System.Windows.Forms.Button()
        Me.BodyEditorIsHTMLCheckBox = New System.Windows.Forms.CheckBox()
        Me.BodyEditorTextBox = New System.Windows.Forms.TextBox()
        Me.InsertTabLink = New System.Windows.Forms.LinkLabel()
        Me.UseTemplateLinkLabel = New System.Windows.Forms.LinkLabel()
        Me.SuspendLayout()
        '
        'OkButton1
        '
        Me.OkButton1.Location = New System.Drawing.Point(355, 432)
        Me.OkButton1.Name = "OkButton1"
        Me.OkButton1.Size = New System.Drawing.Size(75, 23)
        Me.OkButton1.TabIndex = 0
        Me.OkButton1.Text = "Ok"
        Me.OkButton1.UseVisualStyleBackColor = True
        '
        'BodyEditorIsHTMLCheckBox
        '
        Me.BodyEditorIsHTMLCheckBox.AutoSize = True
        Me.BodyEditorIsHTMLCheckBox.Location = New System.Drawing.Point(12, 436)
        Me.BodyEditorIsHTMLCheckBox.Name = "BodyEditorIsHTMLCheckBox"
        Me.BodyEditorIsHTMLCheckBox.Size = New System.Drawing.Size(67, 17)
        Me.BodyEditorIsHTMLCheckBox.TabIndex = 1
        Me.BodyEditorIsHTMLCheckBox.Text = "Is HTML"
        Me.BodyEditorIsHTMLCheckBox.UseVisualStyleBackColor = True
        '
        'BodyEditorTextBox
        '
        Me.BodyEditorTextBox.Dock = System.Windows.Forms.DockStyle.Top
        Me.BodyEditorTextBox.Location = New System.Drawing.Point(0, 0)
        Me.BodyEditorTextBox.Multiline = True
        Me.BodyEditorTextBox.Name = "BodyEditorTextBox"
        Me.BodyEditorTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.BodyEditorTextBox.Size = New System.Drawing.Size(784, 426)
        Me.BodyEditorTextBox.TabIndex = 4
        '
        'InsertTabLink
        '
        Me.InsertTabLink.AutoSize = True
        Me.InsertTabLink.Location = New System.Drawing.Point(680, 437)
        Me.InsertTabLink.Name = "InsertTabLink"
        Me.InsertTabLink.Size = New System.Drawing.Size(92, 13)
        Me.InsertTabLink.TabIndex = 3
        Me.InsertTabLink.TabStop = True
        Me.InsertTabLink.Text = "Insert Tab (Ctrl+T)"
        '
        'UseTemplateLinkLabel
        '
        Me.UseTemplateLinkLabel.AutoSize = True
        Me.UseTemplateLinkLabel.Location = New System.Drawing.Point(525, 437)
        Me.UseTemplateLinkLabel.Name = "UseTemplateLinkLabel"
        Me.UseTemplateLinkLabel.Size = New System.Drawing.Size(73, 13)
        Me.UseTemplateLinkLabel.TabIndex = 2
        Me.UseTemplateLinkLabel.TabStop = True
        Me.UseTemplateLinkLabel.Text = "Use Template"
        '
        'BodyEditor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(784, 462)
        Me.Controls.Add(Me.UseTemplateLinkLabel)
        Me.Controls.Add(Me.InsertTabLink)
        Me.Controls.Add(Me.BodyEditorTextBox)
        Me.Controls.Add(Me.BodyEditorIsHTMLCheckBox)
        Me.Controls.Add(Me.OkButton1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(800, 500)
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(800, 500)
        Me.Name = "BodyEditor"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Body Editor"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents OkButton1 As System.Windows.Forms.Button
    Friend WithEvents BodyEditorIsHTMLCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents BodyEditorTextBox As System.Windows.Forms.TextBox
    Friend WithEvents InsertTabLink As System.Windows.Forms.LinkLabel
    Friend WithEvents UseTemplateLinkLabel As System.Windows.Forms.LinkLabel
End Class
