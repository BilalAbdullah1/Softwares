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
    public partial class UserAccount : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        dbconnection dbcon = new dbconnection();

        public UserAccount()
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.myconnection());
        }
        public void Clear()
        {
            usernametxtbox.Clear();
            passwordtxtbox.Clear();
            fullnametextbox.Clear();
            retypepwtxtbox.Clear();

            rolecombobox.SelectedValue = 0;
            btnsave.Enabled = true;

        }
        private void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                if (passwordtxtbox.Text != retypepwtxtbox.Text)
                {
                    MessageBox.Show("Your Password is not match", "Re-write password Again");
                    return;
                }
                
                    if (MessageBox.Show("Are you sure want to save this Product", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        cn.Open();
                        cm = new SqlCommand("INSERT INTO tbuser (username , password , role, name ) values (@username,@password,@role,@name)", cn);
                        cm.Parameters.AddWithValue("@username", usernametxtbox.Text);
                        cm.Parameters.AddWithValue("@password", passwordtxtbox.Text);
                        cm.Parameters.AddWithValue("@role", rolecombobox.Text);
                        cm.Parameters.AddWithValue("@name", fullnametextbox.Text);
                        cm.ExecuteNonQuery();
                        cn.Close();
                        MessageBox.Show("Record has been Succesfully Saved", "POS");
                        Clear();
                        //product.LoadProduct();
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
