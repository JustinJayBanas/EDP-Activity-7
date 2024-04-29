using MySql.Data.MySqlClient;
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
    public partial class PurchaseForm : Form
    {
        public PurchaseForm()
        {
            InitializeComponent();
        }

        private void home_click(object sender, EventArgs e)
        {
            this.Hide();
            var newmainlogin = new aboutartist();
            newmainlogin.ShowDialog();
        }

        private void Artwork_click(object sender, EventArgs e)
        {
            this.Hide();
            var newartwork = new Artworks();
            newartwork.ShowDialog();
        }
        private void DisplayArtworksTable()
        {
            string myConnectionString = "server=127.0.0.1;uid=root;pwd=root;database=artistangallery";
            try
            {
                using (MySqlConnection conn = new MySqlConnection(myConnectionString))
                {
                    conn.Open();
                    string sql = "SELECT * FROM artworks";
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(sql, conn))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dataGridView.DataSource = dt;
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        private void DisplayCartTable()
        {
            string myConnectionString = "server=127.0.0.1;uid=root;pwd=root;database=artistangallery";
            try
            {
                using (MySqlConnection conn = new MySqlConnection(myConnectionString))
                {
                    conn.Open();
                    string sql = "SELECT * FROM cart";
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(sql, conn))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dataGridViewCart.DataSource = dt;
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        private decimal CalculateTotalPrice()
        {
            decimal totalPrice = 0;
            DataTable dtCart = (DataTable)dataGridViewCart.DataSource;
            foreach (DataRow row in dtCart.Rows)
            {
                totalPrice += Convert.ToDecimal(row["price"]);
            }
            return totalPrice;
        }
        
        private void GenerateReport(string buyer, string buyerAddress, string orderNotes, decimal totalPrice, int totalArtworks)
        {
            
                // Generate the report here
                // You can use a third-party library like iTextSharp or a reporting tool like Crystal Reports
                // to generate the report in a PDF or other format

                // For example, here's how you could generate a simple report using a StringBuilder:
                StringBuilder report = new StringBuilder();
                report.AppendLine("Buyer: " + buyer);
                report.AppendLine("Address: " + buyerAddress);
                report.AppendLine("Order Notes: " + orderNotes);
                report.AppendLine("Total Price: " + totalPrice.ToString("C"));
                report.AppendLine("Total Artworks: " + totalArtworks);
                MessageBox.Show(report.ToString(), "Order Receipt", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            private void DataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Check if the clicked cell is within the row bounds
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                // Clear the current selection and select the entire row
                dataGridView.ClearSelection();
                dataGridView.Rows[e.RowIndex].Selected = true;
            }
        }
        private void PurchaseForm_Load(object sender, EventArgs e)
        {
            DisplayArtworksTable();
            DisplayCartTable();
        }

        private void logout_click(object sender, EventArgs e)
        {
            this.Hide();
            var newlogout = new loginform();
            newlogout.ShowDialog();
        }

        private void placeorder_click(object sender, EventArgs e)
        {
            // Get the data from the text fields
            string buyer = buyerTextField.Text.Trim();
            string buyerAddress = AddressTextField.Text.Trim();
            string orderNotes = orderNotesTextBox.Text.Trim();

            // Calculate the total price and total artworks
            decimal totalPrice = CalculateTotalPrice();
            int totalArtworks = CalculateTotalArtworks();

            // Generate the report
            GenerateReport(buyer, buyerAddress, orderNotes, totalPrice, totalArtworks);

            // Move the data to the placeorderreports table
            string myConnectionString = "server=127.0.0.1;uid=root;pwd=root;database=artistangallery";
            using (MySqlConnection conn = new MySqlConnection(myConnectionString))
            {
                conn.Open();

                // Insert the data into the placeorderreports table
                string insertSql = "INSERT INTO placeorderreports (buyer, buyeraddress, ordernotes, totalprice, totalartworks) " +
                                   "SELECT @buyer, @buyeraddress, @orderNotes, @totalPrice, @totalArtworks " +
                                   "FROM cart";
                MySqlCommand insertCmd = new MySqlCommand(insertSql, conn);
                insertCmd.Parameters.AddWithValue("@buyer", buyer);
                insertCmd.Parameters.AddWithValue("@buyeraddress", buyerAddress);
                insertCmd.Parameters.AddWithValue("@orderNotes", orderNotes);
                insertCmd.Parameters.AddWithValue("@totalPrice", totalPrice);
                insertCmd.Parameters.AddWithValue("@totalArtworks", totalArtworks);
                insertCmd.ExecuteNonQuery();

                // Insert the data into the totalsalesreport table
                string insertTotalsalesreportSql = "INSERT INTO totalsalesreport (id, artworkname, price, artistname, buyer) " +
                                   "SELECT id, artistname, artworkname, price, @buyer " +
                                   "FROM cart";
                MySqlCommand insertTotalsalesreportCmd = new MySqlCommand(insertTotalsalesreportSql, conn);
                insertTotalsalesreportCmd.Parameters.AddWithValue("@buyer", buyer);
                insertTotalsalesreportCmd.ExecuteNonQuery();

                // Delete the rows from the cart table
                string deleteSql = "DELETE FROM cart";
                MySqlCommand deleteCmd = new MySqlCommand(deleteSql, conn);
                deleteCmd.ExecuteNonQuery();

                // Refresh the cart table
                DisplayCartTable();
            }
        }
        private int CalculateTotalArtworks()
        {
            DataTable dtCart = (DataTable)dataGridViewCart.DataSource;
            int totalArtworks = dtCart.Rows.Count;
            return totalArtworks;
        }
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Addtocart(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView.SelectedRows[0];
                object[] rowData = new object[selectedRow.Cells.Count];
                for (int i = 0; i < selectedRow.Cells.Count; i++)
                {
                    rowData[i] = selectedRow.Cells[i].Value;
                }

                // Add the row to the DataTable of dataGridViewCart
                DataTable dtCart = (DataTable)dataGridViewCart.DataSource;
                DataRow newRow = dtCart.Rows.Add(rowData);

                // Remove the row from the DataTable of dataGridView
                DataTable dtArtworks = (DataTable)dataGridView.DataSource;
                dtArtworks.Rows.Remove(((DataRowView)selectedRow.DataBoundItem).Row);

                // Update the database
                string myConnectionString = "server=127.0.0.1;uid=root;pwd=root;database=artistangallery";
                using (MySqlConnection conn = new MySqlConnection(myConnectionString))
                {
                    conn.Open();

                    // Insert the row into the cart table
                    string insertSql = "INSERT INTO cart (id, artistname, artworkname, price) VALUES (@id, @artistname, @artworkname, @price)";
                    MySqlCommand insertCmd = new MySqlCommand(insertSql, conn);
                    insertCmd.Parameters.AddWithValue("@id", rowData[0]);
                    insertCmd.Parameters.AddWithValue("@artistname", rowData[1]);
                    insertCmd.Parameters.AddWithValue("@artworkname", rowData[2]);
                    insertCmd.Parameters.AddWithValue("@price", rowData[3]);
                    insertCmd.ExecuteNonQuery();

                    // Delete the row from the artworks table
                    string deleteSql = "DELETE FROM artworks WHERE id = @id";
                    MySqlCommand deleteCmd = new MySqlCommand(deleteSql, conn);
                    deleteCmd.Parameters.AddWithValue("@id", rowData[0]);
                    deleteCmd.ExecuteNonQuery();
                }
            }
            else
            {
                MessageBox.Show("Please select a row to move.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
    
}
