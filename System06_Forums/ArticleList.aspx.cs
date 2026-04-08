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
    public partial class ArticleList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            
            string categoryID = Request.QueryString["CategoryID"].ToString();
            

            List<string> admin = GetAdmin(categoryID);
            if (Session["LogInStatus"] != null)
            {
                string username = Session["UserName"].ToString();
                if (admin.Contains(username))
                {
                    CommandField commandField = (CommandField)GridView1.Columns[0];
                    commandField.ShowDeleteButton = true;
                }

            }

            if (Session["RoleId"] != null && (Session["RoleId"].ToString() == "1" || Session["RoleId"].ToString() == "2"))
            {
                MyArticle.Visible = true;
                AddNew.Visible = true;
            }


            if (!IsPostBack)
            {
                if (Request.QueryString["CategoryID"] != null)
                {
                    BindGridView(categoryID);
                }
                else
                {
                    Response.Redirect("CategoryList.aspx");
                }
            }
            
        }

        private void BindGridView(string categoryID)
        {
            DataTable dt = new DataTable();
            string connectionString = WebConfigurationManager.ConnectionStrings["ForumDB"].ConnectionString;
            string selectQuery = "SELECT ArticleID, Title, PostTime FROM Article WHERE CategoryID=@CategoryID";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using(SqlCommand command = new SqlCommand(selectQuery, conn))
                {
                    command.Parameters.AddWithValue("@CategoryID",categoryID);
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
                    GridView1.DataKeyNames = new string[] { "ArticleID" };
                    GridView1.DataBind();
                }

            }

        }

        protected void AddNew_Click(object sender, EventArgs e)
        {
            string category = Request.QueryString["CategoryID"];
            Response.Redirect($"Article_Add.aspx?CategoryID={category}");
        }

        private List<string> GetAdmin(string categoryID)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["ForumDB"].ConnectionString;
            string getAdminQuery = "SELECT UserName FROM Admin WHERE CategoryID = @categoryID";
            List<string> admin = new List<string>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using(SqlCommand command = new SqlCommand(getAdminQuery, conn))
                {
                    command.Parameters.AddWithValue("@categoryID",categoryID);
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
                        conn.Close();
                        return admin;
                    }
                    catch(Exception ex)
                    {
                        Response.Write($"<script>alert('{ex.Message}')</script>");
                        return admin;
                    }
                }
            }
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string articleID = e.Keys["ArticleID"].ToString();
            string categoryID = Request.QueryString["CategoryID"].ToString();
            

            DeleteArticle(articleID);

            e.Cancel = true;


            BindGridView(categoryID);

        }

        private void DeleteArticle(string articleID)
        {
            string deleteArticleQuery = "DELETE FROM Article WHERE ArticleID = @articleID";
            string connectionString = WebConfigurationManager.ConnectionStrings["ForumDB"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using(SqlCommand command = new SqlCommand(deleteArticleQuery, conn))
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


        protected void MyArticle_Click(object sender, EventArgs e)
        {
            string username = Session["UserName"].ToString();
            Response.Redirect($"Article_byUser.aspx");
        }
    }
}