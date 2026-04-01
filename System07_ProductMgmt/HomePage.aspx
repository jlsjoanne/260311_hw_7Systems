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
            <b>主類別: &emsp;</b>
            <asp:DropDownList ID="MainDropDown" runat="server" AutoPostBack="True" OnSelectedIndexChanged="MainDropDown_SelectedIndexChanged"></asp:DropDownList>
            <br />
            <b>子類別: &emsp;</b>
            <asp:DropDownList ID="SubDropDown" runat="server" AutoPostBack="True" OnSelectedIndexChanged="SubDropDown_SelectedIndexChanged">
                <asp:ListItem Value="0">------</asp:ListItem>

            </asp:DropDownList>
            <br />
            <asp:GridView ID="ProductGrid" runat="server" Width="100%"
                DataKeyNames="ProductId" OnRowDeleting="ProductGrid_RowDeleting" AutoGenerateColumns="False"
                AllowPaging="True" PageSize="10" OnPageIndexChanging="ProductGrid_PageIndexChanging">
                <Columns>
                    <asp:CommandField ShowDeleteButton="False" />
                    <asp:BoundField DataField="MainCategoryName" HeaderText="主類別" />
                    <asp:BoundField DataField="SubCategoryName" HeaderText="子類別" />
                    <asp:HyperLinkField DataTextField="ProductName" HeaderText="產品名稱" DataNavigateUrlFields="ProductId" DataNavigateUrlFormatString="Product.aspx?ProductId={0}" />
                    <asp:BoundField DataField="BrandName" HeaderText="品牌" />
                    <asp:BoundField DataField="UnitPrice" HeaderText="售價" />
                </Columns>
            </asp:GridView>
        </div>
    </main>
</asp:Content>
