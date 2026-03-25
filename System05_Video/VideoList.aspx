<%@ Page Title="影片列表" Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="VideoList.aspx.cs" Inherits="_260311_hw_7Systems.System05_Video.VideoList" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title">
        <div>
            <asp:Button ID="AddNew" runat="server" Text="新增影片" Visible="False" OnClick="AddNew_Click" />
        </div>
    </main>
</asp:Content>