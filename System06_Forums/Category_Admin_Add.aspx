<%@ Page Title="新增權限" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Category_Admin_Add.aspx.cs" Inherits="_260311_hw_7Systems.System06_Forums.Category_Admin_Add" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <div>
            <h4>分類</h4>
            <asp:DropDownList ID="Admin_Category" runat="server" DataSourceID="CategoryDataSource" DataTextField="CategoryName" DataValueField="CategoryID"></asp:DropDownList>
            <asp:SqlDataSource ID="CategoryDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:ForumDB %>" SelectCommand="SELECT [CategoryID], [CategoryName] FROM [Category] ORDER BY [CategoryOrder]"></asp:SqlDataSource>
        </div>
        <div>
            <h4>使用者名稱 (UserName)</h4>
            <asp:DropDownList ID="Admin_User" runat="server" DataSourceID="UserDataSource" DataTextField="UserName" DataValueField="UserName"></asp:DropDownList>
            <asp:SqlDataSource ID="UserDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:UserDB %>" SelectCommand="SELECT [UserName] FROM [UserList]"></asp:SqlDataSource>
        </div>
        <br />
        <div>
            <asp:Button ID="Submit" runat="server" Text="送出" OnClick="Submit_Click" />
        </div>
        
        
        
    </main>
</asp:Content>