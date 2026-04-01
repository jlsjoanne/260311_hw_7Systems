using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace _260311_hw_7Systems.System07_ProductMgmt
{
    public partial class File_Add : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["ProductId"] != null && Session["LogInStatus"] != null && Session["LogInStatus"].ToString() == "true")
            {

            }
            else if (Request.UrlReferrer != null)
            {
                Response.Redirect(Request.UrlReferrer.ToString());
            }
            else
            {
                Response.Redirect("HomePage.aspx");
            }
        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            if(FName.Text == "")
            {
                Response.Write("<script>alert('檔案名稱不可為空')</script>");
                return;
            }
            string productId = Request.QueryString["ProductId"].ToString();
            string FPath = FileUpload();
            if(FPath == "Failed")
            {
                Response.Write("<script>alert('圖片上傳失敗')</script>");
                return;
            }
            InsertFileToDB(productId, FPath);
            Response.Redirect($"FileMgmt.aspx?ProductId={productId}");

        }

        private void InsertFileToDB(string productId, string FPath)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["ProductDB"].ConnectionString;
            string insertFileQuery = "INSERT INTO [Files] (FName, FDesc, FPath, ProductId) " +
                "VALUES (@FName, @FDesc, @FPath, @ProductId)";

            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                using(SqlCommand command = new SqlCommand(insertFileQuery, conn))
                {
                    command.Parameters.AddWithValue("@FName", FName.Text);
                    command.Parameters.AddWithValue("@FDesc", FDesc.Text);
                    command.Parameters.AddWithValue("@FPath", FPath);
                    command.Parameters.AddWithValue("@ProductId", productId);

                    try
                    {
                        conn.Open();
                        int result = command.ExecuteNonQuery();
                        if(result < 0)
                        {
                            Response.Write("<script>alert('檔案寫入資料庫失敗')</script>");
                        }
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

        private string FileUpload()
        {
            if (RelatedFileUpload.HasFiles)
            {
                string saveDir = Server.MapPath("~/Files/");
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(RelatedFileUpload.FileName);
                string savePath = Path.Combine(saveDir, fileName);

                try
                {
                    if (!Directory.Exists(saveDir))
                    {
                        Directory.CreateDirectory(saveDir);
                    }
                    RelatedFileUpload.SaveAs(savePath);
                }
                catch (Exception ex)
                {
                    Response.Write($"<script>alert('上傳失敗:{ex.Message}')</script>");
                    return "Failed";
                }
                return savePath;
            }
            else
            {
                return "Failed";
            }
        }
    }
}