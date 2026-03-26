<%@ Page Title="新增影片" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Video_Add.aspx.cs" Inherits="_260311_hw_7Systems.System05_Video.Video_Add" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title">
        <h2>新增影片</h2>
        <div>
            <p><b>分類</b></p>
            <asp:DropDownList ID="CategoryDropDown" runat="server" DataSourceID="VideoDataSource" DataTextField="CategoryName" DataValueField="CategoryId"></asp:DropDownList>
            <asp:SqlDataSource ID="VideoDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:VideoDB %>" SelectCommand="SELECT * FROM [Category] ORDER BY [CategoryOrder]"></asp:SqlDataSource>
        </div>
        <br />
        <div>
            <p><b>影片標題</b></p>
            <asp:TextBox ID="VideoName" runat="server" Width="100%"></asp:TextBox>
        </div>
        <br />
        <div>
            <p><b>Youtube影片ID</b></p>
            <asp:TextBox ID="VideoId" runat="server" Width="100%"></asp:TextBox>
        </div>
        <br />
        <div>
            <asp:Button ID="Submit" runat="server" Text="送出" OnClick="Submit_Click" />
        </div>
    </main>
</asp:Content>