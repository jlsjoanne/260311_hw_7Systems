<%@ Page Title="新增圖片資訊" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Photo_Add_Desc.aspx.cs" Inherits="_260311_hw_7Systems.System02_PhotoAlbums.Photo_Add_Desc" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <h3>新增圖片資訊</h3>
        <br />
        <asp:PlaceHolder ID="PhPhoto" runat="server"></asp:PlaceHolder>
        <br />
        <div>
            <asp:Button ID="Submit" runat="server" Text="下一步" OnClick="Submit_Click" />
        </div>
    </main>
</asp:Content>