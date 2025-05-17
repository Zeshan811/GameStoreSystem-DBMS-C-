using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace GameStoreSystem
{
    public partial class details : Form
    {
        string connectionString = "Data Source=ZESHAN\\SQLEXPRESS;Initial Catalog=gamestore;Integrated Security=True;Encrypt=False";
        string t = "";
        public details(string s,string d)
        {
            t = s;
            InitializeComponent();
            this.FormClosing += detail_FormClosing;
            pass.Text = d;
            enteron();
        }
        public details(string s)
        {
            t = s;
            InitializeComponent();
            this.FormClosing += detail_FormClosing;
        }
        private void detail_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
        private void details_Load(object sender, EventArgs e)
        {

        }

        private void logout_Click(object sender, EventArgs e)
        {
            this.Hide();
            customer form = new customer(t);
            form.FormClosed += (s, args) => this.Dispose();
            form.Show();
        }

        private void leftpanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Hide();
            customer form = new customer(t);
            form.FormClosed += (s, args) => this.Dispose();
            form.Show();

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void enteron()
        {

            string title = pass.Text.Trim();
            int qun = 0;
            if (title != "")
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT i.quantity FROM Inventory i JOIN Games g ON g.GameID = i.GameID WHERE Title COLLATE SQL_Latin1_General_CP1_CS_AS = @Title";
                    SqlCommand scmd = new SqlCommand(query, conn);
                    scmd.Parameters.AddWithValue("@Title", title);
                    conn.Open();
                    object userResult = scmd.ExecuteScalar();
                    if (userResult != null)
                    {
                        qun = Convert.ToInt32(userResult);
                    }
                    else
                    {
                        MessageBox.Show("qunatity on gametitle not found!");
                        return;
                    }
                    query = "SELECT Developer, Price, ReleaseDate, Description,Image FROM Games WHERE Title COLLATE SQL_Latin1_General_CP1_CS_AS = @Title";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Title", title);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            developer.Text = reader["Developer"].ToString();
                            price.Text = reader["Price"].ToString();
                            date.Value = Convert.ToDateTime(reader["ReleaseDate"].ToString());
                            description.Text = reader["Description"].ToString();
                            qu.Text = qun.ToString();
                            if (reader["Image"] != DBNull.Value)
                            {
                                byte[] imgBytes = (byte[])reader["Image"];
                                using (MemoryStream ms = new MemoryStream(imgBytes))
                                {
                                    pictureBox2.Image = Image.FromStream(ms);
                                }
                            }
                            else
                            {
                                pictureBox2.Image = null;
                            }

                        }
                        else
                        {
                            developer.Text = "";
                            price.Text = "";
                            date.Value=DateTime.Today ;
                            qu.Text = "";
                            description.Text = "";

                            MessageBox.Show("Game not found.");
                        }
                    }

                }
            }
            else
            {
                MessageBox.Show("Please enter a game title.");
            }
        }
        private void enter_Click(object sender, EventArgs e)
        {
            string title = pass.Text.Trim();
            int qun = 0;
            if (title != "")
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT i.quantity FROM Inventory i JOIN Games g ON g.GameID = i.GameID WHERE Title COLLATE SQL_Latin1_General_CP1_CS_AS = @Title";
                    SqlCommand scmd = new SqlCommand(query, conn);
                    scmd.Parameters.AddWithValue("@Title", title);
                    conn.Open();
                    object userResult = scmd.ExecuteScalar();
                    if (userResult != null)
                    {
                        qun= Convert.ToInt32(userResult);
                    }
                    else
                    {
                        MessageBox.Show("qunatity on gametitle not found!");
                        return;
                    }
                    query = "SELECT Developer, Price, ReleaseDate, Description,Image FROM Games WHERE Title COLLATE SQL_Latin1_General_CP1_CS_AS = @Title";
                    
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Title", title);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            developer.Text = reader["Developer"].ToString();
                            price.Text = reader["Price"].ToString();
                            date.Value = Convert.ToDateTime(reader["ReleaseDate"].ToString());
                            description.Text = reader["Description"].ToString();
                            qu.Text = qun.ToString();
                            if (reader["Image"] != DBNull.Value)
                            {
                                byte[] imgBytes = (byte[])reader["Image"];
                                using (MemoryStream ms = new MemoryStream(imgBytes))
                                {
                                    pictureBox2.Image = Image.FromStream(ms);
                                }
                            }
                            else
                            {
                                pictureBox2.Image = null;
                            }

                        }
                        else
                        {
                            developer.Text = "";
                            price.Text = "";
                            date.Value = DateTime.Today;
                            qu.Text = "";
                            description.Text = "";

                            MessageBox.Show("Game not found.");
                        }
                    }

                }
            }
            else
            {
                MessageBox.Show("Please enter a game title.");
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            string title = pass.Text.Trim();
            int gid = 0, uid = 0, qun = 0;


            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string userQuery = "SELECT id FROM employee WHERE name COLLATE SQL_Latin1_General_CP1_CI_AS = @name";
                SqlCommand userCmd = new SqlCommand(userQuery, conn);
                userCmd.Parameters.AddWithValue("@name", t);  
                object userResult = userCmd.ExecuteScalar();

                if (userResult != null)
                    uid = Convert.ToInt32(userResult);
                else
                {
                    MessageBox.Show("User not found!");
                    return;
                }

                string query = @"
            SELECT i.quantity, g.GameID 
            FROM Inventory i 
            JOIN Games g ON g.GameID = i.GameID 
            WHERE g.Title COLLATE SQL_Latin1_General_CP1_CS_AS = @Title";

                SqlCommand scmd = new SqlCommand(query, conn);
                scmd.Parameters.AddWithValue("@Title", title);
                SqlDataReader reader = scmd.ExecuteReader();

                if (reader.Read())
                {
                    qun = Convert.ToInt32(reader["quantity"]);
                    gid = Convert.ToInt32(reader["GameID"]);
                }
                else
                {
                    MessageBox.Show("Game not found or no quantity info.");
                    return;
                }
                reader.Close();

                if (qun <= 0)
                {
                    MessageBox.Show("Out of stock.");
                    return;
                }

                string updateQtyQuery = "UPDATE Inventory SET quantity = quantity - 1 WHERE GameID = @id";
                SqlCommand updateCmd = new SqlCommand(updateQtyQuery, conn);
                updateCmd.Parameters.AddWithValue("@id", gid);
                updateCmd.ExecuteNonQuery();

                qu.Text = (qun - 1).ToString();

                SqlCommand getCartCmd = new SqlCommand("SELECT cart_id FROM Cart WHERE user_id = @uid", conn);
                getCartCmd.Parameters.AddWithValue("@uid", uid);
                object cartResult = getCartCmd.ExecuteScalar();
                int cartId;

                if (cartResult == null)
                {
                    SqlCommand createCartCmd = new SqlCommand("INSERT INTO Cart (user_id) VALUES (@uid); SELECT CAST(SCOPE_IDENTITY() AS INT);", conn);
                    createCartCmd.Parameters.AddWithValue("@uid", uid);
                    cartId = Convert.ToInt32(createCartCmd.ExecuteScalar());
                }
                else
                {
                    cartId = Convert.ToInt32(cartResult);
                }

                SqlCommand checkGameCmd = new SqlCommand("SELECT quantity FROM CartGame WHERE cart_id = @cid AND game_id = @gid", conn);
                checkGameCmd.Parameters.AddWithValue("@cid", cartId);
                checkGameCmd.Parameters.AddWithValue("@gid", gid);
                object existingQty = checkGameCmd.ExecuteScalar();

                if (existingQty != null)
                {
                    int updatedQty = Convert.ToInt32(existingQty) + 1;
                    SqlCommand updateCartGameCmd = new SqlCommand("UPDATE CartGame SET quantity = @qty WHERE cart_id = @cid AND game_id = @gid", conn);
                    updateCartGameCmd.Parameters.AddWithValue("@qty", updatedQty);
                    updateCartGameCmd.Parameters.AddWithValue("@cid", cartId);
                    updateCartGameCmd.Parameters.AddWithValue("@gid", gid);
                    updateCartGameCmd.ExecuteNonQuery();
                }
                else
                {
                    SqlCommand insertGameCmd = new SqlCommand("INSERT INTO CartGame (cart_id, game_id, quantity) VALUES (@cid, @gid, 1)", conn);
                    insertGameCmd.Parameters.AddWithValue("@cid", cartId);
                    insertGameCmd.Parameters.AddWithValue("@gid", gid);
                    insertGameCmd.ExecuteNonQuery();
                }

                MessageBox.Show("Game added to cart!");
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Hide();
            cartpage form = new cartpage(t);
            form.FormClosed += (s, args) => this.Dispose();
            form.Show();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            customer form = new customer(t);
            form.FormClosed += (s, args) => this.Dispose();
            form.Show();

        }
    }
}
