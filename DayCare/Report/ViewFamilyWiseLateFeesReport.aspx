<%@ Page Title="" Language="C#" MasterPageFile="~/UI/DayCareIndex.Master" AutoEventWireup="true"
    CodeBehind="ViewFamilyWiseLateFeesReport.aspx.cs" Inherits="DayCare.Report.ViewFamilyWiseLateFeesReport" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h3 class="title">
        <img src="../images/arrow.png" />&nbsp; Late Fees Report</h3>
    <div class="clear">
    </div>
    <fieldset>
        <legend><strong>Criteria</strong></legend>
        <div class="fieldDiv">
            <div>
                <table>
                    <tr>
                        <td colspan="3">
                            Family:
                            <br />
                            <asp:DropDownList ID="ddlFamilies" CssClass="fildbox" Width="450" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 165px;">
                            Start Date:
                            <br />
                            <telerik:RadDatePicker ID="rdpStartDate" runat="server">
                            </telerik:RadDatePicker>
                        </td>
                        <td style="width: 165px;">
                            End Date:
                            <br />
                            <telerik:RadDatePicker ID="rdpEndDate" runat="server">
                            </telerik:RadDatePicker>
                        </td>
                        <td valign="bottom" style="padding-top:10px;">
                            <asp:Button ID="btnSubmit" runat="server" CssClass="viewReport" Text="View Report" OnClick="btnSubmit_Click" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div class="left pndR15">
        </div>
        <div class="box ">
        </div>
        <br />
        <div>
            <div class="box pndR15">
            </div>
            <div class="box pndR15">
            </div>
            <div class="left pndR15">
            </div>
        </div>
    </fieldset>
</asp:Content>
