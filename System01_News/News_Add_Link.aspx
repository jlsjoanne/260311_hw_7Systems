<%@ Page Title="新增連結" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="News_Add_Link.aspx.cs" Inherits="_260311_hw_7Systems.System01_News.News_Add_Link" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <p>新增連結數</p>
        <asp:DropDownList ID="AddLinkNum" runat="server" AutoPostBack="True">
            <asp:ListItem Selected="True">0</asp:ListItem>
            <asp:ListItem>1</asp:ListItem>
            <asp:ListItem>2</asp:ListItem>
            <asp:ListItem>3</asp:ListItem>
            <asp:ListItem>4</asp:ListItem>
            <asp:ListItem>5</asp:ListItem>
            <asp:ListItem>6</asp:ListItem>
            <asp:ListItem>7</asp:ListItem>
            <asp:ListItem>8</asp:ListItem>
            <asp:ListItem>9</asp:ListItem>
            <asp:ListItem>10</asp:ListItem>
    </asp:DropDownList> &nbsp;
        
        <br />
        <div>
            <asp:PlaceHolder ID="PhLink" runat="server"></asp:PlaceHolder>
        </div>
        <br />
        <div>
            <asp:Button ID="Submit" runat="server" Text="送出" OnClick="Submit_Click" />
        </div>
    </main>
</asp:Content>