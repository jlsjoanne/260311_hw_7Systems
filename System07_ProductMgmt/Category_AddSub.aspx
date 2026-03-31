<%@ Page Title="新增子類別" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Category_AddSub.aspx.cs" Inherits="_260311_hw_7Systems.System07_ProductMgmt.Category_AddSub" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <h2>新增子類別</h2>
        <div>
            <h3>選擇主類別</h3>
            <asp:DropDownList ID="MainDropdown" runat="server" DataSourceID="MainCategoryDataSource" DataTextField="MainCategoryName" DataValueField="MainCId"></asp:DropDownList>
            <asp:SqlDataSource ID="MainCategoryDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:ProductDB %>" SelectCommand="SELECT * FROM [MainCategory]"></asp:SqlDataSource>
        </div>
        <br />
        <div>
            <h3>輸入子類別</h3>
            <asp:TextBox ID="SubCategory" runat="server"></asp:TextBox>
        </div>
        <br />
        <div>
            <asp:Button ID="Submit" runat="server" Text="送出" OnClick="Submit_Click" />
        </div>
    </main>
</asp:Content>
