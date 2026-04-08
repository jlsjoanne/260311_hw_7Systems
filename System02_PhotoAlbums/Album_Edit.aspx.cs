using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace _260311_hw_7Systems.System02_PhotoAlbums
{
    public partial class Album_Edit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["AlbumId"] != null)
            {
                if (!IsPostBack)
                {
                    GetAlbumInfo();
                }
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

        private void GetAlbumInfo()
        {
            string albumId = Request.QueryString["AlbumId"];
            string connectionString = WebConfigurationManager.ConnectionStrings["PhotoAlbumDB"].ConnectionString;
            string getAlbumQuery = "SELECT * FROM [Album] WHERE AlbumId = @AlbumId";

            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                using(SqlCommand command = new SqlCommand(getAlbumQuery, conn))
                {
                    command.Parameters.AddWithValue("@AlbumId", albumId);
                    SqlDataReader dr = null;

                    try
                    {
                        conn.Open();
                        dr = command.ExecuteReader();
                        if (dr.HasRows)
                        {
                            dr.Read();
                            AName.Text = dr["AlbumName"].ToString();
                            ADesc.Text = dr["AlbumDesc"].ToString();
                        }
                        dr.Close();
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

        protected void Submit_Click(object sender, EventArgs e)
        {
            string albumId = Request.QueryString["AlbumId"];
            UpdateAlbum(albumId);
            Response.Redirect($"Album_Mgmt.aspx?AlbumId={albumId}");
        }

        private void UpdateAlbum(string albumId)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["PhotoAlbumDB"].ConnectionString;
            string updateAlbumQuery = "UPDATE [Album] " +
                "SET AlbumName = @AlbumName, AlbumDesc = @AlbumDesc " +
                "WHERE AlbumId = @AlbumId";

            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                using(SqlCommand command = new SqlCommand(updateAlbumQuery, conn))
                {
                    command.Parameters.AddWithValue("@AlbumName", AName.Text);
                    command.Parameters.AddWithValue("@AlbumDesc", ADesc.Text);
                    command.Parameters.AddWithValue("@AlbumId", albumId);

                    try
                    {
                        conn.Open();
                        int result = command.ExecuteNonQuery();
                        if (result < 0)
                        {
                            Response.Write("<script>alert('更新相簿資訊失敗')</script>");
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