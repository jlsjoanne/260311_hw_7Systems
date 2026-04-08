using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Data;
using System.IO;
using System.Data.SqlClient;

namespace _260311_hw_7Systems.System02_PhotoAlbums
{
    public partial class Album_Add_Cover : System.Web.UI.Page
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
            UpdateCover(albumId);
            Response.Redirect("AlbumList.aspx");
        }

        

        private void UpdateCover(string albumId)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["PhotoAlbumDB"].ConnectionString;
            string updateCoverQuery = "UPDATE [Cover] SET PhotoId = @PhotoId WHERE AlbumId = @AlbumId";

            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                using(SqlCommand command = new SqlCommand(updateCoverQuery, conn))
                {
                    command.Parameters.AddWithValue("@PhotoId", PhotoDropDown.SelectedValue);
                    command.Parameters.AddWithValue("@AlbumId", albumId);

                    try
                    {
                        conn.Open();
                        int result = command.ExecuteNonQuery();
                        if (result < 0)
                        {
                            Response.Write("<script>alert('更新相簿封面失敗')</script>");
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