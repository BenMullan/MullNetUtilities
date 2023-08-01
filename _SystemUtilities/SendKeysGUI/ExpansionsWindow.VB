Public Class ExpansionsWindow

	Private Sub LaunchMSDocs() Handles MSDocsLinkLabel.LinkClicked
		Global.System.Diagnostics.Process.Start("https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.sendkeys.send")
	End Sub

	Public Sub LoadExpansionsHTML() Handles MyBase.Shown
		Me.ExpansionsHTMLWebBrowser.DocumentText = My.Resources.ExpansionsHTML
	End Sub

End Class