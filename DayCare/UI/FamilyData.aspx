<%@ Page Title="" Language="C#" MasterPageFile="~/UI/DayCareIndex.Master" AutoEventWireup="true"
    CodeBehind="FamilyData.aspx.cs" Inherits="DayCare.UI.FamilyData" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/UI/UserControls/MenuLink.ascx" TagName="UCMenuLink" TagPrefix="ucl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="clear">
    </div>
    <h3 class="title">
        <ucl:UCMenuLink ID="usrMenuLink" runat="server" />
    </h3>
    <div class="clear">
    </div>

    <script type="text/javascript" language="javascript">
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

            var txtUserName = document.getElementById("ctl00_ContentPlaceHolder1_txtUserName");
            var txtPassword = document.getElementById("ctl00_ContentPlaceHolder1_txtPassword");
            if (sender.controltovalidate == "ctl00_ContentPlaceHolder1_txtUserName") {
                if (txtUserName.value.length > 6) {
                    args.IsValid = true;
                }
                else {
                    args.IsValid = false;

                }
            }
            if (sender.controltovalidate == "ctl00_ContentPlaceHolder1_txtPassword") {
                if (txtPassword.value.length > 6) {
                    args.IsValid = true;
                }
                else {
                    args.IsValid = false;
                }
            }
            return true;
        }
    </script>
        <fieldset >
            <legend><strong>Personal Details</strong></legend>
            <div class="fieldDiv">
            <div class="box pndR15" style="height: 170px;">
                <div class="left" style="padding: 8px 65px;">
                    <div class="photo">
                        <asp:Image ID="imgFamily" runat="server" ImageUrl="~/FamilyImages/male_photo.png"
                            Width="99" Height="125" />
                    </div>
                </div>
                <div class="box ">
                    Image:
                    <br />
                    <asp:FileUpload ID="fupImage" runat="server" />
                    <asp:Label ID="lblImage" runat="server" Visible="false"></asp:Label>
                </div>
            </div>
            <div class="box pndR15">
                *First Name:
                <br />
                <asp:TextBox ID="txtFirstName" runat="server" CssClass="fildboxstaff"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtFirstName"
                    ErrorMessage="Please enter First Name." Text="*" Font-Size="0" ValidationGroup="FamilyValidate"></asp:RequiredFieldValidator>
            </div>
            <div class="box pndR15">
                *Last Name:
                <br />
                <asp:TextBox ID="txtLastName" runat="server" CssClass="fildboxstaff"></asp:TextBox><asp:RequiredFieldValidator
                    ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtLastName" ErrorMessage="Please enter Last Name."
                    Text="*" Font-Size="0" ValidationGroup="FamilyValidate"></asp:RequiredFieldValidator>
            </div>
            <div class="box">
                *Gender:
                <br />
                <asp:RadioButton ID="rdMale" runat="server" Checked="true" GroupName="Gender" />Male
                &nbsp;
                <asp:RadioButton ID="rdFemale" runat="server" GroupName="Gender" />Female
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
            <div class="box ">
                City:<br />
                <asp:TextBox ID="txtCity" runat="server" CssClass="fildboxstaff"></asp:TextBox>
            </div>
            <div class="box pndR15">
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
            <div class="box ">
                State:
                <br />
                <asp:DropDownList ID="ddlState" CssClass="fildboxstaff" runat="server" Width="230px">
                </asp:DropDownList>
            </div>
            <div class="box pndR15">
                *Main Phone:<br />
                <asp:TextBox ID="txtMainPhone" runat="server" CssClass="fildboxstaff"></asp:TextBox><asp:RequiredFieldValidator
                    ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtMainPhone"
                    ErrorMessage="Please enter Main Phone." Text="*" Font-Size="0" ValidationGroup="FamilyValidate"></asp:RequiredFieldValidator>
            </div>
            <div class="box pndR15">
                Secondary Phone:<br />
                <asp:TextBox ID="txtSecondaryPhone" runat="server" CssClass="fildboxstaff"></asp:TextBox>
            </div>
            <div class="box ">
                Fax:
                <br />
                <asp:TextBox ID="txtFax" runat="server" CssClass="fildboxstaff"></asp:TextBox>
            </div>
            <div class="box pndR15">
            </div>
            <div class="box pndR15">
                *Email:
                <br />
                <asp:TextBox ID="txtEmail" runat="server" CssClass="fildboxstaff"></asp:TextBox><asp:RequiredFieldValidator
                    ID="RequiredFieldValidator12" runat="server" Display="Dynamic" ControlToValidate="txtEmail"
                    Font-Size="0" ErrorMessage="Please enter Email." Text="*" ValidationGroup="FamilyValidate"></asp:RequiredFieldValidator><asp:RegularExpressionValidator
                        ID="rev" ControlToValidate="txtEmail" runat="server" Text="*" ErrorMessage="Please Enter Valid Email Id."
                        Display="Dynamic" ValidationGroup="FamilyValidate" Font-Size="0" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator>
            </div>
            <div class="box">
                Id Info:
                <br />
                <asp:TextBox ID="txtIdInfo" runat="server" CssClass="fildboxstaff"></asp:TextBox>
            </div>
            </div>
        </fieldset>
        <fieldset>
            <legend><strong>User Details</strong></legend>
            <div class="fieldDiv"> 
            <div class="box pndR15">
                *Relationship:
                <br />
                <asp:DropDownList ID="ddlRelationship" runat="server" CssClass="fildboxstaff" Width="230px">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfv1" runat="server" ControlToValidate="ddlRelationship"
                    Font-Size="0" ErrorMessage="Please select Relationship." InitialValue="00000000-0000-0000-0000-000000000000"
                    Text="*" ValidationGroup="FamilyValidate"></asp:RequiredFieldValidator>
            </div>
            <div class="box pndR15">
                *User Name:
                <br />
                <asp:TextBox ID="txtUserName" runat="server" onkeypress="javascript:return RistricChar(this,event);"
                    CssClass="fildboxstaff"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator13"
                        runat="server" ControlToValidate="txtUserName" Font-Size="0" ErrorMessage="Please enter User Name."
                        Text="*" ValidationGroup="FamilyValidate"></asp:RequiredFieldValidator>
                <asp:CustomValidator ID="cv" runat="server" ValidationGroup="FamilyValidate" ControlToValidate="txtUserName"
                    ErrorMessage="User Name minimum 6 character." Text="*" Font-Size="0" SetFocusOnError="true"
                    ClientValidationFunction="CheckNumOfChar"></asp:CustomValidator>
            </div>
            <div class="box pndR15">
                *Password:
                <br />
                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="fildboxstaff"
                    onkeypress="javascript:return RistricChar(this,event);"></asp:TextBox><asp:RequiredFieldValidator
                        ID="RequiredFieldValidator14" runat="server" ControlToValidate="txtPassword"
                        Font-Size="0" ErrorMessage="Please enter Password." Text="*" ValidationGroup="FamilyValidate"></asp:RequiredFieldValidator>
                <asp:CustomValidator ID="CustomValidator1" runat="server" ValidationGroup="FamilyValidate"
                    ControlToValidate="txtPassword" ErrorMessage="Password minimum 6 character."
                    Text="*" Font-Size="0" SetFocusOnError="true" ClientValidationFunction="CheckNumOfChar"></asp:CustomValidator>
            </div>
            <div class="box ">
                Code:<br />
                <asp:TextBox ID="txtCode" runat="server" Style="width: 90px;" CssClass="fildboxstaff"
                    onkeypress="return isNumberKey(this,event);"></asp:TextBox></div>
            <div class="clear">
            </div>
            <div class="box pndR15">
                *Security Question:
                <br />
                <asp:TextBox ID="txtSecurityQuestion" runat="server" TextMode="MultiLine" class="fildboxstaff"
                    Style="height: 50px;"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator17"
                        Font-Size="0" runat="server" ControlToValidate="txtSecurityQuestion" ErrorMessage="Please enter Security Question."
                        Text="*" ValidationGroup="FamilyValidate"></asp:RequiredFieldValidator>
            </div>
            <div class="box pndR15 ">
                *Security Answer:
                <br />
                <asp:TextBox ID="txtSecurityAnswer" runat="server" TextMode="MultiLine" class="fildboxstaff"
                    Style="height: 50px;"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator18"
                        Font-Size="0" runat="server" ControlToValidate="txtSecurityAnswer" ErrorMessage="Please enter Security Answer."
                        Text="*" ValidationGroup="FamilyValidate"></asp:RequiredFieldValidator>
            </div>
            <div class="box pndR15">
                Comments:
                <br />
                <asp:TextBox ID="txtFamilyDataComment" TextMode="MultiLine" runat="server" CssClass="fildboxstaff"
                    Style="height: 50px;"></asp:TextBox>
            </div>
            <div class="box ">
                Active:
                <br />
                <asp:RadioButton ID="rdActive" runat="server" Checked="true" GroupName="Status" />Active
                &nbsp;
                <asp:RadioButton ID="rdInactive" runat="server" GroupName="Status" />Inactive
            </div>
           
            <div class="clear">
            </div>
             </div>
        </fieldset>
    </div>
    <asp:Label ID="lblChildFamilyId" runat="server" Visible="false"></asp:Label>
    <%--<div class="detailsbox ">
        <fieldset class="fieldset">
            <legend><strong>Family Message</strong></legend>
            <div class="box pndR15">
                Message Start date:
                <br />
                <telerik:RadDatePicker ID="rdpMsgStartDate" runat="server" Width="100px">
                    <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x">
                    </Calendar>
                    <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                    <DateInput DisplayDateFormat="M/d/yyyy" DateFormat="M/d/yyyy">
                    </DateInput>
                </telerik:RadDatePicker>
                
            </div>
            <div class="box pndR15">
                Message Start date:
                <br />
                <telerik:RadDatePicker ID="rdpMsgEndDate" runat="server" Width="100px">
                    <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x">
                    </Calendar>
                    <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                    <DateInput DisplayDateFormat="M/d/yyyy" DateFormat="M/d/yyyy">
                    </DateInput>
                </telerik:RadDatePicker>
                            </div>
            <div class="box pndR15">
                Meassage Active:
                <br />
                <asp:CheckBox ID="chkMsgActive" runat="server" />
            </div>
            <div class="clear">
            </div>
            <div class="box pndR15 ">
                Message:
                <br />
                <asp:TextBox ID="txtMessage" runat="server" TextMode="MultiLine" class="fildboxstaff"
                    Style="height: 50px;"></asp:TextBox>
            </div>
            <div class="box pndR15">
                Comments:
                <br />
                <asp:TextBox ID="txtChildFamilyComments" TextMode="MultiLine" runat="server" CssClass="fildboxstaff"
                    Style="height: 50px;"></asp:TextBox>
            </div>
            <asp:Label ID="lblChildFamilyId" runat="server" Visible="false"></asp:Label>
            <div class="clear">
            </div>
        </fieldset>
    </div>--%>
    <div class="clear">
    </div>
    <div class="clear">
    </div>
    <div class="right" style="padding-top: 15px; padding-bottom: 15px;">
        <asp:Button ID="btnCancel" runat="server" CssClass="btn" Text="Cancel" CausesValidation="false"
            OnClick="btnCancel_Click" />
    </div>
    <div class="right" style="padding-top: 15px; padding-bottom: 15px; padding-right: 15px;">
        <asp:Button ID="btnSave" runat="server" CssClass="btn" Text="Save" ValidationGroup="FamilyValidate"
            OnClick="btnSave_Click" /></div>
    <asp:ValidationSummary ID="valSum" runat="server" ValidationGroup="FamilyValidate"
        ShowMessageBox="true" ShowSummary="false" />
    <asp:HiddenField ID="hdnName" runat="server" />
    <asp:HiddenField ID="hdnCode" runat="server" />
    <telerik:RadAjaxManagerProxy ID="AjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="ddlCountry">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="ddlState" LoadingPanelID="RAPL1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <telerik:RadAjaxLoadingPanel ID="RAPL1" runat="server"  Transparency="10">
    </telerik:RadAjaxLoadingPanel>
</asp:Content>
