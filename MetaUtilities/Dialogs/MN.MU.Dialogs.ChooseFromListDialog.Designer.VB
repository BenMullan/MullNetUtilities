Namespace MetaUtilities.Dialogs

	<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
	Partial Public Class ChooseFromListDialog(Of TEachItem) : Inherits MullNet.MetaUtilities.Dialogs.BaseDialog

		'Form overrides dispose to clean up the component list.
		<System.Diagnostics.DebuggerNonUserCode()>
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
			Me.TheDescriptionLabel = New System.Windows.Forms.Label()
			Me.TheItemsToChooseFromComboBox = New System.Windows.Forms.ComboBox()
			Me.SuspendLayout()
			'
			'TheDescriptionLabel
			'
			Me.TheDescriptionLabel.AutoSize = True
			Me.TheDescriptionLabel.Location = New System.Drawing.Point(12, 44)
			Me.TheDescriptionLabel.Name = "TheDescriptionLabel"
			Me.TheDescriptionLabel.Size = New System.Drawing.Size(80, 13)
			Me.TheDescriptionLabel.TabIndex = 1
			Me.TheDescriptionLabel.Text = "{Set-at-runtime}"
			'
			'TheItemsToChooseFromComboBox
			'
			Me.TheItemsToChooseFromComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
			Me.TheItemsToChooseFromComboBox.FormattingEnabled = True
			Me.TheItemsToChooseFromComboBox.Location = New System.Drawing.Point(12, 64)
			Me.TheItemsToChooseFromComboBox.Name = "TheItemsToChooseFromComboBox"
			Me.TheItemsToChooseFromComboBox.Size = New System.Drawing.Size(360, 21)
			Me.TheItemsToChooseFromComboBox.TabIndex = 2
			'
			'ChooseFromListDialog
			'
			Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
			Me.ClientSize = New System.Drawing.Size(384, 162)
			Me.Controls.Add(Me.TheItemsToChooseFromComboBox)
			Me.Controls.Add(Me.TheDescriptionLabel)
			Me.Name = "ChooseFromListDialog"
			Me.Text = "{Set-at-runtime}"
			Me.Controls.SetChildIndex(Me.TheDescriptionLabel, 0)
			Me.Controls.SetChildIndex(Me.TheItemsToChooseFromComboBox, 0)
			Me.ResumeLayout(False)
			Me.PerformLayout()

		End Sub

		Friend WithEvents TheDescriptionLabel As System.Windows.Forms.Label
		Friend WithEvents TheItemsToChooseFromComboBox As System.Windows.Forms.ComboBox

	End Class

End Namespace