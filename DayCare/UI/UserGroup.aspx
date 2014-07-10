<%@ Page Title="" Language="C#" MasterPageFile="~/UI/DayCareIndex.Master" AutoEventWireup="true"
    CodeBehind="UserGroup.aspx.cs" Inherits="DayCare.UI.usergroup" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="clear">
    </div>
    <h3 class="title">
        <img src="../images/arrow.png" />&nbsp; User Group</h3>
    <div class="clear">
    </div>
    <telerik:RadGrid ID="rgUserGroup" runat="server" CssClass="RemoveBorders" AutoGenerateColumns="false"
        AllowFilteringByColumn="false" AllowSorting="true" AllowPaging="true" EnableLinqExpression="false"
        EnableEmbeddedSkins="true" EnableAjaxSkinRendering="true" ReorderColumnsOnClient="true"
        PagerStyle-AlwaysVisible="true" GridLines="None" ShowGroupPanel="false" AllowColunmReorder="True"
        Width="100%" Height="430px" BorderWidth="0px" OnNeedDataSource="rgUserGroup_NeedDataSource"
        OnEditCommand="rgUserGroup_EditCommand" OnInsertCommand="rgUserGroup_InsertCommand"
        OnItemCreated="rgUserGroup_ItemCreated" OnItemDataBound="rgUserGroup_ItemDataBound"
        OnUpdateCommand="rgUserGroup_UpdateCommand" OnItemCommand="rgUserGroup_ItemCommand">
        <ItemStyle Wrap="True" />
        <PagerStyle AlwaysVisible="True" ShowPagerText="true" Mode="NextPrevAndNumeric"  />
        <MasterTableView DataKeyNames="Id" PagerStyle-AlwaysVisible="true" EditMode="InPlace"
            CommandItemDisplay="TopAndBottom" InsertItemDisplay="Top" TableLayout="Auto"
            AllowFilteringByColumn="true" Width="100%">
            <CommandItemSettings AddNewRecordText="Add New User Group" />
            <Columns>
                <telerik:GridEditCommandColumn HeaderStyle-Width="5%" ButtonType="ImageButton" Reorderable="true"
                    HeaderText="Edit" UpdateText="Edit" UniqueName="Edit">
                    <HeaderStyle Width="5%"></HeaderStyle>
                </telerik:GridEditCommandColumn>
                <telerik:GridTemplateColumn HeaderText="Group Title" UniqueName="GroupTitle">
                    <ItemTemplate>
                        <asp:Label ID="lblGroupTitle" runat="server" Text='<%# Eval("GroupTitle") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtGroupTitle" runat="server" Text='<%# Eval("GroupTitle") %>'></asp:TextBox>
                    </EditItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderText="Role" UniqueName="Role">
                    <ItemTemplate>
                        <asp:Label ID="lblRoleName" runat="server" Text='<%# Eval("RoleName") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddlRole" runat="server">
                        </asp:DropDownList>
                    </EditItemTemplate>
                </telerik:GridTemplateColumn>
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
            <telerik:AjaxSetting AjaxControlID="rgUserGroup">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgUserGroup" LoadingPanelID="RAPL1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <telerik:RadAjaxLoadingPanel ID="RAPL1" runat="server" Transparency="10">
    </telerik:RadAjaxLoadingPanel>
</asp:Content>
