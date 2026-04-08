<%@ Page Title="新增連結" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="News_Link_Add.aspx.cs" Inherits="_260311_hw_7Systems.System01_News.News_Link_Add" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <h3>新增連結</h3>
        <div>
            <b>連結名稱: &emsp;</b>
            <asp:TextBox ID="LinkName" runat="server" Width="100%"></asp:TextBox>
            <br />
            <b>連結位址: &emsp;</b>
            <asp:TextBox ID="LinkUrl" runat="server" Width="100%"></asp:TextBox>
            <br />
            <asp:CheckBox ID="IsNewPage" runat="server" Text="是否開新分頁" />
        </div>
        <br />
        <div>
            <asp:Button ID="Submit" runat="server" Text="送出" OnClick="Submit_Click" />
        </div>
        
    </main>
</asp:Content>