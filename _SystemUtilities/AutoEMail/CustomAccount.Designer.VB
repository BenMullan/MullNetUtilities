<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CustomAccount
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
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(CustomAccount))
		Me.OkButton = New System.Windows.Forms.Button()
		Me.Label1 = New System.Windows.Forms.Label()
		Me.Label2 = New System.Windows.Forms.Label()
		Me.SMTPServerLabel = New System.Windows.Forms.Label()
		Me.UsesSSLCheckBox = New System.Windows.Forms.CheckBox()
		Me.SMTPServerTextBox = New System.Windows.Forms.TextBox()
		Me.Label4 = New System.Windows.Forms.Label()
		Me.PortTextBox = New System.Windows.Forms.TextBox()
		Me.PasswordTextBox = New System.Windows.Forms.TextBox()
		Me.EMailAddressTextBox = New System.Windows.Forms.TextBox()
		Me.Label5 = New System.Windows.Forms.Label()
		Me.SuspendLayout()
		'
		'OkButton
		'
		Me.OkButton.Location = New System.Drawing.Point(170, 164)
		Me.OkButton.Name = "OkButton"
		Me.OkButton.Size = New System.Drawing.Size(75, 23)
		Me.OkButton.TabIndex = 5
		Me.OkButton.Text = "Ok"
		Me.OkButton.UseVisualStyleBackColor = True
		'
		'Label1
		'
		Me.Label1.AutoSize = True
		Me.Label1.Location = New System.Drawing.Point(12, 15)
		Me.Label1.Name = "Label1"
		Me.Label1.Size = New System.Drawing.Size(77, 13)
		Me.Label1.TabIndex = 6
		Me.Label1.Text = "EMail Address:"
		'
		'Label2
		'
		Me.Label2.AutoSize = True
		Me.Label2.Location = New System.Drawing.Point(12, 41)
		Me.Label2.Name = "Label2"
		Me.Label2.Size = New System.Drawing.Size(56, 13)
		Me.Label2.TabIndex = 7
		Me.Label2.Text = "Password:"
		'
		'SMTPServerLabel
		'
		Me.SMTPServerLabel.AutoSize = True
		Me.SMTPServerLabel.Location = New System.Drawing.Point(12, 67)
		Me.SMTPServerLabel.Name = "SMTPServerLabel"
		Me.SMTPServerLabel.Size = New System.Drawing.Size(74, 13)
		Me.SMTPServerLabel.TabIndex = 8
		Me.SMTPServerLabel.Text = "SMTP Server:"
		'
		'UsesSSLCheckBox
		'
		Me.UsesSSLCheckBox.AutoSize = True
		Me.UsesSSLCheckBox.Checked = True
		Me.UsesSSLCheckBox.CheckState = System.Windows.Forms.CheckState.Checked
		Me.UsesSSLCheckBox.Location = New System.Drawing.Point(335, 66)
		Me.UsesSSLCheckBox.Name = "UsesSSLCheckBox"
		Me.UsesSSLCheckBox.Size = New System.Drawing.Size(73, 17)
		Me.UsesSSLCheckBox.TabIndex = 2
		Me.UsesSSLCheckBox.Text = "Uses SSL"
		Me.UsesSSLCheckBox.UseVisualStyleBackColor = True
		'
		'SMTPServerTextBox
		'
		Me.SMTPServerTextBox.Location = New System.Drawing.Point(95, 64)
		Me.SMTPServerTextBox.Name = "SMTPServerTextBox"
		Me.SMTPServerTextBox.Size = New System.Drawing.Size(234, 20)
		Me.SMTPServerTextBox.TabIndex = 3
		Me.SMTPServerTextBox.Text = "SMTP.GMail.Com"
		'
		'Label4
		'
		Me.Label4.AutoSize = True
		Me.Label4.Location = New System.Drawing.Point(12, 93)
		Me.Label4.Name = "Label4"
		Me.Label4.Size = New System.Drawing.Size(29, 13)
		Me.Label4.TabIndex = 9
		Me.Label4.Text = "Port:"
		'
		'PortTextBox
		'
		Me.PortTextBox.Location = New System.Drawing.Point(95, 90)
		Me.PortTextBox.Name = "PortTextBox"
		Me.PortTextBox.Size = New System.Drawing.Size(66, 20)
		Me.PortTextBox.TabIndex = 4
		Me.PortTextBox.Text = "587"
		'
		'PasswordTextBox
		'
		Me.PasswordTextBox.Location = New System.Drawing.Point(95, 38)
		Me.PasswordTextBox.Name = "PasswordTextBox"
		Me.PasswordTextBox.Size = New System.Drawing.Size(234, 20)
		Me.PasswordTextBox.TabIndex = 1
		'
		'EMailAddressTextBox
		'
		Me.EMailAddressTextBox.Location = New System.Drawing.Point(95, 12)
		Me.EMailAddressTextBox.Name = "EMailAddressTextBox"
		Me.EMailAddressTextBox.Size = New System.Drawing.Size(234, 20)
		Me.EMailAddressTextBox.TabIndex = 0
		'
		'Label5
		'
		Me.Label5.AutoSize = True
		Me.Label5.Location = New System.Drawing.Point(39, 118)
		Me.Label5.Name = "Label5"
		Me.Label5.Size = New System.Drawing.Size(349, 39)
		Me.Label5.TabIndex = 10
		Me.Label5.Text = "Common ports: 25, 465, 587. If you are using a GMail Account, ensure" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "that the se" & _
		  "tting ""Less secure app acsess"" is turned ""On"". Google reports" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "that this setting" & _
		  " will be removed as of 30-05-2022."
		'
		'CustomAccount
		'
		Me.AcceptButton = Me.OkButton
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(414, 192)
		Me.Controls.Add(Me.OkButton)
		Me.Controls.Add(Me.Label5)
		Me.Controls.Add(Me.EMailAddressTextBox)
		Me.Controls.Add(Me.PasswordTextBox)
		Me.Controls.Add(Me.PortTextBox)
		Me.Controls.Add(Me.Label4)
		Me.Controls.Add(Me.SMTPServerTextBox)
		Me.Controls.Add(Me.UsesSSLCheckBox)
		Me.Controls.Add(Me.SMTPServerLabel)
		Me.Controls.Add(Me.Label2)
		Me.Controls.Add(Me.Label1)
		Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
		Me.MaximizeBox = False
		Me.MaximumSize = New System.Drawing.Size(430, 230)
		Me.MinimizeBox = False
		Me.MinimumSize = New System.Drawing.Size(430, 230)
		Me.Name = "CustomAccount"
		Me.ShowIcon = False
		Me.ShowInTaskbar = False
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
		Me.Text = "Custom Sender Account"
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub
    Friend WithEvents OkButton As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents SMTPServerLabel As System.Windows.Forms.Label
    Friend WithEvents UsesSSLCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents SMTPServerTextBox As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents PortTextBox As System.Windows.Forms.TextBox
    Friend WithEvents PasswordTextBox As System.Windows.Forms.TextBox
    Friend WithEvents EMailAddressTextBox As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
End Class
