<%@ Page Title="檔案管理" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="News_File_Mgmt.aspx.cs" Inherits="_260311_hw_7Systems.System01_News.News_File_Mgmt" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <div>
            <asp:Button ID="GoBack" runat="server" Text="返回消息" OnClick="GoBack_Click" />
            &emsp; &emsp;
            <asp:Button ID="AddFile" runat="server" Text="新增檔案" Visible="False" OnClick="AddFile_Click"/>
        </div>
        <br />
        <div>
            <p><b>檔案管理</b></p>
            <asp:GridView ID="FileGrid" runat="server" DataKeyNames="FileId"
                AutoGenerateColumns="False" Visible="False"
                OnRowDeleting="FileGrid_RowDeleting"
                OnRowCommand="FileGrid_RowCommand">
                <Columns>
                    <asp:CommandField ShowDeleteButton="True" />
                    <asp:BoundField DataField="FName" HeaderText="檔案名稱" />
                    <asp:BoundField DataField="FDesc" HeaderText="檔案描述" />
                    <asp:TemplateField HeaderText="下載">
                        <ItemTemplate>
                            <asp:LinkButton ID="BtnDownload" runat="server" Text="Download"
                                CommandName="DownloadFile"
                                CommandArgument='<%# CombinePath("~/Files/", Eval("FPath"))  %>' ></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </main>
</asp:Content>