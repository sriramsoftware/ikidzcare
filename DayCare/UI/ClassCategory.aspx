<%@ Page Title="" Language="C#" MasterPageFile="~/UI/DayCareIndex.Master" AutoEventWireup="true"
    CodeBehind="ClassCategory.aspx.cs" Inherits="DayCare.UI.ClassCategory" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="clear"></div>
  <h3 class="title">
        <img src="../images/arrow.png" />&nbsp;
        Class Category</h3>
        <div class="clear"></div>
    <telerik:RadGrid ID="rgClassCategory" runat="server" AutoGenerateColumns="false"
        AllowFilteringByColumn="false" AllowPaging="true" AllowSorting="true" EnableLinqExpression="false"
        EnableEmbeddedSkins="true" EnableAjaxSkinRendering="true" ReorderColumnOnClient="true"
        GridLines="None" CssClass="RemoveBorders" ShowGroupPanel="false" allowColumnReorder="True"
        Width="100%" Height="430px" BorderWidth="0px" OnNeedDataSource="rgClassCategory_NeedDataSource"
        OnEditCommand="rgClassCategory_EditCommand" OnInsertCommand="rgClassCategory_InsertCommand"
        OnItemCreated="rgClassCategory_ItemCreated" OnItemDataBound="rgClassCategory_ItemDataBound"
        OnUpdateCommand="rgClassCategory_UpdateCommand" OnItemCommand="rgClassCategory_ItemCommand">
        <ItemStyle Wrap="true" />
        <PagerStyle AlwaysVisible="true" Mode="NextPrevAndNumeric" ShowPagerText="true" />
        <MasterTableView DataKeyNames="Id" PagerStyle-AlwaysVisible="true" EditMode="InPlace"
            CommandItemDisplay="TopAndBottom" InsertItemDisplay="Top" TableLayout="Auto"
            AllowFilteringByColumn="true" Width="100%">
            <CommandItemSettings AddNewRecordText="Add New Class Category" />
            <Columns>
                <telerik:GridEditCommandColumn ButtonType="ImageButton" HeaderStyle-Width="5%" Reorderable="true"
                    HeaderText="Edit" UpdateText="Edit" UniqueName="Edit">
                </telerik:GridEditCommandColumn>
                <telerik:GridBoundColumn DataField="Name" HeaderText="Category Name" AllowFiltering="true"
                    SortExpression="ClassCategoryName" UniqueName="Name">
                </telerik:GridBoundColumn>
                <telerik:GridCheckBoxColumn DataField="Active" HeaderText="Active" AllowFiltering="true"
                    UniqueName="Active" SortExpression="Active" HeaderTooltip="Checked True Then Active">
                </telerik:GridCheckBoxColumn>
                <telerik:GridTemplateColumn HeaderText="Commnets" UniqueName="Comments" AllowFiltering="false">
                    <ItemTemplate>
                        <asp:Label ID="lblComments" runat="server" Text='<%# Eval("Comments") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtComments" runat="server" TextMode="MultiLine" Text='<%# Eval("Comments") %>'></asp:TextBox>
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
            <telerik:AjaxSetting AjaxControlID="rgClassCategory">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgClassCategory" LoadingPanelID="RAPL1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <telerik:RadAjaxLoadingPanel ID="RAPL1" runat="server" Transparency="10">
    </telerik:RadAjaxLoadingPanel>
</asp:Content>
