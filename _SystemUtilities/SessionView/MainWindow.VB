REM To-Do:
'	- The Screenshotter.exe stops working after taking one Screenshot. See D:\Benedict\Documents\Programming\VisualBasic\Projects\InsideAgent\Screenshotter\StartupForm.VB for why.


Public Class MainWindow

	Public Const _
	 Filename_PsExec$ = "PsExec.exe",
	 Filename__RunHidden$ = "_RunHidden.exe",
	 Filename_SaveScreenshot$ = "SaveScreenshot.exe"

	Public ReadOnly Property SpecifiedWorkingDirectory As IO.DirectoryInfo
		Get
			Dim _NewDirInfo As New IO.DirectoryInfo(Me.WorkingDirectoryTextBox.Text)
			If Not _NewDirInfo.Exists Then Throw New IO.DirectoryNotFoundException("The Working-Directory does not exist: " & _NewDirInfo.FullName) Else Return _NewDirInfo
		End Get
	End Property

	Public Sub LoadUIData() Handles Me.Shown
		Try

			Me.AvaliableSessionsComboBox.Items.AddRange(Win32SessionProcurement.GetSessionsForServer(Me.TargetHostnameTextBox.Text).Select(Function(_Session As Win32Session) _Session.SessionID & ": " & _Session.Domain & "\"c & _Session.Username).ToArray())
			Me.AvaliableSessionsComboBox.SelectedIndex = 0
			Me.WorkingDirectoryTextBox.Text = Environment.CurrentDirectory
			Me.TargetHostnameTextBox.Text = My.Computer.Name

		Catch _Ex As Exception : MsgBox(_Ex.Message, MsgBoxStyle.Critical, "UI Data Initialisation") : End Try
	End Sub

	Public Sub PerformScreenshotProcurementProcess() Handles TakeScreenshotButton.Click
		Try

			Me.Enabled = False : Me.Refresh()

			REM Extract PsExec.exe and Screenshotter.exe to {WorkingDirectory}
			IO.File.WriteAllBytes(IO.Path.Combine(Me.SpecifiedWorkingDirectory.FullName, Filename_PsExec$), My.Resources.PsExec)
			IO.File.WriteAllBytes(IO.Path.Combine(Me.SpecifiedWorkingDirectory.FullName, Filename__RunHidden), My.Resources._RunHidden)
			IO.File.WriteAllBytes(IO.Path.Combine(Me.SpecifiedWorkingDirectory.FullName, Filename_SaveScreenshot), My.Resources.SaveScreenshot)

			REM E.g. X:\WorkingDir\MNLT01_Session1.PNG
			Dim _OutputPNGFile As New IO.FileInfo(String.Format("{1}\{2}_Session{0}.PNG", Me.AvaliableSessionsComboBox.Text.Split(":"c).First(), Me.SpecifiedWorkingDirectory.FullName, Me.TargetHostnameTextBox.Text))

			REM Run for Local Computer: {WorkingDir}\PsExec.exe -i {TargetSession} "{WorkingDirectory}\_RunHidden.exe" {WorkingDirectory}\SaveScreenshot.exe /Single /OutputFile:"{WorkingDir}\{Hostname}_{TargetSession}.PNG"
			Dim _PsExecProcess As New Process()
			_PsExecProcess.StartInfo.FileName = IO.Path.Combine(Me.SpecifiedWorkingDirectory.FullName, Filename_PsExec$)
			_PsExecProcess.StartInfo.Arguments = String.Format("-i {0} ""{1}\{3}"" ""{1}\{4}"" /Single /OutputFile:""{2}""", Me.AvaliableSessionsComboBox.Text.Split(":"c).ElementAt(0), Me.SpecifiedWorkingDirectory.FullName, _OutputPNGFile.FullName, Filename__RunHidden, Filename_SaveScreenshot)
			_PsExecProcess.StartInfo.LoadUserProfile = False
			_PsExecProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden

			Dim _PsExecProcess_TextOutput$ = MullNet.MetaUtilities.Processes.RunProcess_AndReturnOutput(_PsExecProcess)

			If Not _PsExecProcess.ExitCode = 0 Then
				My.Computer.Clipboard.SetText(_PsExecProcess.StartInfo.FileName & " "c & _PsExecProcess.StartInfo.Arguments)
				Throw New Exception("PsExec did not run successfully. The Exit-Code was: " & _PsExecProcess.ExitCode.ToString() & "." & vbCrLf & vbCrLf & "The PsExec Output was:" & vbCrLf & "-"c.Repeat(30) & vbCrLf & _PsExecProcess_TextOutput.Replace(vbCrLf & vbCrLf, vbCrLf) & "-"c.Repeat(30) & vbCrLf & vbCrLf & vbCrLf & "It ran for " & (_PsExecProcess.ExitTime - _PsExecProcess.StartTime).TotalSeconds.ToString() & " seconds." & vbCrLf & vbCrLf & "The Command-line arguments used have been copied to the ClipBoard: " & _PsExecProcess.StartInfo.FileName & " "c & _PsExecProcess.StartInfo.Arguments)
			End If

			REM Display the saved image in the PictureBox
			If Not _OutputPNGFile.Exists Then Throw New IO.FileNotFoundException("The Output PNG file was not found, after calling PSEXEC", _OutputPNGFile.FullName)
			Me.ScreenshotPictureBox.Image = Drawing.Image.FromStream(New IO.MemoryStream(IO.File.ReadAllBytes(_OutputPNGFile.FullName)))

			Me.Enabled = True : Me.Refresh() : Me.Activate()

		Catch _Ex As Exception : MsgBox(_Ex.Message, MsgBoxStyle.Critical, "Screenshot Procurement") : End Try
	End Sub

	Public Sub BrowseForWorkingDirectory() Handles BrowseForWorkingDirectoryButton.Click
		Dim _FolderDialog As New Windows.Forms.FolderBrowserDialog() With {.Description = "Browse For Working-Directory..."}
		If _FolderDialog.ShowDialog() = Windows.Forms.DialogResult.OK Then Me.WorkingDirectoryTextBox.Text = _FolderDialog.SelectedPath
	End Sub

	Public Sub OpenWorkingDirectory() Handles OpenWorkingDirectoryLinkLabel.LinkClicked
		Try
			Process.Start("EXPLORER.EXE", Me.SpecifiedWorkingDirectory.FullName)
		Catch _Ex As Exception : MsgBox(_Ex.Message, MsgBoxStyle.Critical, "Open Working Directory") : End Try
	End Sub

End Class

REM Functions
'	- Extract psexec.exe, and Screenshot.exe, to the WorkingDirectory
'	- Run for Other Computer: {WorkingDir}\PsExec.exe \\{Hostname} -u {Username} -p {Password} -i {TargetSession} \\{IPAddr.}\C$\...\{WorkingDirectory}\_RunHidden.exe \\{IPAddr.}\C$\...\SaveScreenshot.exe /Single /OutputFile:"\\{IPAddr.}\C$\...\{Hostname}_{TargetSession}.PNG"
'	- Run for Local Computer: {WorkingDir}\PsExec.exe -i {TargetSession} {WorkingDirectory}\_RunHidden.exe {WorkingDirectory}\SaveScreenshot.exe /Single /OutputFile:"{WorkingDir}\{Hostname}_{TargetSession}.PNG"
'	- Read-in and display the saved image in the PictureBox