using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameStoreSystem
{
    public partial class track : Form
    {
         string connStr = "Data Source=ZESHAN\\SQLEXPRESS;Initial Catalog=gamestore;Integrated Security=True;Encrypt=False";
        int s;
        public track(int d)
        {
            s = d;
            InitializeComponent();
            this.FormClosing += track_FormClosing;
        }

        private void track_FormClosing(object sender, FormClosingEventArgs e)
        {

            Application.Exit();
        }
        private void track_Load(object sender, EventArgs e)
        {
            enteron();
        }

        private void enteron()
        {



            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string query = "SELECT status FROM Orders WHERE order_id = @orderId ";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@orderId", s);

                var status = cmd.ExecuteScalar() as string;

                ResetLabels();

                if (status == null)
                {
                    lbl.Text = "Order not found or access denied.";
                    lbl.ForeColor= Color.Red;
                   
                }
                else
                {
                    lbl.Text = $"Order Status: {status}";
                    lbl.ForeColor = Color.Green;
                    bar.Visible = true;
                    switch (status.ToLower())
                    {
                        case "processing":
                            lblPacked.BackColor = Color.Gold;
                            bar.Value = 30;
                            break;
                        case "shipped":
                            lblPacked.BackColor = Color.Gold;
                            lblShipped.BackColor = Color.DeepSkyBlue;
                            bar.Value = 60;
                            break;
                        case "delivered":
                            lblPacked.BackColor = Color.Gold;
                            lblShipped.BackColor = Color.DeepSkyBlue;
                            lblDelivered.BackColor = Color.LimeGreen;
                            bar.Value = 100;
                            break;
                        case "cancelled":
                            bar.Value = 0;
                            break;
                        default:
                            lbl.Text = "Unknown status.";
                            lbl.ForeColor = Color.OrangeRed;
                            bar.Visible = false;
                            break;
                    }
                }
            }
        }



        private void ResetLabels()
        {
            lblPacked.BackColor = lblShipped.BackColor = lblDelivered.BackColor  = SystemColors.Control;
            lblPacked.ForeColor = lblShipped.ForeColor = lblDelivered.ForeColor = Color.Black;
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void home_Click(object sender, EventArgs e)
        {
            this.Hide();
            Homepage form = new Homepage();
            form.FormClosed += (s, args) => this.Dispose();
            form.Show();

        }

        private void lbl_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            this.Hide();
            Homepage form = new Homepage();
            form.FormClosed += (s, args) => this.Dispose();
            form.Show();

        }
    }
}

