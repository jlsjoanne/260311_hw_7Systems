using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace _260311_hw_7Systems.User
{
    public partial class User_ChangePwd : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["LogInStatus"] != null && Session["LogInStatus"].ToString() == "true")
            {
                string username = Session["UserName"].ToString();

                if(OldPwd.Text != "")
                {
                    CheckOldPwd(username, OldPwd.Text);
                }

                if(checkNewPwd.Text != "")
                {
                    if(newPwd.Text == checkNewPwd.Text)
                    {
                        checkNew.ForeColor = System.Drawing.Color.Green;
                        checkNew.Text = "新密碼輸入一致";
                    }
                    else
                    {
                        checkNew.ForeColor = System.Drawing.Color.Red;
                        checkNew.Text = "新密碼輸入不一致";
                    }
                }
            }
            else if(Request.UrlReferrer != null)
            {
                Response.Redirect(Request.UrlReferrer.ToString());
            }
            else
            {
                Response.Redirect("../Default.aspx");
            }
        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            if (checkPwd.Text == "舊密碼正確" && checkNew.Text == "新密碼輸入一致")
            {
                string username = Session["UserName"].ToString();
                UpdatePwd(username, newPwd.Text);
                Response.Redirect("Setting.aspx");
            }
            else
            {
                if (checkPwd.Text == "舊密碼錯誤")
                {
                    Response.Write("<script>alert('舊密碼錯誤');</script>");
                    return;
                }
                if (checkNew.Text == "新密碼輸入不一致")
                {
                    Response.Write("<script>alert('新密碼輸入不一致');</script>");
                    return;
                }
            }
        }

        private void CheckOldPwd(string username, string pwdInput)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["UserDB"].ConnectionString;
            string checkPwdQuery = "SELECT PassWord FROM [UserList] WHERE UserName = @UserName";

            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                using(SqlCommand command = new SqlCommand(checkPwdQuery, conn))
                {
                    command.Parameters.AddWithValue("@UserName", username);
                    SqlDataReader dr = null;

                    try
                    {
                        conn.Open();
                        dr = command.ExecuteReader();
                        if (dr.HasRows)
                        {
                            dr.Read();
                            string getPwd = dr["PassWord"].ToString();
                            if(pwdInput != getPwd)
                            {
                                checkPwd.ForeColor = System.Drawing.Color.Red;
                                checkPwd.Text = "舊密碼錯誤";
                            }
                            else
                            {
                                checkPwd.ForeColor = System.Drawing.Color.Green;
                                checkPwd.Text = "舊密碼正確";
                            }
                        }
                        else
                        {
                            Response.Write("<script>alert('查無此帳號');</script>");
                        }
                        dr.Close();
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

        

        private void UpdatePwd(string username, string pwd)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["UserDB"].ConnectionString;
            string updatePwdQuery = "UPDATE [UserList] SET PassWord = @PassWord WHERE UserName = @UserName";

            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                using(SqlCommand command = new SqlCommand(updatePwdQuery, conn))
                {
                    command.Parameters.AddWithValue("@PassWord", pwd);
                    command.Parameters.AddWithValue("@UserName", username);

                    try
                    {
                        conn.Open();
                        int result = command.ExecuteNonQuery();
                        if(result < 0)
                        {
                            Response.Write("<script>alert('密碼更新失敗');</script>");
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