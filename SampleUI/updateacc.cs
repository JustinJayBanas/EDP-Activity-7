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
    public partial class updateacc : Form
    {
        public updateacc()
        {
            InitializeComponent();
        }

        private void updateacc_click(object sender, EventArgs e)
        {
            int userid = int.Parse(this.txtuserid.Text); // Assuming txtuserid is a TextBox for entering userid
            string myusername = this.txtusername.Text;
            string mypassword = this.txtpassword.Text;

            MySql.Data.MySqlClient.MySqlConnection conn;
            string myConnectionString;
            myConnectionString = "server=127.0.0.1;uid=root;" +
            "pwd=root;database=artistartwork";
            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection();
                conn.ConnectionString = myConnectionString;
                conn.Open();
                string sql = "UPDATE account SET username = @myuser, pass = @mypass WHERE userid = @userid";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@userid", userid);
                cmd.Parameters.AddWithValue("@myuser", myusername);
                cmd.Parameters.AddWithValue("@mypass", mypassword);
                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Account updated successfully");
                }
                else
                {
                    MessageBox.Show("Failed to update account");
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                switch (ex.Number)
                {
                    case 0:
                        MessageBox.Show("Cannot connect to server");
                        break;
                    default:
                        MessageBox.Show("Error updating account: " + ex.Message);
                        break;
                }
            }

        }

        private void login_click(object sender, EventArgs e)
        {
            this.Hide();
            var existacc = new loginform();
            existacc.ShowDialog();
        }

        private void updateacc_Load(object sender, EventArgs e)
        {

        }
    }
}
