<%@ Page Title="" Language="C#" MasterPageFile="~/UI/DayCareIndex.Master" AutoEventWireup="true"
    CodeBehind="SchoolInfo.aspx.cs" Inherits="DayCare.UI.SchoolInfo" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript" language="javascript">
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if ((charCode >= 48 && charCode <= 57)) {
                return true;
            }
            else {
                return false;
            }
        }
    </script>

    <div class="clear">
    </div>
    <h3 class="title">
        <img src="../images/arrow.png" />&nbsp;School Information</h3>
    <div class="clear">
    </div>
    <fieldset>
        <legend><strong>School Details</strong></legend>
        <div class="fieldDiv">
            <div class="box pndR15">
                <span class="red">*</span>Name:<br />
                <asp:TextBox ID="txtName" CssClass="fildboxstaff" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtName"
                    ErrorMessage="Please enter Name." Text="*" Font-Size="0" ValidationGroup="SchoolValidate"></asp:RequiredFieldValidator>
            </div>
            <div class="box pndR15">
                <span class="red">*</span>Address1:<br />
                <asp:TextBox ID="txtAddress1" CssClass="fildboxstaff" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtAddress1"
                    ErrorMessage="Please enter Address1." Text="*" Font-Size="0" ValidationGroup="SchoolValidate"></asp:RequiredFieldValidator>
            </div>
            <div class="box pndR15">
                Address2:<br />
                <asp:TextBox ID="txtAddress2" CssClass="fildboxstaff" runat="server"></asp:TextBox>
            </div>
            <div class="box ">
                <span class="red">*</span>City:<br />
                <asp:TextBox ID="txtCity" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtCity"
                    ErrorMessage="Please enter City." Text="*" Font-Size="0" ValidationGroup="SchoolValidate"></asp:RequiredFieldValidator>
            </div>
            <div class="clear">
            </div>
            <div class="box pndR15">
                Zip:<br />
                <asp:TextBox ID="txtZip" CssClass="fildboxstaff" runat="server"></asp:TextBox>
            </div>
            <div class="box pndR15">
                <span class="red">*</span>Country:
                <br />
                <asp:DropDownList ID="ddlCountry" runat="server" CssClass="fildboxstaff" Width="230px"
                    AutoPostBack="True" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfv1" runat="server" ControlToValidate="ddlCountry"
                    SetFocusOnError="true" Font-Size="0" ErrorMessage="Please select country." InitialValue="00000000-0000-0000-0000-000000000000"
                    Text="*" ValidationGroup="SchoolValidate"></asp:RequiredFieldValidator>
            </div>
            <div class="box pndR15 ">
                <span class="red">*</span>State:
                <br />
                <asp:DropDownList ID="ddlState" CssClass="fildboxstaff" runat="server" Width="230px">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlState"
                    SetFocusOnError="true" Font-Size="0" ErrorMessage="Please select state." InitialValue="00000000-0000-0000-0000-000000000000"
                    Text="*" ValidationGroup="SchoolValidate"></asp:RequiredFieldValidator>
            </div>
            <div class="box">
                <span class="red">*</span>Main Phone:<br />
                <asp:TextBox ID="txtMainPhone" CssClass="fildboxstaff" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtMainPhone"
                    ErrorMessage="Please enter Main Phone." Text="*" Font-Size="0" ValidationGroup="SchoolValidate"></asp:RequiredFieldValidator>
            </div>
            <div class="clear">
            </div>
            <div class="box pndR15">
                Secondary Phone:<br />
                <asp:TextBox ID="txtSecondaryPhone" CssClass="fildboxstaff" runat="server"></asp:TextBox>
            </div>
            <div class="box pndR15">
                Fax:<br />
                <asp:TextBox ID="txtFax" CssClass="fildboxstaff" runat="server"></asp:TextBox>
            </div>
            <div class="box pndR15">
                <span class="red">*</span>Email:<br />
                <asp:TextBox ID="txtEmail" CssClass="fildboxstaff" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtEmail"
                    ErrorMessage="Please enter Email." Text="*" Font-Size="0" ValidationGroup="SchoolValidate"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="rev1" runat="server" ControlToValidate="txtEmail"
                    SetFocusOnError="true" ErrorMessage="Please enter valid email."
                    Text="*" Font-Size="0" ValidationGroup="SchoolValidate" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
            </div>
            <div class="clear">
            </div>
            <div class="box pndR15">
                WebSite:<br />
                <asp:TextBox ID="txtWebSite" CssClass="fildboxstaff" runat="server"></asp:TextBox>
            </div>
            <div class="box pndR15">
                Passcode required:&nbsp;
                <asp:CheckBox ID="chkCodeRequire" runat="server" />
            </div>
            <div class="box pndR15">
                <span class="red">*</span>Minimum overdue balance amount:<br />
                <asp:TextBox ID="txtLateFee" CssClass="fildboxstaff" runat="server"></asp:TextBox>
                <asp:RegularExpressionValidator ID="rev" runat="server" ControlToValidate="txtLateFee"
                    SetFocusOnError="true" ErrorMessage="Please enter valid overdue balance amount."
                    Text="*" Font-Size="0" ValidationGroup="SchoolValidate" ValidationExpression="(^-?\d\d*\.\d*$)|(^-?\d\d*$)|(^-?\.\d\d*$)"></asp:RegularExpressionValidator>
            </div>
        </div>
    </fieldset>
    <fieldset>
        <legend><strong>iPad Details</strong></legend>
        <div class="fieldDiv">
            <div class="box pndR15" style="height: 180px;">
                <div class="left" style="padding: 8px 65px;">
                    <div class="photo">
                        <asp:Image ID="imgSchholImage" ImageUrl="~/StaffImages/Filetype-Blank-Alt-icon.png"
                            runat="server" Width="99" Height="125" />
                    </div>
                </div>
                <div class="box ">
                    Background Image::
                    <br />
                    <asp:FileUpload ID="fupiPadBackgroundImage" runat="server" />
                    <asp:Label ID="lbliPadBackgroundImage" runat="server" Visible="false"></asp:Label>
                </div>
            </div>
            <div class="left">
                <div class="box pndR15" style="padding-bottom: 20px;">
                    Header Font:<br />
                    <%--<asp:TextBox ID="txtiPadHeaderFont" CssClass="fildboxstaff" runat="server" Visible="false"></asp:TextBox>--%>
                    <asp:DropDownList ID="ddliPadHeaderFont" CssClass="fildboxstaff" runat="server">
                    </asp:DropDownList>
                </div>
                <div class="box pndR15">
                    Header Font Size:<br />
                    <%--<asp:TextBox ID="txtiPadHeaderFontSize" CssClass="fildboxstaff" runat="server" Visible="false"
                    onkeypress="return isNumberKey(event);"></asp:TextBox>--%>
                    <asp:DropDownList ID="ddliPadHeaderFontSize" CssClass="fildboxstaff" runat="server">
                        <asp:ListItem Text="--Select--" Value="-1"></asp:ListItem>
                        <asp:ListItem Text="10" Value="10"></asp:ListItem>
                        <asp:ListItem Text="11" Value="11"></asp:ListItem>
                        <asp:ListItem Text="12" Value="12"></asp:ListItem>
                        <asp:ListItem Text="13" Value="13"></asp:ListItem>
                        <asp:ListItem Text="14" Value="14"></asp:ListItem>
                        <asp:ListItem Text="15" Value="15"></asp:ListItem>
                        <asp:ListItem Text="16" Value="16"></asp:ListItem>
                        <asp:ListItem Text="17" Value="17"></asp:ListItem>
                        <asp:ListItem Text="18" Value="18"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="box">
                    Header:<br />
                    <asp:TextBox ID="txtiPadHeader" CssClass="fildboxstaff" runat="server"></asp:TextBox>
                </div>
                <div class="clear">
                </div>
                <div class="box pndR15">
                    Message Font Size:<br />
                    <%--<asp:TextBox ID="txtiPadMessageFontSize" CssClass="fildboxstaff" runat="server" Visible="false"
                    onkeypress="return isNumberKey(event);"></asp:TextBox>--%>
                    <asp:DropDownList ID="ddliPadMessageFontSize" CssClass="fildboxstaff" runat="server">
                        <asp:ListItem Text="--Select--" Value="-1"></asp:ListItem>
                        <asp:ListItem Text="10" Value="10"></asp:ListItem>
                        <asp:ListItem Text="11" Value="11"></asp:ListItem>
                        <asp:ListItem Text="12" Value="12"></asp:ListItem>
                        <asp:ListItem Text="13" Value="13"></asp:ListItem>
                        <asp:ListItem Text="14" Value="14"></asp:ListItem>
                        <asp:ListItem Text="15" Value="15"></asp:ListItem>
                        <asp:ListItem Text="16" Value="16"></asp:ListItem>
                        <asp:ListItem Text="17" Value="17"></asp:ListItem>
                        <asp:ListItem Text="18" Value="18"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="box pndR15">
                    Message Font:<br />
                    <%--<asp:TextBox ID="txtiPadMessageFont" CssClass="fildboxstaff" runat="server" Visible="false"></asp:TextBox>--%>
                    <asp:DropDownList ID="ddliPadMessageFont" CssClass="fildboxstaff" runat="server">
                    </asp:DropDownList>
                </div>
                <div class="box ">
                    Message:<br />
                    <asp:TextBox ID="txtiPadMessage" CssClass="fildboxstaff" runat="server" TextMode="MultiLine"></asp:TextBox>
                </div>
                <div class="clear">
                </div>
                <div class="box pndR15">
                    Header Color:<br />
                    <asp:Label ID="lbliPadHeaderColor" Height="25" Width="25" runat="server" Visible="false"></asp:Label>
                    <telerik:RadColorPicker ID="rcpiPadHeaderColor" runat="server" ShowIcon="true" CurrentColorText=""
                        PickColorText="" ToolTip="">
                    </telerik:RadColorPicker>
                </div>
                <div class="box ">
                    Message Color:<br />
                    <asp:Label ID="lbliPadMessageColor" Height="25" Width="25" runat="server" Visible="false"></asp:Label>
                    <telerik:RadColorPicker ID="rcpiPadMessageColor" runat="server" PickColorText=""
                        CurrentColorText="" ShowIcon="true">
                    </telerik:RadColorPicker>
                </div>
            </div>
        </div>
    </fieldset>
    <%--<div align="center" width="100%">
        <table align="center" width="100%" style="border: solid 1px;">
            <tr>
                <td align="left">
                    *Name:<br />
                    <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtName"
                        ErrorMessage="Please enter Name." Text="*" Font-Size="0" ValidationGroup="SchoolValidate"></asp:RequiredFieldValidator>
                </td>
                <td align="left">
                    *Address1:<br />
                    <asp:TextBox ID="txtAddress1" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtAddress1"
                        ErrorMessage="Please enter Address1." Text="*" Font-Size="0" ValidationGroup="SchoolValidate"></asp:RequiredFieldValidator>
                </td>
                <td align="left">
                    Address2:<br />
                    <asp:TextBox ID="txtAddress2" runat="server"></asp:TextBox>
                    
                </td>
                <td align="left">
                    *City:<br />
                    <asp:TextBox ID="txtCity" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtCity"
                        ErrorMessage="Please enter City." Text="*" Font-Size="0" ValidationGroup="SchoolValidate"></asp:RequiredFieldValidator>
                </td>
                <td align="left">
                    Zip:<br />
                    <asp:TextBox ID="txtZip" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="left">
                    Country:
                    <br />
                    <asp:DropDownList ID="ddlCountry" runat="server" CssClass="fildboxstaff" Width="230px"
                        AutoPostBack="True" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td align="left">
                    State:
                    <br />
                    <asp:DropDownList ID="ddlState" CssClass="fildboxstaff" runat="server" Width="230px">
                    </asp:DropDownList>
                </td>
                <td align="left">
                    *Main Phone:<br />
                    <asp:TextBox ID="txtMainPhone" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtMainPhone"
                        ErrorMessage="Please enter Main Phone." Text="*" Font-Size="0" ValidationGroup="SchoolValidate"></asp:RequiredFieldValidator>
                </td>
                <td align="left">
                    Secondary Phone:<br />
                    <asp:TextBox ID="txtSecondaryPhone" runat="server"></asp:TextBox>
                </td>
                <td align="left">
                    Fax:<br />
                    <asp:TextBox ID="txtFax" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="left">
                    *Email:<br />
                    <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtEmail"
                        ErrorMessage="Please enter Email." Text="*" Font-Size="0" ValidationGroup="SchoolValidate"></asp:RequiredFieldValidator>
                </td>
                <td align="left">
                    WebSite:<br />
                    <asp:TextBox ID="txtWebSite" runat="server"></asp:TextBox>
                    
                </td>
                <td align="left">
                    Code Required:<br />
                    <asp:CheckBox ID="chkCodeRequire" runat="server" />
                </td>
            </tr>
        </table>
        <br />
        <table width="100%" align="center" style="border: solid 1px;">
            <tr>
                <td align="left">
                    iPad Header:<br />
                    <asp:TextBox ID="txtiPadHeader" runat="server"></asp:TextBox>
                </td>
                <td align="left">
                    iPad Header Font:<br />
                    <asp:TextBox ID="txtiPadHeaderFont" runat="server"></asp:TextBox>
                </td>
                <td align="left">
                    iPad Header Font Size:<br />
                    <asp:TextBox ID="txtiPadHeaderFontSize" runat="server" onkeypress="return isNumberKey(event);"></asp:TextBox>
                </td>
                <td align="left">
                    iPad Header Color:<br />
                    <asp:Label ID="lbliPadHeaderColor" Height="25" Width="25" runat="server" Visible="false"></asp:Label>
                    <telerik:RadColorPicker ID="rcpiPadHeaderColor" runat="server" ShowIcon="true" CurrentColorText=""
                        PickColorText="" ToolTip="">
                    </telerik:RadColorPicker>
                </td>
                <td align="left">
                    iPad Message:<br />
                    <asp:TextBox ID="txtiPadMessage" runat="server" TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="left">
                    iPad Message Font:<br />
                    <asp:TextBox ID="txtiPadMessageFont" runat="server"></asp:TextBox>
                </td>
                <td align="left">
                    iPad Message Font Size:<br />
                    <asp:TextBox ID="txtiPadMessageFontSize" runat="server" onkeypress="return isNumberKey(event);"></asp:TextBox>
                </td>
                <td align="left">
                    iPad Background Image:<br />
                    <asp:FileUpload ID="fupiPadBackgroundImage" runat="server" />
                    <asp:Label ID="lbliPadBackgroundImage" runat="server" Visible="false"></asp:Label>
                </td>
                <td align="left">
                    iPad Message Color:<br />
                    <asp:Label ID="lbliPadMessageColor" Height="25" Width="25" runat="server" Visible="false"></asp:Label>
                    <telerik:RadColorPicker ID="rcpiPadMessageColor" runat="server" PickColorText=""
                        CurrentColorText="" ShowIcon="true">
                    </telerik:RadColorPicker>
                </td>
            </tr>
            </tr>
        </table>
        <asp:Image ID="imgSchholImage" runat="server" />
       
    </div>--%>
    <div class="clear">
    </div>
    <div class="right" style="padding-bottom: 15px;">
        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn" OnClick="btnCancel_Click"
            CausesValidation="false" />
    </div>
    <div class="right" style="padding-bottom: 15px; padding-right: 15px;">
        <asp:Button ID="btnSave" runat="server" CssClass="btn" Text="Save" ValidationGroup="SchoolValidate"
            OnClick="btnSave_Click" /></div>
    <asp:ValidationSummary ID="valSum" runat="server" ValidationGroup="SchoolValidate"
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
