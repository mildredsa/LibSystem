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
    public partial class Users : Form
    {
        private SqlConnection con = Database.GetConnection();
        public Users()
        {
            InitializeComponent();
        }

        private void Users_Load(object sender, EventArgs e)
        {

            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(this.txtUser, "Input User ID.");
        }

        private void btnActivate_Click(object sender, EventArgs e)
        {
            con.Open();

            if (string.IsNullOrEmpty(txtUser.Text) || string.IsNullOrEmpty(txtF.Text) || string.IsNullOrEmpty(txtL.Text) || string.IsNullOrEmpty(cmbGender.Text) || string.IsNullOrEmpty(cmbRole.Text))
            {
                MessageBox.Show("Please fill all fields!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                SqlCommand chkUser = new SqlCommand("SELECT UserID FROM Users WHERE UserID = @UserID", con);
                chkUser.Parameters.AddWithValue("UserID", txtUser.Text);

                if (chkUser.ExecuteScalar() != null)
                {
                    SqlCommand activate = new SqlCommand("UPDATE Users SET Role = @Role, Status = 'Active' WHERE UserID = @UserID", con);
                    activate.Parameters.AddWithValue("UserID", txtUser.Text);
                    activate.Parameters.AddWithValue("Role", cmbRole.Text);
                    activate.ExecuteNonQuery();

                    SqlCommand addBorrower = new SqlCommand("INSERT INTO Borrowers(UserID, [First Name], [Last Name], Gender) VALUES (@UserID, @FirstName, @LastName, @Gender)", con);
                    addBorrower.Parameters.AddWithValue("UserID", txtUser.Text);
                    addBorrower.Parameters.AddWithValue("FirstName", txtF.Text);
                    addBorrower.Parameters.AddWithValue("LastName", txtL.Text);
                    addBorrower.Parameters.AddWithValue("Gender", cmbGender.Text);
                    addBorrower.ExecuteNonQuery();

                    MessageBox.Show("Account Successfully Activated!", "Activation Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("User doesn't exist!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            con.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            con.Open();

            if (string.IsNullOrEmpty(txtUser.Text) || string.IsNullOrEmpty(txtF.Text) || string.IsNullOrEmpty(txtL.Text) || string.IsNullOrEmpty(cmbGender.Text) || string.IsNullOrEmpty(cmbRole.Text))
            {
                MessageBox.Show("Please fill all fields!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                SqlCommand chkUser = new SqlCommand("SELECT UserID FROM Users WHERE UserID = @UserID", con);
                chkUser.Parameters.AddWithValue("UserID", txtUser.Text);

                if (chkUser.ExecuteScalar() != null)
                {
                    SqlCommand updateBorrowers = new SqlCommand("UPDATE Borrowers SET [First Name] = @FirstName, [Last Name] = @LastName, Gender = @Gender WHERE UserID = @UserID", con);
                    updateBorrowers.Parameters.AddWithValue("UserID", txtUser.Text);
                    updateBorrowers.Parameters.AddWithValue("FirstName", txtF.Text);
                    updateBorrowers.Parameters.AddWithValue("LastName", txtL.Text);
                    updateBorrowers.Parameters.AddWithValue("Gender", cmbGender.Text);
                    updateBorrowers.ExecuteNonQuery();

                    SqlCommand updateUsers = new SqlCommand("UPDATE Users SET Role = @Role WHERE UserID = @UserID", con);
                    updateUsers.Parameters.AddWithValue("UserID", txtUser.Text);
                    updateUsers.Parameters.AddWithValue("Role", cmbRole.Text);  
                    updateUsers.ExecuteNonQuery();

                    MessageBox.Show("Account Successfully Updated!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("User doesn't exist!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            con.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            con.Open();

            if (string.IsNullOrEmpty(txtUser.Text))
            {
                MessageBox.Show("Please input a User ID!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {

                SqlCommand chkUser = new SqlCommand("SELECT UserID FROM Users WHERE UserID = @UserID", con);
                chkUser.Parameters.AddWithValue("UserID", txtUser.Text);

                if (chkUser.ExecuteScalar() != null)
                {
                    DialogResult response = MessageBox.Show("Are you sure you want to delete this record?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (response == DialogResult.Yes)
                    {
                        SqlCommand dltUser = new SqlCommand("UPDATE Users SET Role = 'Deleted', Status = 'Deleted' WHERE UserID = @UserID", con);
                        dltUser.Parameters.AddWithValue("UserID", txtUser.Text);
                        dltUser.ExecuteNonQuery();

                        SqlCommand dltBorrower = new SqlCommand("UPDATE Borrowers SET Status = 'Deleted', Deleted = 1 WHERE UserID = @UserID", con);
                        dltBorrower.Parameters.AddWithValue("UserID", txtUser.Text);
                        dltBorrower.ExecuteNonQuery();

                        MessageBox.Show("Account Successfully Deleted!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("User doesn't exist!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            con.Close();
        }

        
    }
}
