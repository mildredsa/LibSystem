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

namespace LibSystem.Manage
{
    public partial class Books : Form
    {
        private SqlConnection con = Database.GetConnection();
        public Books()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            con.Open();

            if (string.IsNullOrEmpty(txtNo.Text) || string.IsNullOrEmpty(txtTitle.Text) || string.IsNullOrEmpty(txtAuthor.Text) || string.IsNullOrEmpty(cmbGenre.Text))
            {
                MessageBox.Show("Please fill all fields!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                SqlCommand chkBook = new SqlCommand("SELECT [Accession Number] FROM Users WHERE [Accession Number] = @AccessionNumber", con);
                chkBook.Parameters.AddWithValue("AccessionNumber", txtNo.Text);

                if (chkBook.ExecuteScalar() != null)
                {
                    MessageBox.Show("Book is already Recorded!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    SqlCommand add = new SqlCommand("INSERT INTO Books ([Accession Number], Title, Author, Genre) VALUES (@AccessionNumber, @Title, @Author, @Genre)", con);
                    add.Parameters.AddWithValue("AccessionNumber", txtNo.Text);
                    add.Parameters.AddWithValue("Title", txtTitle.Text);
                    add.Parameters.AddWithValue ("Author", txtAuthor.Text);
                    add.Parameters.AddWithValue("Genre", cmbGenre.Text);
                    add.ExecuteNonQuery();

                    MessageBox.Show("Book Successfully Added!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            con.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            con.Open();

            if (string.IsNullOrEmpty(txtNo.Text) || string.IsNullOrEmpty(txtTitle.Text) || string.IsNullOrEmpty(txtAuthor.Text) || string.IsNullOrEmpty(cmbGenre.Text))
            {
                MessageBox.Show("Please fill all fields!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                SqlCommand chkBook = new SqlCommand("SELECT [Accession Number] FROM Users WHERE [Accession Number] = @AccessionNumber", con);
                chkBook.Parameters.AddWithValue("AccessionNumber", txtNo.Text);

                if (chkBook.ExecuteScalar() != null)
                {
                    SqlCommand update = new SqlCommand("UPDATE Books SET Title = @Title, Author = @Author, Genre = @Genre WHERE [Accession Number] = @AccessionNumber", con);
                    update.Parameters.AddWithValue("AccessionNumber", txtNo.Text);
                    update.Parameters.AddWithValue("Title", txtTitle.Text);
                    update.Parameters.AddWithValue("Author", txtAuthor.Text);
                    update.Parameters.AddWithValue("Genre", cmbGenre.Text);
                    update.ExecuteNonQuery();

                    MessageBox.Show("Book Successfully Updated!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Book cannot be updated! Book doesn't Exist!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            con.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            con.Open(); 

            if (string.IsNullOrEmpty(txtNo.Text))
            {
                MessageBox.Show("Please input Accession Number!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                SqlCommand chkBook = new SqlCommand("SELECT [Accession Number] FROM Users WHERE [Accession Number] = @AccessionNumber", con);
                chkBook.Parameters.AddWithValue("AccessionNumber", txtNo.Text);

                if (chkBook.ExecuteScalar() != null)
                {
                    DialogResult response = MessageBox.Show("Are you sure you want to delete this record?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (response == DialogResult.Yes)
                    {
                        SqlCommand delete = new SqlCommand("UPDATE Books SET Title = 'Deleted', Author = 'Deleted', Genre = 'Deleted', Status = 'Deleted', Quantity = 0 WHERE [Accession Number] = @AccessionNumber", con);
                        delete.Parameters.AddWithValue("AccessionNumber", txtNo.Text);
                        delete.ExecuteNonQuery();

                        MessageBox.Show("Account Successfully Deleted!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Book cannot be deleted! Book doesn't Exist!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            con.Close();
        }
    }
}
