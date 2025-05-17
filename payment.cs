using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace GameStoreSystem
{
    public partial class payment : Form
    {
        string connectionString = "Data Source=ZESHAN\\SQLEXPRESS;Initial Catalog=gamestore;Integrated Security=True;Encrypt=False";
        int id;
        string user;
        public payment(int i,string u)
        {
            id = i;
            user = u;
            InitializeComponent();
            this.FormClosing += payment_FormClosing;
            chk.SelectedIndex = 0;
        }
        private void SendOrderConfirmationEmail()
        {
            string email = "";

            if (!int.TryParse(uid.Text, out int userId))
            {
                MessageBox.Show("Invalid User ID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT email FROM employee WHERE id = @uid", connection))
                    {
                        cmd.Parameters.AddWithValue("@uid", userId);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                email = reader["email"].ToString();
                            }
                        }
                    }

                    if (!string.IsNullOrEmpty(email))
                    {
                        MailMessage mail = new MailMessage();
                        mail.From = new MailAddress("ahmadwohra983@gmail.com");
                        mail.To.Add(email);
                        mail.Subject = "Your Order Has Been Placed Successfully";
                        mail.Body =
                            $"Dear Customer (User ID: {uid.Text}),\n\n" +
                            $"Your order has been successfully placed.\n\n" +
                            $"Order Details:\n" +
                            $"Order ID      : {oid.Text}\n" +
                            $"Order Date    : {odte.Text}\n" +
                            $"Status        : {stat.Text}\n" +
                            $"Total Amount  : {tot.Text}\n\n" +
                            $"You can track your order using Order ID: {oid.Text}.\n\n" +
                            $"Thank you for shopping with us!\n" +
                            $"- Game Store Team";

                        SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                        smtp.Credentials = new NetworkCredential("ahmadwohra983@gmail.com", "zflqntjeusipuicq"); 
                        smtp.EnableSsl = true;

                        smtp.Send(mail);

                        MessageBox.Show("Check your email. for order Details", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Email not found for this User ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void payment_Load(object sender, EventArgs e)
        {
            Loadpayment();
        }
        private void Loadpayment()
        {


            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT user_id,order_date,total_amount,  status FROM Orders WHERE order_id = @id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read()) 
                {
                    oid.Text = id.ToString();
                    uid.Text = reader["user_id"].ToString();
                    odte.Text = reader["order_date"].ToString();
                    tot.Text = reader["total_amount"].ToString();
                    stat.Text = reader["status"].ToString();

                }
                else
                {
                    MessageBox.Show("No order found with this ID."+id, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }

        }
    private void payment_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void flowLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void method_Click(object sender, EventArgs e)
        {

        }

        private void chk_SelectedIndexChanged(object sender, EventArgs e)
        {
            met.Text = chk.Text;
        }

        private void total_TextChanged(object sender, EventArgs e)
        {

        }

        private void con_Click(object sender, EventArgs e)
        {
            if (!decimal.TryParse(tot.Text, out decimal amount) || amount <= 0)
            {
                MessageBox.Show("Invalid amount. Please check the total amount.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (met.Text != "")
            {
                string query = @"INSERT INTO Payments (order_id, payment_method, payment_status ,Amount)
                         VALUES (@OrderId, @Method, 'Completed', @amount)";

                try
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@OrderId", Convert.ToInt32(oid.Text));
                        cmd.Parameters.AddWithValue("@Method", met.Text);
                        cmd.Parameters.AddWithValue("@amount", amount);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                    MessageBox.Show("Payment Successful! Your order is placed.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    SendOrderConfirmationEmail();


                    this.Hide();

                    customer form = new customer(user);  
                    form.FormClosed += (s, args) => this.Dispose();  
                    form.Show();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select a payment method before confirming.", "Missing Payment Method", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "UPDATE Orders SET status = @status WHERE order_id = @order_id";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@status", "Cancelled");
                cmd.Parameters.AddWithValue("@order_id", id); 
                try
                {
                   conn.Open();
                  int rowsAffected=  cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Your order is cancelled", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Order ID not found or no changes made", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                this.Hide();
                customer form = new customer(user);
                form.FormClosed += (s, args) => this.Dispose();
                form.Show();
            }
        }

        private void odte_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
