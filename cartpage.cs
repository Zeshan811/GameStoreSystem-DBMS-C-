using GameStoreSystem;
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

namespace GameStoreSystem
{
    public partial class cartpage : Form
    {
            string connectionString = "Data Source=ZESHAN\\SQLEXPRESS;Initial Catalog=gamestore;Integrated Security=True;Encrypt=False";
        DataTable cartData = new DataTable();
        int cartPageSize = 10;
        int currentCartPage = 1;
        int currentUserId;
        int selectedGameId = -1;
        int selectedQuantity = 0;
        int sel = 0;
        string username;
        public cartpage(string user)
        {
            InitializeComponent();
            username = user;
            lbl.Text = "Cart:";
            con.Visible = false;
            can.Visible = false;
            currentUserId = GetUserIdFromUsername(username);
            this.cartGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.cartGrid_CellClick);
            this.FormClosing += cart_FormClosing;
        
        }
        private void UpdateBillLabel()
        {
            decimal grandTotal = 0;

            foreach (DataGridViewRow row in cartGrid.Rows)
            {
                if (row.Cells["TotalPrice"].Value != null && row.Cells["TotalPrice"].Value != DBNull.Value)
                {
                    grandTotal += Convert.ToDecimal(row.Cells["TotalPrice"].Value);
                }
            }

            bill.Text = grandTotal.ToString();
        }

        private void cartpage_Load(object sender, EventArgs e)
        {

            LoadUserCart();

        }
        private void cart_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
        private void home_Click(object sender, EventArgs e)
        {
            this.Hide();
            customer form = new customer(username);
            form.FormClosed += (s, args) => this.Dispose();
            form.Show();
        }
        private int GetUserIdFromUsername(string username)
        {
            int userId = -1;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT id FROM employee WHERE name = @username";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@username", username);

