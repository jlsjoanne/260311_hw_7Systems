using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace _260311_hw_7Systems.System01_News
{
    public partial class News_Add : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }



        //static int linkIdx = 0;
        //protected void AddLink_Click(object sender, EventArgs e)
        //{
        //    linkIdx++;
        //    for(int j=0; j < linkIdx; j++)
        //    {
        //        Label Lname = new Label();
        //        Label LUrl = new Label();
        //        Lname.Text = "連結名稱";
        //        LUrl.Text = "Url";
        //        Lname.ID = $"LinkName{j}";
        //        LUrl.ID = $"Url{j}";
        //        CheckBox IsNewWindow = new CheckBox();
        //        IsNewWindow.Text = "是否另開視窗";
        //        IsNewWindow.ID = $"IsNew{j}";
        //        TextBox LnameInput = new TextBox();
        //        TextBox LUrlInput = new TextBox();
        //        LnameInput.ID = $"LNameInput{j}";
        //        LUrlInput.ID = $"LUrlInput{j}";
        //        LnameInput.Columns = 100;
        //        LUrlInput.Columns = 100;


        //        PrLink.Controls.Add(Lname);
        //        PrLink.Controls.Add(LnameInput);
        //        PrLink.Controls.Add(new LiteralControl("<br />"));
        //        PrLink.Controls.Add(LUrl);
        //        PrLink.Controls.Add(LUrlInput);
        //        PrLink.Controls.Add(new LiteralControl("<br />"));
        //        PrLink.Controls.Add(IsNewWindow);
        //        PrLink.Controls.Add(new LiteralControl("<br /><br />"));
        //    }
        //}

        //List<newsFile> imgFiles = new List<newsFile>();
        //static int imgIndex = 0;
        //protected void AddImg_Click(object sender, EventArgs e)
        //{
        //    imgIndex++;
        //    for(int j = 0; j < imgIndex; j++)
        //    {
        //        Label imgName = new Label();
        //        Label imgDesc = new Label();
        //        TextBox imgNameInput = new TextBox();
        //        TextBox imgDescInput = new TextBox();
        //        FileUpload imgFile = new FileUpload();
        //        imgName.Text = "圖片名稱: ";
        //        imgDesc.Text = "圖片說明: ";
        //        imgName.ID = $"imgName{j}";
        //        imgDesc.ID = $"imgDesc{j}";
        //        imgNameInput.ID = $"imgNameInput{j}";
        //        imgDescInput.ID = $"imgDescInput{j}";
        //        imgFile.ID = $"imgFile{j}";
        //        imgFile.Attributes.Add("accept",".jpg,.jpeg,.png,.gif");

        //        PhImg.Controls.Add(imgName);
        //        PhImg.Controls.Add(imgNameInput);
        //        PhImg.Controls.Add(new LiteralControl("<br />"));
        //        PhImg.Controls.Add(imgDesc);
        //        PhImg.Controls.Add(imgDescInput);
        //        PhImg.Controls.Add(new LiteralControl("<br />"));
        //        PhImg.Controls.Add(imgFile);
        //        PhImg.Controls.Add(new LiteralControl("<br /><br />"));

        //    }
        //    ImgUpload.Visible = true;
        //}

        //protected void ImgUpload_Click(object sender, EventArgs e)
        //{
        //    IsImgSuccess.Text += PhImg.Controls.Count.ToString();
        //    string uploadFolderPath = Directory.GetParent(Server.MapPath("~/Images/")).FullName;
        //    if (!Directory.Exists(uploadFolderPath))
        //    {
        //        Directory.CreateDirectory(uploadFolderPath);
        //    }

            
        //    for (int j = 0; j < imgIndex; j++)
        //    {
        //        newsFile imgInfo = new newsFile();
        //        imgFiles.Add(imgInfo);
        //    }

            
        //    int idx = 0;

            

        //    foreach (Control ctrl in PhImg.Controls)
        //    {
                
        //        if (ctrl.GetType() == typeof(TextBox))
        //        {
        //            TextBox tb = (TextBox)ctrl;
        //            if (tb.ID.ToString().Contains("imgNameInput"))
        //            {
        //                imgFiles[idx].fileName = tb.Text;
        //                IsImgSuccess.Text += tb.Text;
        //            }
        //            else if (tb.ID.ToString().Contains("imgDescInput"))
        //            {
        //                imgFiles[idx].fileDesc = tb.Text;
        //                IsImgSuccess.Text += tb.Text;
        //            }
        //        }
        //        else if (ctrl.GetType() == typeof(FileUpload))
        //        {
        //            FileUpload img = (FileUpload)ctrl;
        //            if (img.HasFile)
        //            {
        //                string filename = Path.GetFileName(img.PostedFile.FileName);
        //                string filePath = Path.Combine(uploadFolderPath, filename);
        //                img.PostedFile.SaveAs(filePath);
        //                IsImgSuccess.Text += "上傳成功!";
        //                imgFiles[idx].filePath = filePath;
        //                idx++;
        //            }
        //            else
        //            {
        //                IsImgSuccess.Text += "沒有檔案!";
        //            }
        //        }
        //    }

        //}
            
            
        
    }
}