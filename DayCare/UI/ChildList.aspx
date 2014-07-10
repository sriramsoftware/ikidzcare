<%@ Page Title="" Language="C#" MasterPageFile="~/UI/DayCareIndex.Master" AutoEventWireup="true"
    CodeBehind="ChildList.aspx.cs" Inherits="DayCare.UI.ChildList" %>

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
    <telerik:RadGrid ID="rgChildList" runat="server" CssClass="RemoveBorders" Width="100%"
        Height="950px" AllowPaging="true" AllowSorting="true" AutoGenerateColumns="false"
        OnNeedDataSource="rgChildList_NeedDataSource" PagerStyle-AlwaysVisible="true"
        EnableLinqExpressions="false" ClientSettings-AllowDragToGroup="false" ClientSettings-AllowAutoScrollOnDragDrop="false"
        AutoGenerateEditColumn="false" PageSize="50" BorderWidth="0" ClientSettings-Scrolling-AllowScroll="true"
        AllowFilteringByColumn="True" GridLines="None" OnItemCommand="rgChildList_ItemCommand"
        OnItemDataBound="rgChildList_ItemDataBound" OnItemCreated="rgChildList_ItemCreated"
        OnPreRender="rgChildList_PreRender">
        <GroupingSettings CaseSensitive="false" />
        <ItemStyle Wrap="true" />
        <HeaderContextMenu EnableEmbeddedSkins="true" />
        <PagerStyle AlwaysVisible="True" ShowPagerText="true" Mode="NextPrevAndNumeric" Position="Top" />
        <MasterTableView CommandItemDisplay="None" TableLayout="Auto" DataKeyNames="ChildDataId"
            EditFormSettings-EditColumn-Display="false" AllowAutomaticInserts="false" Width="100%"
            AllowAutomaticUpdates="false">
            <Columns>
                <telerik:GridEditCommandColumn HeaderStyle-Width="5%" HeaderText="Edit" ButtonType="ImageButton"
                    Reorderable="False" UniqueName="Edit" ItemStyle-BorderStyle="None">
                    <HeaderStyle Width="5%"></HeaderStyle>
                </telerik:GridEditCommandColumn>
                <telerik:GridTemplateColumn UniqueName="ChildImage" HeaderText="Image" AllowFiltering="false">
                    <ItemTemplate>
                        <asp:Image ID="imgChild" runat="server" Height="32" Width="32" />
                    </ItemTemplate>
                    <HeaderStyle Width="15%"></HeaderStyle>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="FullName" AllowFiltering="true" AllowSorting="true"
                    SortExpression="FullName" UniqueName="FullName" HeaderText="Child Name">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="ChildFamilyId" AllowFiltering="true" AllowSorting="true"
                    SortExpression="true" UniqueName="ChildFamilyId" Visible="false">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="FamilyName" AllowFiltering="true" AllowSorting="true"
                    SortExpression="FamilyName" HeaderText="Family Name">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Email" AllowFiltering="false" AllowSorting="false"
                    HeaderText="Email">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="HomePhone" AllowFiltering="false" AllowSorting="false"
                    HeaderText="Phone">
                </telerik:GridBoundColumn>
                <telerik:GridCheckBoxColumn HeaderText="Status" UniqueName="Status" DataField="Active"
                    AllowFiltering="true" DataType="System.Boolean" CurrentFilterFunction="EqualTo"
                    CurrentFilterValue="True">
                </telerik:GridCheckBoxColumn>
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
    <telerik:RadAjaxManagerProxy ID="AjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="rgChildList">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgChildList" LoadingPanelID="RAPL1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <telerik:RadAjaxLoadingPanel ID="RAPL1" runat="server" Transparency="10">
    </telerik:RadAjaxLoadingPanel>
    <asp:HiddenField ID="hdnName" runat="server" />
</asp:Content>
