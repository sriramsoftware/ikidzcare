<%@ Page Title="" Language="C#" MasterPageFile="~/UI/DayCareIndex.Master" AutoEventWireup="true"
    CodeBehind="Ledger.aspx.cs" Inherits="DayCare.Report.Ledger" %>

<%@ Register Src="~/UI/UserControls/MenuLink.ascx" TagName="UCMenuLink" TagPrefix="ucl" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--Opening balance div--%>
    <style>
        .OpBalDiv
        {
            position: absolute; /*top: 223px;
            left: 350px; padding-bottom: 13px;*/
            display: block;
            z-index: 5000;
            cursor: pointer;
            opacity: 0.94;
            background: url('../../images/bgI.png') repeat;
            border: solid 1px;
        }
        .OpBalDivArrow
        {
            position: absolute; /*top: 205px;
            left: 360px;*/
            padding-bottom: 13px;
            display: block;
            z-index: 7000;
            cursor: pointer;
        }
    </style>

    <script>
        function HideOpeningBalDiv() {
            var arrow = document.getElementById("arrow");
            if (arrow != null) {

                arrow.style.display = 'none';
            }
            var BalDiv = document.getElementById("<%=OpBaldiv.ClientID%>");
            if (BalDiv != null) {
                BalDiv.style.display = 'none';
            }
            document.getElementById("<%=lblOpeningBalance.ClientID%>").title = "click to show all year opening balance";
        }

        function ShowOpeningBalDiv() {



            var arrow = document.getElementById("arrow");
            if (arrow != null) {
                arrow.style.top = (parseInt(document.getElementById("<%=lblOpeningBalance.ClientID%>").offsetTop) + 15) + "px";
                arrow.style.left = (parseInt(document.getElementById("<%=lblOpeningBalance.ClientID%>").offsetLeft) + 1) + "px";
                arrow.style.display = 'block';
            }
            var BalDiv = document.getElementById("<%=OpBaldiv.ClientID%>");
            if (BalDiv != null) {
                BalDiv.style.top = (parseInt(document.getElementById("<%=lblOpeningBalance.ClientID%>").offsetTop) + 33) + "px";
                BalDiv.style.left = (parseInt(document.getElementById("<%=lblOpeningBalance.ClientID%>").offsetLeft) - 15) + "px";
                BalDiv.style.display = 'block';
            }
            document.getElementById("<%=lblOpeningBalance.ClientID%>").title = "";
        }
    </script>

    <%--end Opening balance div--%>

    <script type="text/javascript" language="javascript">
        function Chk(obj) {
            var prefix = obj.id.substring(0, obj.id.indexOf("UpdateButton"));
            if (prefix == null || prefix == "") {
                prefix = obj.id.substring(0, obj.id.indexOf("PerformInsertButton"));
            }
            var txtDebit = document.getElementById(prefix + "txtDebit");
            var txtCredit = document.getElementById(prefix + "txtCredit");
            var ErrorMsg = "";

            var rdbPayment = document.getElementById(prefix + "rdbPayment");
            var rdbDiscount = document.getElementById(prefix + "rdbDiscount");
            if (rdbPayment != null && rdbDiscount != null) {
                var ddlPayment = document.getElementById(prefix + "ddlPayment");
                var ddlCategory = document.getElementById(prefix + "ddlCategory");
                if (rdbPayment.checked) {
                    if (ddlPayment.selectedIndex == 0) {
                        ErrorMsg += "Please select Payment method\n";
                    }
                }
                else {
                    if (ddlCategory.selectedIndex == 0) {
                        ErrorMsg += "Please select Charge code category\n";
                    }
                }
            }
            var reg = /(^-?\d\d*\.\d*$)|(^-?\d\d*$)|(^-?\.\d\d*$)/;
            if (txtDebit.value != "") {
                if (!reg.test(txtDebit.value)) {
                    ErrorMsg += "Please enter valid Fees amount\n";
                }
            }
            if (txtCredit.value != "") {
                if (!reg.test(txtCredit.value)) {
                    ErrorMsg += "Please enter valid Payment amount\n";
                }
            }
            if (txtDebit.value != "" && txtCredit.value != "") {
                if (txtDebit.value > 0 && txtCredit.value > 0) {
                    ErrorMsg += "You can’t enter fees and payment at a time";
                }
            }

            if (ErrorMsg != "") {
                alert(ErrorMsg);
                return false;
            }
            return true;
        }

        function ChkOperation(operation, obj) {
            if (operation == 'Payment') {
                var prefix = obj.id.substring(0, obj.id.indexOf("rdbPayment"));
                var ddlPayment = document.getElementById(prefix + "ddlPayment");
                var ddlCategory = document.getElementById(prefix + "ddlCategory");
                var txtDebit = document.getElementById(prefix + "txtDebit");
                var txtCredit = document.getElementById(prefix + "txtCredit");
                //var txtComment = document.getElementById(prefix + "txtComment");
                ddlCategory.selectedIndex = 0;
                ddlPayment.style.display = 'block';
                ddlCategory.style.display = 'none';
                txtDebit.disabled = true;
                txtCredit.disabled = false;
                //txtComment.style.display = 'none';

                var lblDebit1 = document.getElementById(prefix + "lblDebit1");
                var lblCredit1 = document.getElementById(prefix + "lblCredit1");
                //txtDebit.value = lblDebit1.value;
                txtDebit.value = "0.00";
                txtCredit.value = lblCredit1.value;
                txtCredit.focus();
            }
            else if (operation == 'Discount') {
                var prefix = obj.id.substring(0, obj.id.indexOf("rdbDiscount"));
                var ddlPayment = document.getElementById(prefix + "ddlPayment");
                var ddlCategory = document.getElementById(prefix + "ddlCategory");
                var txtDebit = document.getElementById(prefix + "txtDebit");
                var txtCredit = document.getElementById(prefix + "txtCredit");
                //var txtComment = document.getElementById(prefix + "txtComment");
                ddlPayment.selectedIndex = 0;
                ddlPayment.style.display = 'none';
                ddlCategory.style.display = 'block';
                txtDebit.disabled = false;
                txtCredit.disabled = false;
                //txtComment.style.display = 'none';

                var lblDebit1 = document.getElementById(prefix + "lblDebit1");
                var lblCredit1 = document.getElementById(prefix + "lblCredit1");
                txtCredit.value = "0.00";
                txtDebit.value = lblDebit1.value;
            }
            return true;
        }

        function OnChargeCodeChange(obj) {

            var prefix = obj.id.substring(0, obj.id.indexOf("ddlCategory"));
            if (prefix != null && prefix != "") {
                var txtDebit = document.getElementById(prefix + "txtDebit");
                var txtCredit = document.getElementById(prefix + "txtCredit");
                var ddlChareCode = obj.options[obj.selectedIndex].text;
                if (ddlChareCode.indexOf("- Fees") > 0) {
                    txtDebit.disabled = false;
                    txtCredit.disabled = true;


                    var lblDebit1 = document.getElementById(prefix + "lblDebit1");
                    var lblCredit1 = document.getElementById(prefix + "lblCredit1");
                    txtCredit.value = "0.00";
                    txtDebit.value = lblDebit1.value;
                    txtDebit.focus();
                }
                else if (ddlChareCode.indexOf("- Payment") > 0) {
                    txtDebit.disabled = true;
                    txtCredit.disabled = false;

                    var lblDebit1 = document.getElementById(prefix + "lblDebit1");
                    var lblCredit1 = document.getElementById(prefix + "lblCredit1");
                    //txtDebit.value = lblDebit1.value;
                    txtDebit.value = "0.00";
                    txtCredit.value = lblCredit1.value;
                    txtCredit.focus();

                }
                else {
                    txtDebit.disabled = false;
                    txtCredit.disabled = false;
                }
            }
            else {
                var prefix = obj.id.substring(0, obj.id.indexOf("ddlPayment"));
                var txtCredit = document.getElementById(prefix + "txtCredit");
                txtCredit.focus();
            }
            return true;
        }

        
    </script>

    <script>
        function ChkAll(obj) {
            var input = document.getElementsByTagName('input');
            for (var cnt = 0; cnt < input.length; cnt++) {
                if (input[cnt].id.indexOf("chkHeaderDelete") != 0) {
                    input[cnt].checked = obj.checked;
                }
            }
        }
        function ChkUnchk(obj) {
            var flag = true;
            if (obj.checked == false) {
                flag = false;
            }
            else {
                var input = document.getElementsByTagName('input');
                for (var cnt = 0; cnt < input.length; cnt++) {
                    if (input[cnt].id.indexOf("chkHeaderDelete") == -1 && input[cnt].type == "checkbox" && input[cnt].checked == false) {
                        flag = false;
                        break;
                    }
                }
            }
            document.getElementById("ctl00_ContentPlaceHolder1_rgLedger_ctl00_ctl02_ctl02_chkHeaderDelete").checked = flag;
        }

        function isCheck() {
            var input = document.getElementsByTagName('input');
            var flag = false;
            for (var cnt = 0; cnt < input.length; cnt++) {
                if (input[cnt].id.indexOf("chkHeaderDelete") == -1 && input[cnt].type == "checkbox" && input[cnt].checked == true) {
                    flag = true;
                    break;
                }
            }
            if (flag) {
                if (confirm("Do you want to delete?")) {
                    return true;
                }
                else {
                    return false;
                }
            }
            else {
                alert("Please select atleast one.");
                return false;
            }
        }
    </script>

    <h3 class="title">
        <ucl:UCMenuLink ID="usrMenuLink" runat="server" />
    </h3>
    <div class="clear">
    </div>
    <table style="width: 50%; font-family: Arial; font-size: 14px;">
        <tr>
            <td>
                Balance
            </td>
            <td>
                <asp:TextBox ID="txtBalance" runat="server" Text="0" ReadOnly="true"></asp:TextBox>
            </td>
            <td>
                YTD Payment
            </td>
            <td>
                <asp:TextBox ID="txtYTDPay" runat="server" Text="0" ReadOnly="true"></asp:TextBox>
            </td>
        </tr>
    </table>
    <table style="width: 100%; display: none;">
        <%--<tr>
            <td>
                Balance
            </td>
            <td>
                <asp:TextBox ID="txtBalance" runat="server" Text="0" ReadOnly="true"></asp:TextBox>
            </td>
            <td>
                YTDPayment
            </td>
            <td>
                <asp:TextBox ID="txtYTDPay" runat="server" Text="0"></asp:TextBox>
            </td>
        </tr>--%>
        <tr>
            <td>
                Date
            </td>
            <td>
                <telerik:RadDatePicker ID="rdpDate" runat="server">
                </telerik:RadDatePicker>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="rdpDate"
                    ErrorMessage="Please Select Date." Text="*" Font-Size="0" ValidationGroup="LedgerValidate"></asp:RequiredFieldValidator>
            </td>
            <td>
                Category
            </td>
            <td>
                <asp:DropDownList ID="ddlCategory" runat="server">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfv1" runat="server" ControlToValidate="ddlCategory"
                    Font-Size="0" ErrorMessage="Please select Category." InitialValue="00000000-0000-0000-0000-000000000000"
                    Text="*" ValidationGroup="LedgerValidate"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                Amount
            </td>
            <td>
                <asp:TextBox ID="txtAmount" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtAmount"
                    ErrorMessage="Please enter Amount." Text="*" Font-Size="0" ValidationGroup="LedgerValidate"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="rev" ControlToValidate="txtAmount" runat="server"
                    Text="*" ErrorMessage="Please Enter Valid Amount." Display="Dynamic" ValidationGroup="LedgerValidate"
                    Font-Size="0" ValidationExpression="(^-?\d\d*\.\d*$)|(^-?\d\d*$)|(^-?\.\d\d*$)">*</asp:RegularExpressionValidator>
            </td>
            <td>
            </td>
            <td colspan="2">
                <table width="100%" border="0">
                    <tr>
                        <td width="120px">
                            <asp:Button ID="btnSave" runat="server" CssClass="btn" Text="Save" OnClick="btnSave_Click"
                                ValidationGroup="LedgerValidate" CausesValidation="true" />
                        </td>
                        <td>
                            <asp:Button ID="btnCancel" runat="server" CssClass="btn" Text="Cancel" OnClick="btnCancel_Click"
                                CausesValidation="false" />
                        </td>
                        <td style="float: right;">
                            <asp:Button ID="btnReposting" runat="server" Visible="false" CssClass="btn" Text="Reposting"
                                OnClientClick="javascript:return isCheck();" OnClick="btnReposting_Click" CausesValidation="false" />
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
                <%--<div style="border: 1px solid;position:absolute;">
                    &nbsp;&nbsp;&nbsp;&nbsp;
                   
                </div>
                <div style="float: right; border: 1px solid;">
                    
                </div>--%>
            </td>
        </tr>
    </table>
    <div class="clear">
    </div>
    <div>
        <div class="left ">
            <br />
            <b style="font-family: Arial; font-size: 16px;">OPENING BALANCE: <span runat="server"
                id="spnlblOpBal">
                <asp:Label ID="lblOpeningBalance" runat="server"></asp:Label></span></b>
            <div>
                <div id="arrow" style="float: left; display: none; width: 13px; height: 15px;" class="OpBalDivArrow">
                    <asp:Image ID="imgArrow" ImageUrl="../images/arrowI.png" runat="server" /></div>
                <div class="OpBalDiv" runat="server" id="OpBaldiv" style="display: none; float: right;">
                    <asp:Repeater ID="rptrOpeningBal" runat="server">
                        <HeaderTemplate>
                            <table style="font-size: 11px; font-family: Times New Roman; min-width: 130px; padding: 5px;">
                                <tr>
                                    <td align="center" colspan="3">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <b>Year</b>
                                    </td>
                                    <td>
                                    </td>
                                    <td align="center">
                                        <b>Op.bal.</b>
                                    </td>
                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <%# Eval("SchoolYear") %>
                                </td>
                                <td>
                                    ->
                                </td>
                                <td align="right">
                                    <%# Eval("ClosingBalanceAmount") %>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table>
                            <div style="padding-right: 3px; padding-bottom: 3px; float: right;">
                                <img src="../images/close.png" title="colse" onclick="javascript:HideOpeningBalDiv();" />
                            </div>
                        </FooterTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </div>
        <div class="right">
            <div class=" left">
                <asp:Button ID="btnBottomDeleteAll" runat="server" Text="Delete" OnClientClick="javascript:return isCheck();"
                    CssClass="btn" Style="float: right;" OnClick="btnDeleteAll_Click" />
            </div>
            <div class=" left" style="padding-left: 5px;">
                <asp:Button ID="btnBottomEditAll" runat="server" Text="Edit" CssClass="btn" OnClick="btnEditAll_Click" /></div>
            <div class=" left" style="padding-left: 5px;">
                <asp:Button ID="btnBottomPrint" runat="server" Text="Print" CssClass="btn" OnClick="btnPrint_Click" />
            </div>
        </div>
        <div class="clear">
        </div>
    </div>
    <telerik:RadGrid ID="rgLedger" CssClass="RemoveBorders" runat="server" Width="100%"
        Height="600px" AllowPaging="true" AllowSorting="true" AutoGenerateColumns="false"
        OnNeedDataSource="rgLedger_NeedDataSource" PagerStyle-AlwaysVisible="true" AutoGenerateEditColumn="false"
        PageSize="50" BorderWidth="0" ClientSettings-Scrolling-AllowScroll="true" AllowFilteringByColumn="false"
        OnEditCommand="rgLedger_EditCommand" GridLines="None" OnItemCommand="rgLedger_ItemCommand"
        OnInsertCommand="rgLedger_InsertCommand" OnItemDataBound="rgLedger_ItemDataBound"
        OnItemCreated="rgLedger_ItemCreated" OnUpdateCommand="rgLedger_UpdateCommand"
        OnDeleteCommand="rgLedger_DeleteCommand" OnPreRender="rgLedger_PreRender">
        <GroupingSettings CaseSensitive="false" />
        <ItemStyle Wrap="true" />
        <HeaderContextMenu EnableEmbeddedSkins="true" />
        <PagerStyle AlwaysVisible="True" ShowPagerText="true" Mode="NextPrevAndNumeric" />
        <MasterTableView CommandItemDisplay="Top" TableLayout="Auto" CommandItemSettings-AddNewRecordText="Add New"
            DataKeyNames="Id" EditFormSettings-EditColumn-Display="false" AllowAutomaticInserts="false"
            Width="100%" AllowAutomaticUpdates="false" EditMode="InPlace">
            <%--  <Columns>
                <telerik:GridEditCommandColumn HeaderStyle-Width="5%" HeaderText="Edit" ButtonType="ImageButton"
                    Reorderable="False" UniqueName="Edit" ItemStyle-BorderStyle="None">
                    <HeaderStyle Width="5%"></HeaderStyle>
                </telerik:GridEditCommandColumn>
                <telerik:GridButtonColumn CommandName="Delete" HeaderText="Delete" UniqueName="DeleteColumn"
                    ButtonType="ImageButton" Reorderable="false" ItemStyle-BorderStyle="None" ConfirmText="Do you want to delete record?"
                    ConfirmTitle="Delete Record" ConfirmDialogType="RadWindow">
                    <HeaderStyle Width="3%"></HeaderStyle>
                </telerik:GridButtonColumn>
                <telerik:GridBoundColumn DataField="TransactionDate" AllowFiltering="false" UniqueName="TransactionDate"
                    HeaderText="Date" DataFormatString="{0:MM/dd/yyy}">
                    <HeaderStyle Width="5%"></HeaderStyle>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Detail" AllowFiltering="false" UniqueName="Detail"
                    HeaderText="Detail">
                    <HeaderStyle Width="15%"></HeaderStyle>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="FamilyName" AllowFiltering="false" UniqueName="FamilyName"
                    HeaderText="Family">
                    <HeaderStyle Width="15%"></HeaderStyle>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Debit" AllowFiltering="false" UniqueName="Debit"
                    HeaderText="Debit">
                    <HeaderStyle Width="5%"></HeaderStyle>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Credit" AllowFiltering="false" UniqueName="Credit"
                    HeaderText="Credit">
                    <HeaderStyle Width="5%"></HeaderStyle>
                </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn AllowFiltering="false" UniqueName="Balance" HeaderText="Balance">
                    <ItemTemplate>
                        <asp:Label ID="lblBalance" runat="server"></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="5%"></HeaderStyle>
                </telerik:GridTemplateColumn>
                
            </Columns>--%>
            <%-- <CommandItemTemplate>
                
                <a href="#" onclick="return ShowInsertForm();">Add New Record</a>
            </CommandItemTemplate>--%>
            <Columns>
                <telerik:GridEditCommandColumn ButtonType="ImageButton" HeaderStyle-Width="5%" Reorderable="true"
                    HeaderText="Edit" UpdateText="Edit" UniqueName="Edit">
                    <HeaderStyle Width="3%"></HeaderStyle>
                </telerik:GridEditCommandColumn>
                <telerik:GridButtonColumn CommandName="Delete" HeaderText="Delete" UniqueName="DeleteColumn"
                    ButtonType="ImageButton" Reorderable="false" ItemStyle-BorderStyle="None" ConfirmText="Do you want to delete record?"
                    ConfirmTitle="Delete Record" ConfirmDialogType="RadWindow">
                    <HeaderStyle Width="5%"></HeaderStyle>
                </telerik:GridButtonColumn>
                <telerik:GridTemplateColumn HeaderText="" UniqueName="DeleteAll" AllowFiltering="false">
                    <HeaderTemplate>
                        <asp:CheckBox ID="chkHeaderDelete" runat="server" onclick="javascript:ChkAll(this);" />
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="chkDelete" runat="server" onclick="javascript:ChkUnchk(this);" />
                    </ItemTemplate>
                    <EditItemTemplate>
                    </EditItemTemplate>
                    <HeaderStyle Width="5%"></HeaderStyle>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderText="" UniqueName="Select" Display="false" AllowFiltering="false">
                    <EditItemTemplate>
                        <asp:RadioButton ID="rdbPayment" runat="server" GroupName="operation" Checked="true"
                            onclick="return ChkOperation('Payment',this);" Text="" TextAlign="Right" />Payment
                        <asp:RadioButton ID="rdbDiscount" runat="server" GroupName="operation" TextAlign="Right"
                            onclick="return ChkOperation('Discount',this);" Text="" />Charges
                    </EditItemTemplate>
                    <HeaderStyle Width="10%"></HeaderStyle>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderText="Transaction Date" UniqueName="TransactionDate"
                    AllowFiltering="false" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblTransactionDate" runat="server" Text='<%# Eval("TransactionDate","{0:MM/dd/yy}") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <telerik:RadDatePicker ID="rdpDate" runat="server" Width="100px">
                        </telerik:RadDatePicker>
                        <asp:Label ID="lblTransactionDate1" runat="server" Text='<%# Eval("TransactionDate","{0:MM/dd/yy}") %>'
                            Visible="false"></asp:Label>
                    </EditItemTemplate>
                    <HeaderStyle Width="12%"></HeaderStyle>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderText="Detail" UniqueName="Detail" AllowFiltering="false"
                    Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblDetail" runat="server" Text='<%# Eval("Detail") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtDetail" runat="server" Text='<%# Eval("Detail") %>' TextMode="MultiLine"
                            Width="120px"></asp:TextBox>
                    </EditItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderText="Family Name" UniqueName="FamilyName" AllowFiltering="false"
                    Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblFamilyName" runat="server" Text='<%# Eval("FamilyName") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtFamilyName" runat="server" Text='<%# Eval("FamilyName") %>' Width="120px"></asp:TextBox>
                    </EditItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderText="Comment" UniqueName="Comment" AllowFiltering="false"
                    Visible="true">
                    <ItemTemplate>
                        <asp:Label ID="lblComment" runat="server" Text='<%# Eval("Comment") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtComment" runat="server" Text='<%# Eval("Comment") %>' TextMode="MultiLine"
                            Width="150px"></asp:TextBox>
                    </EditItemTemplate>
                    <HeaderStyle Width="22%"></HeaderStyle>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderText="Payment / Charges" UniqueName="operation"
                    AllowFiltering="false">
                    <ItemTemplate>
                        <asp:Label ID="lblOperation" runat="server"></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddlPayment" runat="server" Width="150px" onchange="return OnChargeCodeChange(this);">
                            <asp:ListItem Text="--Select--" Value="-1"></asp:ListItem>
                            <asp:ListItem Text="Cash" Value="0"></asp:ListItem>
                            <asp:ListItem Text="Check" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Credit" Value="2"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlCategory" runat="server" Width="150px" onchange="return OnChargeCodeChange(this);">
                        </asp:DropDownList>
                    </EditItemTemplate>
                    <HeaderStyle Width="18%"></HeaderStyle>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderText="Fees" UniqueName="Debit" AllowFiltering="false"
                    ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="lblDebit" runat="server" Text='<%# Eval("Debit") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtDebit" runat="server" MaxLength="9" Style="text-align: right"
                            Text='<%# Eval("Debit") %>' Width="70px"></asp:TextBox>
                        <asp:HiddenField ID="lblDebit1" runat="server" Value='<%# Eval("Debit") %>'></asp:HiddenField>
                    </EditItemTemplate>
                    <HeaderStyle Width="9%"></HeaderStyle>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderText="Payment" UniqueName="Credit" AllowFiltering="false"
                    HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="lblCredit" runat="server" Text='<%# Eval("Credit") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtCredit" runat="server" MaxLength="9" Style="text-align: right"
                            Text='<%# Eval("Credit") %>' Width="70px"></asp:TextBox>
                        <asp:HiddenField ID="lblCredit1" runat="server" Value='<%# Eval("Credit") %>'></asp:HiddenField>
                    </EditItemTemplate>
                    <HeaderStyle Width="12%"></HeaderStyle>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderText="Balance" UniqueName="Balance" AllowFiltering="false"
                    ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="lblBalance" runat="server" Text='<%# Eval("Balance") %>'></asp:Label>
                    </ItemTemplate>
                    <%-- <EditItemTemplate>
                        <asp:TextBox ID="txtBalance" runat="server" Text='<%# Eval("Balance") %>' Width="50px"></asp:TextBox>
                    </EditItemTemplate>--%>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderText="" UniqueName="" AllowFiltering="false" Display="true"
                    ItemStyle-HorizontalAlign="Right">
                    <HeaderStyle Width="5%"></HeaderStyle>
                </telerik:GridTemplateColumn>
            </Columns>
        </MasterTableView>
        <ClientSettings AllowColumnsReorder="false" AllowDragToGroup="false" ReorderColumnsOnClient="True">
            <Selecting AllowRowSelect="True" />
            <Scrolling AllowScroll="True" UseStaticHeaders="True" />
        </ClientSettings>
        <FilterMenu EnableEmbeddedSkins="true" ExpandAnimation-Type="Linear">
            <ExpandAnimation Type="Linear" />
        </FilterMenu>
    </telerik:RadGrid>
    <div class="clear">
    </div>
    <div class="right">
        <div class=" left">
            <asp:Button ID="btnTopDeleteAll" runat="server" Text="Delete" OnClientClick="javascript:return isCheck();"
                CssClass="btn" Style="float: right;" OnClick="btnDeleteAll_Click" />
        </div>
        <div class=" left" style="padding-left: 5px;">
            <asp:Button ID="btnTopEditAll" runat="server" Text="Edit" CssClass="btn" Style="float: right;"
                OnClick="btnEditAll_Click" /></div>
        <div class=" left" style="padding-left: 5px;">
            <asp:Button ID="btnTopPrint" runat="server" Text="Print" CssClass="btn" Style="float: right;"
                OnClick="btnPrint_Click" />
        </div>
    </div>
    <div class="clear">
    </div>
    <telerik:RadAjaxLoadingPanel ID="RAPL1" runat="server" Transparency="10">
    </telerik:RadAjaxLoadingPanel>
    <asp:ValidationSummary ID="valSum" runat="server" ValidationGroup="LedgerValidate"
        ShowMessageBox="true" ShowSummary="false" />
</asp:Content>
