<%@ Page Title="" Language="C#" MasterPageFile="~/UI/DayCareIndex.Master" AutoEventWireup="true"
    CodeBehind="Test.aspx.cs" Inherits="DayCare.UI.Test" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<h3 class="title">
        <img src="../images/arrow.png" />&nbsp;
        Family :<asp:Label ID="lblFamilyTitle" runat="server" ></asp:Label></h3>
        <table style="width:100%;">
            <tr>
                <td>
                    Balance</td>
                <td>
                   <asp:TextBox ID="txtBalance" runat="server" ReadOnly="true"></asp:TextBox></td>
                <td>
                    YTDPay</td>
                <td>
                    <asp:TextBox ID="txtYTDPay" runat="server" ></asp:TextBox></td>
            </tr>
            <tr>
                <td>
                    Date</td>
                <td>
                    <telerik:RadDatePicker ID="rdpDate" runat="server" ></telerik:RadDatePicker></td>
                <td>
                    Category</td>
                <td>
                    <asp:DropDownList ID="ddlCategory" runat="server" ></asp:DropDownList></td>
            </tr>
            <tr>
                <td>
                    Amount</td>
                <td>
                    <asp:TextBox ID="txtAmount" runat="server" ></asp:TextBox></td>
                <td>
                    <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" /></td>
                <td>
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" /></td>
            </tr>
    </table>
        <br />
    <telerik:RadGrid ID="rgTest" CssClass="RemoveBorders" runat="server" Width="100%"
        Height="430px" AllowPaging="true" AllowSorting="true" AutoGenerateColumns="false"
        OnNeedDataSource="rgTest_NeedDataSource" PagerStyle-AlwaysVisible="true" AutoGenerateEditColumn="false"
        PageSize="5" BorderWidth="0" ClientSettings-Scrolling-AllowScroll="true" AllowFilteringByColumn="True"
        OnEditCommand="rgTest_EditCommand" GridLines="None" OnItemCommand="rgTest_ItemCommand"
        OnItemDataBound="rgTest_ItemDataBound" OnItemCreated="rgTest_ItemCreated" OnDeleteCommand="rgTest_DeleteCommand">
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
                            ButtonType="ImageButton" Reorderable="false" ItemStyle-BorderStyle="None" ConfirmText="Do you wnat to delete Record?"
                            ConfirmTitle="Delete Record" ConfirmDialogType="RadWindow">
                            <HeaderStyle Width="5%"></HeaderStyle>
                        </telerik:GridButtonColumn>
                <telerik:GridBoundColumn DataField="TransactionDate" AllowFiltering="false" UniqueName="TransactionDate"
                    HeaderText="Date" DataFormatString="{0:dd-MM-yyyy}">
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
                <telerik:GridBoundColumn DataField="Balance" AllowFiltering="false" UniqueName="Balance"
                    HeaderText="Balance">
                </telerik:GridBoundColumn>
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
</asp:Content>
