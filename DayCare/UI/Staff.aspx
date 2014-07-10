<%@ Page Title="" Language="C#" MasterPageFile="~/UI/DayCareIndex.Master" AutoEventWireup="true"
    CodeBehind="Staff.aspx.cs" Inherits="DayCare.UI.Staff" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/UI/UserControls/MenuLink.ascx" TagName="UCMenuLink" TagPrefix="ucl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="clear">
    </div>
    <h3 class="title">
        <ucl:UCMenuLink ID="usrMenuLink" runat="server" />
    </h3>
    </div>

    <script type="text/javascript" language="javascript">
        function isNumberKey(obj, evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode == 8 || (charCode >= 48 && charCode <= 57)) {
                if (obj != null) {
                    if (charCode == 8 || obj.value.length < 4) {//8= back space
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

        function RistricChar(obj, evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode == 35) {
                return false;
            }
            else {
                return true;
            }
        }
        function CheckNumOfChar(sender, args) {

            var txtUserName = document.getElementById('ctl00_ContentPlaceHolder1_txtUserName');
            var txtPassword = document.getElementById('ctl00_ContentPlaceHolder1_txtPassword');
            if (sender.controltovalidate == "ctl00_ContentPlaceHolder1_txtUserName") {
                if (txtUserName.value.length >= 6) {
                    args.IsValid = true;
                }
                else {
                    args.IsValid = false;

                }
            }
            if (sender.controltovalidate == "ctl00_ContentPlaceHolder1_txtPassword") {
                if (txtPassword.value.length >= 6) {
                    args.IsValid = true;
                }
                else {
                    args.IsValid = false;
                }
            }
            return true;
        }
        function CheckCodeNumOfChar(sender, args) {
            var txtUserName = document.getElementById('ctl00_ContentPlaceHolder1_txtCode');
            if (txtUserName.value.length == 4) {
                args.IsValid = true;
            }
            else {
                args.IsValid = false;
            }
        }
        function GenderShow(obj) {
            var imgStaff = document.getElementById("ctl00_ContentPlaceHolder1_imgStaff");
            var ddlGender = document.getElementById("ctl00_ContentPlaceHolder1_ddlGender");

            if (imgStaff.src.indexOf("male") > 0 || imgStaff.src.indexOf("female") > 0) {
                if (document.getElementById("ctl00_ContentPlaceHolder1_ddlGender").selectedIndex == 1) {
                    imgStaff.src = "../StaffImages/male_photo.png";
                }
                else {
                    imgStaff.src = "../StaffImages/female_photo.png";
                }
            }
        }
    </script>

    <fieldset class="fieldset">
        <legend><strong>Personal Details</strong></legend>
        <div class="fieldDiv">
            <div class="box pndR15" style="height: 170px;">
                <div class="left" style="padding: 8px 65px;">
                    <div class="photo">
                        <asp:Image ID="imgStaff" runat="server" ImageUrl="~/StaffImages/female_photo.png" Width="99"
                            Height="125" />
                    </div>
                </div>
                <div class="box pndbNone">
                    Image:
                    <br />
                    <asp:FileUpload ID="fupImage" runat="server" />
                    <asp:Label ID="lblImage" runat="server" Visible="false"></asp:Label>
                </div>
            </div>
            <div class="box pndR15">
                <span class="red">*</span>First Name:
                <br />
                <asp:TextBox ID="txtFirstName" runat="server" CssClass="fildboxstaff"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtFirstName"
                    ErrorMessage="Please enter First Name." Text="*" Font-Size="0" ValidationGroup="StaffValidate"></asp:RequiredFieldValidator>
            </div>
            <div class="box pndR15">
                <span class="red">*</span>Last Name:
                <br />
                <asp:TextBox ID="txtLastName" runat="server" CssClass="fildboxstaff"></asp:TextBox><asp:RequiredFieldValidator
                    ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtLastName" ErrorMessage="Please enter Last Name."
                    Text="*" Font-Size="0" ValidationGroup="StaffValidate"></asp:RequiredFieldValidator>
            </div>
            <div class="box">
                Gender:
                <br />
                <%--<asp:RadioButton ID="rdMale" runat="server" Checked="true" GroupName="Gender" onclick="javascript:GenderShow('male');" />Male
                &nbsp;
                <asp:RadioButton ID="rdFemale" runat="server" GroupName="Gender" onclick="javascript:GenderShow('female');" />Female--%>
                <asp:DropDownList ID="ddlGender" runat="server" onchange="javascript:GenderShow('');" Width="100px">
                    <asp:ListItem Text="Female" Selected="True" Value="false"></asp:ListItem>
                    <asp:ListItem Text="Male" Value="true"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="box pndR15">
                Address 1:
                <br />
                <asp:TextBox ID="txtAddress1" CssClass="fildboxstaff" runat="server"></asp:TextBox>
            </div>
            <div class="box pndR15">
                Address 2:<br />
                <asp:TextBox ID="txtAddress2" runat="server" CssClass="fildboxstaff"></asp:TextBox>
            </div>
            <div class="box">
                City:<br />
                <asp:TextBox ID="txtCity" runat="server" CssClass="fildboxstaff"></asp:TextBox>
            </div>
            <div class="box pndR15 ">
                Zip:
                <br />
                <asp:TextBox ID="txtZip" runat="server" CssClass="fildboxstaff"></asp:TextBox>
            </div>
            <div class="box pndR15">
                Country:
                <br />
                <asp:DropDownList ID="ddlCountry" runat="server" CssClass="fildboxstaff" Width="230px"
                    AutoPostBack="True" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged">
                </asp:DropDownList>
            </div>
            <div class="box">
                State:
                <br />
                <asp:DropDownList ID="ddlState" CssClass="fildboxstaff" runat="server" Width="230px">
                </asp:DropDownList>
            </div>
            <div class="box pndR15 pndbNone">
                Main Phone:<br />
                <asp:TextBox ID="txtMainPhone" runat="server" CssClass="fildboxstaff"></asp:TextBox><%--<asp:RequiredFieldValidator
                    ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtMainPhone"
                    ErrorMessage="Please enter Main Phone." Text="*" Font-Size="0" ValidationGroup="StaffValidate"></asp:RequiredFieldValidator>--%>
            </div>
            <div class="box pndR15 pndbNone">
                Secondary Phone:<br />
                <asp:TextBox ID="txtSecondaryPhone" runat="server" CssClass="fildboxstaff"></asp:TextBox>
            </div>
            <div class="box pndbNone">
                Email:
                <br />
                <asp:TextBox ID="txtEmail" runat="server" CssClass="fildboxstaff"></asp:TextBox><%--<asp:RequiredFieldValidator
                    ID="RequiredFieldValidator12" runat="server" Display="Dynamic" ControlToValidate="txtEmail"
                    Font-Size="0" ErrorMessage="Please enter Email." Text="*" ValidationGroup="StaffValidate"></asp:RequiredFieldValidator>--%><asp:RegularExpressionValidator
                        ID="rev" ControlToValidate="txtEmail" runat="server" Text="*" ErrorMessage="Please Enter Valid Email Id."
                        Display="Dynamic" ValidationGroup="StaffValidate" Font-Size="0" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator>
            </div>
        </div>
    </fieldset>
    <fieldset class="fieldset">
        <legend><strong>User Details</strong></legend>
        <div class="fieldDiv">
            <div class="box pndR15">
                <span class="red">*</span>User Group:
                <br />
                <asp:DropDownList ID="ddlUserGroup" runat="server" CssClass="fildboxstaff" Width="230px">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfv1" runat="server" ControlToValidate="ddlUserGroup"
                    Font-Size="0" ErrorMessage="Please select User Group." InitialValue="00000000-0000-0000-0000-000000000000"
                    Text="*" ValidationGroup="StaffValidate"></asp:RequiredFieldValidator>
            </div>
            <div class="box pndR15">
                <span class="red">*</span>Staff Category:
                <br />
                <asp:DropDownList ID="ddlStaffCategory" runat="server" CssClass="fildboxstaff" Width="230px">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlStaffCategory"
                    ErrorMessage="Please select Staff Category." Font-Size="0" InitialValue="00000000-0000-0000-0000-000000000000"
                    Text="*" ValidationGroup="StaffValidate"></asp:RequiredFieldValidator>
            </div>
            <div class="box pndR15">
                Username:
                <br />
                <asp:TextBox ID="txtUserName" runat="server" onkeypress="javascript:return RistricChar(this,event);" AutoCompleteType="Disabled"
                    CssClass="fildboxstaff"></asp:TextBox><%--<asp:RequiredFieldValidator ID="RequiredFieldValidator13"
                        runat="server" ControlToValidate="txtUserName" Font-Size="0" ErrorMessage="Please enter User Name."
                        Text="*" ValidationGroup="StaffValidate"></asp:RequiredFieldValidator>--%>
                <asp:CustomValidator ID="cv" runat="server" ValidationGroup="StaffValidate" ControlToValidate="txtUserName"
                    ErrorMessage="User Name minimum 6 character." Text="*" Font-Size="0" SetFocusOnError="true"
                    ClientValidationFunction="CheckNumOfChar"></asp:CustomValidator>
            </div>
            <div class="box ">
                Password:
                <br />
                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="fildboxstaff"  AutoCompleteType="Disabled"
                    onkeypress="javascript:return RistricChar(this,event);"></asp:TextBox><%--<asp:RequiredFieldValidator
                        ID="RequiredFieldValidator14" runat="server" ControlToValidate="txtPassword"
                        Font-Size="0" ErrorMessage="Please enter Password." Text="*" ValidationGroup="StaffValidate"></asp:RequiredFieldValidator>--%>
                <asp:CustomValidator ID="CustomValidator1" runat="server" ValidationGroup="StaffValidate"
                    ControlToValidate="txtPassword" ErrorMessage="Password minimum 6 character."
                    Text="*" Font-Size="0" SetFocusOnError="true" ClientValidationFunction="CheckNumOfChar"></asp:CustomValidator>
            </div>
            <div class="clear">
            </div>
            <div class="box pndR15">
                Message:
                <br />
                <asp:TextBox ID="txtMessage" TextMode="MultiLine" runat="server" CssClass="fildboxstaff"
                    Style="height: 50px;"></asp:TextBox>
            </div>
            <div class="box pndR15">
                Security Question:
                <br />
                <asp:TextBox ID="txtSecurityQuestion" runat="server" TextMode="MultiLine" class="fildboxstaff"
                    Style="height: 50px;"></asp:TextBox><%--<asp:RequiredFieldValidator ID="RequiredFieldValidator17"
                        Font-Size="0" runat="server" ControlToValidate="txtSecurityQuestion" ErrorMessage="Please enter Security Question."
                        Text="*" ValidationGroup="StaffValidate"></asp:RequiredFieldValidator>--%>
            </div>
            <div class="box pndR15 ">
                Security Answer:
                <br />
                <asp:TextBox ID="txtSecurityAnswer" runat="server" TextMode="MultiLine" class="fildboxstaff"
                    Style="height: 50px;"></asp:TextBox><%--<asp:RequiredFieldValidator ID="RequiredFieldValidator18"
                        Font-Size="0" runat="server" ControlToValidate="txtSecurityAnswer" ErrorMessage="Please enter Security Answer."
                        Text="*" ValidationGroup="StaffValidate"></asp:RequiredFieldValidator>--%>
            </div>
            <div class="box">
                Comments:
                <br />
                <asp:TextBox ID="txtComment" TextMode="MultiLine" runat="server" CssClass="fildboxstaff"
                    Style="height: 50px;"></asp:TextBox>
            </div>
            <div class="clear">
            </div>
            <div class="box pndR15 pndbNone">
                Passcode:<br />
                <asp:TextBox ID="txtCode" runat="server" TextMode="Password" Style="width: 90px;"
                    CssClass="fildboxstaff" onkeypress="return isNumberKey(this,event);"></asp:TextBox><%--<asp:RequiredFieldValidator
                        ID="RequiredFieldValidator4" runat="server" Font-Size="0" ControlToValidate="txtCode"
                        ErrorMessage="Please enter Passcode." Text="*" ValidationGroup="StaffValidate"
                        SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                <asp:CustomValidator ID="CustomValidator2" runat="server" ValidationGroup="StaffValidate"
                    ControlToValidate="txtCode" ErrorMessage="Passcode require 4 digit number." Text="*"
                    Font-Size="0" SetFocusOnError="true" ClientValidationFunction="CheckCodeNumOfChar"></asp:CustomValidator>
                <img src="../images/quation.png" alt="4 digit number" title="enter 4 digit number." /></div>
            <div class="box pndR15 pndbNone">
                Active:
                <br />
                <asp:RadioButton ID="rdActive" runat="server" Checked="true" GroupName="Status" />Active
                &nbsp;
                <asp:RadioButton ID="rdInactive" runat="server" GroupName="Status" />Inactive
            </div>
            <%--<div class="box pndR15 pndbNone">
                Primary:
                <br />
                <asp:CheckBox ID="chkIsPrimary" runat="server" />
            </div>--%>
            <div class="clear">
            </div>
        </div>
    </fieldset>
    <div class="clear">
    </div>
    <div class="right" style="padding-bottom: 15px;">
        <asp:Button ID="btnCancel" runat="server" CssClass="btn" Text="Cancel" CausesValidation="false"
            OnClick="btnCancel_Click" />
    </div>
    <div class="right" style="padding-bottom: 15px; padding-right: 15px;">
        <asp:Button ID="btnSave" runat="server" CssClass="btn" Text="Save" ValidationGroup="StaffValidate"
            OnClick="btnSave_Click" /></div>
    <asp:ValidationSummary ID="valSum" runat="server" ValidationGroup="StaffValidate"
        ShowMessageBox="true" ShowSummary="false" />
    <telerik:RadAjaxManagerProxy ID="AjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="ddlCountry">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="ddlState" LoadingPanelID="RAPL1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <telerik:RadAjaxLoadingPanel ID="RAPL1" runat="server" Transparency="10">
    </telerik:RadAjaxLoadingPanel>
</asp:Content>
