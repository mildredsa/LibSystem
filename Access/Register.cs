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
    public partial class Register : Form
    {
        private SqlConnection con = Database.GetConnection();
        public Register()
        {
            InitializeComponent();
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

        private void btnReg_Click(object sender, EventArgs e)
        {
            con.Open();

            if (string.IsNullOrEmpty(txtUser.Text) || string.IsNullOrEmpty(txtPass.Text) || string.IsNullOrEmpty(txtCPass.Text))
            {
                MessageBox.Show("Please fill all fields!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                try
                {
                    if (txtPass.Text == txtCPass.Text)
                    {

                        SqlCommand chkUser = new SqlCommand("SELECT Username FROM Users WHERE Username = @Username", con);
                        chkUser.Parameters.AddWithValue("Username", txtUser.Text);

                        if (chkUser.ExecuteScalar() != null)
                        {
                            MessageBox.Show("Username already taken!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            SqlCommand registerUser = new SqlCommand("INSERT INTO Users(Username, Password) VALUES (@Username, @Password)", con);
                            registerUser.Parameters.AddWithValue("Username", txtUser.Text);
                            registerUser.Parameters.AddWithValue("Password", encryptPassword(txtPass.Text));
                            registerUser.ExecuteNonQuery();

                            DialogResult response = MessageBox.Show("Registration Successful! Proceed to Login?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                            if (response == DialogResult.Yes)
                            {
                                new Login().Show();
                                this.Hide();
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Passwords does not match!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

                con.Close();
            }
        }
    }
}
