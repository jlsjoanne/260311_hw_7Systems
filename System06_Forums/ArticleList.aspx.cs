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
            if (Request.QueryString["CategoryID"] != null)
            {
                string categoryID = Request.QueryString["CategoryID"].ToString();
                BindGridView(categoryID);
            }
            else
            {
                Response.Redirect("CategoryList.aspx");
            }

            if(Session["RoleId"] != null && (Session["RoleId"].ToString() == "1" || Session["RoleId"].ToString() == "2")){
                MyArticle.Visible = true;
                AddNew.Visible = true;
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
    }
}