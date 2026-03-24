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
            string connectionString = WebConfigurationManager.ConnectionStrings["PhotoAlbumDB"].ConnectionString;
            string insertCoverQuery = "INSERT INTO Cover (AlbumId, PhotoId) VALUES (@AlbumId, @PhotoId)";

            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                using(SqlCommand command = new SqlCommand(insertCoverQuery, conn))
                {
                    command.Parameters.AddWithValue("@AlbumId", albumId);
                    command.Parameters.AddWithValue("@PhotoId", DropDownList1.SelectedValue);

                    try
                    {
                        conn.Open();
                        int result = command.ExecuteNonQuery();
                        if(result < 0)
                        {
                            Response.Write("<script>alert('選擇封面失敗')</script>");
                        }
                    }
                    catch(Exception ex)
                    {
                        Response.Write($"<script>alert('{ex.Message}')</sctip>");
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }

            Response.Redirect("AlbumList.aspx");
        }
    }
}