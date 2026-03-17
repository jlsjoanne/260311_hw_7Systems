using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Configuration;
using System.Data.SqlClient;

namespace _260311_hw_7Systems.System06_Forums
{
    public partial class Category_Add : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            
            string connectionString = WebConfigurationManager.ConnectionStrings["ForumDB"].ConnectionString;

            string insertCategory = "INSERT INTO Category(CategoryName) VALUES (@CategoryName);";

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = connectionString;

            SqlCommand command = new SqlCommand(insertCategory, conn);
            command.Parameters.AddWithValue("@CategoryName", CategoryName.Text);

            try
            { 
                conn.Open();
                int result = command.ExecuteNonQuery();
                
                command.Cancel();
                conn.Close();
                if(result < 0)
                {
                    Response.Write("<script>alert('新增分類失敗')</script>");
                }
                else
                {
                    Response.Write("<script>alert('新增分類成功')</script>");
                    Response.Redirect("CategoryList.aspx");
                }
            }
            catch (Exception ex)
            {
                Response.Write($"<script>alert('{ex.Message}')</script>");
            }
        }
    }
}