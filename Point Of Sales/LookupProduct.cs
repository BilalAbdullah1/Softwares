using System;
using System.Collections;
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
    public partial class LookupProduct : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        dbconnection dbcon = new dbconnection();
        SqlDataReader dr;
        Casher cash;

        public LookupProduct(Casher csh)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.myconnection());
            cash = csh;
            LoadProduct();

        }

        public void LoadProduct()
        {
            int i = 0;
            gvproduct.Rows.Clear();
            cn.Open();
            cm = new SqlCommand("SELECT p.prcode, p.barcode, p.pdesc, b.brand, c.category, p.price, p.qty FROM tbproduct AS p INNER JOIN tbbrand AS b ON p.bid = b.id INNER JOIN tbcategory AS c ON p.cid = c.id WHERE CONCAT(p.pdesc, b.brand, c.category) LIKE '%" + searchTxtBox.Text + "%'", cn);
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                gvproduct.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(), dr[6].ToString());
            }
            dr.Close();
            cn.Close();
        }
        private void btncancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void gvproduct_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = gvproduct.Columns[e.ColumnIndex].Name;
            if (colName == "Select")
            {
                Qty qty = new Qty(cash);
                qty.ProductDetails(gvproduct.Rows[e.RowIndex].Cells[1].Value.ToString(), double.Parse(gvproduct.Rows[e.RowIndex].Cells[6].Value.ToString()), cash.lbltransno.Text, int.Parse(gvproduct.Rows[e.RowIndex].Cells[7].Value.ToString()));
                qty.ShowDialog();
            }
        }
    }
}
