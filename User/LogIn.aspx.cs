using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Data.SqlClient;

namespace _260311_hw_7Systems.User
{
    public partial class LogIn : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["UserDB"].ConnectionString;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = connectionString;

            SqlDataReader dr = null;
            string userLoginCheck = "SELECT U.UserName, U.RoleId, R.RoleName FROM UserList AS U LEFT JOIN Role AS R ON U.RoleId = R.RoleId WHERE U.UserName = @uname AND U.PassWord = @pwd";

            SqlCommand command = new SqlCommand(userLoginCheck, conn);
            command.Parameters.AddWithValue("@uname", UsernameInput.Text);
            command.Parameters.AddWithValue("@pwd", PassWordInput.Text);

            try
            {
                conn.Open();
                dr = command.ExecuteReader();

                if (dr.HasRows)
                {
                    dr.Read();
                    Session["UserName"] = dr["UserName"].ToString();
                    Session["RoleId"] = dr["RoleId"].ToString();
                    Session["RoleName"] = dr["RoleName"].ToString();
                    Session["LogInStatus"] = "true";
                    command.Cancel();
                    dr.Close();
                    conn.Close();
                    Response.Redirect("../Default.aspx");
                }
                else
                {
                    Response.Write("<script>alert('帳號/密碼有誤')</script>");
                    command.Cancel();
                    dr.Close();
                    conn.Close();
                    return;
                }
            }
            catch (Exception ex)
            {
                Response.Write($"<script>alert('Error Message: {ex.Message}')</script>");
            }
        }

        protected void SignUp_Click(object sender, EventArgs e)
        {
            Response.Redirect("SignUp.aspx");
        }
    }
}