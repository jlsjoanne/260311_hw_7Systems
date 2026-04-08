<%@ Page Title="照片" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Photo.aspx.cs" Inherits="_260311_hw_7Systems.System02_PhotoAlbums.Photo" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title">
        <div>
            <h3>
                相片名稱: 
                <asp:Label ID="PhotoName" runat="server"></asp:Label>
            </h3>
            <p>
                相片說明: 
                <asp:Label ID="PhotoDesc" runat="server"></asp:Label>
            </p>
        </div>
        <br />
        <div>
            <asp:Image ID="PhotoPath" runat="server"
                Height="100%" Width="100%"/>
        </div>
    </main>
</asp:Content>
