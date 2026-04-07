<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="News.aspx.cs" Inherits="_260311_hw_7Systems.System01_News.News" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <div>

        </div>
        <h3>標題</h3>
        <h4><asp:Label ID="NewsTitle" runat="server"></asp:Label></h4>
        <br />
        <b>分類</b>
        <asp:Label ID="NewsCategory" runat="server"></asp:Label>
        <br />
        <b>發表時間</b>
        <asp:Label ID="PostedTime" runat="server"></asp:Label>
        <br /><br />
        <b>內文</b>
        <div style ="border: 1px solid black; padding: 5px">
            <asp:Literal ID="NewsContent" runat="server"></asp:Literal>
        </div>
        <br />
        <div>
            <p>圖片</p>
            <asp:PlaceHolder ID="PhImg" runat="server"></asp:PlaceHolder>
        </div>
        <br />
        <div>
            <p>檔案</p>
            <asp:PlaceHolder ID="PhFile" runat="server"></asp:PlaceHolder>
        </div>
        <br />
        <div>
            <p>連結</p>
            <asp:PlaceHolder ID="PhLink" runat="server"></asp:PlaceHolder>
        </div>
    </main>
</asp:Content>

