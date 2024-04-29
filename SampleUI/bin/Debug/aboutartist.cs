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
    public partial class aboutartist : Form
    {
        public aboutartist()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void aboutprogram_Load(object sender, EventArgs e)
        {

        }

        private void logout_click(object sender, EventArgs e)
        {
			this.Hide();
			var newlogout = new loginform();
			newlogout.ShowDialog();
		}

        private void artwork_click(object sender, EventArgs e)
        {
            this.Hide();
            var newartwork = new Artworks();
            newartwork.ShowDialog();
        }

        private void puchase_click(object sender, EventArgs e)
        {
            this.Hide();
            var newpurchase = new PurchaseForm();
            newpurchase.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            MySql.Data.MySqlClient.MySqlConnection conn;
            string myConnectionString;
            myConnectionString = "server=127.0.0.1;uid=root;" +
            "pwd=root;database=artistartwork";
            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection();
                conn.ConnectionString = myConnectionString;
                conn.Open();
                MessageBox.Show("Connected");
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
