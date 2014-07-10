<%@ Page Title="" Language="C#" MasterPageFile="~/UI/DayCareIndex.Master" AutoEventWireup="true"
    CodeBehind="ViewClassWiseStudentAssigned.aspx.cs" Inherits="DayCare.Report.ViewClassWiseStudentAssigned" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h3 class="title">
        <img src="../images/arrow.png" />&nbsp; Student List Report</h3>
    <div class="clear">
    </div>
    <fieldset>
        <legend><strong>Criteria</strong></legend>
        <div class="fieldDiv">
            <div class="box pndR15 ">
                Type:
                <br />
                <asp:DropDownList ID="ddlReportType" CssClass="fildbox" runat="server">
                    <asp:ListItem Text="--Select--" Value="--Select--"></asp:ListItem>
                     <asp:ListItem Text="Student Fees by Class" Value="ClassWiseStudentWithFee"></asp:ListItem>
                     <asp:ListItem Text="Student Fees by Program" Value="ProgramWiseStudentWithFee"></asp:ListItem>
                    <asp:ListItem Text="Student List by Class" Value="ClassWiseStudent"></asp:ListItem>                   
                     <asp:ListItem Text="Student List by Program" Value="ProgramWiseStudent"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="box pndR15">
                Class Room:<br />
                <asp:DropDownList ID="ddlClassRoom" CssClass="fildbox" runat="server">
                </asp:DropDownList>
            </div>
            <div class="box pndR15">
                School Program:<br />
                <asp:DropDownList ID="ddlProgram" CssClass="fildbox" runat="server">
                </asp:DropDownList>
            </div>
           <div class="box" style="width:120px;float:left; margin-top:-8px;">
                <br />
                <asp:Button ID="btnSubmit" runat="server" CssClass="viewReport" Text="View Report" OnClick="btnSubmit_Click" />
            </div>
        </div>
    </fieldset>
</asp:Content>
