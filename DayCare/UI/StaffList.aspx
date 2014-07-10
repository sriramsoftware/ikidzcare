<%@ Page Title="" Language="C#" MasterPageFile="~/UI/DayCareIndex.Master" AutoEventWireup="true"
    CodeBehind="StaffList.aspx.cs" Inherits="DayCare.StaffList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="clear">
    </div>
    <h3 class="title">
        <img src="../images/arrow.png" />&nbsp;Staff List</h3>
    <div class="clear">
    </div>
    <telerik:RadGrid ID="rgStaffList" CssClass="RemoveBorders" runat="server" Width="990px"
        Height="800px" AllowPaging="true" AllowSorting="true" AutoGenerateColumns="false" 
        PageSize="50" OnNeedDataSource="rgStaffList_NeedDataSource" PagerStyle-AlwaysVisible="true"
        BorderWidth="0" ClientSettings-Scrolling-AllowScroll="true" AllowFilteringByColumn="True"
        GridLines="None" OnItemCreated="rgStaffList_ItemCreated" OnItemCommand="rgStaffList_ItemCommand"
        OnPreRender="rgStaffList_PreRender" OnItemDataBound="rgStaffList_ItemDataBound" EnableLinqExpressions="false">
        <GroupingSettings CaseSensitive="false"  />
        <ItemStyle Wrap="true" />
        <HeaderContextMenu EnableEmbeddedSkins="true" />
        <PagerStyle AlwaysVisible="True" ShowPagerText="true" Mode="NextPrevAndNumeric" />
        <MasterTableView CommandItemDisplay="TopAndBottom" TableLayout="Auto" CommandItemSettings-AddNewRecordText="Add New Staff"
            DataKeyNames="Id" Width="100%" PagerStyle-Position="TopAndBottom"  >
            <Columns>
                <telerik:GridEditCommandColumn HeaderStyle-Width="5%" HeaderText="Edit" ButtonType="ImageButton"
                    Reorderable="False" UniqueName="Edit" ItemStyle-BorderStyle="None">
                    <HeaderStyle Width="5%"></HeaderStyle>
                </telerik:GridEditCommandColumn>
                <telerik:GridTemplateColumn UniqueName="StaffImage" HeaderText="Image" AllowFiltering="false">
                    <ItemTemplate>
                        <asp:Image ID="imgStaff" runat="server" Height="32" Width="32" />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn HeaderText="Name" UniqueName="FullName" DataField="FullName"
                    HeaderStyle-Width="15%">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Staff Category" UniqueName="StaffCategoryName"
                    DataField="StaffCategoryName" HeaderStyle-Width="15%">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="User Group" UniqueName="UserGroupTitle" DataField="UserGroupTitle"
                    HeaderStyle-Width="15%">
                </telerik:GridBoundColumn>
                <%--                <telerik:GridBoundColumn HeaderText="City" UniqueName="City" DataField="City">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="State" UniqueName="StateName" DataField="StateName"
                    AllowFiltering="false">
                </telerik:GridBoundColumn>--%>
                <telerik:GridBoundColumn HeaderText="Phone" UniqueName="" DataField="MainPhone" AllowFiltering="false">
                </telerik:GridBoundColumn>
                <%--   <telerik:GridBoundColumn HeaderText="Email" UniqueName="Email" DataField="Email"
                    AllowFiltering="false">
                </telerik:GridBoundColumn>--%>
                <telerik:GridBoundColumn HeaderText="Username" UniqueName="UserName" DataField="UserName"
                    AllowFiltering="false">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Code" UniqueName="Code" DataField="Code" Visible="false"
                    AllowFiltering="false">
                </telerik:GridBoundColumn>
                <%--<telerik:GridTemplateColumn HeaderText="Active" UniqueName="Status" Reorderable="false">
                    <ItemTemplate>
                        <%# Eval("Active").ToString().ToLower()=="true"?"Active":"Inactive" %>
                        <%--<asp:Label ID="lblCode" runat="server" Text='<%# Eval("Code") %>'></asp:Label>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>--%>
                 <telerik:GridCheckBoxColumn HeaderText="Status" UniqueName="Status" DataField="Active" AllowFiltering="true"
                    DataType="System.Boolean" CurrentFilterFunction="EqualTo" CurrentFilterValue="True" >
                </telerik:GridCheckBoxColumn>
              <%--  <telerik:GridTemplateColumn HeaderText="Active" UniqueName="Status" Reorderable="false"
                    AllowFiltering="true" DataType="System.Boolean" >
                    <ItemTemplate>
                        <asp:CheckBox ID="Status" runat="server" Checked='<%# Eval("Active") %>' />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>--%>
            </Columns>
        </MasterTableView>
        <ClientSettings AllowColumnsReorder="True" AllowDragToGroup="True" ReorderColumnsOnClient="True">
            <Selecting AllowRowSelect="True" UseClientSelectColumnOnly="true" />
            <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="True" />
        </ClientSettings>
        <FilterMenu EnableEmbeddedSkins="true" ExpandAnimation-Type="Linear">
            <ExpandAnimation Type="Linear" />
        </FilterMenu>
    </telerik:RadGrid>
    <asp:HiddenField ID="hdnName" runat="server" />
    <telerik:RadAjaxManagerProxy ID="AjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="rgStaffList">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgStaffList" LoadingPanelID="RAPL1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <telerik:RadAjaxLoadingPanel ID="RAPL1" runat="server" Transparency="10">
    </telerik:RadAjaxLoadingPanel>
</asp:Content>
