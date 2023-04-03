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
    public partial class ChangePassword : Form
    {
        private SqlConnection con = Database.GetConnection();
        private string username;
        public ChangePassword(string username)
        {
            InitializeComponent();
            lblUser.Text = username;
            this.username = lblUser.Text;
        }

        public static string encryptPassword(string password)
        {
            string encryptedPassword = "";
            foreach (char c in password)
            {
                int asciiValue = (int)c;
                asciiValue += 2;
                encryptedPassword += (char)asciiValue;
            }
            return encryptedPassword;
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            con.Open();

            SqlCommand retrieve = new SqlCommand("SELECT Username, Password FROM Users WHERE Username = @Username", con);
            retrieve.Parameters.AddWithValue("Username", username);
            SqlDataReader reader = retrieve.ExecuteReader();

            if (!string.IsNullOrEmpty(txtPass.Text) || !string.IsNullOrEmpty(txtCPass.Text))
            {
                if (reader.Read())
                {
                    reader.Close();
                    if (txtPass.Text == txtCPass.Text)
                    {
                        SqlCommand changePass = new SqlCommand("UPDATE Users SET Password = @Password WHERE Username = @Username", con);
                        changePass.Parameters.AddWithValue("Username", username);
                        changePass.Parameters.AddWithValue("Password", encryptPassword(txtPass.Text));
                        changePass.ExecuteNonQuery();

                        DialogResult response = MessageBox.Show("Password changed successfully! Proceed to Login?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (response == DialogResult.Yes)
                        {
                            new Login().Show();
                            this.Hide();
                        }

                    }
                    else
                    {
                        MessageBox.Show("Password doesn't match!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Account doesn't exist!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please fill all fields!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            con.Close();
        }
    }
}
