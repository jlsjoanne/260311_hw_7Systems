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
                    <asp:BoundField DataField="MainCategoryName" HeaderText="主類別名稱" SortExpression="MainCategoryName" />
                    <asp:BoundField DataField="MainOrder" HeaderText="排序(不得重複)" SortExpression="MainOrder" />
                </Columns>
            </asp:GridView>
            <asp:SqlDataSource ID="MainCategoryDataSource" runat="server" 
                ConnectionString="<%$ ConnectionStrings:ProductDB %>" 
                DeleteCommand="DELETE FROM [MainCategory] WHERE [MainCId] = @MainCId" 
                SelectCommand="SELECT * FROM [MainCategory]" 
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
        <br />
        <div>
            <p><b>子類別</b></p>
            <asp:GridView ID="SubCategoryGrid" runat="server" AutoGenerateColumns="False" DataSourceID="SubCategoryDataSource" DataKeyNames="SubCId">
                <Columns>
                    <asp:BoundField DataField="SubCId" HeaderText="SubCId" InsertVisible="False" ReadOnly="True" SortExpression="SubCId" />
                    <asp:BoundField DataField="MainCategoryName" HeaderText="MainCategoryName" SortExpression="MainCategoryName" />
                    <asp:BoundField DataField="SubCategoryName" HeaderText="SubCategoryName" SortExpression="SubCategoryName" />
                    <asp:BoundField DataField="SubOrder" HeaderText="SubOrder" SortExpression="SubOrder" />
                </Columns>
            </asp:GridView>
            <asp:SqlDataSource ID="SubCategoryDataSource" runat="server" 
                ConnectionString="<%$ ConnectionStrings:ProductDB %>" 
                DeleteCommand="DELETE FROM SubCategory WHERE SubCId = @SubCId" 
                SelectCommand="SELECT Sub.SubCId, Main.MainCategoryName, Sub.SubCategoryName, SubOrder FROM [MainCategory] AS Main LEFT JOIN [SubCategory] AS Sub ON Main.MainCId = Sub.MainCId">
                <DeleteParameters>
                    <asp:Parameter Name="SubCId" />
                </DeleteParameters>
            </asp:SqlDataSource>
        </div>
    </main>
</asp:Content>
