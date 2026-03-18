using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace _260311_hw_7Systems.System06_Forums
{
    public partial class Article_Edit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["ArticleID"] != null)
                {
                    string articleID = Request.QueryString["ArticleID"];
                    GetArticle(articleID);

                }
                else if (Request.UrlReferrer != null)
                {
                    Response.Redirect(Request.UrlReferrer.ToString());
                }
                else
                {
                    Response.Redirect("CategoryList.aspx");
                }
            }
  
        }

        private void GetArticle(string articleID)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["ForumDB"].ConnectionString;
            string getArticleQuery = "SELECT Title, Content FROM Article WHERE ArticleID= @articleID";

            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                using(SqlCommand command = new SqlCommand(getArticleQuery, conn))
                {
                    command.Parameters.AddWithValue("@articleID", articleID);
                    SqlDataReader dr = null;
                    try
                    {
                        conn.Open();
                        dr = command.ExecuteReader();

                        if (dr.HasRows)
                        {
                            dr.Read();
                            ATitle.Text = dr["Title"].ToString();
                            AContent.Text = dr["Content"].ToString();
                            dr.Close();
                            conn.Close();
                        }
                        else
                        {
                            Response.Write($"<script>alert('無此文章')</script>");
                            dr.Close();
                            conn.Close();
                            if (Request.UrlReferrer != null)
                            {
                                Response.Redirect(Request.UrlReferrer.ToString());
                            }
                            else
                            {
                                Response.Redirect("CategoryList.aspx");
                            }
                        }
                    }
                    catch(Exception ex)
                    {
                        Response.Write($"<script>alert('{ex.Message}')</script>");
                    }
                    
                }
            }
        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            string articleID = Request.QueryString["ArticleID"];
            string connectionString = WebConfigurationManager.ConnectionStrings["ForumDB"].ConnectionString;
            string updateArticleQuery = "UPDATE Article SET Title = @Title, Content = @Content WHERE ArticleID = @ArticleID";

            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                using(SqlCommand command = new SqlCommand(updateArticleQuery, conn))
                {
                    command.Parameters.AddWithValue("@Title", ATitle.Text);
                    command.Parameters.AddWithValue("@Content", AContent.Text);
                    command.Parameters.AddWithValue("@ArticleID", articleID);

                    try
                    {
                        conn.Open();
                        int result = command.ExecuteNonQuery();

                        if (result < 0)
                        {
                            Response.Write("<script>alert('修改文章失敗')</script>");
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
                    finally
                    {
                        conn.Close();
                    }
                }
            }
        }
    }
}