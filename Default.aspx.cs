using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _260311_hw_7Systems
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["LogInStatus"] != null && Session["LogInStatus"].ToString() == "true")
            {
                LogIn.Visible = false;
                SignUp.Visible = false;
                LogOut.Visible = true;
                Welcome.Text += $" {Session["UserName"].ToString()}";
            }
        }

        protected void LogIn_Click(object sender, EventArgs e)
        {
            Response.Redirect("/User/LogIn.aspx");
        }

        protected void SignUp_Click(object sender, EventArgs e)
        {
            Response.Redirect("/User/SignUp.aspx");
        }

        protected void LogOut_Click(object sender, EventArgs e)
        {
            Welcome.Text = "Welcome!";
            Session["LogInStatus"] = null;
            Session["UserName"] = null;
            Session["RoleId"] = null;
            Session["RoleName"] = null;
            LogIn.Visible = true;
            SignUp.Visible = true;
            Response.Redirect("Default.aspx");
        }
    }
}