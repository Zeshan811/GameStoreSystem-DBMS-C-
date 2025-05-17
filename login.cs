using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace GameStoreSystem
{
    public partial class login : Form
    {
        string connectionString ="Data Source=ZESHAN\\SQLEXPRESS;Initial Catalog=gamestore;Integrated Security=True;Encrypt=False";
        public login()
        {
            InitializeComponent();
            this.FormClosing += LoginForm_FormClosing;
        }

    private void LoginForm_FormClosing(object sender, FormClosingEventArgs e)
    {
        Application.Exit();
    }

        public static string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                byte[] hashBytes = sha256.ComputeHash(passwordBytes);
                return Convert.ToBase64String(hashBytes);
            }
        }

        public static bool VerifyPassword(string enteredPassword, string storedHash)
        {
            string enteredHash = HashPassword(enteredPassword);
            return enteredHash == storedHash;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
         }

        private void user1_TextChanged(object sender, EventArgs e)
        {

        }

        private void pass_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();

            signup form = new signup();
            form.FormClosed += (s, args) => this.Dispose();
            form.Show();

        }

        private void button1_Click(object sender, EventArgs e)
        {
          
            string username = user1.Text.Trim();
            string password = pass.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both username and password.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string hashpass=HashPassword(password);
       
            string query = "SELECT role FROM employee WHERE name COLLATE SQL_Latin1_General_CP1_CS_AS = @name AND password COLLATE SQL_Latin1_General_CP1_CS_AS = @password";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@name", username);
                        command.Parameters.AddWithValue("@password", hashpass);

                        object result = command.ExecuteScalar();

                        if (result != null)
                        {
                            string role = result.ToString();

                            MessageBox.Show("Login successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            if (role == "Customer")
                            {
                                this.Hide();
                                customer form = new customer(username);
                                form.FormClosed += (s, args) => this.Dispose();
                                form.Show();
                            }
                            else
                            {
                                this.Hide();
                                admindash form = new admindash(username);
                                form.FormClosed += (s, args) => this.Dispose();
                                form.Show();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Incorrect username or password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        

    }

        private void showPassword_CheckedChanged(object sender, EventArgs e)
        {
            if (showPassword.Checked)
            {
                pass.PasswordChar = '\0';
            }
            else
            {
                pass.PasswordChar = '*';
            }

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Hide();
             Homepage form = new Homepage();
            form.FormClosed += (s, args) => this.Dispose();
            form.Show();
        }
    }
}
