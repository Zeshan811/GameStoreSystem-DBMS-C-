using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Mail;
using System.Data.SqlClient;
using System.Security.Cryptography;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Net;
using System.Threading.Tasks;
using static System.Runtime.CompilerServices.RuntimeHelpers;
using System.Security.Cryptography;
namespace GameStoreSystem
{
    public partial class signup : Form
    {
        string connectionString = "Data Source=ZESHAN\\SQLEXPRESS;Initial Catalog=gamestore;Integrated Security=True;Encrypt=False";
        public signup()
        {
            InitializeComponent();
            role.SelectedIndex = 0;
            this.FormClosing += SignupForm_FormClosing;
            label3.Visible = false;
            code.Visible = false;
        }

        private void SignupForm_FormClosing(object sender, FormClosingEventArgs e)
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
    private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
        private bool IsStrongPassword(string password)
        {
            if (password.Length < 8)
                return false;

            bool hasUpper = false;
            bool hasLower = false;
            bool hasDigit = false;
            bool hasSpecial = false;

            foreach (char c in password)
            {
                if (char.IsUpper(c)) hasUpper = true;
                else if (char.IsLower(c)) hasLower = true;
                else if (char.IsDigit(c)) hasDigit = true;
                else hasSpecial = true;
            }

            return hasUpper && hasLower && hasDigit && hasSpecial;
        }
        private bool IsValidEmail(string email)
        {
            try
            {
                MailAddress addr = new MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
        private Timer countdownTimer;
        private int remainingTime = 30;
        int verificationCode;
        private async void button1_Click(object sender, EventArgs e)
        {
            string rm = email.Text.Trim();
            string username = user1.Text.Trim();
            string password = pass.Text.Trim();
            string rol = role.Text.Trim();
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password)|| string.IsNullOrEmpty(rm))
            {
                MessageBox.Show("Please enter Username, Password,Email", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!IsValidEmail(rm))
            {
                MessageBox.Show("Invalid email address format!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!IsStrongPassword(password))
            {
                MessageBox.Show("Password must be at least 8 characters long and include uppercase, lowercase, digit, and special character.", "Weak Password", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;

            }

            string query = "SELECT COUNT(*) FROM employee WHERE name COLLATE SQL_Latin1_General_CP1_CS_AS = @name";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@name", username);

                        int userExists = (int)command.ExecuteScalar();

                        if (userExists > 0)
                        {   
                            MessageBox.Show("username also exist", "Invalid input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            query = "SELECT COUNT(*) FROM employee WHERE email COLLATE SQL_Latin1_General_CP1_CS_AS = @email";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@email", rm);

                        int userExists = (int)command.ExecuteScalar();

                        if (userExists > 0)
                        {
                            MessageBox.Show(" Another account exist From this email", "Invalid input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        

        query = "INSERT INTO employee (name, password,email,role) VALUES (@name, @password,@email,@role)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        Random rand = new Random();
                       verificationCode = rand.Next(1000, 9999);
                        try
                        {
                            MailMessage mail = new MailMessage();
                            mail.From = new MailAddress("ahmadwohra983@gmail.com");
                            mail.To.Add(rm);
                            mail.Subject = "Game Store Verification Code";
                            mail.Body = $"Hi {username},\n\nYour 4-digit verification code is: {verificationCode}\n\nPlease enter this code to complete your account creation.";
                            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                            smtp.Credentials = new NetworkCredential("ahmadwohra983@gmail.com", "zflqntjeusipuicq");
                            smtp.EnableSsl = true;
                            smtp.Send(mail);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error sending email: " + ex.Message);
                            return;
                        }
                        pass.Visible = false;
                       lab.Visible = false;
                        user1.Visible = false;
                        label.Visible = false;
                        email.Visible = false;
                        label2.Visible = false;
                        button1.Visible = false;
                        role.Visible = false;
                        label3.Visible = true;
                        code.Visible = true;
                        remainingTime = 30;
                        countdownTimer = new Timer();
                        countdownTimer.Interval = 1000;
                        countdownTimer.Tick += CountdownTimer_Tick;
                        countdownTimer.Start();
                        MessageBox.Show("Verification code sent to your email. write with in 30 sec");
                        await Task.Delay(30000);
                        if (code.Text == verificationCode.ToString())
                        {
                            MessageBox.Show("Verification successful");

                            try
                            {

                                MailMessage mail = new MailMessage();
                                mail.From = new MailAddress("ahmadwohra983@gmail.com");
                                mail.To.Add(rm);
                                mail.Subject = "Your Game Store Account Credentials";
                                mail.Body =
                                    $"Hello {username},\n\n" +
                                    "Your account has been successfully created.\n\n" +
                                    "Please save your credentials:\n" +
                                    $"Username: {username}\n" +
                                    $"Password: {password}\n\n" +
                                    "You can now log in and enjoy our services.\n\n" +
                                    "Best regards,\nGame Store Team";
                                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                                smtp.Credentials = new NetworkCredential("ahmadwohra983@gmail.com", "zflqntjeusipuicq");
                                smtp.EnableSsl = true;
                                smtp.Send(mail);
                                MessageBox.Show("Account created and credentials sent via Email");
                                pass.Visible = true;
                                lab.Visible = true;
                                user1.Visible = true;
                                label.Visible = true;
                                email.Visible = true;
                                label2.Visible = true;
                                button1.Visible = true;
                                role.Visible = true;
                                label3.Visible = false;
                                code.Visible = false;
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Error sending email: " + ex.Message);
                                 return;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Incorrect code");
                            return;
                        }
                        string hashpass = HashPassword(password);
                        command.Parameters.AddWithValue("@name", username);
                        command.Parameters.AddWithValue("@password", hashpass);
                        command.Parameters.AddWithValue("@email",rm);
                        command.Parameters.AddWithValue("@role",rol);
                        int rowsAffected = command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                    return;
                }
            }

        }

        private void CountdownTimer_Tick(object sender, EventArgs e)
        {

            if (remainingTime > 0)
            {
               
                remainingTime--;
                label3.Text = $"Time remaining: {remainingTime}s"; 
            }
            else
            {
                if (code.Text == verificationCode.ToString())
                {
                    return;
                }
                countdownTimer.Stop();
                MessageBox.Show("Verification time expired.");
                pass.Visible = true;
                lab.Visible = true;
                user1.Visible = true;
                label.Visible = true;
                email.Visible = true;
                label2.Visible = true;
                button1.Visible = true;
                role.Visible = true;

                label3.Visible = false;
                code.Visible = false;
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            login form = new login();
            form.FormClosed += (s, args) => this.Dispose(); 
            form.Show();

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void pass_TextChanged(object sender, EventArgs e)
        {

        }

        private void signup_Load(object sender, EventArgs e)
        {

        }

        private async  void  enter_Click(object sender, EventArgs e)
        {
            
       
        }

        private void code_TextChanged(object sender, EventArgs e)
        {

        }

        private void role_SelectedIndexChanged(object sender, EventArgs e)
        {

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
