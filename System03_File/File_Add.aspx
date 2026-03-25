<%@ Page Title="新增下載" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="File_Add.aspx.cs" Inherits="_260311_hw_7Systems.System03_File.File_Add" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title">
        <h2>新增檔案</h2>
        <div>
            <p><b>分類</b></p>
            <asp:DropDownList ID="CategoryDropdownList" runat="server" DataSourceID="FileDataSource" DataTextField="CategoryName" DataValueField="CategoryId"></asp:DropDownList>
            <asp:SqlDataSource ID="FileDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:FilesDB %>" SelectCommand="SELECT * FROM [Category] ORDER BY [CategoryOrder]"></asp:SqlDataSource>
        </div>
        <div>
            <p><b>檔案名稱</b></p>
            <asp:TextBox ID="FileName" runat="server"></asp:TextBox>
        </div>
        <br />
        
        <div>
            <p><b>檔案描述</b></p>
            <asp:TextBox ID="FileDesc" runat="server"></asp:TextBox>
        </div>
        <br />
        <div>
            <p><b>檔案上傳</b></p>
            <asp:FileUpload ID="FileUpload1" runat="server" />
        </div>
        <br />
        <div>
            <asp:Button ID="Submit" runat="server" Text="送出" OnClick="Submit_Click" />
        </div>
    </main>
</asp:Content>

