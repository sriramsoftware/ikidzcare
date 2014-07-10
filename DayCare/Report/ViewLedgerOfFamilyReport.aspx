<%@ Page Title="" Language="C#" MasterPageFile="~/UI/DayCareIndex.Master" AutoEventWireup="true"
    CodeBehind="ViewLedgerOfFamilyReport.aspx.cs" Inherits="DayCare.Report.ViewLedgerOfFamilyReport" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h3 class="title">
        <img src="../images/arrow.png" alt="" />&nbsp;Ledger OF Family Report
    </h3>
    <fieldset>
        <legend><strong>Criteria</strong></legend>
        <div class="fieldDiv">
            <div class="box pndR15"  style="width:150px;">
                Start Date:
                <br />
                <telerik:RadDatePicker ID="rdpStartDate" runat="server"  >
                </telerik:RadDatePicker>
            </div>
            <div class="box " style="width:160px;">
                End Date:
                <br />
                <telerik:RadDatePicker ID="rdpEndDate" runat="server">
                </telerik:RadDatePicker>
            </div>
            <div class="box" style="margin-top:-8px;">
                <br />
                <%--<asp:Button ID="btnGrid" runat="server" Text="View In Grid" CssClass="btn" OnClick="btnGrid_Click" />--%>
                <asp:Button ID="btnSave" runat="server" Text="View Report" CssClass="viewReport" OnClick="btnSave_Click" />
            </div>
        </div>
    </fieldset>
</asp:Content>
