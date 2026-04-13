<%@ Page Title="文章列表" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ArticleList.aspx.cs" Inherits="_260311_hw_7Systems.System06_Forums.ArticleList" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <div>
            <asp:LinkButton ID="MyArticle" runat="server" Visible="False" OnClick="MyArticle_Click">我的文章</asp:LinkButton>
            &emsp; &emsp;
            <asp:Button ID="AddNew" runat="server" Text="新增文章" OnClick="AddNew_Click" Visible="False"/>
        </div>
        <br />
        <h2>
            <b>
                <asp:Label ID="CategoryName" runat="server"></asp:Label>
            </b>
        </h2>
        <h4>
            文章列表 (文章數: <asp:Label ID="ArticleCnt" runat="server"></asp:Label>)
        </h4>
        <br />
        <div>
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
                OnRowDeleting="GridView1_RowDeleting">
                <Columns>
                    <asp:CommandField ShowDeleteButton="False"/>
                    <asp:HyperLinkField DataNavigateUrlFields="ArticleID" DataNavigateUrlFormatString="Article.aspx?ArticleID={0}" DataTextField="Title" HeaderText="標題" />
                    <asp:BoundField DataField="PostTime" HeaderText="發表時間" />
                </Columns>
            </asp:GridView>
        </div>
    </main>
</asp:Content>
