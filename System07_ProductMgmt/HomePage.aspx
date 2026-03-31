<%@ Page Title="商品管理系統" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="HomePage.aspx.cs" Inherits="_260311_hw_7Systems.System07_ProductMgmt.HomePage" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title">
        <div>
            <asp:Button ID="AddNew" runat="server" Text="新增商品" OnClick="AddNew_Click" Visible="False" />
            &emsp;
            <asp:Button ID="CategoryMgmt" runat="server" Text="分類管理" Visible="False" OnClick="CategoryMgmt_Click" />
            &emsp;
            <asp:Button ID="BrandMgmt" runat="server" Text="品牌管理" Visible="False" OnClick="BrandMgmt_Click" />
        </div>
        <br />
        <div>
            <h2>商品管理系統</h2>
        </div>
    </main>
</asp:Content>
