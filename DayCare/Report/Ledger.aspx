<%@ Page Title="" Language="C#" MasterPageFile="~/UI/DayCareIndex.Master" AutoEventWireup="true"
    CodeBehind="Ledger.aspx.cs" Inherits="DayCare.Report.Ledger" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h3 class="title">
        <img src="../images/arrow.png" />&nbsp; Ledger <asp:Label ID="lblFamilyTitle" runat="server"></asp:Label></h3>
    <table style="width: 100%;">
        <tr>
            <td>
                Balance
            </td>
            <td>
                <asp:TextBox ID="txtBalance" runat="server" Text="0" ReadOnly="true"></asp:TextBox>
            </td>
            <td>
                YTDPay
            </td>
            <td>
                <asp:TextBox ID="txtYTDPay" runat="server" Text="0"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Date
            </td>
            <td>
                <telerik:RadDatePicker ID="rdpDate" runat="server">
                </telerik:RadDatePicker>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="rdpDate"
                    ErrorMessage="Please Select Date." Text="*" Font-Size="0" ValidationGroup="StaffValidate"></asp:RequiredFieldValidator>
            </td>
            <td>
                Category
            </td>
            <td>
                <asp:DropDownList ID="ddlCategory" runat="server">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfv1" runat="server" ControlToValidate="ddlCategory"
                    Font-Size="0" ErrorMessage="Please select Category." InitialValue="00000000-0000-0000-0000-000000000000"
                    Text="*" ValidationGroup="StaffValidate"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                Amount
            </td>
            <td>
                <asp:TextBox ID="txtAmount" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtAmount"
                    ErrorMessage="Please enter Amount." Text="*" Font-Size="0" ValidationGroup="StaffValidate"></asp:RequiredFieldValidator>
            </td>
            <td colspan="2">
                <asp:Button ID="btnSave" runat="server" CssClass="btn" Text="Save" OnClick="btnSave_Click"
                    ValidationGroup="StaffValidate" CausesValidation="true" />
                <asp:Button ID="btnCancel" runat="server" CssClass="btn" Text="Cancel" OnClick="btnCancel_Click"
                    CausesValidation="false" />
            </td>
        </tr>
    </table>
    <telerik:RadGrid ID="rgLedger" CssClass="RemoveBorders" runat="server" Width="100%"
        Height="430px" AllowPaging="true" AllowSorting="true" AutoGenerateColumns="false"
        OnNeedDataSource="rgLedger_NeedDataSource" PagerStyle-AlwaysVisible="true" AutoGenerateEditColumn="false"
        PageSize="15" BorderWidth="0" ClientSettings-Scrolling-AllowScroll="true" AllowFilteringByColumn="True"
        OnEditCommand="rgLedger_EditCommand" GridLines="None" OnItemCommand="rgLedger_ItemCommand"
        OnItemDataBound="rgLedger_ItemDataBound" OnItemCreated="rgLedger_ItemCreated"
        OnDeleteCommand="rgLedger_DeleteCommand" OnPreRender="rgLedger_PreRender">
        <GroupingSettings CaseSensitive="false" />
        <ItemStyle Wrap="true" />
        <HeaderContextMenu EnableEmbeddedSkins="true" />
        <PagerStyle AlwaysVisible="True" ShowPagerText="true" Mode="NextPrevAndNumeric" />
        <MasterTableView CommandItemDisplay="None" TableLayout="Auto" CommandItemSettings-AddNewRecordText="Add New Child Data"
            DataKeyNames="Id" EditFormSettings-EditColumn-Display="false" AllowAutomaticInserts="false"
            Width="100%" AllowAutomaticUpdates="false">
            <Columns>
                <%--<telerik:GridEditCommandColumn HeaderStyle-Width="5%" HeaderText="Edit" ButtonType="ImageButton"
                    Reorderable="False" UniqueName="Edit" ItemStyle-BorderStyle="None">
                    <HeaderStyle Width="5%"></HeaderStyle>
                </telerik:GridEditCommandColumn>--%>
                <telerik:GridButtonColumn CommandName="Delete" HeaderText="Delete" UniqueName="DeleteColumn"
                    ButtonType="ImageButton" Reorderable="false" ItemStyle-BorderStyle="None" ConfirmText="Do you want to delete record?"
                    ConfirmTitle="Delete Record" ConfirmDialogType="RadWindow">
                    <HeaderStyle Width="5%"></HeaderStyle>
                </telerik:GridButtonColumn>
                <telerik:GridBoundColumn DataField="TransactionDate" AllowFiltering="false" UniqueName="TransactionDate"
                    HeaderText="Date" DataFormatString="{0:MM/dd/yyyy hh:mm tt}">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Detail" AllowFiltering="false" UniqueName="Detail"
                    HeaderText="Detail">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="ChildName" AllowFiltering="false" UniqueName="ChildName"
                    HeaderText="Child">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Debit" AllowFiltering="false" UniqueName="Debit"
                    HeaderText="Debit">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Credit" AllowFiltering="false" UniqueName="Credit"
                    HeaderText="Credit">
                </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn AllowFiltering="false" UniqueName="Balance" HeaderText="Balance">
                    <ItemTemplate>
                        <asp:Label ID="lblBalance" runat="server"></asp:Label>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <%--<telerik:GridBoundColumn DataField="Balance" AllowFiltering="false" UniqueName="Balance"
                    HeaderText="Balance">
                </telerik:GridBoundColumn>--%>
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
    <telerik:RadAjaxLoadingPanel ID="RAPL1" runat="server">
    </telerik:RadAjaxLoadingPanel>
    <asp:ValidationSummary ID="valSum" runat="server" ValidationGroup="StaffValidate"
        ShowMessageBox="true" ShowSummary="false" />
</asp:Content>
