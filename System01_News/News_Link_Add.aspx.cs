using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace _260311_hw_7Systems.System01_News
{
    public partial class News_Link_Add : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["NewsId"] != null)
            {

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

        protected void Submit_Click(object sender, EventArgs e)
        {
            string newsId = Request.QueryString["NewsId"];
            AddLink(newsId);
            Response.Redirect($"News_Link_Mgmt.aspx?NewsId={newsId}");
        }

        private void AddLink(string newsId)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["NewsDB"].ConnectionString;
            string addLinkQuery = "INSERT INTO [Links] (LName, LUrl, IsNewPage, NewsId) " +
                "VALUES (@LName, @LUrl, @IsNewPage, @NewsId)";

            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                using(SqlCommand command = new SqlCommand(addLinkQuery, conn))
                {
                    command.Parameters.AddWithValue("@LName", LinkName.Text);
                    command.Parameters.AddWithValue("@LUrl", LinkUrl.Text);
                    command.Parameters.AddWithValue("@IsNewPage", IsNewPage.Checked);
                    command.Parameters.AddWithValue("@NewsId", newsId);

                    try
                    {
                        conn.Open();
                        int result = command.ExecuteNonQuery();
                        if(result < 0)
                        {
                            Response.Write("<script>alert('連結寫入資料庫失敗')</script>");
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
    }
}