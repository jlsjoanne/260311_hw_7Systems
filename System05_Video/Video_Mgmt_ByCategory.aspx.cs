using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

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
            else if (Request.UrlReferrer != null)
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

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(getVideoQuery, conn))
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

        protected void VideoGrid_RowEditing(object sender, GridViewEditEventArgs e)
        {
            VideoGrid.EditIndex = e.NewEditIndex;
            BindGridDate();
        }

        protected void VideoGrid_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            VideoGrid.EditIndex = -1;
            BindGridDate();
        }

        protected void VideoGrid_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string videoId = VideoGrid.DataKeys[e.RowIndex].Value.ToString();

            GridViewRow row = VideoGrid.Rows[e.RowIndex];
            string vName = ((TextBox)row.Cells[1].Controls[0]).Text;

            UpdateVideo(videoId, vName);
            VideoGrid.EditIndex = -1;
            BindGridDate();
        }

        protected void VideoGrid_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string videoId = VideoGrid.DataKeys[e.RowIndex].Value.ToString();
            DeleteVideo(videoId);
            BindGridDate();
        }

        private void UpdateVideo(string vId, string vName)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["VideoDB"].ConnectionString;
            string updateQuery = "UPDATE [YTVideo] SET VideoName = @VideoName WHERE VideoId = @VideoId";

            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                using(SqlCommand command = new SqlCommand(updateQuery, conn))
                {
                    command.Parameters.AddWithValue("@VideoName", vName);
                    command.Parameters.AddWithValue("@VideoId", vId);

                    try
                    {
                        conn.Open();
                        int result = command.ExecuteNonQuery();
                        if(result < 0)
                        {
                            Response.Write("<script>alert('更新影片失敗');</script>");
                        }
                    }
                    catch (Exception ex)
                    {
                        Response.Write($"<script>alert('{ex.Message}');</script>");
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
        }

        private void DeleteVideo(string vId)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["VideoDB"].ConnectionString;
            string deleteQuery = "DELETE FROM [YTVideo] WHERE VideoId = @VideoId";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using(SqlCommand command = new SqlCommand(deleteQuery, conn))
                {
                    command.Parameters.AddWithValue("@VideoId", vId);

                    try
                    {
                        conn.Open();
                        int result = command.ExecuteNonQuery();
                        if(result < 0)
                        {
                            Response.Write("<script>alert('刪除影片失敗');</script>");
                        }
                    }
                    catch (Exception ex)
                    {
                        Response.Write($"<script>alert('{ex.Message}');</script>");
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