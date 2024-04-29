using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SampleUI
{
    public partial class Artworks : Form
    {
        public Artworks()
        {
            InitializeComponent();
        }

        private void Artworks_Load(object sender, EventArgs e)
        {

        }

        private void home_click(object sender, EventArgs e)
        {
            this.Hide();
            var newhome = new aboutartist();
            newhome.ShowDialog();
        }

        private void logout_click(object sender, EventArgs e)
        {
            this.Hide();
            var newlogout = new loginform();
            newlogout.ShowDialog();
        }

        private void purchase_click(object sender, EventArgs e)
        {
            this.Hide();
            var newpurchase = new PurchaseForm();
            newpurchase.ShowDialog();
        }
    }
}
