<%@ Page Title="我的文章管理" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Article_byUser.aspx.cs" Inherits="_260311_hw_7Systems.System06_Forums.Article_byUser" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <h2>我的文章</h2>
        <div>
            <asp:GridView ID="GridView1" runat="server"
                OnRowDeleting="GridView1_RowDeleting" AutoGenerateColumns="False">
                <Columns>
                    <asp:CommandField ShowDeleteButton="True"/>
                    <asp:HyperLinkField DataNavigateUrlFields="ArticleID" DataNavigateUrlFormatString="Article_Edit.aspx?ArticleID={0}" Text="Edit" />
                    <asp:HyperLinkField DataNavigateUrlFields="ArticleID" DataNavigateUrlFormatString="Article.aspx?ArticleID={0}" DataTextField="Title" HeaderText="標題" />
                    <asp:BoundField DataField="CategoryName" HeaderText="類別" />
                    <asp:BoundField DataField="PostTime" HeaderText="發表時間" />
                </Columns>
            </asp:GridView>
        </div>
    </main>
</asp:Content>
