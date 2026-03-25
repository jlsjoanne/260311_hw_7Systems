using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Data;

namespace _260311_hw_7Systems.System05_Video
{
    public partial class VideoList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["RoleId"] != null && (Session["RoleId"].ToString() == "1" || Session["RoleId"].ToString() == "2"))
            {
                AddNew.Visible = true;
            }
        }

        protected void AddNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("Video_Add.aspx");
        }
    }
}