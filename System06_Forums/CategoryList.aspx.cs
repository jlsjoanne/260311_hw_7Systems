using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _260311_hw_7Systems.System06_Forums
{
    public partial class CategoryList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["RoleId"] != null && Session["RoleId"].ToString() == "1")
            {
                AddCategory.Visible = true;
                if (GridView1.Columns[0] is CommandField commandField1)
                {
                    commandField1.ShowEditButton = true;
                    commandField1.ShowDeleteButton = true;
                }
            }
        }

        protected void AddCategory_Click(object sender, EventArgs e)
        {
            Response.Redirect("Category_Add.aspx");
        }
    }
}