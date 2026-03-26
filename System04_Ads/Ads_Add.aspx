<%@ Page Title="新增廣告" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Ads_Add.aspx.cs" Inherits="_260311_hw_7Systems.System04_Ads.Ads_Add" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title">
        <h2>新增廣告</h2>
        <div>
            <p><b>分類</b></p>
            <asp:DropDownList ID="AddDropDown" runat="server" DataSourceID="AdsDataSource" DataTextField="CategoryName" DataValueField="CategoryId"></asp:DropDownList>
            <asp:SqlDataSource ID="AdsDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:AdsDB %>" SelectCommand="SELECT * FROM [Category] ORDER BY [CategoryOrder] ASC"></asp:SqlDataSource>
        </div>
        <br />
        <div>
            <p><b>廣告名稱</b></p>
            <asp:TextBox ID="AdsName" runat="server"></asp:TextBox>
        </div>
        <br />
        <div>
            <p><b>廣告連結</b></p>
            <asp:TextBox ID="AdsUrl" runat="server"></asp:TextBox>
        </div>
        <br />
        <div>
            <p><b>上傳圖片</b></p>
            <asp:FileUpload ID="ImgUpload" runat="server" accept="image/*" />
        </div>
        <br />
        <div>
            <asp:Button ID="Submit" runat="server" Text="送出" OnClick="Submit_Click" />
        </div>
    </main>
</asp:Content>
