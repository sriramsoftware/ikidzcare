<%@ Page Title="" Language="C#" MasterPageFile="~/UI/DayCareIndex.Master" AutoEventWireup="true"
    CodeBehind="EnrollmentStatus.aspx.cs" Inherits="DayCare.UI.EnrollmentStatus" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="clear"></div>
    <h3 class="title">
        <img src="../images/arrow.png" />&nbsp;
        Enrollment Status</h3>
        <div class="clear"></div>
    <telerik:RadGrid ID="rgEnrollmentStatus" runat="server" AutoGenerateColumns="false"
        AllowFilteringByColumn="false" AllowPaging="true" AllowSorting="true" EnableLinqExpression="false"
        EnableEmbeddedSkins="true" EnableAjaxSkinRendering="true" ReorderColumnOnClient="true"
        GridLines="None" CssClass="RemoveBorders" ShowGroupPanel="false" allowColumnReorder="True"
        Width="100%" Height="430px" BorderWidth="0px" OnNeedDataSource="rgEnrollmentStatus_NeedDataSource"
        OnEditCommand="rgEnrollmentStatus_EditCommand" OnInsertCommand="rgEnrollmentStatus_InsertCommand"
        OnItemCreated="rgEnrollmentStatus_ItemCreated" OnItemDataBound="rgEnrollmentStatus_ItemDataBound"
        OnUpdateCommand="rgEnrollmentStatus_UpdateCommand" OnItemCommand="rgEnrollmentStatus_ItemCommand">
        <ItemStyle Wrap="true" />
        <PagerStyle AlwaysVisible="true" Mode="NextPrevAndNumeric" ShowPagerText="true" />
        <MasterTableView DataKeyNames="Id" PagerStyle-AlwaysVisible="true" EditMode="InPlace"
            CommandItemDisplay="TopAndBottom" InsertItemDisplay="Top" TableLayout="Auto"
            AllowFilteringByColumn="true" Width="100%">
            <CommandItemSettings AddNewRecordText="Add New Enrollment Status" />
            <Columns>
                <telerik:GridEditCommandColumn ButtonType="ImageButton" HeaderStyle-Width="5%" Reorderable="true"
                    HeaderText="Edit" UpdateText="Edit" UniqueName="Edit">
                </telerik:GridEditCommandColumn>
                <telerik:GridBoundColumn DataField="Status" HeaderText="Status" SortExpression="Status"
                    AllowFiltering="true" UniqueName="Status">
                </telerik:GridBoundColumn>
                <telerik:GridCheckBoxColumn DataField="Active" HeaderText="Active" SortExpression="Active"
                    UniqueName="Active" HeaderTooltip="Checked True Then Active">
                </telerik:GridCheckBoxColumn>
                <telerik:GridTemplateColumn HeaderText="Comments" UniqueName="Comments" AllowFiltering="false">
                    <ItemTemplate>
                        <asp:Label ID="lblComments" runat="server" Text='<%# Eval("Comments") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtComments" runat="server" Text='<%# Eval("Comments") %>' TextMode="MultiLine"></asp:TextBox>
                    </EditItemTemplate>
                </telerik:GridTemplateColumn>
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
            <telerik:AjaxSetting AjaxControlID="rgEnrollmentStatus">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgEnrollmentStatus" LoadingPanelID="RAPL1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <telerik:RadAjaxLoadingPanel ID="RAPL1" runat="server"  Transparency="10">
    </telerik:RadAjaxLoadingPanel>
</asp:Content>
