Namespace MetaUtilities.Dialogs

	<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
	Partial Public Class BaseDialog : Inherits System.Windows.Forms.Form

		'Form overrides dispose to clean up the component list.
		<System.Diagnostics.DebuggerNonUserCode()> _
		Protected Overrides Sub Dispose(ByVal disposing As Boolean)
			Try
				If disposing AndAlso components IsNot Nothing Then components.Dispose()
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
			Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(BaseDialog))
			Me.TheOkButton = New System.Windows.Forms.Button()
			Me.TheCancelButton = New System.Windows.Forms.Button()
			Me.SuspendLayout()
			'
			'TheOkButton
			'
			Me.TheOkButton.DialogResult = System.Windows.Forms.DialogResult.OK
			Me.TheOkButton.Location = New System.Drawing.Point(216, 127)
			Me.TheOkButton.Name = "TheOkButton"
			Me.TheOkButton.Size = New System.Drawing.Size(75, 23)
			Me.TheOkButton.TabIndex = 0
			Me.TheOkButton.Text = "&Ok"
			Me.TheOkButton.UseVisualStyleBackColor = True
			'
			'TheCancelButton
			'
			Me.TheCancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
			Me.TheCancelButton.Location = New System.Drawing.Point(297, 127)
			Me.TheCancelButton.Name = "TheCancelButton"
			Me.TheCancelButton.Size = New System.Drawing.Size(75, 23)
			Me.TheCancelButton.TabIndex = 1
			Me.TheCancelButton.Text = "&Cancel"
			Me.TheCancelButton.UseVisualStyleBackColor = True
			'
			'BaseDialog
			'
			Me.AcceptButton = Me.TheOkButton
			Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
			Me.CancelButton = Me.TheCancelButton
			Me.ClientSize = New System.Drawing.Size(384, 162)
			Me.Controls.Add(Me.TheCancelButton)
			Me.Controls.Add(Me.TheOkButton)
			Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
			Me.MaximizeBox = False
			Me.MinimizeBox = False
			Me.Name = "BaseDialog"
			Me.ShowIcon = False
			Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
			Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
			Me.Text = "MullNet MetaUtilities Base Dialog"
			Me.ResumeLayout(False)

		End Sub

		Friend WithEvents TheOkButton As System.Windows.Forms.Button
		Friend WithEvents TheCancelButton As System.Windows.Forms.Button

	End Class

End Namespace