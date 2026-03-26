<%@ Page Title="影片列表" Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="VideoList.aspx.cs" Inherits="_260311_hw_7Systems.System05_Video.VideoList" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title">
        <div>
            <asp:Button ID="AddNew" runat="server" Text="新增影片" Visible="False" OnClick="AddNew_Click" />
        </div>
        <div>
            <asp:Repeater ID="CategoryRepeater" runat="server" OnItemDataBound="Category_ItemDataBound">
                <ItemTemplate>
                    <p><b>
                        <asp:Label ID="CategoryName" runat="server"
                            Text='<%# Eval("CategoryName") %>' ></asp:Label></b></p>
                    <asp:ListView ID="YTList" runat="server" OnItemCommand="YTList_ItemCommand">
                        <LayoutTemplate>
                            <div id="itemPlaceholder" runat="server"></div>
                            <asp:DataPager ID="DataPager1" runat="server" PageSize="5"></asp:DataPager>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <table>
                                <tbody>
                                    <tr>
                                        <td style="text-align: center">
                                            <asp:ImageButton ID="VideoImgBtn" runat="server"
                                                ImageUrl='<%# CombineYtImg(Eval("VideoId")) %>'
                                                CommandName="ToYt"
                                                CommandArgument='<%# CombineYtWatch(Eval("VideoId")) %>'
                                                AlternateText='<%# Eval("VideoName") %>'
                                                OnClientClick="document.forms[0].target='_blank';"/>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: center">
                                            <asp:Label ID="VideoName" runat="server" Text='<%# Eval("VideoName") %>' ></asp:Label>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </ItemTemplate>
                    </asp:ListView>
                </ItemTemplate>
            </asp:Repeater>
        </div>
        
    </main>
</asp:Content>