<%@ Page Title="子類別管理" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CategoryMgmt_Sub.aspx.cs" Inherits="_260311_hw_7Systems.System07_ProductMgmt.CategoryMgmt_Sub" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title">
        <div>
            <asp:Button ID="AddSub" runat="server" Text="新增子類別" OnClick="AddSub_Click" />
        </div>
        <br />
        <div>
            <p><b>子類別</b></p>
            <asp:GridView ID="SubCategoryGrid" runat="server" DataSourceID="SubCategoryDataSource" DataKeyNames="SubCId" AutoGenerateColumns="False">
                <Columns>
                    <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
                    <asp:BoundField DataField="SubCategoryName" HeaderText="子分類名稱" />
                    <asp:BoundField DataField="SubOrder" HeaderText="子分類排序" />
                </Columns>
            </asp:GridView>
            <asp:SqlDataSource ID="SubCategoryDataSource" runat="server" 
                ConnectionString="<%$ ConnectionStrings:ProductDB %>" 
                DeleteCommand="DELETE FROM [SubCategory] WHERE [SubCId] = @SubCId"  
                SelectCommand="SELECT * FROM [SubCategory] WHERE ([MainCId] = @MainCId) ORDER BY MainCID, SubOrder" 
                UpdateCommand="UPDATE [SubCategory] SET [SubCategoryName] = @SubCategoryName, [SubOrder] = @SubOrder WHERE [SubCId] = @SubCId">
                <DeleteParameters>
                    <asp:Parameter Name="SubCId" Type="Int32" />
                </DeleteParameters>
                <SelectParameters>
                    <asp:QueryStringParameter Name="MainCId" QueryStringField="MainCId" Type="Int32" />
                </SelectParameters>
                <UpdateParameters>
                    <asp:Parameter Name="SubCategoryName" Type="String" />
                    <asp:Parameter Name="SubOrder" Type="Int32" />
                    <asp:Parameter Name="MainCId" Type="Int32" />
                    <asp:Parameter Name="SubCId" Type="Int32" />
                </UpdateParameters>
            </asp:SqlDataSource>
        </div>
    </main>
</asp:Content>
