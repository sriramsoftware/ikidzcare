<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="ProgClassCategory.aspx.cs"
    Inherits="DayCare.UI.ProgClassCategory" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../css/DayCare.css" rel="stylesheet" type="text/css" />
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <script src="../JavaScript/common.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server" AsyncPostBackErrorMessage="We cannot serve your request right now. Try again later."
        ScriptMode="Release" AsyncPostBackTimeout="1800" runat="server" EnablePageMethods="True">
    </telerik:RadScriptManager>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" EnableAJAX="true" EnableEmbeddedScripts="true">
    </telerik:RadAjaxManager>
    <div class="clear">
    </div>
    <div class="clear">
    </div>
    <telerik:RadGrid ID="rgProgClassCategory" runat="server" AutoGenerateColumns="false"
        AllowFilteringByColumn="false" AllowPaging="true" AllowSorting="true" EnableLinqExpression="false"
        EnableEmbeddedSkins="true" EnableAjaxSkinRendering="true" ReorderColumnOnClient="true"
        GridLines="None" CssClass="RemoveBorders" ShowGroupPanel="false" allowColumnReorder="True"
        Width="100%" Height="600px" BorderWidth="0px" OnNeedDataSource="rgProgClassCategory_NeedDataSource"
        OnEditCommand="rgProgClassCategory_EditCommand" OnInsertCommand="rgProgClassCategory_InsertCommand"
        OnItemCreated="rgProgClassCategory_ItemCreated" OnItemDataBound="rgProgClassCategory_ItemDataBound"
        OnUpdateCommand="rgProgClassCategory_UpdateCommand" OnItemCommand="rgProgClassCategory_ItemCommand">
        <ItemStyle Wrap="true" />
        <PagerStyle AlwaysVisible="true" Mode="NextPrevAndNumeric" ShowPagerText="true" />
        <MasterTableView DataKeyNames="Id" PagerStyle-AlwaysVisible="true" TableLayout="Auto"
            AllowFilteringByColumn="true">
            <Columns>
                <telerik:GridTemplateColumn HeaderText="Assign" UniqueName="Assign" AllowFiltering="false">
                    <ItemTemplate>
                        <asp:CheckBox ID="Assign" runat="server" AutoPostBack="true" OnCheckedChanged="Assign_CheckedChanged">
                        </asp:CheckBox>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderText="ClassCategory" UniqueName="Name" AllowFiltering="true"
                    SortExpression="ClassCategory">
                    <ItemTemplate>
                        <asp:Label ID="lblClassCategory" runat="server" Text='<%# Eval("ClassCategoryName") %>'></asp:Label>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderText="ClassCategoryId" UniqueName="Id" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblClassCategoryId" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn Visible="false" UniqueName="ProgClassCatId">
                    <ItemTemplate>
                        <asp:Label ID="lblProgClassCatId" runat="server" Text='<%# Eval("Assign") %>'></asp:Label>
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
            <telerik:AjaxSetting AjaxControlID="rgProgClassCategory">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgProgClassCategory" LoadingPanelID="RAPL1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <telerik:RadAjaxLoadingPanel ID="RAPL1" runat="server" Transparency="10">
    </telerik:RadAjaxLoadingPanel>
    </form>
</body>
</html>
