<%@ Page Title="Add New Article" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Article_Add.aspx.cs" Inherits="_260311_hw_7Systems.System06_Forums.Article_Add" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <h2><b>新增文章</b></h2>
        <br />
        <div>
            <h3><b>標題</b></h3>
            <asp:TextBox ID="ATitle" runat="server" Width="100%"></asp:TextBox>
        </div>
        <br />
        <div>
            <h3>內容</h3>
            <asp:TextBox ID="AContent" runat="server" TextMode="MultiLine" Rows="10" Width="100%"></asp:TextBox>
        </div>
        <br />
        <div>
            <asp:Button ID="Submit" runat="server" Text="送出" OnClick="Submit_Click" />
        </div>
    </main>
</asp:Content>
