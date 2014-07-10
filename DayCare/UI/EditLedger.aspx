<%@ Page Title="" Language="C#" MasterPageFile="~/UI/DayCareIndex.Master" AutoEventWireup="true"
    CodeBehind="EditLedger.aspx.cs" Inherits="DayCare.UI.EditLedger" %>

<%@ Register Src="~/UI/UserControls/MenuLink.ascx" TagName="UCMenuLink" TagPrefix="ucl" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript" language="javascript">
        function Chk() {
            var prefix = "";
            var ErrorMsg = "";

            var input = document.getElementsByTagName("input");
            var date = 0;
            var paymentmethod = 0;
            var amount = 0;
            for (var k = 0; k < input.length; k++) {
                if (input[k].id.indexOf("chkHeaderDelete") == -1 && input[k].type == "checkbox" && input[k].checked == true) {
                    prefix = input[k].id.substring(0, input[k].id.indexOf("chkDelete"));
                    var txtDebit = document.getElementById(prefix + "txtDebit");
                    var txtCredit = document.getElementById(prefix + "txtCredit");


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
                }
            }




            if (ErrorMsg != "") {
                ErrorMsg = "Some information you entered may wrong."; //, would you like to skip and save other records?";
                if (alert(ErrorMsg)) {
                    return true;
                }
                else {
                    return false;
                }
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
                if (input[cnt].id.indexOf("chkHeaderDelete") != 0 && input[cnt].type == "checkbox") {
                    input[cnt].checked = obj.checked;
                }
            }
        }
        function ChkUnchk(obj) {
            var chkHeaderDelete = "";
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
            //            document.getElementById("ctl00_ContentPlaceHolder1_rgLedger_ctl00_ctl02_ctl01_chkHeaderDelete").checked = flag;
            var input = document.getElementsByTagName('input');
            for (var cnt = 0; cnt < input.length; cnt++) {
                if (input[cnt].id.indexOf("chkHeaderDelete") > -1 && input[cnt].type == "checkbox") {
                    chkHeaderDelete = input[cnt].id;
                    break;
                }
            }
            document.getElementById(chkHeaderDelete).checked = flag;
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
                if (Chk()) {
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
    <table style="width: 100%;">
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
                <asp:TextBox ID="txtYTDPay" runat="server" Text="0"></asp:TextBox>
            </td>
        </tr>
    </table>
    <div class="right">
        <%--<asp:Button ID="btnTopPrint" runat="server" Text="Print" CssClass="btn" Style="float: right;"
            OnClick="btnPrint_Click" />&nbsp;--%>
        <div class=" left">
            <asp:Button ID="btnTopEditAll" runat="server" Text="Update" CssClass="btn" Style="float: right;"
                OnClientClick="javascript:return isCheck();" OnClick="btnEditAll_Click" />
        </div>
        <div class=" left" style="padding-left: 5px;">
            <asp:Button ID="btnTopBack" runat="server" Text="Back" CssClass="btn" Style="float: right;"
                OnClick="btnTopBack_Click" /></div>
    </div>
    <div class="clear">
    </div>
    <telerik:RadGrid ID="rgLedger" CssClass="RemoveBorders" runat="server" Width="100%"
        Height="600px" AllowPaging="false" AllowSorting="true" AutoGenerateColumns="false"
        OnNeedDataSource="rgLedger_NeedDataSource" PagerStyle-AlwaysVisible="true" AutoGenerateEditColumn="false"
        BorderWidth="0" ClientSettings-Scrolling-AllowScroll="true" AllowFilteringByColumn="false"
        OnItemDataBound="rgLedger_ItemDataBound">
        <GroupingSettings CaseSensitive="false" />
        <ItemStyle Wrap="true" />
        <HeaderContextMenu EnableEmbeddedSkins="true" />
        <PagerStyle AlwaysVisible="True" ShowPagerText="true" Mode="NextPrevAndNumeric" />
        <MasterTableView CommandItemDisplay="None" TableLayout="Auto" DataKeyNames="Id" EditFormSettings-EditColumn-Display="false"
            AllowAutomaticInserts="false" Width="100%" AllowAutomaticUpdates="false" EditMode="InPlace">
            <Columns>
                <%--<telerik:GridEditCommandColumn ButtonType="ImageButton" HeaderStyle-Width="5%" Reorderable="true"
                    HeaderText="Edit" UpdateText="Edit" UniqueName="Edit">
                    <HeaderStyle Width="3%"></HeaderStyle>
                </telerik:GridEditCommandColumn>
                <telerik:GridButtonColumn CommandName="Delete" HeaderText="Delete" UniqueName="DeleteColumn"
                    ButtonType="ImageButton" Reorderable="false" ItemStyle-BorderStyle="None" ConfirmText="Do you want to delete record?"
                    ConfirmTitle="Delete Record" ConfirmDialogType="RadWindow">
                    <HeaderStyle Width="5%"></HeaderStyle>
                </telerik:GridButtonColumn>--%>
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
                <telerik:GridTemplateColumn HeaderText="" UniqueName="Select" Display="true" AllowFiltering="false">
                    <ItemTemplate>
                        <asp:RadioButton ID="rdbPayment" runat="server" GroupName="operation" Checked="true"
                            onclick="return ChkOperation('Payment',this);" Text="" /><asp:Label ID="lblPaymentLabel"
                                runat="server" Text="Payment"></asp:Label>
                        <asp:RadioButton ID="rdbDiscount" runat="server" GroupName="operation" onclick="return ChkOperation('Discount',this);"
                            Text="" /><asp:Label ID="lblChargesLable" runat="server" Text="Charges"></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="10%"></HeaderStyle>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderText="Transaction Date" UniqueName="TransactionDate"
                    AllowFiltering="false" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <telerik:RadDatePicker ID="rdpDate" runat="server" Width="100px">
                        </telerik:RadDatePicker>
                        <asp:Label ID="lblTransactionDate1" runat="server" Text='<%# Eval("TransactionDate","{0:MM/dd/yy}") %>'
                            Visible="false"></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                    </EditItemTemplate>
                    <HeaderStyle Width="12%"></HeaderStyle>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderText="Detail" UniqueName="Detail" AllowFiltering="false"
                    Visible="false">
                    <ItemTemplate>
                        <asp:TextBox ID="txtDetail" runat="server" Text='<%# Eval("Detail") %>' TextMode="MultiLine"
                            Width="120px"></asp:TextBox>
                    </ItemTemplate>
                    <EditItemTemplate>
                    </EditItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderText="Family Name" UniqueName="FamilyName" AllowFiltering="false"
                    Visible="false">
                    <ItemTemplate>
                        <asp:TextBox ID="txtFamilyName" runat="server" Text='<%# Eval("FamilyName") %>' Width="120px"></asp:TextBox>
                    </ItemTemplate>
                    <EditItemTemplate>
                    </EditItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderText="Comment" UniqueName="Comment" AllowFiltering="false"
                    Visible="true">
                    <ItemTemplate>
                        <asp:TextBox ID="txtComment" runat="server" Text='<%# Eval("Comment") %>' TextMode="MultiLine"
                            Width="150px"></asp:TextBox>
                    </ItemTemplate>
                    <EditItemTemplate>
                    </EditItemTemplate>
                    <HeaderStyle Width="18%"></HeaderStyle>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderText="Payment / Charges" UniqueName="operation"
                    AllowFiltering="false">
                    <ItemTemplate>
                        <asp:DropDownList ID="ddlPayment" runat="server" onchange="return OnChargeCodeChange(this);">
                            <asp:ListItem Text="--Select--" Value="-1"></asp:ListItem>
                            <asp:ListItem Text="Cash" Value="0"></asp:ListItem>
                            <asp:ListItem Text="Check" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Credit" Value="2"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlCategory" runat="server" Width="200px" onchange="return OnChargeCodeChange(this);">
                        </asp:DropDownList>
                    </ItemTemplate>
                    <EditItemTemplate>
                    </EditItemTemplate>
                    <HeaderStyle Width="22%"></HeaderStyle>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderText="Fees" UniqueName="Debit" AllowFiltering="false"
                    ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:TextBox ID="txtDebit" runat="server" MaxLength="9" Style="text-align: right"
                            Text='<%# Eval("Debit") %>' Width="70px"></asp:TextBox>
                        <asp:HiddenField ID="lblDebit1" runat="server" Value='<%# Eval("Debit") %>'></asp:HiddenField>
                    </ItemTemplate>
                    <EditItemTemplate>
                    </EditItemTemplate>
                    <HeaderStyle Width="9%"></HeaderStyle>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderText="Payment" UniqueName="Credit" AllowFiltering="false"
                    HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:TextBox ID="txtCredit" runat="server" MaxLength="9" Style="text-align: right"
                            Text='<%# Eval("Credit") %>' Width="70px"></asp:TextBox>
                        <asp:HiddenField ID="lblCredit1" runat="server" Value='<%# Eval("Credit") %>'></asp:HiddenField>
                    </ItemTemplate>
                    <EditItemTemplate>
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
        <%--<asp:Button ID="btnBottomPrint" runat="server" Text="Print" CssClass="btn" Style="float: right;"
            OnClick="btnPrint_Click" />&nbsp;--%>
        <div class=" left">
            <asp:Button ID="btnBottomEditAll" runat="server" Text="Update" CssClass="btn" OnClientClick="javascript:return isCheck();"
                OnClick="btnEditAll_Click" />
        </div>
        <div class=" left" style="padding-left: 5px;">
            <asp:Button ID="btnBottomBack" runat="server" Text="Back" CssClass="btn" />
        </div>
    </div>
    <div class="clear">
    </div>
    <telerik:RadAjaxLoadingPanel ID="RAPL1" runat="server" Transparency="10">
    </telerik:RadAjaxLoadingPanel>
    <asp:ValidationSummary ID="valSum" runat="server" ValidationGroup="LedgerValidate"
        ShowMessageBox="true" ShowSummary="false" />
</asp:Content>
