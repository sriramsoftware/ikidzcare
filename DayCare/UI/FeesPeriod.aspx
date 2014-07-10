<%@ Page Title="" Language="C#" MasterPageFile="~/UI/DayCareIndex.Master" AutoEventWireup="true"
    CodeBehind="FeesPeriod.aspx.cs" Inherits="DayCare.UI.FeesPeriod" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="clear">
    </div>
    <h3 class="title">
        <img src="../images/arrow.png" />&nbsp; Fees Period</h3>
    <div class="clear">
    </div>
    <telerik:RadGrid ID="rgFeesPeriod" runat="server" AllowFilteringByColumn="false"
        AllowPaging="true" AllowSorting="true" EnableLinqExpressions="false" EnableEmbeddedSkins="true"
        EnableAjaxSkinRendering="true" ReorderColumnsOnClient="true" GridLines="None"
        AutoGenerateColumns="false" CssClass="RemoveBorders" ShowGroupPanel="false" AllowColunmReorder="True"
        Width="100%" Height="430px" BorderWidth="0px" OnNeedDataSource="rgFeesPeriod_NeedDataSource"
        OnEditCommand="rgFeesPeriod_EditCommand" OnInsertCommand="rgFeesPeriod_InsertCommand"
        OnItemCreated="rgFeesPeriod_ItemCreated" OnItemDataBound="rgFeesPeriod_ItemDataBound"
        OnUpdateCommand="rgFeesPeriod_UpdateCommand" OnItemCommand="rgFeesPeriod_ItemCommand">
        <ItemStyle Wrap="True" />
        <PagerStyle AlwaysVisible="true" Mode="NextPrevAndNumeric" ShowPagerText="true" />
        <MasterTableView DataKeyNames="Id" PagerStyle-AlwaysVisible="true" EditMode="InPlace"
            CommandItemDisplay="TopAndBottom" InsertItemDisplay="Top" TableLayout="Auto"
            AllowFilteringByColumn="true" Width="100%">
            <CommandItemSettings AddNewRecordText="Add New Fees Period" />
            <Columns>
                <telerik:GridEditCommandColumn HeaderStyle-Width="5%" ButtonType="ImageButton" Reorderable="true"
                    HeaderText="Edit" UpdateText="Edit" UniqueName="Edit">
                    <HeaderStyle Width="5%"></HeaderStyle>
                </telerik:GridEditCommandColumn>
                <telerik:GridBoundColumn DataField="Name" UniqueName="Name" HeaderText="Name" SortExpression="Name"
                    AllowFiltering="true">
                </telerik:GridBoundColumn>
                <telerik:GridCheckBoxColumn DataField="Active" UniqueName="Active" HeaderText="Active"
                    SortExpression="Active" AllowFiltering="true" HeaderTooltip="Checked True Then Active">
                </telerik:GridCheckBoxColumn>
            </Columns>
            <PagerStyle AlwaysVisible="True"></PagerStyle>
        </MasterTableView>
        <ClientSettings AllowColumnsReorder="True" AllowDragToGroup="True" ReorderColumnsOnClient="True">
            <Selecting AllowRowSelect="True" />
            <Scrolling AllowScroll="True" UseStaticHeaders="True" />
        </ClientSettings>
        <FilterMenu EnableEmbeddedSkins="true" ExpandAnimation-Type="Linear">
            <ExpandAnimation Type="Linear" />
        </FilterMenu>
    </telerik:RadGrid>
    <asp:HiddenField ID="hdnName" runat="server" />
    <telerik:RadAjaxManagerProxy ID="AjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="rgFeesPeriod">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgFeesPeriod" LoadingPanelID="RAPL1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <telerik:RadAjaxLoadingPanel ID="RAPL1" runat="server" Transparency="10">
    </telerik:RadAjaxLoadingPanel>
</asp:Content>
