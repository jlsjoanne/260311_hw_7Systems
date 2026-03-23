using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Data.SqlClient;


namespace _260311_hw_7Systems.System01_News
{
    public partial class News_Add_File : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Request.QueryString["NewsID"] != null)
            {
                GetFileData();
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

        static List<string> FileIdList = new List<string>();
        private void GetFileData()
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["NewsDB"].ConnectionString;
            string newsID = Request.QueryString["NewsID"].ToString();
            string getFileDataQuery = "SELECT * FROM Files WHERE NewsId = @NewsId";

            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                using(SqlCommand command = new SqlCommand(getFileDataQuery, conn))
                {
                    command.Parameters.AddWithValue("@NewsId", newsID);
                    SqlDataReader dr = null;

                    try
                    {
                        conn.Open();
                        dr = command.ExecuteReader();

                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                string fileID = dr["FileId"].ToString();

                                if (!FileIdList.Contains(fileID))
                                {
                                    FileIdList.Add(fileID);
                                }

                                Label FName = new Label();
                                Label FDesc = new Label();
                                Label FPath = new Label();
                                TextBox FNameIn = new TextBox();
                                TextBox FDescIn = new TextBox();

                                FName.ID = $"FName_{fileID}";
                                FDesc.ID = $"FDesc_{fileID}";
                                FPath.ID = $"FPath_{fileID}";
                                FNameIn.ID = $"FNameIn_{fileID}";
                                FDescIn.ID = $"FDescIn_{fileID}";

                                FName.Text = "檔案名稱";
                                FDesc.Text = "檔案說明";
                                FPath.Text = "儲存檔案名稱: " + Path.GetFileName(dr["FPath"].ToString());

                                PhFile.Controls.Add(FName);
                                PhFile.Controls.Add(FNameIn);
                                PhFile.Controls.Add(new LiteralControl("<br />"));
                                PhFile.Controls.Add(FDesc);
                                PhFile.Controls.Add(FDescIn);
                                PhFile.Controls.Add(new LiteralControl("<br />"));
                                PhFile.Controls.Add(FPath);
                                PhFile.Controls.Add(new LiteralControl("<br />"));
                            }
                        }
                    }
                    catch(Exception ex)
                    {
                        Response.Write($"<script>alert('{ex.Message}')</script>");
                    }
                    finally
                    {
                        dr.Close();
                        conn.Close();
                    }
                }
            }
        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["NewsDB"].ConnectionString;
            string updateFileInfoQuery = "Update Files SET FName = @FName, FDesc = @FDesc WHERE FileId = @FileId";

            foreach(string fileId in FileIdList)
            {
                TextBox FNameIn = (TextBox)PhFile.FindControl($"FNameIn_{fileId}");
                TextBox FDescIn = (TextBox)PhFile.FindControl($"FDescIn_{fileId}");
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using(SqlCommand command = new SqlCommand(updateFileInfoQuery, conn))
                    {
                        command.Parameters.AddWithValue("@FName", FNameIn.Text);
                        command.Parameters.AddWithValue("@FDesc", FDescIn.Text);
                        command.Parameters.AddWithValue("@FileId", fileId);

                        try
                        {
                            conn.Open();
                            int result = command.ExecuteNonQuery();
                            if(result < 0)
                            {
                                Response.Write("<script>alert('新增檔案資訊失敗')</script>");
                            }
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

            string newsID = Request.QueryString["NewsID"].ToString();
            Response.Redirect($"News_Add_Link.aspx?NewsID={newsID}");
        }
    }
}