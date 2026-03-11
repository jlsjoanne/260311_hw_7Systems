using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _260311_hw_7Systems.User
{
    public partial class Setting : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["LogInStatus"] != null && Session["LogInStatus"].ToString() == "true")
            {
                UserName.Visible = true;
                UserName.Text += Session["UserName"].ToString();
                RoleName.Text += Session["RoleName"].ToString();
            }
            else
            {
                UserName.Visible = false;
                RoleName.Text = "尚未登入";
            }
            
            if (Session["RoleId"] != null && Session["RoleId"].ToString() == "1")
            {
                Management.Visible = true;
            }
        }

        protected void Management_Click(object sender, EventArgs e)
        {
            Response.Redirect("Management.aspx");
        }
    }
}