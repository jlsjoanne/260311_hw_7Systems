<%@ Page Title="文章內容" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Article.aspx.cs" Inherits="_260311_hw_7Systems.System06_Forums.Article" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <h3>
            <asp:Label ID="ATitle" runat="server"></asp:Label>
        </h3>
        <p>分類: <asp:Label ID="Category" runat="server"></asp:Label></p>
        <p>發表時間: <asp:Label ID="PostTime" runat="server"></asp:Label></p>
        <p>發表帳號: <asp:Label ID="Username" runat="server"></asp:Label></p>
        <br />
        <p>
            <asp:Literal ID="AContent" runat="server"></asp:Literal></p>
        <br />
        <br />
        <div>
            <p>留言(
                <asp:Label ID="CommentCount" runat="server"></asp:Label>
                )
            </p>
            <asp:Button ID="AddComment" runat="server" Text="新增留言" OnClick="AddComment_Click" Visible="False"/>
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="Username" HeaderText="帳號">
                        <ItemStyle Width="100px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Content" HeaderText="留言">
                         <ItemStyle Width="500px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="PostTime" HeaderText="發送時間" />
                </Columns>
            </asp:GridView>
        </div>
        
        
        
    </main>
</asp:Content>