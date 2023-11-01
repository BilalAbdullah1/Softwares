using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Point_Of_Sales
{
    class dbconnection
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();

        private string con;
        public string myconnection()
        {
            con = @"Data Source=DESKTOP-HQ68A96\SQLEXPRESS;Initial Catalog=dbpointofsale;Integrated Security=True";
            return con;
        }
        public DataTable getTable (string qry )
        {
            cn.ConnectionString = myconnection();
            cm = new SqlCommand(qry, cn );
            SqlDataAdapter adapter = new SqlDataAdapter(cm);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
        }
    }
}
