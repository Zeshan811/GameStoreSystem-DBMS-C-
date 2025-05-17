using GameStoreSystem;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
namespace GameStoreSystem
{
    public partial class customer : Form
    {
        string connectionString =
             "Data Source=ZESHAN\\SQLEXPRESS;Initial Catalog=gamestore;Integrated Security=True;Encrypt=False";
        public customer(string s)
        {
            InitializeComponent();
            category.SelectedIndex = 0;
            this.FormClosing += customer_FormClosing;
            email.Text = s;
            email.ReadOnly = true;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);

        }
        private void customer_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void home_Click(object sender, EventArgs e)
        {

        }

        private void order_Click(object sender, EventArgs e)
        {
            this.Hide();
            ordersummary form = new ordersummary(email.Text);
            form.FormClosed += (s, args) => this.Dispose();
            form.Show();

        }

        private void detail_Click(object sender, EventArgs e)
        {
            this.Hide();
            details form = new  details(email.Text);
            form.FormClosed += (s, args) => this.Dispose();
            form.Show();

        }

        private void cart_Click(object sender, EventArgs e)
        {
            this.Hide();
            cartpage form = new cartpage(email.Text);
            form.FormClosed += (s, args) => this.Dispose();
            form.Show();

        }

        private void logout_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Logout successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            this.Hide();

            Homepage form = new Homepage();
            form.FormClosed += (s, args) => this.Dispose();
            form.Show();

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Logout successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            this.Hide();

            Homepage form = new Homepage();
            form.FormClosed += (s, args) => this.Dispose();
            form.Show();

        }
        private void adminup_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                if (row.DataBoundItem is DataRowView drv)
                {
                    string gameTitle = drv["Title"].ToString();
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();

                        string query = "Update Games  set  views= views+1 where Title COLLATE SQL_Latin1_General_CP1_CS_AS=@title";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@title", gameTitle);
                            cmd.ExecuteNonQuery();
                        }
                    }
                            this.Hide();
                    details form = new details(email.Text,gameTitle);
                    form.FormClosed += (s, args) => this.Dispose();
                    form.Show();

                }
            }
        }

            private void customer_Load(object sender, EventArgs e)
        {
            LoadAllGames();
        }
        private DataTable allData;
        private int currentPage = 1;
        private int rowsPerPage = 10;

        private void button1_Click(object sender, EventArgs e)
        {
            string selectedCategory = category.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(selectedCategory) || selectedCategory == "All")
            {
                LoadAllGames();
                dataGridView1.ClearSelection();
            }
            else
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string query = "SELECT Title, Category FROM Games WHERE Category = @Category";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Category", selectedCategory);
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        allData = new DataTable();
                        adapter.Fill(allData);
                        currentPage = 1;
                        DisplayPage(currentPage);
                        GeneratePageButtons();
                        dataGridView1.ClearSelection();
                    }
                }
            }
        }

        private void LoadAllGames()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT Title, Category FROM Games";
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                allData = new DataTable();
                adapter.Fill(allData);
                currentPage = 1;
                DisplayPage(currentPage);
                GeneratePageButtons();
            }
        }

        private void DisplayPage(int page)
        {
            if (allData == null || allData.Rows.Count == 0)
                return;

            DataTable pageTable = allData.Clone(); 

            int startIndex = (page - 1) * rowsPerPage;
            int endIndex = Math.Min(startIndex + rowsPerPage, allData.Rows.Count);

            for (int i = startIndex; i < endIndex; i++)
                pageTable.ImportRow(allData.Rows[i]);

            dataGridView1.DataSource = pageTable;
            formatgrid();
            dataGridView1.ClearSelection();

        }

        private void GeneratePageButtons()
        {
            flowPanelPages.Controls.Clear();

            if (allData == null || allData.Rows.Count == 0)
                return;

            int totalPages = (int)Math.Ceiling((double)allData.Rows.Count / rowsPerPage);

            for (int i = 1; i <= totalPages; i++)
            {
                Button btn = new Button
                {
                    Text = i.ToString(),
                    Width = 30,
                    Height = 30,
                    Margin = new Padding(3),
                    BackColor = (i == currentPage) ? Color.LightBlue : Color.White
                };

                int page = i;
                btn.Click += (s, e) =>
                {
                    currentPage = page;
                    DisplayPage(currentPage);
                    GeneratePageButtons(); 
                };

                flowPanelPages.Controls.Add(btn);
            }
        }

        private void formatgrid()
        {
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.AutoResizeColumns();
            dataGridView1.AutoResizeRows();
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.ReadOnly = true;
            ResizeGridHeight();
        }

        private void ResizeGridHeight()
        {
            int rowCount = dataGridView1.Rows.Count;
            int rowHeight = dataGridView1.RowTemplate.Height;
            int headerHeight = dataGridView1.ColumnHeadersHeight;
            if (dataGridView1.AllowUserToAddRows)
                rowCount -= 1;

            int totalHeight = headerHeight + (rowCount * rowHeight);
            int maxHeight = 500;
            dataGridView1.Height = Math.Min(totalHeight + 5, maxHeight);
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            this.Hide();
            ordersummary form = new ordersummary(email.Text);
            form.FormClosed += (s, args) => this.Dispose();
            form.Show();

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Hide();
            details form = new details(email.Text);
            form.FormClosed += (s, args) => this.Dispose();
            form.Show();

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            this.Hide();
            cartpage form = new cartpage(email.Text);
            form.FormClosed += (s, args) => this.Dispose();
            form.Show();

        }
    }
}
