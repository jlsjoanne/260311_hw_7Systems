<%@ Page Title="廣告分類管理" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Ads_Mgmt.aspx.cs" Inherits="_260311_hw_7Systems.System04_Ads.Ads_Mgmt" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <h3>廣告分類管理</h3>
        <div>
            <asp:Button ID="GoBack" runat="server" Text="返回廣告頁面" OnClick="GoBack_Click" />
            &emsp; &emsp;
            <asp:Button ID="AddCategory" runat="server" Text="新增廣告類別" OnClick="AddCategory_Click" />
        </div>
        <br />
        <asp:GridView ID="CategoryGrid" runat="server" 
            AutoGenerateColumns="False" DataKeyNames="CategoryId"
            OnRowDeleting="CategoryGrid_RowDeleting"
            OnRowEditing="CategoryGrid_RowEditing"
            OnRowUpdating="CategoryGrid_RowUpdating"
            OnRowCancelingEdit="CategoryGrid_RowCancelingEdit">
            <Columns>
                <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
                <asp:BoundField DataField="CategoryName" HeaderText="分類名稱" />
                <asp:BoundField DataField="CategoryOrder" HeaderText="分類排序" />
                <asp:HyperLinkField HeaderText="分類廣告管理" Text="Manage" DataNavigateUrlFields="CategoryId" DataNavigateUrlFormatString="Ads_Mgmt_byCategory.aspx?CategoryId={0}" />
            </Columns>
    </asp:GridView>
    </main>
</asp:Content>