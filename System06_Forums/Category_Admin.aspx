<%@ Page Title="論壇版面權限管理" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Category_Admin.aspx.cs" Inherits="_260311_hw_7Systems.System06_Forums.Category_Admin" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <div>
            <asp:Button ID="AddAdmin" runat="server" Text="新增權限" OnClick="AddAdmin_Click" />
            &emsp; &emsp;
            <asp:Button ID="GoBack" runat="server" Text="返回論壇" OnClick="GoBack_Click" />
        </div>
        <br />
        <div>
            <asp:GridView ID="AdminGrid" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" DataKeyNames="SerialNum">
                <Columns>
                    <asp:CommandField ShowDeleteButton="True" />
                    <asp:BoundField DataField="CategoryName" HeaderText="分類" />
                    <asp:BoundField DataField="UserName" HeaderText="使用者名稱" />
                </Columns>
            </asp:GridView>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ForumDB %>" 
                DeleteCommand="DELETE FROM [Admin] WHERE [SerialNum] = @SerialNum" 
                SelectCommand="SELECT * FROM [Admin] AS A JOIN [Category] AS C ON A.CategoryID = C.CategoryID">
                <DeleteParameters>
                    <asp:Parameter Name="SerialNum" Type="Object" />
                </DeleteParameters>
            </asp:SqlDataSource>
        </div>
    </main>
</asp:Content>