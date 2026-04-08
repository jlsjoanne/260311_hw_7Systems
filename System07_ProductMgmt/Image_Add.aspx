<%@ Page Title="新增商品圖片" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Image_Add.aspx.cs" Inherits="_260311_hw_7Systems.System07_ProductMgmt.Image_Add" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <div>
            <b>圖片名稱: &emsp;</b>
            <asp:TextBox ID="ImgName" runat="server" Width="100%"></asp:TextBox>
            <br /><br />
            <b>圖片說明: &emsp;</b>
            <asp:TextBox ID="ImgDesc" runat="server" Width="100%"></asp:TextBox>
            <br /><br />
            <p><b>檔案上傳</b></p>
            <asp:FileUpload ID="ImgFileUpload" runat="server"
                accept="image/*" />
        </div>
        <br />
        <div>
            <asp:Button ID="Submit" runat="server" Text="送出" OnClick="Submit_Click" />
        </div>
    </main>
</asp:Content>