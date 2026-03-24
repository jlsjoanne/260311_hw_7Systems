<%@ Page Title="選擇封面" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Album_Add_Cover.aspx.cs" Inherits="_260311_hw_7Systems.System02_PhotoAlbums.Album_Add_Cover" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title">
        <div>
            <h3>選擇相簿封面</h3>

            <p><b>相片名稱</b></p>
            <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="SqlDataSource1" DataTextField="PhotoName" DataValueField="PhotoId"></asp:DropDownList>
            
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:PhotoAlbumDB %>" SelectCommand="SELECT * FROM [Photo] WHERE ([AlbumId] = @AlbumId)">
                <SelectParameters>
                    <asp:QueryStringParameter Name="AlbumId" QueryStringField="AlbumId" Type="String" />
                </SelectParameters>
            </asp:SqlDataSource>
            
        </div>
        <br />
        <div>
            <asp:Button ID="Submit" runat="server" Text="送出" OnClick="Submit_Click" />
        </div>
    </main>
</asp:Content>
