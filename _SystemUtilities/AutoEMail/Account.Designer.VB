<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Account
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Account))
        Me.OkButton = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.EMailAccountComboBox = New System.Windows.Forms.ComboBox()
        Me.CustomAccountLabel = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'OkButton
        '
        Me.OkButton.Location = New System.Drawing.Point(130, 102)
        Me.OkButton.Name = "OkButton"
        Me.OkButton.Size = New System.Drawing.Size(75, 23)
        Me.OkButton.TabIndex = 1
        Me.OkButton.Text = "Ok"
        Me.OkButton.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(302, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Please select the EMail Account with which EMails will be sent"
        '
        'EMailAccountComboBox
        '
        Me.EMailAccountComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.EMailAccountComboBox.FormattingEnabled = True
        Me.EMailAccountComboBox.Items.AddRange(New Object() {"Bob.The.Builder.802.11@GMail.Com", "Custom..."})
        Me.EMailAccountComboBox.Location = New System.Drawing.Point(15, 37)
        Me.EMailAccountComboBox.Name = "EMailAccountComboBox"
        Me.EMailAccountComboBox.Size = New System.Drawing.Size(299, 21)
        Me.EMailAccountComboBox.TabIndex = 0
        '
        'CustomAccountLabel
        '
        Me.CustomAccountLabel.AutoSize = True
        Me.CustomAccountLabel.Location = New System.Drawing.Point(70, 77)
        Me.CustomAccountLabel.Name = "CustomAccountLabel"
        Me.CustomAccountLabel.Size = New System.Drawing.Size(195, 13)
        Me.CustomAccountLabel.TabIndex = 3
        Me.CustomAccountLabel.Text = "The Custom Account is currently not set"
        '
        'Account
        '
        Me.AcceptButton = Me.OkButton
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(334, 132)
        Me.Controls.Add(Me.CustomAccountLabel)
        Me.Controls.Add(Me.EMailAccountComboBox)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.OkButton)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(350, 170)
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(350, 170)
        Me.Name = "Account"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Sender Account"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents OkButton As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents EMailAccountComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents CustomAccountLabel As System.Windows.Forms.Label
End Class
