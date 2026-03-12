<%@ Page Title="Add News" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="News_Add.aspx.cs" Inherits="_260311_hw_7Systems.System01_News.News_Add" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title">
        <div>
            <h2><b>新增消息</b></h2>
            <h3>標題</h3>
            <p>(字數限制: 50字)</p>
            <asp:TextBox ID="NewTitle" runat="server" Width="100%"></asp:TextBox>
            <br />
            <br />
            <h4>內文</h4>
            <p>(字數限制: 1000字)</p>
            <asp:TextBox ID="Content" runat="server"
                TextMode="MultiLine" Rows="10" Width="100%"></asp:TextBox>
        </div>
        <br />
        <div>
            <h4>上傳圖片 (可上傳多圖)</h4>
            <asp:FileUpload ID="ImageUpload1" runat="server" AllowMultiple="True"/>
            <asp:Button ID="ImageUploadBtn" runat="server" Text="上傳" />
            <br />
            <asp:Label ID="ImgUploadStatus" runat="server"></asp:Label>
        </div>
        <br />
        <div>
            <h4>上傳檔案 (可上傳多檔)</h4>
            <asp:FileUpload ID="FileUpload1" runat="server" AllowMultiple="True" />
            <asp:Button ID="FileUploadBtn" runat="server" Text="上傳" />
            <br />
            <asp:Label ID="FileUploadStatus" runat="server"></asp:Label>
        </div>
        <br />
        <div>
            <h4>新增連結</h4>
            <asp:Button ID="AddLink" runat="server" Text="新增" OnClick="AddLink_Click" />
            <br />
            <asp:PlaceHolder ID="PrLink" runat="server"></asp:PlaceHolder>
        </div>
        <br />
        <div>
            <asp:Button ID="Submit" runat="server" Text="送出" />
        </div>
    </main>
</asp:Content>
