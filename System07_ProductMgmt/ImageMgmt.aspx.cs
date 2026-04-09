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

namespace _260311_hw_7Systems.System07_ProductMgmt
{
    public partial class ImageMgmt : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["ProductId"] != null)
            {
                if (Session["LogInStatus"] != null && Session["LogInStatus"].ToString() == "true")
                {
                    AddNew.Visible = true;
                    if (ImgGrid.Columns[0] is CommandField commandField)
                    {
                        commandField.ShowDeleteButton = true;
                    }
                }

                string productId = Request.QueryString["ProductId"].ToString();

                if (!IsPostBack)
                {
                    BindImgData(productId);
                }

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

        protected void AddNew_Click(object sender, EventArgs e)
        {
            string productId = Request.QueryString["ProductId"].ToString();
            Response.Redirect($"Image_Add.aspx?ProductId={productId}");
        }

        protected void ImgGrid_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string imageId = ImgGrid.DataKeys[e.RowIndex].Value.ToString();
            string productId = Request.QueryString["ProductId"].ToString();
            DeleteFromId(imageId);
            BindImgData(productId);

        }

        private void DeleteFromId(string imageId)
        {
            // Delete From Folder
            DeleteImg(imageId);
            
            // Delete From DB
            string connectionString = WebConfigurationManager.ConnectionStrings["ProductDB"].ConnectionString;
            string deleteImgQuery = "DELETE FROM [Images] WHERE ImageId = @ImageId";

            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                using(SqlCommand command = new SqlCommand(deleteImgQuery, conn))
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

        private void DeleteImg(string imageId)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["ProductDB"].ConnectionString;
            string getImgPath = "SELECT IPath FROM [Images] WHERE ImageId = @ImageId";
            string iPath = "";

            using (SqlConnection conn = new SqlConnection(connectionString))
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
                            iPath = dr["IPath"].ToString();
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

            if(iPath != "")
            {
                string folderPath = Server.MapPath("~/Images/");
                string imgName = Path.GetFileName(iPath);
                string imgPath = Path.Combine(folderPath, imgName);

                if (File.Exists(imgPath))
                {
                    File.Delete(imgPath);
                }
            }
        }

        private void BindImgData(string productId)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["ProductDB"].ConnectionString;
            string getImgData = "SELECT * FROM [Images] WHERE ProductId = @ProductId";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using(SqlCommand command = new SqlCommand(getImgData, conn))
                {
                    command.Parameters.AddWithValue("@ProductId",productId);
                    try
                    {
                        conn.Open();
                        SqlDataAdapter da = new SqlDataAdapter(command);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        ImgGrid.DataSource = dt;
                        ImgGrid.DataBind();
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

        protected string CombineImgPath(object folder, object file)
        {
            string folderPath = folder?.ToString() ?? String.Empty;
            string filePath = file?.ToString() ?? String.Empty;
            string filename = Path.GetFileName(filePath);

            return Path.Combine(folderPath, filename);
        }

        protected void GoBack_Click(object sender, EventArgs e)
        {
            string productId = Request.QueryString["ProductId"];
            Response.Redirect($"Product.aspx?ProductId={productId}");
        }
    }
}