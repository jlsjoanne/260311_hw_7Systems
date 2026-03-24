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
    public partial class Album : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["AlbumId"] != null)
                {
                    string albumId = Request.QueryString["AlbumId"].ToString();
                    GetAlbumData(albumId);
                    PhotoDataBind(albumId);
                }
                else if(Request.UrlReferrer != null)
                {
                    Response.Redirect(Request.UrlReferrer.ToString());
                }
                else
                {
                    Response.Redirect("AlbumList.aspx");
                }
                
                if(Session["RoleId"] != null && (Session["RoleId"].ToString() == "1" || Session["RoleId"].ToString() == "2"))
                {
                    AddPhoto.Visible = true;
                }
            }
        }

        private void GetAlbumData(string albumId)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["PhotoAlbumDB"].ConnectionString;
            string getAlbumQuery = "SELECT * FROM Album WHERE AlbumId = @AlbumId";

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
                            AlbumTitle.Text = dr["AlbumName"].ToString();
                            AlbumDesc.Text = dr["AlbumDesc"].ToString();
                        }
                        else
                        {
                            Response.Write("<script>alert('查無此相簿')</script>");
                            return;
                        }
                    }
                    catch(Exception ex)
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

        private void PhotoDataBind(string albumId)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["PhotoAlbumDB"].ConnectionString;
            string getPhotoQuery = "SELECT * FROM [Photo] WHERE AlbumId = @AlbumId";

            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                using(SqlCommand command = new SqlCommand(getPhotoQuery, conn))
                {
                    command.Parameters.AddWithValue("@AlbumId", albumId);

                    try
                    {
                        conn.Open();
                        SqlDataAdapter da = new SqlDataAdapter(command);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        PhotoRepeater.DataSource = dt;
                        PhotoRepeater.DataBind();
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

        protected void AddPhoto_Click(object sender, EventArgs e)
        {
            string albumId = Request.QueryString["AlbumId"].ToString();
            Response.Redirect($"Photo_Add.aspx?AlbumId={albumId}");
        }

        protected string CombinePath(object folder, object file)
        {
            string folderPath = folder?.ToString() ?? String.Empty;
            string filePath = file?.ToString() ?? String.Empty;
            string filename = Path.GetFileName(filePath);

            return Path.Combine(folderPath, filename);
        }

        protected void PhotoRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if(e.CommandName == "ToPhotoInfo")
            {
                string photoId = e.CommandArgument.ToString();
                Response.Redirect($"Photo.aspx?PhotoId={photoId}");
            }
        }
    }
}