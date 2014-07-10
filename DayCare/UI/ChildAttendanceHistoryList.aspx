<%@ Page Title="" Language="C#" MasterPageFile="~/UI/DayCareIndex.Master" AutoEventWireup="true"
    CodeBehind="ChildAttendanceHistoryList.aspx.cs" Inherits="DayCare.UI.ChildAttendanceHistoryList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/UI/UserControls/MenuLink.ascx" TagName="UCMenuLink" TagPrefix="ucl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h3 class="title">
        <ucl:UCMenuLink ID="usrMenuLink" runat="server" />
    </h3>
    <div class="clear">
        <telerik:RadGrid ID="rgChildAttendanceHistory" runat="server" AutoGenerateColumns="false"
            AllowFilteringByColumn="false" AllowPaging="true" AllowSorting="true" EnableLinqExpression="false"
            EnableEmbeddedSkins="true" EnableAjaxSkinRendering="true" ReorderColumnOnClient="true"
            GridLines="None" CssClass="RemoveBorders" ShowGroupPanel="false" allowColumnReorder="True" PageSize="20"
            Width="100%" Height="600px" BorderWidth="0px" OnNeedDataSource="rgChildAttendanceHistory_NeedDataSource"
            OnEditCommand="rgChildAttendanceHistory_EditCommand" OnInsertCommand="rgChildAttendanceHistory_InsertCommand"
            OnItemCreated="rgChildAttendanceHistory_ItemCreated" OnItemDataBound="rgChildAttendanceHistory_ItemDataBound"
            OnUpdateCommand="rgChildAttendanceHistory_UpdateCommand" OnItemCommand="rgChildAttendanceHistory_ItemCommand">
            <ItemStyle Wrap="true" />
            <PagerStyle AlwaysVisible="true" Mode="NextPrevAndNumeric" ShowPagerText="true" />
            <MasterTableView PagerStyle-AlwaysVisible="true" CommandItemDisplay="TopAndBottom"
                EditMode="InPlace" DataKeyNames="Id" InsertItemDisplay="Top" TableLayout="Auto"
                AllowFilteringByColumn="true" Width="100%">
                <CommandItemSettings AddNewRecordText="Add New"/>
                <%--<Columns>S
                    <telerik:GridBoundColumn DataField="ChildName" HeaderText="Child Name" UniqueName="ChildName">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="CheckInCheckOutDateTime" HeaderText="CheckInCheckOut Date"
                        UniqueName="CheckInCheckOutDateTime" dataformatstring="{0:MM/dd/yyyy}">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="CheckInTime" HeaderText="CheckIn Time" UniqueName="CheckInTime"  dataformatstring="{0:H:mm}">
                    </telerik:GridBoundColumn> 
                    <telerik:GridBoundColumn DataField="CheckOutTime" HeaderText="CheckOut Time" UniqueName="CheckOutTime" dataformatstring="{0:H:mm}">
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
                <telerik:AjaxSetting AjaxControlID="rgChildAttendanceHistory">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="rgChildAttendanceHistory" LoadingPanelID="RAPL1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManagerProxy>
        <telerik:RadAjaxLoadingPanel ID="RAPL1" runat="server" Transparency="10">
        </telerik:RadAjaxLoadingPanel>
</asp:Content>
