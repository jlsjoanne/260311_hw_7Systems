<%@ Page Title="商品管理" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProductMgmt.aspx.cs" Inherits="_260311_hw_7Systems.System07_ProductMgmt.ProductMgmt" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <h3><b>新增/編輯商品</b></h3>
        <div id="Category">
            <b>主類別: &emsp;</b>
            <asp:DropDownList ID="MainDropDown" runat="server" 
                OnSelectedIndexChanged="MainDropdown_SelectedIndexChanged"
                AutoPostBack="True">
            </asp:DropDownList> 
            <br />
            <b>子類別: &emsp;</b>
            <asp:DropDownList ID="SubDropDown" runat="server">
                <asp:ListItem Value="0">------</asp:ListItem>
            </asp:DropDownList>
        </div>
        <br />
        <div id="brand">
            <b>品牌: &emsp;&emsp;</b>
            <asp:DropDownList ID="BrandDropdown" runat="server"></asp:DropDownList>
        </div>
        <br />
        <div id="ProductDetails">
            <b>品名: &emsp;</b>
            <asp:TextBox ID="ProductName" runat="server"></asp:TextBox>
            <br />
            <b>規格: &emsp;</b>
            <asp:TextBox ID="ProductDetail" runat="server"
                TextMode="MultiLine" Rows="10"></asp:TextBox>
            <br />
            <b>售價: &emsp;</b>
            <asp:TextBox ID="UnitPrice" runat="server"></asp:TextBox>
        </div>
        <br />
        <div>
            <asp:Button ID="Submit" runat="server" Text="送出" OnClick="Submit_Click" />
        </div>
    </main>
</asp:Content>
