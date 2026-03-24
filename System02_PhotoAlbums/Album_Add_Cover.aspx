<%@ Page Title="選擇封面" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Album_Add_Cover.aspx.cs" Inherits="_260311_hw_7Systems.System02_PhotoAlbums.Album_Add_Cover" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title">
        <div>
            <b>
                <p>選擇相簿封面</p>
            </b>
            <asp:DropDownList ID="DropDownList1" runat="server"></asp:DropDownList>
            
        </div>
        <br />
        <div>
            <asp:Button ID="Submit" runat="server" Text="送出" />
        </div>
    </main>
</asp:Content>
