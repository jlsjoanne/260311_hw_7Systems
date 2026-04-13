<%@ Page Title="修改密碼" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="User_ChangePwd.aspx.cs" Inherits="_260311_hw_7Systems.User.User_ChangePwd" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <h3>修改密碼</h3>
        <div>
            <b>原密碼: &emsp;</b>
            <asp:TextBox ID="OldPwd" runat="server" AutoPostBack="True"></asp:TextBox>
            &nbsp;
            <asp:Label ID="checkPwd" runat="server"></asp:Label>
        </div>
        <br />
        <div>
            <b>新密碼: &emsp;</b>
            <asp:TextBox ID="newPwd" runat="server"></asp:TextBox>
            <br />
            <b>確認新密碼: &emsp;</b>
            <asp:TextBox ID="checkNewPwd" runat="server" AutoPostBack="True"></asp:TextBox>
            &nbsp;
            <asp:Label ID="checkNew" runat="server"></asp:Label>
        </div>
        <br />
        <div>
            <asp:Button ID="Submit" runat="server" Text="送出" OnClick="Submit_Click" />
        </div>
    </main>
</asp:Content>