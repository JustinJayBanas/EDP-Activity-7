using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace SampleUI
{
    public partial class loginform : Form
    {
        public loginform()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void forgPass_click(object sender, EventArgs e)
        {
			this.Hide();
			var newchangepassform = new Changepassform();
			newchangepassform.ShowDialog();

            
		}

        private void login_click(object sender, EventArgs e)
        {
            string myusername = this.txtusername.Text;
            string mypassword = this.txtpassword.Text;

            MySql.Data.MySqlClient.MySqlConnection conn;
            string myConnectionString;
            myConnectionString = "server=127.0.0.1;uid=root;" +
            "pwd=root;database=artistangallery";
            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection();
                conn.ConnectionString = myConnectionString;
                conn.Open();

                // Set all accounts to inactive
                string updateSql = "UPDATE account SET active = 0";
                MySqlCommand updateCmd = new MySqlCommand(updateSql, conn);
                updateCmd.ExecuteNonQuery();

                // Set the logged-in account to active
                string setActiveSql = "UPDATE account SET active = 1 WHERE username = @myuser AND pass = @mypass";
                MySqlCommand setActiveCmd = new MySqlCommand(setActiveSql, conn);
                setActiveCmd.Parameters.AddWithValue("@myuser", myusername);
                setActiveCmd.Parameters.AddWithValue("@mypass", mypassword);
                int rowsAffected = setActiveCmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Login successful");
                    var myform = new aboutartist();
                    this.Hide();
                    myform.Show();
                }
                else
                {
                    MessageBox.Show("Invalid username or password");
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                switch (ex.Number)
                {
                    case 0:
                        MessageBox.Show("Cannot connect to server");
                        break;
                    case 1045:
                        MessageBox.Show("Invalid username/password, please try again");
                        break;

                }
            }

        }


        private void loginform_Load(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void createacc_click(object sender, EventArgs e)
        {
            this.Hide();
            var createacc = new createaccount();
            createacc.ShowDialog();
        }

        private void adminsetting_click(object sender, EventArgs e)
        {
            this.Hide();
            var newadminsetting = new adminlogin();
            newadminsetting.ShowDialog();
        }
    }
}
