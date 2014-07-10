<%@ Page Title="" Language="C#" MasterPageFile="~/UI/DayCareIndex.Master" AutoEventWireup="true"
    CodeBehind="ViewPaymentDeposits.aspx.cs" Inherits="DayCare.Report.ViewPaymentDeposits" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h3 class="title">
        <img src="../images/arrow.png" />&nbsp; Deposits Report</h3>
        <div class="clear" style="height:8px;"></div>
    <div style="font-family:Tahoma; font-size:14px; ">
        <asp:Label ID="lblLastUpdatedLedger" Text="Note: The ledger was last updated on {0}. Do you wish to update the ledger to get up to date report? If yes," runat="server" ForeColor="Blue"></asp:Label>
        <asp:LinkButton ID="lnkUpdateLedger" runat="server" Text="Click Here" OnClick="btnUpdateLedger_OnClick"></asp:LinkButton>
        <%--<div style="float: right;">
            <asp:Button ID="btnUpdateLedger" Text="Update Ledger" runat="server" CssClass="btn" OnClick="btnUpdateLedger_OnClick" /></div>--%>
    </div>
     <div class="clear" style="height:5px;"></div>
    <legend><strong>Criteria</strong></legend>
    <fieldset>
        <div>
            <div class="fieldDiv">
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
                <div class="box" style="margin-top: -6px;">
                    <br />
                    <asp:Button ID="btnSave" runat="server" Text="View Report" CssClass="viewReport"
                        OnClick="btnSave_Click" />
                </div>
            </div>
    </fieldset>
</asp:Content>
