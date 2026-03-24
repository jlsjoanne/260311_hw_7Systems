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
    public partial class AlbumList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                AlbumDataBind();
            }
            if (Session["RoleId"] != null && (Session["RoleId"].ToString() == "1" || Session["RoleId"].ToString() == "2"))
            {
                AddNew.Visible = true;
            }
            
            
        }

        protected void AddNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("Album_Add.aspx");
        }

        private void AlbumDataBind()
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["PhotoAlbumDB"].ConnectionString;
            string getAlbumData = "SELECT A.AlbumId, A.AlbumName, P.PhotoName, P.PhotoPath FROM [Album] AS A LEFT JOIN [Cover] AS C ON A.AlbumId = C.AlbumId LEFT JOIN [Photo] AS P ON C.PhotoId = P.PhotoId";

            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                using(SqlCommand command = new SqlCommand(getAlbumData, conn))
                {
                    try
                    {
                        conn.Open();
                        SqlDataAdapter da = new SqlDataAdapter(command);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        AlbumRepeater.DataSource = dt;
                        AlbumRepeater.DataBind();
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

        protected string CombinePath(object folder, object file)
        {
            string folderPath = folder?.ToString() ?? String.Empty;
            string filePath = file?.ToString() ?? String.Empty;
            string filename = Path.GetFileName(filePath);

            return Path.Combine(folderPath, filename);
        }

        protected void AlbumRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if(e.CommandName == "ToAlbum")
            {
                string albumId = e.CommandArgument.ToString();
                Response.Redirect($"Album.aspx?AlbumId={albumId}");
            }
            
            
        }
    }
}