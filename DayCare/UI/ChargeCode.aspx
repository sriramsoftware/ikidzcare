<%@ Page Title="" Language="C#" MasterPageFile="~/UI/DayCareIndex.Master" AutoEventWireup="true"
    CodeBehind="ChargeCode.aspx.cs" Inherits="DayCare.UI.ChargeCode" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="clear">
    </div>
    <h3 class="title">
        <img src="../images/arrow.png" />&nbsp; Charge Code</h3>
    <div class="clear">
    </div>
    <telerik:RadGrid ID="rgChargeCode" runat="server" AutoGenerateColumns="false" AllowFilteringByColumn="false"
        AllowPaging="true" AllowSorting="true" EnableLinqExpression="false" EnableEmbeddedSkins="true"
        EnableAjaxSkinRendering="true" ReorderColumnOnClient="true" GridLines="None"
        CssClass="RemoveBorders" ShowGroupPanel="false" allowColumnReorder="True" Width="100%"
        Height="430px" BorderWidth="0px" OnNeedDataSource="rgChargeCode_NeedDataSource"
        OnEditCommand="rgChargeCode_EditCommand" OnInsertCommand="rgChargeCode_InsertCommand"
        OnItemCreated="rgChargeCode_ItemCreated" OnItemDataBound="rgChargeCode_ItemDataBound"
        OnUpdateCommand="rgChargeCode_UpdateCommand" OnItemCommand="rgCharge_ItemCommand">
        <ItemStyle Wrap="true" />
        <PagerStyle AlwaysVisible="true" Mode="NextPrevAndNumeric" ShowPagerText="true" Position="Top" />
        <MasterTableView DataKeyNames="Id" PagerStyle-AlwaysVisible="true" EditMode="InPlace"
            CommandItemDisplay="TopAndBottom" InsertItemDisplay="Top" TableLayout="Auto"
            AllowFilteringByColumn="true" Width="100%">
            <CommandItemSettings AddNewRecordText="Add New Charge Code" />
            <Columns>
                <telerik:GridEditCommandColumn ButtonType="ImageButton" HeaderStyle-Width="5%" Reorderable="true"
                    HeaderText="Edit" UpdateText="Edit" UniqueName="Edit">
                </telerik:GridEditCommandColumn>
                <%--<telerik:GridBoundColumn DataField ="Name" HeaderText ="ChargeCodeName" SortExpression="Name" AllowFiltering ="true" 
UniqueName ="Name" HeaderStyle-Width ="300px">
</telerik:GridBoundColumn>--%>
                <telerik:GridTemplateColumn HeaderText="Charge Code" UniqueName="Name">
                    <ItemTemplate>
                        <asp:Label ID="lblName" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtChargeCodeName" runat="server" Text='<%# Eval("Name") %>'></asp:TextBox>
                    </EditItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderText="Category" UniqueName="Category" AllowFiltering="false">
                    <ItemTemplate>
                        <asp:Label ID="lblCategory" runat="server"></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <%-- <asp:TextBox ID="txtChargeCodeCategory" runat="server" Text='<%# Eval("Category") %>'></asp:TextBox>--%>
                        <asp:DropDownList ID="ddlCategory" runat="server">
                            <asp:ListItem Text="--Select--" Value="-1"></asp:ListItem>
                            <asp:ListItem Text="Fees" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Payment" Value="2"></asp:ListItem>
                        </asp:DropDownList>
                    </EditItemTemplate>
                </telerik:GridTemplateColumn>
                <%--<telerik:GridBoundColumn DataField ="Category" HeaderText ="ChargeCodeCategory" SortExpression ="Category" AllowFiltering="true" UniqueName="Category"></telerik:GridBoundColumn>
--%>
                <%--<telerik:GridCheckBoxColumn DataField="DebitCrdit" HeaderText="Debit/Crdit" SortExpression="DebitCrdit"
                    AllowFiltering="true" UniqueName="DebitCrdit" HeaderTooltip="Checked True Then Debit">
                </telerik:GridCheckBoxColumn>--%>
                <telerik:GridTemplateColumn HeaderText="Fee" UniqueName="Debit" AllowFiltering="false"
                    Visible="false">
                    <ItemTemplate>
                        <asp:RadioButton ID="rbDebit" runat="server" GroupName="DC" Enabled="false" />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:RadioButton ID="rbDebit" runat="server" GroupName="DC" />
                    </EditItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderText="Payment" UniqueName="Credit" AllowFiltering="false"
                    Visible="false">
                    <ItemTemplate>
                        <asp:RadioButton ID="rbCredit" runat="server" GroupName="DC" Enabled="false" />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:RadioButton ID="rbCredit" runat="server" GroupName="DC" />
                    </EditItemTemplate>
                </telerik:GridTemplateColumn>
            </Columns>
            <PagerStyle AlwaysVisible="true" />
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
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="rgChargeCode">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgChargeCode" LoadingPanelID="RAPL1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <telerik:RadAjaxLoadingPanel ID="RAPL1" runat="server" Transparency="10">
    </telerik:RadAjaxLoadingPanel>
</asp:Content>
