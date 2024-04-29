using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace SampleUI
{
    public partial class adminlogin : Form
    {
        public adminlogin()
        {
            InitializeComponent();
        }

        private void adminsetting_click(object sender, EventArgs e)
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
                string updateSql = "UPDATE adminaccount SET active = 0";
                MySqlCommand updateCmd = new MySqlCommand(updateSql, conn);
                updateCmd.ExecuteNonQuery();

                // Set the logged-in account to active
                string setActiveSql = "UPDATE adminaccount SET active = 1 WHERE username = @myuser AND pass = @mypass";
                MySqlCommand setActiveCmd = new MySqlCommand(setActiveSql, conn);
                setActiveCmd.Parameters.AddWithValue("@myuser", myusername);
                setActiveCmd.Parameters.AddWithValue("@mypass", mypassword);
                int rowsAffected = setActiveCmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Login successful");
                    var myform = new adminsetting();
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

        private void createacc_click(object sender, EventArgs e)
        {
            this.Hide();
            var newaccount = new createaccount();
            newaccount.ShowDialog();
        }

        private void adminlogin_Load(object sender, EventArgs e)
        {

        }
    }
}
