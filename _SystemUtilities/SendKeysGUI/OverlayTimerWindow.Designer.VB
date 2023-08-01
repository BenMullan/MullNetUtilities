<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class OverlayTimerWindow
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
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(OverlayTimerWindow))
		Me.SecondsLeftLabel = New System.Windows.Forms.Label()
		Me.InstructionLabel = New System.Windows.Forms.Label()
		Me.ActionAfterTimeLabel = New System.Windows.Forms.Label()
		Me.SuspendLayout()
		'
		'SecondsLeftLabel
		'
		Me.SecondsLeftLabel.AutoEllipsis = True
		Me.SecondsLeftLabel.AutoSize = True
		Me.SecondsLeftLabel.Font = New System.Drawing.Font("Consolas", 72.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.SecondsLeftLabel.Location = New System.Drawing.Point(150, 69)
		Me.SecondsLeftLabel.Name = "SecondsLeftLabel"
		Me.SecondsLeftLabel.Size = New System.Drawing.Size(100, 112)
		Me.SecondsLeftLabel.TabIndex = 0
		Me.SecondsLeftLabel.Text = "3"
		'
		'InstructionLabel
		'
		Me.InstructionLabel.AutoEllipsis = True
		Me.InstructionLabel.AutoSize = True
		Me.InstructionLabel.Font = New System.Drawing.Font("Consolas", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.InstructionLabel.ForeColor = System.Drawing.Color.Red
		Me.InstructionLabel.Location = New System.Drawing.Point(11, 9)
		Me.InstructionLabel.Name = "InstructionLabel"
		Me.InstructionLabel.Size = New System.Drawing.Size(378, 19)
		Me.InstructionLabel.TabIndex = 0
		Me.InstructionLabel.Text = "Position the Mouse over the Target Window"
		'
		'ActionAfterTimeLabel
		'
		Me.ActionAfterTimeLabel.AutoEllipsis = True
		Me.ActionAfterTimeLabel.AutoSize = True
		Me.ActionAfterTimeLabel.Font = New System.Drawing.Font("Consolas", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.ActionAfterTimeLabel.ForeColor = System.Drawing.SystemColors.ControlText
		Me.ActionAfterTimeLabel.Location = New System.Drawing.Point(65, 61)
		Me.ActionAfterTimeLabel.Name = "ActionAfterTimeLabel"
		Me.ActionAfterTimeLabel.Size = New System.Drawing.Size(270, 19)
		Me.ActionAfterTimeLabel.TabIndex = 0
		Me.ActionAfterTimeLabel.Text = "Capturing MousePosition in..."
		'
		'OverlayTimerWindow
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.BackColor = System.Drawing.Color.Silver
		Me.ClientSize = New System.Drawing.Size(400, 200)
		Me.ControlBox = False
		Me.Controls.Add(Me.ActionAfterTimeLabel)
		Me.Controls.Add(Me.InstructionLabel)
		Me.Controls.Add(Me.SecondsLeftLabel)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
		Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
		Me.MaximizeBox = False
		Me.MinimizeBox = False
		Me.Name = "OverlayTimerWindow"
		Me.Opacity = 0.75R
		Me.ShowIcon = False
		Me.ShowInTaskbar = False
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
		Me.Text = "OverlayTimerWindow"
		Me.TopMost = True
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub
	Friend WithEvents SecondsLeftLabel As System.Windows.Forms.Label
	Friend WithEvents InstructionLabel As System.Windows.Forms.Label
	Friend WithEvents ActionAfterTimeLabel As System.Windows.Forms.Label
End Class
