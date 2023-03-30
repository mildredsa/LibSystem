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
                        SqlCommand chkUser = new SqlCommand("SELECT Username, Status FROM Users WHERE Username = @Username", con);
                        chkUser.Parameters.AddWithValue("Username", username);

                        SqlDataReader users = chkUser.ExecuteReader();

                        if (users.Read())
                        {
                            string userStatus = users.GetString(1);

                            if (userStatus == "Active")
                            {
                                SqlCommand updateBooks = new SqlCommand("UPDATE Books SET Status = 'Unavailable', Quantity = Quantity - 1 WHERE [Accession Number] = @AccessionNumber", con);
                                updateBooks.Parameters.AddWithValue("AccessionNumber", txtNo.Text);
                                updateBooks.ExecuteNonQuery();

                                SqlCommand borrowed = new SqlCommand("INSERT INTO Borrowed(Username, [Accession Number]) VALUES(@Username, @AccessionNumber)", con);
                                borrowed.Parameters.AddWithValue("Username", username);
                                borrowed.Parameters.AddWithValue("AccessionNumber", txtNo.Text);
                                borrowed.ExecuteNonQuery();

                                MessageBox.Show("Book Successfully Borrowed", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

                    if (bookStatus == "Unvailable")
                    {
                        SqlCommand chkUser = new SqlCommand("SELECT Username, Status FROM Users WHERE Username = @Username", con);
                        chkUser.Parameters.AddWithValue("Username", username);

                        SqlDataReader users = chkUser.ExecuteReader();

                        if (users.Read())
                        {
                            string userStatus = users.GetString(1);

                            if (userStatus == "Active")
                            {
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

                                MessageBox.Show("Book Successfully Borrowed", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

            con.Close();

        }

        private void Transaction_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'libSystemDataSet.Books' table. You can move, or remove it, as needed.
            this.booksTableAdapter.Fill(this.libSystemDataSet.Books);

        }

        private void grid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            con.Open();

            SqlCommand genre = new SqlCommand("SELECT * FROM Books WHERE Genre = @Genre", con);
            genre.Parameters.AddWithValue("Genre", cmbGenre.Text);

            SqlDataAdapter adap = new SqlDataAdapter();
            DataTable dt = new DataTable();
            adap.Fill(dt);

            grid.DataSource = dt;

            con.Close();
        }

        private void picSearch_Click(object sender, EventArgs e)
        {
            con.Open();

            SqlCommand searchCmd = new SqlCommand("SELECT * FROM Books WHERE CONCAT(AccessionNumber, ' ', Title, ' ', Author, ' ', Genre, ' ', Status) LIKE @searchString", con);
            searchCmd.Parameters.AddWithValue("searchString", "%" + txtSearch.Text + "%");
            searchCmd.ExecuteNonQuery();

            SqlDataAdapter adap = new SqlDataAdapter(searchCmd);
            DataTable dt = new DataTable();
            adap.Fill(dt);

            grid.DataSource = dt;

            con.Close();
        }


        private void btnLogout_Click(object sender, EventArgs e)
        {
            new Login().Show();
            this.Close();
        }
    }

}

    
