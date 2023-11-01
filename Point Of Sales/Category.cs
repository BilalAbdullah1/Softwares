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
    public partial class Category : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        dbconnection dbcon = new dbconnection();
        SqlDataReader dr;
        public Category()
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.myconnection());
            LoadCategory();
        }
        public void LoadCategory()
        {
            int i = 0;
            gvcategory.Rows.Clear();
            cn.Open();
            cm = new SqlCommand("select * from tbcategory order by category", cn);
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                gvcategory.Rows.Add(i, dr["id"].ToString(), dr["category"].ToString());
            }
            dr.Close();
            cn.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CategoryModule bm = new CategoryModule(this);
            bm.ShowDialog();
        }

        private void gvcategory_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = gvcategory.Columns[e.ColumnIndex].Name;
            if (colName == "Delete")
            {
                if (MessageBox.Show("Are you sure want to Delete this record", "Delete Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cn.Open();
                    cm = new SqlCommand("DELETE FROM tbcategory WHERE id LIKE '" + gvcategory[1, e.RowIndex].Value.ToString() + "'", cn);
                    cm.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("Record Has Beed Succesfully Deleted", "POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            else if (colName == "Edit")
            {
                CategoryModule CategoryModule = new CategoryModule(this);
                CategoryModule.labelid.Text = gvcategory[1, e.RowIndex].Value.ToString();
                CategoryModule.categorytxtbox.Text = gvcategory[2, e.RowIndex].Value.ToString();
                CategoryModule.btnsave.Enabled = false;
                CategoryModule.btnupdate.Enabled = true;
                CategoryModule.ShowDialog();
            }
            LoadCategory();
        }
    }
}
