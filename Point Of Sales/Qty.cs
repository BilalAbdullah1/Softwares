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
    public partial class Qty : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        dbconnection dbcon = new dbconnection();
        SqlDataReader dr;
        private string pcode;
        private double price;
        private string transno;
        private int qty;
        Casher cashier;
        public Qty(Casher casher)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.myconnection());
            cashier = casher;

        }

        public void ProductDetails(string pcode , double price , string transno , int qty)
        {
            this.pcode = pcode;
            this.price = price;
            this.transno = transno;
            this.qty = qty;
        }

        private void qtytxtbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == 27) && (qtytxtbox.Text != string.Empty))
            {
                    cn.Open();
                    cm = new SqlCommand("Insert into tbcart (transno,pcode, price, qty,sdate, cashier) values (@transno,@pcode,@price,@qty,@sdate,@cashier)", cn);
                    cm.Parameters.AddWithValue("@transno",transno);
                    cm.Parameters.AddWithValue("@pcode", pcode);
                    cm.Parameters.AddWithValue("@price", price);
                    cm.Parameters.AddWithValue("@qty", int.Parse(qtytxtbox.Text));
                    cm.Parameters.AddWithValue("@sdate", DateTime.Now);
                    cm.Parameters.AddWithValue("@cashier", cashier.lblusername.Text);
                    cm.ExecuteNonQuery();
                    cn.Close();
                cashier.LoadCart();
                this.Dispose();
                
            }
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            this.Dispose(true);
        }
    }
}
