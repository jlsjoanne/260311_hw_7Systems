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
    public partial class Article : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["ArticleID"] != null)
            {
                string articleId = Request.QueryString["ArticleID"];

                if (Session["LogInStatus"] != null && Session["LogInStatus"].ToString() == "true")
                {
                    AddComment.Visible = true;

                    string username = Session["UserName"].ToString();
                    List<string> admin = GetAdmin(articleId);
                    if (admin.Contains(username))
                    {
                        CommandField commandField = (CommandField)GridView1.Columns[0];
                        commandField.ShowDeleteButton = true;
                    }
                }

                if (!IsPostBack)
                {
                    GetArticleData(articleId);
                }




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

        private void GetArticleData(string articleId)
        {
            
            string getArticleQuery = "SELECT Title, Content, Username, C.CategoryName, PostTime  FROM Article AS A JOIN Category AS C ON A.CategoryID=C.CategoryID WHERE A.ArticleID=@articleID";
            string connectionString = WebConfigurationManager.ConnectionStrings["ForumDB"].ConnectionString;

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = connectionString;
            SqlDataReader dr = null;

            SqlCommand command = new SqlCommand(getArticleQuery, conn);
            command.Parameters.AddWithValue("@articleID", articleId);

            try
            {
                conn.Open();
                dr = command.ExecuteReader();

                if (dr.HasRows)
                {
                    dr.Read();
                    ATitle.Text = dr["Title"].ToString();
                    Category.Text = dr["CategoryName"].ToString();
                    PostTime.Text = dr["PostTime"].ToString();
                    Username.Text = dr["Username"].ToString();
                    AContent.Text += dr["Content"].ToString();

                    command.Cancel();
                    dr.Close();
                    BindCommentGrid(articleId, connectionString);
                    GetCommentCount(articleId, connectionString);
                    conn.Close();
                }
                else
                {
                    Response.Write("<script>alert('Wrong ArticleID')</script>");
                    command.Cancel();
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
            catch (Exception ex)
            {
                Response.Write($"<script>alert('{ex.Message}')</script>");
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

        private void GetCommentCount(string articleId, string connectionString)
        {
            string getCommentCountQuery = "SELECT COUNT(*) 'CommentCnt' FROM [Comment] WHERE ArticleID = @ArticleID";

            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                using(SqlCommand command = new SqlCommand(getCommentCountQuery, conn))
                {
                    command.Parameters.AddWithValue("@ArticleID", articleId);
                    SqlDataReader dr = null;

                    try
                    {
                        conn.Open();
                        dr = command.ExecuteReader();
                        dr.Read();
                        CommentCount.Text = dr["CommentCnt"].ToString();
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

        private void BindCommentGrid(string articleID, string connectionString)
        {
            DataTable dt = new DataTable();
            string getCommentQuery = "Select * FROM Comment WHERE ArticleID = @articleID";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using(SqlCommand command = new SqlCommand(getCommentQuery, conn))
                {
                    command.Parameters.AddWithValue("@articleID", articleID);
                    SqlDataAdapter da = new SqlDataAdapter(command);
                    try
                    {
                        conn.Open();
                        da.Fill(dt);
                    }
                    catch(Exception ex)
                    {
                        Response.Write($"<script>alert('{ex.Message}')</script>");
                    }
                    finally
                    {
                        conn.Close();
                    }
                    GridView1.DataSource = dt;
                    GridView1.DataKeyNames = new string[] { "CommentID" };
                    GridView1.DataBind();
                }
            }
        }

        protected void AddComment_Click(object sender, EventArgs e)
        {
            string articleID = Request.QueryString["ArticleID"];
            
            Response.Redirect($"Comment_Add.aspx?ArticleID={articleID}");
        }

        private List<string> GetAdmin(string articleId)
        {
            List<string> admin = new List<string>();

            string connectionString = WebConfigurationManager.ConnectionStrings["ForumDB"].ConnectionString;
            string getAdminQuery = "SELECT UserName FROM [Admin] WHERE CategoryID = (SELECT CategoryID FROM [Article] WHERE ArticleID = @ArticleID)";

            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                using(SqlCommand command = new SqlCommand(getAdminQuery, conn))
                {
                    command.Parameters.AddWithValue("@ArticleID", articleId);
                    SqlDataReader dr = null;

                    try
                    {
                        conn.Open();
                        dr = command.ExecuteReader();
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                admin.Add(dr["UserName"].ToString());
                            }
                        }
                        dr.Close();
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

            return admin;


        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string articleId = Request.QueryString["ArticleID"];
            string commentId = GridView1.DataKeys[e.RowIndex].Value.ToString();
            string connectionString = WebConfigurationManager.ConnectionStrings["ForumDB"].ConnectionString;

            DeleteComment(commentId, connectionString);
            BindCommentGrid(articleId, connectionString);


        }

        private void DeleteComment(string commentId, string connectionString)
        {
            string deleteCommentQuery = "DELETE FROM Comment WHERE CommentID = @CommentID";

            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                using(SqlCommand command = new SqlCommand(deleteCommentQuery, conn))
                {
                    command.Parameters.AddWithValue("@CommentID", commentId);

                    try
                    {
                        conn.Open();
                        int result = command.ExecuteNonQuery();
                        if(result < 0)
                        {
                            Response.Write("<script>alert('留言刪除失敗')</script>");
                        }
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