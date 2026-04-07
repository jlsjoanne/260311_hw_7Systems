<%@ Page Title="新增消息" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="News_Add.aspx.cs" Inherits="_260311_hw_7Systems.System01_News.News_Add" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title">
        <div>
            <h2><b>新增消息</b></h2>
            <h3>標題</h3>
            <p>(字數限制: 50字)</p>
            <asp:TextBox ID="NewTitle" runat="server" Width="100%"></asp:TextBox>
        </div>
        <br />
        <div>
            <h4>分類</h4>
            <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="SqlDataSource2" DataTextField="CategoryName" DataValueField="CategoryId"></asp:DropDownList>
            <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:NewsDB %>" SelectCommand="SELECT * FROM [Category] ORDER BY [CategoryOrder]"></asp:SqlDataSource>
        </div>
        <br />
        <div>
            <h4>內文</h4>
            <p>(字數限制: 1000字)</p>
            <asp:TextBox ID="Content" runat="server"
                TextMode="MultiLine" Rows="10" Width="100%"></asp:TextBox>
        </div>
        <br />
        <div>
            <asp:Button ID="Submit" runat="server" Text="送出" OnClick="Submit_Click" />
        </div>
    </main>
</asp:Content>
