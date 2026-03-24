using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace _260311_hw_7Systems.System02_PhotoAlbums
{
    public partial class Photo_Add_Desc : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["AlbumId"] != null)
            {
                string albumId = Request.QueryString["AlbumId"].ToString();
                GetPhotoData(albumId);
                
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

        static List<string> photoIdList = new List<string>();

        private void GetPhotoData(string albumId)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["PhotoAlbumDB"].ConnectionString;
            string getPhotoQuery = "SELECT * FROM Photo WHERE AlbumId = @AlbumId";
            
            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                using(SqlCommand command = new SqlCommand(getPhotoQuery, conn))
                {
                    command.Parameters.AddWithValue("@AlbumId", albumId);
                    SqlDataReader dr = null;

                    try
                    {
                        conn.Open();
                        dr = command.ExecuteReader();
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                string photoId = dr["PhotoId"].ToString();
                                if (!photoIdList.Contains(photoId))
                                {
                                    photoIdList.Add(photoId);
                                }                    

                                string folderPath = "~/Images/";

                                Label PName = new Label();
                                Label PDesc = new Label();
                                Image Photo = new Image();
                                TextBox PNameIn = new TextBox();
                                TextBox PDescIn = new TextBox();

                                PName.ID = $"PName_{photoId}";
                                PDesc.ID = $"PDesc_{photoId}";
                                Photo.ID = $"Photo_{photoId}";
                                PNameIn.ID = $"PNameIn_{photoId}";
                                PDescIn.ID = $"PDescIn_{photoId}";

                                PName.Text = "圖片名稱";
                                PDesc.Text = "圖片描述";
                                Photo.ImageUrl = Path.Combine(folderPath, Path.GetFileName(dr["PhotoPath"].ToString()));
                                Photo.AlternateText = dr["PhotoName"].ToString();
                                Photo.Height = new Unit("100px");
                                Photo.Width = new Unit("200px");
                                PNameIn.Text = dr["PhotoName"].ToString();
                                PDescIn.Text = dr["PhotoDesc"].ToString();

                                PhPhoto.Controls.Add(Photo);
                                PhPhoto.Controls.Add(new LiteralControl("<br />"));
                                PhPhoto.Controls.Add(PName);
                                PhPhoto.Controls.Add(PNameIn);
                                PhPhoto.Controls.Add(new LiteralControl("<br />"));
                                PhPhoto.Controls.Add(PDesc);
                                PhPhoto.Controls.Add(PDescIn);
                                PhPhoto.Controls.Add(new LiteralControl("<br />"));

                            }
                        }
                        else
                        {
                            dr.Close();
                            conn.Close();
                            return;
                        }
                    }
                    catch(Exception ex)
                    {
                        Response.Write($"<script>alert('{ex.Message}')</sctip>");
                    }
                    finally
                    {
                        dr.Close();
                        conn.Close();
                    }

                }
            }
            

        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            string albumId = Request.QueryString["AlbumId"].ToString();
            string connectionString = WebConfigurationManager.ConnectionStrings["PhotoAlbumDB"].ConnectionString;
            string updatePhotoQuery = "UPDATE Photo SET PhotoName = @PhotoName, PhotoDesc = @PhotoDesc WHERE PhotoId = @PhotoId";

            foreach(string photoId in photoIdList)
            {
                TextBox PNameIn = (TextBox)PhPhoto.FindControl($"PNameIn_{photoId}");
                TextBox PDescIn = (TextBox)PhPhoto.FindControl($"PDescIn_{photoId}");


                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using(SqlCommand command = new SqlCommand(updatePhotoQuery,conn))
                    {
                        command.Parameters.AddWithValue("@PhotoName", PNameIn.Text);
                        command.Parameters.AddWithValue("@PhotoDesc", PDescIn.Text);
                        command.Parameters.AddWithValue("@PhotoId", photoId);

                        try
                        {
                            conn.Open();
                            int result = command.ExecuteNonQuery();
                            if(result < 0)
                            {
                                Response.Write("<script>alert('新增照片資訊失敗')</script>");
                            }
                        }
                        catch(Exception ex)
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

            Response.Redirect($"Album_Add_Cover.aspx?AlbumId={albumId}");
        }
    }
}