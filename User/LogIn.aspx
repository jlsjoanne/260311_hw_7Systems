<%@ Page Title="登入" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LogIn.aspx.cs" Inherits="_260311_hw_7Systems.User.LogIn" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <h4>帳號登入</h4>
        <div>

            <asp:Label ID="UserName" runat="server" Text="帳號"></asp:Label>
            <asp:TextBox ID="UsernameInput" runat="server"></asp:TextBox>

        </div>
        <br />
        <div>

            <asp:Label ID="PassWord" runat="server" Text="密碼"></asp:Label>
            <asp:TextBox ID="PassWordInput" runat="server" TextMode="Password"></asp:TextBox>

        </div>
        <br />
        <div>

            <asp:Button ID="Submit" runat="server" Text="登入" OnClick="Submit_Click" />

        </div>
        <br />
        <div>
            <asp:LinkButton ID="SignUp" runat="server" OnClick="SignUp_Click">尚未註冊? 前往註冊</asp:LinkButton>
        </div>
    </main>
</asp:Content>
