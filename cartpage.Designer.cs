namespace GameStoreSystem
{
    partial class cartpage
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label2 = new System.Windows.Forms.Label();
            this.home = new System.Windows.Forms.Button();
            this.cartGrid = new System.Windows.Forms.DataGridView();
            this.flowPanelCartPages = new System.Windows.Forms.FlowLayoutPanel();
            this.chk = new System.Windows.Forms.Button();
            this.lbl = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.pictureBox6 = new System.Windows.Forms.PictureBox();
            this.total = new System.Windows.Forms.Label();
            this.bill = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.can = new System.Windows.Forms.Button();
            this.con = new System.Windows.Forms.Button();
            this.plus = new System.Windows.Forms.Button();
            this.minus = new System.Windows.Forms.Button();
            this.txt = new System.Windows.Forms.Label();
            this.del = new System.Windows.Forms.Button();
            this.upd = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.cartGrid)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AccessibleName = "";
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label2.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(11)))), ((int)(((byte)(97)))));
            this.label2.Location = new System.Drawing.Point(-20, 170);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 24);
            this.label2.TabIndex = 45;
            // 
            // home
            // 
            this.home.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(11)))), ((int)(((byte)(97)))));
            this.home.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.home.FlatAppearance.BorderSize = 0;
            this.home.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.home.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold);
            this.home.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.home.Location = new System.Drawing.Point(1090, 18);
            this.home.Name = "home";
            this.home.Size = new System.Drawing.Size(124, 47);
            this.home.TabIndex = 47;
            this.home.Text = "Back";
            this.home.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.home.UseVisualStyleBackColor = false;
            this.home.Click += new System.EventHandler(this.home_Click);
            // 
            // cartGrid
            // 
            this.cartGrid.AllowUserToAddRows = false;
            this.cartGrid.AllowUserToResizeColumns = false;
            this.cartGrid.AllowUserToResizeRows = false;
            this.cartGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.cartGrid.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.cartGrid.BackgroundColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(11)))), ((int)(((byte)(97)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.cartGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.cartGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(11)))), ((int)(((byte)(97)))));
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.cartGrid.DefaultCellStyle = dataGridViewCellStyle5;
            this.cartGrid.EnableHeadersVisualStyles = false;
            this.cartGrid.GridColor = System.Drawing.SystemColors.Control;
            this.cartGrid.Location = new System.Drawing.Point(2, 88);
            this.cartGrid.Name = "cartGrid";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(11)))), ((int)(((byte)(97)))));
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.cartGrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.cartGrid.RowHeadersWidth = 62;
            this.cartGrid.RowTemplate.Height = 28;
            this.cartGrid.Size = new System.Drawing.Size(1254, 376);
            this.cartGrid.TabIndex = 48;
            this.cartGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.cartGrid_CellContentClick);
            // 
            // flowPanelCartPages
            // 
            this.flowPanelCartPages.BackColor = System.Drawing.SystemColors.Control;
            this.flowPanelCartPages.Location = new System.Drawing.Point(33, 15);
            this.flowPanelCartPages.Name = "flowPanelCartPages";
            this.flowPanelCartPages.Size = new System.Drawing.Size(249, 60);
            this.flowPanelCartPages.TabIndex = 49;
            this.flowPanelCartPages.Paint += new System.Windows.Forms.PaintEventHandler(this.flowPanelCartPages_Paint);
            // 
            // chk
            // 
            this.chk.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(11)))), ((int)(((byte)(97)))));
            this.chk.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.chk.FlatAppearance.BorderSize = 2;
            this.chk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chk.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.chk.Location = new System.Drawing.Point(1043, 172);
            this.chk.Name = "chk";
            this.chk.Size = new System.Drawing.Size(199, 59);
            this.chk.TabIndex = 50;
            this.chk.Text = "Check out";
            this.chk.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.chk.UseVisualStyleBackColor = false;
            this.chk.Click += new System.EventHandler(this.button1_Click);
            // 
            // lbl
            // 
            this.lbl.AutoSize = true;
            this.lbl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(11)))), ((int)(((byte)(97)))));
            this.lbl.Cursor = System.Windows.Forms.Cursors.SizeNS;
            this.lbl.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lbl.Location = new System.Drawing.Point(73, 26);
            this.lbl.Name = "lbl";
            this.lbl.Size = new System.Drawing.Size(96, 39);
            this.lbl.TabIndex = 52;
            this.lbl.Text = "Cart:";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(11)))), ((int)(((byte)(97)))));
            this.panel2.Controls.Add(this.home);
            this.panel2.Controls.Add(this.pictureBox5);
            this.panel2.Controls.Add(this.lbl);
            this.panel2.Controls.Add(this.pictureBox6);
            this.panel2.Location = new System.Drawing.Point(-1, 1);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1266, 81);
            this.panel2.TabIndex = 55;
            // 
            // pictureBox5
            // 
            this.pictureBox5.Image = global::GameStoreSystem.Properties.Resources.shopping_cart;
            this.pictureBox5.Location = new System.Drawing.Point(13, 20);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(54, 45);
            this.pictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox5.TabIndex = 53;
            this.pictureBox5.TabStop = false;
            // 
            // pictureBox6
            // 
            this.pictureBox6.Image = global::GameStoreSystem.Properties.Resources.arrow_bac1;
            this.pictureBox6.Location = new System.Drawing.Point(1025, 20);
            this.pictureBox6.Name = "pictureBox6";
            this.pictureBox6.Size = new System.Drawing.Size(59, 39);
            this.pictureBox6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox6.TabIndex = 51;
            this.pictureBox6.TabStop = false;
            this.pictureBox6.Click += new System.EventHandler(this.pictureBox6_Click);
            // 
            // total
            // 
            this.total.AutoSize = true;
            this.total.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(11)))), ((int)(((byte)(97)))));
            this.total.Cursor = System.Windows.Forms.Cursors.SizeNS;
            this.total.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.total.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.total.Location = new System.Drawing.Point(713, 32);
            this.total.Name = "total";
            this.total.Size = new System.Drawing.Size(96, 34);
            this.total.TabIndex = 56;
            this.total.Text = "Total:";
            // 
            // bill
            // 
            this.bill.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bill.Location = new System.Drawing.Point(815, 32);
            this.bill.Multiline = true;
            this.bill.Name = "bill";
            this.bill.ReadOnly = true;
            this.bill.Size = new System.Drawing.Size(213, 34);
            this.bill.TabIndex = 57;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(11)))), ((int)(((byte)(97)))));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.can);
            this.panel1.Controls.Add(this.con);
            this.panel1.Controls.Add(this.plus);
            this.panel1.Controls.Add(this.minus);
            this.panel1.Controls.Add(this.txt);
            this.panel1.Controls.Add(this.del);
            this.panel1.Controls.Add(this.upd);
            this.panel1.Controls.Add(this.total);
            this.panel1.Controls.Add(this.bill);
            this.panel1.Controls.Add(this.chk);
            this.panel1.Controls.Add(this.flowPanelCartPages);
            this.panel1.Location = new System.Drawing.Point(2, 453);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1263, 243);
            this.panel1.TabIndex = 56;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // can
            // 
            this.can.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(11)))), ((int)(((byte)(97)))));
            this.can.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.can.FlatAppearance.BorderSize = 2;
            this.can.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.can.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.can.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.can.Location = new System.Drawing.Point(1043, 172);
            this.can.Name = "can";
            this.can.Size = new System.Drawing.Size(199, 59);
            this.can.TabIndex = 64;
            this.can.Text = "Cancel";
            this.can.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.can.UseVisualStyleBackColor = false;
            this.can.Click += new System.EventHandler(this.can_Click);
            // 
            // con
            // 
            this.con.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(11)))), ((int)(((byte)(97)))));
            this.con.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.con.FlatAppearance.BorderSize = 2;
            this.con.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.con.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.con.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.con.Location = new System.Drawing.Point(1043, 78);
            this.con.Name = "con";
            this.con.Size = new System.Drawing.Size(199, 59);
            this.con.TabIndex = 63;
            this.con.Text = "Confirm";
            this.con.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.con.UseVisualStyleBackColor = false;
            this.con.Click += new System.EventHandler(this.con_Click);
            // 
            // plus
            // 
            this.plus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(11)))), ((int)(((byte)(97)))));
            this.plus.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.plus.FlatAppearance.BorderSize = 2;
            this.plus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.plus.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.plus.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.plus.Location = new System.Drawing.Point(523, 154);
            this.plus.Name = "plus";
            this.plus.Size = new System.Drawing.Size(42, 44);
            this.plus.TabIndex = 62;
            this.plus.Text = "+";
            this.plus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.plus.UseVisualStyleBackColor = false;
            this.plus.Click += new System.EventHandler(this.button5_Click);
            // 
            // minus
            // 
            this.minus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(11)))), ((int)(((byte)(97)))));
            this.minus.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.minus.FlatAppearance.BorderSize = 2;
            this.minus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.minus.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.minus.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.minus.Location = new System.Drawing.Point(422, 154);
            this.minus.Name = "minus";
            this.minus.Size = new System.Drawing.Size(42, 44);
            this.minus.TabIndex = 61;
            this.minus.Text = "-";
            this.minus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.minus.UseVisualStyleBackColor = false;
            this.minus.Click += new System.EventHandler(this.button4_Click);
            // 
            // txt
            // 
            this.txt.AutoSize = true;
            this.txt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(11)))), ((int)(((byte)(97)))));
            this.txt.Cursor = System.Windows.Forms.Cursors.SizeNS;
            this.txt.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txt.Location = new System.Drawing.Point(480, 159);
            this.txt.Name = "txt";
            this.txt.Size = new System.Drawing.Size(0, 39);
            this.txt.TabIndex = 60;
            // 
            // del
            // 
            this.del.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(11)))), ((int)(((byte)(97)))));
            this.del.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.del.FlatAppearance.BorderSize = 2;
            this.del.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.del.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.del.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.del.Location = new System.Drawing.Point(43, 147);
            this.del.Name = "del";
            this.del.Size = new System.Drawing.Size(139, 59);
            this.del.TabIndex = 59;
            this.del.Text = "Delete";
            this.del.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.del.UseVisualStyleBackColor = false;
            this.del.Click += new System.EventHandler(this.button3_Click);
            // 
            // upd
            // 
            this.upd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(11)))), ((int)(((byte)(97)))));
            this.upd.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.upd.FlatAppearance.BorderSize = 2;
            this.upd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.upd.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.upd.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.upd.Location = new System.Drawing.Point(214, 147);
            this.upd.Name = "upd";
            this.upd.Size = new System.Drawing.Size(139, 59);
            this.upd.TabIndex = 58;
            this.upd.Text = "Update";
            this.upd.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.upd.UseVisualStyleBackColor = false;
            this.upd.Click += new System.EventHandler(this.button2_Click);
            // 
            // cartpage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1260, 696);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.cartGrid);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "cartpage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "cartpage";
            this.Load += new System.EventHandler(this.cartpage_Load);
            ((System.ComponentModel.ISupportInitialize)(this.cartGrid)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button home;
        private System.Windows.Forms.DataGridView cartGrid;
        private System.Windows.Forms.FlowLayoutPanel flowPanelCartPages;
        private System.Windows.Forms.Button chk;
        private System.Windows.Forms.PictureBox pictureBox6;
        private System.Windows.Forms.Label lbl;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label total;
        private System.Windows.Forms.TextBox bill;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button del;
        private System.Windows.Forms.Button upd;
        private System.Windows.Forms.Button plus;
        private System.Windows.Forms.Button minus;
        private System.Windows.Forms.Label txt;
        private System.Windows.Forms.Button con;
        private System.Windows.Forms.Button can;
    }
}