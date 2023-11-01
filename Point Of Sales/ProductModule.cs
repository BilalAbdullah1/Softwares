using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Point_Of_Sales
{
    public partial class ProductModule : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        dbconnection dbcon = new dbconnection();
        Product product;
        public ProductModule( Product prd)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.myconnection());
            product = prd;
            LoadCategory();
            LoadBrand();    
        }

        public void LoadCategory()
        {
            categorycombobox.Items.Clear();
            categorycombobox.DataSource = dbcon.getTable("select * from tbcategory");
            categorycombobox.DisplayMember = "category";
            categorycombobox.ValueMember = "id";
        }
        public void LoadBrand()
        {
            brandcmbobox.Items.Clear();
            brandcmbobox.DataSource = dbcon.getTable("select * from tbbrand");
            brandcmbobox.DisplayMember = "brand";
            brandcmbobox.ValueMember= "id";
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        public void Clear()
        {
            productcodetxt.Clear();
            barcodetxt.Clear() ;
            descriptiontxt.Clear() ;
            pricetextbox.Clear();
            brandcmbobox.SelectedValue = 0;
            categorycombobox.SelectedValue = 0;
            reorderleveltxtbox.Value = 1;
            productcodetxt.Enabled= true;
            productcodetxt.Focus();
            btnsave.Enabled= true;
            btnupdate.Enabled= false;

        }
        private void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure want to save this Product", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cn.Open();
                    cm = new SqlCommand("INSERT INTO tbproduct(prcode , barcode , pdesc, bid ,cid ,price , reorder) values (@prcode,@barcode,@pdesc,@bid,@cid , @price ,@reorder)", cn);
                    cm.Parameters.AddWithValue("@prcode", productcodetxt.Text);
                    cm.Parameters.AddWithValue("@barcode", barcodetxt.Text);
                    cm.Parameters.AddWithValue("@pdesc", descriptiontxt.Text);
                    cm.Parameters.AddWithValue("@bid", brandcmbobox.SelectedValue);
                    cm.Parameters.AddWithValue("@cid", categorycombobox.SelectedValue);
                    cm.Parameters.AddWithValue("@price", double.Parse(pricetextbox.Text));
                    cm.Parameters.AddWithValue("@reorder", reorderleveltxtbox.Text);
                    cm.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("Record has been Succesfully Saved", "POS");
                    Clear();
                    product.LoadProduct();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure want to update this Product", "Update Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cn.Open();
                    cm = new SqlCommand("UPDATE tbproduct SET barcode = @barcode, pdesc = @pdesc, bid = @bid ,cid =@cid ,price = @price , reorder = @reorder where prcode like @prcode", cn);
                    cm.Parameters.AddWithValue("@barcode", barcodetxt.Text);
                    cm.Parameters.AddWithValue("@pdesc", descriptiontxt.Text);
                    cm.Parameters.AddWithValue("@bid", brandcmbobox.SelectedValue);
                    cm.Parameters.AddWithValue("@cid", categorycombobox.SelectedValue);
                    cm.Parameters.AddWithValue("@price", double.Parse(pricetextbox.Text));
                    cm.Parameters.AddWithValue("@reorder", reorderleveltxtbox.Value);
                    cm.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("Record has been Succesfully Updated", "POS");
                    Clear();
                    this.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btncancel_Click(object sender, EventArgs e)
        {
            Clear();
        }
    }
}
