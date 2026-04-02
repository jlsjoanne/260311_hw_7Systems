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

namespace _260311_hw_7Systems.System07_ProductMgmt
{
    public partial class Category_AddSub : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            if(SubCategory.Text != "")
            {
                AddSub();
                Response.Redirect("CategoryMgmt.aspx");
            }
            else
            {
                Response.Write("<script>alert('輸入值不得為空');</sctipt>");
            }
        }

        private void AddSub()
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["ProductDB"].ConnectionString;
            string insertToSub = "INSERT INTO SubCategory (SubCategoryName, MainCId) VALUES (@SubCategoryName, @MainCId)";

            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                using(SqlCommand command = new SqlCommand(insertToSub, conn))
                {
                    command.Parameters.AddWithValue("@SubCategoryName", SubCategory.Text);
                    command.Parameters.AddWithValue("@MainCId", MainDropdown.SelectedValue);

                    try
                    {
                        conn.Open();
                        int result = command.ExecuteNonQuery();

                        if(result < 0)
                        {
                            Response.Write("<script>alert('子類別新增失敗');</script>");
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