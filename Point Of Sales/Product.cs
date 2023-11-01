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

namespace Point_Of_Sales
{
    public partial class Product : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        dbconnection dbcon = new dbconnection();
        SqlDataReader dr;
        public Product()
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.myconnection());
            LoadProduct();
        }
        public void LoadProduct()
        {
            int i = 0;
            gvproduct.Rows.Clear();
            cn.Open();
            cm = new SqlCommand("SELECT p.prcode, p.barcode, p.pdesc, b.brand, c.category, p.price, p.reorder FROM tbproduct AS p INNER JOIN tbbrand AS b ON p.bid = b.id INNER JOIN tbcategory AS c ON p.cid = c.id WHERE CONCAT(p.pdesc, b.brand, c.category) LIKE '%" + searchTxtBox.Text + "%'", cn);
            dr = cm.ExecuteReader(); 
            while (dr.Read())
            {
                i++;
                gvproduct.Rows.Add(i , dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(), dr[6].ToString());
            }
            dr.Close();
            cn.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ProductModule productmodule = new ProductModule(this);
            productmodule.ShowDialog();
        }

        private void gvproduct_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = gvproduct.Columns[e.ColumnIndex].Name;
            if (colName == "Delete")
            {
                if (MessageBox.Show("Are you sure want to Delete this record", "Delete Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cn.Open();
                    cm = new SqlCommand("DELETE FROM tbproduct WHERE prcode LIKE '" + gvproduct[1, e.RowIndex].Value.ToString() + "'", cn);
                    cm.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("Record Has Beed Succesfully Deleted", "POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if (colName == "Edit")
            {
                ProductModule productmodule = new ProductModule(this);
                productmodule.productcodetxt.Text = gvproduct[1, e.RowIndex].Value.ToString();
                productmodule.barcodetxt.Text = gvproduct[2, e.RowIndex].Value.ToString();
                productmodule.descriptiontxt.Text = gvproduct[3, e.RowIndex].Value.ToString();
                productmodule.pricetextbox.Text = gvproduct[6, e.RowIndex].Value.ToString();
                productmodule.brandcmbobox.Text = gvproduct[4, e.RowIndex].Value.ToString();
                productmodule.categorycombobox.Text = gvproduct[5, e.RowIndex].Value.ToString();
                productmodule.reorderleveltxtbox.Value = int.Parse( gvproduct[7, e.RowIndex].Value.ToString());

                productmodule.productcodetxt.Enabled= false;
                productmodule.btnsave.Enabled = false;
                productmodule.btnupdate.Enabled = true;
                productmodule.ShowDialog();
            }
            LoadProduct();
        }

        private void searchTxtBox_Click(object sender, EventArgs e)
        {
            LoadProduct();
        }
    }
}
