<%@ Page Title="" Language="C#" MasterPageFile="~/UI/DayCareIndex.Master" AutoEventWireup="true"
    CodeBehind="FamilyLateFee.aspx.cs" Inherits="DayCare.UI.FamilyLateFee" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .textalign
        {
            text-align: right;
        }
    </style>

    <script>
        function KeyUpForCharge(obj) {
            var input = document.getElementsByTagName("input");
            for (var cnt = 0; cnt < input.length; cnt++) {
                if (input[cnt].id.indexOf("txtFamilyLateFee") > -1 && input[cnt].type == "text") {
                    input[cnt].value = obj.value;

                }
            }

        }
        function isCheck() {
            var ErrorMsg = "";
            var focusid = "";
            var flag = true;
            var cntForFocusId = 0;
            var input = document.getElementsByTagName("input");
            for (var cnt = 0; cnt < input.length; cnt++) {
                if (input[cnt].id.indexOf("txtFamilyLateFee") > -1 && input[cnt].type == "text") {
                    var reg = /(^-?\d\d*\.\d*$)|(^-?\d\d*$)|(^-?\.\d\d*$)/;
                    if (input[cnt].value != "") {
                        if (flag) {
                            flag = false;
                        }
                        if (!reg.test(input[cnt].value)) {
                            ErrorMsg += "Some information you entered may wrong.";
                            focusid = input[cnt].id;
                            flag = false;
                            break;
                        }
                    }
                    else {
                        if (cntForFocusId == 0) {
                            focusid = input[cnt].id;
                            cntForFocusId = 1;
                        }
                    }

                }
            }
            if (flag) {
                ErrorMsg += "Please enter late fee charge atleast a family\n";
                
            }
            if (ErrorMsg.length > 0) {
                alert(ErrorMsg);
                document.getElementById(focusid).select();
                document.getElementById(focusid).focus();
                return false;
            }
            return true;
        }

        function CheckBalanceAmount() {
            var ErrorMsg = "";
            var obj = document.getElementById("<%=txtBlance.ClientID %>");
            var reg = /(^-?\d\d*\.\d*$)|(^-?\d\d*$)|(^-?\.\d\d*$)/;
            if (obj.value.replace(/^\s+|\s+$/g, "") != "") {
                if (!reg.test(obj.value)) {
                    ErrorMsg += "Please enter valid balance\n";
                }
            }
            else {
                ErrorMsg += "Please enter balance\n";
            }
            if (ErrorMsg.length > 0) {
                alert(ErrorMsg);
                obj.select();
                obj.focus();
                return false;
            }
            return true;
        }
    </script>

    <div class="clear">
    </div>
    <h3 class="title">
        <img src="../images/arrow.png" />&nbsp; Late Fees</h3>
    <div class="clear">
    </div>
    <div align="center" style="font-family: segoe ui; font-size: 12px;">
        <fieldset style="margin-top: 8px;">
            <legend><strong>Late Fees</strong></legend>
            <div class="fieldDiv">
                <table width="100%" align="center">
                    <tr>
                        <td width="58%" align="left">
                            Show everyone whose balance is&nbsp;&nbsp;>&nbsp;&nbsp;
                            <asp:TextBox ID="txtBlance" runat="server" Width="70px"></asp:TextBox>&nbsp;&nbsp;
                            and set&nbsp;&nbsp;
                            <asp:TextBox ID="txtLateCharge" runat="server" Width="70px" onkeyup="javascript:KeyUpForCharge(this);"></asp:TextBox>
                            &nbsp;&nbsp;as late fee by default
                        </td>
                        <td width="42%" align="right">
                            <asp:Button ID="btnOk" runat="server" CssClass="btn" Text="Show" OnClick="btnOk_Click"
                                OnClientClick="javascript:return CheckBalanceAmount();" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="left">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="left">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="left">
                        </td>
                    </tr>
                </table>
            </div>
        </fieldset>
    </div>
    <div class="clear">
    </div>
    <div class="right">
        <asp:Button ID="btnTopSave" runat="server" Text="Apply" CssClass="btn" Style="float: right;"
            Visible="false" OnClientClick="javascript:return isCheck();" OnClick="btnTopSave_Click" />&nbsp;
    </div>
    <div class="clear">
    </div>
    <telerik:RadGrid ID="rgLateFee" CssClass="RemoveBorders" runat="server" Width="100%" 
        AllowPaging="false" AllowSorting="true" AutoGenerateColumns="false" PagerStyle-AlwaysVisible="true"
        AutoGenerateEditColumn="false" BorderWidth="0" OnItemDataBound="rgLateFee_ItemDataBound"
        ClientSettings-Scrolling-AllowScroll="true" AllowFilteringByColumn="false">
        <GroupingSettings CaseSensitive="false" />
        <ItemStyle Wrap="true" />
        <HeaderContextMenu EnableEmbeddedSkins="true" />
        <PagerStyle AlwaysVisible="True" ShowPagerText="true" Mode="NextPrevAndNumeric" />
        <MasterTableView CommandItemDisplay="None" TableLayout="Auto" DataKeyNames="Id" EditFormSettings-EditColumn-Display="false"
            AllowAutomaticInserts="false" Width="100%" AllowAutomaticUpdates="false" EditMode="InPlace">
            <Columns>
                <telerik:GridBoundColumn DataField="ChildName" AllowFiltering="true" AllowSorting="false"
                    UniqueName="FamilyTitle"   HeaderText="Guardian" ItemStyle-Font-Bold="true">
                    <HeaderStyle Width="10%"></HeaderStyle>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Balance" AllowFiltering="false" AllowSorting="false"
                    HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" HeaderText="Balance">
                    <HeaderStyle Width="5%"></HeaderStyle>
                </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn UniqueName="" HeaderText="" AllowFiltering="false" ItemStyle-HorizontalAlign="Left">
                    <HeaderStyle Width="1%"></HeaderStyle>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="" HeaderText="Late Fees Charge" AllowFiltering="false"
                    ItemStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <asp:TextBox ID="txtFamilyLateFee" runat="server" Width="80" CssClass="textalign"></asp:TextBox>
                    </ItemTemplate>
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
        <asp:Button ID="btnBottomSave" runat="server" Text="Apply" CssClass="btn" Style="float: right;"
            Visible="false" OnClientClick="javascript:return isCheck();" OnClick="btnTopSave_Click" />&nbsp;
    </div>
    <div class="clear">
    </div>
</asp:Content>
