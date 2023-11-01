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
    public partial class Brand : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        dbconnection dbcon = new dbconnection();
        SqlDataReader dr;
 
        public Brand()
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.myconnection());
            LoadBrand();
        }
        public void LoadBrand()
        {
            int i = 0;
            gvbrand.Rows.Clear();
            cn.Open();
            cm = new SqlCommand("select * from tbbrand order by brand" , cn);
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i ++;
                gvbrand.Rows.Add(i , dr["id"].ToString(), dr["brand"].ToString());
            }
            dr.Close();
            cn.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            BrandModule bm = new BrandModule(this);
            bm.ShowDialog();
        }

        private void gvbrand_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = gvbrand.Columns[e.ColumnIndex].Name;
            if (colName == "Delete")
            {
                if (MessageBox.Show("Are you sure want to Delete this record", "Delete Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cn.Open();
                    cm = new SqlCommand("DELETE FROM  tbbrand WHERE id LIKE '" + gvbrand[1 , e.RowIndex].Value.ToString() +"'", cn);
                    cm.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("Record Has Beed Succesfully Deleted", "POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
         
            }
            else if (colName == "Edit")
            {
                BrandModule brandModule = new BrandModule(this);
                brandModule.labelid.Text = gvbrand[1, e.RowIndex].Value.ToString();
                brandModule.brandtextbox.Text = gvbrand[2, e.RowIndex].Value.ToString();
                brandModule.btnsave.Enabled = false;
                brandModule.btnupdate.Enabled = true;
                brandModule.ShowDialog();
            }
            LoadBrand();
        }
    }
}
