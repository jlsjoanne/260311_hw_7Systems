using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Web.Configuration;
using System.Data.SqlClient;

namespace _260311_hw_7Systems.System07_ProductMgmt
{
    public partial class HomePage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["LogInStatus"] != null && Session["LogInStatus"].ToString() == "true")
            {
                AddNew.Visible = true;
                if (ProductGrid.Columns[0] is CommandField commandField)
                {
                    commandField.ShowDeleteButton = true;
                }
            }
            if(Session["RoleId"]!= null && (Session["RoleId"].ToString() == "1" || Session["RoleId"].ToString() == "2"))
            {
                CategoryMgmt.Visible = true;
                BrandMgmt.Visible = true;
            }

            if (!IsPostBack)
            {
                GetMainCategory();
                BindGridData();
            }
        }

        protected void AddNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("ProductMgmt.aspx");
        }

        protected void CategoryMgmt_Click(object sender, EventArgs e)
        {
            Response.Redirect("CategoryMgmt.aspx");
        }

        protected void BrandMgmt_Click(object sender, EventArgs e)
        {
            Response.Redirect("BrandMgmt.aspx");
        }

        private void GetMainCategory()
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["ProductDB"].ConnectionString;
            string getMainQuery = "SELECT * FROM [MainCategory]";

            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                using(SqlCommand command = new SqlCommand(getMainQuery, conn))
                {
                    try
                    {
                        conn.Open();
                        MainDropDown.DataSource = command.ExecuteReader();
                        MainDropDown.DataTextField = "MainCategoryName";
                        MainDropDown.DataValueField = "MainCId";
                        MainDropDown.DataBind();
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
            MainDropDown.Items.Insert(0, new ListItem("--選擇主分類--", "0"));
        }
        protected void MainDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedMain = MainDropDown.SelectedValue;
            GetSubCategory(selectedMain);
            BindGridData(selectedMain);
        }

        private void GetSubCategory(string selectedMain)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["ProductDB"].ConnectionString;
            string getsubQuery = "SELECT * FROM [SubCategory] WHERE MainCId = @MainCId";

            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                using(SqlCommand command = new SqlCommand(getsubQuery, conn))
                {
                    command.Parameters.AddWithValue("@MainCId", selectedMain);

                    try
                    {
                        conn.Open();
                        SubDropDown.DataSource = command.ExecuteReader();
                        SubDropDown.DataTextField = "SubCategoryName";
                        SubDropDown.DataValueField = "SubCId";
                        SubDropDown.DataBind();
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

            SubDropDown.Items.Insert(0, new ListItem("--選擇子分類--", "0"));
        }

        protected void SubDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedSub = SubDropDown.SelectedValue;
            BindGridData(MainDropDown.SelectedValue, selectedSub);
        }

        protected void ProductGrid_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string productId = ProductGrid.DataKeys[e.RowIndex].Value.ToString();

            DeleteFromId(productId);

            BindGridData(MainDropDown.SelectedValue, SubDropDown.SelectedValue);
        }

        private void DeleteFromId(string productId)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["ProductDB"].ConnectionString;
            string deleteProductQuery = "DELETE FROM [Products] WHERE [ProductId] = @ProductId";

            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                using(SqlCommand command = new SqlCommand(deleteProductQuery, conn))
                {
                    command.Parameters.AddWithValue("@ProductId", productId);

                    try
                    {
                        conn.Open();
                        int result = command.ExecuteNonQuery();
                        if(result < 0)
                        {
                            Response.Write("<script>alert('刪除產品失敗')</script>");
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

        protected void ProductGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            ProductGrid.PageIndex = e.NewPageIndex;
            BindGridData(MainDropDown.SelectedValue, SubDropDown.SelectedValue);
        }

        private void BindGridData(string mainCat="", string subCat = "")
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["ProductDB"].ConnectionString;
            //select all
            if ((mainCat=="" || mainCat=="0") && (subCat == "" || subCat == "0"))
            {
                string getAllQuery = "SELECT P.ProductId, P.ProductName, P.UnitPrice, B.BrandName, M.MainCategoryName, Sub.SubCategoryName " +
                    "FROM [Products] AS P " +
                    "LEFT JOIN [Brand] AS B ON P.BrandId = B.BrandId "+
                    "LEFT JOIN [SubCategory] AS Sub ON P.SubCId = Sub.SubCId " +
                    "LEFT JOIN [MainCategory] AS M ON Sub.MainCId = M.MainCId";

                using(SqlConnection conn = new SqlConnection(connectionString))
                {
                    using(SqlCommand command = new SqlCommand(getAllQuery, conn))
                    {
                        BindFromSql(conn, command);
                    }
                }
            }
            //select based on main category
            else if((mainCat !="" && mainCat != "0") && (subCat == "" || subCat == "0"))
            {
                string getMainProductQuery = "SELECT P.ProductId, P.ProductName, P.UnitPrice, B.BrandName, M.MainCategoryName, Sub.SubCategoryName " +
                    "FROM [Products] AS P " +
                    "LEFT JOIN [Brand] AS B ON P.BrandId = B.BrandId " +
                    "LEFT JOIN [SubCategory] AS Sub ON P.SubCId = Sub.SubCId " +
                    "LEFT JOIN [MainCategory] AS M ON Sub.MainCId = M.MainCId " +
                    "WHERE P.SubCId IN (SELECT SubCId FROM [SubCategory] WHERE MainCId = @MainCId)";

                using(SqlConnection conn = new SqlConnection(connectionString))
                {
                    using(SqlCommand command = new SqlCommand(getMainProductQuery, conn))
                    {
                        command.Parameters.AddWithValue("@MainCId", mainCat);

                        BindFromSql(conn, command);
                    }
                }
            }
            //select based on main and sub category
            else
            {
                string getSubProduct = "SELECT P.ProductId, P.ProductName, P.UnitPrice, B.BrandName, M.MainCategoryName, Sub.SubCategoryName " +
                    "FROM [Products] AS P " +
                    "LEFT JOIN [Brand] AS B ON P.BrandId = B.BrandId " +
                    "LEFT JOIN [SubCategory] AS Sub ON P.SubCId = Sub.SubCId " +
                    "LEFT JOIN [MainCategory] AS M ON Sub.MainCId = M.MainCId " +
                    "WHERE P.SubCId IN (SELECT SubCId FROM [SubCategory] WHERE SubCId = @SubCId)";

                using(SqlConnection conn = new SqlConnection(connectionString))
                {
                    using(SqlCommand command = new SqlCommand(getSubProduct, conn))
                    {
                        command.Parameters.AddWithValue("@SubCId", subCat);

                        BindFromSql(conn, command);
                    }
                }
            }
        }

        private void BindFromSql(SqlConnection conn, SqlCommand command)
        {
            try
            {
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                da.Fill(dt);

                ProductGrid.DataSource = dt;
                ProductGrid.DataBind();
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