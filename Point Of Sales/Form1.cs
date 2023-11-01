using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Point_Of_Sales
{
    public partial class Form1 : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        dbconnection dbcon = new dbconnection();
        public Form1()
        {
            InitializeComponent();
            CustomDesign();
            cn = new SqlConnection(dbcon.myconnection());
            cn.Open();
            //MessageBox.Show("Data Base Connected");

        }
        private Form activeform = null; 
        public void openChildren (Form ChildForm)
        {
            if (activeform != null)
                activeform.Close();
            activeform = ChildForm;
            ChildForm.TopLevel = false;
            ChildForm.FormBorderStyle = FormBorderStyle.None;
            ChildForm.Dock =DockStyle.Fill;
            labelTitle.Text = ChildForm.Text;
            mainpannel.Controls.Add(ChildForm);
            mainpannel.Tag = ChildForm;
            ChildForm.BringToFront();
            ChildForm.Show();
            

            
        }
        #region MenuBar
        private void CustomDesign()
        {
            panelsubproduct.Visible = false;
            panelsubrecord.Visible = false;
            panelsubsetting.Visible = false;
            panelsubstock.Visible = false;
        }
        private void HideSubMenu()
        {
            if (panelsubproduct.Visible == true)
                panelsubproduct.Visible = false;
            if (panelsubrecord.Visible == true)
                panelsubrecord.Visible = false;
            if (panelsubsetting.Visible == true)
                panelsubsetting.Visible = false;
            if (panelsubstock.Visible == true)
                panelsubsetting.Visible = false;
        }

        private void ShowSubMenu(Panel Submenu)
        {
            if (Submenu.Visible == false)
            {
                HideSubMenu();
                Submenu.Visible = true;
            }
            else
            {
                Submenu.Visible = false;
            }
        }
        #endregion MenuBar
        private void panel8_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnProduct_Click(object sender, EventArgs e)
        {
            ShowSubMenu(panelsubproduct);
        }

        private void btnproductlist_Click(object sender, EventArgs e)
        {
            openChildren(new Product());
            HideSubMenu();
        }

        private void btncatagory_Click(object sender, EventArgs e)
        {
            openChildren(new Category());
            HideSubMenu();
        }

        private void btnbrand_Click(object sender, EventArgs e)
        {
            openChildren(new Brand());
            HideSubMenu();
        }

        private void btninstock_Click(object sender, EventArgs e)
        {
            ShowSubMenu(panelsubstock);
        }

        private void btnstockentry_Click(object sender, EventArgs e)
        {
            HideSubMenu();
        }

        private void btnstockadjustment_Click(object sender, EventArgs e)
        {
            HideSubMenu();
        }

        private void btnrecord_Click(object sender, EventArgs e)
        {
            ShowSubMenu(panelsubrecord);
        }

        private void btnsalehistory_Click(object sender, EventArgs e)
        {
            HideSubMenu();
        }

        private void btnposrecord_Click(object sender, EventArgs e)
        {
            HideSubMenu();
        }

        private void btnsetting_Click(object sender, EventArgs e)
        {
            ShowSubMenu(panelsubsetting);
        }

        private void btnuser_Click(object sender, EventArgs e)
        {
            openChildren(new UserAccount());
            HideSubMenu();
        }

        private void btnstore_Click(object sender, EventArgs e)
        {

            HideSubMenu();
        }

        private void Setting_Click(object sender, EventArgs e)
        {
            ShowSubMenu(panelsubsetting);
        }

        private void btnsupplier_Click(object sender, EventArgs e)
        {
            openChildren(new Supplier());

        }
    }
}
