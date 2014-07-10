<%@ Page Title="" Language="C#" MasterPageFile="~/UI/DayCareIndex.Master" AutoEventWireup="true"
    CodeBehind="SecondaryProgram.aspx.cs" Inherits="DayCare.UI.SecondaryProgram" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/UI/UserControls/MenuLink.ascx" TagName="UCMenuLink" TagPrefix="ucl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--<script type="text/javascript" language="javascript">
        function ShowLateFee(ChildDataId, Title) {
            var oWnd = radopen('AdditionalNotes.aspx?ChildDataId=' + ChildDataId);
            setTimeout(function() { oWnd.setActive(true); }, 100);
            oWnd.set_title(' Additional Notes');
            oWnd.center();
        }
    </script>--%>

    <script language="javascript" type="text/javascript">
        function callFun() {
            location.href = document.URL.replace('#', "") + '#';
        }

        //        function ValidationOnformSubmit() {
        //            var ErrorMsg = "";
        //            var cntChekBox = 0;

        //            if (document.getElementById("ctl00_ContentPlaceHolder1_ddlProg").selectedIndex == 0) {
        //                ErrorMsg += "Please select program\n";
        //            }
        //            if (document.getElementById("ctl00_ContentPlaceHolder1_ddlFeesPeriod").selectedIndex == 0) {
        //                ErrorMsg += "Please select Fees Period\n";
        //            }
        //            if (document.getElementById("ctl00_ContentPlaceHolder1_txtFee").value == "") {
        //                ErrorMsg += "Please enter Fee\n";
        //            }
        //            else {
        //                var reg = /(^-?\d\d*\.\d*$)|(^-?\d\d*$)|(^-?\.\d\d*$)/;
        //                if (!reg.test(document.getElementById("ctl00_ContentPlaceHolder1_txtFee").value)) {
        //                    ErrorMsg += "Please enter valid Fee\n";
        //                }

        //            }
        //            if (document.getElementById("ctl00_ContentPlaceHolder1_rdpStartDate").value == "") {
        //                ErrorMsg += "Please enter Start Date\n";
        //            }
        //            if (document.getElementById("ctl00_ContentPlaceHolder1_rdpEndDate").value == "") {
        //                ErrorMsg += "Please enter End Date\n";
        //            }

        //            var startdate = new Date(document.getElementById("ctl00_ContentPlaceHolder1_rdpStartDate").value);
        //            var enddate = new Date(document.getElementById("ctl00_ContentPlaceHolder1_rdpEndDate").value);
        //            if (startdate > enddate) {
        //                ErrorMsg += "Start Date must less than End Date\n";
        //            }

        //            if (document.getElementById("ctl00_ContentPlaceHolder1_ChkMon").checked == true) {
        //                if (document.getElementById("ctl00_ContentPlaceHolder1_ddlDayType1").selectedIndex == 0) {
        //                    ErrorMsg += "Please select time of Monday\n";
        //                }
        //                if (document.getElementById("ctl00_ContentPlaceHolder1_ddlChildProgClassRoom").selectedIndex == 0) {
        //                    ErrorMsg += "Please select class-room of Monday\n";
        //                }
        //                cntChekBox++;
        //            }


        //            if (document.getElementById("ctl00_ContentPlaceHolder1_ChkTue").checked == true) {
        //                if (document.getElementById("ctl00_ContentPlaceHolder1_ddlDayType2").selectedIndex == 0) {
        //                    ErrorMsg += "Please select time of Tuesday\n";
        //                }
        //                if (document.getElementById("ctl00_ContentPlaceHolder1_ddlChildProgClassRoom1").selectedIndex == 0) {
        //                    ErrorMsg += "Please select class-room of Tuesday\n";
        //                }
        //                cntChekBox++;
        //            }


        //            if (document.getElementById("ctl00_ContentPlaceHolder1_ChkWen").checked == true) {
        //                if (document.getElementById("ctl00_ContentPlaceHolder1_ddlDayType3").selectedIndex == 0) {
        //                    ErrorMsg += "Please select time of Wednesday\n";
        //                }
        //                if (document.getElementById("ctl00_ContentPlaceHolder1_ddlChildProgClassRoom2").selectedIndex == 0) {
        //                    ErrorMsg += "Please select class-room of Wednesday\n";
        //                }
        //                cntChekBox++;
        //            }


        //            if (document.getElementById("ctl00_ContentPlaceHolder1_ChkThus").checked == true) {
        //                if (document.getElementById("ctl00_ContentPlaceHolder1_ddlDayType4").selectedIndex == 0) {
        //                    ErrorMsg += "Please select time of Thursday\n";
        //                }
        //                if (document.getElementById("ctl00_ContentPlaceHolder1_ddlChildProgClassRoom3").selectedIndex == 0) {
        //                    ErrorMsg += "Please select class-room of Thursday\n";
        //                }
        //                cntChekBox++;
        //            }


        //            if (document.getElementById("ctl00_ContentPlaceHolder1_ChkFri").checked == true) {
        //                if (document.getElementById("ctl00_ContentPlaceHolder1_ddlDayType5").selectedIndex == 0) {
        //                    ErrorMsg += "Please select time of Friday\n";
        //                }
        //                if (document.getElementById("ctl00_ContentPlaceHolder1_ddlChildProgClassRoom4").selectedIndex == 0) {
        //                    ErrorMsg += "Please select class-room of Friday\n";
        //                }
        //                cntChekBox++;
        //            }


        //            if (document.getElementById("ctl00_ContentPlaceHolder1_txtFirstName").value == "") {
        //                ErrorMsg += "Please enter First Name\n";
        //            }
        //            if (document.getElementById("ctl00_ContentPlaceHolder1_txtLastName").value == "") {
        //                ErrorMsg += "Please enter Last Name\n";
        //            }
        //            if (document.getElementById("ctl00_ContentPlaceHolder1_rdpDOB").value == "") {
        //                ErrorMsg += "Please enter Date Of Birth\n";
        //            }
        //            if (document.getElementById("ctl00_ContentPlaceHolder1_ddlEnrollmentStatus").selectedIndex == 0) {
        //                ErrorMsg += "Please select Enrollment Status\n";
        //            }
        //            if (document.getElementById("ctl00_ContentPlaceHolder1_rdpEnrollmentDate").value == "") {
        //                ErrorMsg += "Please enter Enrollment Date\n";
        //            }

        //            if (document.getElementById("ctl00_ContentPlaceHolder1_ddlProg").selectedIndex > 0) {
        //                if (cntChekBox == 0) {
        //                    ErrorMsg += "Please check any week day\n";
        //                }
        //            }
        //            if (ErrorMsg != "") {
        //                alert(ErrorMsg);
        //                return false;
        //            }
        //        }


        function ValidationOnformSubmit() {
            //debugger;
            var ErrorMsg = "";
            var cntChekBox = 0;

            if (document.getElementById("ctl00_ContentPlaceHolder1_txtFirstName").value.replace(/^\s+|\s+$/g, "") == "") {
                ErrorMsg += "Please enter First Name\n";
            }
            if (document.getElementById("ctl00_ContentPlaceHolder1_txtLastName").value.replace(/^\s+|\s+$/g, "") == "") {
                ErrorMsg += "Please enter Last Name\n";
            }


            if (document.getElementById("ctl00_ContentPlaceHolder1_ddlProg").selectedIndex > 0) {
                if (document.getElementById("ctl00_ContentPlaceHolder1_ddlFeesPeriod").selectedIndex == 0) {
                    ErrorMsg += "Please select Fees Period\n";
                }
                if (document.getElementById("ctl00_ContentPlaceHolder1_txtFee").value == "") {
                    // ErrorMsg += "Please enter Fee\n";
                }
                else {
                    var reg = /(^-?\d\d*\.\d*$)|(^-?\d\d*$)|(^-?\.\d\d*$)/;
                    if (!reg.test(document.getElementById("ctl00_ContentPlaceHolder1_txtFee").value)) {
                        ErrorMsg += "Please enter valid Fee\n";
                    }
                }
                if (document.getElementById("ctl00_ContentPlaceHolder1_rdpStartDate").value == "") {
                    ErrorMsg += "Please enter Start Date\n";
                }
                if (document.getElementById("ctl00_ContentPlaceHolder1_rdpEndDate").value == "") {
                    ErrorMsg += "Please enter End Date\n";
                }

                var startdate = new Date(document.getElementById("ctl00_ContentPlaceHolder1_rdpStartDate").value);
                var enddate = new Date(document.getElementById("ctl00_ContentPlaceHolder1_rdpEndDate").value);
                if (startdate > enddate) {
                    ErrorMsg += "Start Date must less than End Date\n";
                }

                if (document.getElementById("ctl00_ContentPlaceHolder1_ChkMon").checked == true) {
                    //                    if (document.getElementById("ctl00_ContentPlaceHolder1_ddlDayType1").selectedIndex == 0) {
                    //                        ErrorMsg += "Please select time of Monday\n";
                    //                    }
                    //                    if (document.getElementById("ctl00_ContentPlaceHolder1_ddlChildProgClassRoom").selectedIndex == 0) {
                    //                        ErrorMsg += "Please select class-room of Monday\n";
                    //                    }
                    cntChekBox++;
                }


                if (document.getElementById("ctl00_ContentPlaceHolder1_ChkTue").checked == true) {
                    //                    if (document.getElementById("ctl00_ContentPlaceHolder1_ddlDayType2").selectedIndex == 0) {
                    //                        ErrorMsg += "Please select time of Tuesday\n";
                    //                    }
                    //                    if (document.getElementById("ctl00_ContentPlaceHolder1_ddlChildProgClassRoom1").selectedIndex == 0) {
                    //                        ErrorMsg += "Please select class-room of Tuesday\n";
                    //                    }
                    cntChekBox++;
                }


                if (document.getElementById("ctl00_ContentPlaceHolder1_ChkWen").checked == true) {
                    //                    if (document.getElementById("ctl00_ContentPlaceHolder1_ddlDayType3").selectedIndex == 0) {
                    //                        ErrorMsg += "Please select time of Wednesday\n";
                    //                    }
                    //                    if (document.getElementById("ctl00_ContentPlaceHolder1_ddlChildProgClassRoom2").selectedIndex == 0) {
                    //                        ErrorMsg += "Please select class-room of Wednesday\n";
                    //                    }
                    cntChekBox++;
                }


                if (document.getElementById("ctl00_ContentPlaceHolder1_ChkThus").checked == true) {
                    //                    if (document.getElementById("ctl00_ContentPlaceHolder1_ddlDayType4").selectedIndex == 0) {
                    //                        ErrorMsg += "Please select time of Thursday\n";
                    //                    }
                    //                    if (document.getElementById("ctl00_ContentPlaceHolder1_ddlChildProgClassRoom3").selectedIndex == 0) {
                    //                        ErrorMsg += "Please select class-room of Thursday\n";
                    //                    }
                    cntChekBox++;
                }

                if (document.getElementById("ctl00_ContentPlaceHolder1_ChkFri").checked == true) {
                    //                    if (document.getElementById("ctl00_ContentPlaceHolder1_ddlDayType5").selectedIndex == 0) {
                    //                        ErrorMsg += "Please select time of Friday\n";
                    //                    }
                    //                    if (document.getElementById("ctl00_ContentPlaceHolder1_ddlChildProgClassRoom4").selectedIndex == 0) {
                    //                        ErrorMsg += "Please select class-room of Friday\n";
                    //                    }
                    cntChekBox++;
                }
                if (document.getElementById("ctl00_ContentPlaceHolder1_ddlProg").selectedIndex > 0) {
                    if (cntChekBox == 0) {
                        ErrorMsg += "Please check any Week Day\n";
                    }
                }
                //                if (document.getElementById("ctl00_ContentPlaceHolder1_ddlEnrollmentStatus").selectedIndex == 0) {
                //                    ErrorMsg += "Please select Enrollment Status\n";
                //                }
                //                if (document.getElementById("ctl00_ContentPlaceHolder1_rdpEnrollmentDate").value == "") {
                //                    ErrorMsg += "Please enter Enrollment Date\n";
                //                }
            }
            else {
                //                if (document.getElementById("ctl00_ContentPlaceHolder1_rdpEnrollmentDate").value != "") {
                //                    if (document.getElementById("ctl00_ContentPlaceHolder1_ddlEnrollmentStatus").selectedIndex == 0) {
                //                        ErrorMsg += "Please select Enrollment Status\n";
                //                    }
                //                }
                ErrorMsg += "Please select secondary program";
            }


            if (ErrorMsg != "") {
                alert(ErrorMsg);
                return false;
            }
        }


        function GenderShow(obj) {
            var imgStaff = document.getElementById("ctl00_ContentPlaceHolder1_imgStaff");
            var ddlGender = document.getElementById("ctl00_ContentPlaceHolder1_ddlGender");

            if (imgStaff.src.indexOf("boy") > 0 || imgStaff.src.indexOf("girl") > 0) {
                if (document.getElementById("ctl00_ContentPlaceHolder1_ddlGender").selectedIndex == 1) {
                    imgStaff.src = "../ChildImages/boy.png";
                }
                else if (document.getElementById("ctl00_ContentPlaceHolder1_ddlGender").selectedIndex == 0) {
                    imgStaff.src = "../ChildImages/girl.png";
                }
            }
        }
        function CheckUncheckWeekDay(obj) {
            document.getElementById("ctl00_ContentPlaceHolder1_ChkMon").checked = obj.checked;
            document.getElementById("ctl00_ContentPlaceHolder1_ChkTue").checked = obj.checked;
            document.getElementById("ctl00_ContentPlaceHolder1_ChkWen").checked = obj.checked;
            document.getElementById("ctl00_ContentPlaceHolder1_ChkThus").checked = obj.checked;
            document.getElementById("ctl00_ContentPlaceHolder1_ChkFri").checked = obj.checked;
            return true;
        }
        function CheckForAll() {
            var ChkMon = document.getElementById("ctl00_ContentPlaceHolder1_ChkMon");
            var ChkTue = document.getElementById("ctl00_ContentPlaceHolder1_ChkTue");
            var ChkWen = document.getElementById("ctl00_ContentPlaceHolder1_ChkWen");
            var ChkThus = document.getElementById("ctl00_ContentPlaceHolder1_ChkThus");
            var ChkFri = document.getElementById("ctl00_ContentPlaceHolder1_ChkFri");
            if (ChkMon.checked == true && ChkTue.checked == true && ChkWen.checked == true && ChkThus.checked == true && ChkFri.checked == true) {
                document.getElementById("ctl00_ContentPlaceHolder1_ChkAll").checked = true;
            }
            else {
                document.getElementById("ctl00_ContentPlaceHolder1_ChkAll").checked = false;
            }
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
    <asp:Panel ID="pnlChild" runat="server">
        <fieldset>
            <legend><strong>Child Details</strong></legend>
            <div class="fieldDiv">
                <div class="box pndR15" style="height: 170px;">
                    <div class="left" style="padding: 8px 65px;">
                        <div class="photo">
                            <asp:Image ID="imgStaff" runat="server" ImageUrl="~/ChildImages/girl.png" Width="99"
                                Height="125" />
                        </div>
                    </div>
                    <div class="box ">
                        Image:
                        <br />
                        <asp:FileUpload ID="fupImage" runat="server" />
                        <asp:Label ID="lblImage" runat="server" Visible="false"></asp:Label>
                    </div>
                </div>
                <div class="left" style="width: 720px;">
                    <div class="box pndR15" style="display: none;">
                        <span class="red">*</span>First Name:
                        <br />
                        <asp:TextBox ID="txtFirstName" runat="server" CssClass="fildboxstaff" Enabled="false"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtFirstName"
                            SetFocusOnError="true" ErrorMessage="Please enter First Name." Text="*" Font-Size="0"
                            ValidationGroup="ChildValidate"></asp:RequiredFieldValidator>
                    </div>
                    <div class="box pndR15" style="display: none;">
                        <span class="red">*</span>Last Name:
                        <br />
                        <asp:TextBox ID="txtLastName" runat="server" CssClass="fildboxstaff" Enabled="false"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtLastName"
                            SetFocusOnError="true" ErrorMessage="Please enter Last Name." Text="*" Font-Size="0"
                            ValidationGroup="ChildValidate"></asp:RequiredFieldValidator>
                    </div>
                    <div class="box " style="display: none;">
                        <div class=" left">
                            Date of Birth:
                            <br />
                            <telerik:RadDatePicker ID="rdpDOB" runat="server" Width="100px" Enabled="false">
                            </telerik:RadDatePicker>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="rdpDOB"
                                SetFocusOnError="true" ErrorMessage="Please enter Date Of Birth." Text="*" Font-Size="0"
                                ValidationGroup="ChildValidate"></asp:RequiredFieldValidator></div>
                        <div class=" left">
                            Gender:
                            <br />
                            <%--<asp:RadioButton ID="rdMale" runat="server" Checked="true" onclick="javascript:GenderShow('boy');"
                                GroupName="Gender" />Male &nbsp;
                            <asp:RadioButton ID="rdFemale" runat="server" GroupName="Gender" onclick="javascript:GenderShow('girl');" />Female--%>
                            <asp:DropDownList ID="ddlGender" runat="server" onchange="javascript:GenderShow('');"
                                Width="80px" Enabled="false">
                                <%--<asp:ListItem Text="--Select--" Value="-1"></asp:ListItem>--%>
                                <asp:ListItem Text="Female" Value="false"></asp:ListItem>
                                <asp:ListItem Text="Male" Selected="True" Value="true"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class=" clear">
                    </div>
                    <div class="box pndR15">
                        Secondary program:<br />
                        <asp:DropDownList ID="ddlProg" runat="server" OnSelectedIndexChanged="ddlProg_SelectedIndexChanged"
                            AutoPostBack="true">
                        </asp:DropDownList>
                    </div>
                    <div class="box pndR15">
                        Fees Period:<br />
                        <asp:DropDownList ID="ddlFeesPeriod" runat="server" Width="208px" OnSelectedIndexChanged="ddlFeesPeriod_SelectedIndexChanged"
                            AutoPostBack="true">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlFeesPeriod"
                            SetFocusOnError="true" ErrorMessage="Please select Fee Period." InitialValue="00000000-0000-0000-0000-000000000000"
                            Text="*" Font-Size="0" ValidationGroup="ChildValidate"></asp:RequiredFieldValidator>
                    </div>
                    <div class="box">
                        Fee:<br />
                        <asp:TextBox ID="txtFee" runat="server" Width="100px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtFee"
                            SetFocusOnError="true" ErrorMessage="Please enter Fee." Text="*" Font-Size="0"
                            ValidationGroup="ChildValidate"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="rev" runat="server" ControlToValidate="txtFee"
                            SetFocusOnError="true" ErrorMessage="Please enter valid Fee." Text="*" Font-Size="0"
                            ValidationGroup="ChildValidate" ValidationExpression="(^-?\d\d*\.\d*$)|(^-?\d\d*$)|(^-?\.\d\d*$)"></asp:RegularExpressionValidator>
                    </div>
                    <div class=" clear">
                    </div>
                    <div class="box pndR15">
                        Start Date:<br />
                        <telerik:RadDatePicker ID="rdpStartDate" runat="server">
                        </telerik:RadDatePicker>
                        </asp:DropDownList>
                    </div>
                    <div class="box pndR15">
                        End Date:<br />
                        <telerik:RadDatePicker ID="rdpEndDate" runat="server">
                        </telerik:RadDatePicker>
                    </div>
                    <div class="box">
                    </div>
                    <div class=" clear">
                    </div>
                    <div class="box pndR15" style="padding-bottom: 5px;">
                        <%--OnCheckedChanged="ChkAll_CheckedChanged"--%>
                        <asp:CheckBox ID="ChkAll" runat="server" AutoPostBack="true" onclick="javascript:return CheckUncheckWeekDay(this);" />Select
                        all week days:
                    </div>
                    <%--<div class="box pndR15" style="padding-bottom: 5px;">
                        Time:
                    </div>--%>
                    <div class="box " style="padding-bottom: 5px;">
                        Class Room:
                    </div>
                    <div class=" clear">
                    </div>
                    <div class="box pndR15" style="padding-bottom: 5px;">
                        <asp:CheckBox ID="ChkMon" runat="server" onclick="javascript:return CheckForAll();" />Monday
                    </div>
                    <%--<div class="box pndR15" style="padding-bottom: 5px;">
                        <asp:DropDownList ID="ddlDayType1" runat="server" CssClass="brdbox">
                            <asp:ListItem Text="--Select--"></asp:ListItem>
                            <asp:ListItem Text="FullDay"></asp:ListItem>
                            <asp:ListItem Text="AM"></asp:ListItem>
                            <asp:ListItem Text="PM"></asp:ListItem>
                        </asp:DropDownList>
                    </div>--%>
                    <div class="box " style="padding-bottom: 5px;">
                        <asp:DropDownList ID="ddlChildProgClassRoom" runat="server" CssClass="brdbox">
                        </asp:DropDownList>
                    </div>
                    <div class=" clear">
                    </div>
                    <div class="box pndR15" style="padding-bottom: 5px;">
                        <asp:CheckBox ID="ChkTue" runat="server" onclick="javascript:return CheckForAll();" />Tuesday
                    </div>
                    <%-- <div class="box pndR15" style="padding-bottom: 5px;">
                        <asp:DropDownList ID="ddlDayType2" runat="server" CssClass="brdbox">
                            <asp:ListItem Text="--Select--"></asp:ListItem>
                            <asp:ListItem Text="FullDay"></asp:ListItem>
                            <asp:ListItem Text="AM"></asp:ListItem>
                            <asp:ListItem Text="PM"></asp:ListItem>
                        </asp:DropDownList>
                    </div>--%>
                    <div class="box " style="padding-bottom: 5px;">
                        <asp:DropDownList ID="ddlChildProgClassRoom1" runat="server" CssClass="brdbox">
                        </asp:DropDownList>
                    </div>
                    <div class=" clear">
                    </div>
                    <div class="box pndR15" style="padding-bottom: 5px;">
                        <asp:CheckBox ID="ChkWen" runat="server" onclick="javascript:return CheckForAll();" />Wednesday</div>
                    <%--<div class="box pndR15" style="padding-bottom: 5px;">
                        <asp:DropDownList ID="ddlDayType3" runat="server" CssClass="brdbox">
                            <asp:ListItem Text="--Select--"></asp:ListItem>
                            <asp:ListItem Text="FullDay"></asp:ListItem>
                            <asp:ListItem Text="AM"></asp:ListItem>
                            <asp:ListItem Text="PM"></asp:ListItem>
                        </asp:DropDownList>
                    </div>--%>
                    <div class="box " style="padding-bottom: 5px;">
                        <asp:DropDownList ID="ddlChildProgClassRoom2" runat="server" CssClass="brdbox">
                        </asp:DropDownList>
                    </div>
                    <div class=" clear">
                    </div>
                    <div class="box pndR15" style="padding-bottom: 5px;">
                        <asp:CheckBox ID="ChkThus" runat="server" onclick="javascript:return CheckForAll();" />Thursday</div>
                    <%--<div class="box pndR15" style="padding-bottom: 5px;">
                        <asp:DropDownList ID="ddlDayType4" runat="server" CssClass="brdbox">
                            <asp:ListItem Text="--Select--"></asp:ListItem>
                            <asp:ListItem Text="FullDay"></asp:ListItem>
                            <asp:ListItem Text="AM"></asp:ListItem>
                            <asp:ListItem Text="PM"></asp:ListItem>
                        </asp:DropDownList>
                    </div>--%>
                    <div class="box " style="padding-bottom: 5px;">
                        <asp:DropDownList ID="ddlChildProgClassRoom3" runat="server" CssClass="brdbox">
                        </asp:DropDownList>
                    </div>
                    <div class=" clear">
                    </div>
                    <div class="box pndR15" style="padding-bottom: 5px;">
                        <asp:CheckBox ID="ChkFri" runat="server" onclick="javascript:return CheckForAll();" />Friday</div>
                    <%--  <div class="box pndR15" style="padding-bottom: 5px;">
                        <asp:DropDownList ID="ddlDayType5" runat="server" CssClass="brdbox">
                            <asp:ListItem Text="--Select--"></asp:ListItem>
                            <asp:ListItem Text="FullDay"></asp:ListItem>
                            <asp:ListItem Text="AM"></asp:ListItem>
                            <asp:ListItem Text="PM"></asp:ListItem>
                        </asp:DropDownList>
                    </div>--%>
                    <div class="box " style="padding-bottom: 5px;">
                        <asp:DropDownList ID="ddlChildProgClassRoom4" runat="server" CssClass="brdbox">
                        </asp:DropDownList>
                    </div>
                    <div class=" clear">
                    </div>
                    <div class=" left" style="display: none;">
                        <div class="left" style="padding-right: 10px;">
                            Enrollment Status:<br />
                            <asp:DropDownList ID="ddlEnrollmentStatus" runat="server">
                            </asp:DropDownList>
                        </div>
                        <div class="left ">
                            Enroll Date:<br />
                            <telerik:RadDatePicker ID="rdpEnrollmentDate" runat="server" Width="115px">
                            </telerik:RadDatePicker>
                        </div>
                    </div>
                    <div class="box pndR15" style="padding-bottom: 0px; display: none;">
                        Comments:<br />
                        <asp:TextBox ID="txtComments" runat="server" TextMode="MultiLine" CssClass="fildboxstaff"></asp:TextBox>
                    </div>
                    <div class="left " style="display: none;">
                        Active:<br />
                        <asp:CheckBox ID="chkActive" runat="server" ToolTip="Checked True Then Active" />
                        &nbsp;
                    </div>
                    <br />
                    <div class=" clear">
                    </div>
                    <div>
                        <%--<asp:Button ID="Button2" runat="server" Text="Add/View Additional Notes" UseSubmitBehavior="false" OnClick="btnSave_Click" CausesValidation="true" style=" float:left;  background:#abd4e8; padding:8px; text-align:center; font-size:14px; font-weight:bold; border:1px solid #74b2d0; color:#1457b3; margin:5px 0 0 0;" />--%>
                        <%--<asp:HyperLink ID="hlAdditionalNotes" Text="Add/View Additional Notes" Visible="false"
                            runat="server" CssClass="addView">Add/View Additional Notes</asp:HyperLink>--%>
                    </div>
                </div>
        </fieldset>
        <div class=" clear">
        </div>
        <asp:HiddenField ID="hdSchoolProgram" runat="server" />
        <asp:HiddenField ID="hdnMon" runat="server" />
        <asp:HiddenField ID="hdnTue" runat="server" />
        <asp:HiddenField ID="hdnThus" runat="server" />
        <asp:HiddenField ID="hdnWen" runat="server" />
        <asp:HiddenField ID="hdnFri" runat="server" />
    </asp:Panel>
    <div class="right" style="padding-top: 0px;">
        <div class="" style="margin-right: 15px; float: left;">
            <asp:Button ID="btnAdditionalNotes" runat="server" Text="AdditionalNotes" OnClick="btnAdditionalNotes_Click"
                CausesValidation="true" Visible="false" CssClass="btn" Width="150px" />
            <%--<asp:HyperLink ID="hlAdditionalNotes" runat="server" NavigateUrl="" CssClass="btn">AdditionalNotes</asp:HyperLink>--%>
        </div>
        &nbsp;&nbsp;&nbsp;
        <div class=" left">
            <%--ValidationGroup="ChildValidate"--%>
            <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click " CausesValidation="true"
                CssClass="btn" /></div>
        <div class=" left" style="padding-left: 15px;">
            <asp:Button ID="btnBack" runat="server" Text="Back" CausesValidation="false" OnClick="btnBack_Click"
                CssClass="btn" /></div>
        <div class=" left" style="padding-left: 15px;">
            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false"
                OnClick="btnCancel_Click" CssClass="btn" /></div>
    </div>
    <br />
    <br />
    <telerik:RadGrid ID="rgAddEditChid" runat="server" AutoGenerateColumns="false" AllowFilteringByColumn="false"
        AllowPaging="true" AllowSorting="true" EnableLinqExpression="false" EnableEmbeddedSkins="true"
        EnableAjaxSkinRendering="true" ReorderColumnOnClient="true" GridLines="None"
        CssClass="RemoveBorders" ShowGroupPanel="false" allowColumnReorder="True" Width="100%"
        Height="600px" BorderWidth="0px" OnNeedDataSource="rgAddEditChid_NeedDataSource"
        OnEditCommand="rgAddEditChid_EditCommand" OnItemDataBound="rgAddEditChid_ItemDataBound"
        OnPreRender="rgAddEditChid_PreRender" OnItemCommand="rgAddEditChid_ItemCommand"
        OnDetailTableDataBind="rgAddEditChid_DetailTableDataBind" OnDeleteCommand="rgAddEditChid_DeleteCommand"
        PageSize="25">
        <GroupingSettings CaseSensitive="false" />
        <ItemStyle Wrap="true" />
        <PagerStyle AlwaysVisible="true" Mode="NextPrevAndNumeric" ShowPagerText="true" />
        <HeaderContextMenu EnableEmbeddedSkins="true" />
        <MasterTableView DataKeyNames="Id" PagerStyle-AlwaysVisible="true" EditMode="InPlace"
            CommandItemSettings-AddNewRecordText="Add New Child" HierarchyLoadMode="Client"
            CommandItemDisplay="None" TableLayout="Auto" AllowFilteringByColumn="false" Width="100%"
            Name="Child">
            <DetailTables>
                <telerik:GridTableView DataKeyNames="SchoolProgramId" CssClass="RemoveBorders" AutoGenerateColumns="false"
                    Width="100%" AllowPaging="false" NoDetailRecordsText="No Program" PagerStyle-AlwaysVisible="false"
                    DataMember="ProgramDetail" Name="Program" CommandItemDisplay="Top">
                    <ParentTableRelation>
                        <telerik:GridRelationFields DetailKeyField="ChildDataId" MasterKeyField="Id" />
                    </ParentTableRelation>
                    <CommandItemSettings AddNewRecordText="Add New Secondary Program" />
                    <Columns>
                        <telerik:GridEditCommandColumn HeaderStyle-Width="5%" HeaderText="Edit" ButtonType="ImageButton"
                            Reorderable="False" UniqueName="Edit" ItemStyle-BorderStyle="None">
                            <HeaderStyle Width="5%"></HeaderStyle>
                        </telerik:GridEditCommandColumn>
                        <telerik:GridButtonColumn CommandName="Delete" HeaderText="Delete" UniqueName="DeleteColumn"
                            ButtonType="ImageButton" Reorderable="false" ItemStyle-BorderStyle="None" ConfirmText="Do you wnat to delete Program?"
                            ConfirmTitle="Delete Record" ConfirmDialogType="RadWindow">
                            <HeaderStyle Width="15%"></HeaderStyle>
                        </telerik:GridButtonColumn>
                        <telerik:GridBoundColumn HeaderText="Secondary Program" DataField="ProgramTitle"
                            UniqueName="ProgramTitle" AllowFiltering="false">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="" DataField="ChildDataId" UniqueName="ChildDataId"
                            AllowFiltering="false" Visible="false">
                        </telerik:GridBoundColumn>
                        <telerik:GridCheckBoxColumn DataField="IsPrimary" UniqueName="IsPrimary" AllowFiltering="true"
                            HeaderText="Primary">
                        </telerik:GridCheckBoxColumn>
                    </Columns>
                    <NoRecordsTemplate>
                        No Program
                    </NoRecordsTemplate>
                </telerik:GridTableView>
            </DetailTables>
            <Columns>
                <%--   <telerik:GridEditCommandColumn HeaderStyle-Width="5%" HeaderText="Edit" ButtonType="ImageButton"
                    Reorderable="False" UniqueName="Edit" ItemStyle-BorderStyle="None">
                    <HeaderStyle Width="5%"></HeaderStyle>
                </telerik:GridEditCommandColumn>--%>
                <telerik:GridTemplateColumn HeaderText="Image" UniqueName="Photo" AllowFiltering="false">
                    <ItemTemplate>
                        <asp:Image ID="imgPhoto" runat="server" Height="32px" Width="32px" />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="FullName" UniqueName="FullName" AllowFiltering="false"
                    SortExpression="true" HeaderText="Name">
                </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn UniqueName="Gender" AllowFiltering="false" HeaderText="Gender">
                    <ItemTemplate>
                        <%# Convert.ToBoolean(Eval("Gender").ToString())==true?"Male":"Female" %>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="DOB" UniqueName="DOB" AllowFiltering="false"
                    SortExpression="true" HeaderText="DOB" DataFormatString="{0:MM/dd/yy}">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Comments" DataField="Comments" UniqueName="Comments"
                    AllowFiltering="false">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Enrollment Status" DataField="EnrollmentStatus"
                    UniqueName="EnrollmentStatus" AllowFiltering="false">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="EnrollmentDate" UniqueName="EnrollmentDate" AllowFiltering="false"
                    SortExpression="true" HeaderText="Enrollment Date" DataFormatString="{0:MM/dd/yy}">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Program" DataField="ProgramName" UniqueName="ProgramName"
                    AllowFiltering="false" Visible="false">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="SchoolProgramId" HeaderText="SchoolProgramId"
                    UniqueName="SchoolProgramId" Visible="false">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="ChildEnrollmentStatusId" HeaderText="ChildEnrollmentStatusId"
                    UniqueName="ChildEnrollmentStatusId" Visible="false">
                </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn UniqueName="Active" AllowFiltering="true" HeaderText="Active">
                    <ItemTemplate>
                        <%# Convert.ToBoolean(Eval("Active").ToString())==true?"Active":"Inactive" %>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="ChildAbsentHistory" AllowFiltering="false"
                    HeaderText="Absent History">
                    <ItemTemplate>
                        <asp:HyperLink ID="hlChildAbsentHistory" runat="server"><img src="../images/BrowseIcon.gif" height="16" width="16" /></asp:HyperLink>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
            </Columns>
        </MasterTableView>
        <ClientSettings AllowColumnsReorder="True" AllowDragToGroup="True" ReorderColumnsOnClient="True">
            <Selecting AllowRowSelect="True" />
            <Scrolling AllowScroll="True" UseStaticHeaders="True" />
        </ClientSettings>
        <FilterMenu EnableEmbeddedSkins="true" ExpandAnimation-Type="Linear">
            <ExpandAnimation Type="Linear" />
        </FilterMenu>
    </telerik:RadGrid>
    <%--<telerik:RadWindowManager ID="rwm1" runat="server" DestroyOnClose="false" InitialBehaviors="Resize"
        Modal="true" KeepInScreenBounds="true" ReloadOnShow="true" Title="Shortcuts"
        AutoSize="false" Behaviors="Close" Width="900px" Height="500px" Skin="Office2007"
        Overlay="true" ShowContentDuringLoad="false" Animation="Fade" VisibleStatusbar="false">
    </telerik:RadWindowManager>--%>
    <telerik:RadAjaxManagerProxy ID="AjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="ddlProg">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlChild" LoadingPanelID="RAPL1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ddlFeesPeriod">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlChild" LoadingPanelID="RAPL1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rgAddEditChid">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlChild" LoadingPanelID="RAPL1" />
                    <telerik:AjaxUpdatedControl ControlID="rgAddEditChid" LoadingPanelID="RAPL1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <telerik:RadAjaxLoadingPanel ID="RAPL1" runat="server" Transparency="10">
    </telerik:RadAjaxLoadingPanel>
    <asp:ValidationSummary ID="valSum" runat="server" ValidationGroup="ChildValidate"
        ShowMessageBox="true" ShowSummary="false" />
</asp:Content>
