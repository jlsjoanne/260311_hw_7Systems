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
    public partial class ProductMgmt : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["LogInStatus"] != null && Session["LogInStatus"].ToString() == "true")
            {
                if (!IsPostBack)
                {
                    if (Request.QueryString["ProductId"] != null)
                    {
                        string productId = Request.QueryString["ProductId"].ToString();
                        GetMainCategory(productId);
                        GetBrand(productId);
                        GetProductDetail(productId);
                    }
                    else
                    {
                        GetMainCategory();
                        GetBrand();
                    }
                }
            }
            else if(Request.UrlReferrer != null)
            {
                Response.Redirect(Request.UrlReferrer.ToString());
            }
            else
            {
                Response.Redirect("HomePage.aspx");
            }
        }

        private void GetProductDetail(string productId)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["ProductDB"].ConnectionString;
            string getProductQuery = "SELECT * FROM [Products] WHERE ProductId = @ProductId";

            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                using(SqlCommand command = new SqlCommand(getProductQuery, conn))
                {
                    command.Parameters.AddWithValue("@ProductId", productId);
                    SqlDataReader dr = null;

                    conn.Open();
                    dr = command.ExecuteReader();
                    if (dr.HasRows)
                    {
                        dr.Read();
                        ProductName.Text = dr["ProductName"].ToString();
                        ProductDetail.Text = dr["ProductDetail"].ToString();
                        UnitPrice.Text = dr["UnitPrice"].ToString();
                    }
                    else
                    {
                        Response.Write("<script>alert('查無此產品');</script>");
                    }
                    dr.Close();
                    conn.Close();
                }
            }
        }
        
        private void GetMainCategory(string productId="")
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["ProductDB"].ConnectionString;
            string getMainQuery = "SELECT * FROM [MainCategory]";

            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(getMainQuery, conn))
                {
                    try
                    {
                        conn.Open();
                        MainDropDown.DataSource = command.ExecuteReader();
                        MainDropDown.DataTextField = "MainCategoryName";
                        MainDropDown.DataValueField = "MainCId";
                        if(productId != "")
                        {
                            string[] productCategory = GetProductCategory(productId);
                            MainDropDown.SelectedValue = productCategory[0];
                            GetSubCategory(productCategory[0], productCategory[1]);
                        }
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

            if(productId == "")
            {
                MainDropDown.Items.Insert(0, new ListItem("--選擇主分類--", "0"));
            }
            
        }

        private string[] GetProductCategory(string productId)
        {
            string[] result = new string[2];

            string connectionString = WebConfigurationManager.ConnectionStrings["ProductDB"].ConnectionString;
            string getCategoryQuery = "SELECT P.SubCId, S.MainCId FROM [Products] AS P LEFT JOIN [SubCategory] AS S ON P.SubCId = S.SubCId WHERE ProductId = @ProductId";

            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                using(SqlCommand command = new SqlCommand(getCategoryQuery, conn))
                {
                    SqlDataReader dr = null;
                    
                    command.Parameters.AddWithValue("@ProductId", productId);

                    conn.Open();
                    dr = command.ExecuteReader();
                    if (dr.HasRows)
                    {
                        dr.Read();
                        result[0] = dr["MainCId"].ToString();
                        result[1] = dr["SubCId"].ToString(); 
                    }

                    dr.Close();
                    conn.Close();

                    return result;
                }
            }
        }
        
        protected void MainDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedMain = MainDropDown.SelectedValue;

            GetSubCategory(selectedMain);
        }

        private void GetSubCategory(string selectedMain, string existingSub = "")
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
                        if(existingSub != "")
                        {
                            SubDropDown.SelectedValue = existingSub;
                        }
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

            if(existingSub == "")
            {
                SubDropDown.Items.Insert(0, new ListItem("--選擇子分類--", "0"));
            }

            
        }

        private void GetBrand(string productId="")
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["ProductDB"].ConnectionString;
            string getBrandQuery = "SELECT * FROM [Brand]";

            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                using(SqlCommand command = new SqlCommand(getBrandQuery, conn))
                {
                    try
                    {
                        conn.Open();
                        BrandDropdown.DataSource = command.ExecuteReader();
                        BrandDropdown.DataTextField = "BrandName";
                        BrandDropdown.DataValueField = "BrandId";
                        if(productId != null)
                        {
                            string brandId = GetProductBrand(productId);
                            BrandDropdown.SelectedValue = brandId;
                        }
                        BrandDropdown.DataBind();
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

            if(productId == "")
            {
                BrandDropdown.Items.Insert(0, new ListItem("--選擇品牌--", "0"));
            }
            
        }

        private string GetProductBrand(string productId)
        {
            string brandId = "";
            string connectionString = WebConfigurationManager.ConnectionStrings["ProductDB"].ConnectionString;
            string getBrandQuery = "SELECT BrandId FROM [Products] WHERE ProductId = @ProductId";

            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                using(SqlCommand command = new SqlCommand(getBrandQuery, conn))
                {
                    SqlDataReader dr = null;
                    command.Parameters.AddWithValue("ProductId", productId);

                    conn.Open();
                    dr = command.ExecuteReader();
                    if (dr.HasRows)
                    {
                        dr.Read();
                        brandId = dr["BrandId"].ToString();
                    }
                    dr.Close();
                    conn.Close();
                    return brandId;
                }
            }
        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            
            if(int.TryParse(UnitPrice.Text,out int price) == false)
            {
                Response.Write("<script>alert('輸入售價非數字');</script>");
                return;
            }
            if(ProductName.Text == "")
            {
                Response.Write("<script>alert('品名不能為空');</script>");
                return;
            }
            if(MainDropDown.SelectedValue == "0")
            {
                Response.Write("<script>alert('請選擇主類別');</script>");
                return;
            }
            if(SubDropDown.SelectedValue == "0")
            {
                Response.Write("<script>alert('請選擇子類別');</script>");
                return;
            }
            if(BrandDropdown.SelectedValue == "0")
            {
                Response.Write("<script>alert('請選擇品牌');</script>");
                return;
            }


            if (Request.QueryString["ProductId"] != null)
            {
                string productId = Request.QueryString["ProductId"].ToString();
                UpdateProductDB(productId);
                Response.Redirect($"Product.aspx?ProductId={productId}");
            }
            else
            {
                InsertProductDB();
                Response.Redirect("HomePage.aspx");
            }
            
        }

        private void UpdateProductDB(string productId)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["ProductDB"].ConnectionString;
            string updateDataQuery = "UPDATE [Products] " +
                "SET ProductName = @ProductName, ProductDetail = @ProductDetail, " +
                "UnitPrice = @UnitPrice, BrandId = @BrandId, SubCId= @SubCId " +
                "WHERE ProductId = @ProductId";

            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                using(SqlCommand command = new SqlCommand(updateDataQuery, conn))
                {
                    command.Parameters.AddWithValue("@ProductName", ProductName.Text);
                    command.Parameters.AddWithValue("@ProductDetail", ProductDetail.Text);
                    command.Parameters.AddWithValue("@UnitPrice", UnitPrice.Text);
                    command.Parameters.AddWithValue("@BrandId", BrandDropdown.SelectedValue);
                    command.Parameters.AddWithValue("@SubCId", SubDropDown.SelectedValue);
                    command.Parameters.AddWithValue("@ProductId", productId);

                    try
                    {
                        conn.Open();
                        int result = command.ExecuteNonQuery();
                        if(result < 0)
                        {
                            Response.Write("<script>alert('更新商品失敗')</script>");
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

        private void InsertProductDB()
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["ProductDB"].ConnectionString;
            string insertDataQuery = "INSERT INTO [Products] (ProductName, ProductDetail, UnitPrice, BrandId, SubCId) " +
                "VALUES (@ProductName, @ProductDetail, @UnitPrice, @BrandId, @SubCId)";

            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                using(SqlCommand command = new SqlCommand(insertDataQuery, conn))
                {
                    command.Parameters.AddWithValue("@ProductName", ProductName.Text);
                    command.Parameters.AddWithValue("@ProductDetail", ProductDetail.Text);
                    command.Parameters.AddWithValue("@UnitPrice", UnitPrice.Text);
                    command.Parameters.AddWithValue("@BrandId", BrandDropdown.SelectedValue);
                    command.Parameters.AddWithValue("@SubCId", SubDropDown.SelectedValue);

                    try
                    {
                        conn.Open();
                        int result = command.ExecuteNonQuery();
                        if(result < 0)
                        {
                            Response.Write("<script>alert('新增商品失敗')</script>");
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