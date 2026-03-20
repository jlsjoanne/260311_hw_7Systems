<%@ Page Title="News_List" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="News_List.aspx.cs" Inherits="_260311_hw_7Systems.System01_News.News_List" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <div>
            <asp:Button ID="AddNew" runat="server" Text="新增" />
        </div>
        <br />
        <asp:GridView ID="GridView1" runat="server"></asp:GridView>
    </main>
</asp:Content>
