<%@ Page Title="管理最新消息" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="News_List_Mgmt.aspx.cs" Inherits="_260311_hw_7Systems.System01_News.News_List_Mgmt" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title">
        <asp:GridView ID="NewsGrid" runat="server" Width="100%"
            AutoGenerateColumns="False" DataKeyNames="NewsId"
            OnRowDeleting="NewsGrid_RowDeleting"
            AllowPaging="True" PageSize="20"
            OnPageIndexChanging="NewsGrid_PageIndexChanging">
            <Columns>
                <asp:CommandField ShowDeleteButton="True" />
                <asp:HyperLinkField DataNavigateUrlFields="NewsId" DataNavigateUrlFormatString="News_Edit.aspx?NewsId={0}" Text="Edit" />
                <asp:BoundField DataField="CategoryName" HeaderText="分類" />
                <asp:HyperLinkField DataTextField="NewsTitle" HeaderText="標題" DataNavigateUrlFields="NewsId" DataNavigateUrlFormatString="News.aspx?NewsId={0}" />
                <asp:BoundField DataField="PostDate" HeaderText="發表時間" />
            </Columns>

        </asp:GridView>
    </main>
</asp:Content>