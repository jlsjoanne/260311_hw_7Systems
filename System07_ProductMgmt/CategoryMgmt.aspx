<%@ Page Title="商品分類管理" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CategoryMgmt.aspx.cs" Inherits="_260311_hw_7Systems.System07_ProductMgmt.CategoryMgmt" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title">
        <div>
            <asp:Button ID="AddMain" runat="server" Text="新增主類別" OnClick="AddMain_Click" />
            &emsp;
            <asp:Button ID="AddSub" runat="server" Text="新增子類別" OnClick="AddSub_Click" />
        </div>
        <br />
        <div>
            <p><b>主類別</b></p>
            <asp:GridView ID="MainCategoryGrid" runat="server" AutoGenerateColumns="False" DataKeyNames="MainCId" DataSourceID="MainCategoryDataSource">
                <Columns>
                    <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
                    <asp:HyperLinkField DataNavigateUrlFields="MainCId" DataNavigateUrlFormatString="CategoryMgmt_Sub.aspx?MainCId={0}" DataTextField="MainCId" HeaderText="主類別編號" />
                    <asp:BoundField DataField="MainCategoryName" HeaderText="主類別名稱" SortExpression="MainCategoryName" />
                    <asp:BoundField DataField="MainOrder" HeaderText="排序(不得重複)" SortExpression="MainOrder" />
                </Columns>
            </asp:GridView>
            <asp:SqlDataSource ID="MainCategoryDataSource" runat="server" 
                ConnectionString="<%$ ConnectionStrings:ProductDB %>" 
                DeleteCommand="DELETE FROM [MainCategory] WHERE [MainCId] = @MainCId" 
                SelectCommand="SELECT * FROM [MainCategory] ORDER BY [MainOrder] ASC" 
                UpdateCommand="UPDATE [MainCategory] SET [MainCategoryName] = @MainCategoryName, [MainOrder] = @MainOrder WHERE [MainCId] = @MainCId">
                <DeleteParameters>
                    <asp:Parameter Name="MainCId" Type="Int32" />
                </DeleteParameters>
                <UpdateParameters>
                    <asp:Parameter Name="MainCategoryName" Type="String" />
                    <asp:Parameter Name="MainOrder" Type="Int32" />
                    <asp:Parameter Name="MainCId" Type="Int32" />
                </UpdateParameters>
            </asp:SqlDataSource>
        </div>
    </main>
</asp:Content>
