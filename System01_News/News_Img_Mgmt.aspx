<%@ Page Title="圖片管理" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="News_Img_Mgmt.aspx.cs" Inherits="_260311_hw_7Systems.System01_News.News_Img_Mgmt" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <div>
            <asp:Button ID="AddImg" runat="server" Text="新增圖片" OnClick="AddImg_Click" Visible="False" />
        </div>
        <br />
        <div>
            <h3>圖片管理</h3>
            <asp:GridView ID="ImgGrid" runat="server" DataKeyNames="ImageId"
                AutoGenerateColumns="False" Visible="False" OnRowDeleting="ImgGrid_RowDeleting">
                <Columns>
                    <asp:CommandField ShowDeleteButton="True" />
                    <asp:BoundField DataField="IName" HeaderText="圖片名稱" />
                    <asp:BoundField DataField="IDesc" HeaderText="圖片描述" />
                    <asp:TemplateField HeaderText="圖片">
                        <ItemTemplate>
                            <asp:Image ID="Img" runat="server" 
                                ImageUrl='<%# CombineImgPath("~/Images/",Eval("IPath")) %>'
                                Width="100%" Height="200px"/>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </main>
</asp:Content>