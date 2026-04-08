using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Configuration;
using System.Data.SqlClient;

namespace _260311_hw_7Systems.System07_ProductMgmt
{
    public partial class Image_Add : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Request.QueryString["ProductId"] != null && Session["LogInStatus"] != null && Session["LogInStatus"].ToString() == "true")
            {
                
            }
            else if(Request.UrlReferrer != null)
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
            if(ImgName.Text == "")
            {
                Response.Write("<script>alert('圖片名稱不可為空')</script>");
                return;
            }
            
            string productId = Request.QueryString["ProductId"].ToString();
            string savedPath = ImgUpload();
            if(savedPath == "Failed")
            {
                Response.Write("<script>alert('圖片上傳失敗')</script>");
                return;
            }
            InsertImgToDB(productId, savedPath);
            Response.Redirect($"ImageMgmt.aspx?ProductId={productId}");
        }

        private void InsertImgToDB(string productId, string savedPath)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["ProductDB"].ConnectionString;
            string insertImgQuery = "INSERT INTO Images (IName, IDesc, IPath, ProductId) " +
                "VALUES(@IName, @IDesc, @IPath, @ProductId)";

            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(insertImgQuery, conn))
                {
                    command.Parameters.AddWithValue("@IName", ImgName.Text);
                    command.Parameters.AddWithValue("@IDesc", ImgDesc.Text);
                    command.Parameters.AddWithValue("@IPath", savedPath);
                    command.Parameters.AddWithValue("@ProductId", productId);

                    try
                    {
                        conn.Open();
                        int result = command.ExecuteNonQuery();
                        if(result < 0)
                        {
                            Response.Write("<script>alert('圖片寫入資料庫失敗')</script>");
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

        private string ImgUpload()
        {
            if (ImgFileUpload.HasFile)
            {
                string saveDir = Server.MapPath("~/Images/");
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(ImgFileUpload.FileName);
                string savePath = Path.Combine(saveDir, fileName);
                try
                {
                    if (!Directory.Exists(saveDir))
                    {
                        Directory.CreateDirectory(saveDir);
                    }

                    ImgFileUpload.SaveAs(savePath);
                }
                catch(Exception ex)
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