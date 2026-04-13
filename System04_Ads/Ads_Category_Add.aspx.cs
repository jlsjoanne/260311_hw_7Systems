using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Data;
using System.Data.SqlClient;


namespace _260311_hw_7Systems.System04_Ads
{
    public partial class Ads_Category_Add : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["RoleId"] != null && (Session["RoleId"].ToString() == "1" || Session["RoleId"].ToString() == "2"))
            {

            }
            else if (Request.UrlReferrer != null)
            {
                Response.Redirect(Request.UrlReferrer.ToString());
            }
            else
            {
                Response.Redirect("AdsList.aspx");
            }
        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            AddCategory();
            Response.Redirect("Ads_Mgmt.aspx");
        }

        private void AddCategory()
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["AdsDB"].ConnectionString;
            string insertCategory = "INSERT INTO [Category] (CategoryName) VALUES (@CategoryName)";

            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                using(SqlCommand command = new SqlCommand(insertCategory, conn))
                {
                    command.Parameters.AddWithValue("@CategoryName", CName.Text);

                    try
                    {
                        conn.Open();
                        int result = command.ExecuteNonQuery();
                        if(result < 0)
                        {
                            Response.Write("<script>alert('新增分類失敗')</script>");
                        }
                    }
                    catch (Exception ex)
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