<%@ Page Title="帳號管理" MasterPageFile="~/Site.Master" Language="C#" AutoEventWireup="true" CodeBehind="Management.aspx.cs" Inherits="_260311_hw_7Systems.User.Management" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title">
        <div>
            <asp:GridView ID="GridView1" runat="server" Visible="False" DataKeyName ="UserName"
                OnRowEditing ="GridView1_RowEditing" OnRowCancelingEdit ="GridView1_RowCancelingEdit"
                OnRowUpdating="GridView1_RowUpdating" OnRowDeleting="GridView1_RowDeleting">
                <Columns>
                    <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
                </Columns>

            </asp:GridView>
            
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" >
                <DeleteParameters>
                    <asp:Parameter Name="UserName" Type="String" />
                </DeleteParameters>
                <UpdateParameters>
                    <asp:Parameter Name="PassWord" Type="String" />
                    <asp:Parameter Name="RoleId" Type="Int32" />
                    <asp:Parameter Name="UserName" Type="String" />
                </UpdateParameters>
            </asp:SqlDataSource>
            
        </div>
    </main>
</asp:Content>