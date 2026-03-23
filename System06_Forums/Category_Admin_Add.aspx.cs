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
    public partial class Category_Admin_Add : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["ForumDB"].ConnectionString;
            string insertAdminQuery = "INSERT INTO Admin (UserName, CategoryID) VALUES (@UserName, @CategoryID)";

            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                using(SqlCommand command = new SqlCommand(insertAdminQuery, conn))
                {
                    command.Parameters.AddWithValue("@UserName", Admin_User.SelectedValue);
                    command.Parameters.AddWithValue("@CategoryID", Admin_Category.SelectedValue);

                    try
                    {
                        conn.Open();
                        int result = command.ExecuteNonQuery();

                        if(result < 0)
                        {
                            Response.Write("<script>alert('權限新增失敗')</script>");
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

            Response.Redirect("Category_Admin.aspx");
        }
    }
}