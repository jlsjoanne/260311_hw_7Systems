<%@ Page Title="論壇首頁" MasterPageFile="~/Site.Master" Language="C#" AutoEventWireup="true" CodeBehind="CategoryList.aspx.cs" Inherits="_260311_hw_7Systems.System06_Forums.CategoryList" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <h2>文章類別</h2>
        <div>
            <asp:Button ID="AddCategory" runat="server" Text="新增類別" OnClick="AddCategory_Click" Visible="False" /> &nbsp;
            <asp:Button ID="AdminMgn" runat="server" Text="版面權限管理" OnClick="AdminMgn_Click" Visible="False" />
        </div>

        <br />
        <div>
            <asp:GridView ID="GridView1" runat="server" DataSourceID="ForumCategory" DataKeyNames="CategoryID" AutoGenerateColumns="False" Width ="100%">
                <Columns>
                    <asp:CommandField ShowDeleteButton="False" ShowEditButton="False" />
                    <asp:BoundField DataField="CategoryOrder" HeaderText="分類排序" Visible="False" />
                    <asp:BoundField DataField="CategoryName" HeaderText="分類名稱" Visible="False" />
                    <asp:HyperLinkField DataNavigateUrlFields="CategoryID" DataNavigateUrlFormatString="ArticleList.aspx?CategoryID={0}" DataTextField="CategoryName" HeaderText="分類名稱" />
                </Columns>
            </asp:GridView>
            <asp:SqlDataSource ID="ForumCategory" runat="server" 
                ConnectionString="<%$ ConnectionStrings:ForumDB %>" 
                DeleteCommand="DELETE FROM [Category] WHERE CategoryID = @CategoryID" 
                SelectCommand="SELECT * FROM [Category] ORDER BY CategoryOrder" 
                UpdateCommand="UPDATE [Category] SET CategoryName = @CategoryName, CategoryOrder = @CategoryOrder WHERE CategoryID = @CategoryID">
                <DeleteParameters>
                    <asp:Parameter Name="CategoryID" />
                </DeleteParameters>
                <UpdateParameters>
                    <asp:Parameter Name="CategoryName" />
                    <asp:Parameter Name="CategoryID" />
                </UpdateParameters>
            </asp:SqlDataSource>
        </div>
    </main>
</asp:Content>
