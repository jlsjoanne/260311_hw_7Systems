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
    public partial class Comment_Add : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["ArticleID"] != null && Session["UserName"] != null)
            {
                
            }
            else if(Request.UrlReferrer != null)
            {
                Response.Redirect(Request.UrlReferrer.ToString());
            }
            else
            {
                Response.Redirect("CategoryList.aspx");
            }
        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            string articleID = Request.QueryString["ArticleID"];
            string addCommentQuery = "INSERT INTO Comment (Content, Username, ArticleID) VALUES (@content, @username, @articleID)";
            string connectionString = WebConfigurationManager.ConnectionStrings["ForumDB"].ConnectionString;

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = connectionString;

            SqlCommand command = new SqlCommand(addCommentQuery, conn);
            command.Parameters.AddWithValue("@content", Comment.Text);
            command.Parameters.AddWithValue("@username", Session["UserName"].ToString());
            command.Parameters.AddWithValue("@articleID", articleID);

            try
            {
                conn.Open();
                int result = command.ExecuteNonQuery();

                command.Cancel();
                conn.Close();

                if (result < 0)
                {
                    Response.Write("<script>alert('新增留言失敗')</script>");
                }
                else
                {
                    Response.Redirect($"Article.aspx?ArticleID={articleID}");
                }
            }
            catch(Exception ex)
            {
                Response.Write($"<script>alert('{ex.Message}')</script>");
            }

        }
    }
}