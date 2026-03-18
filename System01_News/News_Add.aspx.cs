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
            if (IsPostBack)
            {
                CreateLink(LinkCount);
            }
        }

        private int LinkCount
        {
            get
            {
                if (ViewState["LinkCount"] == null) { return 0; }
                return (int)ViewState["LinkCount"];
            }
            set
            {
                ViewState["LinkCount"] = value;
            }
        }

        protected void AddLink_Click(object sender, EventArgs e)
        {
            LinkCount++;
            CreateLink(LinkCount);
        }

        private void CreateLink(int count)
        {
            PhLink.Controls.Clear();

            for(int i = 0; i < count; i++)
            {
                Label LTitle = new Label();
                Label LUrl = new Label();
                TextBox TitleInput = new TextBox();
                TextBox UrlInput = new TextBox();
                CheckBox IsNew = new CheckBox();

                LTitle.ID = $"LTitle{i}";
                LUrl.ID = $"LUrl{i}";
                TitleInput.ID = $"TitleInput{i}";
                UrlInput.ID = $"UrlInput{i}";
                IsNew.ID = $"IsNew{i}";

                LTitle.Text = "連結名稱";
                LUrl.Text = "Url";
                IsNew.Text = "是否開新視窗";

                PhLink.Controls.Add(LTitle);
                PhLink.Controls.Add(TitleInput);
                PhLink.Controls.Add(new LiteralControl("<br />"));
                PhLink.Controls.Add(LUrl);
                PhLink.Controls.Add(UrlInput);
                PhLink.Controls.Add(new LiteralControl("<br />"));
                PhLink.Controls.Add(IsNew);
                PhLink.Controls.Add(new LiteralControl("<br /><br />"));
            }
        }
    }
}