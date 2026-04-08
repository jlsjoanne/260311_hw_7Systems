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

namespace _260311_hw_7Systems.System02_PhotoAlbums
{
    public partial class Album_Add : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["PhotoAlbumDB"].ConnectionString;
            string checkRepeatQuery = "SELECT * FROM Album WHERE AlbumName = @AlbumName";
            string insertAlbumQuery = "INSERT INTO Album (AlbumName, AlbumDesc) VALUES (@AlbumName, @AlbumDesc)";
            string getIDQuery = "SELECT AlbumId FROM Album WHERE AlbumName = @AlbumName";
            string albumId = "";

            if(AlbumName.Text.Trim() == "")
            {
                Response.Write("<script>alert('相簿名稱不得為空')</script>");
                return;
            }



            //check repeat
            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                using(SqlCommand command = new SqlCommand(checkRepeatQuery, conn))
                {
                    command.Parameters.AddWithValue("@AlbumName", AlbumName.Text.Trim());
                    SqlDataReader dr = null;

                    try
                    {
                        conn.Open();
                        dr = command.ExecuteReader();
                        if (dr.HasRows)
                        {
                            dr.Close();
                            conn.Close();
                            Response.Write("<script>alert('相簿名已存在，不能重複')</script>");
                            return;
                        }
                    }
                    catch(Exception ex)
                    {
                        Response.Write($"<script>alert('{ex.Message}')</script>");
                    }
                    finally
                    {
                        dr.Close();
                        conn.Close();
                    }

                }
            }

            // insert album
            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                using(SqlCommand command = new SqlCommand(insertAlbumQuery, conn))
                {
                    command.Parameters.AddWithValue("@AlbumName", AlbumName.Text.Trim());
                    command.Parameters.AddWithValue("@AlbumDesc", AlbumDesc.Text.Trim());

                    try
                    {
                        conn.Open();
                        int result = command.ExecuteNonQuery();
                        if(result < 0)
                        {
                            Response.Write("<script>alert('建立相簿失敗')</script>");
                        }
                    }
                    catch(Exception ex)
                    {
                        Response.Write($"<script>alert('{ex.Message}')</script>");
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }

            // get AlbumId
            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(getIDQuery,conn))
                {
                    command.Parameters.AddWithValue("@AlbumName", AlbumName.Text.Trim());
                    SqlDataReader dr = null;

                    try
                    {
                        conn.Open();
                        dr = command.ExecuteReader();

                        if (dr.HasRows)
                        {
                            dr.Read();
                            albumId = dr["AlbumId"].ToString();
                        }
                        else
                        {
                            dr.Close();
                            conn.Close();
                            Response.Write("<script>alert('查無相簿ID')</script>");
                            return;
                        }
                    }
                    catch(Exception ex)
                    {
                        Response.Write($"<script>alert('{ex.Message}')</script>");
                    }
                    finally
                    {
                        dr.Close();
                        conn.Close();
                    }
                }
            }

            Response.Redirect($"Photo_Add.aspx?AlbumId={albumId}");
        }
    }
}