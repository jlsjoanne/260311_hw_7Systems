<%@ Page Title="新增影片分類" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Video_Category_Add.aspx.cs" Inherits="_260311_hw_7Systems.System05_Video.Video_Category_Add" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <h3>新增影片分類</h3>
        <div>
            <p><b>分類名稱</b></p>
            <asp:TextBox ID="CategoryName" runat="server"></asp:TextBox>
        </div>
        <br />
        <div>
            <asp:Button ID="Submit" runat="server" Text="送出" OnClick="Submit_Click" />
        </div>
    </main>
</asp:Content>
