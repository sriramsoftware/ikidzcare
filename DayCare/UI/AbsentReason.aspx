<%@ Page Title="" Language="C#" MasterPageFile="~/UI/DayCareIndex.Master" AutoEventWireup="true"
    CodeBehind="AbsentReason.aspx.cs" Inherits="DayCare.UI.AbsentReason" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="clear">
    </div>
    <h3 class="title">
        <img src="../images/arrow.png" />&nbsp; Absent Reason</h3>
    <div class="clear">
    </div>
    <telerik:RadGrid ID="rgAbsentReason" runat="server" AutoGenerateColumns="false" AllowFilteringByColumn="false"
        AllowPaging="true" AllowSorting="true" EnableLinqExpression="false" EnableEmbeddedSkins="true"
        EnableAjaxSkinRendering="true" ReorderColumnOnClient="true" GridLines="None"
        CssClass="RemoveBorders" ShowGroupPanel="false" allowColumnReorder="True" Width="100%"
        Height="430px" BorderWidth="0px" OnNeedDataSource="rgAbsentReason_NeedDataSource"
        OnEditCommand="rgAbsentReason_EditCommand" OnInsertCommand="rgAbsentReason_InsertCommand"
        OnItemCreated="rgAbsentReason_ItemCreated" OnItemDataBound="rgAbsentReason_ItemDataBound"
        OnUpdateCommand="rgAbsentReason_UpdateCommand" OnItemCommand="rgAbsentReason_ItemCommand">
        <ItemStyle Wrap="true" />
        <PagerStyle AlwaysVisible="true" Mode="NextPrevAndNumeric" ShowPagerText="true" />
        <MasterTableView DataKeyNames="Id" PagerStyle-AlwaysVisible="true" EditMode="InPlace"
            CommandItemDisplay="TopAndBottom" InsertItemDisplay="Top" TableLayout="Auto"
            AllowFilteringByColumn="true" Width="100%">
            <CommandItemSettings AddNewRecordText="Add New Absent Reason" />
            <Columns>
                <telerik:GridEditCommandColumn ButtonType="ImageButton" HeaderStyle-Width="5%" Reorderable="true"
                    HeaderText="Edit" UpdateText="Edit" UniqueName="Edit">
                </telerik:GridEditCommandColumn>
                <telerik:GridTemplateColumn HeaderText="Reason" UniqueName="Reason" AllowFiltering="true"
                    SortExpression="Reason">
                    <ItemTemplate>
                        <asp:Label ID="lblReason" runat="server" Text='<%# Eval("Reason") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtReason" runat="server" Text='<%# Eval("Reason") %>' TextMode="MultiLine"></asp:TextBox>
                    </EditItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridCheckBoxColumn DataField="BillingAffected" HeaderText="Billing Affected"
                    UniqueName="BillingAffected" SortExpression="BillingAffected" AllowFiltering="true">
                </telerik:GridCheckBoxColumn>
                <telerik:GridCheckBoxColumn DataField="Active" HeaderText="Active" UniqueName="Active"
                    SortExpression="Active" AllowFiltering="true" HeaderTooltip="Checked true then Active">
                </telerik:GridCheckBoxColumn>
                <telerik:GridTemplateColumn HeaderText="Comments" UniqueName="Comments" AllowFiltering="false"
                    SortExpression="Comments">
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
            <telerik:AjaxSetting AjaxControlID="rgAbsentReason">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgAbsentReason" LoadingPanelID="RAPL1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <telerik:RadAjaxLoadingPanel ID="RAPL1" runat="server" Transparency="10">
    </telerik:RadAjaxLoadingPanel>
</asp:Content>
