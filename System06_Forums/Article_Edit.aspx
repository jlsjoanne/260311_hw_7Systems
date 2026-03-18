<%@ Page Title="編輯文章" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Article_Edit.aspx.cs" Inherits="_260311_hw_7Systems.System06_Forums.Article_Edit" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <h2>文章編輯</h2>
        <br />
        <div>
            <h3>標題</h3>
            <asp:TextBox ID="ATitle" runat="server" Width="100%"></asp:TextBox>
        </div>
        
        <div>
            <h4>內文</h4>
            <asp:TextBox ID="AContent" runat="server" TextMode="MultiLine" Rows="10" Width="100%"></asp:TextBox>
        </div>
        <div>
            <asp:Button ID="Submit" runat="server" Text="送出" OnClick="Submit_Click" />
        </div>
    </main>
</asp:Content>