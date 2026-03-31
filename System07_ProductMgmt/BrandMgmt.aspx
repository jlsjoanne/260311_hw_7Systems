<%@ Page Title="品牌管理" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BrandMgmt.aspx.cs" Inherits="_260311_hw_7Systems.System07_ProductMgmt.BrandMgmt" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title">
        <div>
            <asp:Button ID="AddBrand" runat="server" Text="新增品牌" OnClick="AddBrand_Click" />
        </div>
        <br />
        <div>
            <p><b>品牌列表</b></p>
            <asp:GridView ID="BrandGrid" runat="server" AutoGenerateColumns="False" DataSourceID="BrandDataSource" DataKeyNames="BrandId">
                <Columns>
                    <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
                    <asp:BoundField DataField="BrandName" HeaderText="品牌名稱" />
                </Columns>
            </asp:GridView>
            <asp:SqlDataSource ID="BrandDataSource" runat="server" 
                ConnectionString="<%$ ConnectionStrings:ProductDB %>" 
                DeleteCommand="DELETE FROM [Brand] WHERE [BrandId] = @BrandId" 
                SelectCommand="SELECT * FROM [Brand]" 
                UpdateCommand="UPDATE [Brand] SET [BrandName] = @BrandName WHERE [BrandId] = @BrandId">
                <DeleteParameters>
                    <asp:Parameter Name="BrandId" Type="Object" />
                </DeleteParameters>
                <UpdateParameters>
                    <asp:Parameter Name="BrandName" Type="String" />
                    <asp:Parameter Name="BrandId" Type="Object" />
                </UpdateParameters>
            </asp:SqlDataSource>
        </div>
    </main>
</asp:Content>

