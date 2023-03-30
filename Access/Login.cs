using LibSystem.Borrower;
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
    public partial class Login : Form
    {
        private SqlConnection con = Database.GetConnection();
        public Login()
        {
            InitializeComponent();
        }

        public static string decryptPassword(string encryptedPassword)
        {
            string decryptedPassword = "";
            foreach (char c in encryptedPassword)
            {
                int asciiValue = (int)c;
                asciiValue -= 2;
                decryptedPassword += (char)asciiValue;
            }
            return decryptedPassword;
        }

        private void btnLog_Click(object sender, EventArgs e)
        {
            con.Open();

            string username = txtUser.Text;

            SqlCommand retrieve = new SqlCommand("SELECT Username, Password, Role, Status FROM Users WHERE Username = @Username", con);
            retrieve.Parameters.AddWithValue("Username", username);
            SqlDataReader reader = retrieve.ExecuteReader();

            if (string.IsNullOrEmpty(txtUser.Text) || string.IsNullOrEmpty(txtPass.Text))
            {
                MessageBox.Show("Please fill all fields!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                try
                {
                    if (reader.Read())
                    {
                        string encryptedPassword = reader.GetString(1);
                        string decryptedPassword = decryptPassword(encryptedPassword);

                        if (decryptedPassword == txtPass.Text)
                        {
                            string status = reader.GetString(3);

                            if (status == "Active")
                            {
                                string role = reader.GetString(2);

                                if (role == "Admin")
                                {
                                    new Dashboard().Show();
                                    this.Hide();
                                }
                                else if (role == "Borrower")
                                {
                                    Transaction transaction = new Transaction(txtUser.Text);
                                    transaction.Show();
                                    this.Hide();
                                }
                                else
                                {
                                    MessageBox.Show("Account may have been deleted. Please Inquire.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                }
                            }
                            else if (status == "Inactive")
                            {
                                MessageBox.Show("Account not activated. Wait for activation.", "Wait", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                            }
                            else
                            {
                                MessageBox.Show("Account may have been deleted. Please Inquire.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Username and password doesn't match!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        DialogResult response = MessageBox.Show("User Account doesn't exist. Proceed to Register?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (response == DialogResult.Yes)
                        {
                            new Register().Show();
                            this.Hide();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show( "Error: " + ex.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }

            
            con.Close();
        }

        private void btnReg_Click(object sender, EventArgs e)
        {
            new Register().Show();
            this.Hide();
        }
    }
}
