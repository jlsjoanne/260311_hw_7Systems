<%@ Page Title="Add News" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="News_Add.aspx.cs" Inherits="_260311_hw_7Systems.System01_News.News_Add" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title">
        <div>
            <h2><b>新增消息</b></h2>
            <h3>標題</h3>
            <p>(字數限制: 50字)</p>
            <asp:TextBox ID="NewTitle" runat="server" Width="100%"></asp:TextBox>
        </div>
        <br />
        <div>
            <h4>分類</h4>
            <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="SqlDataSource2" DataTextField="CategoryName" DataValueField="CategoryId"></asp:DropDownList>
            <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:NewsDB %>" SelectCommand="SELECT * FROM [Category]"></asp:SqlDataSource>
        </div>
        <br />
        <div>
            <h4>內文</h4>
            <p>(字數限制: 1000字)</p>
            <asp:TextBox ID="Content" runat="server"
                TextMode="MultiLine" Rows="10" Width="100%"></asp:TextBox>
        </div>
        <br />
        <div>
            <h4>圖片</h4>
            <p>(可上傳多檔)</p>
            <asp:FileUpload ID="ImagesUpload" runat="server" 
                AllowMultiple="true" accept="image/*"/>
        </div>
        <br />
        <div>
            <h4>檔案</h4>
            <p>(可上傳多檔)</p>
            <asp:FileUpload ID="FilesUpload" runat="server"
                AllowMiltiple="true"/>
        </div>
        <br />
        <div>
            <h4>連結</h4>
            <asp:Button ID="AddLink" runat="server" Text="新增連結" OnClick="AddLink_Click"/>
            <br />
            <asp:PlaceHolder ID="PhLink" runat="server"></asp:PlaceHolder>
        </div>
        <br />
        <div>
            <asp:Button ID="Submit" runat="server" Text="送出" />
        </div>
        
    </main>
</asp:Content>
