using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;


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
                GetCategory();
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
            DeleteFromFolder(fileId);
            DeleteFromDB(fileId);
            e.Cancel = true;
            string selectedCategory = CategoryDropDown.SelectedValue;
            BindGridData(selectedCategory);

        }

        private void DeleteFromFolder(string fileId)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["FilesDB"].ConnectionString;
            string getFilePath = "SELECT FilePath FROM [Files] WHERE FileId = @FileId";
            string fPath = "";

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
                            fPath = dr["FilePath"].ToString();
                        }
                        dr.Close();
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

            if(fPath != "")
            {
                string folderPath = Server.MapPath("~/Files/");
                string fileName = Path.GetFileName(fPath);
                string filePath = Path.Combine(folderPath, fileName);

                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }
        }

        private void DeleteFromDB(string fileId)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["FilesDB"].ConnectionString;
            string deleteQuery = "DELETE FROM Files WHERE FileId = @FileId";

            using (SqlConnection conn = new SqlConnection(connectionString))
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
        }

        protected void FileGridPaging_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            FileGrid.PageIndex = e.NewPageIndex;
            string selectedCategory = CategoryDropDown.SelectedValue;
            BindGridData(selectedCategory);
        }

        private void GetCategory()
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["FilesDB"].ConnectionString;
            string getCategoryQuery = "SELECT * FROM [Category]";

            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                using(SqlCommand command = new SqlCommand(getCategoryQuery, conn))
                {
                    try
                    {
                        conn.Open();
                        CategoryDropDown.DataSource = command.ExecuteReader();
                        CategoryDropDown.DataTextField = "CategoryName";
                        CategoryDropDown.DataValueField = "CategoryId";
                        CategoryDropDown.DataBind();
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
            CategoryDropDown.Items.Insert(0, new ListItem("--------", "0"));
        }

        protected void CategoryDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedCategory = CategoryDropDown.SelectedValue;
            BindGridData(selectedCategory);
        }

        private void BindGridData(string selectedCategory="0")
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["FilesDB"].ConnectionString;
            string getDataQuery = "";
            if (selectedCategory == "0")
            {
                getDataQuery = "SELECT FileId, FileTitle, FileDesc, FilePath, UploadDate, CategoryName " +
                    "FROM [Files] AS F " +
                    "JOIN [Category] AS C ON F.CategoryId = C.CategoryId " +
                    "ORDER BY CategoryOrder ASC, UploadDate DESC";
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
            else
            {
                getDataQuery = "SELECT FileId, FileTitle, FileDesc, FilePath, UploadDate, CategoryName " +
                    "FROM [Files] AS F " +
                    "JOIN [Category] AS C ON F.CategoryId = C.CategoryId " +
                    "WHERE C.CategoryId = @CategoryId " +
                    "ORDER BY CategoryOrder ASC, UploadDate DESC";

                using(SqlConnection conn = new SqlConnection(connectionString))
                {
                    using(SqlCommand command = new SqlCommand(getDataQuery, conn))
                    {
                        command.Parameters.AddWithValue("@CategoryId", selectedCategory);

                        try
                        {
                            conn.Open();
                            SqlDataAdapter da = new SqlDataAdapter(command);
                            DataTable dt = new DataTable();
                            da.Fill(dt);

                            FileGrid.DataSource = dt;

                            FileGrid.DataBind();
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

            

            
        }

        
    }
}