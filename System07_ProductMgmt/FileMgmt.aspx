<%@ Page Title="商品檔案管理" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FileMgmt.aspx.cs" Inherits="_260311_hw_7Systems.System07_ProductMgmt.FileMgmt" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <div>
            <asp:Button ID="AddNew" runat="server" Text="新增檔案" 
                OnClick="AddNew_Click" Visible="False" />
        </div>
        <br />
        <div>
            <p><b>檔案管理</b></p>
            <asp:GridView ID="FileGrid" runat="server" 
                AutoGenerateColumns="False" DataKeyNames="FileId"
                 OnRowDeleting="FileGrid_RowDeleting">
                <Columns>
                    <asp:CommandField ShowDeleteButton="False" />
                    <asp:BoundField DataField="FName" HeaderText="檔案名稱" />
                    <asp:BoundField DataField="FDesc" HeaderText="檔案說明" />
                </Columns>

            </asp:GridView>
        </div>
    </main>
</asp:Content>