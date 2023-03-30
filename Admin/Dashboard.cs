using DGVPrinterHelper;
using LibSystem.Admin;
using LibSystem.Borrower;
using LibSystem.Manage;
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
using System.Windows.Forms.DataVisualization.Charting;

namespace LibSystem
{
    public partial class Dashboard : Form
    {
        private SqlConnection con = Database.GetConnection();
        public Dashboard()
        {
            InitializeComponent();
        }

        private void btnUsers_Click(object sender, EventArgs e)
        {
            new Users().Show();
        }

        private void btnBooks_Click(object sender, EventArgs e)
        {
            new Books().Show();
        }

        

        private void borrowerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            con.Open();

            date1.Visible = false;
            date2.Visible = false;
            lblStart.Visible = false;
            lblEnd.Visible = false;
            picFilter.Visible = false;

            SqlCommand borrowers = new SqlCommand("SELECT * FROM Borrowers", con);

            SqlDataAdapter adap = new SqlDataAdapter(borrowers);
            DataTable dt = new DataTable();
            adap.Fill(dt);

            grid.DataSource = dt;

            con.Close();
        }

        private void booksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            con.Open();

            date1.Visible = false;
            date2.Visible = false;
            lblStart.Visible = false;
            lblEnd.Visible = false;
            picFilter.Visible = false;

            SqlCommand books = new SqlCommand("SELECT * FROM Books", con);

            SqlDataAdapter adap = new SqlDataAdapter(books);
            DataTable dt = new DataTable();
            adap.Fill(dt);

            grid.DataSource = dt;

            con.Close();
        }

        private void borrowedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            con.Open();

            date1.Visible = false;
            date2.Visible = false;
            lblStart.Visible = false;
            lblEnd.Visible = false;
            picFilter.Visible = false;

            SqlCommand borrowed = new SqlCommand("SELECT * FROM Borrowed", con);

            SqlDataAdapter adap = new SqlDataAdapter(borrowed);
            DataTable dt = new DataTable();
            adap.Fill(dt);

            grid.DataSource = dt;

            con.Close();
        }

        private void returnedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            con.Open();

            date1.Visible = false;
            date2.Visible = false;
            lblStart.Visible = false;
            lblEnd.Visible = false;
            picFilter.Visible = false;

            SqlCommand returned = new SqlCommand("SELECT * FROM Returned", con);

            SqlDataAdapter adap = new SqlDataAdapter(returned);
            DataTable dt = new DataTable();
            adap.Fill(dt);

            grid.DataSource = dt;

            con.Close();
        }

        private void activateUsersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            con.Open();

            date1.Visible = false;
            date2.Visible = false;
            lblStart.Visible = false;
            lblEnd.Visible = false;
            picFilter.Visible = false;

            SqlCommand users = new SqlCommand("SELECT * FROM Users WHERE Status <> 'Active'", con);

            SqlDataAdapter adap = new SqlDataAdapter(users);
            DataTable dt = new DataTable();
            adap.Fill(dt);

            grid.DataSource = dt;

            con.Close();
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            date1.Visible = false;
            date2.Visible = false;
            lblStart.Visible = false;
            lblEnd.Visible = false;
            picFilter.Visible = false;

            DialogResult response = MessageBox.Show("Are you sure you want to logout?", "Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (response == DialogResult.Yes)
            {
                new Login().Show();
                this.Close();
            }
            
        }


        private void transactionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            con.Open();

            date1.Visible = true;
            date2.Visible = true;
            lblStart.Visible = true;
            lblEnd.Visible = true;

            SqlCommand transaction = new SqlCommand("SELECT * FROM Transactions", con);

            SqlDataAdapter adap = new SqlDataAdapter(transaction);
            DataTable dt = new DataTable();
            adap.Fill(dt);

            grid.DataSource = dt;

            con.Close();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            con.Open();

            DGVPrinter printer = new DGVPrinter();
            printer.Title = "Summary Report";
            printer.SubTitleSpacing = 10;
            printer.SubTitle = "Date: " + DateTime.Now.ToString("MM/dd/yyyy");
            printer.ProportionalColumns = true;
            printer.HeaderCellAlignment = StringAlignment.Near;
            printer.PageNumbers = true;
            printer.PageNumberInHeader = true;
            printer.HeaderCellAlignment = StringAlignment.Center;

            int totalRecords;
            if (grid.RowCount == 0)
            {
                totalRecords = 0;
            }
            else
            {
                totalRecords = grid.RowCount - 1;
            }

            printer.Footer = "Total Records: " + totalRecords.ToString();

            printer.PrintPreviewDataGridView(grid);

            con.Close();
        }



        private void picFilter_Click(object sender, EventArgs e)
        {
            DateTime startDate = date1.Value;
            DateTime endDate = date2.Value;

            string[] columns = { "Date Borrowed", "Due Date", "Date Returned" };
            DialogResult result = MessageBox.Show("Please select a column to filter by", "Column Selection", MessageBoxButtons.OKCancel);
            if (result == DialogResult.OK)
            {
                string selectedColumn = DateFilterMessageBox.Show(columns);
                if (selectedColumn == null)
                {
                    return;
                }

                using (SqlConnection con = new SqlConnection(Database.connection))
                {
                    con.Open();

                    SqlCommand filter = new SqlCommand($"SELECT * FROM Transactions WHERE [{selectedColumn}] BETWEEN @startDate AND @endDate", con);
                    filter.Parameters.AddWithValue("@startDate", startDate);
                    filter.Parameters.AddWithValue("@endDate", endDate);

                    SqlDataAdapter adap = new SqlDataAdapter(filter);
                    DataTable dt = new DataTable();
                    adap.Fill(dt);

                    grid.DataSource = dt;

                    con.Close();
                }
            }

        }

        private void graphsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Graphs().Show();
        }
    }
}
