<%@ Page Title="新增檔案" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="News_File_Add.aspx.cs" Inherits="_260311_hw_7Systems.System01_News.News_File_Add" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <h3>新增檔案</h3>
        <div>
            <b>檔案名稱: &emsp;</b>
            <asp:TextBox ID="FName" runat="server" Width="100%"></asp:TextBox>
            <br />
            <b>檔案描述: &emsp;</b>
            <asp:TextBox ID="FDesc" runat="server" Width="100%"></asp:TextBox>
            <br />
            <b>檔案上傳: &emsp;</b>
            <asp:FileUpload ID="NewsFileUpload" runat="server" />
        </div>
        <br />
        <div>
            <asp:Button ID="Submit" runat="server" Text="送出" OnClick="Submit_Click" />
        </div>
    </main>
</asp:Content>