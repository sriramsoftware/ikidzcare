<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MenuLink.ascx.cs" Inherits="DayCare.UI.UserControls.MenuLink" %>
<asp:DataList ID="dlMenuLink" runat="server" OnItemDataBound="dlMenuLink_ItemDataBound">
    <ItemTemplate>
        <img src="../images/arrow.png" />&nbsp;<asp:HyperLink ID="hlMenuLink" runat="server"></asp:HyperLink>&nbsp;&nbsp;
    </ItemTemplate>
</asp:DataList>
