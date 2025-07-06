namespace SmartcardAppLaunch {

    partial class Form1 {

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && ( components != null )) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.InsertSmartcard_IconPictureBox = new System.Windows.Forms.PictureBox();
            this.InsertSmartcard_InfoLabel = new System.Windows.Forms.Label();
            this.CardholderName_Label = new System.Windows.Forms.Label();
            this.LaunchingApp_ProgressBar = new System.Windows.Forms.ProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.InsertSmartcard_IconPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // InsertSmartcard_IconPictureBox
            // 
            this.InsertSmartcard_IconPictureBox.Image = global::SmartcardAppLaunch.Properties.Resources.smartcard_reader_0;
            this.InsertSmartcard_IconPictureBox.Location = new System.Drawing.Point(9, 7);
            this.InsertSmartcard_IconPictureBox.Name = "InsertSmartcard_IconPictureBox";
            this.InsertSmartcard_IconPictureBox.Size = new System.Drawing.Size(69, 64);
            this.InsertSmartcard_IconPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.InsertSmartcard_IconPictureBox.TabIndex = 0;
            this.InsertSmartcard_IconPictureBox.TabStop = false;
            // 
            // InsertSmartcard_InfoLabel
            // 
            this.InsertSmartcard_InfoLabel.AutoSize = true;
            this.InsertSmartcard_InfoLabel.Location = new System.Drawing.Point(81, 29);
            this.InsertSmartcard_InfoLabel.Name = "InsertSmartcard_InfoLabel";
            this.InsertSmartcard_InfoLabel.Size = new System.Drawing.Size(190, 13);
            this.InsertSmartcard_InfoLabel.TabIndex = 1;
            this.InsertSmartcard_InfoLabel.Text = "Please insert a smartcard to continue...";
            // 
            // CardholderName_Label
            // 
            this.CardholderName_Label.AutoSize = true;
            this.CardholderName_Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CardholderName_Label.Location = new System.Drawing.Point(82, 54);
            this.CardholderName_Label.Name = "CardholderName_Label";
            this.CardholderName_Label.Size = new System.Drawing.Size(95, 13);
            this.CardholderName_Label.TabIndex = 1;
            this.CardholderName_Label.Text = "(No cardholder)";
            this.CardholderName_Label.Visible = false;
            // 
            // LaunchingApp_ProgressBar
            // 
            this.LaunchingApp_ProgressBar.Location = new System.Drawing.Point(12, 22);
            this.LaunchingApp_ProgressBar.MarqueeAnimationSpeed = 5;
            this.LaunchingApp_ProgressBar.Name = "LaunchingApp_ProgressBar";
            this.LaunchingApp_ProgressBar.Size = new System.Drawing.Size(260, 21);
            this.LaunchingApp_ProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.LaunchingApp_ProgressBar.TabIndex = 2;
            this.LaunchingApp_ProgressBar.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 81);
            this.ControlBox = false;
            this.Controls.Add(this.LaunchingApp_ProgressBar);
            this.Controls.Add(this.CardholderName_Label);
            this.Controls.Add(this.InsertSmartcard_InfoLabel);
            this.Controls.Add(this.InsertSmartcard_IconPictureBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Insert Smartcard";
            this.TopMost = true;
            this.Shown += new System.EventHandler(this.Form1_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.InsertSmartcard_IconPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

		#endregion

		private System.Windows.Forms.PictureBox InsertSmartcard_IconPictureBox;
		private System.Windows.Forms.Label InsertSmartcard_InfoLabel;
		private System.Windows.Forms.Label CardholderName_Label;
		private System.Windows.Forms.ProgressBar LaunchingApp_ProgressBar;
	}

}