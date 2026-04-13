using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Data.SqlClient;
using System.Data;

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
                UserGrid.Visible = true;
            }

            if (!IsPostBack)
            {
                BindUserData();
            }
            
        }

        protected void UserGrid_RowEditing(object sender, GridViewEditEventArgs e)
        {
            UserGrid.EditIndex = e.NewEditIndex;
            BindUserData();
        }

        protected void UserGrid_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            UserGrid.EditIndex = -1;
            BindUserData();
        }

        protected void UserGrid_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string username = UserGrid.DataKeys[e.RowIndex].Value.ToString();

            GridViewRow row = UserGrid.Rows[e.RowIndex];
            string pwd = ((TextBox)row.FindControl("tbPwd")).Text;
            string roleId = ((TextBox)row.Cells[3].Controls[0]).Text;

            if(pwd == "")
            {
                Response.Write("<script>alert('密碼不得為空')</script>");
                return;
            }

            if(roleId != "1" && roleId != "2" && roleId != "3")
            {
                Response.Write("<script>alert('輸入錯誤，無此權限代碼')</script>");
                return;
            }

            UpdateUserInfo(username, pwd, roleId);
            UserGrid.EditIndex = -1;
            BindUserData();
        }

        protected void UserGrid_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string username = UserGrid.DataKeys[e.RowIndex].Value.ToString();
            DeleteUser(username);
            e.Cancel = true;
            BindUserData();
        }

        private void BindUserData()
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["UserDB"].ConnectionString;
            string getUserQuery = "SELECT UserName, PassWord, U.RoleId, R.RoleName " +
                "FROM [UserList] AS U " +
                "LEFT JOIN [Role] AS R ON U.RoleId = R.RoleId";

            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                using(SqlCommand command = new SqlCommand(getUserQuery, conn))
                {
                    try
                    {
                        conn.Open();
                        SqlDataAdapter da = new SqlDataAdapter(command);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        UserGrid.DataSource = dt;
                        UserGrid.DataBind();
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

        private void UpdateUserInfo(string username, string pwd, string roleId)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["UserDB"].ConnectionString;
            string updateUserQuery = "UPDATE [UserList] " +
                "SET PassWord = @PassWord, RoleId = @RoleId " +
                "WHERE UserName = @UserName";

            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                using(SqlCommand command = new SqlCommand(updateUserQuery, conn))
                {
                    command.Parameters.AddWithValue("@PassWord", pwd);
                    command.Parameters.AddWithValue("@RoleId", roleId);
                    command.Parameters.AddWithValue("@UserName", username);

                    try
                    {
                        conn.Open();
                        int result = command.ExecuteNonQuery();
                        if(result < 0)
                        {
                            Response.Write("<script>alert('帳戶更新失敗')</script>");
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

        private void DeleteUser(string username)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["UserDB"].ConnectionString;
            string deleteUserQuery = "DELETE FROM [UserLis] WHERE UserName = @UserName";

            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                using(SqlCommand command = new SqlCommand(deleteUserQuery, conn))
                {
                    command.Parameters.AddWithValue("@UserName", username);

                    try
                    {
                        conn.Open();
                        int result = command.ExecuteNonQuery();
                        if(result < 0)
                        {
                            Response.Write("<script>alert('帳戶刪除失敗')</script>");
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