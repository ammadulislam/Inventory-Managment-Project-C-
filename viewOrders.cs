using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DGVPrinterHelper;
using System.IO;

namespace MypProjectInventorySystem
{
    public partial class viewOrders : Form
    {
        SqlConnection connection = null;
        SqlCommand cmd = null;
        string constr = "Data Source=DESKTOP-TJRF2CO\\SQLEXPRESS;Initial Catalog=inventoryDB;Integrated Security=True";
        public viewOrders()
        {
            InitializeComponent();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void viewOrders_Load(object sender, EventArgs e)
        {
            populateOrders();
        }
        void populateOrders()
        {
            try
            {
                connection = new SqlConnection(constr);
                connection.Open();
                string query = "select * from orderTb";
                SqlDataAdapter da = new SqlDataAdapter(query, connection);
                SqlCommandBuilder builder = new SqlCommandBuilder(da);
                var ds = new DataSet();
                da.Fill(ds);
                ordersgv.DataSource = ds.Tables[0];
                connection.Close();
            }
            catch
            {

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void ordersgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtORid.Text = ordersgv.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtcusId.Text = ordersgv.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtcusName.Text = ordersgv.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtdate.Text = ordersgv.Rows[e.RowIndex].Cells[3].Value.ToString();
            txtamount.Text = ordersgv.Rows[e.RowIndex].Cells[4].Value.ToString();
            if (printPreviewDialog1.ShowDialog()==DialogResult.OK) {

                printDocument1.Print();
            }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
           
            e.Graphics.DrawString("Order Summary", new Font("Century", 25, FontStyle.Bold), Brushes.Red, new Point(250));
            e.Graphics.DrawString("Order ID: "+txtORid.Text, new Font("Century", 25, FontStyle.Regular), Brushes.Black, new Point(80,100));
            e.Graphics.DrawString("Customer ID: "+txtcusId.Text, new Font("Century", 25, FontStyle.Regular), Brushes.Black, new Point(80,150));
            e.Graphics.DrawString("Customer Name: "+txtcusName.Text, new Font("Century", 25, FontStyle.Regular), Brushes.Black, new Point(80,200));
            e.Graphics.DrawString("Order Date: "+txtdate.Text, new Font("Century", 25, FontStyle.Regular), Brushes.Black, new Point(80,250));
            e.Graphics.DrawString("Order Amount: "+txtamount.Text, new Font("Century", 25, FontStyle.Regular), Brushes.Black, new Point(80,300));
            e.Graphics.DrawString("PoweredByAmmadShopCente", new Font("Century", 25, FontStyle.Bold), Brushes.BlueViolet, new Point(180,400));

        }

        private void button2_Click(object sender, EventArgs e)
        {
            int index = ordersgv.Rows.Count - 1;
            DGVPrinter printer = new DGVPrinter();
            printer.Title = "Employee File";
            printer.SubTitle = "WellCome";
            // printer.SubTitle =string.Format("Date:{0}",DateTime.Now.Date);
            printer.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
            printer.PageNumbers = true;
            printer.PageNumberInHeader = false;
            printer.PorportionalColumns = true;
            printer.HeaderCellAlignment = StringAlignment.Near;
            printer.Footer = string.Format("Date:{0}", DateTime.Now.Date);
            printer.FooterSpacing = 15;
            printer.PrintDataGridView(ordersgv);

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string msg;
            FileStream f = new FileStream("C:\\Users\\HP\\Desktop\\text.txt", FileMode.Append,
                FileAccess.Write);
            StreamWriter sw = new StreamWriter(f);

            for (int i = 0; i < ordersgv.Rows.Count - 1; i++)
            {
                
                msg = ordersgv.Rows[i].Cells[0].Value.ToString() + "," + ordersgv.Rows[i].Cells[1].Value.ToString() + "," + ordersgv.Rows[i].Cells[2].Value.ToString() + "," + ordersgv.Rows[i].Cells[3].Value.ToString()+ "," + ordersgv.Rows[i].Cells[3].Value.ToString()+  ";"+Environment.NewLine;

                sw.WriteLine(msg);

            }
           
            MessageBox.Show("Successfull");
            sw.Close();
            f.Close();
        }
    }
}