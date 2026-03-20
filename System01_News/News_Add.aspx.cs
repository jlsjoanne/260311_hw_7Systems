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


namespace _260311_hw_7Systems.System01_News
{
    public partial class News_Add : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                CreateLink(LinkCount);
            }
        }

        private int LinkCount
        {
            get
            {
                if (ViewState["LinkCount"] == null) { return 0; }
                return (int)ViewState["LinkCount"];
            }
            set
            {
                ViewState["LinkCount"] = value;
            }
        }

        protected void AddLink_Click(object sender, EventArgs e)
        {
            LinkCount++;
            CreateLink(LinkCount);
        }

        private void CreateLink(int count)
        {
            PhLink.Controls.Clear();

            for(int i = 0; i < count; i++)
            {
                Label LTitle = new Label();
                Label LUrl = new Label();
                TextBox TitleInput = new TextBox();
                TextBox UrlInput = new TextBox();
                CheckBox IsNew = new CheckBox();

                LTitle.ID = $"LTitle{i}";
                LUrl.ID = $"LUrl{i}";
                TitleInput.ID = $"TitleInput{i}";
                UrlInput.ID = $"UrlInput{i}";
                IsNew.ID = $"IsNew{i}";

                LTitle.Text = "連結名稱";
                LUrl.Text = "Url";
                IsNew.Text = "是否開新視窗";

                PhLink.Controls.Add(LTitle);
                PhLink.Controls.Add(TitleInput);
                PhLink.Controls.Add(new LiteralControl("<br />"));
                PhLink.Controls.Add(LUrl);
                PhLink.Controls.Add(UrlInput);
                PhLink.Controls.Add(new LiteralControl("<br />"));
                PhLink.Controls.Add(IsNew);
                PhLink.Controls.Add(new LiteralControl("<br /><br />"));
            }
        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            // Title, Content, Category to SQL
            string newsID = AddToNewsList();

            // Image to SQL
            AddToImages(newsID);

            // File to SQL
            AddToFiles(newsID);

            // Links to SQL
        }

        private string AddToNewsList()
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["NewsDB"].ConnectionString;
            string insertDataQuery = "INSERT INTO NewsList (NewsTitle, NewsContent, CategoryID) VALUES (@NewsTitle, @NewsContent, @CategoryID)";

            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                using(SqlCommand command = new SqlCommand(insertDataQuery, conn))
                {
                    command.Parameters.AddWithValue("@NewsTitle", NewTitle.Text);
                    command.Parameters.AddWithValue("@NewsContent", Content.Text);
                    command.Parameters.AddWithValue("@CategoryID", DropDownList1.SelectedValue);

                    try
                    {
                        conn.Open();
                        int result = command.ExecuteNonQuery();

                        if (result < 0)
                        {
                            Response.Write("<script>alert('新增文章失敗')</script>");
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

            string getNewsIDQuery = "SELECT NewsID FROM NewsList WHERE NewsTitle = @NewsTitle AND NewsContent = @NewsContent AND CategoryID = @CategoryID";

            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                using(SqlCommand command = new SqlCommand(getNewsIDQuery, conn))
                {
                    command.Parameters.AddWithValue("@NewsTitle", NewTitle.Text);
                    command.Parameters.AddWithValue("@NewsContent", Content.Text);
                    command.Parameters.AddWithValue("@CategoryID", DropDownList1.SelectedValue);

                    SqlDataReader dr = null;
                    try
                    {
                        conn.Open();
                        dr = command.ExecuteReader();

                        if (dr.HasRows)
                        {
                            dr.Read();
                            return dr["NewsID"].ToString();
                        }
                        else
                        {
                            throw new Exception("Can't find NewsID");
                        }
                    }
                    catch(Exception ex)
                    {
                        Response.Write($"<script>alert('{ex.Message}')</script>");
                        throw new Exception(ex.Message);
                    }
                    finally
                    {
                        dr.Close();
                        conn.Close();
                    }
                }
            }
        }

        private void AddToImages(string newsID)
        {
            if (ImagesUpload.HasFiles)
            {
                string folderPath = Server.MapPath("~/Images/");
                string connectionString = WebConfigurationManager.ConnectionStrings["NewsDB"].ConnectionString;
                string addToImagesQuery = "INSERT INTO Images (IPath, NewsID) VALUES (@IPath, @NewsID)";

                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                foreach(HttpPostedFile postedFile in ImagesUpload.PostedFiles)
                {
                    string fileName = Path.GetFileName(postedFile.FileName);
                    postedFile.SaveAs(Path.Combine(folderPath, fileName));

                    using(SqlConnection conn = new SqlConnection(connectionString))
                    {
                        using(SqlCommand command = new SqlCommand(addToImagesQuery, conn))
                        {
                            command.Parameters.AddWithValue("@IPath", Path.Combine(folderPath, fileName));
                            command.Parameters.AddWithValue("@NewsID", newsID);

                            try
                            {
                                conn.Open();
                                int result = command.ExecuteNonQuery();

                                if (result < 0)
                                {
                                    Response.Write("<script>alert('新增圖片失敗')</script>");
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
            }
        }

        private void AddToFiles(string newsID)
        {
            if (FilesUpload.HasFiles)
            {
                string folderPath = Server.MapPath("~/Files/");
                string connectionString = WebConfigurationManager.ConnectionStrings["NewsDB"].ConnectionString;
                string addToFilesQuery = "INSERT INTO Files (FPath, NewsID) VALUES (@FPath, @NewsID)";

                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                foreach(HttpPostedFile postedFile in FilesUpload.PostedFiles)
                {
                    string fileName = Path.GetFileName(postedFile.FileName);
                    postedFile.SaveAs(Path.Combine(folderPath, fileName));

                    using(SqlConnection conn = new SqlConnection(connectionString))
                    {
                        using(SqlCommand command = new SqlCommand(addToFilesQuery, conn))
                        {
                            command.Parameters.AddWithValue("@FPath", Path.Combine(folderPath, fileName));
                            command.Parameters.AddWithValue("@NewsID", newsID);

                            try
                            {
                                conn.Open();
                                int result = command.ExecuteNonQuery();

                                if(result < 0)
                                {
                                    Response.Write("<script>alert('新增檔案失敗')</script>");
                                }
                            }
                            catch(Exception ex)
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

        private void AddToLinks(string newsID)
        {
            List<string[]> links = new List<string[]>();
            string connectionString = WebConfigurationManager.ConnectionStrings["NewsDB"].ConnectionString;

            for (int i = 0; i< LinkCount; i++)
            {
                string[] link = new string[3];
                TextBox titleTb = (TextBox)PhLink.FindControl($"TitleInput{i}");
                TextBox contentTb = (TextBox)PhLink.FindControl($"UrlInput{i}");
                CheckBox isNew = (CheckBox)PhLink.FindControl($"IsNew{i}");

                link[0] = titleTb.Text;
                link[1] = contentTb.Text;

                if (isNew.Checked)
                {
                    link[2] = "TRUE";
                }
                else
                {
                    link[2] = "FALSE";
                }

                links.Add(link);
            }


            string addToLinksQuery = "INSER INTO Links (LName, LUrl, IsNewPage, NewsID) VALUES (@LName, @LUrl, @IsNew, @NewsID)";

            foreach (string[] link in links)
            {
                if (link[1] != "" || link[1] != null)
                {
                    using(SqlConnection conn = new SqlConnection(connectionString))
                    {
                        using(SqlCommand command = new SqlCommand(addToLinksQuery, conn))
                        {
                            command.Parameters.AddWithValue("@LName", link[0]);
                            command.Parameters.AddWithValue("@LUrl", link[1]);
                            command.Parameters.AddWithValue("@IsNew", link[2]);
                            command.Parameters.AddWithValue("@NewsID", newsID);

                            try
                            {
                                conn.Open();
                                int result = command.ExecuteNonQuery();
                                if(result < 0)
                                {
                                    Response.Write("<script>alert('新增連結失敗')</script>");
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
            }


        }

        
    }
}