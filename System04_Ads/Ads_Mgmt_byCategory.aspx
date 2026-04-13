<%@ Page Title="廣告管理" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Ads_Mgmt_byCategory.aspx.cs" Inherits="_260311_hw_7Systems.System04_Ads.Ads_Mgmt_byCategory" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <div>
            <asp:Button ID="GoBack" runat="server" Text="返回類別管理" OnClick="GoBack_Click" />
            &emsp; &emsp;
            <asp:Button ID="AddNew" runat="server" Text="新增廣告" OnClick="AddNew_Click" />
        </div>
        <br />
        <div>
            <asp:GridView ID="AdsGrid" runat="server" 
                AutoGenerateColumns="False" DataKeyNames="AdId"
                OnRowDeleting="AdsGrid_RowDeleting"
                OnRowEditing="AdsGrid_RowEditing"
                OnRowUpdating="AdsGrid_RowUpdating"
                OnRowCancelingEdit="AdsGrid_RowCancelingEdit">
                <Columns>
                    <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
                    <asp:BoundField DataField="AdName" HeaderText="廣告名稱" />
                    <asp:BoundField DataField="AdUrl" HeaderText="廣告連結" />
                    <asp:TemplateField HeaderText="廣告圖片">
                        <ItemTemplate>
                            <asp:Image ID="AdsImg" runat="server"
                                ImageUrl='<%# CombinePath("~/Images/", Eval("AdImgPath")) %>'
                                Height="200px" Width="100%"/>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </main>
</asp:Content>