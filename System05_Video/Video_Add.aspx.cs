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
    public partial class Video_Add : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["RoleId"] != null && (Session["RoleId"].ToString() == "1" || Session["RoleId"].ToString() == "2"))
            {

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

        protected void Submit_Click(object sender, EventArgs e)
        {
            InsertVideoData();
            Response.Redirect("VideoList.aspx");
        }

        private void InsertVideoData()
        {
            //img upload
            string folderPath = Server.MapPath("~/Images/");
            string fileName = "";
            if (VideoImg.HasFile)
            {
                fileName = Guid.NewGuid().ToString() + Path.GetExtension(VideoImg.FileName);
                VideoImg.SaveAs(Path.Combine(folderPath, fileName));
            }
            else
            {
                Response.Write("<script>alert('無影片縮圖')</script>");
                return;
            }
            
            string connectionString = WebConfigurationManager.ConnectionStrings["VideoDB"].ConnectionString;
            string insertDataQuery = "INSERT INTO VideoData (VideoName, VideoUrl, VideoImg, CategoryId) VALUES (@VideoName, @VideoUrl, @VideoImg, @CategoryId)";

            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                using(SqlCommand command = new SqlCommand(insertDataQuery, conn))
                {
                    command.Parameters.AddWithValue("@VideoName", VideoName.Text);
                    command.Parameters.AddWithValue("@VideoUrl", VideoUrl.Text);
                    command.Parameters.AddWithValue("@VideoImg", Path.Combine(folderPath, fileName));
                    command.Parameters.AddWithValue("@CategoryId", CategoryDropDown.SelectedValue);

                    try
                    {
                        conn.Open();
                        int result = command.ExecuteNonQuery();
                        if(result < 0)
                        {
                            Response.Write("<script>alert('影片寫入資料庫失敗')</script>");
                        }
                    }
                    catch (Exception ex)
                    {
                        Response.Write($"<script>alert('{ex.Message}')<script>");
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