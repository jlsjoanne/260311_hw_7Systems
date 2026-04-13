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
            if(VideoName.Text == "")
            {
                Response.Write("<script>alert('影片標題不得為空');</script>");
                return;
            }
            if(VideoId.Text.Length != 11)
            {
                Response.Write("<script>alert('Youtube影片ID格式錯誤');</script>");
                return;
            }
            
            InsertVideoData();
            Response.Redirect("VideoList.aspx");
        }

        private void InsertVideoData()
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["VideoDB"].ConnectionString;
            string insertDataQuery = "INSERT INTO YTVideo (VideoId, VideoName, CategoryId) VALUES (@VideoId, @VideoName, @CategoryId)";

            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                using(SqlCommand command = new SqlCommand(insertDataQuery, conn))
                {
                    command.Parameters.AddWithValue("@VideoId", VideoId.Text);
                    command.Parameters.AddWithValue("@VideoName", VideoName.Text);
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