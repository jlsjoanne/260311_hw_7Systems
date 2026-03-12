<%@ Page Title="帳號管理" MasterPageFile="~/Site.Master" Language="C#" AutoEventWireup="true" CodeBehind="Management.aspx.cs" Inherits="_260311_hw_7Systems.User.Management" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title">
        <div>
            
            
            <asp:GridView ID="GridView1" runat="server" 
                DataKeyNames="UserName" 
                DataSourceID="SqlDataSource1">
                <Columns>
                    <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
                </Columns>
            </asp:GridView>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                ConnectionString="<%$ ConnectionStrings:UserDB %>" 
                SelectCommand="SELECT * FROM [UserList]" 
                DeleteCommand="DELETE FROM [UserList] Where UserName = @UserName" 
                UpdateCommand="UPDATE [UserList] SET RoleId = @RoleId, PassWord = @PassWord WHERE UserName = @UserName;">
                <DeleteParameters>
                    <asp:Parameter Name="UserName" />
                </DeleteParameters>
                <UpdateParameters>
                    <asp:Parameter Name="RoleId" />
                    <asp:Parameter Name="PassWord" />
                    <asp:Parameter Name="UserName" />
                </UpdateParameters>
            </asp:SqlDataSource>
            
            
        </div>
    </main>
</asp:Content>