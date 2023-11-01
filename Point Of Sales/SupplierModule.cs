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
    public partial class SupplierModule : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        dbconnection dbcon = new dbconnection();
        Supplier Supplier;
        public SupplierModule(Supplier spl)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.myconnection());
            Supplier = spl;
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            this.Dispose(); 
        }
        public void Clear()
        {
            supplierTxtBox.Clear();
            AddressTextBOx.Clear();
            ContactPersonTxtBOx.Clear();
            phonenotxtbox.Clear();
            faxtxtbox.Clear();
            emailaddresstxtbox.Clear();
            supplierTxtBox.Enabled = true;
            supplierTxtBox.Focus();
            btnsave.Enabled = true;
            btnupdate.Enabled = false;

        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure want to save this Supplier", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cn.Open();
                    cm = new SqlCommand("INSERT INTO tbsupplier(suppliername, address, contactperson, phonenumber, email, fax) values (@suppliername,@address,@contactperson,@phonenumber,@email,@fax)", cn);
                    cm.Parameters.AddWithValue("@suppliername", supplierTxtBox.Text);
                    cm.Parameters.AddWithValue("@address", AddressTextBOx.Text);
                    cm.Parameters.AddWithValue("@contactperson", ContactPersonTxtBOx.Text);
                    cm.Parameters.AddWithValue("@phonenumber", phonenotxtbox.Text);
                    cm.Parameters.AddWithValue("@email", emailaddresstxtbox.Text);
                    cm.Parameters.AddWithValue("@fax", faxtxtbox.Text);
                    cm.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("Record has been Succesfully Saved", "POS");
                    Clear();
                    Supplier.LoadSupplier();
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
                if (MessageBox.Show("Are you sure want to update this Supplier", "Update Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cn.Open();
                    cm = new SqlCommand("UPDATE tbsupplier SET suppliername = @suppliername, address = @address, contactperson = @contactperson ,phonenumber =@phonenumber ,email = @email , fax = @fax where id like '"+labelid.Text+"'", cn);
                    cm.Parameters.AddWithValue("@suppliername", supplierTxtBox.Text);
                    cm.Parameters.AddWithValue("@address", AddressTextBOx.Text);
                    cm.Parameters.AddWithValue("@contactperson", ContactPersonTxtBOx.Text);
                    cm.Parameters.AddWithValue("@phonenumber", phonenotxtbox.Text);
                    cm.Parameters.AddWithValue("@email", emailaddresstxtbox.Text);
                    cm.Parameters.AddWithValue("@fax", faxtxtbox.Text);
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
