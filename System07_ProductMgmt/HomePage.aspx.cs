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
            }
            if(Session["RoleId"]!= null && (Session["RoleId"].ToString() == "1" || Session["RoleId"].ToString() == "2"))
            {
                CategoryMgmt.Visible = true;
                BrandMgmt.Visible = true;
            }

            if (!IsPostBack)
            {
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

        private void BindGridData(string mainCat="", string subCat = "")
        {

        }
    }
}