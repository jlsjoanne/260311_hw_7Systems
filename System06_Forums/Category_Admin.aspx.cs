using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _260311_hw_7Systems.System06_Forums
{
    public partial class Category_Admin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["RoleId"] != null && Session["RoleId"].ToString() == "1")
            {

            }
            else if (Request.UrlReferrer != null)
            {
                Response.Redirect(Request.UrlReferrer.ToString());
            }
            else
            {
                Response.Redirect("CategoryList.aspx");
            }
        }

        protected void AddAdmin_Click(object sender, EventArgs e)
        {
            Response.Redirect("Category_Admin_Add.aspx");
        }
    }
}