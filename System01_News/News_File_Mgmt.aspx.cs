using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _260311_hw_7Systems.System01_News
{
    public partial class News_File_Mgmt : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["NewsId"] != null)
            {
                if (Session["RoleId"] != null && (Session["RoleId"].ToString() == "1" || Session["RoleId"].ToString() == "2"))
                {
                    AddFile.Visible = true;
                    FileGrid.Visible = true;
                }
            }
        }
    }
}