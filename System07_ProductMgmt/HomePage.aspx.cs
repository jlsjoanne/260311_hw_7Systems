using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _260311_hw_7Systems.System07_ProductMgmt
{
    public partial class HomePage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["LogInStatus"] != null && Session["LogInStatus"].ToString() == "true")
            {
                AddNew.Visible = true;
            }
            if(Session["RoleId"]!= null && (Session["RoleId"].ToString() == "1" || Session["RoleId"].ToString() == "2"))
            {
                CategoryMgmt.Visible = true;
                BrandMgmt.Visible = true;
            }
        }

        protected void AddNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("ProductMgmt.aspx");
        }

        protected void CategoryMgmt_Click(object sender, EventArgs e)
        {
            Response.Redirect("CategoryMgmt.aspx");
        }

        protected void BrandMgmt_Click(object sender, EventArgs e)
        {
            Response.Redirect("BrandMgmt.aspx");
        }
    }
}