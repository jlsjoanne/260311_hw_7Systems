using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.Configuration;

namespace _260311_hw_7Systems.System01_News
{
    public partial class News_File_Add : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["NewsId"] != null)
            {

            }
            else if (Request.UrlReferrer != null)
            {
                Response.Redirect(Request.UrlReferrer.ToString());
            }
            else
            {
                Response.Redirect("News_List.aspx");
            }
        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            string newsId = Request.QueryString["NewsId"];
            string fPath = FUpload();
            if(fPath == "Failed")
            {
                Response.Write("<script>alert('檔案上傳失敗')</script>");
                return;
            }
            AddFileToDB(newsId, fPath);
            Response.Redirect($"News_File_Mgmt.aspx?NewsId={newsId}");
        }

        private string FUpload()
        {
            if (NewsFileUpload.HasFiles)
            {
                string saveDir = Server.MapPath("~/Files/");
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(NewsFileUpload.FileName);
                string savePath = Path.Combine(saveDir, fileName);
                try
                {
                    if (!Directory.Exists(saveDir))
                    {
                        Directory.CreateDirectory(saveDir);
                    }
                    NewsFileUpload.SaveAs(savePath);
                }
                catch (Exception ex)
                {
                    Response.Write($"<script>alert('上傳失敗:{ex.Message}')</script>");
                    return "Failed";
                }
                return savePath;
            }
            else
            {
                return "Failed";
            }
        }

        private void AddFileToDB(string newsId, string fPath)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["NewsDB"].ConnectionString;
            string addFileQuery = "INSERT INTO [Files] (FName, FDesc, FPath, NewsId) " +
                "VALUES (@FName, @FDesc, @FPath, @NewsId)";

            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                using(SqlCommand command = new SqlCommand(addFileQuery, conn))
                {
                    command.Parameters.AddWithValue("@FName", FName.Text);
                    command.Parameters.AddWithValue("@FDesc", FDesc.Text);
                    command.Parameters.AddWithValue("@FPath", fPath);
                    command.Parameters.AddWithValue("@NewsId", newsId);

                    try
                    {
                        conn.Open();
                        int result = command.ExecuteNonQuery();
                        if (result < 0)
                        {
                            Response.Write("<script>alert('檔案寫入資料庫失敗')</script>");
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