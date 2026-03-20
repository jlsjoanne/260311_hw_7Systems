<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="News.aspx.cs" Inherits="_260311_hw_7Systems.System01_News.News" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <h3>標題</h3>
        <h4><asp:Label ID="NewsTitle" runat="server"></asp:Label></h4>
        <br />
        <b>分類</b>
        <asp:Label ID="NewsCategory" runat="server"></asp:Label>
        <br />
        <b>發表時間</b>
        <asp:Label ID="PostedTime" runat="server"></asp:Label>
        <br /><br />
        <asp:Literal ID="NewsContent" runat="server"></asp:Literal>
    </main>
</asp:Content>

