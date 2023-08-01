REM -------------------------------------
REM Ben Mullan 2019. All Rights Reserved.
REM -------------------------------------

Public Class Home

#Region "Control Declarations"
	Friend WithEvents RecTextBox As System.Windows.Forms.TextBox
	Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
	Friend WithEvents RecipientsList As System.Windows.Forms.ListBox
	Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
	Friend WithEvents Label1 As System.Windows.Forms.Label
	Friend WithEvents SubjectTextBox As System.Windows.Forms.TextBox
	Friend WithEvents BodyTextBox As System.Windows.Forms.TextBox
	Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
	Friend WithEvents SendCountLabel2 As System.Windows.Forms.Label
	Friend WithEvents Label3 As System.Windows.Forms.Label
	Friend WithEvents SendCount As System.Windows.Forms.NumericUpDown
	Friend WithEvents StartButton As System.Windows.Forms.Button
	Friend WithEvents Label5 As System.Windows.Forms.Label
	Friend WithEvents PriorityComboBox As System.Windows.Forms.ComboBox
	Friend WithEvents HTMLBodyCheckBox As System.Windows.Forms.CheckBox
	Friend WithEvents HomeMenuStrip As System.Windows.Forms.MenuStrip
	Friend WithEvents AccountToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Friend WithEvents HelpToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Friend WithEvents LinkLabel1 As System.Windows.Forms.LinkLabel
	Friend WithEvents DelayAfterEachSendNumericUpDown As System.Windows.Forms.NumericUpDown
	Friend WithEvents Label2 As System.Windows.Forms.Label
	Friend WithEvents Label4 As System.Windows.Forms.Label
	Friend WithEvents AddRecButton As System.Windows.Forms.Button
