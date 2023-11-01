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
    public partial class StockEntry : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        dbconnection dbcon = new dbconnection();
        SqlDataReader dr;

        public StockEntry()
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.myconnection());
            LoadSupplier();
            //GetRef();
        }
        public void LoadSupplier()
        {
            suplliertxtbox.Items.Clear();
            suplliertxtbox.DataSource = dbcon.getTable("select * from tbsupplier");
            suplliertxtbox.DisplayMember = "suppliername";
        }

        public void GetRef()
        {
            Random rmd = new Random();
            referencetxtbox.Clear();
            referencetxtbox.Text += rmd.Next();
        }
        private void suplliertxtbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            cn.Open();
            cm = new SqlCommand("select * from tbsupplier where suppliername LIKE '" + suplliertxtbox.Text + "'", cn);
            dr = cm.ExecuteReader();    
            dr.Read();
            if (dr.HasRows)
            {
                labelid.Text = dr["id"].ToString();
                contactprstxtbox.Text = dr["contactperson"].ToString();
                addresstxtbox.Text = dr["address"].ToString();
            }
            dr.Close();
            cn.Close();
        }

        private void suplliertxtbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void metroLink1_Click(object sender, EventArgs e)
        {
            GetRef();
        }
    }
}
