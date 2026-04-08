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

namespace _260311_hw_7Systems.System04_Ads
{
    public partial class Ads_Add : System.Web.UI.Page
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
                Response.Redirect("AdsList.aspx");
            }
        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            InsertAdsData();
            Response.Redirect("AdsList.aspx");
        }

        private void InsertAdsData()
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["AdsDB"].ConnectionString;
            string insertDataQuery = "INSERT INTO Ads (AdName, AdUrl, AdImgPath, CategoryId) VALUES (@AdName, @AdUrl, @AdImgPath, @CategoryId)";

            string folderPath = Server.MapPath("~/Images/");
            string fileName = "";
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            if (ImgUpload.HasFile)
            {
                fileName = Guid.NewGuid().ToString() + Path.GetExtension(ImgUpload.FileName);
                ImgUpload.SaveAs(Path.Combine(folderPath, fileName));
            }
            else
            {
                Response.Write("<script>alert('無附圖檔')</script>");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using(SqlCommand command = new SqlCommand(insertDataQuery, conn))
                {
                    command.Parameters.AddWithValue("@AdName", AdsName.Text);
                    command.Parameters.AddWithValue("@AdUrl", AdsUrl.Text);
                    command.Parameters.AddWithValue("@AdImgPath", Path.Combine(folderPath, fileName));
                    command.Parameters.AddWithValue("@CategoryId", AddDropDown.SelectedValue);

                    try
                    {
                        conn.Open();
                        int result = command.ExecuteNonQuery();
                        if(result < 0)
                        {
                            Response.Write("<script>alert('廣告寫入資料庫失敗')</script>");
                        }
                    }
                    catch (Exception ex)
                    {
                        Response.Write($"<script>alert('{ex.Message}')<script>");
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