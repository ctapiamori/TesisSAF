using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SOCAUD.Web
{
    public partial class test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection cnx = new SqlConnection("Server=localhost;DataBase=wi141955_socaud;User Id=wi141955_upc_tesis;password=Abc12345678;Integrated Security=SSPI;"))
            //using (SqlConnection cnx = new SqlConnection("Server=localhost;DataBase=SI_SOCAUD;Integrated Security=SSPI;"))
            {
                using (SqlCommand cmd = new SqlCommand("select * from SAF_USUARIO", cnx))
                {
                    cnx.Open();
                    cmd.Connection = cnx;
                    cmd.CommandType = System.Data.CommandType.Text;

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                }
            }
        }
    }
}