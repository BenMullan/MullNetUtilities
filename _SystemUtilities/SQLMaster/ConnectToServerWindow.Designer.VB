<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ConnectToServerWindow
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
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ConnectToServerWindow))
		Me.OkButton = New System.Windows.Forms.Button()
		Me.ServerNameLabel = New System.Windows.Forms.Label()
		Me.ServerNameAndInstance_TextBox = New System.Windows.Forms.TextBox()
		Me.GroupBox1 = New System.Windows.Forms.GroupBox()
		Me.AuthTypeLabel = New System.Windows.Forms.Label()
		Me.AuthPassword_TextBox = New System.Windows.Forms.TextBox()
		Me.Label2 = New System.Windows.Forms.Label()
		Me.AuthUsername_TextBox = New System.Windows.Forms.TextBox()
		Me.AuthUsernameLabel = New System.Windows.Forms.Label()
		Me.AuthenticationMode_ComboBox = New System.Windows.Forms.ComboBox()
		Me.GroupBox2 = New System.Windows.Forms.GroupBox()
		Me.Label3 = New System.Windows.Forms.Label()
		Me.ListLocalInstances_LinkLabel = New System.Windows.Forms.LinkLabel()
		Me.GroupBox1.SuspendLayout()
		Me.GroupBox2.SuspendLayout()
		Me.SuspendLayout()
		'
		'OkButton
		'
		Me.OkButton.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
				  Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.OkButton.Location = New System.Drawing.Point(130, 234)
		Me.OkButton.Name = "OkButton"
		Me.OkButton.Size = New System.Drawing.Size(75, 23)
		Me.OkButton.TabIndex = 2
		Me.OkButton.Text = "&Ok"
		Me.OkButton.UseVisualStyleBackColor = True
		'
		'ServerNameLabel
		'
		Me.ServerNameLabel.AutoSize = True
		Me.ServerNameLabel.Location = New System.Drawing.Point(6, 25)
		Me.ServerNameLabel.Name = "ServerNameLabel"
		Me.ServerNameLabel.Size = New System.Drawing.Size(41, 13)
		Me.ServerNameLabel.TabIndex = 0
		Me.ServerNameLabel.Text = "Server:"
		'
		'ServerNameAndInstance_TextBox
		'
		Me.ServerNameAndInstance_TextBox.Location = New System.Drawing.Point(74, 22)
		Me.ServerNameAndInstance_TextBox.Name = "ServerNameAndInstance_TextBox"
		Me.ServerNameAndInstance_TextBox.Size = New System.Drawing.Size(230, 20)
		Me.ServerNameAndInstance_TextBox.TabIndex = 2
		'
		'GroupBox1
		'
		Me.GroupBox1.Controls.Add(Me.AuthTypeLabel)
		Me.GroupBox1.Controls.Add(Me.AuthPassword_TextBox)
		Me.GroupBox1.Controls.Add(Me.Label2)
		Me.GroupBox1.Controls.Add(Me.AuthUsername_TextBox)
		Me.GroupBox1.Controls.Add(Me.AuthUsernameLabel)
		Me.GroupBox1.Controls.Add(Me.AuthenticationMode_ComboBox)
		Me.GroupBox1.Location = New System.Drawing.Point(12, 111)
		Me.GroupBox1.Name = "GroupBox1"
		Me.GroupBox1.Size = New System.Drawing.Size(310, 106)
		Me.GroupBox1.TabIndex = 1
		Me.GroupBox1.TabStop = False
		Me.GroupBox1.Text = "Authentication"
		'
		'AuthTypeLabel
		'
		Me.AuthTypeLabel.AutoSize = True
		Me.AuthTypeLabel.Location = New System.Drawing.Point(6, 27)
		Me.AuthTypeLabel.Name = "AuthTypeLabel"
		Me.AuthTypeLabel.Size = New System.Drawing.Size(34, 13)
		Me.AuthTypeLabel.TabIndex = 0
		Me.AuthTypeLabel.Text = "Type:"
		'
		'AuthPassword_TextBox
		'
		Me.AuthPassword_TextBox.Location = New System.Drawing.Point(74, 77)
		Me.AuthPassword_TextBox.Name = "AuthPassword_TextBox"
		Me.AuthPassword_TextBox.Size = New System.Drawing.Size(230, 20)
		Me.AuthPassword_TextBox.TabIndex = 5
		Me.AuthPassword_TextBox.UseSystemPasswordChar = True
		'
		'Label2
		'
		Me.Label2.AutoSize = True
		Me.Label2.Location = New System.Drawing.Point(6, 80)
		Me.Label2.Name = "Label2"
		Me.Label2.Size = New System.Drawing.Size(56, 13)
		Me.Label2.TabIndex = 2
		Me.Label2.Text = "Password:"
		'
		'AuthUsername_TextBox
		'
		Me.AuthUsername_TextBox.Location = New System.Drawing.Point(74, 51)
		Me.AuthUsername_TextBox.Name = "AuthUsername_TextBox"
		Me.AuthUsername_TextBox.Size = New System.Drawing.Size(230, 20)
		Me.AuthUsername_TextBox.TabIndex = 4
		'
		'AuthUsernameLabel
		'
		Me.AuthUsernameLabel.AutoSize = True
		Me.AuthUsernameLabel.Location = New System.Drawing.Point(6, 54)
		Me.AuthUsernameLabel.Name = "AuthUsernameLabel"
		Me.AuthUsernameLabel.Size = New System.Drawing.Size(58, 13)
		Me.AuthUsernameLabel.TabIndex = 1
		Me.AuthUsernameLabel.Text = "Username:"
		'
		'AuthenticationMode_ComboBox
		'
		Me.AuthenticationMode_ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.AuthenticationMode_ComboBox.FormattingEnabled = True
		Me.AuthenticationMode_ComboBox.Items.AddRange(New Object() {"Windows Authentication", "SQL Server Authentication"})
		Me.AuthenticationMode_ComboBox.Location = New System.Drawing.Point(74, 24)
		Me.AuthenticationMode_ComboBox.Name = "AuthenticationMode_ComboBox"
		Me.AuthenticationMode_ComboBox.Size = New System.Drawing.Size(230, 21)
		Me.AuthenticationMode_ComboBox.TabIndex = 3
		'
		'GroupBox2
		'
		Me.GroupBox2.Controls.Add(Me.ListLocalInstances_LinkLabel)
		Me.GroupBox2.Controls.Add(Me.ServerNameAndInstance_TextBox)
		Me.GroupBox2.Controls.Add(Me.Label3)
		Me.GroupBox2.Controls.Add(Me.ServerNameLabel)
		Me.GroupBox2.Location = New System.Drawing.Point(12, 12)
		Me.GroupBox2.Name = "GroupBox2"
		Me.GroupBox2.Size = New System.Drawing.Size(310, 83)
		Me.GroupBox2.TabIndex = 0
		Me.GroupBox2.TabStop = False
		Me.GroupBox2.Text = "Instance"
		'
		'Label3
		'
		Me.Label3.AutoSize = True
		Me.Label3.ForeColor = System.Drawing.SystemColors.WindowFrame
		Me.Label3.Location = New System.Drawing.Point(77, 45)
		Me.Label3.Name = "Label3"
		Me.Label3.Size = New System.Drawing.Size(185, 13)
		Me.Label3.TabIndex = 1
		Me.Label3.Text = "E.g. ""."" or ""MNLT01\SQLEXPRESS"""
		'
		'ListLocalInstances_LinkLabel
		'
		Me.ListLocalInstances_LinkLabel.AutoSize = True
		Me.ListLocalInstances_LinkLabel.Location = New System.Drawing.Point(77, 61)
		Me.ListLocalInstances_LinkLabel.Name = "ListLocalInstances_LinkLabel"
		Me.ListLocalInstances_LinkLabel.Size = New System.Drawing.Size(106, 13)
		Me.ListLocalInstances_LinkLabel.TabIndex = 3
		Me.ListLocalInstances_LinkLabel.TabStop = True
		Me.ListLocalInstances_LinkLabel.Text = "List local Instances..."
		'
		'ConnectToServerWindow
		'
		Me.AcceptButton = Me.OkButton
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(334, 265)
		Me.Controls.Add(Me.GroupBox2)
		Me.Controls.Add(Me.GroupBox1)
		Me.Controls.Add(Me.OkButton)
		Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
		Me.MaximizeBox = False
		Me.MinimizeBox = False
		Me.Name = "ConnectToServerWindow"
		Me.ShowIcon = False
		Me.ShowInTaskbar = False
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
		Me.Text = "Connect to SQL Server"
		Me.GroupBox1.ResumeLayout(False)
		Me.GroupBox1.PerformLayout()
		Me.GroupBox2.ResumeLayout(False)
		Me.GroupBox2.PerformLayout()
		Me.ResumeLayout(False)

	End Sub
	Friend WithEvents OkButton As System.Windows.Forms.Button
	Friend WithEvents ServerNameLabel As System.Windows.Forms.Label
	Friend WithEvents ServerNameAndInstance_TextBox As System.Windows.Forms.TextBox
	Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
	Friend WithEvents AuthTypeLabel As System.Windows.Forms.Label
	Friend WithEvents AuthenticationMode_ComboBox As System.Windows.Forms.ComboBox
	Friend WithEvents AuthUsername_TextBox As System.Windows.Forms.TextBox
	Friend WithEvents AuthUsernameLabel As System.Windows.Forms.Label
	Friend WithEvents AuthPassword_TextBox As System.Windows.Forms.TextBox
	Friend WithEvents Label2 As System.Windows.Forms.Label
	Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
	Friend WithEvents Label3 As System.Windows.Forms.Label
	Friend WithEvents ListLocalInstances_LinkLabel As System.Windows.Forms.LinkLabel
End Class
