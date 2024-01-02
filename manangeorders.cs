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
    public partial class manangeorders1 : Form
    {
        int stock;
        SqlConnection connection = null;
        SqlCommand cmd = null;
        string constr = "Data Source=DESKTOP-TJRF2CO\\SQLEXPRESS;Initial Catalog=inventoryDB;Integrated Security=True";
        public manangeorders1()
        {
            InitializeComponent();
        }
        void fillcategory()
        {
            connection = new SqlConnection(constr);
            string query = "select * from category";
            cmd = new SqlCommand(query, connection);
            SqlDataReader sdr;
            try
            {
                connection.Open();
                DataTable dt = new DataTable();
                dt.Columns.Add("categoryname", typeof(string));
                sdr = cmd.ExecuteReader();
                dt.Load(sdr);
               
              //  combosearch.ValueMember = "categoryname";
               // combosearch.DataSource = dt;
                combosearch.ValueMember = "categoryname";
                combosearch.DataSource = dt;

                connection.Close();
            }
            catch
            {

            }

        }
       
        void updateproduct()
        {

            int id = int.Parse(txtqtyseac.Text);
            int newqty = stock - Convert.ToInt32(qtyTb.Text);
            if (newqty < 0)
            {
                MessageBox.Show("operation failed");
            }
            else
            {
                connection.Open();
                string query = "update products set productquantity='" + newqty + "' where productID='" + id + "'";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                connection.Close();
                populateproduct();
            }
        }
        int num = 0;
        int uprice, totprice, qty;
        string product;
        private void manangeorders_Load(object sender, EventArgs e)
        {
            populateproduct();
            populate();
            fillcategory();
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
                customerygv.DataSource = ds.Tables[0];
                connection.Close();
            }
            catch
            {

            }
        }
        void populateproduct()
        {
            try
            {
                connection = new SqlConnection(constr);
                connection.Open();
                string query = "select * from products";
                SqlDataAdapter da = new SqlDataAdapter(query, connection);
                SqlCommandBuilder builder = new SqlCommandBuilder(da);
                var ds = new DataSet();
                da.Fill(ds);
                productgv.DataSource = ds.Tables[0];
                connection.Close();
            }
            catch
            {

            }
        }
        private void customerygv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtcusid.Text = customerygv.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtcusName.Text = customerygv.Rows[e.RowIndex].Cells[1].Value.ToString();
        }
        int flag = 0;

        

        private void productgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtqtyseac.Text = productgv.Rows[e.RowIndex].Cells[2].Value.ToString();
            
            product = productgv.Rows[e.RowIndex].Cells[1].Value.ToString();
            //qty = Convert.ToInt32(qtyTb.Text);
            stock= Convert.ToInt32(productgv.Rows[e.RowIndex].Cells[2].Value.ToString());
            uprice = Convert.ToInt32(productgv.Rows[e.RowIndex].Cells[3].Value.ToString());
            // totprice = qty * uprice;
            flag = 1;
        }
        int sum = 0;

        private void btnsearch_Click(object sender, EventArgs e)
        {
            
            if (qtyTb.Text == "")
            {
                MessageBox.Show("Enter the quantity of product");

            }
            
           else if (flag == 0)
            {
                MessageBox.Show("Select the product");
            }
            else if (Convert.ToInt32(qtyTb.Text) > stock)
            {
                MessageBox.Show("No enough stock Available");
            }
            else
            {
                num = num + 1;
                qty = Convert.ToInt32(qtyTb.Text);
                totprice = qty * uprice;

                orderGv.Rows.Add(num, product, qty, uprice, totprice);
                flag = 0;
            }
            sum = sum + totprice;
            totalAmount.Text =  sum.ToString();
            updateproduct();
        }

        private void orderGv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(txtorderid.Text=="" || txtcusName.Text == ""|| txtcusid.Text == ""|| totalAmount.Text == "")
            {
                MessageBox.Show("Enter data Correctly");
            }
            else
            {
                try
                {
                    connection = new SqlConnection(constr);
                    connection.Open();
                    string query = "insert into orderTb values('" + txtorderid.Text + "','" + txtcusid.Text + "','" + txtcusName.Text + "','" + dateTimePicker1.Value.ToString() + "'," + sum + ")";
                    cmd = new SqlCommand(query, connection);
                    cmd.ExecuteNonQuery();
                    connection.Close();
                    MessageBox.Show("Order Added successfully");
                    //populate();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            viewOrders view = new viewOrders();
            view.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Home home = new Home();
            home.Show();
            this.Hide();
        }

        private void txtorderid_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtqtyseac_TextChanged(object sender, EventArgs e)
        {
        
        }

        private void combosearch_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void combosearch_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                connection = new SqlConnection(constr);
                connection.Open();
                string query = "select * from products where CatName='" + combosearch.SelectedValue.ToString() + "'";
                SqlDataAdapter da = new SqlDataAdapter(query, connection);
                SqlCommandBuilder builder = new SqlCommandBuilder(da);
                var ds = new DataSet();
                da.Fill(ds);
                productgv.DataSource = ds.Tables[0];
                connection.Close();
            }
            catch
            {

            }
        }
    }
}
