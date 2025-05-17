using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameStoreSystem
{
    public partial class Homepage : Form
    {
        string connectionString = "Data Source=ZESHAN\\SQLEXPRESS;Initial Catalog=gamestore;Integrated Security=True;Encrypt=False";
        public Homepage()
        {
            InitializeComponent();
            this.FormClosing += home_FormClosing;
        }

        private void home_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void Homepage_Load(object sender, EventArgs e)
        {
            loadpick();
        }

        private void login_Click(object sender, EventArgs e)
        {
            this.Hide();
           login form = new login();
            form.FormClosed += (s, args) => this.Dispose();
            form.Show();

        }

        private void signup_Click(object sender, EventArgs e)
        {
            this.Hide();
            signup form = new signup();
            form.FormClosed += (s, args) => this.Dispose();
            form.Show();

        }

        private void lab_Click(object sender, EventArgs e)
        {

        }

        private void enter_Click(object sender, EventArgs e)
        {
            int orderId;
            if (!int.TryParse(tid.Text, out orderId))
            {
                MessageBox.Show("enter valid number");
                return;
            }
            this.Hide();
            track form = new track(orderId);
            form.FormClosed += (s, args) => this.Dispose();
            form.Show();
        }
       private void loadpick()
       {
                try
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        string query = "SELECT TOP 3 GameID, Title, Image FROM Games ORDER BY views DESC";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                box1.Image = null;
                                box2.Image = null;
                                box3.Image = null;

                                int index = 1;
                                while (reader.Read() && index <= 3)
                                {
                                    PictureBox pictureBox = index == 1 ? box1 : index == 2 ? box2 : box3;
                                    string title = reader["Title"].ToString();
                                    pictureBox.Tag = title; 

                                    if (!reader.IsDBNull(reader.GetOrdinal("Image")))
                                    {
                                        byte[] imageBytes = (byte[])reader["Image"];
                                        using (MemoryStream ms = new MemoryStream(imageBytes))
                                        {
                                            pictureBox.Image = Image.FromStream(ms);
                                        }
                                    }
                                    else
                                    {
                                        pictureBox.Image = GetPlaceholderImage();
                                    }

                                    index++;
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred while loading top games: " + ex.Message);
                }
      }

            private Image GetPlaceholderImage()
            {
                Bitmap placeholder = new Bitmap(200, 200);
                using (Graphics g = Graphics.FromImage(placeholder))
                {
                    g.Clear(Color.Gray);
                    g.DrawString("No Image", new Font("Arial", 12), Brushes.White, 50, 90);
                }
                return placeholder;
            }
        
        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }
    }
}
