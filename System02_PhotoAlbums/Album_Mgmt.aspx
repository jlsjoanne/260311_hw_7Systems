<%@ Page Title="相簿管理" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Album_Mgmt.aspx.cs" Inherits="_260311_hw_7Systems.System02_PhotoAlbums.Album_Mgmt" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <div>
            <asp:Label ID="NoPermission" runat="server" Text="無此頁面權限"></asp:Label>
        </div>
        <br />
        <div>
            <asp:Button ID="GoBack" runat="server" Text="返回相簿" OnClick="GoBack_Click" />
            &emsp; &emsp;
            <asp:Button ID="EditAlbum" runat="server" Text="編輯相簿資訊" OnClick="EditAlbum_Click" Visible="False" />
            &emsp; &emsp;
            <asp:Button ID="EditCover" runat="server" Text="選擇封面相片" Visible="False" OnClick="EditCover_Click"/>
        </div>
        <br />
        <div>
            <p><b>
                <asp:Label ID="PhotoMgmt" runat="server" Text="相片管理" Visible="False"></asp:Label>
            </b></p>
            <asp:GridView ID="PhotoGrid" runat="server" Visible="False"
                AutoGenerateColumns="False" DataKeyNames="PhotoId"
                OnRowEditing="PhotoGrid_RowEditing"
                OnRowUpdating="PhotoGrid_RowUpdating"
                OnRowCancelingEdit="PhotoGrid_RowCancelingEdit"
                OnRowDeleting="PhotoGrid_RowDeleting">
                <Columns>
                    <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
                    <asp:BoundField DataField="PhotoName" HeaderText="相片名稱" />
                    <asp:BoundField DataField="PhotoDesc" HeaderText="相片描述" />
                    <asp:TemplateField HeaderText="相片">
                        <ItemTemplate>
                            <asp:Image ID="Image1" runat="server"
                                ImageUrl='<%# CombinePath("~/Images/", Eval("PhotoPath")) %>'
                                Height="200px" Width="100%"/>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </main>
</asp:Content>
