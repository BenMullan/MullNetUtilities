<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
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
	<System.Diagnostics.DebuggerStepThrough()> _
	Private Sub InitializeComponent()
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainWindow))
		Me.TheMenuStrip = New System.Windows.Forms.MenuStrip()
		Me.ConnectToServerToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.ListDatabasesOnServerToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.ListAllTablesInDatabaseToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.SQLQuery_TextBox = New System.Windows.Forms.TextBox()
		Me.TheOutputDataGridView = New System.Windows.Forms.DataGridView()
		Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
		Me.TheStatusLabel = New System.Windows.Forms.ToolStripStatusLabel()
		Me.ExecuteSQLQuery_Button = New System.Windows.Forms.Button()
		Me.TheMenuStrip.SuspendLayout()
		CType(Me.TheOutputDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.StatusStrip1.SuspendLayout()
		Me.SuspendLayout()
		'
		'TheMenuStrip
		'
		Me.TheMenuStrip.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
		Me.TheMenuStrip.ImageScalingSize = New System.Drawing.Size(20, 20)
		Me.TheMenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ConnectToServerToolStripMenuItem, Me.ListDatabasesOnServerToolStripMenuItem, Me.ListAllTablesInDatabaseToolStripMenuItem})
		Me.TheMenuStrip.Location = New System.Drawing.Point(0, 0)
		Me.TheMenuStrip.Name = "TheMenuStrip"
		Me.TheMenuStrip.Padding = New System.Windows.Forms.Padding(5, 3, 5, 3)
		Me.TheMenuStrip.Size = New System.Drawing.Size(684, 30)
		Me.TheMenuStrip.TabIndex = 0
		Me.TheMenuStrip.Text = "MenuStrip1"
		'
		'ConnectToServerToolStripMenuItem
		'
		Me.ConnectToServerToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(CType(CType(218, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(216, Byte), Integer))
		Me.ConnectToServerToolStripMenuItem.Image = Global.MullNet.SystemUtilities.SQLMaster.My.Resources.Resources.network_server_database
		Me.ConnectToServerToolStripMenuItem.Name = "ConnectToServerToolStripMenuItem"
		Me.ConnectToServerToolStripMenuItem.Size = New System.Drawing.Size(142, 24)
		Me.ConnectToServerToolStripMenuItem.Text = "&Connect to Server..."
		'
		'ListDatabasesOnServerToolStripMenuItem
		'
		Me.ListDatabasesOnServerToolStripMenuItem.Image = Global.MullNet.SystemUtilities.SQLMaster.My.Resources.Resources.Run
		Me.ListDatabasesOnServerToolStripMenuItem.Name = "ListDatabasesOnServerToolStripMenuItem"
		Me.ListDatabasesOnServerToolStripMenuItem.Size = New System.Drawing.Size(180, 24)
		Me.ListDatabasesOnServerToolStripMenuItem.Text = "List all &Databases on Server"
		'
		'ListAllTablesInDatabaseToolStripMenuItem
		'
		Me.ListAllTablesInDatabaseToolStripMenuItem.Image = Global.MullNet.SystemUtilities.SQLMaster.My.Resources.Resources.Run
		Me.ListAllTablesInDatabaseToolStripMenuItem.Name = "ListAllTablesInDatabaseToolStripMenuItem"
		Me.ListAllTablesInDatabaseToolStripMenuItem.Size = New System.Drawing.Size(173, 24)
		Me.ListAllTablesInDatabaseToolStripMenuItem.Text = "List all &Tables in Database"
		'
		'SQLQuery_TextBox
		'
		Me.SQLQuery_TextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
				  Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.SQLQuery_TextBox.BackColor = System.Drawing.SystemColors.Info
		Me.SQLQuery_TextBox.Font = New System.Drawing.Font("Consolas", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.SQLQuery_TextBox.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
		Me.SQLQuery_TextBox.Location = New System.Drawing.Point(0, 29)
		Me.SQLQuery_TextBox.Name = "SQLQuery_TextBox"
		Me.SQLQuery_TextBox.Size = New System.Drawing.Size(612, 23)
		Me.SQLQuery_TextBox.TabIndex = 4
		Me.SQLQuery_TextBox.Text = "Query..."
		'
		'TheOutputDataGridView
		'
		Me.TheOutputDataGridView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
				  Or System.Windows.Forms.AnchorStyles.Left) _
				  Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.TheOutputDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
		Me.TheOutputDataGridView.Location = New System.Drawing.Point(0, 52)
		Me.TheOutputDataGridView.Name = "TheOutputDataGridView"
		Me.TheOutputDataGridView.Size = New System.Drawing.Size(684, 438)
		Me.TheOutputDataGridView.TabIndex = 6
		'
		'StatusStrip1
		'
		Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TheStatusLabel})
		Me.StatusStrip1.Location = New System.Drawing.Point(0, 490)
		Me.StatusStrip1.Name = "StatusStrip1"
		Me.StatusStrip1.Size = New System.Drawing.Size(684, 22)
		Me.StatusStrip1.TabIndex = 7
		Me.StatusStrip1.Text = "StatusStrip1"
		'
		'TheStatusLabel
		'
		Me.TheStatusLabel.Name = "TheStatusLabel"
		Me.TheStatusLabel.Size = New System.Drawing.Size(64, 17)
		Me.TheStatusLabel.Text = "Status: Idle"
		'
		'ExecuteSQLQuery_Button
		'
		Me.ExecuteSQLQuery_Button.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.ExecuteSQLQuery_Button.FlatAppearance.BorderColor = System.Drawing.SystemColors.Highlight
		Me.ExecuteSQLQuery_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat
		Me.ExecuteSQLQuery_Button.Location = New System.Drawing.Point(609, 29)
		Me.ExecuteSQLQuery_Button.Name = "ExecuteSQLQuery_Button"
		Me.ExecuteSQLQuery_Button.Size = New System.Drawing.Size(75, 23)
		Me.ExecuteSQLQuery_Button.TabIndex = 5
		Me.ExecuteSQLQuery_Button.Text = "&Execute"
		Me.ExecuteSQLQuery_Button.UseVisualStyleBackColor = True
		'
		'MainWindow
		'
		Me.AcceptButton = Me.ExecuteSQLQuery_Button
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(684, 512)
		Me.Controls.Add(Me.StatusStrip1)
		Me.Controls.Add(Me.TheOutputDataGridView)
		Me.Controls.Add(Me.ExecuteSQLQuery_Button)
		Me.Controls.Add(Me.SQLQuery_TextBox)
		Me.Controls.Add(Me.TheMenuStrip)
		Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
		Me.MainMenuStrip = Me.TheMenuStrip
		Me.MinimumSize = New System.Drawing.Size(200, 200)
		Me.Name = "MainWindow"
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
		Me.Text = "SQL Master - Not Connected"
		Me.TheMenuStrip.ResumeLayout(False)
		Me.TheMenuStrip.PerformLayout()
		CType(Me.TheOutputDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
		Me.StatusStrip1.ResumeLayout(False)
		Me.StatusStrip1.PerformLayout()
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub

	Friend WithEvents TheMenuStrip As System.Windows.Forms.MenuStrip
	Friend WithEvents ConnectToServerToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Friend WithEvents SQLQuery_TextBox As System.Windows.Forms.TextBox
	Friend WithEvents ExecuteSQLQuery_Button As System.Windows.Forms.Button
	Friend WithEvents TheOutputDataGridView As System.Windows.Forms.DataGridView
	Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
	Friend WithEvents TheStatusLabel As System.Windows.Forms.ToolStripStatusLabel
	Friend WithEvents ListDatabasesOnServerToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Friend WithEvents ListAllTablesInDatabaseToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem

End Class