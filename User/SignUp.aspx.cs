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
    public partial class SignUp : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["UserDB"].ConnectionString;
            
            if(PwdInput.Text != CPwdInput.Text)
            {
                Response.Write("<script>alert('密碼輸入不一致')</script>");
            }
            else
            {
                // SQL Query
                string checkUname = "SELECT * FROM UserList WHERE UserName = @uname";
                string insertUname = "INSERT INTO UserList (UserName, PassWord, RoleId) Values (@Username, @Pwd, 3)";
                
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = connectionString;

                SqlDataReader dr = null;

                SqlCommand command1 = new SqlCommand(checkUname, conn);
                command1.Parameters.AddWithValue("@uname", UNameInput.Text);

                SqlCommand command2 = new SqlCommand(insertUname, conn);
                command2.Parameters.AddWithValue("@Username", UNameInput.Text);
                command2.Parameters.AddWithValue("@Pwd", PwdInput.Text);

                try
                {
                    conn.Open();
                    dr = command1.ExecuteReader();

                    if (dr.HasRows)
                    {
                        command1.Cancel();
                        command2.Cancel();
                        dr.Close();
                        conn.Close();
                        Response.Write("<script>alert('帳號已存在')</script>");
                        return;
                    }

                    dr.Close();
                    int result = command2.ExecuteNonQuery();

                    command1.Cancel();
                    command2.Cancel();
                    conn.Close();

                    if(result < 0)
                    {
                        Response.Write("<script>alert('註冊失敗')</script>");
                    }
                    else
                    {
                        Response.Write("<script>alert('註冊成功')</scrpipt>");
                        Response.Redirect("LogIn.aspx");
                    }

                }
                catch(Exception ex)
                {
                    Response.Write($"<script>alert('{ex.Message}')</script>");
                }
            }
        }
    }
}