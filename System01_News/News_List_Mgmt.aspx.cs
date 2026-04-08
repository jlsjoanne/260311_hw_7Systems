using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Web.Configuration;
using System.Data.SqlClient;

namespace _260311_hw_7Systems.System01_News
{
    public partial class News_List_Mgmt : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["RoleId"] != null && (Session["RoleId"].ToString() == "1" || Session["RoleId"].ToString() == "2"))
            {
                if (!IsPostBack)
                {
                    BindNewsGrid();
                }
            }
            else if (Request.UrlReferrer != null)
            {
                Response.Redirect(Request.UrlReferrer.ToString());
            }
            else
            {
                Response.Redirect("News_List.aspx");
            }
        }

        private void BindNewsGrid()
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["NewsDB"].ConnectionString;
            string getNewsQuery = "SELECT NewsId, NewsTitle, PostDate, CategoryName FROM [NewsList] AS N " +
                "LEFT JOIN [Category] AS C ON N.CategoryId = C.CategoryId "+
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

        protected void NewsGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            NewsGrid.PageIndex = e.NewPageIndex;
            BindNewsGrid();
        }

        protected void NewsGrid_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string newsId = NewsGrid.DataKeys[e.RowIndex].Value.ToString();
            DeleteNewsFromDB(newsId);
            BindNewsGrid();
        }

        private void DeleteNewsFromDB(string newsId)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["NewsDB"].ConnectionString;
            string deleteNewsQuery = "DELETE FROM [NewsList] WHERE NewsId = @NewsId";

            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                using(SqlCommand command = new SqlCommand(deleteNewsQuery, conn))
                {
                    command.Parameters.AddWithValue("@NewsId", newsId);
                    try
                    {
                        conn.Open();
                        int result = command.ExecuteNonQuery();
                        if(result < 0)
                        {
                            Response.Write("<script>alert('刪除消息失敗');</script>");
                        }
                    }
                    catch(Exception ex)
                    {
                        Response.Write($"<script>alert('{ex.Message}');</script>");
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