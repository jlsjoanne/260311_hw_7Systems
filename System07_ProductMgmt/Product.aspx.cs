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

namespace _260311_hw_7Systems.System07_ProductMgmt
{
    public partial class Product : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["ProductId"] != null)
            {
                if(Session["LogInStatus"] != null && Session["LogInStatus"].ToString() == "true")
                {
                    EditProduct.Visible = true;
                    ImgMgmt.Visible = true;
                    FileMgmt.Visible = true;
                }
                string productId = Request.QueryString["ProductId"].ToString();
                GetProductInfo(productId);
                GetImageData(productId);
                GetFileData(productId);

            }
            else if (Request.UrlReferrer != null)
            {
                Response.Redirect(Request.UrlReferrer.ToString());
            }
            else
            {
                Response.Redirect("Homepage.aspx");
            }
        }

        protected void EditProduct_Click(object sender, EventArgs e)
        {
            string productId = Request.QueryString["ProductId"].ToString();
            Response.Redirect($"ProductMgmt.aspx?ProductId={productId}");
        }

        protected void ImgMgmt_Click(object sender, EventArgs e)
        {
            string productId = Request.QueryString["ProductId"].ToString();
            Response.Redirect($"ImageMgmt.aspx?ProductId={productId}");
        }

        protected void FileMgmt_Click(object sender, EventArgs e)
        {
            string productId = Request.QueryString["ProductId"].ToString();
            Response.Redirect($"FileMgmt.aspx?ProductId={productId}");
        }

        private void GetProductInfo(string productId)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["ProductDB"].ConnectionString;
            string getProductQuery = "SELECT P.ProductName, P.ProductDetail, P.UnitPrice, B.BrandName, S.SubCategoryName, M.MainCategoryName " +
                "FROM [Products] AS P " +
                "LEFT JOIN [Brand] AS B ON P.BrandId = B.BrandId " +
                "LEFT JOIN [SubCategory] AS S ON P.SubCId = S.SubCId " +
                "LEFT JOIN [MainCategory] AS M ON S.MainCId = M.MainCId " +
                "WHERE P.ProductId = @ProductId";

            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                using(SqlCommand command = new SqlCommand(getProductQuery, conn))
                {
                    SqlDataReader dr = null;
                    command.Parameters.AddWithValue("@ProductId", productId);

                    try
                    {
                        conn.Open();
                        dr = command.ExecuteReader();

                        if (dr.HasRows)
                        {
                            dr.Read();
                            MainCategory.Text = dr["MainCategoryName"].ToString();
                            SubCategory.Text = dr["SubCategoryName"].ToString();
                            Brand.Text = dr["BrandName"].ToString();
                            PName.Text = dr["ProductName"].ToString();
                            PDetails.Text += dr["ProductDetail"].ToString();
                            Price.Text = dr["UnitPrice"].ToString();
                        }
                    }
                    catch (Exception ex)
                    {
                        Response.Write($"<script>alert('{ex.Message}');</script>");
                    }
                    finally
                    {
                        dr.Close();
                        conn.Close();
                    }
                }
            }
        }

        private void GetImageData(string productId)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["ProductDB"].ConnectionString;
            string getImgQuery = "SELECT * FROM [Images] WHERE ProductId = @ProductId";

            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                using(SqlCommand command = new SqlCommand(getImgQuery, conn))
                {
                    command.Parameters.AddWithValue("@ProductId", productId);

                    try
                    {
                        conn.Open();
                        SqlDataAdapter da = new SqlDataAdapter(command);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        ImgRepeater.DataSource = dt;
                        ImgRepeater.DataBind();
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

        private void GetFileData(string productId)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["ProductDB"].ConnectionString;
            string getFileQuery = "SELECT * FROM [Files] WHERE ProductId = @ProductId";

            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                using(SqlCommand command = new SqlCommand(getFileQuery, conn))
                {
                    command.Parameters.AddWithValue("@ProductId", productId);

                    try
                    {
                        conn.Open();
                        SqlDataAdapter da = new SqlDataAdapter(command);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        FileRepeater.DataSource = dt;
                        FileRepeater.DataBind();
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

        protected void FileRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
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
                    Response.Write("<script>alert('檔案不存在')</script>");
                }
            }
        }
    }
}