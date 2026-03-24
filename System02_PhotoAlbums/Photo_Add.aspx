<%@ Page Title="新增相片" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Photo_Add.aspx.cs" Inherits="_260311_hw_7Systems.System02_PhotoAlbums.Photo_Add" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <div>
            <h2>新增相片</h2>
            <p>(可上傳多檔)</p>
            <asp:FileUpload ID="ImgUpload" runat="server" 
                AllowMultiple="True" accept="image/*" />
        </div>
        <br />
        <div>
            <asp:Button ID="Submit" runat="server" Text="下一步" OnClick="Submit_Click" />
        </div>

    </main>
</asp:Content>