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

namespace _260311_hw_7Systems.System03_File
{
    public partial class FileList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["RoleId"] != null && (Session["RoleId"].ToString() == "1" || Session["RoleId"].ToString() == "2"))
            {
                AddNew.Visible = true;
                if (FileGrid.Columns[0] is CommandField commandField)
                {
                    commandField.ShowDeleteButton = true;
                }
            }
            if (!IsPostBack)
            {
                BindGridData();
            }
            
        }

        protected void AddNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("File_Add.aspx");
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
            if(e.CommandName == "Download")
            {
                string filePath = e.CommandArgument.ToString();
                string physicalPath = Server.MapPath(filePath);

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
                    Response.Write("<script>alert('This file does not exist.')</script>");
                }


            }
        }

        protected void FileGrid_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string fileId = FileGrid.DataKeys[e.RowIndex].Value.ToString();
            string connectionString = WebConfigurationManager.ConnectionStrings["FilesDB"].ConnectionString;
            string deleteQuery = "DELETE FROM Files WHERE FileId = @FileId";

            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(deleteQuery, conn))
                {
                    command.Parameters.AddWithValue("@FileId", fileId);

                    try
                    {
                        conn.Open();
                        command.ExecuteNonQuery();
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

            BindGridData();

        }

        protected void FileGridPaging_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            FileGrid.PageIndex = e.NewPageIndex;
            BindGridData();
        }

        private void BindGridData()
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["FilesDB"].ConnectionString;
            string getDataQuery = "SELECT FileId, FileTitle, FileDesc, FilePath, UploadDate, CategoryName FROM [Files] AS F JOIN [Category] AS C ON F.CategoryId = C.CategoryId ORDER BY CategoryOrder ASC, UploadDate DESC";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(getDataQuery, conn))
                {
                    try
                    {
                        conn.Open();
                        SqlDataAdapter da = new SqlDataAdapter(command);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        FileGrid.DataSource = dt;
                        
                        FileGrid.DataBind();
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