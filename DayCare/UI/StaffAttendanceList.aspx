<%@ Page Title="" Language="C#" MasterPageFile="~/UI/DayCareIndex.Master" AutoEventWireup="true"
    CodeBehind="StaffAttendanceList.aspx.cs" Inherits="DayCare.UI.StaffAttendanceList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h3 class="title">
        <img src="../images/arrow.png" />&nbsp;Staff List</h3>
    <telerik:RadGrid ID="rgStaffAttendanceList" CssClass="RemoveBorders" runat="server"
        Width="990px" Height="800px" AllowPaging="true" AllowSorting="true" AutoGenerateColumns="false"
        PageSize="50" OnNeedDataSource="rgStaffAttendanceList_NeedDataSource" PagerStyle-AlwaysVisible="true"
        BorderWidth="0" ClientSettings-Scrolling-AllowScroll="true" AllowFilteringByColumn="True"
        GridLines="None" OnItemCreated="rgStaffAttendanceList_ItemCreated" OnItemCommand="rgStaffAttendanceList_ItemCommand"
        OnItemDataBound="rgStaffAttendanceList_ItemDataBound">
        <GroupingSettings CaseSensitive="false" />
        <ItemStyle Wrap="true" />
        <HeaderContextMenu EnableEmbeddedSkins="true" />
        <PagerStyle AlwaysVisible="True" ShowPagerText="true" Mode="NextPrevAndNumeric" Position="Top" />
        <MasterTableView TableLayout="Auto" DataKeyNames="StaffSchoolYearId" Width="100%">
            <Columns>
                <telerik:GridEditCommandColumn HeaderText="Edit" HeaderStyle-Width="5%" ButtonType="ImageButton"
                    Reorderable="False" UniqueName="Edit" ItemStyle-BorderStyle="None">
                    <HeaderStyle Width="5%"></HeaderStyle>
                </telerik:GridEditCommandColumn>
                <telerik:GridTemplateColumn UniqueName="StaffImage" HeaderText="Image" AllowFiltering="false">
                    <ItemTemplate>
                        <asp:Image ID="imgStaff" runat="server" Height="32" Width="32" />
                    </ItemTemplate>
                    <HeaderStyle Width="15%"></HeaderStyle>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn HeaderText="Name" UniqueName="FullName" DataField="FullName">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="SchoolYearId" UniqueName="SchoolYearId" DataField="SchoolYearId"
                    Visible="false">
                </telerik:GridBoundColumn>
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
            <telerik:AjaxSetting AjaxControlID="rgStaffAttendanceList">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgStaffAttendanceList" LoadingPanelID="RAPL1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <telerik:RadAjaxLoadingPanel ID="RAPL1" runat="server" Transparency="10">
    </telerik:RadAjaxLoadingPanel>
</asp:Content>
