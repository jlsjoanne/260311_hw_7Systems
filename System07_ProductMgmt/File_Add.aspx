<%@ Page Title="新增商品檔案" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="File_Add.aspx.cs" Inherits="_260311_hw_7Systems.System07_ProductMgmt.File_Add" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <div>
            <b>檔案名稱: &emsp;</b>
            <asp:TextBox ID="FName" runat="server" Width="100%"></asp:TextBox>
            <br /><br />
            <b>檔案說明: &emsp;</b>
            <asp:TextBox ID="FDesc" runat="server" Width="100%"></asp:TextBox>
            <br /><br />
            <p><b>檔案上傳</b></p>
            <asp:FileUpload ID="RelatedFileUpload" runat="server" />
        </div>
        <br />
        <div>
            <asp:Button ID="Submit" runat="server" Text="送出" OnClick="Submit_Click" />
        </div>
    </main>
</asp:Content>