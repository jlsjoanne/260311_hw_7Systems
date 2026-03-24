<%@ Page Title="新增相簿" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Album_Add.aspx.cs" Inherits="_260311_hw_7Systems.System02_PhotoAlbums.Album_Add" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <h2>新增相簿</h2>
        <h4>相簿名稱</h4>
        <p>(字數限制:100字)</p>
        <asp:TextBox ID="AlbumName" runat="server" Width="100%"></asp:TextBox>
        <br />
        <b>
            <p>相簿描述</p>
        </b>
        <asp:TextBox ID="AlbumDesc" runat="server"
            TextMode="MultiLine" Rows="10" Width="100%"></asp:TextBox>
        <br />
        <asp:Button ID="Submit" runat="server" Text="送出" OnClick="Submit_Click" />
    </main>
</asp:Content>