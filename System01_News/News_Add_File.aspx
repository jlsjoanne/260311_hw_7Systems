<%@ Page Title="新增檔案資訊" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="News_Add_File.aspx.cs" Inherits="_260311_hw_7Systems.System01_News.News_Add_File" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <h2>新增消息</h2>
        <h3>編輯檔案資訊</h3>
        <br />
        <asp:PlaceHolder ID="PhFile" runat="server"></asp:PlaceHolder>
        <br />
        <div>
            <asp:Button ID="Submit" runat="server" Text="下一步" OnClick="Submit_Click" />
        </div>
    </main>
</asp:Content>