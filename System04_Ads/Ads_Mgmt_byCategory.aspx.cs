using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _260311_hw_7Systems.System04_Ads
{
    public partial class Ads_Mgmt_byCategory : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["CategoryId"] != null)
            {
                if (!IsPostBack)
                {
                    BindAdsGrid();
                }

            }
            else if(Request.UrlReferrer != null)
            {
                Response.Redirect(Request.UrlReferrer.ToString());
            }
            else
            {
                Response.Redirect("AdsList.aspx");
            }
        }

        protected void AddNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("Ads_Add.aspx");
        }

        private void BindAdsGrid()
        {
            string categoryId = Request.QueryString["CategoryId"];
            string connectionString = WebConfigurationManager.ConnectionStrings["AdsDB"].ConnectionString;
            string getAdsQuery = "SELECT * FROM [Ads] WHERE CategoryId = @CategoryId";

            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                using(SqlCommand command = new SqlCommand(getAdsQuery, conn))
                {
                    command.Parameters.AddWithValue("@CategoryId", categoryId);

                    try
                    {
                        conn.Open();
                        SqlDataAdapter da = new SqlDataAdapter(command);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        AdsGrid.DataSource = dt;
                        AdsGrid.DataBind();
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

        protected void AdsGrid_RowEditing(object sender, GridViewEditEventArgs e)
        {
            AdsGrid.EditIndex = e.NewEditIndex;
            BindAdsGrid();
        }

        protected string CombinePath(object folder, object file)
        {
            string folderPath = folder?.ToString() ?? String.Empty;
            string filePath = file?.ToString() ?? String.Empty;
            string filename = Path.GetFileName(filePath);
            return Path.Combine(folderPath, filename);
        }

        protected void AdsGrid_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            AdsGrid.EditIndex = -1;
            BindAdsGrid();
        }

        protected void AdsGrid_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string adId = AdsGrid.DataKeys[e.RowIndex].Value.ToString();
            DeleteAds(adId);
            BindAdsGrid();
        }

        protected void AdsGrid_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string adId = AdsGrid.DataKeys[e.RowIndex].Value.ToString();
            GridViewRow row = AdsGrid.Rows[e.RowIndex];
            string adName = ((TextBox)row.Cells[1].Controls[0]).Text;
            string adUrl = ((TextBox)row.Cells[2].Controls[0]).Text;
            UpdateAds(adId, adName, adUrl);
            AdsGrid.EditIndex = -1;
            BindAdsGrid();
        }

        private void DeleteAds(string adId)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["AdsDB"].ConnectionString;
            string deleteQuery = "DELETE FROM [Ads] WHERE AdId = @AdId";

            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                using(SqlCommand command = new SqlCommand(deleteQuery, conn))
                {
                    command.Parameters.AddWithValue("@AdId", adId);

                    try
                    {
                        conn.Open();
                        int result = command.ExecuteNonQuery();
                        if (result < 0)
                        {
                            Response.Write("<script>alert('刪除廣告失敗')</script>");
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

        private void UpdateAds(string adId, string adName, string adUrl)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["AdsDB"].ConnectionString;
            string updateAds = "UPDATE [Ads] " +
                "SET AdName = @AdName, AdUrl = @AdUrl " +
                "WHERE AdId = @AdId";

            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                using(SqlCommand command = new SqlCommand(updateAds, conn))
                {
                    command.Parameters.AddWithValue("@AdName", adName);
                    command.Parameters.AddWithValue("@AdUrl", adUrl);
                    command.Parameters.AddWithValue("@AdId", adId);

                    try
                    {
                        conn.Open();
                        int result = command.ExecuteNonQuery();
                        if (result < 0)
                        {
                            Response.Write("<script>alert('更新廣告失敗')</script>");
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

        protected void GoBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("Ads_Mgmt.aspx");
        }
    }
}