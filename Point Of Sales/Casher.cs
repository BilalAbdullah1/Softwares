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
    public partial class Casher : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        dbconnection dbcon = new dbconnection();
        SqlDataReader dr;
        public Casher()
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.myconnection());

        }
            public void LoadCart()
        {
            int i = 0;
            double total = 0;
            double discount = 0;
            gvcash.Rows.Clear();
            cn.Open();
            cm = new SqlCommand("SELECT c.id , c.pcode , p.pdesc ,c.price ,c.qty , c.disc , c.total FROM tbcart AS c INNER JOIN tbprodcut AS p ON c.pcode = pcode WHERE c.transno LIKE @transno and c.status 'Pending'", cn);
            cm.Parameters.AddWithValue("@transno", lbltransno.Text);
            dr = cm.ExecuteReader();
            while(dr.Read()) {
                i++;
                total += Convert.ToDouble(dr["total"].ToString());
                discount += Convert.ToDouble(dr["disc"].ToString());
                gvcash.Rows.Add(i, dr["id"].ToString(), dr["pcode"].ToString(), dr["pdesc"].ToString(), dr["price"].ToString(), dr["qty"].ToString(), dr["disc"].ToString(), double.Parse(dr["total"].ToString()).ToString("#,## 0.00"));
            }
            dr.Close();
            cn.Close();
            labelsaletotal.Text = total.ToString("#,## 0.00");
            labeldiscount.Text = discount.ToString("#,## 0.00");
            GetCartTotal();
        }

            public void GetCartTotal()
        {
            double discount = double.Parse(labeldiscount.Text);
            double sales = double.Parse(labelsaletotal.Text) - discount;
            double vat =  sales * 0.12;
            double vatable = sales - vat;

        }
        private void btnclose_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure want to exit this Applicaton", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {   
            lbltimer.Text = DateTime.Now.ToString("hh:mm:ss tt");
        }

        private void btndashboard_Click(object sender, EventArgs e)
        {
            GetTrade();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
        public void GetTrade()
        {
            string sdate = DateTime.Now.ToString("yyyyMMdd");
            string transon = sdate + "1001";
            lbltransno.Text = transon;
        }

        private void btnsearch_Click(object sender, EventArgs e)
        {
            LookupProduct lookup = new LookupProduct(this);
            lookup.LoadProduct();
            lookup.ShowDialog();
        }
    }
}
