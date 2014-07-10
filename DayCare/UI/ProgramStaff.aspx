<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="ProgramStaff.aspx.cs"
    Inherits="DayCare.UI.ProgramStaff" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../css/DayCare.css" rel="stylesheet" type="text/css" />
    <link href="../css/style.css" rel="stylesheet" type="text/css" />

    <script src="../JavaScript/common.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
        function CheckActive(obj) {

            var prefix = obj.id.substring(0, obj.id.indexOf("chkStaff"));

            var chkActive = document.getElementById(prefix + "chkActive");
            if (obj.checked == true && chkActive != null) {
                chkActive.checked = true;
            }
            else {
                chkActive.checked = false;
            }
            return true;
        }
    </script>

</head>
<body class="loginbody">
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
    <telerik:RadGrid ID="rgProgramStaff" runat="server" AutoGenerateColumns="false" AllowFilteringByColumn="false"
        AllowPaging="true" AllowSorting="true" EnableLinqExpression="false" EnableEmbeddedSkins="true"
        EnableAjaxSkinRendering="true" ReorderColumnOnClient="true" GridLines="None"
        CssClass="RemoveBorders" ShowGroupPanel="false" allowColumnReorder="True" Width="100%"
        Height="330px" BorderWidth="0px" OnNeedDataSource="rgProgramStaff_NeedDataSource"
        OnEditCommand="rgProgramStaff_EditCommand" OnInsertCommand="rgProgramStaff_InsertCommand"
        OnItemCreated="rgProgramStaff_ItemCreated" OnItemDataBound="rgProgramStaff_ItemDataBound"
        OnUpdateCommand="rgProgramStaff_UpdateCommand" OnItemCommand="rgProgramStaff_ItemCommand">
        <ItemStyle Wrap="true" />
        <PagerStyle AlwaysVisible="true" Mode="NextPrevAndNumeric" ShowPagerText="true" />
        <MasterTableView DataKeyNames="Id" PagerStyle-AlwaysVisible="true" TableLayout="Auto"
            AllowFilteringByColumn="true">
            <Columns>
                <telerik:GridTemplateColumn HeaderText="Assign" UniqueName="SelectCheckStaff" DataType="System.Boolean">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkStaff" runat="server" AutoPostBack="true" OnCheckedChanged="chkStaff_CheckedChanged" /><%--onclick="javascript:return CheckActive(this);"--%>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderText="StaffId" Visible="false" UniqueName="StaffId"
                    AllowFiltering="true" SortExpression="StaffId">
                    <ItemTemplate>
                        <asp:Label ID="lblStaffId" Visible="false" runat="server" Text='<%# Eval("StaffId") %>'></asp:Label>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn HeaderText="Full Name" UniqueName="StaffFullName" DataField="StaffFullName"
                    SortExpression="StaffFullName">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="User Name" UniqueName="StaffUserName" DataField="StaffUserName"
                    SortExpression="StaffUserName">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Email" UniqueName="StaffEmail" DataField="StaffEmail"
                    SortExpression="StaffEmail">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="City" UniqueName="StaffCity" DataField="StaffCity"
                    SortExpression="StaffCity">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Main Phone" UniqueName="StaffMainPhone" DataField="StaffMainPhone"
                    SortExpression="StaffMainPhone" AllowFiltering="false">
                    <HeaderStyle Width="80px" />
                </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn HeaderText="Active" UniqueName="Active" Visible="false">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkActive" runat="server" />
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
    <div width="100%" align="right" style="background-color: #0876cb;">
        <asp:Button ID="btnSave" Text="Save" Visible="false" runat="server" OnClick="btnSave_Click" />
    </div>
    <asp:HiddenField ID="hdnName" runat="server" />
    <telerik:RadAjaxManagerProxy ID="AjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="rgProgramStaff">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgProgramStaff" LoadingPanelID="RAPL1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <telerik:RadAjaxLoadingPanel ID="RAPL1" runat="server" Transparency="10">
    </telerik:RadAjaxLoadingPanel>
    </form>
</body>
</html>
