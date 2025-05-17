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
    public partial class admindash : Form
    {


        string connectionString = "Data Source=ZESHAN\\SQLEXPRESS;Initial Catalog=gamestore;Integrated Security=True;Encrypt=False";

        public admindash(string d)
        {
            InitializeComponent();
            email.Text = d;
            this.FormClosing += adminformclosed;
            email.ReadOnly = true;
            category.SelectedIndex = 0;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);

        }
        private void adminformclosed(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                if (row.DataBoundItem is DataRowView drv)
                {
                    string gameTitle = drv["Title"].ToString();
                    this.Hide();
                    mngproduct form = new mngproduct(email.Text, gameTitle);
                    form.FormClosed += (s, args) => this.Dispose();
                    form.Show();

                }
            }
        }
        private DataTable allData;
        private int currentPage = 1;
        private int rowsPerPage = 10;


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
        private void admindash_Load(object sender, EventArgs e)
        {

            {
                LoadAllGames();
            }

        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void logout_Click_1(object sender, EventArgs e)
        {
            MessageBox.Show("Logout successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            this.Hide();

           Homepage form = new Homepage();
            form.FormClosed += (s, args) => this.Dispose();
            form.Show();
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
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

        private void detail_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            mngproduct form = new mngproduct(email.Text);
            form.FormClosed += (s, args) => this.Dispose();
            form.Show();

        }

        private void order_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            ordmng  form= new ordmng(email.Text);
            form.FormClosed += (s, args) => this.Dispose();
            form.Show();
        }

        private void home_Click_1(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            this.Hide();
            ordmng form = new ordmng(email.Text);
            form.FormClosed += (s, args) => this.Dispose();
            form.Show();

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Hide();
            mngproduct form = new mngproduct(email.Text);
            form.FormClosed += (s, args) => this.Dispose();
            form.Show();

        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Logout successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            this.Hide();

            Homepage form = new Homepage();
            form.FormClosed += (s, args) => this.Dispose();
            form.Show();

        }
    }
}
