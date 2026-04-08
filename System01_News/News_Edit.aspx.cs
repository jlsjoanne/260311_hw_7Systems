using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Configuration;
using System.Data.SqlClient;

namespace _260311_hw_7Systems.System01_News
{
    public partial class News_Edit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["NewsId"] != null)
            {
                string newsId = Request.QueryString["NewsId"].ToString();
                if (!IsPostBack)
                {
                    GetNewsDate(newsId);
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

        private void GetNewsDate(string newsId)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["NewsDB"].ConnectionString;
            string getNewsQuery = "SELECT NewsTitle, NewsContent, C.CategoryName, PostDate FROM NewsList AS N JOIN Category AS C ON N.CategoryId=C.CategoryId WHERE NewsId = @NewsId";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using(SqlCommand command = new SqlCommand(getNewsQuery, conn))
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
                            NCategory.Text = dr["CategoryName"].ToString();
                            PostedTime.Text = dr["PostDate"].ToString();
                            NContent.Text = dr["NewsContent"].ToString();
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

        protected void Submit_Click(object sender, EventArgs e)
        {
            string newsId = Request.QueryString["NewsId"].ToString();
            string connectionString = WebConfigurationManager.ConnectionStrings["NewsDB"].ConnectionString;
            string updateNewsQuery = "UPDATE NewsList SET NewsTitle = @NewsTitle, NewsContent = @NewsContent, CategoryId = @CategoryId WHERE NewsId = @NewsId";

            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                using(SqlCommand command = new SqlCommand(updateNewsQuery, conn))
                {
                    command.Parameters.AddWithValue("@NewsTitle", NewsTitle.Text);
                    command.Parameters.AddWithValue("@NewsContent", NContent.Text);
                    command.Parameters.AddWithValue("@CategoryId", DropDownList1.SelectedValue);
                    command.Parameters.AddWithValue("@NewsId",newsId);

                    try
                    {
                        conn.Open();
                        int result = command.ExecuteNonQuery();

                        if(result < 0)
                        {
                            Response.Write("<script>alert('消息更新失敗')</script>");
                        }
                    }
                    catch(Exception ex)
                    {
                        Response.Write($"<script>alert('{ex.Message}')</script>");
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }

            Response.Redirect($"News.aspx?NewsId={newsId}");
        }
    }
}