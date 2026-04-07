<%@ Page Title="檔案管理" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="News_File_Mgmt.aspx.cs" Inherits="_260311_hw_7Systems.System01_News.News_File_Mgmt" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <div>
            <asp:Button ID="AddFile" runat="server" Text="新增檔案" Visible="False"/>
        </div>
        <br />
        <div>
            <asp:GridView ID="FileGrid" runat="server" 
                AutoGenerateColumns="False" Visible="False"></asp:GridView>
        </div>
    </main>
</asp:Content>