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
using Excel = Microsoft.Office.Interop.Excel;

namespace SampleUI
{
    public partial class Reports : Form
    {
        public Reports()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Reports_Load(object sender, EventArgs e)
        {
            DisplayPlaceOrdersTable();
            DisplayItemInventoryTable();
            DisplayTotalSalesTable();
        }
        private void DisplayPlaceOrdersTable()
        {
            string myConnectionString = "server=127.0.0.1;uid=root;pwd=root;database=artistangallery";
            try
            {
                using (MySqlConnection conn = new MySqlConnection(myConnectionString))
                {
                    conn.Open();
                    string sql = "SELECT * FROM placeorderreports";
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(sql, conn))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dataGridViewPlaceOrders.DataSource = dt;
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        private void DisplayItemInventoryTable()
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
                        dataGridViewInventory.DataSource = dt;
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        private void DisplayTotalSalesTable()
        {
            string myConnectionString = "server=127.0.0.1;uid=root;pwd=root;database=artistangallery";
            try
            {
                using (MySqlConnection conn = new MySqlConnection(myConnectionString))
                {
                    conn.Open();
                    string sql = "SELECT * FROM totalsalesreport";
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(sql, conn))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dataGridViewTotalSales.DataSource = dt;
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }



        private void ExportPlaceOrders(object sender, EventArgs e)
        {
            // Check if the DataGridView is null
            if (dataGridViewPlaceOrders == null)
            {
                MessageBox.Show("DataGridView is not initialized.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Specify the existing Excel file to which you want to append the data
            string existingExcelFile = "C:\\Users\\Teacher\\source\\repos\\SampleUI\\SampleUI\\PlaceOrdersList.xlsx";

            // Create an instance of the Excel application
            Excel.Application excelApp = new Excel.Application();

            // Open the existing workbook
            Excel.Workbook workbook = excelApp.Workbooks.Open(existingExcelFile);

            // Get the "Sheet1" and "PlaceOrders" worksheets
            Excel.Worksheet sheet1 = (Excel.Worksheet)workbook.Sheets["Sheet1"];
            Excel.Worksheet Invoice = (Excel.Worksheet)workbook.Sheets["Commercial Invoice"];

            // Specify the starting cell for the exported data (B5)
            int startRow = 9;
            int startCol = 2;

            // Copy the DataGridView rows to the "Sheet1" worksheet
            for (int i = 0; i < dataGridViewPlaceOrders.RowCount; i++)
            {
                // Write buyer, total artworks, and total price to the worksheet
                sheet1.Cells[startRow + i, startCol].Value = dataGridViewPlaceOrders.Rows[i].Cells["buyer"].Value?.ToString();
                sheet1.Cells[startRow + i, startCol + 1].Value = dataGridViewPlaceOrders.Rows[i].Cells["totalartworks"].Value?.ToString();
                sheet1.Cells[startRow + i, startCol + 2].Value = dataGridViewPlaceOrders.Rows[i].Cells["totalprice"].Value?.ToString();
            }

            // Copy the DataGridView rows to the "PlaceOrders" worksheet
            for (int i = 0; i < dataGridViewPlaceOrders.RowCount; i++)
            {
                // Write buyer, total artworks, and total price to the worksheet
                Invoice.Cells[startRow + i, startCol].Value = dataGridViewPlaceOrders.Rows[i].Cells["buyer"].Value?.ToString();
                Invoice.Cells[startRow + i, startCol + 1].Value = dataGridViewPlaceOrders.Rows[i].Cells["buyeraddress"].Value?.ToString();
                Invoice.Cells[startRow + i, startCol + 2].Value = dataGridViewPlaceOrders.Rows[i].Cells["ordernotes"].Value?.ToString();
                Invoice.Cells[startRow + i, startCol + 3].Value = dataGridViewPlaceOrders.Rows[i].Cells["totalartworks"].Value?.ToString();
                Invoice.Cells[startRow + i, startCol + 4].Value = dataGridViewPlaceOrders.Rows[i].Cells["totalprice"].Value?.ToString();
            }

            // Create a chart on the "Sheet1" worksheet
            Excel.ChartObjects charts = (Excel.ChartObjects)sheet1.ChartObjects();
            Excel.ChartObject chartObj = charts.Add(100, 100, 400, 300);
            Excel.Chart chart = chartObj.Chart;

            // Set the chart data
            Excel.Range chartRange = sheet1.Range[sheet1.Cells[startRow, startCol + 1], sheet1.Cells[startRow + dataGridViewPlaceOrders.RowCount - 1, startCol + 1]];
            chart.SetSourceData(chartRange);

            // Add series for "total price" on the left axis
            Excel.Series series1 = chart.SeriesCollection().NewSeries();
            series1.Values = sheet1.Range[sheet1.Cells[startRow, startCol + 2], sheet1.Cells[startRow + dataGridViewPlaceOrders.RowCount - 1, startCol + 2]];
            series1.XValues = chartRange;
            series1.AxisGroup = Excel.XlAxisGroup.xlPrimary;
            series1.ChartType = Excel.XlChartType.xlColumnClustered;
            series1.Name = "Total Price";
            chart.Axes(Excel.XlAxisType.xlValue, Excel.XlAxisGroup.xlPrimary).HasTitle = true;
            chart.Axes(Excel.XlAxisType.xlValue, Excel.XlAxisGroup.xlPrimary).AxisTitle.Text = "Total Price";

            // Add series for "buyer" on the right axis
            Excel.Series series2 = chart.SeriesCollection().NewSeries();
            series2.Values = sheet1.Range[sheet1.Cells[startRow, startCol], sheet1.Cells[startRow + dataGridViewPlaceOrders.RowCount - 1, startCol]];
            series2.XValues = chartRange;
            series2.AxisGroup = Excel.XlAxisGroup.xlSecondary;
            series2.ChartType = Excel.XlChartType.xlColumnClustered;
            series2.Name = "Buyer";
            chart.Axes(Excel.XlAxisType.xlValue, Excel.XlAxisGroup.xlSecondary).HasTitle = true;
            chart.Axes(Excel.XlAxisType.xlValue, Excel.XlAxisGroup.xlSecondary).AxisTitle.Text = "Buyer";

            // Set the chart type to clustered column chart
            chart.ChartType = Excel.XlChartType.xlColumnClustered;

            // Set the chart title
            chart.HasTitle = true;
            chart.ChartTitle.Text = "Buyer vs Total Artworks vs Total Price";

            // Save the changes to the existing Excel file
            workbook.Save();

            // Close the workbook and the Excel application
            workbook.Close();
            excelApp.Quit();
        }

        private void ExportInventory(object sender, EventArgs e)
        {
            // Check if the DataGridView is null
            if (dataGridViewInventory == null)
            {
                MessageBox.Show("DataGridView is not initialized.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Specify the existing Excel file to which you want to append the data
            string existingExcelFile = "C:\\Users\\Teacher\\source\\repos\\SampleUI\\SampleUI\\Inventory list.xlsx";

            // Create an instance of the Excel application
            Excel.Application excelApp = new Excel.Application();

            // Open the existing workbook
            Excel.Workbook workbook = excelApp.Workbooks.Open(existingExcelFile);

            // Get the specific worksheet (e.g., Sheet1)
            Excel.Worksheet worksheet = (Excel.Worksheet)workbook.Sheets["Sheet1"];
            Excel.Worksheet inventory = (Excel.Worksheet)workbook.Sheets["Inventory List"];

            // Specify the starting cell for the exported data (B5)
            int startRow = 12;
            int startCol = 2;

            // Copy the DataGridView rows to the worksheet
            for (int i = 0; i < dataGridViewInventory.RowCount; i++)
            {
                for (int j = 0; j < dataGridViewInventory.ColumnCount; j++)
                {
                    worksheet.Cells[startRow + i, startCol + j].Value = dataGridViewInventory.Rows[i].Cells[j].Value?.ToString();
                }
            }
            for (int i = 0; i < dataGridViewInventory.RowCount; i++)
            {
                for (int j = 0; j < dataGridViewInventory.ColumnCount; j++)
                {
                    inventory.Cells[startRow + i, startCol + j].Value = dataGridViewInventory.Rows[i].Cells[j].Value?.ToString();
                }
            }

            // Create a chart
            Excel.ChartObjects charts = (Excel.ChartObjects)worksheet.ChartObjects();
            Excel.ChartObject chartObj = charts.Add(100, 100, 400, 300);
            Excel.Chart chart = chartObj.Chart;

            // Set the chart data
            Excel.Range chartRange = worksheet.Range[worksheet.Cells[startRow, startCol + 2], worksheet.Cells[startRow + dataGridViewInventory.RowCount - 1, startCol + 3]];
            chart.SetSourceData(chartRange);

            // Add series for "price" and "artistname"
            Excel.Series series1 = chart.SeriesCollection().NewSeries();
            series1.Values = worksheet.Range[worksheet.Cells[startRow, startCol + 2], worksheet.Cells[startRow + dataGridViewInventory.RowCount - 1, startCol + 2]];
            series1.Name = "Price";

            // Set the chart type to clustered column chart
            chart.ChartType = Excel.XlChartType.xlColumnClustered;

            // Set the chart title
            chart.HasTitle = true;
            chart.ChartTitle.Text = "Artwork Prices by Artist";

            // Set the category labels (artist names) on the bottom axis
            chart.Axes(Excel.XlAxisType.xlCategory, Excel.XlAxisGroup.xlPrimary).CategoryNames = worksheet.Range[worksheet.Cells[startRow, startCol + 3], worksheet.Cells[startRow + dataGridViewInventory.RowCount - 1, startCol + 3]];

            // Save the changes to the existing Excel file
            workbook.Save();

            // Close the workbook and the Excel application
            workbook.Close();
            excelApp.Quit();
        }

        private void ExportSales(object sender, EventArgs e)
        {
            // Check if the DataGridView is null
            if (dataGridViewTotalSales == null)
            {
                MessageBox.Show("DataGridView is not initialized.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Specify the existing Excel file to which you want to append the data
            string existingExcelFile = "C:\\Users\\Teacher\\source\\repos\\SampleUI\\SampleUI\\SaleReport.xlsx";

            // Create an instance of the Excel application
            Excel.Application excelApp = new Excel.Application();

            // Open the existing workbook
            Excel.Workbook workbook = excelApp.Workbooks.Open(existingExcelFile);

            // Get the specific worksheet (e.g., Sheet1)
            Excel.Worksheet worksheet = (Excel.Worksheet)workbook.Sheets["Sheet1"];
            Excel.Worksheet sales = (Excel.Worksheet)workbook.Sheets["Sales data"];

            // Specify the starting cell for the exported data (B5)
            int startRow = 7;
            int startCol = 3;

            // Copy the DataGridView rows to the worksheet
            for (int i = 0; i < dataGridViewTotalSales.RowCount; i++)
            {
                for (int j = 0; j < dataGridViewTotalSales.ColumnCount; j++)
                {
                    worksheet.Cells[startRow + i, startCol + j].Value = dataGridViewTotalSales.Rows[i].Cells[j].Value?.ToString();
                }
            }
            for (int i = 0; i < dataGridViewTotalSales.RowCount; i++)
            {
                for (int j = 0; j < dataGridViewTotalSales.ColumnCount; j++)
                {
                    sales.Cells[startRow + i, startCol + j].Value = dataGridViewTotalSales.Rows[i].Cells[j].Value?.ToString();
                }
            }

            // Create a chart
            Excel.ChartObjects charts = (Excel.ChartObjects)worksheet.ChartObjects();
            Excel.ChartObject chartObj = charts.Add(100, 100, 400, 300);
            Excel.Chart chart = chartObj.Chart;

            // Set the chart data
            Excel.Range chartRange = worksheet.Range[worksheet.Cells[startRow, startCol + 2], worksheet.Cells[startRow + dataGridViewTotalSales.RowCount - 1, startCol + 3]];
            chart.SetSourceData(chartRange);

            // Add series for "price" and "artistname"
            Excel.Series series1 = chart.SeriesCollection().NewSeries();
            series1.Values = worksheet.Range[worksheet.Cells[startRow, startCol + 2], worksheet.Cells[startRow + dataGridViewTotalSales.RowCount - 1, startCol + 2]];
            series1.Name = "Price";

            // Set the chart type to clustered column chart
            chart.ChartType = Excel.XlChartType.xlColumnClustered;

            // Set the chart title
            chart.HasTitle = true;
            chart.ChartTitle.Text = "Total Sales by Artist";

            // Set the category labels (artist names) on the bottom axis
            chart.Axes(Excel.XlAxisType.xlCategory, Excel.XlAxisGroup.xlPrimary).CategoryNames = worksheet.Range[worksheet.Cells[startRow, startCol + 3], worksheet.Cells[startRow + dataGridViewTotalSales.RowCount - 1, startCol + 3]];

            // Save the changes to the existing Excel file
            workbook.Save();

            // Close the workbook and the Excel application
            workbook.Close();
            excelApp.Quit();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
    }
}
