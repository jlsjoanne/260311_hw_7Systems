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
    public partial class FileMgmt : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["ProductId"] != null)
            {
                if (Session["LogInStatus"] != null && Session["LogInStatus"].ToString() == "true")
                {
                    AddNew.Visible = true;
                    if (FileGrid.Columns[0] is CommandField commandField)
                    {
                        commandField.ShowDeleteButton = true;
                    }
                }

                string productId = Request.QueryString["ProductId"].ToString();

                if (!IsPostBack)
                {
                    BindFileData(productId);
                }
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

        protected void AddNew_Click(object sender, EventArgs e)
        {
            string productId = Request.QueryString["ProductId"].ToString();
            Response.Redirect($"File_Add.aspx?ProductId={productId}");
        }

        private void BindFileData(string productId)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["ProductDB"].ConnectionString;
            string getFileData = "SELECT * FROM [Files] WHERE ProductId = @ProductId";

            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                using(SqlCommand command = new SqlCommand(getFileData, conn))
                {
                    command.Parameters.AddWithValue("@ProductId", productId);

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

        protected void FileGrid_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string fileId = FileGrid.DataKeys[e.RowIndex].Value.ToString();
            string productId = Request.QueryString["ProductId"].ToString();
            DeleteFromId(fileId);
            e.Cancel = true;
            BindFileData(productId);
        }

        private void DeleteFromId(string fileId)
        {
            //Delete from folder
            DeleteFile(fileId);
            //Delete from DB
            string connectionString = WebConfigurationManager.ConnectionStrings["ProductDB"].ConnectionString;
            string deleteFileQuery = "DELETE FROM [Files] WHERE FileId = @FileId";

            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                using(SqlCommand command = new SqlCommand(deleteFileQuery, conn))
                {
                    command.Parameters.AddWithValue("@FileId", fileId);

                    try
                    {
                        conn.Open();
                        int result = command.ExecuteNonQuery();
                        if(result < 0)
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

        private void DeleteFile(string fileId)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["ProductDB"].ConnectionString;
            string getPathQuery = "SELECT FPath FROM [Files] WHERE FileId = @FileId";
            string fPath = "";

            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                using(SqlCommand command = new SqlCommand(getPathQuery, conn))
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
                            fPath = dr["FPath"].ToString();
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

        protected void GoBack_Click(object sender, EventArgs e)
        {
            string productId = Request.QueryString["ProductId"];
            Response.Redirect($"Product.aspx?ProductId={productId}");
        }
    }
}