<%@ Page Title="相簿編輯" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Album_Edit.aspx.cs" Inherits="_260311_hw_7Systems.System02_PhotoAlbums.Album_Edit" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <h3>相簿資訊編輯</h3>
        <div>
            <b>相簿名稱:</b> &emsp;
            <asp:TextBox ID="AName" runat="server" Width="100%"></asp:TextBox>
            <br />
            <b>相簿描述:</b> &emsp;
            <asp:TextBox ID="ADesc" runat="server" Width="100%"></asp:TextBox>
        </div>
        <br />
        <div>
            <asp:Button ID="Submit" runat="server" Text="送出" OnClick="Submit_Click" />
        </div>
    </main>
</asp:Content>