Public Class SendLog
    Public Sub Log(ByVal Text As String)
        LogListBox.Items.Add(Text)
        LogListBox.SelectedIndex = LogListBox.Items.Count - 1
        LogListBox.SelectedIndex = -1
    End Sub

    Private Sub ClearLogToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ClearLogToolStripMenuItem.Click
        Try
            LogListBox.Items.Clear()
        Catch ex As Exception
            MsgBox("The Log could not be cleared", MsgBoxStyle.Critical, "Log Error")
        End Try
        
    End Sub

    Private Sub CopyToClipBoardToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles CopyToClipBoardToolStripMenuItem.Click
        Try
            Dim LogText As String = ""
            For Each Item As String In LogListBox.Items
                LogText &= Item & vbCrLf
            Next
            My.Computer.Clipboard.SetText(LogText, TextDataFormat.Text)
            MsgBox("Copied to ClipBoard" & vbCrLf & "Use Ctrl + V to Paste", MsgBoxStyle.Information, "Log")
        Catch ex As Exception
            MsgBox("The Log could not be copied to the ClipBoard", MsgBoxStyle.Critical, "Log Error")
        End Try
    End Sub
End Class