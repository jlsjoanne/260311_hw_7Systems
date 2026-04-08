<%@ Page Title="新增類別" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Category_Add.aspx.cs" Inherits="_260311_hw_7Systems.System06_Forums.Category_Add" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <h2>新增類別</h2>
        <br />
        <div>
            <h3>類別名稱</h3>
            <p>(字數限制:50)</p>
            <asp:TextBox ID="CategoryName" runat="server"></asp:TextBox>
        </div>
        <br />
        
        <div>
            <asp:Button ID="Submit" runat="server" Text="送出" OnClick="Submit_Click" />
        </div>
    </main>
</asp:Content>

