<%@ Page Title="帳號" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Setting.aspx.cs" Inherits="_260311_hw_7Systems.User.Setting" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <h2>帳號資訊</h2>
        <div>
            <asp:Label ID="UserName" runat="server" Visible="False" Text="帳號: "></asp:Label>
            <br />
            <asp:Label ID="RoleName" runat="server" Text="權限: "></asp:Label>
        </div>
        <div>
            <asp:LinkButton ID="Management" runat="server" Visible="False" OnClick="Management_Click">會員管理</asp:LinkButton>
        </div>
        
    </main>
</asp:Content>