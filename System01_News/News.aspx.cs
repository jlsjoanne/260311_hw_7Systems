using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace _260311_hw_7Systems.System01_News
{
    public partial class News : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["NewsId"] != null)
            {
                if (Session["RoleId"] != null && (Session["RoleId"].ToString() == "1" || Session["RoleId"].ToString() == "2"))
                {
                    NewsEdit.Visible = true;
                    ImgMgmt.Visible = true;
                    FileMgmt.Visible = true;
                    LinkMgmt.Visible = true;
                    Publish.Visible = true;
                }
                string newsId = Request.QueryString["NewsId"].ToString();
                GetNewsData(newsId);
                GetImgData(newsId);
                GetFileData(newsId);
                GetLinkData(newsId);
                
                
            }
            else if(Request.UrlReferrer != null)
            {
                Response.Redirect(Request.UrlReferrer.ToString());
            }
            else
            {
                Response.Redirect("News_List.aspx");
            }
        }

        private void GetNewsData(string newsId)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["NewsDB"].ConnectionString;
            string getnewsQuery = "SELECT NewsTitle, NewsContent, PostDate, C.CategoryName FROM NewsList AS N JOIN Category AS C ON N.CategoryId = C.CategoryId WHERE NewsId = @NewsId";

            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                using(SqlCommand command = new SqlCommand(getnewsQuery, conn))
                {
                    command.Parameters.AddWithValue("@NewsId", newsId);
                    SqlDataReader dr = null;

                    try
                    {
                        conn.Open();
                        dr = command.ExecuteReader();

                        if (dr.HasRows)
                        {
                            dr.Read();
                            NewsTitle.Text = dr["NewsTitle"].ToString();
                            NewsCategory.Text = dr["CategoryName"].ToString();
                            PostedTime.Text = dr["PostDate"].ToString();
                            NewsContent.Text += dr["NewsContent"].ToString();
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

        private void GetImgData(string newsId)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["NewsDB"].ConnectionString;
            string getImgQuery = "SELECT * FROM Images WHERE NewsId = @NewsId";

            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                using(SqlCommand command = new SqlCommand(getImgQuery, conn))
                {
                    command.Parameters.AddWithValue("@NewsId", newsId);
                    SqlDataReader dr = null;

                    try
                    {
                        conn.Open();
                        dr = command.ExecuteReader();
                        string folderPath = "~/Images/";

                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                
                                Image imgBtn = new Image();
                                imgBtn.ID = dr["ImageId"].ToString();
                                imgBtn.ImageUrl = Path.Combine(folderPath, Path.GetFileName(dr["IPath"].ToString()));
                                imgBtn.AlternateText = dr["IName"].ToString();
                                imgBtn.ToolTip = dr["IDesc"].ToString();
                                imgBtn.Height = new Unit("50px");
                                imgBtn.Width = new Unit("100px");

                                PhImg.Controls.Add(imgBtn);
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

        
        private void GetFileData(string newsId)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["NewsDB"].ConnectionString;
            string getFileQuery = "SELECT * FROM [Files] WHERE NewsId = @NewsId";

            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(getFileQuery, conn))
                {
                    command.Parameters.AddWithValue("@NewsId", newsId);
                    SqlDataReader dr = null;
                    string folderPath = "~/Files/";

                    try
                    {
                        conn.Open();
                        dr = command.ExecuteReader();

                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                LinkButton fileBtn = new LinkButton();
                                fileBtn.ID = dr["FileId"].ToString();
                                fileBtn.Text = dr["FName"].ToString();
                                fileBtn.ToolTip = dr["FDesc"].ToString();
                                fileBtn.CommandArgument = folderPath + Path.GetFileName(dr["FPath"].ToString());
                                fileBtn.Click += new EventHandler(fileBtn_Click);

                                PhFile.Controls.Add(fileBtn);
                                PhFile.Controls.Add(new LiteralControl("<br />"));
                            }
                        }
                    }
                    catch (Exception ex)
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

        protected void fileBtn_Click(object sender, EventArgs e)
        {
            string filePath = (sender as LinkButton).CommandArgument;
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

        private void GetLinkData(string newsId)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["NewsDB"].ConnectionString;
            string getLinkQuery = "SELECT * FROM Links WHERE NewsId = @NewsId";

            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                using(SqlCommand command = new SqlCommand(getLinkQuery, conn))
                {
                    command.Parameters.AddWithValue("@NewsId", newsId);
                    SqlDataReader dr = null;

                    try
                    {
                        conn.Open();
                        dr = command.ExecuteReader();

                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                HyperLink link = new HyperLink();
                                link.ID = dr["LinkId"].ToString();
                                link.Text = dr["LName"].ToString();
                                link.NavigateUrl = dr["LUrl"].ToString();
                                if (dr["IsNewPage"].ToString() == "True")
                                {
                                    link.Target = "_blank";
                                }

                                PhLink.Controls.Add(link);
                                PhLink.Controls.Add(new LiteralControl("<br />"));
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

        protected void Publish_Click(object sender, EventArgs e)
        {
            string newsId = Request.QueryString["NewsId"];
            string connectionString = WebConfigurationManager.ConnectionStrings["NewsDB"].ConnectionString;
            string publishQuery = "UPDATE [NewsList] SET IsPublish = 1 WHERE NewsId = @NewsId";

            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                using(SqlCommand command = new SqlCommand(publishQuery, conn))
                {
                    command.Parameters.AddWithValue("@NewsId", newsId);

                    try
                    {
                        conn.Open();
                        int result = command.ExecuteNonQuery();
                        if(result < 0)
                        {
                            Response.Write("<script>alert('發布消息失敗');</script>");
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

            Response.Redirect("News_List.aspx");
        }

        protected void NewsEdit_Click(object sender, EventArgs e)
        {
            string newsId = Request.QueryString["NewsId"];
            Response.Redirect($"News_Edit.aspx?NewsId={newsId}");
        }

        protected void ImgMgmt_Click(object sender, EventArgs e)
        {
            string newsId = Request.QueryString["NewsId"];
            Response.Redirect($"News_Img_Mgmt.aspx?NewsId={newsId}");
        }

        protected void FileMgmt_Click(object sender, EventArgs e)
        {
            string newsId = Request.QueryString["NewsId"];
            Response.Redirect($"News_File_Mgmt.aspx?NewsId={newsId}");
        }

        protected void LinkMgmt_Click(object sender, EventArgs e)
        {
            string newsId = Request.QueryString["NewsId"];
            Response.Redirect($"News_Link_Mgmt.aspx?NewsId={newsId}");
        }
    }
}