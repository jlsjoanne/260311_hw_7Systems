<%@ Page Title="新增廠牌" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Brand_Add.aspx.cs" Inherits="_260311_hw_7Systems.System07_ProductMgmt.Brand_Add" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <div>
            <p><b>新增品牌</b></p>
            <asp:TextBox ID="BrandName" runat="server"></asp:TextBox>
        </div>
        <br />
        <div>
            <asp:Button ID="Submit" runat="server" Text="送出" OnClick="Submit_Click" />
        </div>
    </main>
</asp:Content>
