using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.IO;

namespace _260311_hw_7Systems.System01_News
{
    public partial class News_Img_Mgmt : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["NewsId"] != null)
            {
                if(Session["RoleId"] != null && (Session["RoleId"].ToString() == "1" || Session["RoleId"].ToString() == "2"))
                {
                    AddImg.Visible = true;
                    ImgGrid.Visible = true;
                }
                if (!IsPostBack)
                {
                    BindImgGrid();
                }
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

        private void BindImgGrid()
        {
            string newsId = Request.QueryString["NewsId"];
            string connectionString = WebConfigurationManager.ConnectionStrings["NewsDB"].ConnectionString;
            string getImgQuery = "SELECT * FROM [Images] WHERE NewsId = @NewsId";

            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                using(SqlCommand command = new SqlCommand(getImgQuery, conn))
                {
                    command.Parameters.AddWithValue("@NewsId", newsId);

                    try
                    {
                        conn.Open();
                        ImgGrid.DataSource = command.ExecuteReader();
                        ImgGrid.DataBind();
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

        protected string CombineImgPath(object folder, object file)
        {
            string folderPath = folder?.ToString() ?? String.Empty;
            string filePath = file?.ToString() ?? String.Empty;
            string filename = Path.GetFileName(filePath);

            return Path.Combine(folderPath, filename);
        }

        protected void AddImg_Click(object sender, EventArgs e)
        {
            string newsId = Request.QueryString["NewsId"];
            Response.Redirect($"News_Img_Add.aspx?NewsId={newsId}");
        }

        protected void GoBack_Click(object sender, EventArgs e)
        {
            string newsId = Request.QueryString["NewsId"];
            Response.Redirect($"News.aspx?NewsId={newsId}");
        }

        protected void ImgGrid_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string imageId = ImgGrid.DataKeys[e.RowIndex].Value.ToString();
            DeleteImg(imageId);
            e.Cancel = true;
            BindImgGrid();
        }

        private void DeleteImg(string imageId)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["NewsDB"].ConnectionString;
            string getImgPath = "SELECT IPath FROM [Images] WHERE ImageId = @ImageId";
            string deleteImg = "DELETE FROM [Images] WHERE ImageId = @ImageId";

            // Get Img Path
            string ImgPath = "";
            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                using(SqlCommand command = new SqlCommand(getImgPath, conn))
                {
                    command.Parameters.AddWithValue("@ImageId", imageId);
                    SqlDataReader dr = null;

                    try
                    {
                        conn.Open();
                        dr = command.ExecuteReader();
                        if (dr.HasRows)
                        {
                            dr.Read();
                            ImgPath = dr["IPath"].ToString();
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

            // Delete Img file
            if(ImgPath != "")
            {
                string folderPath = Server.MapPath("~/Images/");
                string imgName = Path.GetFileName(ImgPath);
                string imgfile = Path.Combine(folderPath, imgName);
                if (File.Exists(imgfile))
                {
                    File.Delete(imgfile);
                }
            }

            // Delete img from DB
            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                using(SqlCommand command = new SqlCommand(deleteImg, conn))
                {
                    command.Parameters.AddWithValue("@ImageId", imageId);

                    try
                    {
                        conn.Open();
                        int result = command.ExecuteNonQuery();
                        if(result < 0)
                        {
                            Response.Write("<script>alert('刪除圖片失敗')</script>");
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