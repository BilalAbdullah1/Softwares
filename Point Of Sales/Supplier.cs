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
    public partial class Supplier : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        dbconnection dbcon = new dbconnection();
        SqlDataReader dr;
        
        public Supplier()
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.myconnection());
            LoadSupplier();
        }
        public void LoadSupplier()
        {
            int i = 0;
            gvsupplier.Rows.Clear();
            cn.Open();
            cm = new SqlCommand("SELECT * FROM tbsupplier", cn);
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                gvsupplier.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(), dr[6].ToString());
            }
            dr.Close();
            cn.Close();
        }

        private void gvsupplier_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = gvsupplier.Columns[e.ColumnIndex].Name;
            if (colName == "Delete")
            {
                if (MessageBox.Show("Are you sure want to Delete this record", "Delete Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cn.Open();
                    cm = new SqlCommand("DELETE FROM tbsupplier WHERE id LIKE '" + gvsupplier[1, e.RowIndex].Value.ToString() + "'", cn);
                    cm.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("Record Has Beed Succesfully Deleted", "POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if (colName == "Edit")
            {
                SupplierModule SupplierModule = new SupplierModule(this);
                SupplierModule.labelid.Text = gvsupplier[1, e.RowIndex].Value.ToString();
                SupplierModule.supplierTxtBox.Text = gvsupplier[2, e.RowIndex].Value.ToString();
                SupplierModule.AddressTextBOx.Text = gvsupplier[3, e.RowIndex].Value.ToString();
                SupplierModule.ContactPersonTxtBOx.Text = gvsupplier[4, e.RowIndex].Value.ToString();
                SupplierModule.phonenotxtbox.Text = gvsupplier[5, e.RowIndex].Value.ToString();
                SupplierModule.emailaddresstxtbox.Text = gvsupplier[6, e.RowIndex].Value.ToString();
                SupplierModule.faxtxtbox.Text = gvsupplier[7, e.RowIndex].Value.ToString();

                SupplierModule.btnsave.Enabled = false;
                SupplierModule.btnupdate.Enabled = true;
                SupplierModule.ShowDialog();
            }
            LoadSupplier();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SupplierModule productmodule = new SupplierModule(this);
            productmodule.ShowDialog();
        }
    }
}
