<%@ Page Title="帳號管理" MasterPageFile="~/Site.Master" Language="C#" AutoEventWireup="true" CodeBehind="Management.aspx.cs" Inherits="_260311_hw_7Systems.User.Management" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title">
        <div>
            <asp:GridView ID="UserGrid" runat="server" DataKeyNames="UserName"
                AutoGenerateColumns="False" Visible="False"
                OnRowEditing="UserGrid_RowEditing"
                OnRowUpdating="UserGrid_RowUpdating"
                OnRowCancelingEdit="UserGrid_RowCancelingEdit"
                OnRowDeleting="UserGrid_RowDeleting">
                <Columns>
                    <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
                    <asp:BoundField DataField="UserName" HeaderText="帳號" ReadOnly="True" />
                    <asp:TemplateField HeaderText="密碼">
                        <ItemTemplate>
                            <asp:Label ID="labelPwd" runat="server" Text="******"></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="tbPwd" runat="server" 
                                Text='<%# Eval("PassWord") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="RoleId" HeaderText="權限代碼" />
                    <asp:BoundField DataField="RoleName" HeaderText="權限名稱" ReadOnly="True" />
                </Columns>
            </asp:GridView>
        </div>
    </main>
</asp:Content>