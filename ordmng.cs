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
    public partial class ordmng : Form
    {
        string connectionString = "Data Source=ZESHAN\\SQLEXPRESS;Initial Catalog=gamestore;Integrated Security=True;Encrypt=False";
        DataTable cartData = new DataTable();
        int cartPageSize = 10;
        int currentCartPage = 1;
        string username;

        public ordmng(string s)
        {
            username = s;
            InitializeComponent();
            this.FormClosing += order_FormClosing;
            this.Load += Form1_Load;
        }

        private void order_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadUserCart();
            cartGrid.EditingControlShowing += CartGrid_EditingControlShowing;
        }

        private void LoadUserCart()
        {
            cartData.Clear();
            cartGrid.DataSource = null;
            cartGrid.Rows.Clear();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                        SELECT order_id, order_date, total_amount, status 
                        FROM Orders";
                    SqlCommand cmd = new SqlCommand(query, conn);
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
            catch (Exception ex)
            {
                MessageBox.Show("Error loading orders: " + ex.Message);
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
            cartGrid.RowTemplate.Height = 40;

            if (cartGrid.Columns.Contains("order_id"))
                cartGrid.Columns["order_id"].HeaderText = "Order ID";
            if (cartGrid.Columns.Contains("order_date"))
                cartGrid.Columns["order_date"].HeaderText = "Order Date";
            if (cartGrid.Columns.Contains("total_amount"))
                cartGrid.Columns["total_amount"].HeaderText = "Total Amount";
            if (cartGrid.Columns.Contains("status"))
                cartGrid.Columns["status"].HeaderText = "Status";

            if (cartGrid.Columns.Contains("status"))
                cartGrid.Columns["status"].Visible = false;
            if (!cartGrid.Columns.Contains("StatusCombo"))
            {
                DataGridViewComboBoxColumn comboCol = new DataGridViewComboBoxColumn();
                comboCol.Name = "StatusCombo";
                comboCol.HeaderText = "Status";
                comboCol.Items.AddRange("Processing", "Shipped", "Delivered", "Cancelled");
                comboCol.DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton;
                cartGrid.Columns.Add(comboCol);
                foreach (DataGridViewColumn column in cartGrid.Columns)
                {
                    column.ReadOnly = true;
                }
                cartGrid.Columns["StatusCombo"].ReadOnly = false;
            }

            foreach (DataGridViewRow row in cartGrid.Rows)
            {
                if (row.Cells["status"].Value != DBNull.Value)
                {
                    row.Cells["StatusCombo"].Value = row.Cells["status"].Value.ToString();
                }
            }
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

        private void UpdateOrderStatus(int orderId, string newStatus)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "UPDATE Orders SET status = @status WHERE order_id = @id";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@status", newStatus);
                    cmd.Parameters.AddWithValue("@id", orderId);
                    cmd.ExecuteNonQuery();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating status: " + ex.Message);
            }
        }

        private void CartGrid_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (cartGrid.CurrentCell.ColumnIndex == cartGrid.Columns["StatusCombo"].Index)
            {
                ComboBox comboBox = e.Control as ComboBox;
                if (comboBox != null)
                {
                    comboBox.SelectedIndexChanged -= StatusCombo_SelectedIndexChanged;
                    comboBox.SelectedIndexChanged += StatusCombo_SelectedIndexChanged;
                }
            }
        }

        private void StatusCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox combo = sender as ComboBox;
            if (combo != null && cartGrid.CurrentRow != null)
            {
                int orderId = Convert.ToInt32(cartGrid.CurrentRow.Cells["order_id"].Value);
                string newStatus = combo.SelectedItem.ToString();
                UpdateOrderStatus(orderId, newStatus);
            }
        }

        private void home_Click(object sender, EventArgs e)
        {
            this.Hide();
            admindash form = new admindash(username);
            form.FormClosed += (s, args) => this.Dispose();
            form.Show();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            this.Hide();
            admindash form = new admindash(username);
            form.FormClosed += (s, args) => this.Dispose();
            form.Show();

        }
    }
}
