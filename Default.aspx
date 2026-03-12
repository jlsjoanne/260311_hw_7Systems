<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="_260311_hw_7Systems._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <main>
        <h2>七大系統作業</h2>
        <div>
            <asp:Label ID="Welcome" runat="server" Text="Welcome!"></asp:Label>
        </div>
        <br />
        <div>

            <asp:LinkButton ID="LogIn" runat="server" OnClick="LogIn_Click">登入</asp:LinkButton>
            <asp:LinkButton ID="LogOut" runat="server" OnClick="LogOut_Click" Visible="False">登出</asp:LinkButton>
        </div>
        <div>

            <asp:LinkButton ID="SignUp" runat="server" OnClick="SignUp_Click">註冊</asp:LinkButton>

        </div>
    </main>

</asp:Content>
