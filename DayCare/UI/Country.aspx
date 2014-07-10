<%@ Page Title="" Language="C#" MasterPageFile="~/UI/DayCareIndex.Master" AutoEventWireup="true"
    CodeBehind="Country.aspx.cs" Inherits="DayCare.UI.Country" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <div class="clear"></div>
    <h3 class="title">
        Country</h3>
        <div class="clear"></div>
    <telerik:RadGrid ID="rgCountries" runat="server" AllowPaging="true" AllowSorting="true"
        AutoGenerateColumns="false" OnNeedDataSource="rgCountries_NeedDataSource" OnEditCommand="rgCountries_EditCommand"
        OnInsertCommand="rgCountries_InsertCommand" OnItemCreated="rgCountries_ItemCreated"
        Width="100%" Height="430px" OnItemDataBound="rgCountries_ItemDataBound" OnUpdateCommand="rgCountries_UpdateCommand">
        <ItemStyle Wrap="True" />
        <PagerStyle AlwaysVisible="True" Mode="NextPrevAndNumeric" ShowPagerText="true" />
        <MasterTableView DataKeyNames="Id" CommandItemDisplay="TopAndBottom" CommandItemSettings-AddNewRecordText="Add New Coountry"
            PagerStyle-AlwaysVisible="true" EditMode="InPlace" InsertItemDisplay="Top" AllowFilteringByColumn="true">
            <Columns>
                <telerik:GridEditCommandColumn HeaderStyle-Width="25%" ButtonType="ImageButton" Reorderable="true"
                    UpdateText="Edit" UniqueName="Edit">
                    <HeaderStyle Width="25%"></HeaderStyle>
                    <ItemStyle BorderStyle="None"></ItemStyle>
                </telerik:GridEditCommandColumn>
                <telerik:GridBoundColumn HeaderText="Country" UniqueName="Name" DataField="Name"
                    SortExpression="Name">
                    <HeaderStyle Width="25%"></HeaderStyle>
                    <ItemStyle BorderStyle="None"></ItemStyle>
                </telerik:GridBoundColumn>
            </Columns>
            <EditFormSettings>
                <FormTableItemStyle Wrap="False"></FormTableItemStyle>
                <FormCaptionStyle></FormCaptionStyle>
                <FormMainTableStyle GridLines="None" CellSpacing="0" CellPadding="3" BackColor="White"
                    Width="100%" />
                <FormTableStyle CellSpacing="0" CellPadding="2" Height="110px" BackColor="White" />
                <FormTableAlternatingItemStyle Wrap="False"></FormTableAlternatingItemStyle>
                <EditColumn ButtonType="ImageButton" InsertText="Insert Order" UpdateText="Update record"
                    UniqueName="EditCommandColumn1" CancelText="Cancel edit">
                </EditColumn>
                <FormTableButtonRowStyle HorizontalAlign="right"></FormTableButtonRowStyle>
            </EditFormSettings>
        </MasterTableView>
        <ClientSettings AllowColumnHide="false" AllowColumnsReorder="false" AllowDragToGroup="false"
            ReorderColumnsOnClient="false" ColumnsReorderMethod="Reorder">
            <Selecting AllowRowSelect="false" />
            <Resizing ClipCellContentOnResize="false" AllowColumnResize="false" ResizeGridOnColumnResize="false" />
            <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true" ScrollHeight="100%" />
        </ClientSettings>
    </telerik:RadGrid>
    <asp:HiddenField ID="hdnName" runat="server" />
    <telerik:RadAjaxManagerProxy ID="AjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="rgCountries">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgCountries" LoadingPanelID="RAPL1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <telerik:RadAjaxLoadingPanel ID="RAPL1" runat="server">
    </telerik:RadAjaxLoadingPanel>
</asp:Content>
