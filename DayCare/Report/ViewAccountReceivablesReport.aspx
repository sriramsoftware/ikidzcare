<%@ Page Title="" Language="C#" MasterPageFile="~/UI/DayCareIndex.Master" AutoEventWireup="true"
    CodeBehind="ViewAccountReceivablesReport.aspx.cs" Inherits="DayCare.Report.ViewAccountReciableReport" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h3 class="title">
        <img src="../images/arrow.png" alt="" />&nbsp;Accounts Receivable Report
    </h3>
      <div class="clear" style="height:8px;"></div>
   <div style="font-family:Tahoma; font-size:14px;">
        <asp:Label ID="lblLastUpdatedLedger" Text="Note: The ledger was last updated on {0}. Do you wish to update the ledger to get up to date report? If yes," runat="server" ForeColor="Blue"></asp:Label>
        <asp:LinkButton ID="lnkUpdateLedger" runat="server" Text="Click Here" OnClick="btnUpdateLedger_OnClick"></asp:LinkButton>
        <%--<div style="float: right;">
            <asp:Button ID="btnUpdateLedger" Text="Update Ledger" runat="server" CssClass="btn" OnClick="btnUpdateLedger_OnClick" /></div>--%>
    </div>
     <div class="clear" style="height:5px;"></div>
    <fieldset>
        <legend><strong>Criteria</strong></legend>
        <div class="fieldDiv">
            <div class="box pndR15" style="width: 150px;">
                Select "As on" Date:
                <br />
                <telerik:RadDatePicker ID="rdpStartDate" runat="server">
                </telerik:RadDatePicker>
            </div>
            <div class="box" style="margin-top: -8px;">
                <br />
                <%--<asp:Button ID="btnGrid" runat="server" Text="View In Grid" CssClass="btn" OnClick="btnGrid_Click" />--%>
                <asp:Button ID="btnSave" runat="server" Text="View Report" CssClass="viewReport"
                    OnClick="btnSave_Click" />
            </div>
        </div>
    </fieldset>
    <%--<telerik:RadGrid ID="rgAccRec" runat="server" AutoGenerateColumns="false" AllowFilteringByColumn="false"
        AllowPaging="true" AllowSorting="true" EnableLinqExpression="false" EnableEmbeddedSkins="true"
        EnableAjaxSkinRendering="true" ReorderColumnOnClient="true" GridLines="None"
        CssClass="RemoveBorders" ShowGroupPanel="false" allowColumnReorder="True" Width="100%"
        Height="430px" BorderWidth="0px" OnNeedDataSource="rgAccRec_NeedDataSource">
        <ItemStyle Wrap="true" />
        <PagerStyle AlwaysVisible="true" Mode="NextPrevAndNumeric" ShowPagerText="true" />
        <MasterTableView PagerStyle-AlwaysVisible="true" EditMode="InPlace" CommandItemDisplay="None"
            InsertItemDisplay="Top" TableLayout="Auto" AllowFilteringByColumn="true" Width="100%">
            <Columns>
                <telerik:GridBoundColumn DataField="FamilyTitle" HeaderText="Family" SortExpression="FamilyTitle"
                    AllowFiltering="true" UniqueName="FamilyTitle">
                    <HeaderStyle Width="30%" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="1-7 Days" DataField="First" UniqueName="First"
                    HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" AllowFiltering="false">
                    <HeaderStyle Width="8%" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="8-14 Days" DataField="Second" UniqueName="Second"
                    HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" AllowFiltering="false">
                    <HeaderStyle Width="8%" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="15-21 Days" DataField="Third" UniqueName="Third"
                    HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" AllowFiltering="false">
                    <HeaderStyle Width="8%" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="21+ Days" DataField="Four" UniqueName="Four"
                    HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" AllowFiltering="false">
                    <HeaderStyle Width="8%" />
                </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn HeaderText="" Visible="false" AllowFiltering="false">
                    <ItemTemplate>
                        <headerstyle width="18%" />
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
    <asp:HiddenField ID="hdnName" runat="server" />
    <telerik:RadAjaxManagerProxy ID="AjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="rgAccRec">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgAccRec" LoadingPanelID="RAPL1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <telerik:RadAjaxLoadingPanel ID="RAPL1" runat="server" Transparency="10">
    </telerik:RadAjaxLoadingPanel>--%>
</asp:Content>
