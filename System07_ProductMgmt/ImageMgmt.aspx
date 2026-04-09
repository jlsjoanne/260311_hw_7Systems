<%@ Page Title="商品圖片管理" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ImageMgmt.aspx.cs" Inherits="_260311_hw_7Systems.System07_ProductMgmt.ImageMgmt" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <div>
            <asp:Button ID="GoBack" runat="server" Text="返回商品" OnClick="GoBack_Click" />
            &emsp; &emsp;
            <asp:Button ID="AddNew" runat="server" Text="新增圖片" OnClick="AddNew_Click" Visible="False" />
        </div>
        <br />
        <div>
            <p><b>圖片管理</b></p>
            <asp:GridView ID="ImgGrid" runat="server" 
                AutoGenerateColumns="False" DataKeyNames="ImageId"
                OnRowDeleting="ImgGrid_RowDeleting">
                <Columns>
                    <asp:CommandField ShowDeleteButton="False" />
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