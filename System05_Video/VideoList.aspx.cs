using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Data;

namespace _260311_hw_7Systems.System05_Video
{
    public partial class VideoList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["RoleId"] != null && (Session["RoleId"].ToString() == "1" || Session["RoleId"].ToString() == "2"))
            {
                AddNew.Visible = true;
            }
            if (!IsPostBack)
            {
                GetCategoryData();
            }
        }

        protected void AddNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("Video_Add.aspx");
        }

        private void GetCategoryData()
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["VideoDB"].ConnectionString;
            string getCategoryQuery = "SELECT * FROM [Category] ORDER BY [CategoryOrder] ASC";

            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(getCategoryQuery, conn))
                {
                    try
                    {
                        conn.Open();
                        SqlDataAdapter da = new SqlDataAdapter(command);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        CategoryRepeater.DataSource = dt;
                        CategoryRepeater.DataBind();
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

        protected void Category_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            string categoryId = DataBinder.Eval(e.Item.DataItem, "CategoryId").ToString();

            ListView childListView = (ListView)e.Item.FindControl("YtList");

            childListView.DataSource = BindVideoData(categoryId);
            childListView.DataBind();
        }

        

        protected DataTable BindVideoData(string categoryId)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["VideoDB"].ConnectionString;
            string getVideoQuery = "SELECT * FROM [YTVideo] WHERE CategoryId = @CategoryId ORDER BY UploadOrder ASC";

            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                using(SqlCommand command = new SqlCommand(getVideoQuery, conn))
                {
                    command.Parameters.AddWithValue("@CategoryId", categoryId);

                    try
                    {
                        conn.Open();
                        SqlDataAdapter da = new SqlDataAdapter(command);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        conn.Close();

                        return dt;
                    }
                    catch (Exception ex)
                    {
                        Response.Write($"<script>alert('{ex.Message}')</script>");
                        return null;
                    }
                }
            }
        }

        protected string CombineYtWatch(object vId)
        {
            string ytId = vId?.ToString() ?? String.Empty;

            return "https://www.youtube.com/watch?v=" + ytId;
        }

        protected string CombineYtEmbed(object vId)
        {
            string ytId = vId?.ToString() ?? String.Empty;
            return "https://www.youtube.com/embed/" + ytId;
        }

        protected string CombineYtImg(object vId)
        {
            string ytId = vId?.ToString() ?? String.Empty;
            return "https://img.youtube.com/vi/" + ytId + "/sddefault.jpg";
        }

        

        
    }
}