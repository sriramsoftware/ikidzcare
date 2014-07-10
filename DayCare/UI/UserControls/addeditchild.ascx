<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="addeditchild.ascx.cs"
    Inherits="DayCare.UI.UserControls.addeditchild" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<fieldset>

    

    <legend><strong>Child Details</strong></legend>
    <div class="fieldDiv">
        <div class="box pndR15" style="height: 170px;">
            <div class="left" style="padding: 8px 65px;">
                <div class="photo">
                    <asp:Image ID="imgStaff" runat="server" ImageUrl="~/StaffImages/male_photo.png" Width="99"
                        Height="125" />
                </div>
            </div>
            <div class="box ">
                Image:
                <br />
                <%--<asp:FileUpload ID="fupImage" runat="server" EnableViewState="true" />--%>
                <telerik:RadUpload ID="fupImage" runat="server" ControlObjectsVisibility="None" AllowedFileExtensions=".jpg,.jpeg,.PNG,.png,.JPG,.JPEG"
                    InitialFileInputsCount="1" Localization-Select="Browse" MaxFileInputsCount="1"
                    OverwriteExistingFiles="True" Width="270px" BorderWidth="0px" BorderStyle="Solid"
                    BackColor="Transparent" InputSize="33" Height="22px" Style="margin-left: 5px;">
                    <Localization Select="Browse" />
                </telerik:RadUpload>
                <telerik:RadProgressArea ID="RadProgressArea1" runat="server" EnableAjaxSkinRendering="true" />
                <telerik:RadProgressManager ID="radprogressmanager1" runat="server" EnableAjaxSkinRendering="true" />
                <asp:Label ID="lblImage" runat="server" Visible="false"></asp:Label>
            </div>
        </div>
        <div class="box pndR15">
            <span class="red">*</span>First Name:
            <br />
            <asp:TextBox ID="txtFirstName" runat="server" CssClass="fildboxstaff"></asp:TextBox>
           <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtFirstName"
                ErrorMessage="Please enter First Name." Text="*" Font-Size="0" ValidationGroup="StaffValidate"></asp:RequiredFieldValidator>
        </div>
        <div class="box pndR15">
            <span class="red">*</span>Last Name:
            <br />
            <asp:TextBox ID="txtLastName" runat="server" CssClass="fildboxstaff"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtLastName"
                ErrorMessage="Please enter Last Name." Text="*" Font-Size="0" ValidationGroup="StaffValidate"></asp:RequiredFieldValidator>
        </div>
        <div class="box ">
            <div class=" left">
                <span class="red">*</span>Date Of Birth:
                <br />
                <telerik:RadDatePicker ID="rdpDOB" runat="server" Width="100px">
                    <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x">
                    </Calendar>
                    <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                    <DateInput DisplayDateFormat="M/d/yyyy" DateFormat="M/d/yyyy">
                    </DateInput>
                </telerik:RadDatePicker>
               <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="rdpDOB"
                    ErrorMessage="Please enter Date Of Birth." Text="*" Font-Size="0" ValidationGroup="StaffValidate"></asp:RequiredFieldValidator></div>
            <div class=" left">
                Gender:
                <br />
                <asp:RadioButton ID="rdMale" runat="server" Checked="true" GroupName="Gender" />Male
                &nbsp;
                <asp:RadioButton ID="rdFemale" runat="server" GroupName="Gender" />Female
            </div>
        </div>
        <div class="box pndR15" style="display: none;">
            Social Security ?
            <asp:TextBox ID="txtSocSec" runat="server" CssClass="fildboxstaff"></asp:TextBox>
        </div>
        <div class="box pndR15">
            Comments:<br />
            <asp:TextBox ID="txtComments" runat="server" TextMode="MultiLine" CssClass="fildboxstaff"></asp:TextBox>
        </div>
        <div class="box">
            Active:<br />
            <asp:CheckBox ID="chkActive" runat="server" ToolTip="Checked True Then Active" />
            &nbsp;
        </div>
        <div class="right" style="padding-top: 25px;">
            <div class=" left">
                <asp:Button ID="btnSave" runat="server" Text="Save  " OnClick="btnSave_Click" CommandName='<%# (Container is GridEditFormInsertItem) ? "Update" : "PerformInsert" %>'
                    CausesValidation="true" CssClass="btn" ValidationGroup="StaffValidate"  /></div>
            <div class=" left" style="padding-left: 15px;">
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false"
                    CommandName="Cancel" CssClass="btn" /></div>
        </div>
    </div>
    <asp:ValidationSummary ID="valSum" runat="server" ValidationGroup="StaffValidate"
        ShowMessageBox="true" ShowSummary="false" />
    <telerik:RadAjaxManagerProxy ID="AjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnSave">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="fupImage" LoadingPanelID="RAPL1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <telerik:RadAjaxLoadingPanel ID="RAPL1" runat="server">
    </telerik:RadAjaxLoadingPanel>
</fieldset>
