using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace _260311_hw_7Systems.System01_News
{
    public partial class News_Add_Link : System.Web.UI.Page
    {
        
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["NewsID"] != null)
            {
                int linkNum = int.Parse(AddLinkNum.SelectedValue);
                AddLinkControls(linkNum);
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

        private void AddLinkControls(int num)
        {
            for(int i = 0; i < num; i++)
            {
                Label LName = new Label();
                Label LUrl = new Label();
                TextBox NameInput = new TextBox();
                TextBox UrlInput = new TextBox();
                CheckBox IsNew = new CheckBox();

                LName.ID = $"LName_{i}";
                LUrl.ID = $"LUrl_{i}";
                NameInput.ID = $"NameInput_{i}";
                UrlInput.ID = $"UrlInput_{i}";
                IsNew.ID = $"IsNew_{i}";

                LName.Text = "連結名稱: ";
                LUrl.Text = "Url: ";
                IsNew.Text = "是否開新視窗";

                PhLink.Controls.Add(LName);
                PhLink.Controls.Add(NameInput);
                PhLink.Controls.Add(new LiteralControl("<br />"));
                PhLink.Controls.Add(LUrl);
                PhLink.Controls.Add(UrlInput);
                PhLink.Controls.Add(new LiteralControl("<br />"));
                PhLink.Controls.Add(IsNew);
                PhLink.Controls.Add(new LiteralControl("<br />"));
            }
            
            

            
        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["NewsDB"].ConnectionString;
            string insertLinkQuery = "INSERT INTO Links (LName, LUrl, IsNewPage, NewsId) VALUES (@LName, @LUrl, @IsNewPage, @NewsId)";
            int linkNum = int.Parse(AddLinkNum.SelectedValue);
            
            

            for(int i = 0; i < linkNum; i++)
            {
                TextBox LNameIn = (TextBox)PhLink.FindControl($"NameInput_{i}");
                TextBox LUrlIn = (TextBox)PhLink.FindControl($"UrlInput_{i}");
                CheckBox IsNew = (CheckBox)PhLink.FindControl($"IsNew_{i}");

                using(SqlConnection conn = new SqlConnection(connectionString))
                {
                    using(SqlCommand command = new SqlCommand(insertLinkQuery, conn))
                    {
                        command.Parameters.AddWithValue("@LName", LNameIn.Text);
                        command.Parameters.AddWithValue("@LUrl", LUrlIn.Text);
                        command.Parameters.AddWithValue("@IsNewPage", IsNew.Checked);
                        command.Parameters.AddWithValue("@NewsId", Request.QueryString["NewsID"].ToString());

                        try
                        {
                            conn.Open();
                            int result = command.ExecuteNonQuery();
                            if(result < 0)
                            {
                                Response.Write($"<script>alert('新增連結失敗')</script>");
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

            Response.Redirect("News_List.aspx");

        }
    }
}