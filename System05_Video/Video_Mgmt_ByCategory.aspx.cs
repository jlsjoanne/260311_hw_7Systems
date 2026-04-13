using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace _260311_hw_7Systems.System05_Video
{
    public partial class Video_Mgmt_ByCategory : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["CategoryId"] != null)
            {
                if (Session["RoleId"] != null && (Session["RoleId"].ToString() == "1" || Session["RoleId"].ToString() == "2"))
                {
                    AddNew.Visible = true;
                    VideoGrid.Visible = true;
                }
                if (!IsPostBack)
                {
                    BindGridDate();
                }
            }
            else if(Request.UrlReferrer != null)
            {
                Response.Redirect(Request.UrlReferrer.ToString());
            }
            else
            {
                Response.Redirect("VideoList.aspx");
            }
        }

        protected void GoBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("Video_Mgmt.aspx");
        }

        protected void AddNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("Video_Add.aspx");
        }

        private void BindGridDate()
        {
            string categoryId = Request.QueryString["CategoryId"];
            string connectionString = WebConfigurationManager.ConnectionStrings["VideoDB"].ConnectionString;
            string getVideoQuery = "SELECT * FROM [YTVideo] WHERE CategoryId = @CategoryId";

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

                        VideoGrid.DataSource = dt;
                        VideoGrid.DataBind();
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