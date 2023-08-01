<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Loading
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
		Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
		Me.SuspendLayout()
		'
		'ProgressBar1
		'
		Me.ProgressBar1.Location = New System.Drawing.Point(12, 12)
		Me.ProgressBar1.MarqueeAnimationSpeed = 6
		Me.ProgressBar1.Name = "ProgressBar1"
		Me.ProgressBar1.Size = New System.Drawing.Size(97, 23)
		Me.ProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee
		Me.ProgressBar1.TabIndex = 0
		'
		'Deleting
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(121, 48)
		Me.ControlBox = False
		Me.Controls.Add(Me.ProgressBar1)
		Me.MaximizeBox = False
		Me.MinimizeBox = False
		Me.Name = "Deleting"
		Me.ShowIcon = False
		Me.ShowInTaskbar = False
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
		Me.Text = "Loading..."
		Me.TopMost = True
		Me.ResumeLayout(False)

	End Sub
	Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
End Class
