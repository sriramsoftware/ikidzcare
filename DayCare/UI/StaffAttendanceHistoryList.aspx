<%@ Page Title="" Language="C#" MasterPageFile="~/UI/DayCareIndex.Master" AutoEventWireup="true"
    CodeBehind="StaffAttendanceHistoryList.aspx.cs" Inherits="DayCare.UI.StaffAttendanceHistoryList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/UI/UserControls/MenuLink.ascx" TagName="UCMenuLink" TagPrefix="ucl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h3 class="title">
        <ucl:UCMenuLink ID="usrMenuLink" runat="server" />
    </h3>
    <div class="clear">
        <telerik:RadGrid ID="rgStaffAttendanceHistory" runat="server" AutoGenerateColumns="false"
            AllowFilteringByColumn="false" AllowPaging="true" AllowSorting="true" EnableLinqExpression="false"
            EnableEmbeddedSkins="true" EnableAjaxSkinRendering="true" ReorderColumnOnClient="true"
            GridLines="None" CssClass="RemoveBorders" ShowGroupPanel="false" allowColumnReorder="True"
            PageSize="20" Width="100%" Height="600px" BorderWidth="0px" OnNeedDataSource="rgStaffAttendanceHistory_NeedDataSource"
            OnEditCommand="rgStaffAttendanceHistory_EditCommand" OnInsertCommand="rgStaffAttendanceHistory_InsertCommand"
            OnItemCreated="rgStaffAttendanceHistory_ItemCreated" OnItemDataBound="rgStaffAttendanceHistory_ItemDataBound"
            OnUpdateCommand="rgStaffAttendanceHistory_UpdateCommand" OnItemCommand="rgStaffAttendanceHistory_ItemCommand">
            <ItemStyle Wrap="true" />
            <PagerStyle AlwaysVisible="true" Mode="NextPrevAndNumeric" ShowPagerText="true" />
            <MasterTableView PagerStyle-AlwaysVisible="true" TableLayout="Auto" DataKeyNames="Id"
                EditMode="InPlace" CommandItemDisplay="TopAndBottom" InsertItemDisplay="Top"
                AllowFilteringByColumn="true" Width="100%">
                <CommandItemSettings AddNewRecordText="Add New" />
                <%--  <Columns>
                    <telerik:GridEditCommandColumn ButtonType="ImageButton" HeaderStyle-Width="5%" Reorderable="true"
                        HeaderText="Edit" UpdateText="Edit" UniqueName="Edit">
                    </telerik:GridEditCommandColumn>
                    <telerik:GridBoundColumn DataField="StaffName" HeaderText="Staff Name" UniqueName="ChildName"
                        AllowFiltering="false" Visible="false">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="CheckInCheckOutDateTime" HeaderText="CheckInCheckOut Date"
                        UniqueName="CheckInCheckOutDateTime" DataFormatString="{0:MM/dd/yyyy}" AllowFiltering="false">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="CheckInTime" HeaderText="CheckIn Time" UniqueName="CheckInTime"
                        DataFormatString="{0:H:mm}" AllowFiltering="false">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="CheckOutTime" HeaderText="CheckOut Time" UniqueName="CheckOutTime"
                        DataFormatString="{0:H:mm}" AllowFiltering="false">
                    </telerik:GridBoundColumn>
                </Columns>--%>
                <Columns>
                    <telerik:GridEditCommandColumn ButtonType="ImageButton" HeaderStyle-Width="5%" Reorderable="true"
                        HeaderText="Edit" UpdateText="Edit" UniqueName="Edit">
                    </telerik:GridEditCommandColumn>
                    <telerik:GridTemplateColumn HeaderText="Date" UniqueName="CheckInCheckOutDateTime"
                        AllowFiltering="false">
                        <ItemTemplate>
                            <asp:Label ID="lblCheckInCheckOutDateTime" runat="server" Text='<%# Eval("CheckInCheckOutDateTime","{0:MM/dd/yyyy}") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <telerik:RadDatePicker ID="rdpCheckInCheckOutDateTime" runat="server">
                            </telerik:RadDatePicker>
                        </EditItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderText="CheckIn Time" UniqueName="CheckInTime" AllowFiltering="false">
                        <ItemTemplate>
                            <asp:Label ID="lblCheckInTime" runat="server" Text='<%# Eval("CheckInTime","{0:T}") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <telerik:RadTimePicker ID="rtpCheckInTime" runat="server">
                                <TimeView Interval="0:30:0" Columns="4">
                                </TimeView>
                            </telerik:RadTimePicker>
                        </EditItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderText="CheckOut Time" UniqueName="CheckOutTime"
                        AllowFiltering="false">
                        <ItemTemplate>
                            <asp:Label ID="lblCheckOutTime" runat="server" Text='<%# Eval("CheckOutTime","{0:T}") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <telerik:RadTimePicker ID="rtpCheckOutTime" runat="server">
                                <TimeView Interval="0:30:0" Columns="4">
                                </TimeView>
                            </telerik:RadTimePicker>
                        </EditItemTemplate>
                    </telerik:GridTemplateColumn>
                    <%-- <telerik:GridBoundColumn DataField="StaffSchoolYearId" HeaderText="StaffSchoolYearId" UniqueName="StaffSchoolYearId" Visible="false">
                    </telerik:GridBoundColumn>--%>
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
        <telerik:RadAjaxManagerProxy ID="AjaxManagerProxy1" runat="server">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="rgStaffAttendanceHistory">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="rgStaffAttendanceHistory" LoadingPanelID="RAPL1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManagerProxy>
        <telerik:RadAjaxLoadingPanel ID="RAPL1" runat="server"  Transparency="10">
        </telerik:RadAjaxLoadingPanel>
</asp:Content>
