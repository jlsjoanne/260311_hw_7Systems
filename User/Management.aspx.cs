using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Data.SqlClient;

namespace _260311_hw_7Systems.User
{
    public partial class Management : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["RoleId"] == null)
            {
                Response.Redirect("../Default.aspx");
            }
            else if(Session["RoleId"] != null && Session["RoleId"].ToString() != "1")
            {
                Response.Write("<script>alert('帳號無權限')</script>");
                Response.Redirect("Setting.aspx");
            }
            else if(Session["RoleId"] != null && Session["RoleId"].ToString() == "1")
            {
                GridView1.Visible = true;
            }

            if (!IsPostBack)
            {
                
            }
            
        }


    }
}