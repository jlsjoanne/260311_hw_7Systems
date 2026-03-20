using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace _260311_hw_7Systems.System01_News
{
    public partial class News : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["NewsId"] != null)
            {
                string newsId = Request.QueryString["NewsId"].ToString();
                GetNewsData(newsId);
            }
            else if(Request.UrlReferrer != null)
            {
                Response.Redirect(Request.UrlReferrer.ToString());
            }
            else
            {
                Response.Redirect("News_List.aspx");
            }
        }

        private void GetNewsData(string newsId)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["NewsDB"].ConnectionString;
            string getnewsQuery = "SELECT NewsTitle, NewsContent, PostDate, C.CategoryName FROM NewsList AS N JOIN Category AS C ON N.CategoryId = C.CategoryId WHERE NewsId = @NewsId";

            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                using(SqlCommand command = new SqlCommand(getnewsQuery, conn))
                {
                    command.Parameters.AddWithValue("@NewsId", newsId);
                    SqlDataReader dr = null;

                    try
                    {
                        conn.Open();
                        dr = command.ExecuteReader();

                        if (dr.HasRows)
                        {
                            dr.Read();
                            NewsTitle.Text = dr["NewsTitle"].ToString();
                            NewsCategory.Text = dr["CategoryName"].ToString();
                            PostedTime.Text = dr["PostDate"].ToString();
                            NewsContent.Text += dr["NewsContent"].ToString();
                        }
                    }
                    catch(Exception ex)
                    {
                        Response.Write($"<script>alert('{ex.Message}')</script>");
                    }
                    finally
                    {
                        dr.Close();
                        conn.Close();
                    }
                }
            }
        }
    }
}