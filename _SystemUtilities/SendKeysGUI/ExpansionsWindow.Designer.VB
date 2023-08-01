<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ExpansionsWindow
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
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ExpansionsWindow))
		Me.MSDocsLinkLabel = New System.Windows.Forms.LinkLabel()
		Me.ExpansionsHTMLWebBrowser = New System.Windows.Forms.WebBrowser()
		Me.SuspendLayout()
		'
		'MSDocsLinkLabel
		'
		Me.MSDocsLinkLabel.AutoSize = True
		Me.MSDocsLinkLabel.Location = New System.Drawing.Point(12, 9)
		Me.MSDocsLinkLabel.Name = "MSDocsLinkLabel"
		Me.MSDocsLinkLabel.Size = New System.Drawing.Size(417, 17)
		Me.MSDocsLinkLabel.TabIndex = 0
		Me.MSDocsLinkLabel.TabStop = True
		Me.MSDocsLinkLabel.Text = "https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.sendkeys.send"
		Me.MSDocsLinkLabel.UseCompatibleTextRendering = True
		'
		'ExpansionsHTMLWebBrowser
		'
		Me.ExpansionsHTMLWebBrowser.AllowNavigation = False
		Me.ExpansionsHTMLWebBrowser.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
				  Or System.Windows.Forms.AnchorStyles.Left) _
				  Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.ExpansionsHTMLWebBrowser.IsWebBrowserContextMenuEnabled = False
		Me.ExpansionsHTMLWebBrowser.Location = New System.Drawing.Point(12, 29)
		Me.ExpansionsHTMLWebBrowser.MinimumSize = New System.Drawing.Size(20, 20)
		Me.ExpansionsHTMLWebBrowser.Name = "ExpansionsHTMLWebBrowser"
		Me.ExpansionsHTMLWebBrowser.Size = New System.Drawing.Size(810, 421)
		Me.ExpansionsHTMLWebBrowser.TabIndex = 1
		'
		'ExpansionsWindow
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.BackColor = System.Drawing.Color.White
		Me.ClientSize = New System.Drawing.Size(834, 462)
		Me.Controls.Add(Me.ExpansionsHTMLWebBrowser)
		Me.Controls.Add(Me.MSDocsLinkLabel)
		Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
		Me.MaximizeBox = False
		Me.MinimizeBox = False
		Me.MinimumSize = New System.Drawing.Size(300, 300)
		Me.Name = "ExpansionsWindow"
		Me.ShowIcon = False
		Me.ShowInTaskbar = False
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
		Me.Text = "SendKeys Expansions"
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub
	Friend WithEvents MSDocsLinkLabel As System.Windows.Forms.LinkLabel
	Friend WithEvents ExpansionsHTMLWebBrowser As System.Windows.Forms.WebBrowser
End Class
