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
    public partial class ProductStockIn : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        dbconnection dbcon = new dbconnection();
        SqlDataReader dr;
        StockEntry stck;
        public ProductStockIn()
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
            cm = new SqlCommand("SELECT pcode , pdesc , qty from tbproduct where pdesc LIKE '" +searchTxtBox.Text+ "' ", cn);
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                gvproduct.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString());
            }
            dr.Close();
            cn.Close();
        }

        private void gvproduct_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = gvproduct.Columns[e.ColumnIndex].Name;
            if (colName == "Select")
            {
                if (MessageBox.Show("Add this Item?","POS", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cn.Open();
                    cm = new SqlCommand("Insert into tbstockin (refno,pcode, sdate, stockinby,supplierid) values (@refno,@pcode,@sdate,@stockinby,@supplierid)", cn);
                    cm.Parameters.AddWithValue("@refno",stck.referencetxtbox.Text);
                    cm.Parameters.AddWithValue("@pcode", gvproduct.Rows[e.RowIndex].Cells[1].Value.ToString());
                    cm.Parameters.AddWithValue("@sdate", stck.stockdate.Value);
                    cm.Parameters.AddWithValue("@stockinby", stck.stockinbytxtbox.Text);
                    cm.Parameters.AddWithValue("@supplierid", stck.labelid.Text);
                    cm.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("Record Has Beed Succesfully Deleted", "POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}
