<%@ Page Title="News_List" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="News_List.aspx.cs" Inherits="_260311_hw_7Systems.System01_News.News_List" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <div>
            <asp:Button ID="AddNew" runat="server" Text="新增" Visible="False" OnClick="AddNew_Click"/>
            &emsp; &emsp;
            <asp:Button ID="NewsMgmt" runat="server" Text="管理最新消息" Visible="False" OnClick="NewsMgmt_Click" />
        </div>
        <br />
        <div>
            <asp:GridView ID="NewsGrid" runat="server" 
                AutoGenerateColumns="False"
                AllowPaging="True" PageSize="20"
                OnPageIndexChanging="NewsGrid_PageIndexChanging">
                <Columns>
                    <asp:BoundField DataField="CategoryName" HeaderText="類別" />
                    <asp:HyperLinkField DataNavigateUrlFields="NewsId" DataNavigateUrlFormatString="News.aspx?NewsId={0}" DataTextField="NewsTitle" HeaderText="標題" />
                    <asp:BoundField DataField="PostDate" HeaderText="發表日期" />
                </Columns>
            </asp:GridView>
        </div>
    </main>
</asp:Content>
