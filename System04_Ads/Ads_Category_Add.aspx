<%@ Page Title="新增類別" Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Ads_Category_Add.aspx.cs" Inherits="_260311_hw_7Systems.System04_Ads.Ads_Category_Add" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title">
        <h3>新增類別</h3>
        <div>
            <b>類別名稱: &emsp;</b>
            <asp:TextBox ID="CName" runat="server"></asp:TextBox>
        </div>
        <br />
        <div>
            <asp:Button ID="Submit" runat="server" Text="送出" OnClick="Submit_Click" />
        </div>
    </main>
</asp:Content>