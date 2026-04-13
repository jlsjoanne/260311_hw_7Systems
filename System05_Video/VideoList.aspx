<%@ Page Title="影片列表" Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="VideoList.aspx.cs" Inherits="_260311_hw_7Systems.System05_Video.VideoList" %>



<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <script type="module" src="https://cdn.jsdelivr.net/npm/@justinribeiro/lite-youtube@1.5.0/lite-youtube.js"></script>
    <main aria-labelledby="title">
        <div>
            <asp:Button ID="Mgmt" runat="server" Text="影片管理" Visible="False" OnClick="Mgmt_Click" />
            &emsp; &emsp;
            <asp:Button ID="AddNew" runat="server" Text="新增影片" Visible="False" OnClick="AddNew_Click" />
        </div>
        <br />
        <div>
            <asp:Repeater ID="CategoryRepeater" runat="server" OnItemDataBound="Category_ItemDataBound">
                <ItemTemplate>
                    <p><b>
                        <asp:Label ID="CategoryName" runat="server" Text='<%# Eval("CategoryName") %>' >

                        </asp:Label>
                    </b></p>

                    <asp:ListView ID="YtList" runat="server">
                        <ItemTemplate>
                            <div style="display: inline-block; width: 300px; height:169px">
                                <lite-youtube videoid='<%# Eval("VideoId") %>' >
                                    <a class="lite-youtube-fallback" href='<%# CombineYtWatch(Eval("VideoId")) %>' ></a>
                                </lite-youtube>
                            </div>
                        </ItemTemplate>
                    </asp:ListView>
                </ItemTemplate>
            </asp:Repeater>
        </div>

    </main>
</asp:Content>