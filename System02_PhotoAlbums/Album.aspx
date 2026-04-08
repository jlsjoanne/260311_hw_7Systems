<%@ Page Title="相簿" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Album.aspx.cs" Inherits="_260311_hw_7Systems.System02_PhotoAlbums.Album" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <div>
            <asp:Button ID="AddPhoto" runat="server" Text="新增相片" Visible="False" OnClick="AddPhoto_Click"/>
            &emsp; &emsp;
            <asp:Button ID="AlbumMgmt" runat="server" Text="相簿管理" Visible="False" OnClick="AlbumMgmt_Click" />
        </div>
        <div>
            <h2>
                相簿名稱: 
                <asp:Label ID="AlbumTitle" runat="server"></asp:Label>
            </h2>
            <h4>
                相簿說明: 
                <asp:Label ID="AlbumDesc" runat="server"></asp:Label>
            </h4>
        </div>
        <div>
            <asp:Repeater ID="PhotoRepeater" runat="server" OnItemCommand="PhotoRepeater_ItemCommand">
                <ItemTemplate>
                    <tr>
                        <td>
                            <asp:ImageButton ID="PhotoBtn" runat="server"
                                ImageUrl='<%# CombinePath("~/Images/", Eval("PhotoPath")) %>'
                                CommandName="ToPhotoInfo"
                                CommandArgument='<%# Eval("PhotoId") %>'
                                AlternateText='<%# Eval("PhotoName") %>'
                                Height="150px" Width="100%"/>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </main>
</asp:Content>
