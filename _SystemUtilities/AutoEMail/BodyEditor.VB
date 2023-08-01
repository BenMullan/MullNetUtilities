REM -------------------------------------
REM Ben Mullan 2019. All Rights Reserved.
REM -------------------------------------

Public Class BodyEditor

	Private Sub BodyEditorIsHTMLCheckBox_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles BodyEditorIsHTMLCheckBox.CheckedChanged
		Home.HTMLBodyCheckBox.Checked = Me.BodyEditorIsHTMLCheckBox.Checked
	End Sub

	Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles OkButton1.Click
		Home.BodyTextBox.Text = Me.BodyEditorTextBox.Text
		Home.HTMLBodyCheckBox.Checked = Me.BodyEditorIsHTMLCheckBox.Checked
		Me.Close()
	End Sub

	Private Sub BodyEditor_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
		Me.BodyEditorIsHTMLCheckBox.Checked = Home.HTMLBodyCheckBox.Checked
		Me.BodyEditorTextBox.Text = Home.BodyTextBox.Text
		BodyEditorTextBox.Select()
		Me.KeyPreview = True
	End Sub

	Private Sub InsertTab() Handles InsertTabLink.Click
		Dim InsertText As String = vbTab
		Dim InsertPosition As Integer = BodyEditorTextBox.SelectionStart
		BodyEditorTextBox.Text = BodyEditorTextBox.Text.Insert(InsertPosition, InsertText)
		BodyEditorTextBox.SelectionStart = InsertPosition + InsertText.Length
		BodyEditorTextBox.Select()
	End Sub

	Sub InsertTabByShortCutKey(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
		If (e.Control AndAlso (e.KeyCode = Keys.T)) Then
			InsertTab()
		End If
	End Sub

	Sub SelectAllByShortCutKey(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
		If (e.Control AndAlso (e.KeyCode = Keys.A)) Then
			BodyEditorTextBox.SelectAll()
		End If
	End Sub

	Private Sub UseTemplateLinkLabel_LinkClicked(sender As System.Object, e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles UseTemplateLinkLabel.LinkClicked
		Me.BodyEditorTextBox.Text = My.Resources.HTML_EMail
		Me.BodyEditorIsHTMLCheckBox.Checked = True

		'Set the EMail Subject
		Home.SubjectTextBox.Text = "Complain about Bob"
	End Sub
End Class