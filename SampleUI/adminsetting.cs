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
    public partial class adminsetting : Form
    {
        public adminsetting()
        {
            InitializeComponent();
        }

        private void updateform_click(object sender, EventArgs e)
        {
            
            var newupdateacc = new updateacc();
            newupdateacc.ShowDialog();
        }

        private void deleteacc_click(object sender, EventArgs e)
        {
            
            var deleteacc = new deleteacc();
            deleteacc.ShowDialog();
        }

        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void DisplayAccountTable()
        {
            MySql.Data.MySqlClient.MySqlConnection conn;
            string myConnectionString;
            myConnectionString = "server=127.0.0.1;uid=root;" +
            "pwd=root;database=artistangallery";
            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection();
                conn.ConnectionString = myConnectionString;
                conn.Open();
                string sql = "SELECT * FROM account";
                MySqlDataAdapter adapter = new MySqlDataAdapter(sql, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dataGridView.DataSource = dt;
                conn.Close();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        private void DisplayAccountAdminTable()
        {
            MySql.Data.MySqlClient.MySqlConnection conn;
            string myConnectionString;
            myConnectionString = "server=127.0.0.1;uid=root;" +
            "pwd=root;database=artistangallery";
            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection();
                conn.ConnectionString = myConnectionString;
                conn.Open();
                string sql = "SELECT * FROM adminaccount";
                MySqlDataAdapter adapter = new MySqlDataAdapter(sql, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dataGridViewadmin.DataSource = dt;
                conn.Close();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void adminsetting_Load(object sender, EventArgs e)
        {
            DisplayAccountTable();
            DisplayAccountAdminTable();
        }

        private void login_click(object sender, EventArgs e)
        {
            this.Hide();
            var newlogin = new loginform();
            newlogin.ShowDialog();
        }

        private void refresh_click(object sender, EventArgs e)
        {
            this.Hide();
            var refresh = new adminsetting();
            refresh.ShowDialog();
        }

        private void reports_Click(object sender, EventArgs e)
        {
            var myform = new Reports();
            this.Hide();
            myform.Show();
        }
    }
}
