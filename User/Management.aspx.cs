using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;

namespace _260311_hw_7Systems.User
{
    public partial class Management : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["RoleId"] == null)
            {
                Response.Redirect("../Default.aspx");
            }
            else if(Session["RoleId"] != null && Session["RoleId"].ToString() != "1")
            {
                Response.Write("<script>alert('帳號無權限')</script>");
                Response.Redirect("Setting.aspx");
            }
            else if(Session["RoleId"] != null && Session["RoleId"].ToString() == "1")
            {
                GridView1.Visible = true;
            }

            if (!IsPostBack)
            {
                GridView1.DataSource = BindGridView();
                GridView1.DataBind();
            }
            
        }

        private SqlDataSource BindGridView()
        {
            SqlDataSource sqlDataSource1 = new SqlDataSource();
            sqlDataSource1.ID = "SqlDataSource1";

            string connectionString = WebConfigurationManager.ConnectionStrings["UserDB"].ConnectionString;
            sqlDataSource1.ConnectionString = connectionString;

            string selectCommand = "SELECT * FROM UserList";
            sqlDataSource1.SelectCommand = selectCommand;

            string deleteCommand = "DELECT * FROM UserList WHERE UserName = @UserName";
            sqlDataSource1.DeleteCommand = deleteCommand;

            string updateCommand = "UPDATE UserList SET PassWord = @PassWord, RoleId= @RoleId WHERE UserName = @UserName";
            sqlDataSource1.UpdateCommand = updateCommand;

            return sqlDataSource1;

            
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e) { }
        //gmail草稿





    }
}