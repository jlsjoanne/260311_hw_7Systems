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
    public partial class News_Add : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["RoleId"] != null && (Session["RoleId"].ToString() == "1" || Session["RoleId"].ToString() == "2"))
            {
                
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

        protected void Submit_Click(object sender, EventArgs e)
        {
            // Title, Content, Category to SQL
            AddToNewsList();

            Response.Redirect("News_List_Mgmt.aspx");
        }

        private void AddToNewsList()
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["NewsDB"].ConnectionString;
            string insertDataQuery = "INSERT INTO NewsList (NewsTitle, NewsContent, CategoryId) VALUES (@NewsTitle, @NewsContent, @CategoryID)";

            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                using(SqlCommand command = new SqlCommand(insertDataQuery, conn))
                {
                    command.Parameters.AddWithValue("@NewsTitle", NewTitle.Text);
                    command.Parameters.AddWithValue("@NewsContent", Content.Text);
                    command.Parameters.AddWithValue("@CategoryID", DropDownList1.SelectedValue);

                    try
                    {
                        conn.Open();
                        int result = command.ExecuteNonQuery();

                        if (result < 0)
                        {
                            Response.Write("<script>alert('新增文章失敗')</script>");
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
        }  
    }
}