using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.IO;
using System.Data;

namespace _260311_hw_7Systems.System04_Ads
{
    public partial class AdsList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["RoleId"] != null && (Session["RoleId"].ToString() == "1" || Session["RoleId"].ToString() == "2"))
            {
                AddNew.Visible = true;
                AdsMgmt.Visible = true;
            }
            if (!IsPostBack)
            {
                BindCategory();
            }
        }

        protected void AddNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("Ads_Add.aspx");
        }

        protected void AdsMgmt_Click(object sender, EventArgs e)
        {
            Response.Redirect("Ads_Mgmt.aspx");
        }

        protected void BindCategory()
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["AdsDB"].ConnectionString;
            string getCategoryQuery = "SELECT * FROM [Category] WHERE IsPublished = 1 ORDER BY [CategoryOrder] ASC";

            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                using(SqlCommand command = new SqlCommand(getCategoryQuery, conn))
                {
                    try
                    {
                        conn.Open();
                        SqlDataAdapter da = new SqlDataAdapter(command);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        CategoryRepeater.DataSource = dt;
                        CategoryRepeater.DataBind();
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

        protected void CatRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            string categoryId = DataBinder.Eval(e.Item.DataItem, "CategoryId").ToString();

            Repeater childRepeater = (Repeater)e.Item.FindControl("AdRepeater");

            childRepeater.DataSource = BindAdsData(categoryId);
            childRepeater.DataBind();
        }

        protected DataTable BindAdsData(string categoryId)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["AdsDB"].ConnectionString;
            string getAdsQuery = "SELECT * FROM [Ads] WHERE CategoryId = @CategoryId";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(getAdsQuery, conn))
                {
                    command.Parameters.AddWithValue("@CategoryId", categoryId);
                    try
                    {
                        conn.Open();
                        SqlDataAdapter da = new SqlDataAdapter(command);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        conn.Close();

                        return dt;
                        
                    }
                    catch (Exception ex)
                    {
                        Response.Write($"<script>alert('{ex.Message}')</script>");
                        return null;
                    }
                    
                }
            }
        }

        protected string CombinePath(object folder, object file)
        {
            string folderPath = folder?.ToString() ?? String.Empty;
            string filePath = file?.ToString() ?? String.Empty;
            string filename = Path.GetFileName(filePath);

            return Path.Combine(folderPath, filename);
        }

        protected void AdRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "ToUrl")
            {
                string AdUrl = e.CommandArgument.ToString();
                Response.Redirect(AdUrl);
            }
        }

        
    }
}