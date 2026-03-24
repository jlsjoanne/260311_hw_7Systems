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
    public partial class Photo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["PhotoId"] != null)
            {
                string photoId = Request.QueryString["PhotoId"].ToString();
                GetPhotoData(photoId);
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

        private void GetPhotoData(string photoId)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["PhotoAlbumDB"].ConnectionString;
            string getPhotoQuery = "SELECT * FROM Photo WHERE PhotoId = @PhotoId";

            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                using(SqlCommand command = new SqlCommand(getPhotoQuery, conn))
                {
                    command.Parameters.AddWithValue("@PhotoId", photoId);
                    SqlDataReader dr = null;

                    try
                    {
                        conn.Open();
                        dr = command.ExecuteReader();

                        if (dr.HasRows)
                        {
                            dr.Read();
                            PhotoName.Text = dr["PhotoName"].ToString();
                            PhotoDesc.Text = dr["PhotoDesc"].ToString();
                            PhotoPath.ImageUrl = Path.Combine("~/Images/", Path.GetFileName(dr["PhotoPath"].ToString()));
                            PhotoPath.AlternateText = dr["PhotoName"].ToString();
                        }
                        else
                        {
                            dr.Close();
                            conn.Close();
                            Response.Write("<script>alert('查無此相片')</script>");
                        }
                    }
                    catch (Exception ex)
                    {
                        Response.Write($"<script>alert('{ex.Message}')</script>");
                    }
                    finally
                    {
                        dr.Close();
                        conn.Close();
                    }

                }
            }
        }
    }
}