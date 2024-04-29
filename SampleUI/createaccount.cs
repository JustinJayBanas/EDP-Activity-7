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
    public partial class createaccount : Form
    {
        public createaccount()
        {
            InitializeComponent();
        }

        private void createaccount_Load(object sender, EventArgs e)
        {

        }

        private void createacc_click(object sender, EventArgs e)
        {
            string myusername = this.txtusername.Text;
            string mypassword = this.txtpassword.Text;
            string animal = this.txtanimal.Text;
            int userid = int.Parse(this.txtuserid.Text); // Assuming txtuserid is a TextBox for entering userid

            MySql.Data.MySqlClient.MySqlConnection conn;
            string myConnectionString;
            myConnectionString = "server=127.0.0.1;uid=root;" +
            "pwd=root;database=artistangallery";
            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection();
                conn.ConnectionString = myConnectionString;
                conn.Open();
                string sql = "INSERT INTO account (userid, username, pass, animal) VALUES (@userid, @myuser, @mypass, @animal)";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@userid", userid);
                cmd.Parameters.AddWithValue("@myuser", myusername);
                cmd.Parameters.AddWithValue("@mypass", mypassword);
                cmd.Parameters.AddWithValue("@animal", animal);
                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Account created successfully");
                }
                else
                {
                    MessageBox.Show("Failed to create account");
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                switch (ex.Number)
                {
                    case 0:
                        MessageBox.Show("Cannot connect to server");
                        break;
                    case 1062:
                        MessageBox.Show("Username already exists");
                        break;
                    default:
                        MessageBox.Show("Error creating account: " + ex.Message);
                        break;
                }
            }

        }




        private void existacc_click(object sender, LinkLabelLinkClickedEventArgs e)
            {
                this.Hide();
                var existacc = new loginform();
                existacc.ShowDialog();
            }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void createaddmin_click(object sender, EventArgs e)
        {
            {
                string myusername = this.txtusername.Text;
                string mypassword = this.txtpassword.Text;
                string animal = this.txtanimal.Text;
                int userid = int.Parse(this.txtuserid.Text); // Assuming txtuserid is a TextBox for entering userid

                MySql.Data.MySqlClient.MySqlConnection conn;
                string myConnectionString;
                myConnectionString = "server=127.0.0.1;uid=root;" +
                "pwd=root;database=artistangallery";
                try
                {
                    conn = new MySql.Data.MySqlClient.MySqlConnection();
                    conn.ConnectionString = myConnectionString;
                    conn.Open();
                    string sql = "INSERT INTO adminaccount (adminid, username, pass, animal) VALUES (@adminid, @myuser, @mypass, @animal)";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@adminid", userid);
                    cmd.Parameters.AddWithValue("@myuser", myusername);
                    cmd.Parameters.AddWithValue("@mypass", mypassword);
                    cmd.Parameters.AddWithValue("@animal", animal);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Account created successfully");
                    }
                    else
                    {
                        MessageBox.Show("Failed to create account");
                    }
                }
                catch (MySql.Data.MySqlClient.MySqlException ex)
                {
                    switch (ex.Number)
                    {
                        case 0:
                            MessageBox.Show("Cannot connect to server");
                            break;
                        case 1062:
                            MessageBox.Show("Username already exists");
                            break;
                        default:
                            MessageBox.Show("Error creating account: " + ex.Message);
                            break;
                    }
                }

            }
        }
    }
    } 

