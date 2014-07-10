<%@ Page Title="" Language="C#" MasterPageFile="~/UI/DayCareIndex.Master" AutoEventWireup="true"
    CodeBehind="viewstudentschedule.aspx.cs" Inherits="DayCare.Report.viewstudentschedule" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    .
    <h3 class="title">
        <img src="../images/arrow.png" />&nbsp; Student List Report</h3>
    <div class="clear">
    </div>
    <fieldset>
        <legend><strong>Criteria</strong></legend>
        <div class="fieldDiv">
            <div class="left pndR15 ">
                Last Name From:
                <br />
                <asp:TextBox ID="txtLastNameFrom" runat="server" Text="A" ></asp:TextBox>
            </div>
            <div class="left pndR15">
                Last Name To:<br />
                <asp:TextBox ID="txtLastNameTo" runat="server" Text="Z" ></asp:TextBox>
            </div>
            <div class="left" style="width:90px;float:left; margin-top:-8px;">
                <br />
                <asp:Button ID="btnSubmit" runat="server" CssClass="viewReport" Text="View Report" OnClick="btnSubmit_Click" />
            </div>
        </div>
    </fieldset>
</asp:Content>
