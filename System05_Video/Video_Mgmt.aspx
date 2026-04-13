<%@ Page Title="影片分類管理" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Video_Mgmt.aspx.cs" Inherits="_260311_hw_7Systems.System05_Video.Video_Mgmt" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <div>
            <asp:Button ID="GoBack" runat="server" Text="返回影片列表" OnClick="GoBack_Click" />
            &emsp; &emsp;
            <asp:Button ID="AddCategory" runat="server" Text="新增影片分類" OnClick="AddCategory_Click" />
        </div>
        <br />
        <div>
            <h3>分類管理</h3>
            <asp:GridView ID="CategoryGrid" runat="server" Width="100%"
                AutoGenerateColumns="False" DataKeyNames="CategoryId"
                OnRowEditing="CategoryGrid_RowEditing"
                OnRowUpdating="CategoryGrid_RowUpdating"
                OnRowCancelingEdit="CategoryGrid_RowCancelingEdit"
                OnRowDeleting="CategoryGrid_RowDeleting">
                <Columns>
                    <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
                    <asp:BoundField DataField="CategoryName" HeaderText="分類名稱" />
                    <asp:BoundField DataField="CategoryOrder" HeaderText="分類排序" />
                    <asp:CheckBoxField DataField="IsPublished" HeaderText="是否公開" />
                    <asp:HyperLinkField DataNavigateUrlFields="CategoryId" DataNavigateUrlFormatString="Video_Mgmt_ByCategory.aspx?CategoryId={0}" HeaderText="影片管理" Text="Management" />
                </Columns>
            </asp:GridView>
        </div>
    </main>
</asp:Content>
