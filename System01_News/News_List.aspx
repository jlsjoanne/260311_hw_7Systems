<%@ Page Title="News_List" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="News_List.aspx.cs" Inherits="_260311_hw_7Systems.System01_News.News_List" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <div>
            <asp:Button ID="AddNew" runat="server" Text="新增" Visible="False" OnClick="AddNew_Click"/>
        </div>
        <br />
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="NewsId" DataSourceID="SqlDataSource1">
            <Columns>
                <asp:CommandField ShowDeleteButton="False" />
                <asp:HyperLinkField DataNavigateUrlFields="NewsId" DataNavigateUrlFormatString="News_Edit.aspx?NewsId={0}" HeaderText="Edit" Text="Edit" Visible="False"/>
                <asp:BoundField DataField="CategoryId" HeaderText="CategoryId" SortExpression="CategoryId" />
                <asp:HyperLinkField DataNavigateUrlFields="NewsId" DataNavigateUrlFormatString="News.aspx?NewsId={0}" DataTextField="NewsTitle" HeaderText="標題" />
                <asp:BoundField DataField="PostDate" HeaderText="PostDate" SortExpression="PostDate" />
            </Columns>
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:NewsDB %>" DeleteCommand="DELETE FROM [NewsList] WHERE [NewsId] = @NewsId" InsertCommand="INSERT INTO [NewsList] ([NewsTitle], [CategoryId], [PostDate]) VALUES (@NewsTitle, @CategoryId, @PostDate)" SelectCommand="SELECT [NewsId], [NewsTitle], [CategoryId], [PostDate] FROM [NewsList]" UpdateCommand="UPDATE [NewsList] SET [NewsTitle] = @NewsTitle, [CategoryId] = @CategoryId, [PostDate] = @PostDate WHERE [NewsId] = @NewsId">
        <DeleteParameters>
            <asp:Parameter Name="NewsId" Type="Int32" />
        </DeleteParameters>
        
    </asp:SqlDataSource>
    </main>
</asp:Content>
