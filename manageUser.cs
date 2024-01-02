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
    public partial class manageUser : Form
    {
        SqlConnection connection = null;
        SqlCommand cmd = null;
        string constr = "Data Source=DESKTOP-TJRF2CO\\SQLEXPRESS;Initial Catalog=inventoryDB;Integrated Security=True";
        public manageUser()
        {
            InitializeComponent();
        }

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

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
                string query = "select * from manageuser";
                SqlDataAdapter da = new SqlDataAdapter(query, connection);
                SqlCommandBuilder builder = new SqlCommandBuilder(da);
                var ds = new DataSet();
                da.Fill(ds);
                usergv.DataSource = ds.Tables[0];
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
                string query = "insert into manageuser values('" + txtuser.Text + "','" + txtfull.Text + "','" + txtpassword.Text + "','" + txtphone.Text + "')";
                cmd = new SqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("user Added successfully");
                populate();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void manageUser_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void usergv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtuser.Text = usergv.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtfull.Text = usergv.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtpassword.Text = usergv.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtphone.Text = usergv.Rows[e.RowIndex].Cells[3].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (txtphone.Text == "")
            {
                MessageBox.Show("Enter phone number");
                txtphone.Focus();
            }
            else
            {
                connection = new SqlConnection(constr);
                connection.Open();
                string query="delete from manageuser where uphone='"+txtphone.Text+"'";
                cmd = new SqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("user deleted successfully");
                populate();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                connection = new SqlConnection(constr);
                connection.Open();
                string query = "update manageuser set F_Name='" + txtfull.Text + "',L_Name='" + txtuser.Text + "',Password='" + txtpassword.Text + "',Phone_No='" + txtphone.Text + "' where uphone='" + txtphone.Text + "' ";
                cmd = new SqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("user updated successfully");
                populate();
            }
            catch
            {

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Home home = new Home();
            this.Hide();
            home.Show();
            
        }
    }
}
