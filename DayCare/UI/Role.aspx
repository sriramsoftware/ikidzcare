<%@ Page Title="" Language="C#" MasterPageFile="~/UI/DayCareIndex.Master" AutoEventWireup="true"
    CodeBehind="Role.aspx.cs" Inherits="DayCare.UI.Role" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="clear"></div>
<h3 class="title">
        <img src="../images/arrow.png" />&nbsp;
        Role</h3>
        <div class="clear"></div>

    <telerik:RadGrid ID="rgRoles" CssClass="RemoveBorders" runat="server" Width="100%" Height="430px" AllowPaging="true"
        AllowSorting="true" AutoGenerateColumns="false" OnNeedDataSource="rgRoles_NeedDataSource"
        PagerStyle-AlwaysVisible="true" AllowFilteringByColumn="True" GridLines="None"
        OnDeleteCommand="rgRoles_DeleteCommand" OnEditCommand="rgRoles_EditCommand" OnInsertCommand="rgRoles_InsertCommand"
        OnItemCommand="rgRoles_ItemCommand" OnItemCreated="rgRoles_ItemCreated" OnItemDataBound="rgRoles_ItemDataBound"
        OnUpdateCommand="rgRoles_UpdateCommand">
        <GroupingSettings CaseSensitive="false" />
        <HeaderContextMenu EnableEmbeddedSkins="true" />
        <PagerStyle AlwaysVisible="True" ShowPagerText="true" Mode="NextPrevAndNumeric" />
        <MasterTableView CommandItemDisplay="TopAndBottom" CommandItemSettings-AddNewRecordText="Add New Role"
            HorizontalAlign="NotSet" EditMode="InPlace" DataKeyNames="Id" Width="100%">
            <Columns>
                <telerik:GridEditCommandColumn HeaderText="" ButtonType="ImageButton" Reorderable="False"
                    UniqueName="Edit" ItemStyle-BorderStyle="None">
                    <HeaderStyle Width="5%"></HeaderStyle>
                    <ItemStyle BorderStyle="None"></ItemStyle>
                </telerik:GridEditCommandColumn>
                <telerik:GridButtonColumn CommandName="Delete" UniqueName="Delete" HeaderText=""
                    ButtonType="ImageButton" Reorderable="false" ItemStyle-BorderStyle="None" ConfirmText="Sure you want to Delete?"
                    ConfirmTitle="Delete Record" ConfirmDialogType="RadWindow">
                    <HeaderStyle Width="5%" BorderStyle="None"></HeaderStyle>
                    <ItemStyle BorderStyle="None"></ItemStyle>
                </telerik:GridButtonColumn>
                <telerik:GridTemplateColumn HeaderText="Role" UniqueName="Name">
                    <ItemTemplate>
                        <asp:Label ID="lblName" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtName" runat="server" Text='<%# Eval("Name") %>' ></asp:TextBox>
                    </EditItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridCheckBoxColumn UniqueName="Active" DataField="Active" HeaderText="Active"></telerik:GridCheckBoxColumn>
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
            <telerik:AjaxSetting AjaxControlID="rgRole">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgRole" LoadingPanelID="RAPL1"  />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <telerik:RadAjaxLoadingPanel ID="RAPL1" runat="server" Transparency="10"></telerik:RadAjaxLoadingPanel>
</asp:Content>
