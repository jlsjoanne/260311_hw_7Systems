<%@ Page Title="Edit News" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="News_Edit.aspx.cs" Inherits="_260311_hw_7Systems.System01_News.News_Edit" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <div>
            <h3>標題</h3>
            <h4>
                <asp:TextBox ID="NewsTitle" runat="server" Width="100%"></asp:TextBox>
            </h4>
        </div>
        <br />
        <div>
            <b>分類</b>
            <br />
            <b>原分類</b>
            <asp:Label ID="NCategory" runat="server"></asp:Label><br />
            <b>編輯後分類</b>
            <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="CategoryDTsource" DataTextField="CategoryName" DataValueField="CategoryId"></asp:DropDownList>
            <asp:SqlDataSource ID="CategoryDTsource" runat="server" ConnectionString="<%$ ConnectionStrings:NewsDB %>" SelectCommand="SELECT * FROM [Category] ORDER BY [CategoryOrder]"></asp:SqlDataSource>
        </div>
        <br />
        <div>
            <b>發表時間</b>
            <asp:Label ID="PostedTime" runat="server"></asp:Label>
        </div>
        <br />
        <br />
        <div>
            <b><p>內文</p></b>
            <asp:TextBox ID="NContent" runat="server"
                TextMode="MultiLine" Rows="10" Width="100%"></asp:TextBox>
        </div>
        <br />
        <div>
            <asp:Button ID="Submit" runat="server" Text="送出" OnClick="Submit_Click" />
        </div>
        
        

    </main>
</asp:Content>