#End Region

	Private Sub InitializeComponent()
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Home))
		Me.AddRecButton = New System.Windows.Forms.Button()
		Me.RecTextBox = New System.Windows.Forms.TextBox()
		Me.GroupBox1 = New System.Windows.Forms.GroupBox()
		Me.RecipientsList = New System.Windows.Forms.ListBox()
		Me.GroupBox2 = New System.Windows.Forms.GroupBox()
		Me.BodyTextBox = New System.Windows.Forms.TextBox()
		Me.SubjectTextBox = New System.Windows.Forms.TextBox()
		Me.LinkLabel1 = New System.Windows.Forms.LinkLabel()
		Me.HTMLBodyCheckBox = New System.Windows.Forms.CheckBox()
		Me.Label1 = New System.Windows.Forms.Label()
		Me.GroupBox3 = New System.Windows.Forms.GroupBox()
		Me.Label2 = New System.Windows.Forms.Label()
		Me.DelayAfterEachSendNumericUpDown = New System.Windows.Forms.NumericUpDown()
		Me.PriorityComboBox = New System.Windows.Forms.ComboBox()
		Me.Label5 = New System.Windows.Forms.Label()
		Me.SendCount = New System.Windows.Forms.NumericUpDown()
		Me.SendCountLabel2 = New System.Windows.Forms.Label()
		Me.Label3 = New System.Windows.Forms.Label()
		Me.Label4 = New System.Windows.Forms.Label()
		Me.StartButton = New System.Windows.Forms.Button()
		Me.HomeMenuStrip = New System.Windows.Forms.MenuStrip()
		Me.AccountToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.HelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.GroupBox1.SuspendLayout()
		Me.GroupBox2.SuspendLayout()
		Me.GroupBox3.SuspendLayout()
		CType(Me.DelayAfterEachSendNumericUpDown, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.SendCount, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.HomeMenuStrip.SuspendLayout()
		Me.SuspendLayout()
		'
		'AddRecButton
		'
		Me.AddRecButton.Location = New System.Drawing.Point(198, 353)
		Me.AddRecButton.Name = "AddRecButton"
		Me.AddRecButton.Size = New System.Drawing.Size(38, 23)
		Me.AddRecButton.TabIndex = 2
		Me.AddRecButton.Text = "Add"
		Me.AddRecButton.UseVisualStyleBackColor = True
		'
		'RecTextBox
		'
		Me.RecTextBox.Location = New System.Drawing.Point(6, 355)
		Me.RecTextBox.Name = "RecTextBox"
		Me.RecTextBox.Size = New System.Drawing.Size(186, 20)
		Me.RecTextBox.TabIndex = 1
		'
		'GroupBox1
		'
		Me.GroupBox1.Controls.Add(Me.RecipientsList)
		Me.GroupBox1.Controls.Add(Me.AddRecButton)
		Me.GroupBox1.Controls.Add(Me.RecTextBox)
		Me.GroupBox1.Location = New System.Drawing.Point(12, 46)
		Me.GroupBox1.Name = "GroupBox1"
		Me.GroupBox1.Size = New System.Drawing.Size(242, 382)
		Me.GroupBox1.TabIndex = 2
		Me.GroupBox1.TabStop = False
		Me.GroupBox1.Text = "Recipients"
		'
		'RecipientsList
		'
		Me.RecipientsList.FormattingEnabled = True
		Me.RecipientsList.HorizontalScrollbar = True
		Me.RecipientsList.Location = New System.Drawing.Point(6, 17)
		Me.RecipientsList.Name = "RecipientsList"
		Me.RecipientsList.Size = New System.Drawing.Size(230, 316)
		Me.RecipientsList.TabIndex = 0
		'
		'GroupBox2
		'
		Me.GroupBox2.Controls.Add(Me.BodyTextBox)
		Me.GroupBox2.Controls.Add(Me.SubjectTextBox)
		Me.GroupBox2.Controls.Add(Me.LinkLabel1)
		Me.GroupBox2.Controls.Add(Me.HTMLBodyCheckBox)
		Me.GroupBox2.Controls.Add(Me.Label1)
		Me.GroupBox2.Location = New System.Drawing.Point(260, 46)
		Me.GroupBox2.Name = "GroupBox2"
		Me.GroupBox2.Size = New System.Drawing.Size(259, 258)
		Me.GroupBox2.TabIndex = 3
		Me.GroupBox2.TabStop = False
		Me.GroupBox2.Text = "EMail"
		'
		'BodyTextBox
		'
		Me.BodyTextBox.Location = New System.Drawing.Point(58, 51)
		Me.BodyTextBox.Multiline = True
		Me.BodyTextBox.Name = "BodyTextBox"
		Me.BodyTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both
		Me.BodyTextBox.Size = New System.Drawing.Size(195, 178)
		Me.BodyTextBox.TabIndex = 1
		'
		'SubjectTextBox
		'
		Me.SubjectTextBox.Location = New System.Drawing.Point(58, 20)
		Me.SubjectTextBox.Name = "SubjectTextBox"
		Me.SubjectTextBox.Size = New System.Drawing.Size(195, 20)
		Me.SubjectTextBox.TabIndex = 0
		'
		'LinkLabel1
		'
		Me.LinkLabel1.AutoSize = True
		Me.LinkLabel1.Location = New System.Drawing.Point(6, 54)
		Me.LinkLabel1.Name = "LinkLabel1"
		Me.LinkLabel1.Size = New System.Drawing.Size(34, 13)
		Me.LinkLabel1.TabIndex = 2
		Me.LinkLabel1.TabStop = True
		Me.LinkLabel1.Text = "Body:"
		'
		'HTMLBodyCheckBox
		'
		Me.HTMLBodyCheckBox.AutoSize = True
		Me.HTMLBodyCheckBox.Location = New System.Drawing.Point(109, 235)
		Me.HTMLBodyCheckBox.Name = "HTMLBodyCheckBox"
		Me.HTMLBodyCheckBox.Size = New System.Drawing.Size(93, 17)
		Me.HTMLBodyCheckBox.TabIndex = 3
		Me.HTMLBodyCheckBox.Text = "Body is HTML"
		Me.HTMLBodyCheckBox.UseVisualStyleBackColor = True
		'
		'Label1
		'
		Me.Label1.AutoSize = True
		Me.Label1.Location = New System.Drawing.Point(6, 23)
		Me.Label1.Name = "Label1"
		Me.Label1.Size = New System.Drawing.Size(46, 13)
		Me.Label1.TabIndex = 4
		Me.Label1.Text = "Subject:"
		'
		'GroupBox3
		'
		Me.GroupBox3.Controls.Add(Me.Label2)
		Me.GroupBox3.Controls.Add(Me.DelayAfterEachSendNumericUpDown)
		Me.GroupBox3.Controls.Add(Me.PriorityComboBox)
		Me.GroupBox3.Controls.Add(Me.Label5)
		Me.GroupBox3.Controls.Add(Me.SendCount)
		Me.GroupBox3.Controls.Add(Me.SendCountLabel2)
		Me.GroupBox3.Controls.Add(Me.Label3)
		Me.GroupBox3.Controls.Add(Me.Label4)
		Me.GroupBox3.Location = New System.Drawing.Point(260, 310)
		Me.GroupBox3.Name = "GroupBox3"
		Me.GroupBox3.Size = New System.Drawing.Size(259, 118)
		Me.GroupBox3.TabIndex = 4
		Me.GroupBox3.TabStop = False
		Me.GroupBox3.Text = "Options"
		'
		'Label2
		'
		Me.Label2.AutoSize = True
		Me.Label2.Location = New System.Drawing.Point(28, 86)
		Me.Label2.Name = "Label2"
		Me.Label2.Size = New System.Drawing.Size(116, 13)
		Me.Label2.TabIndex = 6
		Me.Label2.Text = "Delay after each Send:"
		'
		'DelayAfterEachSendNumericUpDown
		'
		Me.DelayAfterEachSendNumericUpDown.Location = New System.Drawing.Point(145, 84)
		Me.DelayAfterEachSendNumericUpDown.Maximum = New Decimal(New Integer() {100000000, 0, 0, 0})
		Me.DelayAfterEachSendNumericUpDown.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
		Me.DelayAfterEachSendNumericUpDown.Name = "DelayAfterEachSendNumericUpDown"
		Me.DelayAfterEachSendNumericUpDown.Size = New System.Drawing.Size(74, 20)
		Me.DelayAfterEachSendNumericUpDown.TabIndex = 2
		Me.DelayAfterEachSendNumericUpDown.Value = New Decimal(New Integer() {30, 0, 0, 0})
		'
		'PriorityComboBox
		'
		Me.PriorityComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.PriorityComboBox.FormattingEnabled = True
		Me.PriorityComboBox.Items.AddRange(New Object() {"High", "Normal", "Low"})
		Me.PriorityComboBox.Location = New System.Drawing.Point(98, 51)
		Me.PriorityComboBox.Name = "PriorityComboBox"
		Me.PriorityComboBox.Size = New System.Drawing.Size(121, 21)
		Me.PriorityComboBox.TabIndex = 1
		'
		'Label5
		'
		Me.Label5.AutoSize = True
		Me.Label5.Location = New System.Drawing.Point(28, 54)
		Me.Label5.Name = "Label5"
		Me.Label5.Size = New System.Drawing.Size(70, 13)
		Me.Label5.TabIndex = 5
		Me.Label5.Text = "EMail Priority:"
		'
		'SendCount
		'
		Me.SendCount.Location = New System.Drawing.Point(60, 20)
		Me.SendCount.Maximum = New Decimal(New Integer() {10000, 0, 0, 0})
		Me.SendCount.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
		Me.SendCount.Name = "SendCount"
		Me.SendCount.Size = New System.Drawing.Size(51, 20)
		Me.SendCount.TabIndex = 0
		Me.SendCount.Value = New Decimal(New Integer() {1, 0, 0, 0})
		'
		'SendCountLabel2
		'
		Me.SendCountLabel2.AutoSize = True
		Me.SendCountLabel2.Location = New System.Drawing.Point(111, 22)
		Me.SendCountLabel2.Name = "SendCountLabel2"
		Me.SendCountLabel2.Size = New System.Drawing.Size(120, 13)
		Me.SendCountLabel2.TabIndex = 4
		Me.SendCountLabel2.Text = "EMail to each Recipient"
		'
		'Label3
		'
		Me.Label3.AutoSize = True
		Me.Label3.Location = New System.Drawing.Point(28, 22)
		Me.Label3.Name = "Label3"
		Me.Label3.Size = New System.Drawing.Size(32, 13)
		Me.Label3.TabIndex = 3
		Me.Label3.Text = "Send"
		'
		'Label4
		'
		Me.Label4.AutoSize = True
		Me.Label4.Location = New System.Drawing.Point(221, 86)
		Me.Label4.Name = "Label4"
		Me.Label4.Size = New System.Drawing.Size(20, 13)
		Me.Label4.TabIndex = 7
		Me.Label4.Text = "ms"
		'
		'StartButton
		'
		Me.StartButton.Location = New System.Drawing.Point(221, 433)
		Me.StartButton.Name = "StartButton"
		Me.StartButton.Size = New System.Drawing.Size(75, 23)
		Me.StartButton.TabIndex = 0
		Me.StartButton.Text = "Start"
		Me.StartButton.UseVisualStyleBackColor = True
		'
		'HomeMenuStrip
		'
		Me.HomeMenuStrip.BackColor = System.Drawing.SystemColors.ActiveCaption
		Me.HomeMenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AccountToolStripMenuItem, Me.HelpToolStripMenuItem})
		Me.HomeMenuStrip.Location = New System.Drawing.Point(0, 0)
		Me.HomeMenuStrip.Name = "HomeMenuStrip"
		Me.HomeMenuStrip.Size = New System.Drawing.Size(529, 24)
		Me.HomeMenuStrip.TabIndex = 1
		Me.HomeMenuStrip.Text = "Menu Bar"
		'
		'AccountToolStripMenuItem
		'
		Me.AccountToolStripMenuItem.Name = "AccountToolStripMenuItem"
		Me.AccountToolStripMenuItem.Size = New System.Drawing.Size(64, 20)
		Me.AccountToolStripMenuItem.Text = "Account"
		'
		'HelpToolStripMenuItem
		'
		Me.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem"
		Me.HelpToolStripMenuItem.Size = New System.Drawing.Size(44, 20)
		Me.HelpToolStripMenuItem.Text = "Help"
		'
		'Home
		'
		Me.AcceptButton = Me.AddRecButton
		Me.ClientSize = New System.Drawing.Size(529, 462)
		Me.Controls.Add(Me.StartButton)
		Me.Controls.Add(Me.GroupBox3)
		Me.Controls.Add(Me.GroupBox2)
		Me.Controls.Add(Me.GroupBox1)
		Me.Controls.Add(Me.HomeMenuStrip)
		Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
		Me.MainMenuStrip = Me.HomeMenuStrip
		Me.MaximizeBox = False
		Me.MaximumSize = New System.Drawing.Size(545, 500)
		Me.MinimizeBox = False
		Me.MinimumSize = New System.Drawing.Size(545, 500)
		Me.Name = "Home"
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
		Me.Text = "AutoEMail"
		Me.GroupBox1.ResumeLayout(False)
		Me.GroupBox1.PerformLayout()
		Me.GroupBox2.ResumeLayout(False)
		Me.GroupBox2.PerformLayout()
		Me.GroupBox3.ResumeLayout(False)
		Me.GroupBox3.PerformLayout()
		CType(Me.DelayAfterEachSendNumericUpDown, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.SendCount, System.ComponentModel.ISupportInitialize).EndInit()
		Me.HomeMenuStrip.ResumeLayout(False)
		Me.HomeMenuStrip.PerformLayout()
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub

	Private Sub Home_Load() Handles MyBase.Load
		RecTextBox.Select()
		PriorityComboBox.SelectedIndex = 1
	End Sub

	Private Sub AddRecButton_Click() Handles AddRecButton.Click
		If RecTextBox.Text = "" Then
			MsgBox("Please specify a Recipient to add", MsgBoxStyle.Information, "Recipients")
		Else
			'Check that RecTextBox.Text matches the RegEx of an EMail
			If IsValidEMailAddress(RecTextBox.Text) Then
				RecipientsList.Items.Add(RecTextBox.Text)
			Else
				MsgBox("Please specify a valid EMail Address", MsgBoxStyle.Information, "Recipients")
			End If
			RecTextBox.Text = ""
		End If
	End Sub

	Private Sub SendCount_ValueChanged() Handles SendCount.ValueChanged
		If SendCount.Value = 1 Then
			SendCountLabel2.Text = "EMail to each Recipient"
		Else
			SendCountLabel2.Text = "EMails to each Recipient"
		End If
	End Sub

	Function IsValidEMailAddress(ByVal EmailAddress As String) As Boolean
		Dim EMailAddressRegularExpression As New System.Text.RegularExpressions.Regex("^[_a-zA-Z0-9-]+(.[a-zA-Z0-9-]+)@[a-zA-Z0-9-]+(.[a-zA-Z0-9-]+)*(.[a-zA-Z]{2,4})$")
		Return EMailAddressRegularExpression.IsMatch(EmailAddress)
	End Function

	'Public Sub Log(ByVal Text As String)
	'    SendLog.LogListBox.Items.Add(Text)
	'    SendLog.LogListBox.SelectedIndex = LogListBox.Items.Count - 1
	'    SendLog.LogListBox.SelectedIndex = -1
	'End Sub

	Public Const Default_SenderEMailAddress As String = "bob.the.builder.802.11@gmail.com"
	Public Const Default_SenderEMailPassword As String = "take the time 80:21"
	Public Const Default_SMTPServer As String = "smtp.gmail.com"
	Public Const Default_SMTPServerPort As Integer = 587 'STARTTLS/TLS: 587    SSL: 465
	Public Const Default_SMTPServerUsesSSL As Boolean = True

	Public SenderEMailAddress As String = Default_SenderEMailAddress
	Public SenderEMailPassword As String = Default_SenderEMailPassword
	Public SMTPServer As String = Default_SMTPServer
	Public SMTPServerPort As Integer = Default_SMTPServerPort 'STARTTLS/TLS: 587    SSL: 465
	Public SMTPServerUsesSSL As Boolean = Default_SMTPServerUsesSSL

	Public SMTPClient As New System.Net.Mail.SmtpClient()
	Public EMail As New System.Net.Mail.MailMessage()

	Public Function InitialiseEMail() As Boolean
		Try
			With SMTPClient
				.UseDefaultCredentials = False
				.Credentials = New Net.NetworkCredential(SenderEMailAddress, SenderEMailPassword)
				.Port = SMTPServerPort
				.EnableSsl = SMTPServerUsesSSL
				.Host = SMTPServer
			End With
			Return True
		Catch Ex As Exception
			MsgBox("Error: SMTP Server - " & Ex.Message, MsgBoxStyle.Critical, "SMTP Server Error")
			Return False
		End Try
	End Function

	Public Sub Start() Handles StartButton.Click
		If RecipientsList.Items.Count = 0 Or BodyTextBox.Text = "" Or SubjectTextBox.Text = "" Then
			MsgBox("Please specify at least one Recipient, as well as a Body and a Subject", MsgBoxStyle.Information, "AutoEmail")
		Else
			'Show the Log Form
			SendLog.Show()

			SendLog.Log("------------------------------------------------------------------ Started ------------------------------------------------------------------")
			SendLog.Log("Using Sender Account: " & SenderEMailAddress)

			'Set the MousePointer to a Wait Cursor
			Cursor = Cursors.WaitCursor

			'(Try to) Connect to the SMTP Server
			If InitialiseEMail() = True Then

				'Send the Message to everyone on the list, as many times as have been specified
				For Count As Integer = 1 To SendCount.Value Step 1
					For Each Recipient As String In RecipientsList.Items
						Try
							'Define "EMail" as a new, blank EMail Message
							EMail = New System.Net.Mail.MailMessage() With
							{.From = New System.Net.Mail.MailAddress(SenderEMailAddress),
							.Subject = SubjectTextBox.Text,
							.IsBodyHtml = HTMLBodyCheckBox.Checked,
							.Body = BodyTextBox.Text}

							'Specify the Addr. to which this EMail should be sent
							EMail.To.Add(Recipient)
							'Set the Priority of the EMail
							EMail.Priority = ResolveEMailPriority()

							'Send the EMail
							SMTPClient.Send(EMail)

							'Tell the user that the EMail has been sent
							SendLog.Log("Cycle " & Count.ToString() & " of " & SendCount.Value.ToString() & ", Sent To: " & Recipient)

							'Pause a the specified amount
							SendLog.Log("Waiting " & DelayAfterEachSendNumericUpDown.Value.ToString() & " MilliSeconds...")
							Threading.Thread.Sleep(CInt(DelayAfterEachSendNumericUpDown.Value))

						Catch Ex As Exception
							SendLog.Log("Cycle " & Count.ToString() & " of " & SendCount.Value.ToString() & ", Error: " & Recipient & " - " & Ex.Message)
						End Try
					Next
				Next
				SendLog.Log("------------------------------------------------------------------- Done -------------------------------------------------------------------")
			End If

			'Set the MousePointer back to normal
			Cursor = Cursors.Default

		End If
	End Sub

	Public Function ResolveEMailPriority() As System.Net.Mail.MailPriority
		Select Case Me.PriorityComboBox.Text
			Case "High"
				Return System.Net.Mail.MailPriority.High
			Case "Normal"
				Return Net.Mail.MailPriority.Normal
			Case "Low"
				Return Net.Mail.MailPriority.Low
			Case Else
				Return System.Net.Mail.MailPriority.Normal
		End Select
	End Function

	Private Sub AccountToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles AccountToolStripMenuItem.Click
		Account.ShowDialog()
	End Sub

	Private Sub HelpToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles HelpToolStripMenuItem.Click
		HelpAndAbout.ShowDialog()
	End Sub

	Private Sub RecipientsList_KeyDown1(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles RecipientsList.KeyDown

		Try
			If e.KeyCode = Keys.Delete Or e.KeyCode = Keys.Back Then
				RecipientsList.Items.RemoveAt(RecipientsList.SelectedIndex)
			End If
		Catch ex As Exception
		End Try

	End Sub

	Private Sub RecipientsList_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles RecipientsList.MouseDown
		Try
			If e.Button = MouseButtons.Right Then
				RecipientsList.SelectedIndex = RecipientsList.IndexFromPoint(e.X, e.Y)
				RecipientsList.Items.RemoveAt(RecipientsList.SelectedIndex)
			End If
		Catch ex As Exception
		End Try
	End Sub

	Private Sub RecTextBox_GotFocus() Handles RecTextBox.GotFocus
		Me.AcceptButton = AddRecButton
	End Sub

	Private Sub ChangeAcceptButtonToStart() Handles SubjectTextBox.GotFocus, BodyTextBox.GotFocus, SendCount.GotFocus, PriorityComboBox.GotFocus, HTMLBodyCheckBox.GotFocus
		Me.AcceptButton = StartButton
	End Sub

	Private Sub LinkLabel1_LinkClicked() Handles LinkLabel1.LinkClicked
		BodyEditor.ShowDialog()
	End Sub

End Class