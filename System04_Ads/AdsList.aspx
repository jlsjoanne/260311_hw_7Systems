<%@ Page Title="廣告連結" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdsList.aspx.cs" Inherits="_260311_hw_7Systems.System04_Ads.AdsList" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title">
        <div>
            <asp:Button ID="AddNew" runat="server" Text="新增廣告" OnClick="AddNew_Click" Visible="False" />
            &emsp; &emsp;
            <asp:Button ID="AdsMgmt" runat="server" Text="管理廣告" Visible="False" OnClick="AdsMgmt_Click" />
        </div>
        <div>
            <asp:Repeater ID="CategoryRepeater" runat="server" OnItemDataBound="CatRepeater_ItemDataBound">
                <ItemTemplate>
                    <p>
                        <b><asp:Label ID="AdCategory" runat="server"
                            Text='<%# Eval("CategoryName") %>'></asp:Label></b>
                    </p>
                    
                    <asp:Repeater ID="AdRepeater" runat="server" OnItemCommand="AdRepeater_ItemCommand">
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <asp:ImageButton ID="ImageBtn" runat="server"
                                        ImageUrl='<%# CombinePath("~/Images/",Eval("AdImgPath")) %>'
                                        CommandName="ToUrl"
                                        CommandArgument='<%# Eval("AdUrl") %>'
                                        AlternateText='<%# Eval("AdName") %>'
                                        Width="100%" Height="200px"
                                        OnClientClick="document.forms[0].target='_blank';"/>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <br />
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </main>
</asp:Content>
