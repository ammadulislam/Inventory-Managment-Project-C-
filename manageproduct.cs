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
    public partial class manageproduct : Form
    {
        SqlConnection connection = null;
        SqlCommand cmd = null;
        string constr = "Data Source=DESKTOP-TJRF2CO\\SQLEXPRESS;Initial Catalog=inventoryDB;Integrated Security=True";
        public manageproduct()
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
                dt.Load(sdr);
                Catcombo.ValueMember = "categoryname";
                Catcombo.DataSource = dt;
                combosearch.ValueMember = "categoryname";
                combosearch.DataSource = dt;

                connection.Close();
            }
                catch
                {

                }
            
        }
       
        private void Button1_Click(object sender, EventArgs e)
        {

        }
        void populate()
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
                categorygv.DataSource = ds.Tables[0];
                connection.Close();
            }
            catch
            {

            }
        }
        void filterbycategory()
        {
            try
            {
                connection = new SqlConnection(constr);
                connection.Open();
                string query = "select * from products where Productcatgory='" + combosearch.SelectedValue.ToString()+"'";
                SqlDataAdapter da = new SqlDataAdapter(query, connection);
                SqlCommandBuilder builder = new SqlCommandBuilder(da);
                var ds = new DataSet();
                da.Fill(ds);
                categorygv.DataSource = ds.Tables[0];
                connection.Close();
            }
            catch
            {

            }
        }
        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                connection = new SqlConnection(constr);
                connection.Open();
                string query = "insert into products values('" + txtid.Text + "','" + txtname.Text + "','" + txtqty.Text + "','" + txtprice.Text + "','" + txtdescription.Text + "','" + Catcombo.SelectedValue.ToString() + "')";
                cmd = new SqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand("product", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ActionType", "insert");
                cmd.Parameters.AddWithValue("@pname", txtname.Text);
                cmd.Parameters.AddWithValue("@pqty", txtqty.Text);
                cmd.Parameters.AddWithValue("@price", txtprice.Text);
                cmd.Parameters.AddWithValue("@des", txtdescription.Text);
                cmd.Parameters.AddWithValue("@cat", Catcombo.SelectedItem);
                cmd.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("category Added successfully");
                populate();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

       

        private void button2_Click(object sender, EventArgs e)
        {
            if (txtid.Text == "")
            {
                MessageBox.Show("Enter ProductID ID");
                txtid.Focus();
            }
            else
            {
                connection = new SqlConnection(constr);
                connection.Open();
                //string query = "delete from products where productID='" + txtid.Text + "'";
                //cmd = new SqlCommand(query, connection);
                cmd = new SqlCommand("product", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ActionType", "delete");
                cmd.Parameters.AddWithValue("@pid", txtid.Text);
                
                cmd.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("product deleted successfully");
                populate();
            }
        }

        private void categorygv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtid.Text = categorygv.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtname.Text = categorygv.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtqty.Text = categorygv.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtprice.Text = categorygv.Rows[e.RowIndex].Cells[3].Value.ToString();
            txtdescription.Text = categorygv.Rows[e.RowIndex].Cells[4].Value.ToString();
            Catcombo.Text = categorygv.Rows[e.RowIndex].Cells[5].Value.ToString();
        }

        private void manageproduct_Load(object sender, EventArgs e)
        {
            fillcategory();
            populate();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnsearch_Click(object sender, EventArgs e)
        {
            filterbycategory();


        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            populate();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Home home = new Home();
            home.Show();
            this.Hide();
        }

        private void btnedit_Click(object sender, EventArgs e)
        {
            try
            {
                connection = new SqlConnection(constr);
                connection.Open();
                string query = "update products set productname='" + txtname.Text + "',productquantity='" + txtqty.Text + "',productprice='" + txtprice.Text + "',productdesc='" + txtdescription.Text + "','" + Catcombo.SelectedValue.ToString() + "' where productID='" + txtid.Text + "'";
                cmd = new SqlCommand(query, connection);
                //cmd = new SqlCommand("product", connection);
                //cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@ActionType", "update");
                //cmd.Parameters.AddWithValue("@pid", txtname.Text);
                //cmd.Parameters.AddWithValue("@pname", txtqty.Text);
                //cmd.Parameters.AddWithValue("@pqty", txtprice.Text);
                //cmd.Parameters.AddWithValue("@des", txtdescription.Text);
                //cmd.Parameters.AddWithValue("@cat", Catcombo.SelectedItem);





                cmd.ExecuteNonQuery();
               
                MessageBox.Show("product updated successfully");
                connection.Close();
                populate();
            }
            catch(Exception ex)
            {
                    MessageBox.Show(ex.Message);
                }
        }

        private void combosearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            //}
            //if (combosearch.SelectedIndex >= 0)
            //{
            //    if (combosearch.SelectedItem == "Mobile")
            //    {
            //        fillcategory();
            //    }
            //    if(combosearch.SelectedItem == "Loptop")
            //    {
            //        fillcategory();

            //    }

            //    if (combosearch.SelectedItem == "Electronic")
            //    {

            //    }s



            try
            {
                connection = new SqlConnection(constr);
                connection.Open();
                string query = "select * from products where Productcatgory='" + combosearch.SelectedValue.ToString() + "'";
                SqlDataAdapter da = new SqlDataAdapter(query, connection);
                SqlCommandBuilder builder = new SqlCommandBuilder(da);
                var ds = new DataSet();
                da.Fill(ds);
                categorygv.DataSource = ds.Tables[0];
                connection.Close();
            }
            catch
            {
            }
            



            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}