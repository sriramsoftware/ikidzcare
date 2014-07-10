<%@ Page Title="" Language="C#" MasterPageFile="~/UI/DayCareIndex.Master" AutoEventWireup="true"
    CodeBehind="ViewLedgerReport.aspx.cs" Inherits="DayCare.Report.ViewLedgerReport" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h3 class="title">
        <img src="../images/arrow.png" alt="" />&nbsp;Ledger Report
    </h3>
    <div style="font-family: Tahoma; font-size: 14px; padding-top: 5px;">
        <asp:Label ID="lblLastUpdatedLedger" Text="Note: The ledger was last updated on {0}. Do you wish to update the ledger to get up to date report? If yes,"
            runat="server" ForeColor="Blue"></asp:Label>
        <asp:LinkButton ID="lnkUpdateLedger" runat="server" Text="Click Here" OnClick="btnUpdateLedger_OnClick"></asp:LinkButton>
    </div>
    <div class="clear" style="height: 8px;">
    </div>
    <fieldset>
        <legend><strong>Criteria</strong></legend>
        <div class="fieldDiv">
            <div class="box pndR15 ">
                <span class="red">*</span> Family:
                <br />
                <asp:DropDownList ID="ddlFamily" CssClass="fildbox" runat="server">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvfamily" runat="server" ControlToValidate="ddlFamily"
                    SetFocusOnError="true" Font-Size="0" ErrorMessage="Please select family." InitialValue="00000000-0000-0000-0000-000000000000"
                    Text="*" ValidationGroup="SchoolValidate"></asp:RequiredFieldValidator>
            </div>
            <div class="box pndR15" style="width: 150px;">
                Start Date:
                <br />
                <telerik:RadDatePicker ID="rdpStartDate" runat="server">
                </telerik:RadDatePicker>
            </div>
            <div class="box pndR15" style="width: 150px;">
                End Date:
                <br />
                <telerik:RadDatePicker ID="rdpEndDate" runat="server">
                </telerik:RadDatePicker>
            </div>
            <div class="box" style="margin-top: -8px;">
                <br />
                <%--<asp:Button ID="btnGrid" runat="server" Text="View In Grid" CssClass="btn" OnClick="btnGrid_Click" />--%>
                <asp:Button ID="btnSave" runat="server" Text="View Report" CssClass="viewReport"
                    OnClick="btnSave_Click" ValidationGroup="SchoolValidate" />
            </div>
        </div>
    </fieldset>
</asp:Content>
