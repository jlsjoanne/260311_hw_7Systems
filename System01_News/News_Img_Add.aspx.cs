using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace _260311_hw_7Systems.System01_News
{
    public partial class News_Img_Add : System.Web.UI.Page
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
            string imgPath = ImgUpload();
            if(imgPath == "Failed")
            {
                Response.Write("<script>alert('圖片上傳失敗')</script>");
                return;
            }
            AddImgToDB(newsId, imgPath);
            Response.Redirect($"News_Img_Mgmt.aspx?NewsId={newsId}");
        }

        
        private string ImgUpload()
        {
            if (ImgFileUpload.HasFiles)
            {
                string saveDir = Server.MapPath("~/Images/");
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(ImgFileUpload.FileName);
                string savePath = Path.Combine(saveDir, fileName);
                try
                {
                    if (!Directory.Exists(saveDir))
                    {
                        Directory.CreateDirectory(saveDir);
                    }
                    ImgFileUpload.SaveAs(savePath);
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

        private void AddImgToDB(string newsId, string imgPath)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["NewsDB"].ConnectionString;
            string addImgQuery = "INSERT INTO [Images] (IName, IDesc, IPath, NewsId) " +
                "VALUES (@IName, @IDesc, @IPath, @NewsId)";

            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                using(SqlCommand command = new SqlCommand(addImgQuery, conn))
                {
                    command.Parameters.AddWithValue("@IName", ImgName.Text);
                    command.Parameters.AddWithValue("@IDesc", ImgDesc.Text);
                    command.Parameters.AddWithValue("@IPath",imgPath);
                    command.Parameters.AddWithValue("@NewsId",newsId);

                    try
                    {
                        conn.Open();
                        int result = command.ExecuteNonQuery();
                        if(result < 0)
                        {
                            Response.Write("<script>alert('圖片寫入資料庫失敗')</script>");
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