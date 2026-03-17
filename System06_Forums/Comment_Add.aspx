<%@ Page Title="新增留言" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Comment_Add.aspx.cs" Inherits="_260311_hw_7Systems.System06_Forums.Comment_Add" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <h3>新增留言</h3>
        <asp:TextBox ID="Comment" runat="server"></asp:TextBox>
        <br />
        <asp:Button ID="Submit" runat="server" Text="送出" OnClick="Submit_Click" />
    </main>
</asp:Content>