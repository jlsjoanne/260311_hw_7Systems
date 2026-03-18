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
    public partial class Article_byUser : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] != null)
            {
                string username = Session["UserName"].ToString();

                if (!IsPostBack)
                {
                    BindGridView(username);
                }
            }
            else
            {
                Response.Redirect("CategoryList.aspx");
            }
        }

        private void BindGridView(string userName)
        {
            DataTable dt = new DataTable();
            string connectionString = WebConfigurationManager.ConnectionStrings["ForumDB"].ConnectionString;
            string selectbyUserQuery = "SELECT ArticleID, Title, Content, PostTime, C.CategoryName FROM Article AS A JOIN Category AS C ON A.CategoryID=C.CategoryID WHERE Username = @userName";

            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                using(SqlCommand command = new SqlCommand(selectbyUserQuery, conn))
                {
                    command.Parameters.AddWithValue("@userName", userName);
                    SqlDataAdapter da = new SqlDataAdapter(command);
                    try
                    {
                        conn.Open();
                        da.Fill(dt);
                    }
                    catch (Exception ex)
                    {
                        Response.Write($"<script>alert('{ex.Message}')</script>");
                    }
                    finally
                    {
                        conn.Close();
                    }

                    GridView1.DataSource = dt;
                    GridView1.DataKeyNames = new string[] { "ArticleID" };
                    GridView1.DataBind();
                }
            }
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string articleID = e.Keys["ArticleID"].ToString();
            string username = Session["UserName"].ToString();

            DeleteArticle(articleID);

            e.Cancel = true;

            
            BindGridView(username);

        }

        private void DeleteArticle(string articleID)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["ForumDB"].ConnectionString;
            string deleteArticleQuery = "DELETE FROM Article WHERE ArticleID = @articleID";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(deleteArticleQuery, conn))
                {
                    command.Parameters.AddWithValue("@articleID", articleID);
                    conn.Open();
                    int result = command.ExecuteNonQuery();

                    conn.Close();

                    if (result < 0)
                    {
                        Response.Write("<script>alert('刪除文章失敗')</script>");
                    }
                }
            }
        }

        
    }
}