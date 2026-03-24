<%@ Page Title="相簿系統" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AlbumList.aspx.cs" Inherits="_260311_hw_7Systems.System02_PhotoAlbums.AlbumList" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <h2>相簿系統</h2>
        <div>
            <asp:Button ID="AddNew" runat="server" Text="新增相簿" OnClick="AddNew_Click" Visible="False"/>
        </div>
        <br />
        <div>
            <asp:Repeater ID="AlbumRepeater" runat="server" OnItemCommand="AlbumRepeater_ItemCommand">
                <ItemTemplate>
                    <tr>
                        <td><asp:ImageButton ID="Albums" runat="server"
                            ImageUrl ='<%# CombinePath("~/Images/",Eval("PhotoPath")) %>'
                            CommandName ="ToAlbum"
                            CommandArgument='<%# Eval("AlbumId")%>'
                            AlternateText='<%# Eval("PhotoName") %>'
                            Height="200px" Width="100%"/>

                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </main>
</asp:Content>
