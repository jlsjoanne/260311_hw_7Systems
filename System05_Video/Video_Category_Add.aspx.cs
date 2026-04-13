using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace _260311_hw_7Systems.System05_Video
{
    public partial class Video_Category_Add : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["RoleId"] != null && (Session["RoleId"].ToString() == "1" || Session["RoleId"].ToString() == "2"))
            {

            }
            else if(Request.UrlReferrer != null)
            {
                Response.Redirect(Request.UrlReferrer.ToString());
            }
            else
            {
                Response.Redirect("VideoList.aspx");
            }
        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            if(CategoryName.Text == "")
            {
                Response.Write("<script>alert('分類名稱不得為空');</script>");
                return;
            }
            
            AddCategory();
            Response.Redirect("Video_Mgmt.aspx");
        }

        private void AddCategory()
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["VideoDB"].ConnectionString;
            string insertQuery = "INSERT INTO [Category] (CategoryName) VALUES (@CategoryName)";

            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                using(SqlCommand command = new SqlCommand(insertQuery, conn))
                {
                    command.Parameters.AddWithValue("@CategoryName", CategoryName.Text);

                    try
                    {
                        conn.Open();
                        int result = command.ExecuteNonQuery();
                        if(result < 0)
                        {
                            Response.Write("<script>alert('新增分類失敗');</script>");
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