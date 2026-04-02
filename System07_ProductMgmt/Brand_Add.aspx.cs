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
    public partial class Brand_Add : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            if(BrandName.Text != "")
            {
                AddBrand();
                Response.Redirect("BrandMgmt.aspx");
            }
            else
            {
                Response.Write("<script>alert('輸入值不得為空')</script>");
            }
        }

        private void AddBrand()
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["ProductDB"].ConnectionString;
            string insertBrandQuery = "INSERT INTO Brand (BrandName) VALUES (@BrandName)";

            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                using(SqlCommand command = new SqlCommand(insertBrandQuery,conn))
                {
                    command.Parameters.AddWithValue("@BrandName", BrandName.Text);

                    try
                    {
                        conn.Open();
                        int result = command.ExecuteNonQuery();

                        if(result < 0)
                        {
                            Response.Write("<script>alert('品牌新增失敗');</script>");
                        }
                    }
                    catch (Exception ex)
                    {
                        Response.Write($"<script>alert('{ex.Message}'); </script>");
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