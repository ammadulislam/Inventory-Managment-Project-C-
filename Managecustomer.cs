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

namespace MypProjectInventorySystem
{
    public partial class Managecustomer : Form
    {
        SqlConnection connection = null;
        SqlCommand cmd = null;
        string constr = "Data Source=DESKTOP-TJRF2CO\\SQLEXPRESS;Initial Catalog=inventoryDB;Integrated Security=True";
        public Managecustomer()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        void populate()
        {
            try
            {
                connection = new SqlConnection(constr);
                connection.Open();
                string query = "select * from customer";
                SqlDataAdapter da = new SqlDataAdapter(query, connection);
                SqlCommandBuilder builder = new SqlCommandBuilder(da);
                var ds = new DataSet();
                da.Fill(ds);
                customergv.DataSource = ds.Tables[0];
                connection.Close();
            }
            catch
            {

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                connection = new SqlConnection(constr);
                connection.Open();
                string query = "insert into customer values('" + txtxusID.Text + "','" + txtname.Text + "','" + txtcusphone.Text + "')";
                cmd = new SqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("customer Added successfully");
                populate();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                connection = new SqlConnection(constr);
                connection.Open();
                string query = "update customer set CustomerID='" + txtxusID.Text + "',Customername='" + txtname.Text + "',customerphone='" + txtcusphone.Text + "' where CustomerID='" + txtxusID.Text + "' ";
                cmd = new SqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("customer updated successfully");
                populate();
            }
            catch
            {

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (txtxusID.Text == "")
            {
                MessageBox.Show("Enter customer ID");
                txtxusID.Focus();
            }
            else
            {
                connection = new SqlConnection(constr);
                connection.Open();
                string query = "delete from customer where CustomerID='" + txtxusID.Text + "'";
                cmd = new SqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("customer deleted successfully");
                populate();
            }
        }

        private void customergv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtxusID.Text = customergv.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtname.Text = customergv.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtcusphone.Text = customergv.Rows[e.RowIndex].Cells[2].Value.ToString();
            connection.Open();
            SqlDataAdapter sda = new SqlDataAdapter("Select Count(*) from orderTb where Customerid=" + txtxusID.Text + "", connection);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            orderlabel.Text = dt.Rows[0][0].ToString();

            SqlDataAdapter sda1 = new SqlDataAdapter("Select Sum(Sum) from orderTb where Customerid=" + txtxusID.Text + "", connection);
            DataTable dt1 = new DataTable();
            sda1.Fill(dt1);
            amountlabel.Text = dt1.Rows[0][0].ToString();

            SqlDataAdapter sda2 = new SqlDataAdapter("Select Max(Date) from orderTb where Customerid=" + txtxusID.Text + "", connection);
            DataTable dt2 = new DataTable();
            sda2.Fill(dt2);
            datelabel.Text = dt2.Rows[0][0].ToString();
            connection.Close();
            
        }

        private void Managecustomer_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Home home = new Home();
            home.Show();
            this.Hide();
        }

        private void datelabel_Click(object sender, EventArgs e)
        {

        }

        private void orderlabel_Click(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
