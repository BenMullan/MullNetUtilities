<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class MainWindow : Inherits System.Windows.Forms.Form

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
	<System.Diagnostics.DebuggerStepThrough()>
	Private Sub InitializeComponent()
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainWindow))
		Me.TheCameraControl = New Camera_NET.CameraControl()
		Me.SuspendLayout()
		'
		'TheCameraControl
		'
		Me.TheCameraControl.DirectShowLogFilepath = ""
		Me.TheCameraControl.Dock = System.Windows.Forms.DockStyle.Fill
		Me.TheCameraControl.Location = New System.Drawing.Point(0, 0)
		Me.TheCameraControl.Name = "TheCameraControl"
		Me.TheCameraControl.Size = New System.Drawing.Size(584, 412)
		Me.TheCameraControl.TabIndex = 0
		'
		'MainWindow
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.BackColor = System.Drawing.SystemColors.ControlDarkDark
		Me.ClientSize = New System.Drawing.Size(584, 412)
		Me.Controls.Add(Me.TheCameraControl)
		Me.Enabled = False
		Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
		Me.Name = "MainWindow"
		Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
		Me.Text = "Webcam View"
		Me.ResumeLayout(False)

	End Sub

	Friend WithEvents TheCameraControl As Camera_NET.CameraControl

End Class