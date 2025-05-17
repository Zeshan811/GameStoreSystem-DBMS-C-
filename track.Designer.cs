namespace GameStoreSystem
{
    partial class track
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblStatusMessage = new System.Windows.Forms.Label();
            this.lblDelivered = new System.Windows.Forms.Label();
            this.lblShipped = new System.Windows.Forms.Label();
            this.lblPacked = new System.Windows.Forms.Label();
            this.lbl = new System.Windows.Forms.Label();
            this.bar = new System.Windows.Forms.ProgressBar();
            this.home = new System.Windows.Forms.Button();
            this.pictureBox6 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).BeginInit();
            this.SuspendLayout();
            // 
            // lblStatusMessage
            // 
            this.lblStatusMessage.AutoSize = true;
            this.lblStatusMessage.Location = new System.Drawing.Point(80, 151);
            this.lblStatusMessage.Name = "lblStatusMessage";
            this.lblStatusMessage.Size = new System.Drawing.Size(0, 20);
            this.lblStatusMessage.TabIndex = 0;
            // 
            // lblDelivered
            // 
            this.lblDelivered.AutoSize = true;
            this.lblDelivered.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.lblDelivered.ForeColor = System.Drawing.SystemColors.Control;
            this.lblDelivered.Location = new System.Drawing.Point(602, 238);
            this.lblDelivered.Name = "lblDelivered";
            this.lblDelivered.Size = new System.Drawing.Size(149, 37);
            this.lblDelivered.TabIndex = 2;
            this.lblDelivered.Text = "Delivered";
            // 
            // lblShipped
            // 
            this.lblShipped.AutoSize = true;
            this.lblShipped.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.lblShipped.ForeColor = System.Drawing.SystemColors.Control;
            this.lblShipped.Location = new System.Drawing.Point(348, 238);
            this.lblShipped.Name = "lblShipped";
            this.lblShipped.Size = new System.Drawing.Size(134, 37);
            this.lblShipped.TabIndex = 3;
            this.lblShipped.Text = "Shipped";
            // 
            // lblPacked
            // 
            this.lblPacked.AutoSize = true;
            this.lblPacked.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.lblPacked.ForeColor = System.Drawing.SystemColors.Control;
            this.lblPacked.Location = new System.Drawing.Point(77, 238);
            this.lblPacked.Name = "lblPacked";
            this.lblPacked.Size = new System.Drawing.Size(175, 37);
            this.lblPacked.TabIndex = 4;
            this.lblPacked.Text = "Processing";
            this.lblPacked.Click += new System.EventHandler(this.label5_Click);
            // 
            // lbl
            // 
            this.lbl.AutoSize = true;
            this.lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.lbl.ForeColor = System.Drawing.SystemColors.Control;
            this.lbl.Location = new System.Drawing.Point(77, 97);
            this.lbl.Name = "lbl";
            this.lbl.Size = new System.Drawing.Size(117, 37);
            this.lbl.TabIndex = 5;
            this.lbl.Text = "Status:";
            this.lbl.Click += new System.EventHandler(this.lbl_Click);
            // 
            // bar
            // 
            this.bar.Location = new System.Drawing.Point(84, 308);
            this.bar.Name = "bar";
            this.bar.Size = new System.Drawing.Size(667, 59);
            this.bar.TabIndex = 6;
            // 
            // home
            // 
            this.home.FlatAppearance.BorderSize = 0;
            this.home.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.home.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.home.ForeColor = System.Drawing.SystemColors.Control;
            this.home.Location = new System.Drawing.Point(654, 21);
            this.home.Name = "home";
            this.home.Padding = new System.Windows.Forms.Padding(5);
            this.home.Size = new System.Drawing.Size(127, 56);
            this.home.TabIndex = 19;
            this.home.Text = "Home";
            this.home.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.home.UseVisualStyleBackColor = false;
            this.home.Click += new System.EventHandler(this.home_Click);
            // 
            // pictureBox6
            // 
            this.pictureBox6.Image = global::GameStoreSystem.Properties.Resources.home;
            this.pictureBox6.Location = new System.Drawing.Point(606, 35);
            this.pictureBox6.Name = "pictureBox6";
            this.pictureBox6.Size = new System.Drawing.Size(42, 32);
            this.pictureBox6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox6.TabIndex = 52;
            this.pictureBox6.TabStop = false;
            this.pictureBox6.Click += new System.EventHandler(this.pictureBox6_Click);
            // 
            // track
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(11)))), ((int)(((byte)(97)))));
            this.ClientSize = new System.Drawing.Size(871, 625);
            this.Controls.Add(this.pictureBox6);
            this.Controls.Add(this.home);
            this.Controls.Add(this.bar);
            this.Controls.Add(this.lbl);
            this.Controls.Add(this.lblPacked);
            this.Controls.Add(this.lblShipped);
            this.Controls.Add(this.lblDelivered);
            this.Controls.Add(this.lblStatusMessage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "track";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "track";
            this.Load += new System.EventHandler(this.track_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblStatusMessage;
        private System.Windows.Forms.Label lblDelivered;
        private System.Windows.Forms.Label lblShipped;
        private System.Windows.Forms.Label lblPacked;
        private System.Windows.Forms.Label lbl;
        private System.Windows.Forms.ProgressBar bar;
        private System.Windows.Forms.Button home;
        private System.Windows.Forms.PictureBox pictureBox6;
    }
}