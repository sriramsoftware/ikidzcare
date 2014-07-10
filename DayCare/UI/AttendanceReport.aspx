<%@ Page Title="" Language="C#" MasterPageFile="~/UI/DayCareIndex.Master" AutoEventWireup="true"
    CodeBehind="AttendanceReport.aspx.cs" Inherits="DayCare.UI.AttendanceReport" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/UI/UserControls/MenuLink.ascx" TagName="UCMenuLink" TagPrefix="ucl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="clear">
    </div>
    <h3 class="title">
        <ucl:UCMenuLink ID="usrMenuLink" runat="server" />
    </h3>
    <fieldset>
        <legend><strong>Search Criteria</strong></legend>
        <div class="fieldDiv">
            <div class="box pndR15">
                Select Attendace Report For:
                <br />
                <asp:DropDownList ID="ddlReportFor" runat="server" CssClass="fildbox">
                    <asp:ListItem Text="--Select--" Value="--Select--"></asp:ListItem>
                    <asp:ListItem Text="Staff" Value="Staff"></asp:ListItem>
                    <asp:ListItem Text="Student" Value="Student"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="box pndR15">
                Start Date:
                <br />
                <telerik:RadDatePicker ID="rdpStartDate" runat="server">
                </telerik:RadDatePicker>
            </div>
            <div class="box pndR15">
                End Date
                <br />
                <telerik:RadDatePicker ID="rdpEndDate" runat="server">
                </telerik:RadDatePicker>
            </div>
            <div class="box">
                <br />
                <asp:Button ID="btnSave" runat="server" Text="View Report" CssClass="btn" OnClick="btnSave_Click" />
            </div>
        </div>
    </fieldset>
    <br />
    <br />
    <%--<asp:Button ID="btnPdf" runat="server" Text="Export to PDF" OnClick="btnPdf_Click" />--%>
    <asp:Button ID="btnCSV" runat="server" Text="Export to CSV" OnClick="btnCSV_Click"
        CssClass="btn" />
    <br />
    <div class="clear"></div>
    <telerik:RadGrid ID="rgAttendanceReport" runat="server" CssClass="RemoveBorders"
        PageSize="10" OnNeedDataSource="rgAttendanceReport_NeedDataSource" Width="100%"
        Height="600px" AllowPaging="true" AllowSorting="true" AutoGenerateColumns="false"
        PagerStyle-AlwaysVisible="true" AllowFilteringByColumn="True" GridLines="None"
        BorderWidth="0">
        <GroupingSettings CaseSensitive="false" />
        <HeaderContextMenu EnableEmbeddedSkins="true" />
        <PagerStyle AlwaysVisible="True" ShowPagerText="true" Mode="NextPrevAndNumeric" />
        <MasterTableView CommandItemDisplay="NONE" CommandItemSettings-AddNewRecordText="Add New Role"
            HorizontalAlign="NotSet" EditMode="InPlace" Width="100%" DataKeyNames="Id">
            <Columns>
                <telerik:GridBoundColumn DataField="Name" HeaderText="Name" UniqueName="Name">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="CheckInCheckOutDateTime" HeaderText="Date" UniqueName="CheckInCheckOutDate"
                    DataFormatString="{0:dd/MM/yyyy}">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="CheckInCheckOutDateTime" HeaderText="Time" UniqueName="CheckInCheckOutTime"
                    DataFormatString="{0:hh:mm tt}" AllowFiltering="false">
                </telerik:GridBoundColumn>
                <telerik:GridCheckBoxColumn DataField="CheckIn" HeaderText="Clock In" UniqueName="CheckIn">
                </telerik:GridCheckBoxColumn>
                <telerik:GridCheckBoxColumn DataField="CheckOut" HeaderText="Clock Out" UniqueName="CheckOut">
                </telerik:GridCheckBoxColumn>
                <telerik:GridBoundColumn DataField="CheckIn" HeaderText="Sign In" UniqueName="SignIn"
                    Visible="false">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="CheckOut" HeaderText="Sign Out" UniqueName="SignOut"
                    Visible="false">
                </telerik:GridBoundColumn>
            </Columns>
        </MasterTableView>
        <ClientSettings AllowColumnsReorder="True" AllowDragToGroup="True" ReorderColumnsOnClient="True">
            <Selecting AllowRowSelect="True" />
            <Scrolling AllowScroll="True" UseStaticHeaders="True" />
        </ClientSettings>
        <FilterMenu EnableEmbeddedSkins="true" ExpandAnimation-Type="Linear">
            <ExpandAnimation Type="Linear" />
        </FilterMenu>
        <ExportSettings HideStructureColumns="true">
        </ExportSettings>
    </telerik:RadGrid>
    <br />
    <br />
</asp:Content>
