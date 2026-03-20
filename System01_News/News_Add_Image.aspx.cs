using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Configuration;
using System.Data.SqlClient;
using System.IO;

namespace _260311_hw_7Systems.System01_News
{
    public partial class News_Add_Image : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["NewsID"] != null)
            {
                GetImageData();
            }
            else if(Request.UrlReferrer != null)
            {
                Response.Redirect(Request.UrlReferrer.ToString());
            }
            else
            {
                Response.Redirect("News_List.aspx");
            }
            
        }

        static List<string> ImgIDList = new List<string>();

        private void GetImageData()
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["NewsDB"].ConnectionString;
            string getImageDataQuery = "SELECT * FROM Images WHERE NewsID = @NewsID";
            string newsID = Request.QueryString["NewsID"].ToString();
            

            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                using(SqlCommand command = new SqlCommand(getImageDataQuery, conn))
                {
                    command.Parameters.AddWithValue("NewsID", newsID);
                    SqlDataReader dr = null;

                    try
                    {
                        conn.Open();
                        dr = command.ExecuteReader();
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {

                                string imageID = dr["ImageId"].ToString();

                                if (!ImgIDList.Contains(imageID))
                                {
                                    ImgIDList.Add(imageID);
                                }

                                Label IName = new Label();
                                Label IDesc = new Label();
                                Label IFname = new Label();
                                TextBox INameInput = new TextBox();
                                TextBox IDescInput = new TextBox();

                                IName.ID = $"IName_{imageID}";
                                IDesc.ID = $"IDesc_{imageID}";
                                IFname.ID = $"IFname_{imageID}";
                                INameInput.ID = $"INameInput_{imageID}";
                                IDescInput.ID = $"IDescInput_{imageID}";
                                
                                IName.Text = "圖片名稱";
                                IDesc.Text = "圖片描述";
                                IFname.Text = "檔案名稱: " + Path.GetFileName(dr["IPath"].ToString());

                                PhImage.Controls.Add(IName);
                                PhImage.Controls.Add(INameInput);
                                PhImage.Controls.Add(new LiteralControl("<br />"));
                                PhImage.Controls.Add(IDesc);
                                PhImage.Controls.Add(IDescInput);
                                PhImage.Controls.Add(new LiteralControl("<br />"));
                                PhImage.Controls.Add(IFname);
                                PhImage.Controls.Add(new LiteralControl("<br /><br />"));
                            }
                        }
                    }
                    catch(Exception ex)
                    {
                        Response.Write($"<script>alert('{ex.Message}')</sctip>");
                    }
                    finally
                    {
                        dr.Close();
                        conn.Close();
                    }
                }
            }
        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            string newsID = Request.QueryString["NewsID"].ToString();
            string connectionString = WebConfigurationManager.ConnectionStrings["NewsDB"].ConnectionString;
            string updateImgInfo = "UPDATE Images SET IName = @IName, IDesc = @IDesc WHERE ImageId = @ImageId";

            foreach(string imgID in ImgIDList)
            {
                TextBox INameIn = (TextBox)PhImage.FindControl($"INameInput_{imgID}");
                TextBox IDescIn = (TextBox)PhImage.FindControl($"IDescInput_{imgID}");

                using(SqlConnection conn = new SqlConnection(connectionString))
                {
                    using(SqlCommand command = new SqlCommand(updateImgInfo, conn))
                    {
                        command.Parameters.AddWithValue("@IName", INameIn.Text);
                        command.Parameters.AddWithValue("@IDesc", IDescIn.Text);
                        command.Parameters.AddWithValue("@ImageId", imgID);

                        try
                        {
                            conn.Open();
                            int result = command.ExecuteNonQuery();
                            if(result < 0)
                            {
                                Response.Write("<script>alert('新增圖片資訊失敗')</script>");
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
            }

            Response.Redirect($"News_Add_File.aspx?NewsID={newsID}");
        }
    }
}