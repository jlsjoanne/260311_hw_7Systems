using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Data.SqlClient;

namespace _260311_hw_7Systems.System01_News
{
    public partial class News_List : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["RoleId"] != null && (Session["RoleId"].ToString() == "1" || Session["RoleId"].ToString() == "2"))
            {
                AddNew.Visible = true;
                NewsMgmt.Visible = true;
            }
            if (!IsPostBack)
            {
                BindNewsGrid();
            }
        }

        protected void AddNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("News_Add.aspx");
        }

        protected void NewsMgmt_Click(object sender, EventArgs e)
        {
            Response.Redirect("News_List_Mgmt.aspx");
        }

        protected void NewsGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            NewsGrid.PageIndex = e.NewPageIndex;
            BindNewsGrid();
        }

        private void BindNewsGrid()
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["NewsDB"].ConnectionString;
            string getNewsQuery = "SELECT NewsId, NewsTitle, PostDate, CategoryName FROM [NewsList] AS N "+
                "LEFT JOIN [Category] AS C ON N.CategoryId = C.CategoryId "+
                "WHERE N.IsPublish = 1 " +
                "ORDER BY PostDate DESC, CategoryOrder ASC";

            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                using(SqlCommand command = new SqlCommand(getNewsQuery, conn))
                {
                    try
                    {
                        conn.Open();
                        SqlDataAdapter da = new SqlDataAdapter(command);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        NewsGrid.DataSource = dt;
                        NewsGrid.DataBind();
                    }
                    catch (Exception ex)
                    {
                        Response.Write($"<script>alert('{ex.Message}')</script>");
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
        }
    }
}