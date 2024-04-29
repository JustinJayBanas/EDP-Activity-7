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
    public partial class Changepassform : Form
    {
        public Changepassform()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void changepass_click(object sender, EventArgs e)
        {
            string myusername = this.txtusername.Text;
            string animal = this.txtanimal.Text; // Assuming txtanimal is a TextBox for entering the animal associated with the account

            MySql.Data.MySqlClient.MySqlConnection conn;
            string myConnectionString;
            myConnectionString = "server=127.0.0.1;uid=root;" +
            "pwd=root;database=artistartwork";
            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection();
                conn.ConnectionString = myConnectionString;
                conn.Open();
                string sql = "SELECT COUNT(*) from account where username = @myuser AND animal = @animal";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@myuser", myusername);
                cmd.Parameters.AddWithValue("@animal", animal);
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                if (count > 0)
                {
                    sql = "SELECT pass from account where username = @myuser";
                    cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@myuser", myusername);
                    string password = cmd.ExecuteScalar().ToString();
                    MessageBox.Show("Your password is: " + password, "Password Recovery");
                }
                else
                {
                    MessageBox.Show("Invalid username or animal");
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


        private void Changepassform_Load(object sender, EventArgs e)
        {

        }

        private void login_click(object sender, EventArgs e)
        {
            this.Hide();
            var newlogin = new loginform();
            newlogin.ShowDialog();
        }

        private void adminrecovery_click(object sender, EventArgs e)
        {
            {
                string myusername = this.txtusername.Text;
                string animal = this.txtanimal.Text; // Assuming txtanimal is a TextBox for entering the animal associated with the account

                MySql.Data.MySqlClient.MySqlConnection conn;
                string myConnectionString;
                myConnectionString = "server=127.0.0.1;uid=root;" +
                "pwd=root;database=artistartwork";
                try
                {
                    conn = new MySql.Data.MySqlClient.MySqlConnection();
                    conn.ConnectionString = myConnectionString;
                    conn.Open();
                    string sql = "SELECT COUNT(*) from adminaccount where username = @myuser AND animal = @animal";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@myuser", myusername);
                    cmd.Parameters.AddWithValue("@animal", animal);
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    if (count > 0)
                    {
                        sql = "SELECT pass from adminaccount where username = @myuser";
                        cmd = new MySqlCommand(sql, conn);
                        cmd.Parameters.AddWithValue("@myuser", myusername);
                        string password = cmd.ExecuteScalar().ToString();
                        MessageBox.Show("Your password is: " + password, "Password Recovery");
                    }
                    else
                    {
                        MessageBox.Show("Invalid username or animal");
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
        }
    }
}
