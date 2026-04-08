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
    public partial class Album_Mgmt : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Request.QueryString["AlbumId"] != null)
            {
                if(Session["RoleId"] != null && (Session["RoleId"].ToString() == "1" || Session["RoleId"].ToString() == "2"))
                {
                    NoPermission.Visible = false;
                    EditAlbum.Visible = true;
                    EditCover.Visible = true;
                    PhotoMgmt.Visible = true;
                    PhotoGrid.Visible = true;
                }
                if (!IsPostBack)
                {
                    BindPhotoGrid();
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

        protected string CombinePath(object folder, object file)
        {
            string folderPath = folder?.ToString() ?? String.Empty;
            string filePath = file?.ToString() ?? String.Empty;
            string filename = Path.GetFileName(filePath);
            return Path.Combine(folderPath, filename);
        }

        private void BindPhotoGrid()
        {
            string albumId = Request.QueryString["AlbumId"];
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
                        PhotoGrid.DataSource = command.ExecuteReader();
                        PhotoGrid.DataBind();
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

        

        protected void EditAlbum_Click(object sender, EventArgs e)
        {
            string albumId = Request.QueryString["AlbumId"];
            Response.Redirect($"Album_Edit.aspx?AlbumId={albumId}");
        }

        protected void EditCover_Click(object sender, EventArgs e)
        {
            string albumId = Request.QueryString["AlbumId"];
            Response.Redirect($"Album_Add_Cover.aspx?AlbumId={albumId}");
        }

        protected void PhotoGrid_RowEditing(object sender, GridViewEditEventArgs e)
        {
            PhotoGrid.EditIndex = e.NewEditIndex;
            BindPhotoGrid();
        }

        protected void PhotoGrid_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            PhotoGrid.EditIndex = -1;
            BindPhotoGrid();
        }

        protected void PhotoGrid_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string photoId = PhotoGrid.DataKeys[e.RowIndex].Value.ToString();

            // Extract new value
            GridViewRow row = PhotoGrid.Rows[e.RowIndex];
            string pName = ((TextBox)row.Cells[1].Controls[0]).Text;
            string pDesc = ((TextBox)row.Cells[2].Controls[0]).Text;

            // update photo db
            UpdatePhoto(photoId, pName, pDesc);

            PhotoGrid.EditIndex = -1;
            BindPhotoGrid();
        }

        protected void PhotoGrid_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string photoId = PhotoGrid.DataKeys[e.RowIndex].Value.ToString();
            DeletePhoto(photoId);
            BindPhotoGrid();
        }

        private void UpdatePhoto(string pId, string pName, string pDesc)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["PhotoAlbumDB"].ConnectionString;
            string updatePhotoQuery = "UPDATE [Photo] " +
                "SET PhotoName = @PhotoName, PhotoDesc = @PhotoDesc " +
                "WHERE PhotoId = @PhotoId";

            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                using(SqlCommand command = new SqlCommand(updatePhotoQuery, conn))
                {
                    command.Parameters.AddWithValue("@PhotoName",pName);
                    command.Parameters.AddWithValue("@PhotoDesc",pDesc);
                    command.Parameters.AddWithValue("@PhotoId", pId);

                    try
                    {
                        conn.Open();
                        int result = command.ExecuteNonQuery();
                        if(result < 0)
                        {
                            Response.Write("<script>alert('更新相片資訊失敗')</script>");
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

        private void DeletePhoto(string pId)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["PhotoAlbumDB"].ConnectionString;
            string deletePhotoQuery = "DELETE FROM [Photo] WHERE PhotoId = @PhotoId";

            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                using(SqlCommand command = new SqlCommand(deletePhotoQuery, conn))
                {
                    command.Parameters.AddWithValue("@PhotoId", pId);

                    try
                    {
                        conn.Open();
                        int result = command.ExecuteNonQuery();
                        if (result < 0)
                        {
                            Response.Write("<script>alert('刪除相片失敗')</script>");
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

        protected void GoBack_Click(object sender, EventArgs e)
        {
            string albumId = Request.QueryString["AlbumId"];
            Response.Redirect($"Album.aspx?AlbumId={albumId}");
        }
    }
}