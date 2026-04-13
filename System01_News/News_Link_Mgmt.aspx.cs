using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace _260311_hw_7Systems.System01_News
{
    public partial class News_Link_Mgmt : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["NewsId"] != null)
            {
                if (Session["RoleId"] != null && (Session["RoleId"].ToString() == "1" || Session["RoleId"].ToString() == "2"))
                {
                    AddLink.Visible = true;
                    LinkGrid.Visible = true;
                }
                if (!IsPostBack)
                {
                    BindLinkGrid();
                }
            }
            else if (Request.UrlReferrer != null)
            {
                Response.Redirect(Request.UrlReferrer.ToString());
            }
            else
            {
                Response.Redirect("News_List.aspx");
            }
        }

        protected void AddLink_Click(object sender, EventArgs e)
        {
            string newsId = Request.QueryString["NewsId"];
            Response.Redirect($"News_Link_Add.aspx?NewsId={newsId}");
        }

        protected void GoBack_Click(object sender, EventArgs e)
        {
            string newsId = Request.QueryString["NewsId"];
            Response.Redirect($"News.aspx?NewsId={newsId}");
        }

        private void BindLinkGrid()
        {
            string newsId = Request.QueryString["NewsId"];
            string connectionString = WebConfigurationManager.ConnectionStrings["NewsDB"].ConnectionString;
            string getLinkQuery = "SELECT * FROM [Links] WHERE NewsId = @NewsId";

            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                using(SqlCommand command = new SqlCommand(getLinkQuery, conn))
                {
                    command.Parameters.AddWithValue("@NewsId", newsId);

                    try
                    {
                        conn.Open();
                        LinkGrid.DataSource = command.ExecuteReader();
                        LinkGrid.DataBind();
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

        protected void LinkGrid_RowEditing(object sender, GridViewEditEventArgs e)
        {
            LinkGrid.EditIndex = e.NewEditIndex;
            BindLinkGrid();
        }

        protected void LinkGrid_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            LinkGrid.EditIndex = -1;
            BindLinkGrid();
        }

        protected void LinkGrid_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string linkId = LinkGrid.DataKeys[e.RowIndex].Value.ToString();
            GridViewRow row = LinkGrid.Rows[e.RowIndex];
            string lName = ((TextBox)row.Cells[1].Controls[0]).Text;
            string lUrl = ((TextBox)row.Cells[2].Controls[0]).Text;
            bool isNew = ((CheckBox)row.Cells[3].Controls[0]).Checked;

            // Update in DB
            string connectionString = WebConfigurationManager.ConnectionStrings["NewsDB"].ConnectionString;
            string updateLinkQuery = "UPDATE [Links] " +
                "SET LName = @LName, LUrl = @LUrl, IsNewPage = @IsNewPage " +
                "WHERE LinkId = @LinkId";

            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                using(SqlCommand command = new SqlCommand(updateLinkQuery, conn))
                {
                    command.Parameters.AddWithValue("@LName",lName);
                    command.Parameters.AddWithValue("@LUrl", lUrl);
                    command.Parameters.AddWithValue("@IsNewPage",isNew);
                    command.Parameters.AddWithValue("@LinkId", linkId);

                    try
                    {
                        conn.Open();
                        int result = command.ExecuteNonQuery();
                        if(result < 0)
                        {
                            Response.Write("<script>alert('更新連結失敗')</script>");
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

            LinkGrid.EditIndex = -1;
            BindLinkGrid();
        }

        protected void LinkGrid_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string linkId = LinkGrid.DataKeys[e.RowIndex].Value.ToString();
            DeleteLink(linkId);
            e.Cancel = true;
            BindLinkGrid();
        }

        private void DeleteLink(string linkId)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["NewsDB"].ConnectionString;
            string deleteLinkQuery = "DELETE FROM [Links] WHERE LinkId = @LinkId";

            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                using(SqlCommand command = new SqlCommand(deleteLinkQuery, conn))
                {
                    command.Parameters.AddWithValue("@LinkId", linkId);

                    try
                    {
                        conn.Open();
                        int result = command.ExecuteNonQuery();
                        if(result < 0)
                        {
                            Response.Write("<script>alert('刪除連結失敗')</script>");
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