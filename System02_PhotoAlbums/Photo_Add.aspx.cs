using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Data.SqlClient;

namespace _260311_hw_7Systems.System02_PhotoAlbums
{
    public partial class Photo_Add : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["AlbumId"] != null)
            {
                
            }
            else if (Request.UrlReferrer != null)
            {
                Response.Redirect(Request.UrlReferrer.ToString());
            }
            else
            {
                Response.Redirect("AlbumList.aspx");
            }
        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            string albumId = Request.QueryString["AlbumId"].ToString();

            // Photo% to SQL
            AddToPhoto(albumId);

            //Redirect to Edit Photo Name and Desc
            Response.Redirect($"Photo_Add_Desc.aspx?AlbumId={albumId}");
        }

        private void AddToPhoto(string albumId)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["PhotoAlbumDB"].ConnectionString;
            string insertPhotoQuery = "INSERT INTO Photo (PhotoName, PhotoPath, AlbumId) VALUES (@PhotoName, @PhotoPath, @AlbumId)";

            string folderPath = Server.MapPath("~/Images/");

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            foreach(HttpPostedFile postedFile in ImgUpload.PostedFiles)
            {
                string photoName = Path.GetFileName(postedFile.FileName);
                string storedfileName = Guid.NewGuid().ToString() + Path.GetExtension(postedFile.FileName);
                postedFile.SaveAs(Path.Combine(folderPath, storedfileName));

                using(SqlConnection conn = new SqlConnection(connectionString))
                {
                    using(SqlCommand command = new SqlCommand(insertPhotoQuery, conn))
                    {
                        command.Parameters.AddWithValue("@PhotoName", photoName);
                        command.Parameters.AddWithValue("@PhotoPath", Path.Combine(folderPath, storedfileName));
                        command.Parameters.AddWithValue("@AlbumId", albumId);

                        try
                        {
                            conn.Open();
                            int result = command.ExecuteNonQuery();
                            if(result < 0)
                            {
                                Response.Write("<script>alert('新增圖片失敗')</script>");
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
}