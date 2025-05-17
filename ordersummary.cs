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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
namespace GameStoreSystem
{

    public partial class ordersummary : Form
    {

        string connectionString = "Data Source=ZESHAN\\SQLEXPRESS;Initial Catalog=gamestore;Integrated Security=True;Encrypt=False";

        DataTable cartData = new DataTable();
        int cartPageSize = 10;
        int currentCartPage = 1;
        int currentUserId;
        int selectedGameId = -1;
        int selectedQuantity = 0;
        string username;
        public ordersummary(string s)
        {
            username = s;
            InitializeComponent();
            this.FormClosing += order_FormClosing;
        }

        private void order_FormClosing(object sender, FormClosingEventArgs e)
        {

            Application.Exit();
        }

        private void ordersummary_Load(object sender, EventArgs e)
        {
            {
                currentUserId = GetUserIdFromUsername(username);
                LoadUserCart();
            }


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
                                    SELECT order_id, order_date, total_amount, status 
                                    FROM Orders 
                                    WHERE user_id = @uid";
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

                }
                else
                {

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

            cartGrid.DataSource = pageTable;
            FormatCartGrid();
        }

        private void FormatCartGrid()
        {
            cartGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            cartGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            cartGrid.ReadOnly = true;
            cartGrid.RowTemplate.Height = 40;

            if (cartGrid.Columns.Contains("order_id"))
                cartGrid.Columns["order_id"].HeaderText = "Order ID";
            if (cartGrid.Columns.Contains("order_date"))
                cartGrid.Columns["order_date"].HeaderText = "Order Date";
            if (cartGrid.Columns.Contains("total_amount"))
                cartGrid.Columns["total_amount"].HeaderText = "Total Amount";
            if (cartGrid.Columns.Contains("status"))
                cartGrid.Columns["status"].HeaderText = "Status";
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

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }


        private void home_Click(object sender, EventArgs e)
        {

            this.Hide();
            customer form = new customer(username);
            form.FormClosed += (s, args) => this.Dispose();
            form.Show();

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