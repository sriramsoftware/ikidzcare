<%@ Page Title="" Language="C#" MasterPageFile="~/UI/DayCareIndex.Master" AutoEventWireup="true"
    CodeBehind="FamilyInfo.aspx.cs" Inherits="DayCare.UI.FamilyInfo" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/UI/UserControls/MenuLink.ascx" TagName="UCMenuLink" TagPrefix="ucl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <link href="../css/DayCare.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" language="javascript">
        var popUp;
        function PopUpShowing(sender, eventArgs) {
            popUp = eventArgs.get_popUp();

            //popUp.style.left = "13%";
            //popUp.style.vertical-align='middle';
            //            popUp.style.top = "60%";
            var popUp = eventArgs.get_popUp();

            var popUpWidth = popUp.style.width.substr(0, popUp.style.width.indexOf("px"));

            var popUpHeight = popUp.style.height.substr(0, popUp.style.height.indexOf("px"));

            var windowHeight = (typeof window.innerHeight != 'undefined' ? window.innerHeight : document.body.offsetHeight);

            var windowWidth = document.body.offsetWidth;



            if (popUpHeight == "") popUpHeight = 300; // if the height isn't set on the popup, default to 300px     

            popUp.style.position = "fixed";

            popUp.style.left = (Math.floor((windowWidth - popUpWidth) / 2)).toString() + "px";

            popUp.style.top = (Math.floor((windowHeight - popUpHeight) / 2)).toString() + "px";
        }
        function isNumberKey(obj, evt) {
            //            var charCode = (evt.which) ? evt.which : event.keyCode
            //            if (charCode == 46 || charCode == 8 || (charCode >= 48 && charCode <= 57)) {
            //                if (obj != null) {
            //                    if (charCode == 8 || obj.value.length < 4) {//8= back space
            //                        return true;
            //                    }
            //                    else {
            //                        return false;
            //                    }
            //                }
            //                return false;
            //            }
            //            else
            //                return false;
            if (obj.value.length < 4) {
                return;
            }
            else {
                return false;
            }
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

        function CheckIsFamilyActive() {
            var chkActive = document.getElementById("ctl00_ContentPlaceHolder1_chkActive");
            try {
                if (document.URL.indexOf('ChildFamilyId') > -1) {
                    if (document.getElementById("ctl00_ContentPlaceHolder1_txtFirstNameGuardian1").value.length > 0 && document.getElementById("ctl00_ContentPlaceHolder1_txtLastNameGuardian1").value.length > 0) {
                        if (chkActive != null && chkActive.checked == false) {
                            if (confirm("If family will set inactive the children will")) {
                                return true;
                            }
                            else {
                                return false;
                            }
                        }
                    }
                }
            }
            catch (ex) { }
            return true;
        }
        
    </script>

    <div class="clear">
    </div>
    <h3 class="title">
        <ucl:UCMenuLink ID="usrMenuLink" runat="server" />
    </h3>
    <div class="clear">
    </div>
    <div class="innerMain">
        <div class="content">
            <fieldset style="width: 474px; margin-right: 16px;">
                <legend><strong>Guardian 1</strong></legend>
                <div class="fieldDiv" style="width: 462px;">
                    <div class="box3 pndR15" style="height: 180px;">
                        <div class="left" style="padding: 8px 65px;">
                            <div class="photo" style="border: 1px solid #666666;">
                                <asp:Image ID="imgFamilyGuardian1" runat="server" ImageUrl="~/FamilyImages/male_photo.png"
                                    Width="99" Height="125" />
                                <asp:Label ID="lblGuardian1FamilyId" runat="server" Visible="false"></asp:Label>
                            </div>
                        </div>
                        <div class="box3 ">
                            Image:
                            <br />
                            <asp:FileUpload ID="fupImageGuardian1" runat="server" />
                            <asp:Label ID="lblImageGuardian1" runat="server" Visible="false"></asp:Label>
                        </div>
                    </div>
                    <div class="box3">
                        <span class="red">*</span> First Name:
                        <br />
                        <asp:TextBox ID="txtFirstNameGuardian1" runat="server" CssClass="fildbox"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtFirstNameGuardian1"
                            SetFocusOnError="true" ErrorMessage="Please enter Guardian 1 First Name." Text="*"
                            Font-Size="0" ValidationGroup="FamilyValidate"></asp:RequiredFieldValidator>
                    </div>
                    <div class="box3">
                        <span class="red">*</span> Last Name:
                        <br />
                        <asp:TextBox ID="txtLastNameGuardian1" runat="server" CssClass="fildbox"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtLastNameGuardian1"
                            SetFocusOnError="true" ErrorMessage="Please enter Guardian 1 Last Name." Text="*"
                            Font-Size="0" ValidationGroup="FamilyValidate"></asp:RequiredFieldValidator>
                    </div>
                    <div class="box3 ">
                        Relationship:
                        <br />
                        <asp:DropDownList ID="ddlRelationshipGuardian1" runat="server" CssClass="fildboxstaff"
                            Width="230px">
                        </asp:DropDownList>
                    </div>
                    <div class="box3">
                        <!--<span class="red">*</span> -->
                        Email:<br />
                        <asp:TextBox ID="txtEmailGuardian1" runat="server" CssClass="fildbox"></asp:TextBox>
                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" Display="Dynamic"
                            SetFocusOnError="true" ControlToValidate="txtEmailGuardian1" Font-Size="0" ErrorMessage="Please enter Guardian 1 Email."
                            Text="*" ValidationGroup="FamilyValidate"></asp:RequiredFieldValidator>--%><asp:RegularExpressionValidator
                                ID="rev" ControlToValidate="txtEmailGuardian1" runat="server" Text="*" ErrorMessage="Please Enter Valid Guardian 1 Email."
                                SetFocusOnError="true" Display="Dynamic" ValidationGroup="FamilyValidate" Font-Size="0"
                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator>
                    </div>
                    <div class="box3 pndR15" style="padding-bottom: 0px;">
                        Phone Type:<br />
                        <asp:DropDownList ID="ddlPhoneType1Guardian1" runat="server" CssClass="fildboxstaff"
                            Width="230px">
                            <asp:ListItem Text="Work" Value="Work"></asp:ListItem>
                            <asp:ListItem Text="Mobile" Value="Mobile"></asp:ListItem>
                            <asp:ListItem Text="Other" Value="Other"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="box3">
                        <!--<span class="red">*</span> -->
                        Phone 1:
                        <br />
                        <asp:TextBox ID="txtPhone1Guardian1" runat="server" CssClass="fildbox"></asp:TextBox>
                        <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtPhone1Guardian1"
                            SetFocusOnError="true" ErrorMessage="Please enter Guardian 1 Phone 1." Text="*"
                            Font-Size="0" ValidationGroup="FamilyValidate"></asp:RequiredFieldValidator>--%>
                    </div>
                    <div class="box3 pndR15" style="padding-bottom: 0px;">
                        Phone Type:<br />
                        <asp:DropDownList ID="ddPhoneType2Guardian1" runat="server" CssClass="fildboxstaff"
                            Width="230px">
                            <asp:ListItem Text="Work" Value="Work"></asp:ListItem>
                            <asp:ListItem Text="Mobile" Value="Mobile"></asp:ListItem>
                            <asp:ListItem Text="Other" Value="Other"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="box3 " style="padding-bottom: 0px;">
                        Phone 2:<br />
                        <asp:TextBox ID="txtPhone2Guardian1" runat="server" CssClass="fildbox"></asp:TextBox>
                    </div>
                    <div class="clear">
                    </div>
                </div>
            </fieldset>
            <fieldset style="width: 474px;">
                <legend><strong>Guardian 2</strong></legend>
                <div class="fieldDiv" style="width: 462px;">
                    <div class="box3 pndR15" style="height: 180px;">
                        <div class="left" style="padding: 8px 65px;">
                            <div class="photo" style="border: 1px solid #666666;">
                                <asp:Image ID="imgFamilyGuardian2" runat="server" ImageUrl="~/FamilyImages/male_photo.png"
                                    Width="99" Height="125" />
                                <asp:Label ID="lblGuardian2FamilyId" runat="server" Visible="false"></asp:Label>
                            </div>
                        </div>
                        <div class="box3 ">
                            Image:
                            <br />
                            <asp:FileUpload ID="fupImageGuardian2" runat="server" />
                            <asp:Label ID="lblImageGuardian2" runat="server" Visible="false"></asp:Label>
                        </div>
                    </div>
                    <div class="box3">
                        First Name:
                        <br />
                        <asp:TextBox ID="txtFirstNameGuardian2" runat="server" CssClass="fildbox"></asp:TextBox>
                    </div>
                    <div class="box3">
                        Last Name:
                        <br />
                        <asp:TextBox ID="txtLastNameGuardian2" runat="server" CssClass="fildbox"></asp:TextBox>
                    </div>
                    <div class="box3 ">
                        Relationship:
                        <br />
                        <asp:DropDownList ID="ddlRelationshipGuardian2" runat="server" CssClass="fildboxstaff"
                            Width="230px">
                        </asp:DropDownList>
                    </div>
                    <div class="box3">
                        Email:<br />
                        <asp:TextBox ID="txtEmailGuardian2" runat="server" CssClass="fildbox"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtEmailGuardian2"
                            runat="server" Text="*" ErrorMessage="Please Enter Valid Guardian 2 Email." SetFocusOnError="true"
                            Display="Dynamic" ValidationGroup="FamilyValidate" Font-Size="0" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator>
                    </div>
                    <div class="box3 pndR15" style="padding-bottom: 0px;">
                        Phone Type:<br />
                        <asp:DropDownList ID="ddlPhoneType1Guardian2" runat="server" CssClass="fildboxstaff"
                            Width="230px">
                            <asp:ListItem Text="Work" Value="Work"></asp:ListItem>
                            <asp:ListItem Text="Mobile" Value="Mobile"></asp:ListItem>
                            <asp:ListItem Text="Other" Value="Other"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="box3">
                        Phone 1:
                        <br />
                        <asp:TextBox ID="txtPhone1Guardian2" runat="server" CssClass="fildbox"></asp:TextBox>
                    </div>
                    <div class="box3 pndR15" style="padding-bottom: 0px;">
                        Phone Type:<br />
                        <asp:DropDownList ID="ddlPhoneType2Guardian2" runat="server" CssClass="fildboxstaff"
                            Width="230px">
                            <asp:ListItem Text="Work" Value="Work"></asp:ListItem>
                            <asp:ListItem Text="Mobile" Value="Mobile"></asp:ListItem>
                            <asp:ListItem Text="Other" Value="Other"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="box3 " style="padding-bottom: 0px;">
                        Phone 2:<br />
                        <asp:TextBox ID="txtPhone2Guardian2" runat="server" CssClass="fildbox"></asp:TextBox>
                    </div>
                    <div class="clear">
                    </div>
                </div>
            </fieldset>
            <fieldset>
                <legend><strong>Family Info</strong></legend>
                <div class="fieldDiv">
                    <div class="box pndR15" style="display: none;">
                        <!--<span class="red">*</span> -->
                        Family Title:
                        <br />
                        <asp:TextBox ID="txtFamilyTitle" runat="server" CssClass="fildboxstaff"></asp:TextBox>
                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtFamilyTitle"
                            SetFocusOnError="true" Font-Size="0" ErrorMessage="Please enter Family Title."
                            Text="*" ValidationGroup="FamilyValidate"></asp:RequiredFieldValidator>--%>
                    </div>
                    <div class="box pndR15">
                        <!--<span class="red">*</span> -->
                        Username:
                        <br />
                        <asp:TextBox ID="txtUserName" runat="server" CssClass="fildboxstaff" onkeypress="javascript:return RistricChar(this,event);"></asp:TextBox>
                        <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txtUserName"
                            SetFocusOnError="true" Font-Size="0" ErrorMessage="Please enter User Name." Text="*"
                            ValidationGroup="FamilyValidate"></asp:RequiredFieldValidator>--%>
                        <asp:CustomValidator ID="cv" runat="server" ValidationGroup="FamilyValidate" ControlToValidate="txtUserName"
                            ErrorMessage="User Name minimum 6 character." Text="*" Font-Size="0" SetFocusOnError="true"
                            ClientValidationFunction="CheckNumOfChar"></asp:CustomValidator>
                    </div>
                    <div class="box pndR15">
                        <!--<span class="red">*</span> -->
                        Password:
                        <br />
                        <asp:TextBox ID="txtPassword" runat="server" CssClass="fildboxstaff" TextMode="Password"
                            onkeypress="javascript:return RistricChar(this,event);"></asp:TextBox>
                        <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="txtPassword"
                            SetFocusOnError="true" Font-Size="0" ErrorMessage="Please enter Password." Text="*"
                            ValidationGroup="FamilyValidate"></asp:RequiredFieldValidator>--%>
                        <asp:CustomValidator ID="CustomValidator1" runat="server" ValidationGroup="FamilyValidate"
                            ControlToValidate="txtPassword" ErrorMessage="Password minimum 6 character."
                            Text="*" Font-Size="0" SetFocusOnError="true" ClientValidationFunction="CheckNumOfChar"></asp:CustomValidator>
                    </div>
                    <div class="box pndR15">
                        Passcode:<br />
                        <asp:TextBox ID="txtCode" runat="server" TextMode="Password" Style="width: 90px;"
                            CssClass="fildboxstaff"></asp:TextBox>&nbsp;
                        <%--onkeypress="return isNumberKey(this,event);"--%>
                        <%--<asp:CompareValidator ID="cmpr" ControlToValidate="txtCode" ErrorMessage="Passcode require only numeric."
                            ValidationGroup="FamilyValidate" runat="server" Text="*" Font-Size="0" Operator="DataTypeCheck" Type="Integer"></asp:CompareValidator>--%>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ControlToValidate="txtCode"
                            runat="server" Text="*" ErrorMessage="Passcode require 4 digit number." SetFocusOnError="true"
                            ValidationGroup="FamilyValidate" Font-Size="0" ValidationExpression="[0-9][0-9][0-9][0-9]"></asp:RegularExpressionValidator>
                        <img src="../images/quation.png" alt="4 digit number" title="enter 4 digit number." />
                    </div>
                    <div class="box ">
                        Active:<br />
                        <asp:CheckBox ID="chkActive" runat="server" Checked="true" />
                    </div>
                    <div class=" clear">
                    </div>
                    <div class="box pndR15">
                        Address 1:<br />
                        <asp:TextBox ID="txtAddress1" runat="server" CssClass="fildboxstaff"></asp:TextBox>
                    </div>
                    <div class="box pndR15">
                        Address 2:
                        <br />
                        <asp:TextBox ID="txtAddress2" runat="server" CssClass="fildboxstaff"></asp:TextBox>
                    </div>
                    <div class="box pndR15">
                        City:
                        <br />
                        <asp:TextBox ID="txtCity" runat="server" CssClass="fildboxstaff"></asp:TextBox>
                    </div>
                    <div class="box">
                        Zip:
                        <br />
                        <asp:TextBox ID="txtZip" runat="server" CssClass="fildboxstaff"></asp:TextBox>
                    </div>
                    <div class="box pndR15">
                        State:<br />
                        <asp:DropDownList ID="ddlState" CssClass="fildboxstaff" runat="server" Width="230px">
                        </asp:DropDownList>
                    </div>
                    <div class="box pndR15">
                        <!--<span class="red">*</span> -->
                        Home Phone:<br />
                        <asp:TextBox ID="txtHomePhone" runat="server" CssClass="fildboxstaff"></asp:TextBox>
                        </asp:TextBox><%--<asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server"
                            ControlToValidate="txtHomePhone" ErrorMessage="Please enter Home Phone." Text="*"
                            SetFocusOnError="true" Font-Size="0" ValidationGroup="FamilyValidate"></asp:RequiredFieldValidator>--%>
                    </div>
                    <div class="clear">
                    </div>
                    <div class="box pndR15" style="padding-bottom: 0px;">
                        Message:
                        <asp:TextBox ID="txtMessage" runat="server" CssClass="fildboxstaff" TextMode="MultiLine"></asp:TextBox>
                    </div>
                    <div class="left pndR15">
                        <div class="left" style="padding-right: 10px;">
                            Message Start Date:<br />
                            <telerik:RadDatePicker ID="rdpMsgStartDate" runat="server" Width="100px">
                                <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x">
                                </Calendar>
                                <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                                <DateInput DisplayDateFormat="M/d/yyyy" DateFormat="M/d/yyyy">
                                </DateInput>
                            </telerik:RadDatePicker>
                        </div>
                        <div class="left">
                            Message End Date:<br />
                            <telerik:RadDatePicker ID="rdpMsgEndDate" runat="server" Width="100px">
                                <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x">
                                </Calendar>
                                <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                                <DateInput DisplayDateFormat="M/d/yyyy" DateFormat="M/d/yyyy">
                                </DateInput>
                            </telerik:RadDatePicker>
                        </div>
                    </div>
                    <div class="box pndR15">
                        Comments:
                        <br />
                        <asp:TextBox ID="txtComments" runat="server" TextMode="MultiLine" CssClass="fildboxstaff"></asp:TextBox>
                    </div>
                    <div class="box ">
                        Message Active:
                        <br />
                        <asp:CheckBox ID="chkMsgActive" runat="server" />
                    </div>
                </div>
            </fieldset>
        </div>
    </div>
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
            OnClick="btnSave_Click" OnClientClick="javascript:return CheckIsFamilyActive();" /></div>
    <asp:ValidationSummary ID="valSum1" runat="server" ValidationGroup="FamilyValidate"
        ShowMessageBox="true" ShowSummary="false" />
    <asp:HiddenField ID="hdnName" runat="server" />
    <asp:HiddenField ID="hdnCode" runat="server" />
    <br />
    <br />
    <div class="clear">
    </div>
    <h3 class="title" style="width: 990px;">
        <img src="../images/arrow.png" />&nbsp; Child Data</h3>
    <div class="clear">
    </div>
    <telerik:RadProgressManager ID="radprogressmanager1" runat="server" EnableAjaxSkinRendering="true" />
    <%--<telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" ClientEvents-OnRequestStart="conditionalPostback">--%>
    <telerik:RadGrid ID="rgChildData" CssClass="RemoveBorders" runat="server" Width="100%"
        Height="600px" AllowPaging="true" AllowSorting="true" AutoGenerateColumns="false"
        EnableAJAXLoadingTemplate="True" EnableAJAX="False" OnNeedDataSource="rgChildData_NeedDataSource"
        PagerStyle-AlwaysVisible="true" AutoGenerateEditColumn="false" PageSize="5" BorderWidth="0"
        ClientSettings-Scrolling-AllowScroll="true" AllowFilteringByColumn="True" OnEditCommand="rgChildData_EditCommand"
        GridLines="None" OnItemCommand="rgChildData_ItemCommand" OnItemDataBound="rgChildData_ItemDataBound"
        OnInsertCommand="rgChildData_UpdateCommand" OnUpdateCommand="rgChildData_InsertCommand"
        OnItemCreated="rgChildData_ItemCreated">
        <GroupingSettings CaseSensitive="false" />
        <ItemStyle Wrap="true" />
        <HeaderContextMenu EnableEmbeddedSkins="true" />
        <PagerStyle AlwaysVisible="True" ShowPagerText="true" Mode="NextPrevAndNumeric" Position="Top" />
        <MasterTableView CommandItemDisplay="TopAndBottom" DataKeyNames="Id" ItemStyle-Wrap="true"
            CommandItemSettings-AddNewRecordText="Add New Child" EditMode="PopUp" TableLayout="Auto"
            Width="100%">
            <Columns>
                <telerik:GridEditCommandColumn HeaderStyle-Width="5%" HeaderText="Edit" ButtonType="ImageButton"
                    Reorderable="False" UniqueName="Edit" ItemStyle-BorderStyle="None">
                    <HeaderStyle Width="5%"></HeaderStyle>
                </telerik:GridEditCommandColumn>
                <telerik:GridTemplateColumn HeaderText="Image" UniqueName="Photo" AllowFiltering="false">
                    <ItemTemplate>
                        <asp:Image ID="imgPhoto" runat="server" Height="25px" Width="25px" />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="FullName" UniqueName="FullName" AllowFiltering="true"
                    SortExpression="true" HeaderText="Name">
                </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn UniqueName="Gender" AllowFiltering="true" HeaderText="Gender">
                    <ItemTemplate>
                        <%# Convert.ToBoolean(Eval("Gender").ToString())==true?"Male":"Female" %>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="DOB" UniqueName="DOB" AllowFiltering="true" SortExpression="true"
                    HeaderText="DOB" DataFormatString="{0:MM/dd/yy}">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="SocSec" UniqueName="SocSec" AllowFiltering="true"
                    SortExpression="true" HeaderText="SocSec" Visible="false">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Comments" DataField="Comments" UniqueName="Comments"
                    AllowFiltering="false">
                </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn UniqueName="Active" AllowFiltering="true" HeaderText="Active">
                    <ItemTemplate>
                        <%# Convert.ToBoolean(Eval("Active").ToString())==true?"Active":"Inactive" %>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="ChildEnrollmentStatus" Visible="false" AllowFiltering="false"
                    HeaderText="Enrolment">
                    <ItemTemplate>
                        <asp:HyperLink ID="hlChildEnrollmentStatus" runat="server"><img src="../images/BrowseIcon.gif" height="16" width="16" /></asp:HyperLink>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="ChildAbsentHistory" AllowFiltering="false"
                    HeaderText="Absent History">
                    <ItemTemplate>
                        <asp:HyperLink ID="hlChildAbsentHistory" runat="server"><img src="../images/BrowseIcon.gif" height="16" width="16" /></asp:HyperLink>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="ChildSchedule" AllowFiltering="false" Visible="false"
                    HeaderText="Child Schedule">
                    <ItemTemplate>
                        <asp:HyperLink ID="hlChildSchedule" runat="server"><img src="../images/BrowseIcon.gif" height="16" width="16" /></asp:HyperLink>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
            </Columns>
            <%--<EditFormSettings InsertCaption="Child" CaptionFormatString="Child" FormTableStyle-GridLines="None"
                UserControlName="UserControls/addeditchild.ascx" EditFormType="WebUserControl"
                PopUpSettings-Height="80%" PopUpSettings-Width="990px" EditColumn-ItemStyle-HorizontalAlign="Center"
                EditColumn-HeaderStyle-VerticalAlign="Middle" FormMainTableStyle-HorizontalAlign="Center"
                FormTableItemStyle-VerticalAlign="Middle" FormTableItemStyle-HorizontalAlign="Center"
                PopUpSettings-Modal="true">
                <EditColumn UniqueName="EditCommandColumn1">
                    <HeaderStyle VerticalAlign="Middle"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </EditColumn>
                <FormTableStyle HorizontalAlign="Center" />
                <PopUpSettings Modal="True" Height="45%" Width="990px"></PopUpSettings>
            </EditFormSettings>--%>
        </MasterTableView>
        <ClientSettings AllowColumnsReorder="True" AllowDragToGroup="True" ReorderColumnsOnClient="True">
            <%--<ClientEvents OnPopUpShowing="PopUpShowing" />--%>
            <Selecting AllowRowSelect="True" />
            <Scrolling AllowScroll="True" UseStaticHeaders="True" />
            <Resizing ClipCellContentOnResize="false" AllowColumnResize="false" ResizeGridOnColumnResize="false" />
        </ClientSettings>
        <FilterMenu EnableEmbeddedSkins="true" ExpandAnimation-Type="Linear">
            <ExpandAnimation Type="Linear" />
        </FilterMenu>
    </telerik:RadGrid>
    <telerik:RadAjaxManagerProxy ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="rgChildData">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgChildData" LoadingPanelID="RAPL1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <%--</telerik:RadAjaxPanel>--%>
    <telerik:RadAjaxLoadingPanel ID="RAPL1" runat="server" Transparency="10">
    </telerik:RadAjaxLoadingPanel>
</asp:Content>
