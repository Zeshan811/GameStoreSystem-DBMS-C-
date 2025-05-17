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
    public partial class mngproduct : Form
    {
        string connectionString = "Data Source=ZESHAN\\SQLEXPRESS;Initial Catalog=gamestore;Integrated Security=True;Encrypt=False";
        string t;
        public mngproduct(string s)
        {
            t = s;
            InitializeComponent();
            category.SelectedIndex = 1;
            this.FormClosing += mngproduct_FormClosing;
         date.Value=DateTime.Today;
        }

        public mngproduct(string s,string d)
        {
            t = s;
            InitializeComponent();
            this.FormClosing += mngproduct_FormClosing;
            pass.Text = d;
            category.SelectedIndex = 1;
            enteron();

        }
        private void mngproduct_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            string gameTitle = pass.Text.Trim();
            string dev = developer.Text.Trim();
            string gamePrice = price.Text.Trim();
            DateTime releaseDate = date.Value;
            string desc = description.Text.Trim();
            string quantityStr = qu.Text.Trim();
            string cat = category.Text.Trim();

            if (gameTitle == "" || dev == "" || gamePrice == "" || date.Value.Date == DateTime.Today || desc == "" || quantityStr == "")
            {
                MessageBox.Show("Please fill all the fields.");
                return;
            }

            if (!int.TryParse(quantityStr, out int quantity))
            {
                MessageBox.Show("Invalid quantity.");
                return;
            }

            byte[] imageBytes = null;
            if (pictureBox1.Image != null)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    pictureBox1.Image.Save(ms, pictureBox1.Image.RawFormat);
                    imageBytes = ms.ToArray();
                }
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string checkQuery = "SELECT COUNT(*) FROM Games WHERE Title COLLATE SQL_Latin1_General_CP1_CS_AS = @Title";
                using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                {
                    checkCmd.Parameters.AddWithValue("@Title", gameTitle);
                    int exists = (int)checkCmd.ExecuteScalar();
                    if (exists > 0)
                    {
                        MessageBox.Show("Game already exists!");
                        return;
                    }
                }

                string insertGame = @"INSERT INTO Games (Title,Category, Developer, Price, ReleaseDate, Description, Image)
                              VALUES (@Title,@Category, @Developer, @Price, @ReleaseDate, @Description, @Image);
                              SELECT SCOPE_IDENTITY();"; 

                int newGameID;
                using (SqlCommand insertCmd = new SqlCommand(insertGame, conn))
                {
                    insertCmd.Parameters.AddWithValue("@Title", gameTitle);
                    insertCmd.Parameters.AddWithValue("@Category", cat);
                    insertCmd.Parameters.AddWithValue("@Developer", dev);
                    insertCmd.Parameters.AddWithValue("@Price", decimal.Parse(gamePrice));
                    insertCmd.Parameters.Add("@ReleaseDate", SqlDbType.Date).Value = releaseDate.Date;
                    insertCmd.Parameters.AddWithValue("@Description", desc);
                    SqlParameter imgParam = new SqlParameter("@Image", SqlDbType.VarBinary, -1);
                    imgParam.Value = (object)imageBytes ?? DBNull.Value; 
                    insertCmd.Parameters.Add(imgParam);

                    newGameID = Convert.ToInt32(insertCmd.ExecuteScalar());
                }

                string insertInventory = "INSERT INTO Inventory (GameID, Quantity) VALUES (@GameID, @Quantity)";
                using (SqlCommand invCmd = new SqlCommand(insertInventory, conn))
                {
                    invCmd.Parameters.AddWithValue("@GameID", newGameID);
                    invCmd.Parameters.AddWithValue("@Quantity", quantity);
                    invCmd.ExecuteNonQuery();
                }

                MessageBox.Show("Game added successfully!");
            }
        }


        private void button4_Click(object sender, EventArgs e)
        {
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    pictureBox1.Image = Image.FromFile(openFileDialog.FileName);
                    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage; 
                }
            }

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
                            date.Value = Convert.ToDateTime(reader["ReleaseDate"]);
                            description.Text = reader["Description"].ToString();
                            qu.Text = qun.ToString();
                            if (reader["Image"] != DBNull.Value)
                            {
                                byte[] imgBytes = (byte[])reader["Image"];
                                using (MemoryStream ms = new MemoryStream(imgBytes))
                                {
                                    pictureBox1.Image = Image.FromStream(ms);
                                }
                            }
                            else
                            {
                                pictureBox1.Image = null;
                            }

                        }
                        else
                        {
                            developer.Text = "";
                            price.Text = "";
                            qu.Text = "";
                            description.Text = "";
                            date.Value = DateTime.Today;
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
        private void mngproduct_Load(object sender, EventArgs e)
        {
        }

        private void logout_Click(object sender, EventArgs e)
        {
            this.Hide();
            admindash form = new admindash(t);
            form.FormClosed += (s, args) => this.Dispose();
            form.Show();

        }

        private void description_TextChanged(object sender, EventArgs e)
        {

        }

        private void enter_Click(object sender, EventArgs e)
        {
            enteron();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            {
                string title = pass.Text.Trim();

                if (string.IsNullOrWhiteSpace(title))
                {
                    MessageBox.Show("Please enter a game title.");
                    return;
                }

                DialogResult result = MessageBox.Show("Are you sure you want to delete this game?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result != DialogResult.Yes)
                {
                    return;
                }

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string getIdQuery = "SELECT GameID FROM Games WHERE Title COLLATE SQL_Latin1_General_CP1_CS_AS = @Title";
                    int gameId = -1;
                    using (SqlCommand cmd = new SqlCommand(getIdQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@Title", title);
                        object resultObj = cmd.ExecuteScalar();
                        if (resultObj == null)
                        {
                            MessageBox.Show("Game not found.");
                            return;
                        }
                        gameId = Convert.ToInt32(resultObj);
                    }
                    string deleteInventory = "DELETE FROM Inventory WHERE GameID = @GameID";
                    using (SqlCommand cmd = new SqlCommand(deleteInventory, conn))
                    {
                        cmd.Parameters.AddWithValue("@GameID", gameId);
                        cmd.ExecuteNonQuery();
                    }
                    string deletegame = "DELETE FROM CartGame WHERE game_id = @GameID";
                    using (SqlCommand cmd = new SqlCommand(deletegame, conn))
                    {
                        cmd.Parameters.AddWithValue("@GameID", gameId);
                        cmd.ExecuteNonQuery();
                    }
                    string deleteGame = "DELETE FROM Games WHERE GameID = @GameID";
                    using (SqlCommand cmd = new SqlCommand(deleteGame, conn))
                    {
                        cmd.Parameters.AddWithValue("@GameID", gameId);
                        cmd.ExecuteNonQuery();
                    }
                    MessageBox.Show("Game and related inventory deleted successfully!");
                    pictureBox1.Image = null;
                    developer.Text = "";
                    price.Text = "";
                    date.Value = DateTime.Today;
                    pass.Text = "";
                    qu.Text = "";
                    description.Text = "";

                }
            }

        }
        //private void button2_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        string title = pass.Text.Trim();
        //        string dev = developer.Text.Trim();
        //        string gamePrice = price.Text.Trim();
        //        DateTime releaseDate = date.Value;  
        //        string desc = description.Text.Trim();
        //        string quantityStr = qu.Text.Trim();

        //        if (title == "" || dev == "" || gamePrice == "" || date.Value.Date == DateTime.Today || desc == "" || quantityStr == "")
        //        {
        //            MessageBox.Show("Please fill all the fields.");
        //            return;
        //        }

        //        if (!int.TryParse(quantityStr, out int quantity))
        //        {
        //            MessageBox.Show("Invalid quantity.");
        //            return;
        //        }

        //        byte[] imageBytes = null;
        //        if (pictureBox1.Image != null)
        //        {
        //            using (MemoryStream ms = new MemoryStream())
        //            {
        //                pictureBox1.Image.Save(ms, pictureBox1.Image.RawFormat);
        //                imageBytes = ms.ToArray();
        //            }
        //        }

        //        using (SqlConnection conn = new SqlConnection(connectionString))
        //        {
        //            conn.Open();
        //            string getGameIDQuery = "SELECT GameID FROM Games WHERE Title COLLATE SQL_Latin1_General_CP1_CS_AS = @Title";
        //            int gameId = -1;
        //            using (SqlCommand cmd = new SqlCommand(getGameIDQuery, conn))
        //            {
        //                cmd.Parameters.AddWithValue("@Title", title);
        //                object result = cmd.ExecuteScalar();
        //                if (result == null)
        //                {
        //                    MessageBox.Show("Game not found.");
        //                    return;
        //                }
        //                gameId = Convert.ToInt32(result);
        //            }

        //            string updateGame = @"UPDATE Games 
        //            SET Developer = @Developer, Price = @Price, ReleaseDate = @ReleaseDate, 
        //                Description = @Description, Image = @Image ,Title=@title
        //            WHERE GameID = @GameID";
        //            using (SqlCommand cmd = new SqlCommand(updateGame, conn))
        //            {
        //                cmd.Parameters.AddWithValue("@Developer", dev);
        //                cmd.Parameters.AddWithValue("@Price", decimal.Parse(gamePrice));
        //                cmd.Parameters.Add("@ReleaseDate", SqlDbType.Date).Value = releaseDate.Date;
        //                cmd.Parameters.AddWithValue("@Description", desc);
        //                cmd.Parameters.Add("@Image", SqlDbType.VarBinary, -1).Value = (object)imageBytes ?? DBNull.Value; 
        //                cmd.Parameters.AddWithValue("@GameID", gameId);
        //                cmd.Parameters.AddWithValue("@title", title);

        //                cmd.ExecuteNonQuery();
        //            }

        //            string updateInventory = "UPDATE Inventory SET Quantity = @Quantity WHERE GameID = @GameID";
        //            using (SqlCommand cmd = new SqlCommand(updateInventory, conn))
        //            {
        //                cmd.Parameters.AddWithValue("@Quantity", quantity);
        //                cmd.Parameters.AddWithValue("@GameID", gameId);
        //                cmd.ExecuteNonQuery();
        //            }

        //            MessageBox.Show("Game updated successfully!");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("An error occurred: " + ex.Message);
        //    }
        //}
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string title = pass.Text.Trim();
                string dev = developer.Text.Trim();
                string gamePrice = price.Text.Trim();
                DateTime releaseDate = date.Value;
                string desc = description.Text.Trim();
                string quantityStr = qu.Text.Trim();

                if (title == "" || dev == "" || gamePrice == "" || date.Value.Date == DateTime.Today || desc == "" || quantityStr == "")
                {
                    MessageBox.Show("Please fill all the fields.");
                    return;
                }

                if (!int.TryParse(quantityStr, out int quantity))
                {
                    MessageBox.Show("Invalid quantity.");
                    return;
                }

                byte[] imageBytes = null;
                if (pictureBox1.Image != null)
                {
                    try
                    {
                        using (MemoryStream ms = new MemoryStream())
                        {
                            using (Bitmap bmp = new Bitmap(pictureBox1.Image))
                            {
                                bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                            }
                            imageBytes = ms.ToArray();
                        }
                    }
                    catch (Exception imgEx)
                    {
                        MessageBox.Show("Image saving error: " + imgEx.Message);
                        return;
                    }
                }

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string getGameIDQuery = "SELECT GameID FROM Games WHERE Title COLLATE SQL_Latin1_General_CP1_CS_AS = @Title";
                    int gameId = -1;
                    using (SqlCommand cmd = new SqlCommand(getGameIDQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@Title", title);
                        object result = cmd.ExecuteScalar();
                        if (result == null)
                        {
                            MessageBox.Show("Game not found.");
                            return;
                        }
                        gameId = Convert.ToInt32(result);
                    }

                    string updateGame = @"UPDATE Games 
                SET Developer = @Developer, Price = @Price, ReleaseDate = @ReleaseDate, 
                    Description = @Description, Image = @Image , Title = @title
                WHERE GameID = @GameID";
                    using (SqlCommand cmd = new SqlCommand(updateGame, conn))
                    {
                        cmd.Parameters.AddWithValue("@Developer", dev);
                        cmd.Parameters.AddWithValue("@Price", decimal.Parse(gamePrice));
                        cmd.Parameters.Add("@ReleaseDate", SqlDbType.Date).Value = releaseDate.Date;
                        cmd.Parameters.AddWithValue("@Description", desc);
                        cmd.Parameters.Add("@Image", SqlDbType.VarBinary, -1).Value = (object)imageBytes ?? DBNull.Value;
                        cmd.Parameters.AddWithValue("@GameID", gameId);
                        cmd.Parameters.AddWithValue("@title", title);

                        cmd.ExecuteNonQuery();
                    }

                    string updateInventory = "UPDATE Inventory SET Quantity = @Quantity WHERE GameID = @GameID";
                    using (SqlCommand cmd = new SqlCommand(updateInventory, conn))
                    {
                        cmd.Parameters.AddWithValue("@Quantity", quantity);
                        cmd.Parameters.AddWithValue("@GameID", gameId);
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Game updated successfully!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Hide();
            admindash form = new admindash(t);
            form.FormClosed += (s, args) => this.Dispose();
            form.Show();

        }

        private void adminup_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
