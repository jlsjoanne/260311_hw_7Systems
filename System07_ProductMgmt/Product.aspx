<%@ Page Title="商品詳情" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Product.aspx.cs" Inherits="_260311_hw_7Systems.System07_ProductMgmt.Product" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <div ID="edit">
            <asp:Button ID="EditProduct" runat="server" Text="編輯商品資訊" Visible="False" OnClick="EditProduct_Click" />
            &emsp;
            <asp:Button ID="ImgMgmt" runat="server" Text="新增/編輯圖片" Visible="False" OnClick="ImgMgmt_Click"/>
            &emsp;
            <asp:Button ID="FileMgmt" runat="server" Text="新增/編輯檔案" Visible="False" OnClick="FileMgmt_Click" />
        </div>
        <br />
        <div ID="ProductInfo">
            <div ID="Category">
                <b>主類別: &emsp;</b>
                <asp:Label ID="MainCategory" runat="server"></asp:Label>
                <br />
                <b>子類別: &emsp;</b>
                <asp:Label ID="SubCategory" runat="server"></asp:Label>
            </div>
            <br />
            <b>品牌: &emsp;</b>
            <asp:Label ID="Brand" runat="server"></asp:Label>
            <br />
            <div ID="Info">
                <b>品名: &emsp;</b>
                <asp:Label ID="PName" runat="server"></asp:Label>
                <br />
                <b>規格: &emsp;</b>
                <asp:Literal ID="PDetails" runat="server"></asp:Literal>
                <br />
                <b>售價: &emsp;</b>
                <asp:Label ID="Price" runat="server"></asp:Label>
            </div>
        </div>
        <br />
        <div ID="Imgs">
            <p><b>圖片 &emsp;</b></p>
            <asp:Repeater ID="ImgRepeater" runat="server"></asp:Repeater>
        </div>
        <br />
        <div ID="Files">
            <p><b>檔案 &emsp;</b></p>
            <asp:Repeater ID="FileRepeater" runat="server"></asp:Repeater>
        </div>
    </main>
</asp:Content>
