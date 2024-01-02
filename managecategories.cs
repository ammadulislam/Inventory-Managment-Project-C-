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
    public partial class managecategories : Form
    {
        SqlConnection connection = null;
        SqlCommand cmd = null;
        string constr = "Data Source=DESKTOP-TJRF2CO\\SQLEXPRESS;Initial Catalog=inventoryDB;Integrated Security=True";
        public managecategories()
        {
            InitializeComponent();
        }
        void populate()
        {
            try
            {
                connection = new SqlConnection(constr);
                connection.Open();
                string query = "select * from category";
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

        private void btnadd_Click(object sender, EventArgs e)
        {
            try
            {
                connection = new SqlConnection(constr);
                connection.Open();
                string query = "insert into category values('" + txtid.Text + "','" + txtname.Text + "')";
                cmd = new SqlCommand(query, connection);
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

        private void btnedit_Click(object sender, EventArgs e)
        {
            try
            {
                connection = new SqlConnection(constr);
                connection.Open();
                string query = "update category set categoryID='" + txtid.Text + "',categoryname='" + txtname.Text + "' where categoryID='" + txtid.Text + "' ";
                cmd = new SqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("category updated successfully");
                populate();
            }
            catch
            {

            }
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            if (txtid.Text == "")
            {
                MessageBox.Show("Enter category ID");
                txtid.Focus();
            }
            else
            {
                connection = new SqlConnection(constr);
                connection.Open();
                string query = "delete from category where categoryID='" + txtid.Text + "'";
                cmd = new SqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("category deleted successfully");
                populate();
            }
        }

        private void managecategories_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void categorygv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtid.Text = categorygv.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtname.Text = categorygv.Rows[e.RowIndex].Cells[1].Value.ToString();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnhome_Click(object sender, EventArgs e)
        {
            Home home = new Home();
            home.Show();
            this.Hide();
        }
    }
}