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
    public partial class deleteacc : Form
    {
        public deleteacc()
        {
            InitializeComponent();
        }

        private void loginform_click(object sender, EventArgs e)
        {
            this.Hide();
            var newloginform = new loginform();
            newloginform.ShowDialog();
        }

        private void deleteacc_click(object sender, EventArgs e)
        {
            int userid = int.Parse(this.txtuserid.Text); // Assuming txtuserid is a TextBox for entering userid

            MySql.Data.MySqlClient.MySqlConnection conn;
            string myConnectionString;
            myConnectionString = "server=127.0.0.1;uid=root;" +
            "pwd=root;database=artistartwork";
            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection();
                conn.ConnectionString = myConnectionString;
                conn.Open();
                string selectSql = "SELECT username, pass FROM account WHERE userid = @userid";
                MySqlCommand selectCmd = new MySqlCommand(selectSql, conn);
                selectCmd.Parameters.AddWithValue("@userid", userid);
                MySqlDataReader reader = selectCmd.ExecuteReader();
                string deletedUsername = "";
                string deletedPassword = "";
                if (reader.Read())
                {
                    deletedUsername = reader.GetString(0);
                    deletedPassword = reader.GetString(1);
                }
                reader.Close();

                string deleteSql = "DELETE FROM account WHERE userid = @userid";
                MySqlCommand deleteCmd = new MySqlCommand(deleteSql, conn);
                deleteCmd.Parameters.AddWithValue("@userid", userid);
                int rowsAffected = deleteCmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show($"Account deleted successfully\nUsername: {deletedUsername}\nPassword: {deletedPassword}");
                }
                else
                {
                    MessageBox.Show("Failed to delete account");
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
                        MessageBox.Show("Error deleting account: " + ex.Message);
                        break;
                }
            }

        }

    }
}
