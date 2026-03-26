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
                <asp:BoundField DataField="CategoryName" HeaderText="分類" SortExpression="CategoryId" />
                <asp:HyperLinkField DataNavigateUrlFields="NewsId" DataNavigateUrlFormatString="News.aspx?NewsId={0}" DataTextField="NewsTitle" HeaderText="標題" />
                <asp:BoundField DataField="PostDate" HeaderText="PostDate" SortExpression="PostDate" />
            </Columns>
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:NewsDB %>" 
        DeleteCommand="DELETE FROM [NewsList] WHERE [NewsId] = @NewsId" 
        SelectCommand="SELECT [NewsId], [NewsTitle], C.CategoryName, N.CategoryId, [PostDate] FROM [NewsList] AS N JOIN [Category] AS C ON N.CategoryId = C.CategoryId" >
        <DeleteParameters>
            <asp:Parameter Name="NewsId" Type="String" />
        </DeleteParameters>
        
    </asp:SqlDataSource>
    </main>
</asp:Content>
