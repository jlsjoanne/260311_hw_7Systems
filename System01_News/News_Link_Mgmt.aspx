<%@ Page Title="連結管理" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="News_Link_Mgmt.aspx.cs" Inherits="_260311_hw_7Systems.System01_News.News_Link_Mgmt" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <div>
            <asp:Button ID="GoBack" runat="server" Text="返回消息" OnClick="GoBack_Click" />
            &emsp; &emsp;
            <asp:Button ID="AddLink" runat="server" Text="新增連結" OnClick="AddLink_Click" />
        </div>
        <br />
        <div>
            <p><b>連結管理</b></p>
            <asp:GridView ID="LinkGrid" runat="server" DataKeyNames="LinkId"
                AutoGenerateColumns="False"
                OnRowEditing="LinkGrid_RowEditing"
                OnRowUpdating="LinkGrid_RowUpdating"
                OnRowCancelingEdit="LinkGrid_RowCancelingEdit"
                OnRowDeleting="LinkGrid_RowDeleting">
                <Columns>
                    <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
                    <asp:BoundField DataField="LName" HeaderText="連結名稱" />
                    <asp:BoundField DataField="LUrl" HeaderText="Url" />
                    <asp:CheckBoxField DataField="IsNewPage" HeaderText="是否開新分頁" />
                </Columns>

            </asp:GridView>
        </div>
        
        
    </main>
</asp:Content>