                object result = cmd.ExecuteScalar();
                if (result != null)
                {
                    userId = Convert.ToInt32(result);
                }
            }

            return userId;
        }

        private void LoadUserCart()
        {
            cartData.Clear();
            cartGrid.DataSource = null;
            cartGrid.Rows.Clear(); 

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
                 SELECT g.Image, g.Title, gc.Quantity, g.Price
                FROM CartGame gc
                JOIN Cart c ON gc.cart_id = c.cart_id
                JOIN Games g ON gc.game_id = g.GameID
                WHERE c.user_id = @uid";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@uid", currentUserId);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                cartData.Clear();
                adapter.Fill(cartData);

                flowPanelCartPages.Controls.Clear(); 
                if (cartData.Rows.Count == 0)
                {
                    cartGrid.DataSource = null;
                    cartGrid.Visible = false;
                    flowPanelCartPages.Visible = false;
                    lbl.Visible=true;
                    lbl.Text = "Cart is empty!";
                    
                    if (lbl.Text == "Cart is empty!")
                    {
                        con.Visible = false;
                        can.Visible = false;
                        chk.Visible = true;
                        minus.Visible = false;
                        plus.Visible = false;
                        del.Visible = false;
                        upd.Visible = false;
                        bill.Text = "";
                    }

                }
                else
                {
                    if (!cartData.Columns.Contains("TotalPrice"))
                        cartData.Columns.Add("TotalPrice", typeof(decimal), "Price * Quantity");

                    cartGrid.Visible = true;
                    flowPanelCartPages.Visible = true;
                    flowPanelCartPages.WrapContents = true;
                    flowPanelCartPages.AutoSize = true;
                    flowPanelCartPages.AutoSizeMode = AutoSizeMode.GrowAndShrink;
                    currentCartPage = 1;
                    LoadCartPage();
                    CreateCartPaginationButtons();
                }
            }
        }

        private void LoadCartPage()
        {
            DataTable pageTable = cartData.Clone();
            int start = (currentCartPage - 1) * cartPageSize;
            int end = Math.Min(start + cartPageSize, cartData.Rows.Count);

            for (int i = start; i < end; i++)
            {
                pageTable.ImportRow(cartData.Rows[i]);
            }

            cartGrid.Columns.Clear();
            cartGrid.DataSource = pageTable;
            UpdateBillLabel();
            FormatCartGrid();
        }
        private void FormatCartGrid()
        {
            cartGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            cartGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            cartGrid.RowTemplate.Height = 100;
            cartGrid.ReadOnly = true;

            if (cartGrid.Columns.Contains("Image"))
                cartGrid.Columns.Remove("Image");

            DataGridViewImageColumn imgCol = new DataGridViewImageColumn();
            imgCol.Name = "Image";
            imgCol.HeaderText = "Game Image";
            imgCol.ImageLayout = DataGridViewImageCellLayout.Stretch;
            cartGrid.Columns.Insert(0, imgCol);

            foreach (DataGridViewRow row in cartGrid.Rows)
            {
                if (row.DataBoundItem is DataRowView drv && drv["Image"] != DBNull.Value)
                {
                    byte[] imgData = (byte[])drv["Image"];
                    using (MemoryStream ms = new MemoryStream(imgData))
                    {
                        Image original = Image.FromStream(ms);
                        Image resized = ResizeImage(original, 100, 100);
                        row.Cells["Image"].Value = resized;
                    }
                }
            }
        }
        private Image ResizeImage(Image img, int width, int height)
        {
            Bitmap bmp = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.DrawImage(img, 0, 0, width, height);
            }
            return bmp;
        }

        private void CreateCartPaginationButtons()
        {
            flowPanelCartPages.Controls.Clear();
            int pageCount = (int)Math.Ceiling((double)cartData.Rows.Count / cartPageSize);

            for (int i = 1; i <= pageCount; i++)
            {
                Button btn = new Button
                {
                    Text = i.ToString(),
                    Tag = i,
                    Width = 40,
                    Height = 30
                };

                if (i == currentCartPage)
                    btn.BackColor = Color.LightBlue;

                btn.Click += (s, e) =>
                {
                    currentCartPage = (int)((Button)s).Tag;
                    LoadCartPage();
                    CreateCartPaginationButtons();
                };

                flowPanelCartPages.Controls.Add(btn);
            }
        }


    
        private void button1_Click(object sender, EventArgs e)
        {
            if(lbl.Text!= "Cart is empty!")
            {
                lbl.Text = "Order summary";
                chk.Visible = false;
                txt.Visible = false;
                minus.Visible = false;
                plus.Visible = false;
                del.Visible = false;
                upd.Visible = false;
                can.Visible = true;
                con.Visible = true;
            }
        }

        private void flowPanelCartPages_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cartGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void cartGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0)
            {
                panel1.Visible = true;
                DataGridViewRow row = cartGrid.Rows[e.RowIndex];

                if (row.DataBoundItem is DataRowView drv)
                {
                    string gameTitle = drv["Title"].ToString();
                    selectedQuantity = Convert.ToInt32(drv["Quantity"]);
                    txt.Text = selectedQuantity.ToString();
                    sel = selectedQuantity;
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand("SELECT GameID FROM Games WHERE Title = @title", conn);
                        cmd.Parameters.AddWithValue("@title", gameTitle);
                        object result = cmd.ExecuteScalar();
                        if (result != null)
                            selectedGameId = Convert.ToInt32(result);
                    }
                }
            }
        }
        

        private void button2_Click(object sender, EventArgs e)
        {
            
                if (selectedGameId == -1) return;
                 int newQuantity = Convert.ToInt32(txt.Text) ;
            if(newQuantity<=0)
            {
                MessageBox.Show("Enter valid units ");
                return;

            }
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                    conn.Open();
                SqlCommand stockCmd = new SqlCommand("SELECT Quantity FROM Inventory WHERE GameID = @gid", conn);
                stockCmd.Parameters.AddWithValue("@gid", selectedGameId);
                object stockResult = stockCmd.ExecuteScalar();
                if (stockResult == null)
                {
                    MessageBox.Show("Game not found.");
                    return;
                }

                int availableStock = Convert.ToInt32(stockResult);

                if (newQuantity > availableStock)
                {
                    MessageBox.Show($"Only {availableStock} units available.");
                    return;
                }
                SqlCommand cmd = new SqlCommand(@"
                UPDATE CartGame
                  SET Quantity = @qty
                  WHERE cart_id = (SELECT cart_id FROM Cart WHERE user_id = @uid)
                AND game_id = @gid", conn);

                    cmd.Parameters.AddWithValue("@qty", newQuantity);
                    cmd.Parameters.AddWithValue("@uid", currentUserId);
                    cmd.Parameters.AddWithValue("@gid", selectedGameId);
                MessageBox.Show(" Quantity updated.");
                int sub = newQuantity - sel;
                cmd.ExecuteNonQuery();
                string updateQtyQuery = "UPDATE Inventory SET Quantity = Quantity - @quan WHERE GameID = @id";
                SqlCommand updateCmd = new SqlCommand(updateQtyQuery, conn);
                updateCmd.Parameters.AddWithValue("@quan", sub); 
                updateCmd.Parameters.AddWithValue("@id", selectedGameId);
                updateCmd.ExecuteNonQuery();


            }

            LoadUserCart();
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            selectedQuantity++;
            txt.Text = selectedQuantity.ToString();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            selectedQuantity--;
            txt.Text = selectedQuantity.ToString();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            {
                if (selectedGameId == -1) return;

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlTransaction transaction = conn.BeginTransaction();

                    try
                    {
                        SqlCommand getCartIdCmd = new SqlCommand("SELECT cart_id FROM Cart WHERE user_id = @uid", conn, transaction);
                        getCartIdCmd.Parameters.AddWithValue("@uid", currentUserId);
                        object cartIdObj = getCartIdCmd.ExecuteScalar();
                        if (cartIdObj == null) return;
                        int cartId = Convert.ToInt32(cartIdObj);

                        SqlCommand getQtyCmd = new SqlCommand("SELECT Quantity FROM CartGame WHERE cart_id = @cid AND game_id = @gid", conn, transaction);
                        getQtyCmd.Parameters.AddWithValue("@cid", cartId);
                        getQtyCmd.Parameters.AddWithValue("@gid", selectedGameId);
                        object qtyObj = getQtyCmd.ExecuteScalar();
                        if (qtyObj == null) return;
                        int cartQuantity = Convert.ToInt32(qtyObj);

                        SqlCommand updateGameQtyCmd = new SqlCommand("UPDATE Inventory SET Quantity = Quantity + @qty WHERE GameID = @gid", conn, transaction);
                        updateGameQtyCmd.Parameters.AddWithValue("@qty", cartQuantity);
                        updateGameQtyCmd.Parameters.AddWithValue("@gid", selectedGameId);
                        updateGameQtyCmd.ExecuteNonQuery();

                        SqlCommand deleteCmd = new SqlCommand("DELETE FROM CartGame WHERE cart_id = @cid AND game_id = @gid", conn, transaction);
                        deleteCmd.Parameters.AddWithValue("@cid", cartId);
                        deleteCmd.Parameters.AddWithValue("@gid", selectedGameId);
                        deleteCmd.ExecuteNonQuery();

                        transaction.Commit();

                        MessageBox.Show("Game removed from cart and quantity updated.");
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }

                selectedGameId = -1;
                txt.Text = "";
                UpdateBillLabel();
                LoadUserCart(); 
            }

        }

        private void con_Click(object sender, EventArgs e)
        {
            int orderId;
            string insertOrderQuery = @"INSERT INTO Orders (user_id, total_amount, status)
                            VALUES (@UserId, @Total, 'Processing');
                            SELECT SCOPE_IDENTITY();";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(insertOrderQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId",currentUserId);
                    cmd.Parameters.AddWithValue("@Total", Convert.ToDecimal(bill.Text.Trim()));
                    conn.Open();
                    orderId = Convert.ToInt32(cmd.ExecuteScalar());
                }
                delcart();
                MessageBox.Show("Your order is confirmed!");

                this.Hide();
                payment form = new  payment(orderId,username);
                form.FormClosed += (s, args) => this.Dispose();
                form.Show();


            }
        }
        private void delcart()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    SqlCommand getCartIdCmd = new SqlCommand("SELECT cart_id FROM Cart WHERE user_id = @uid", conn, transaction);
                    getCartIdCmd.Parameters.AddWithValue("@uid", currentUserId);
                    object cartIdObj = getCartIdCmd.ExecuteScalar();
                    if (cartIdObj == null) return;
                    int cartId = Convert.ToInt32(cartIdObj);

                    SqlCommand getGamesCmd = new SqlCommand("SELECT game_id, Quantity FROM CartGame WHERE cart_id = @cid", conn, transaction);
                    getGamesCmd.Parameters.AddWithValue("@cid", cartId);
                    SqlDataReader reader = getGamesCmd.ExecuteReader();

                    List<(int gameId, int quantity)> gamesToUpdate = new List<(int, int)>();
                    while (reader.Read())
                    {
                        int gameId = Convert.ToInt32(reader["game_id"]);
                        int quantity = Convert.ToInt32(reader["Quantity"]);
                        gamesToUpdate.Add((gameId, quantity));
                    }
                    reader.Close();
                    SqlCommand deleteAllCmd = new SqlCommand("DELETE FROM CartGame WHERE cart_id = @cid", conn, transaction);
                    deleteAllCmd.Parameters.AddWithValue("@cid", cartId);
                    deleteAllCmd.ExecuteNonQuery();

                    SqlCommand deleteCartCmd = new SqlCommand("DELETE FROM Cart WHERE cart_id = @cid", conn, transaction);
                    deleteCartCmd.Parameters.AddWithValue("@cid", cartId);
                    deleteCartCmd.ExecuteNonQuery();

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show("Error: " + ex.Message);
                }
            }

            selectedGameId = -1;
            txt.Text = "";
            UpdateBillLabel();
            LoadUserCart();

        }
        private void can_Click(object sender, EventArgs e)
        {

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    SqlCommand getCartIdCmd = new SqlCommand("SELECT cart_id FROM Cart WHERE user_id = @uid", conn, transaction);
                    getCartIdCmd.Parameters.AddWithValue("@uid", currentUserId);
                    object cartIdObj = getCartIdCmd.ExecuteScalar();
                    if (cartIdObj == null) return;
                    int cartId = Convert.ToInt32(cartIdObj);

                    SqlCommand getGamesCmd = new SqlCommand("SELECT game_id, Quantity FROM CartGame WHERE cart_id = @cid", conn, transaction);
                    getGamesCmd.Parameters.AddWithValue("@cid", cartId);
                    SqlDataReader reader = getGamesCmd.ExecuteReader();

                    List<(int gameId, int quantity)> gamesToUpdate = new List<(int, int)>();
                    while (reader.Read())
                    {
                        int gameId = Convert.ToInt32(reader["game_id"]);
                        int quantity = Convert.ToInt32(reader["Quantity"]);
                        gamesToUpdate.Add((gameId, quantity));
                    }
                    reader.Close();

                    foreach (var (gameId, quantity) in gamesToUpdate)
                    {
                        SqlCommand updateGameQtyCmd = new SqlCommand("UPDATE Inventory SET Quantity = Quantity + @qty WHERE GameID = @gid", conn, transaction);
                        updateGameQtyCmd.Parameters.AddWithValue("@qty", quantity);
                        updateGameQtyCmd.Parameters.AddWithValue("@gid", gameId);
                        updateGameQtyCmd.ExecuteNonQuery();
                    }

                    SqlCommand deleteAllCmd = new SqlCommand("DELETE FROM CartGame WHERE cart_id = @cid", conn, transaction);
                    deleteAllCmd.Parameters.AddWithValue("@cid", cartId);
                    deleteAllCmd.ExecuteNonQuery();

                    SqlCommand deleteCartCmd = new SqlCommand("DELETE FROM Cart WHERE cart_id = @cid", conn, transaction);
                    deleteCartCmd.Parameters.AddWithValue("@cid", cartId);
                    deleteCartCmd.ExecuteNonQuery();

                    transaction.Commit();
                    MessageBox.Show("Cart cleared and deleted successfully.");
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show("Error: " + ex.Message);
                }
            }

            selectedGameId = -1;
            txt.Text = "";
            UpdateBillLabel();
            LoadUserCart();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            this.Hide();
            customer form = new customer(username);
            form.FormClosed += (s, args) => this.Dispose();
            form.Show();

        }
    }
    }
