using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace _260311_hw_7Systems.System05_Video
{
    public partial class Video_Mgmt : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["RoleId"] != null && (Session["RoleId"].ToString() == "1" || Session["RoleId"].ToString() == "2"))
            {
                if (!IsPostBack)
                {
                    BindGridData();
                }
            }
            else if (Request.UrlReferrer != null)
            {
                Response.Redirect(Request.UrlReferrer.ToString());
            }
            else
            {
                Response.Redirect("VideoList.aspx");
            }
        }

        private void BindGridData()
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["VideoDB"].ConnectionString;
            string getCategoryQuery = "SELECT * FROM [Category] ORDER BY CategoryOrder";

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

                        CategoryGrid.DataSource = dt;
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

        protected void CategoryGrid_RowEditing(object sender, GridViewEditEventArgs e)
        {
            CategoryGrid.EditIndex = e.NewEditIndex;
            BindGridData();
        }

        protected void CategoryGrid_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            CategoryGrid.EditIndex = -1;
            BindGridData();
        }

        protected void CategoryGrid_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string categoryId = CategoryGrid.DataKeys[e.RowIndex].Value.ToString();

            GridViewRow row = CategoryGrid.Rows[e.RowIndex];
            string cName = ((TextBox)row.Cells[1].Controls[0]).Text;
            string cOrder = ((TextBox)row.Cells[2].Controls[0]).Text;
            bool cIsPublished = ((CheckBox)row.Cells[3].Controls[0]).Checked;
            
            if(!int.TryParse(cOrder,out int result))
            {
                Response.Write("<script>alert('分類排序應為數字');</script>");
                return;
            }

            UpdateCategory(categoryId, cName, cOrder, cIsPublished);

            CategoryGrid.EditIndex = -1;
            BindGridData();

        }

        protected void CategoryGrid_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string categoryId = CategoryGrid.DataKeys[e.RowIndex].Value.ToString();
            DeleteCategory(categoryId);
            e.Cancel = true;
            BindGridData();
        }

        private void UpdateCategory(string cId, string cName, string cOrder, bool cIsPublished)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["VideoDB"].ConnectionString;
            string updateQuery = "UPDATE [Category] " +
                "SET CategoryName = @CategoryName, CategoryOrder = @CategoryOrder, IsPublished = @IsPublished " +
                "WHERE CategoryId = @CategoryId";

            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                using(SqlCommand command = new SqlCommand(updateQuery, conn))
                {
                    command.Parameters.AddWithValue("@CategoryName",cName);
                    command.Parameters.AddWithValue("@CategoryOrder",cOrder);
                    command.Parameters.AddWithValue("@IsPublished",cIsPublished);
                    command.Parameters.AddWithValue("@CategoryId", cId);

                    try
                    {
                        conn.Open();
                        int result = command.ExecuteNonQuery();
                        if(result < 0)
                        {
                            Response.Write("<script>alert('更新分類失敗');</script>");
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

        private void DeleteCategory(string cId)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["VideoDB"].ConnectionString;
            string DeleteQuery = "DELETE FROM [Category] WHERE CategoryId = @CategoryId";

            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                using(SqlCommand command = new SqlCommand(DeleteQuery, conn))
                {
                    command.Parameters.AddWithValue("@CategoryId",cId);

                    try
                    {
                        conn.Open();
                        int result = command.ExecuteNonQuery();
                        if(result < 0)
                        {
                            Response.Write("<script>alert('刪除分類失敗');</script>");
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

        protected void AddCategory_Click(object sender, EventArgs e)
        {
            Response.Redirect("Video_Category_Add.aspx");
        }

        protected void GoBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("VideoList.aspx");
        }
    }
}