using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Web.Configuration;
using System.Data.SqlClient;

namespace _260311_hw_7Systems.System03_File
{
    public partial class File_Add : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["RoleId"] != null && (Session["RoleId"].ToString() == "1" || Session["RoleId"].ToString() == "2"))
            {

            }
            else if (Request.UrlReferrer != null)
            {
                Response.Redirect(Request.UrlReferrer.ToString());
            }
            else
            {
                Response.Redirect("FileList.aspx");
            }
        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["FilesDB"].ConnectionString;
            string insertFileQuery = "INSERT INTO Files (FileTitle, FileDesc, FilePath, CategoryId) VALUES (@FileTitle, @FileDesc, @FilePath, @CategoryId);";
            string fileName = FileName.Text;
            if(fileName == "")
            {
                fileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
            }

            if (FileUpload1.HasFile)
            {
                string folderPath = Server.MapPath("~/Files/");

                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                string savedFile = Guid.NewGuid().ToString() + Path.GetExtension(FileUpload1.FileName);
                FileUpload1.SaveAs(Path.Combine(folderPath, savedFile));

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(insertFileQuery, conn))
                    {
                        command.Parameters.AddWithValue("@FileTitle", fileName);
                        command.Parameters.AddWithValue("@FileDesc", FileDesc.Text);
                        command.Parameters.AddWithValue("@FilePath", Path.Combine(folderPath, savedFile));
                        command.Parameters.AddWithValue("@CategoryId", CategoryDropdownList.SelectedValue);

                        try
                        {
                            conn.Open();
                            int result = command.ExecuteNonQuery();
                            if(result < 0)
                            {
                                Response.Write("<script>alert('檔案無法寫入資料庫')</script>");
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
            else
            {
                Response.Write("<script>alert('無選擇上傳檔案')</script>");
                return;
            }

            Response.Redirect("FileList.aspx");
        }
    }
}