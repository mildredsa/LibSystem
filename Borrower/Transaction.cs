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

namespace LibSystem.Borrower
{
    public partial class Transaction : Form
    {
        private SqlConnection con = Database.GetConnection();
        private string username;
        public Transaction(string username)
        {
            InitializeComponent();
            lblUser.Text = username;
            this.username = lblUser.Text;
        }

        private void loaddatagrid()
        {
            using(SqlConnection con = Database.GetConnection())
            {
                con.Open();

                SqlCommand books = new SqlCommand("SELECT * FROM Books", con);
                books.Parameters.AddWithValue("Genre", cmbGenre.Text);

                SqlDataAdapter adap = new SqlDataAdapter(books);
                DataTable dt = new DataTable();
                adap.Fill(dt);

                grid.DataSource = dt;
            }
        }

        private void btnBorrow_Click(object sender, EventArgs e)
        {
            con.Open();

            Transaction transaction = new Transaction(username);
            transaction.Show();

            if (!string.IsNullOrEmpty(txtNo.Text))
            {
                SqlCommand chkBook = new SqlCommand("SELECT [Accession Number], Status FROM Books WHERE [Accession Number] = @AccessionNumber", con);
                chkBook.Parameters.AddWithValue("AccessionNumber", txtNo.Text);
                SqlDataReader books = chkBook.ExecuteReader();

                if (books.Read())
                {
                    string bookStatus = books.GetString(1);

                    if (bookStatus == "Available")
                    {
                        books.Close();

                        SqlCommand chkUser = new SqlCommand("SELECT Username, Status FROM Users WHERE Username = @Username", con);
                        chkUser.Parameters.AddWithValue("Username", username);

                        SqlDataReader users = chkUser.ExecuteReader();

                        if (users.Read())
                        {
                            string userStatus = users.GetString(1);

                            if (userStatus == "Active")
                            {
                                users.Close();

                                SqlCommand chkBorrowed = new SqlCommand("SELECT Username, Returned FROM Borrowed WHERE Username = @Username", con);
                                chkBorrowed.Parameters.AddWithValue("Username", username);

                                SqlDataReader borrowing = chkBorrowed.ExecuteReader();

                                if (borrowing.Read())
                                {
                                    bool userReturned = borrowing.GetBoolean(1);

                                    if (userReturned == true)
                                    {
                                        borrowing.Close();

                                        SqlCommand updateBooks = new SqlCommand("UPDATE Books SET Status = 'Unavailable', Quantity = Quantity - 1 WHERE [Accession Number] = @AccessionNumber", con);
                                        updateBooks.Parameters.AddWithValue("AccessionNumber", txtNo.Text);
                                        updateBooks.ExecuteNonQuery();

                                        SqlCommand borrowed = new SqlCommand("INSERT INTO Borrowed(Username, [Accession Number]) VALUES(@Username, @AccessionNumber)", con);
                                        borrowed.Parameters.AddWithValue("Username", username);
                                        borrowed.Parameters.AddWithValue("AccessionNumber", txtNo.Text);
                                        borrowed.ExecuteNonQuery();

                                        MessageBox.Show("Book Successfully Borrowed", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                    else
                                    {
                                        MessageBox.Show("Please still haven't returned your borrowed book.", "Stop", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Book Successfully Returned", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }

                            }
                            else if (userStatus == "Inactive")
                            {
                                MessageBox.Show("User has not been activated. Please Inquire.", "Inactive", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                            }
                            else
                            {
                                MessageBox.Show("Borrower does not exist", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }

                        }
                        else
                        {
                            MessageBox.Show("Borrower does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Book has been Borrowed.", "Unavailable", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }
                }
                else
                {
                    MessageBox.Show("Book does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please input an Accession Number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            loaddatagrid();
            con.Close();
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            con.Open();

            Transaction transaction = new Transaction(username);
            transaction.Show();

            if(!string.IsNullOrEmpty(txtNo.Text))
            {
                SqlCommand chkBook = new SqlCommand("SELECT [Accession Number], Status FROM Books WHERE [Accession Number] = @AccessionNumber", con);
                chkBook.Parameters.AddWithValue("AccessionNumber", txtNo.Text);
                SqlDataReader books = chkBook.ExecuteReader();

                if (books.Read())
                {
                    string bookStatus = books.GetString(1);

                    if (bookStatus == "Unavailable")
                    {
                        books.Close();

                        SqlCommand chkUser = new SqlCommand("SELECT Username, Status FROM Users WHERE Username = @Username", con);
                        chkUser.Parameters.AddWithValue("Username", username);

                        SqlDataReader users = chkUser.ExecuteReader();

                        if (users.Read())
                        {
                            string userStatus = users.GetString(1);

                            if (userStatus == "Active")
                            {
                                users.Close();

                                SqlCommand updateBooks = new SqlCommand("UPDATE Books SET Status = 'Available', Quantity = Quantity + 1 WHERE [Accession Number] = @AccessionNumber", con);
                                updateBooks.Parameters.AddWithValue("AccessionNumber", txtNo.Text);
                                updateBooks.ExecuteNonQuery();

                                SqlCommand updateBorrowed = new SqlCommand("UPDATE Borrowed SET Returned = 1 WHERE [Accession Number] = @AccessionNumber", con);
                                updateBorrowed.Parameters.AddWithValue("AccessionNumber", txtNo.Text);
                                updateBorrowed.ExecuteNonQuery();

                                SqlCommand borrowed = new SqlCommand("INSERT INTO Returned(Username, [Accession Number]) VALUES(@Username, @AccessionNumber)", con);
                                borrowed.Parameters.AddWithValue("Username", username);
                                borrowed.Parameters.AddWithValue("AccessionNumber", txtNo.Text);
                                borrowed.ExecuteNonQuery();

                                MessageBox.Show("Book Successfully Returned", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else if (userStatus == "Inactive")
                            {
                                MessageBox.Show("User has not been activated. Please Inquire.", "Inactive", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                            }
                            else
                            {
                                MessageBox.Show("Borrower does not exist", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }

                        }
                        else
                        {
                            MessageBox.Show("Borrower does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Book has not been Borrowed.", "Unavailable", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }
                }
                else
                {
                    MessageBox.Show("Book does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please input an Accession Number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            loaddatagrid();
            con.Close();

        }


        private void picSearch_Click(object sender, EventArgs e)
        {
            con.Open();

            SqlCommand searchCmd = new SqlCommand("SELECT * FROM Books WHERE CONCAT([Accession Number], ' ', Title, ' ', Author, ' ', Genre, ' ', Status) LIKE @searchString", con);
            searchCmd.Parameters.AddWithValue("searchString", "%" + txtSearch.Text + "%");
            searchCmd.ExecuteNonQuery();

            SqlDataAdapter adap = new SqlDataAdapter(searchCmd);
            DataTable dt = new DataTable();
            adap.Fill(dt);

            grid.DataSource = dt;

            con.Close();
        }

        private void cmbGenre_SelectedIndexChanged(object sender, EventArgs e)
        {
            con.Open();

            SqlCommand status = new SqlCommand("SELECT * FROM Books WHERE Status = @Status", con);
            status.Parameters.AddWithValue("Status", cmbGenre.Text);

            SqlDataAdapter adap = new SqlDataAdapter(status);
            DataTable dt = new DataTable();
            adap.Fill(dt);

            grid.DataSource = dt;

            con.Close();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            DialogResult response = MessageBox.Show("Are you sure you want to logout?", "Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (response == DialogResult.Yes)
            {
                new Login().Show();
                this.Close();
            }
        }

        private void grid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == grid.Columns["Accession Number"].Index)
            {
                DataGridViewRow row = grid.Rows[e.RowIndex];
                string accessionNo = row.Cells["Accession Number"].Value.ToString();
                txtNo.Text = accessionNo;
            }
        }

        private void Transaction_Load(object sender, EventArgs e)
        {
            con.Open();

            SqlCommand books = new SqlCommand("SELECT * FROM Books", con);
            books.Parameters.AddWithValue("Genre", cmbGenre.Text);

            SqlDataAdapter adap = new SqlDataAdapter(books);
            DataTable dt = new DataTable();
            adap.Fill(dt);

            grid.DataSource = dt;

            con.Close();
        }
    }

}

    
