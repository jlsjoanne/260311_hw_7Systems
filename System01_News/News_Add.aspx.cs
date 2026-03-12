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

        static int linkIdx = 0;
        protected void AddLink_Click(object sender, EventArgs e)
        {
            linkIdx++;
            for(int j=0; j < linkIdx; j++)
            {
                Label Lname = new Label();
                Label LUrl = new Label();
                Lname.Text = "連結名稱";
                LUrl.Text = "Url";
                Lname.ID = $"LinkName{j}";
                LUrl.ID = $"Url{j}";
                CheckBox IsNewWindow = new CheckBox();
                IsNewWindow.Text = "是否另開視窗";
                IsNewWindow.ID = $"IsNew{j}";
                TextBox LnameInput = new TextBox();
                TextBox LUrlInput = new TextBox();
                LnameInput.ID = $"LNameInput{j}";
                LUrlInput.ID = $"LUrlInput{j}";
                LnameInput.Columns = 100;
                LUrlInput.Columns = 100;


                PrLink.Controls.Add(Lname);
                PrLink.Controls.Add(LnameInput);
                PrLink.Controls.Add(new LiteralControl("<br />"));
                PrLink.Controls.Add(LUrl);
                PrLink.Controls.Add(LUrlInput);
                PrLink.Controls.Add(new LiteralControl("<br />"));
                PrLink.Controls.Add(IsNewWindow);
                PrLink.Controls.Add(new LiteralControl("<br /><br />"));
            }
        }

        
    }
}