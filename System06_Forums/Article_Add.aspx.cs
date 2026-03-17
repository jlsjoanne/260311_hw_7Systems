using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Configuration;
using System.Data.SqlClient;

namespace _260311_hw_7Systems.System06_Forums
{
    public partial class Article_Add : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Request.QueryString["CategoryID"] == null)
            {
                Response.Redirect("CategoryList.aspx");
            }
            
        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            if(Request.QueryString["CategoryID"] != null)
            {
                string categoryID = Request.QueryString["CategoryID"];
                string username = Session["UserName"].ToString();
                string connectionString = WebConfigurationManager.ConnectionStrings["ForumDB"].ConnectionString;
                string addArticleQuery = "INSERT INTO Article(Title,Content,UserName,CategoryID) VALUES (@title, @content,@username,@categoryID)";
                
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = connectionString;

                SqlCommand command = new SqlCommand(addArticleQuery, conn);
                command.Parameters.AddWithValue("@title", ATitle.Text);
                command.Parameters.AddWithValue("@content", AContent.Text);
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@categoryID", categoryID);

                try
                {
                    conn.Open();
                    int result = command.ExecuteNonQuery();

                    command.Cancel();
                    conn.Close();

                    if(result < 0)
                    {
                        Response.Write("<script>alert('新增文章失敗')</script>");
                    }
                    else
                    {
                        Response.Write("<script>alert('新增文章成功')</script>");
                        Response.Redirect($"ArticleList.aspx?CategoryID={categoryID}");
                    }

                }
                catch(Exception ex)
                {
                    Response.Write($"<script>alert('{ex.Message}')</script>");
                }



            }
        }
    }
}