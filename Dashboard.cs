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

            SqlCommand returned = new SqlCommand("SELECT * FROM Returned", con);

            SqlDataAdapter adap = new SqlDataAdapter(returned);
            DataTable dt = new DataTable();
            adap.Fill(dt);

            grid.DataSource = dt;

            con.Close();
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Login().Show();
            this.Close();
        }
    }
}
