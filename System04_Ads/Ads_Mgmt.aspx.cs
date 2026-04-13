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
    public partial class Ads_Mgmt : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["RoleId"] != null && (Session["RoleId"].ToString() == "1" || Session["RoleId"].ToString() == "2"))
            {
                if (!IsPostBack)
                {
                    BindAdsGrid();
                }
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

        private void BindAdsGrid()
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["AdsDB"].ConnectionString;
            string getCategoryQuery = "SELECT * FROM [Category]";

            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                using(SqlCommand command = new SqlCommand(getCategoryQuery, conn))
                {
                    try
                    {
                        conn.Open();
                        CategoryGrid.DataSource = command.ExecuteReader();
                        CategoryGrid.DataBind();
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

        protected void CategoryGrid_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string categoryId = CategoryGrid.DataKeys[e.RowIndex].Value.ToString();
            DeleteCategory(categoryId);
            e.Cancel = true;
            BindAdsGrid();
        }

        protected void CategoryGrid_RowEditing(object sender, GridViewEditEventArgs e)
        {
            CategoryGrid.EditIndex = e.NewEditIndex;
            BindAdsGrid();
        }

        protected void CategoryGrid_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string categoryId = CategoryGrid.DataKeys[e.RowIndex].Value.ToString();
            GridViewRow row = CategoryGrid.Rows[e.RowIndex];
            string cName = ((TextBox)row.Cells[1].Controls[0]).Text;
            string cOrder = ((TextBox)row.Cells[2].Controls[0]).Text;
            bool isPublished = ((CheckBox)row.Cells[3].Controls[0]).Checked;
            
            if(cName == "")
            {
                Response.Write("<script>alert('類別名稱不得為空');</script>");
                return;
            }
            
            if (cOrder == "")
            {
                UpdateCategory(categoryId, cName, null, isPublished);
                CategoryGrid.EditIndex = -1;
                BindAdsGrid();
                return;
            }

            if (!int.TryParse(cOrder, out int orderNum))
            {
                Response.Write("<script>alert('類別順序需為數字');</script>");
                return;
            }

            UpdateCategory(categoryId,cName,orderNum, isPublished);
            CategoryGrid.EditIndex = -1;
            BindAdsGrid();
        }

        protected void CategoryGrid_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            CategoryGrid.EditIndex = -1;
            BindAdsGrid();
        }

        private void DeleteCategory(string categoryId)
        {
            
            
            string connectionString = WebConfigurationManager.ConnectionStrings["AdsDB"].ConnectionString;
            string deleteQuery = "DELETE FROM [Category] WHERE CategoryId = @CategoryId";

            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                using(SqlCommand command = new SqlCommand(deleteQuery, conn))
                {
                    command.Parameters.AddWithValue("@CategoryId", categoryId);

                    try
                    {
                        conn.Open();
                        int result = command.ExecuteNonQuery();
                        if(result < 0)
                        {
                            Response.Write("<script>alert('刪除分類失敗')</script>");
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

        private void UpdateCategory(string cId, string cName, int? cOrder, bool isPublished)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["AdsDB"].ConnectionString;
            string updateQuery = "UPDATE [Category] " +
                "SET CategoryName = @CategoryName, CategoryOrder = @CategoryOrder, IsPublished = @IsPublished "+
                "WHERE CategoryId = @CategoryId";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using(SqlCommand command = new SqlCommand(updateQuery, conn))
                {
                    command.Parameters.AddWithValue("@CategoryName", cName);
                    command.Parameters.AddWithValue("@CategoryOrder", cOrder);
                    command.Parameters.AddWithValue("@CategoryId", cId);
                    command.Parameters.AddWithValue("@IsPublished", isPublished);

                    try
                    {
                        conn.Open();
                        int result = command.ExecuteNonQuery();
                        if(result < 0)
                        {
                            Response.Write("<script>alert('更新類別失敗')</script>");
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

        protected void AddCategory_Click(object sender, EventArgs e)
        {
            Response.Redirect("Ads_Category_Add.aspx");
        }

        protected void GoBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdsList.aspx");
        }
    }
}