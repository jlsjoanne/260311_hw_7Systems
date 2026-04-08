using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace _260311_hw_7Systems.System01_News
{
    public partial class News_File_Mgmt : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["NewsId"] != null)
            {
                if (Session["RoleId"] != null && (Session["RoleId"].ToString() == "1" || Session["RoleId"].ToString() == "2"))
                {
                    AddFile.Visible = true;
                    FileGrid.Visible = true;
                }
                if (!IsPostBack)
                {
                    BindFileGrid();
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

        protected void AddFile_Click(object sender, EventArgs e)
        {
            string newsId = Request.QueryString["NewsId"];
            Response.Redirect($"News_File_Add.aspx?NewsId={newsId}");
        }

        private void BindFileGrid()
        {
            string newsId = Request.QueryString["NewsId"];
            string connectionString = WebConfigurationManager.ConnectionStrings["NewsDB"].ConnectionString;
            string getFileQuery = "SELECT * FROM [Files] WHERE NewsId = @NewsId";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using(SqlCommand command = new SqlCommand(getFileQuery, conn))
                {
                    command.Parameters.AddWithValue("@NewsId", newsId);

                    try
                    {
                        conn.Open();
                        FileGrid.DataSource = command.ExecuteReader();
                        FileGrid.DataBind();
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

        protected void FileGrid_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string fileId = FileGrid.DataKeys[e.RowIndex].Value.ToString();
            DeleteFile(fileId);
            BindFileGrid();
        }

        private void DeleteFile(string fileId)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["NewsDB"].ConnectionString;
            string getFilePath = "SELECT FPath FROM [Files] WHERE FileId = @FileId";
            string deleteFile = "DELETE FROM [Files] WHERE FileId = @FileId";

            // Get File Path
            string filePath = "";
            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                using(SqlCommand command = new SqlCommand(getFilePath, conn))
                {
                    command.Parameters.AddWithValue("@FileId", fileId);
                    SqlDataReader dr = null;

                    try
                    {
                        conn.Open();
                        dr = command.ExecuteReader();
                        if (dr.HasRows)
                        {
                            dr.Read();
                            filePath = dr["FPath"].ToString();
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

            // Delete File from Folder
            if(filePath != "")
            {
                string folderPath = Server.MapPath("~/Files/");
                string FName = Path.GetFileName(filePath);
                string fileFullPath = Path.Combine(folderPath, FName);
                if (File.Exists(fileFullPath))
                {
                    File.Delete(fileFullPath);
                }
            }

            // Delete From DB
            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                using(SqlCommand command = new SqlCommand(deleteFile, conn))
                {
                    command.Parameters.AddWithValue("@FileId", fileId);
                    try
                    {
                        conn.Open();
                        int result = command.ExecuteNonQuery();
                        if (result < 0)
                        {
                            Response.Write("<script>alert('刪除檔案失敗')</script>");
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

        protected string CombinePath(object folder, object file)
        {
            string folderPath = folder?.ToString() ?? String.Empty;
            string filePath = file?.ToString() ?? String.Empty;
            string filename = Path.GetFileName(filePath);

            return Path.Combine(folderPath, filename);
        }

        protected void FileGrid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if(e.CommandName == "DownloadFile")
            {
                string fPath = e.CommandArgument.ToString();
                string physicalPath = Server.MapPath(fPath);
                FileInfo file = new FileInfo(physicalPath);

                if (file.Exists)
                {
                    Response.Clear();
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
                    Response.AddHeader("Content-Length", file.Length.ToString());
                    Response.ContentType = "application/octet-stream";
                    Response.WriteFile(file.FullName);
                    Response.Flush();
                    Response.End();
                }
                else
                {
                    Response.Write("<script>alert('檔案不存在')</script>");
                }
            }
        }
    }
}