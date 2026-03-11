<%@ Page Title="註冊" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SignUp.aspx.cs" Inherits="_260311_hw_7Systems.User.SignUp" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <h4>帳號註冊</h4>
        <div>
            <asp:Label ID="UName" runat="server" Text="帳號"></asp:Label> <br />
            <asp:TextBox ID="UNameInput" runat="server"></asp:TextBox>
        </div>
        <br />
        <div>
            <asp:Label ID="Pwd" runat="server" Text="密碼"></asp:Label> <br />
            <asp:TextBox ID="PwdInput" runat="server" TextMode="Password"></asp:TextBox>
        </div>
        <br />
        <div>

            <asp:Label ID="CPwd" runat="server" Text="確認密碼"></asp:Label> <br />
            <asp:TextBox ID="CPwdInput" runat="server" TextMode="Password"></asp:TextBox>&nbsp;
            

        </div>
        <br />
        <div>

            <asp:Button ID="Submit" runat="server" Text="送出" OnClick="Submit_Click" />

        </div>
    </main>
</asp:Content>