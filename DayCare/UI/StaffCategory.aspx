<%@ Page Title="" Language="C#" MasterPageFile="~/UI/DayCareIndex.Master" AutoEventWireup="true"
    CodeBehind="StaffCategory.aspx.cs" Inherits="DayCare.UI.StaffCategory" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="clear"></div>
<h3 class="title">
        <img src="../images/arrow.png" />&nbsp;
        Staff Category</h3>
        <div class="clear"></div>
    <telerik:RadGrid ID="rgStaffCategory" runat="server" AutoGenerateColumns="false"
        AllowFilteringByColumn="false" AllowSorting="true" AllowPaging="true" EnableLinqExpression="false"
        EnableEmbeddedSkins="true" EnableAjaxSkinRendering="true" ReorderColumnsOnClient="true"
        GridLines="None" CssClass="RemoveBorders" ShowGroupPanel="false" AllowColunmReorder="True"
        Width="100%" Height="430px" BorderWidth="0px" OnNeedDataSource="rgStaffCategory_NeedDataSource"
        OnEditCommand="rgStaffCategory_EditCommand" OnInsertCommand="rgStaffCategory_InsertCommand"
        OnItemCreated="rgStaffCategory_ItemCreated" OnItemDataBound="rgStaffCategory_ItemDataBound"
        OnUpdateCommand="rgStaffCategory_UpdateCommand" OnItemCommand="rgStaffCategory_ItemCommand">
        <GroupingSettings CaseSensitive="false" />
        <ItemStyle Wrap="True" />
        <HeaderContextMenu EnableEmbeddedSkins="true" />
        <PagerStyle AlwaysVisible="true" Mode="NextPrevAndNumeric" ShowPagerText="true" />
        <MasterTableView DataKeyNames="Id" PagerStyle-AlwaysVisible="true" EditMode="InPlace"
            CommandItemDisplay="TopAndBottom" InsertItemDisplay="Top" TableLayout="Auto"
            AllowFilteringByColumn="true" Width="100%">
            <CommandItemSettings AddNewRecordText="Add New Staff Category" />
            <Columns>
                <telerik:GridEditCommandColumn HeaderStyle-Width="5%" ButtonType="ImageButton" Reorderable="true"
                    HeaderText="Edit" UpdateText="Edit" UniqueName="Edit">
                    <HeaderStyle Width="5%"></HeaderStyle>
                </telerik:GridEditCommandColumn>
                <telerik:GridBoundColumn DataField="Name" UniqueName="Name" HeaderText="Staff Category"
                    SortExpression="Name" AllowFiltering="true">
                </telerik:GridBoundColumn>
                <%-- <telerik:GridBoundColumn DataField="Comments" UniqueName="Comments" HeaderText="Comments" SortExpression ="Comments" AllowFiltering ="true" ></telerik:GridBoundColumn>--%>
                <telerik:GridTemplateColumn HeaderText="Comments" UniqueName="Comments" AllowFiltering="false">
                    <ItemTemplate>
                        <asp:Label ID="lblComments" runat="server" Text='<%# Eval("Comments") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtComments" runat="server" Text='<%# Eval("Comments") %>' TextMode="MultiLine"></asp:TextBox>
                    </EditItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridCheckBoxColumn DataField="Active" UniqueName="Active" HeaderText="Active"
                    SortExpression="Active" AllowFiltering="true" HeaderTooltip="Checked True Then Active">
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
    <asp:HiddenField ID="hdnName" runat="server" />
    <telerik:RadAjaxManagerProxy ID="AjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="rgStaffCategory">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgStaffCategory" LoadingPanelID="RAPL1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <telerik:RadAjaxLoadingPanel ID="RAPL1" runat="server" Transparency="10">
    </telerik:RadAjaxLoadingPanel>
</asp:Content>
