<%@ Page Title="" Language="C#" MasterPageFile="~/UI/DayCareIndex.Master" AutoEventWireup="true"
    CodeBehind="State.aspx.cs" Inherits="DayCare.UI.State" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="clear"></div>
<h3 class="title">
        <img src="../images/arrow.png" />&nbsp;
       State</h3>
        <div class="clear"></div>
        <telerik:RadGrid ID="rgStates" runat="server" AllowPaging="true" AllowSorting="true"
        AutoGenerateColumns="false" OnNeedDataSource="rgStates_NeedDataSource" OnEditCommand="rgStates_EditCommand"
        OnInsertCommand="rgStates_InsertCommand" OnItemCreated="rgStates_ItemCreated"
        Width="100%" Height="330" OnItemDataBound="rgStates_ItemDataBound" OnUpdateCommand="rgStates_UpdateCommand">
        <GroupingSettings CaseSensitive="false" />
        <HeaderContextMenu EnableEmbeddedSkins="true" />
        <PagerStyle AlwaysVisible="True" ShowPagerText="true" Mode="NextPrevAndNumeric" />
        <MasterTableView DataKeyNames="Id" CommandItemDisplay="TopAndBottom" CommandItemSettings-AddNewRecordText="Add New State"
            PagerStyle-AlwaysVisible="true" EditMode="InPlace" InsertItemDisplay="Top" AllowFilteringByColumn="true"
            HorizontalAlign="NotSet">
            <Columns>
                <telerik:GridEditCommandColumn HeaderStyle-Width="25%" ButtonType="ImageButton" Reorderable="true"
                    UpdateText="Edit" UniqueName="Edit">
                    <HeaderStyle Width="5%"></HeaderStyle>
                    <ItemStyle BorderStyle="None"></ItemStyle>
                </telerik:GridEditCommandColumn>
                <telerik:GridTemplateColumn HeaderText="State" UniqueName="Name" SortExpression="Name" AllowFiltering="true">
                    <ItemTemplate>
                        <asp:Label ID="lblName" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtState" runat="server" Text='<%# Eval("Name") %>'></asp:TextBox>
                    </EditItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="CountryName" HeaderText="Country Name" AllowFiltering="true">
                    <ItemTemplate>
                        <asp:Label ID="lblCountryName" runat="server" Text='<%# Eval("CountryName") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddlCountry" runat="server">
                        </asp:DropDownList>
                    </EditItemTemplate>
                    <HeaderStyle Width="25%"></HeaderStyle>
                    <ItemStyle BorderStyle="None"></ItemStyle>
                </telerik:GridTemplateColumn>
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
            <telerik:AjaxSetting AjaxControlID="rgStates">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgStates" LoadingPanelID="RAPL1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <telerik:RadAjaxLoadingPanel ID="RAPL1" runat="server">
    </telerik:RadAjaxLoadingPanel>
</asp:Content>
