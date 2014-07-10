<%@ Page Title="" Language="C#" MasterPageFile="~/UI/DayCareIndex.Master" AutoEventWireup="true"
    CodeBehind="MyAccount.aspx.cs" Inherits="DayCare.UI.MyAccount" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--<center>--%>

    <script language="javascript" type="text/javascript">
        function isNumberKey(obj, evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode == 46 || charCode == 8 || (charCode >= 48 && charCode <= 57)) {
                if (obj != null) {
                    if (obj.value.length < 4) {
                        return true;
                    }
                    else {
                        return false;
                    }
                }
                return false;
            }
            else
                return false;
        }
    </script>

    <div class="clear">
    </div>
    <h3 class="title">
        <img src="../images/arrow.png" />&nbsp;My Account</h3>
    <div class="clear">
    </div>
    <fieldset>
        <legend><strong>Details</strong></legend>
        <div class="fieldDiv" style="background: none; border: 0;">
            <div style="width: 355px; margin: 0 auto; background: #f5f9fb; padding: 10px; border: 1px solid #e3f4fc">
                <div class="box2 pndR15">
                    <span class="red">*</span>Old Password:</div>
                <div class="box">
                    <asp:TextBox ID="txtOldPassword" runat="server" TextMode="Password" CssClass="fildboxstaff"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtOldPassword"
                        ErrorMessage="Please enter old Password ." Text="*" Font-Size="0" ValidationGroup="myaccount"></asp:RequiredFieldValidator>
                </div>
                <div class=" clear">
                </div>
                <div class="box2 pndR15">
                    <span class="red">*</span>New Password:</div>
                <div class="box ">
                    <asp:TextBox ID="txtPassword" runat="server" CssClass="fildboxstaff" TextMode="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtPassword"
                        ErrorMessage="Please enter new Password ." Text="*" Font-Size="0" ValidationGroup="myaccount"></asp:RequiredFieldValidator>
                </div>
                <div class=" clear">
                </div>
                <div class="box2 pndR15">
                    <span class="red">*</span>Verify Password:</div>
                <div class="box ">
                    <asp:TextBox ID="txtNewPassword" runat="server" TextMode="Password" CssClass="fildboxstaff"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtNewPassword"
                        ErrorMessage="Please enter varify Password ." Text="*" Font-Size="0" ValidationGroup="myaccount"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtNewPassword"
                        ErrorMessage="New Password Does not Match." Text="*" Font-Size="0" ValidationGroup="myaccount"
                        ControlToValidate="txtPassword"></asp:CompareValidator>
                </div>
                <div class=" box2 pndR15">
                </div>
                <div class="box " style="padding-bottom: 0px;">
                    <div class="left">
                        <asp:Button ID="btnSave" Text="Save" runat="server" CssClass="btn" ValidationGroup="myaccount"
                            CausesValidation="true" OnClick="btnSave_Click" />
                    </div>
                    <div class="left" style="padding-left: 10px;">
                        <asp:Button ID="Button2" Text="Cancel" runat="server" CssClass="btn" OnClick="btnCancel_Click" />
                    </div>
                </div>
                <div class=" clear">
                </div>
            </div>
            <div class="box2 pndR15" style="display: none;">
                *First Name:</div>
            <div class="box pndR15">
                <asp:TextBox ID="txtFirstName" runat="server" CssClass="fildboxstaff" Visible="false"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtFirstName"
                    ErrorMessage="Please enter First Name." Text="*" Font-Size="0" ValidationGroup="myaccount"></asp:RequiredFieldValidator>
            </div>
            <div class="box2 pndR15" style="display: none;">
                *Last Name:</div>
            <div class="box pndR15">
                <asp:TextBox ID="txtLastName" runat="server" CssClass="fildboxstaff" Visible="false"></asp:TextBox>
            </div>
            <div class="box2 pndR15" style="display: none;">
                *User Name:</div>
            <div class="box pndR15">
                <asp:TextBox ID="txtUserName" runat="server" Enabled="false" CssClass="fildboxstaff"
                    Visible="false"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtUserName"
                    ErrorMessage="Please enter UserName ." Text="*" Visible="false" Font-Size="0"
                    ValidationGroup="myaccount"></asp:RequiredFieldValidator>
            </div>
        </div>
    </fieldset>
    <%--<table>
        <tr>
            <td align="right">
                <asp:Label ID="lblFirstName" runat="server" Text="First Name:"></asp:Label>
            </td>
            <td align="right">
                <asp:TextBox ID="txtFirstName" runat="server" Width="330px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtFirstName"
                        ErrorMessage="Please enter First Name." Text="*" Font-Size="0" ValidationGroup="myaccount"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:Label ID="lblLastName" runat="server" Text="Last Name:"></asp:Label>
            </td>
            <td align="right">
               <asp:TextBox ID="txtLastName" runat="server" Width="330px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtLastName"
                        ErrorMessage="Please enter Last Name." Text="*" Font-Size="0" ValidationGroup="myaccount"></asp:RequiredFieldValidator>
               
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:Label ID="lblUserName" runat="server" Text="User Name:"></asp:Label>
            </td>
            <td align="right">
                  <asp:TextBox ID="txtUserName" runat="server"  Width="330px" Enabled="false"></asp:TextBox>
                   <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtUserName"
                        ErrorMessage="Please enter UserName Name." Text="*" Visible="false" Font-Size="0" ValidationGroup="myaccount"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:Label ID="OldPassword" runat="server" Text="Old Password:"></asp:Label>
            </td>
            <td align="right">
                  <asp:TextBox ID="txtOldPassword" runat="server" TextMode="Password"  Width="330px"></asp:TextBox>
               
            </td>
        <tr>
            <td align="right">
                <asp:Label ID="lblPassword" runat="server" Text="New Password:"></asp:Label>
            </td>
            <td align="right">
                  <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"  Width="330px"></asp:TextBox>
                   <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtPassword"
                        ErrorMessage="Please enter Password Name." Text="*" Font-Size="0" ValidationGroup="myaccount"></asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtNewPassword" ErrorMessage="New Password Does not Match."
                        Text="*" Font-Size="0" ValidationGroup="myaccount" ControlToValidate="txtPassword"></asp:CompareValidator>
                        
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:Label ID="lblNewPassword" runat="server" Text="Varify Password:" ></asp:Label>
            </td>
            <td align="right">
                  <asp:TextBox ID="txtNewPassword" runat="server" TextMode="Password"  Width="330px"></asp:TextBox>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtNewPassword"
                        ErrorMessage="Please enter New Password Name." Text="*" Font-Size="0" ValidationGroup="myaccount"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:Label ID="lblCode" runat="server" Text="Code:"> </asp:Label>
            </td>
            <td align="right">
                  <asp:TextBox ID="txtCode" runat="server"  Width="330px"></asp:TextBox>
                   
            </td>
        </tr>
        <tr>
            <td align="right" colspan="2">
               <asp:Button ID="btnSave" Text="Save"  runat="server"  Width="65px" ValidationGroup="myaccount" CausesValidation="true" OnClick="btnSave_Click"/>&nbsp;
                   <asp:Button ID="btnCancel" Text="Cancel" runat="server" Width="65px" OnClick="btnCancel_Click"/>
            </td>
        </tr>
        
    </table>--%>
    <asp:HiddenField ID="hdnPassword" runat="server" />
    <%--</center>--%>
    <asp:ValidationSummary ID="valSum" runat="server" ValidationGroup="myaccount" ShowMessageBox="true"
        ShowSummary="false" />
</asp:Content>
