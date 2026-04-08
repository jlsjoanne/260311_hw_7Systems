<%@ Page Title="檔案下載系統" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FileList.aspx.cs" Inherits="_260311_hw_7Systems.System03_File.FileList" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title">
        <div>
            <asp:Button ID="AddNew" runat="server" Text="新增檔案" OnClick="AddNew_Click" Visible="False"/>
        </div>
        <div>
            <asp:GridView ID="FileGrid" runat="server" DataKeyNames="FileId"
                AllowPaging="True" PageSize="5" OnPageIndexChanging="FileGridPaging_PageIndexChanging"
                OnRowCommand="FileGrid_RowCommand" OnRowDeleting="FileGrid_RowDeleting" AutoGenerateColumns="False">
                <Columns>
                    <asp:CommandField ShowDeleteButton="False" />
                    <asp:BoundField HeaderText="分類" DataField="CategoryName"/>
                    <asp:TemplateField HeaderText="檔案名稱">
                        <ItemTemplate>
                            <asp:LinkButton ID="FileDownload" runat="server"
                                Text='<%# Eval("FileTitle") %>'
                                CommandName="Download"
                                CommandArgument='<%# CombinePath("~/Files/", Eval("FilePath")) %>' ></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:BoundField HeaderText="檔案說明" DataField="FileDesc" />
                    <asp:BoundField HeaderText="上傳時間" DataField="UploadDate" />

                </Columns>

            </asp:GridView>
        </div>
    </main>
</asp:Content>
