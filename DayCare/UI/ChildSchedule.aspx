<%@ Page Title="" Language="C#" MasterPageFile="~/UI/DayCareIndex.Master" AutoEventWireup="true"
    CodeBehind="ChildSchedule.aspx.cs" Inherits="DayCare.UI.ChildSchedule" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/UI/UserControls/MenuLink.ascx" TagName="UCMenuLink" TagPrefix="ucl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript" language="javascript">
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode == 46 || (charCode >= 48 && charCode <= 57)) {
                return true;
            }
            else {
                return false;
            }
        }
    </script>

    <div class="clear">
    </div>
    <%--<h3 class="title">
        <img src="../images/arrow.png" />&nbsp; <a href="">Family Data</a>&nbsp;&nbsp;
        <img src="../images/arrow.png" />&nbsp; Child Data</h3>--%>
    <h3 class="title">
        <ucl:UCMenuLink ID="usrMenuLink" runat="server" />
    </h3>
    <div class="clear">
    </div>
    <fieldset>
        <legend><strong>User Details</strong></legend>
        <div class="fieldDiv">
            <div class="box pndR15">
                *Program:
                <br />
                <asp:DropDownList ID="ddlProgram" CssClass="fildboxstaff" runat="server" AutoPostBack="True"
                    OnSelectedIndexChanged="ddlProgram_SelectedIndexChanged">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfv1" runat="server" ControlToValidate="ddlProgram"
                    Font-Size="0" ErrorMessage="Please select Program." InitialValue="00000000-0000-0000-0000-000000000000"
                    Text="*" ValidationGroup="ChildScheduleValidate"></asp:RequiredFieldValidator>
            </div>
            <%--<div class="box pndR15" >
            *Class Category:
            <br />
            <asp:DropDownList ID="ddlClassCategory" CssClass="fildboxstaff" runat="server">
            </asp:DropDownList><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlClassCategory"
                Font-Size="0" ErrorMessage="Please select Class Category." InitialValue="00000000-0000-0000-0000-000000000000"
                Text="*" ValidationGroup="ChildScheduleValidate"></asp:RequiredFieldValidator>
        </div>--%>
            <div class="box" style="width: 475px;">
                *Pro Schedule:
                <br />
                <asp:DropDownList ID="ddlProgSchedule" CssClass="fildboxstaff" runat="server" Style="width: 470px;">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlProgSchedule"
                    Font-Size="0" ErrorMessage="Please select Prog. Schedule." InitialValue="00000000-0000-0000-0000-000000000000"
                    Text="*" ValidationGroup="ChildScheduleValidate"></asp:RequiredFieldValidator>
            </div>
            <div class="box pndR15">
                <div class="left pndR15">
                    *Begin Date:<br />
                    <telerik:RadDatePicker ID="rdpBeginDate" BorderWidth="0px" CssClass="fildboxstaff"
                        runat="server" Width="100px">
                        <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x">
                        </Calendar>
                        <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                        <DateInput DisplayDateFormat="M/d/yyyy" DateFormat="M/d/yyyy">
                        </DateInput>
                    </telerik:RadDatePicker>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="rdpBeginDate"
                        Font-Size="0" ErrorMessage="Please select Begin Date." Text="*" ValidationGroup="ChildScheduleValidate"></asp:RequiredFieldValidator>
                </div>
                <div class="left ">
                    *End Date:<br />
                    <telerik:RadDatePicker ID="rdpEndDate" BorderWidth="0px" CssClass="fildboxstaff"
                        runat="server" Width="100px">
                        <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x">
                        </Calendar>
                        <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                        <DateInput DisplayDateFormat="M/d/yyyy" DateFormat="M/d/yyyy">
                        </DateInput>
                    </telerik:RadDatePicker>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="rdpEndDate"
                        Font-Size="0" ErrorMessage="Please select End Date." Text="*" ValidationGroup="ChildScheduleValidate"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="box pndR15">
                Discount:
                <br />
                <asp:TextBox ID="txtDiscount" runat="server" CssClass="fildboxstaff" onkeypress="return isNumberKey(event);"></asp:TextBox>
            </div>
            <div class="left" style="padding-top: 10px; padding-left: 100px;">
                <div class="left">
                    <asp:Button ID="btnSave" Text="Save" runat="server" CssClass="btn" ValidationGroup="ChildScheduleValidate"
                        OnClick="btnSave_Click" />
                </div>
                <div class="left" style="padding-left: 10px;">
                    <asp:Button ID="btnCancel" Text="Cancel" runat="server" CssClass="btn" CausesValidation="false"
                        OnClick="btnCancel_Click" />
                </div>
            </div>
        </div>
    </fieldset>
    <asp:DropDownList ID="ddlClassRoom" Visible="false" runat="server">
    </asp:DropDownList>
    <div align="center" width="100%">
        <div class="clear">
        </div>
        <telerik:RadGrid ID="rgChildSchedule" CssClass="RemoveBorders" runat="server" Width="990px"
            Height="600px" AllowPaging="true" AllowSorting="true" AutoGenerateColumns="false"
            OnNeedDataSource="rgChildSchedule_NeedDataSource" PagerStyle-AlwaysVisible="true"
            BorderWidth="0" ClientSettings-Scrolling-AllowScroll="true" AllowFilteringByColumn="True"
            OnEditCommand="rgChildSchedule_EditCommand" GridLines="None" OnItemCommand="rgChildSchedule_ItemCommand"
            OnItemDataBound="rgChildSchedule_ItemDataBound">
            <GroupingSettings CaseSensitive="false" />
            <ItemStyle Wrap="true" />
            <HeaderContextMenu EnableEmbeddedSkins="true" />
            <PagerStyle AlwaysVisible="True" ShowPagerText="true" Mode="NextPrevAndNumeric" />
            <MasterTableView CommandItemDisplay="TopAndBottom" TableLayout="Auto" CommandItemSettings-AddNewRecordText="Add New Child Data"
                DataKeyNames="Id" EditFormSettings-EditColumn-Display="false" Width="100%">
                <Columns>
                    <telerik:GridEditCommandColumn HeaderStyle-Width="5%" HeaderText="Edit" ButtonType="ImageButton"
                        Reorderable="False" UniqueName="Edit" ItemStyle-BorderStyle="None">
                        <HeaderStyle Width="5%"></HeaderStyle>
                    </telerik:GridEditCommandColumn>
                    <telerik:GridBoundColumn DataField="SchoolProgramTitle" UniqueName="Program" AllowFiltering="true"
                        SortExpression="true" HeaderText="Program">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="ProgScheduleDay" UniqueName="ProgScheduleDay"
                        AllowFiltering="false" SortExpression="true" HeaderText="Day">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="ProgScheduleBeginTime" UniqueName="ProgScheduleBeginTime"
                        AllowFiltering="false" SortExpression="true" HeaderText="Schedule BeginTime"
                        DataFormatString="{0:t}">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="ProgScheduleEndTime" UniqueName="ProgScheduleEndTime"
                        AllowFiltering="false" SortExpression="true" HeaderText="Schedule EndTime" DataFormatString="{0:t}">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="ProgClassCategoryName" UniqueName="ProgClassCategory"
                        AllowFiltering="false" SortExpression="true" HeaderText="Class Category">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="ProgClassRoomName" UniqueName="ProgClassRoomName"
                        AllowFiltering="false" SortExpression="true" HeaderText="Class Room">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="BeginDate" UniqueName="BeginDate" AllowFiltering="false"
                        SortExpression="true" HeaderText="Begin Date" DataFormatString="{0:MM/dd/yyyy}">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText="End Date" DataField="EndDate" UniqueName="EndDate"
                        AllowFiltering="false" DataFormatString="{0:MM/dd/yyyy}">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="StaffFullName" UniqueName="StaffFullName" AllowFiltering="false"
                        SortExpression="true" HeaderText="Staff">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText="Discount" DataField="Discount" UniqueName="Discount"
                        AllowFiltering="false">
                    </telerik:GridBoundColumn>
                    <%-- <telerik:GridBoundColumn HeaderText="End Date" DataField="EndDate" UniqueName="EndDate"
                        AllowFiltering="false">
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
        <asp:Label ID="lblSchoolProgramId" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="lblProgClassCategoryId" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="lblProgScheduleId" runat="server" Visible="false"></asp:Label>
        <asp:ValidationSummary ID="valSum" runat="server" ValidationGroup="ChildScheduleValidate"
            ShowMessageBox="true" ShowSummary="false" />
        <telerik:RadAjaxManagerProxy ID="AjaxManagerProxy1" runat="server">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="ddlProgram">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="ddlClassCategory" LoadingPanelID="RAPL1" />
                        <telerik:AjaxUpdatedControl ControlID="ddlClassRoom" LoadingPanelID="RAPL1" />
                        <telerik:AjaxUpdatedControl ControlID="ddlProgSchedule" LoadingPanelID="RAPL1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManagerProxy>
        <telerik:RadAjaxLoadingPanel ID="RAPL1" runat="server" Transparency="10">
        </telerik:RadAjaxLoadingPanel>
    </div>
</asp:Content>
