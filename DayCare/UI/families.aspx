<%@ Page Title="" Language="C#" MasterPageFile="~/UI/DayCareIndex.Master" AutoEventWireup="true"
    CodeBehind="families.aspx.cs" Inherits="DayCare.UI.families" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/UI/UserControls/MenuLink.ascx" TagName="UCMenuLink" TagPrefix="ucl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h3 class="title">
        <ucl:UCMenuLink ID="usrMenuLink" runat="server" />
    </h3>
    <div class="clear">
    </div>
    <asp:ValidationSummary ID="valSum" runat="server" ValidationGroup="Familyvalidate"
        ShowMessageBox="true" ShowSummary="false" />
    <telerik:RadGrid ID="rgChildFamily" runat="server" CssClass="RemoveBorders" Width="100%"
        Height="950px" AllowPaging="true" AllowSorting="true" AutoGenerateColumns="false"
        OnNeedDataSource="rgChildFamily_NeedDataSource" PagerStyle-AlwaysVisible="true"
        ClientSettings-AllowDragToGroup="false" ClientSettings-AllowAutoScrollOnDragDrop="false"
        AutoGenerateEditColumn="false" PageSize="25" BorderWidth="0" ClientSettings-Scrolling-AllowScroll="true"
        AllowFilteringByColumn="True" OnEditCommand="rgChildFamily_EditCommand" GridLines="None"
        OnItemCommand="rgChildFamily_ItemCommand" OnItemDataBound="rgChildFamily_ItemDataBound"
        OnItemCreated="rgChildFamily_ItemCreated">
        <GroupingSettings CaseSensitive="false" />
        <ItemStyle Wrap="true" />
        <HeaderContextMenu EnableEmbeddedSkins="true" />
        <PagerStyle AlwaysVisible="True" ShowPagerText="true" Mode="NextPrevAndNumeric" Position="Top" />
        <MasterTableView CommandItemDisplay="None" TableLayout="Auto" CommandItemSettings-AddNewRecordText="Add New Family"
            DataKeyNames="Id" EditFormSettings-EditColumn-Display="false" AllowAutomaticInserts="false"
            Width="100%" AllowAutomaticUpdates="false">
            <Columns>
                <telerik:GridEditCommandColumn HeaderStyle-Width="5%" HeaderText="" ButtonType="ImageButton"
                    Reorderable="False" UniqueName="Edit" ItemStyle-BorderStyle="None">
                    <HeaderStyle Width="5%"></HeaderStyle>
                </telerik:GridEditCommandColumn>
                <telerik:GridBoundColumn DataField="FamilyTitle" AllowFiltering="true" AllowSorting="true"
                    UniqueName="FamilyTitle" SortExpression="true" HeaderText="Guardian">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="UserName" AllowFiltering="true" AllowSorting="true"
                    HeaderText="Username" SortExpression="true">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Code" AllowFiltering="false" AllowSorting="false"
                    HeaderText="CheckIn/Out Code" Visible="false">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Email" AllowFiltering="false" AllowSorting="false"
                    HeaderText="Email">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="HomePhone" AllowFiltering="false" AllowSorting="false"
                    HeaderText="Phone">
                </telerik:GridBoundColumn>
            </Columns>
        </MasterTableView>
        <ClientSettings AllowColumnsReorder="True" AllowDragToGroup="True" ReorderColumnsOnClient="True">
            <Selecting AllowRowSelect="True" />
            <Scrolling AllowScroll="True" UseStaticHeaders="True" />
        </ClientSettings>
        <FilterMenu EnableEmbeddedSkins="true" ExpandAnimation-Type="Linear">
            <ExpandAnimation Type="Linear" />
        </FilterMenu>
    </telerik:RadGrid>
    <telerik:RadAjaxLoadingPanel ID="RAPL1" runat="server" Transparency="10">
    </telerik:RadAjaxLoadingPanel>
    <asp:HiddenField ID="hdnName" runat="server" />
</asp:Content>
