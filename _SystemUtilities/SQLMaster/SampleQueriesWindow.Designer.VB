<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SampleQueriesWindow : Inherits System.Windows.Forms.Form

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
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SampleQueriesWindow))
		Me.InsertSelected_Button = New System.Windows.Forms.Button()
		Me.SampleQueriesListBox = New System.Windows.Forms.ListBox()
		Me.SuspendLayout()
		'
		'InsertSelected_Button
		'
		Me.InsertSelected_Button.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.InsertSelected_Button.Location = New System.Drawing.Point(12, 243)
		Me.InsertSelected_Button.Name = "InsertSelected_Button"
		Me.InsertSelected_Button.Size = New System.Drawing.Size(228, 23)
		Me.InsertSelected_Button.TabIndex = 0
		Me.InsertSelected_Button.Text = "Insert selected into Query Textbox"
		Me.InsertSelected_Button.UseVisualStyleBackColor = True
		'
		'SampleQueriesListBox
		'
		Me.SampleQueriesListBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
				  Or System.Windows.Forms.AnchorStyles.Left) _
				  Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.SampleQueriesListBox.Font = New System.Drawing.Font("Consolas", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.SampleQueriesListBox.FormattingEnabled = True
		Me.SampleQueriesListBox.Location = New System.Drawing.Point(12, 12)
		Me.SampleQueriesListBox.Name = "SampleQueriesListBox"
		Me.SampleQueriesListBox.ScrollAlwaysVisible = True
		Me.SampleQueriesListBox.Size = New System.Drawing.Size(944, 225)
		Me.SampleQueriesListBox.TabIndex = 1
		Me.SampleQueriesListBox.UseTabStops = False
		'
		'SampleQueriesWindow
		'
		Me.AcceptButton = Me.InsertSelected_Button
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(968, 272)
		Me.Controls.Add(Me.SampleQueriesListBox)
		Me.Controls.Add(Me.InsertSelected_Button)
		Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
		Me.Name = "SampleQueriesWindow"
		Me.Text = "Sample Queries"
		Me.ResumeLayout(False)

	End Sub
	Friend WithEvents InsertSelected_Button As System.Windows.Forms.Button
	Friend WithEvents SampleQueriesListBox As System.Windows.Forms.ListBox
End Class
