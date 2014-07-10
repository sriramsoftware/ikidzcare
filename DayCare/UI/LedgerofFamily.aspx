<%@ Page Title="" Language="C#" MasterPageFile="~/UI/DayCareIndex.Master" AutoEventWireup="true"
    CodeBehind="LedgerofFamily.aspx.cs" Inherits="DayCare.UI.LedgerofFamily" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/UI/UserControls/MenuLink.ascx" TagName="UCMenuLink" TagPrefix="ucl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript" language="javascript">


        function ShowLateFee(ChildFamilyId, Title) {
            var oWnd = radopen('LateFee.aspx?ChildFamilyId=' + ChildFamilyId);
            setTimeout(function() { oWnd.setActive(true); }, 100);
            oWnd.set_title(Title + ' Late Fee');
            oWnd.center();
        }

       
    </script>

    <h3 class="title">
        <ucl:UCMenuLink ID="usrMenuLink" runat="server" />
    </h3>
    <div class="clear">
    </div>
    <asp:ValidationSummary ID="valSum" runat="server" ValidationGroup="Familyvalidate"
        ShowMessageBox="true" ShowSummary="false" />
    <telerik:RadGrid ID="rgFamilyLedger" runat="server" CssClass="RemoveBorders" Width="100%"
        Height="800px" AllowPaging="true" AllowSorting="true" AutoGenerateColumns="false"
        OnNeedDataSource="rgFamilyLedger_NeedDataSource" PagerStyle-AlwaysVisible="true"
        ClientSettings-AllowDragToGroup="false" ClientSettings-AllowAutoScrollOnDragDrop="false"
        AutoGenerateEditColumn="false" PageSize="50" BorderWidth="0" ClientSettings-Scrolling-AllowScroll="true"
        AllowFilteringByColumn="True" OnEditCommand="rgFamilyLedger_EditCommand" GridLines="None"
        OnItemCommand="rgFamilyLedger_ItemCommand" OnItemDataBound="rgFamilyLedger_ItemDataBound"
        OnPreRender="rgFamilyLedger_PreRender" OnItemCreated="rgFamilyLedger_ItemCreated"
        EnableLinqExpressions="false">
        <GroupingSettings CaseSensitive="false" />
        <ItemStyle Wrap="true" />
        <HeaderContextMenu EnableEmbeddedSkins="true" />
        <PagerStyle AlwaysVisible="True" ShowPagerText="true" Mode="NextPrevAndNumeric" Position="Top" />
        <MasterTableView CommandItemDisplay="None" TableLayout="Auto" CommandItemSettings-AddNewRecordText="Add New Family"
            DataKeyNames="Id" EditFormSettings-EditColumn-Display="false" AllowAutomaticInserts="false"
            Width="100%" AllowAutomaticUpdates="false">
            <Columns>
                <telerik:GridEditCommandColumn HeaderStyle-Width="5%" HeaderText="Edit" ButtonType="ImageButton"
                    Reorderable="False" UniqueName="Edit" ItemStyle-BorderStyle="None">
                    <HeaderStyle Width="5%"></HeaderStyle>
                </telerik:GridEditCommandColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="15%" DataField="FamilyTitle" AllowFiltering="true"
                    AllowSorting="true" UniqueName="FamilyTitle" SortExpression="FamilyTitle" HeaderText="Guardian"
                    ItemStyle-Font-Bold="true">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="ChildName" AllowFiltering="true" AllowSorting="false"
                    HeaderText="Child">
                    <HeaderStyle Width="25%"></HeaderStyle>
                </telerik:GridBoundColumn>
                <%--<telerik:GridBoundColumn DataField="Email" AllowFiltering="false" AllowSorting="false"
                    HeaderText="Email">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="HomePhone" AllowFiltering="false" AllowSorting="false"
                    HeaderText="Phone">
                    <HeaderStyle Width="10%"></HeaderStyle>
                </telerik:GridBoundColumn>--%>
                <telerik:GridBoundColumn DataField="OpBal" AllowFiltering="false" AllowSorting="false"
                    HeaderStyle-HorizontalAlign="Right" DataType="System.Decimal" ItemStyle-HorizontalAlign="Right"
                    HeaderText="Op. Bal.">
                    <HeaderStyle Width="9%"></HeaderStyle>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Debit" AllowFiltering="false" AllowSorting="false"
                    HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" HeaderText="Fees">
                    <HeaderStyle Width="9%"></HeaderStyle>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Credit" AllowFiltering="false" AllowSorting="false"
                    HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" HeaderText="Payment">
                    <HeaderStyle Width="9%"></HeaderStyle>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Balance" AllowFiltering="false" AllowSorting="false"
                    HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" HeaderText="Balance">
                    <HeaderStyle Width="9%"></HeaderStyle>
                </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn UniqueName="" HeaderText="Late Fees Charge" AllowFiltering="false"
                    ItemStyle-HorizontalAlign="Center" Visible="false">
                    <ItemTemplate>
                        <asp:HyperLink ID="hlLateFees" runat="server" NavigateUrl=""><img src="../images/BrowseIcon.gif" title="Late Fees Charge" height="16" width="16" /></asp:HyperLink>
                        <%--<asp:ImageButton
                            ID="imgBtnProgClassRoom" CommandName="ProgClassRoom" runat="server" />--%>
                    </ItemTemplate>
                    <HeaderStyle Width="5%"></HeaderStyle>
                </telerik:GridTemplateColumn>
                <telerik:GridCheckBoxColumn HeaderText="Status" UniqueName="Status" DataField="Active"
                    AllowFiltering="true" HeaderStyle-Width="5%" DataType="System.Boolean" CurrentFilterFunction="EqualTo"
                    CurrentFilterValue="True">
                </telerik:GridCheckBoxColumn>
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
    <telerik:RadWindowManager ID="rwm1" runat="server" DestroyOnClose="false" InitialBehaviors="Resize"
        Modal="true" KeepInScreenBounds="true" ReloadOnShow="true" Title="Shortcuts"
        AutoSize="false" Behaviors="Close" Width="500px" Height="270px" Skin="Office2007"
        Overlay="true" ShowContentDuringLoad="false" Animation="Fade" VisibleStatusbar="false">
    </telerik:RadWindowManager>
    <telerik:RadAjaxManagerProxy ID="AjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="rgFamilyLedger">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgFamilyLedger" LoadingPanelID="RAPL1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <telerik:RadAjaxLoadingPanel ID="RAPL1" runat="server" Transparency="10">
    </telerik:RadAjaxLoadingPanel>
    <asp:HiddenField ID="hdnName" runat="server" />
</asp:Content>
