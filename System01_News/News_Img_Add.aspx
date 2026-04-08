<%@ Page Title="新增圖片" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="News_Img_Add.aspx.cs" Inherits="_260311_hw_7Systems.System01_News.News_Img_Add" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <h3>新增圖片</h3>
        <br />
        <div>
            <b>圖片名稱: &emsp;</b>
            <asp:TextBox ID="ImgName" runat="server" Width="100%"></asp:TextBox>
            <br />
            <b>圖片描述: &emsp;</b>
            <asp:TextBox ID="ImgDesc" runat="server" Width="100%"></asp:TextBox>
            <br />
            <b>選擇圖片: &emsp;</b>
            <asp:FileUpload ID="ImgFileUpload" runat="server" />
        </div>
        <br />
        <div>
            <asp:Button ID="Submit" runat="server" Text="送出" OnClick="Submit_Click" />
        </div>
        <br />
    </main>
</asp:Content>