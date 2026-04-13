<%@ Page Title="影片管理" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Video_Mgmt_ByCategory.aspx.cs" Inherits="_260311_hw_7Systems.System05_Video.Video_Mgmt_ByCategory" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <div>
            <asp:Button ID="GoBack" runat="server" Text="返回影片分類管理" OnClick="GoBack_Click" />
            &emsp; &emsp;
            <asp:Button ID="AddNew" runat="server" Text="新增影片" Visible="False" OnClick="AddNew_Click" />
        </div>
        <br />
        <div>
            <p><b>分類影片管理</b></p>
            <asp:GridView ID="VideoGrid" runat="server" 
                Visible="False" AutoGenerateColumns="False">
                <Columns>
                    <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
                    <asp:BoundField DataField="VideoName" HeaderText="影片名稱" />
                    <asp:BoundField DataField="VideoId" HeaderText="Youtube Video Id" />
                    <asp:HyperLinkField DataNavigateUrlFields="VideoId" DataNavigateUrlFormatString="https://www.youtube.com/watch?v={0}" HeaderText="影片連結" Target="_blank" Text="View" />
                </Columns>
            </asp:GridView>
        </div>
    </main>
</asp:Content>
