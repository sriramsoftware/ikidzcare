<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="ProgramClassRoom.aspx.cs"
    Inherits="DayCare.ProgramClassRoom" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <link href="../css/DayCare.css" rel="stylesheet" type="text/css" />

    <script src="../JavaScript/common.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript">
        function chkcombo(obj) {
            var ddlProgStaff = obj.id;
            var input = document.getElementsByTagName("select");
            var prefix = obj.id.substring(0, obj.id.indexOf("ddlProgStaff"));
            var chkClassRoom = document.getElementById(prefix + "chkClassRoom");
            var cnt = 0;
            if (chkClassRoom.checked == true) {
                for (var k = 0; k < input.length; k++) {
                    var chkbox = input[k].id.substring(0, input[k].id.indexOf("ddlProgStaff"));
                    chkbox = document.getElementById(chkbox + "chkClassRoom");
                    if (input[k].value == obj.value && obj.value != '00000000-0000-0000-0000-000000000000' && chkbox.checked == true) {
                        cnt++;
                    }
                }
            }
            if (cnt > 1) {
                alert(obj.options[obj.selectedIndex].text + " can not select more than one");
                obj.value = '00000000-0000-0000-0000-000000000000';
                return false;
            }
            return true;
        }
        function chkCheckbox(obj) {
            var ddlProgStaff = obj.id;
            var input = document.getElementsByTagName("select");
            var prefix = obj.id.substring(0, obj.id.indexOf("chkClassRoom"));
            var ddlProgStaff = document.getElementById(prefix + "ddlProgStaff");
            var cnt = 0;
            for (var k = 0; k < input.length; k++) {
                var chkClassRoom = input[k].id.substring(0, input[k].id.indexOf("ddlProgStaff"));
                chkClassRoom = document.getElementById(chkClassRoom + "chkClassRoom");
                if (input[k].value == ddlProgStaff.value && ddlProgStaff.value != '00000000-0000-0000-0000-000000000000' && chkClassRoom.checked == true) {
                    cnt++;
                }
            }
            if (cnt > 1) {
                alert(ddlProgStaff.options[ddlProgStaff.selectedIndex].text + " can not select more than one");
                ddlProgStaff.value = '00000000-0000-0000-0000-000000000000';
                return false;
            }
            if (ddlProgStaff.value == '00000000-0000-0000-0000-000000000000') {
                alert("Please select Staff");
                ddlProgStaff.focus();
                return false;
            }
            __doPostBack('" + chkClassRoom.ClientID + @"', '');
            return true;
        }
    </script>

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
    <telerik:RadGrid ID="rgProgClassRoom" runat="server" AutoGenerateColumns="false"
        AllowFilteringByColumn="false" AllowPaging="true" AllowSorting="true" EnableLinqExpression="false"
        EnableEmbeddedSkins="true" EnableAjaxSkinRendering="true" ReorderColumnOnClient="true"
        GridLines="None" CssClass="RemoveBorders" ShowGroupPanel="false" allowColumnReorder="True"
        Width="100%" Height="330px" BorderWidth="0px" OnNeedDataSource="rgProgClassRoom_NeedDataSource"
        OnEditCommand="rgProgClassRoom_EditCommand" OnInsertCommand="rgProgClassRoom_InsertCommand"
        OnItemCreated="rgProgClassRoom_ItemCreated" OnItemDataBound="rgProgClassRoom_ItemDataBound"
        OnUpdateCommand="rgProgClassRoom_UpdateCommand" OnItemCommand="rgProgClassRoom_ItemCommand">
        <ItemStyle Wrap="true" />
        <PagerStyle AlwaysVisible="true" Mode="NextPrevAndNumeric" ShowPagerText="true" />
        <MasterTableView DataKeyNames="Id" PagerStyle-AlwaysVisible="true" AllowFilteringByColumn="true">
            <Columns>
                <telerik:GridTemplateColumn HeaderText="Class Room" Visible="false" UniqueName="ClassRoomId">
                    <ItemTemplate>
                        <asp:Label ID="lblClassRoomId" Visible="false" runat="server" Text='<%# Eval("ClassRoomId") %>'></asp:Label>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderText="Class Room" AllowFiltering="true" UniqueName="Day">
                    <ItemTemplate>
                        <asp:Label ID="lblClassRoom" runat="server" Text='<%# Eval("ClassRoomName") %>'></asp:Label>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderText="Assign" UniqueName="SelectCheckStaff" DataType="System.Boolean">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkClassRoom" runat="server" AutoPostBack="true" OnCheckedChanged="chkClassRoom_CheckedChanged" /><%--onclick="javascript:return chkCheckbox(this);"--%>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderText="Staff" UniqueName="ProgStaff" AllowFiltering="true"
                    Visible="false">
                    <ItemTemplate>
                        <asp:DropDownList ID="ddlProgStaff" runat="server">
                            <%--onchange="javascript:return chkcombo(this);"--%>
                        </asp:DropDownList>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridCheckBoxColumn DataField="Active" HeaderText="Active" SortExpression="Active"
                    UniqueName="Active" HeaderTooltip="Checked True Then Active">
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
    <asp:HiddenField ID="hdnName" runat="server" />
    <telerik:RadAjaxManagerProxy ID="AjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="rgProgClassRoom">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgProgClassRoom" LoadingPanelID="RAPL1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <telerik:RadAjaxLoadingPanel ID="RAPL1" runat="server" Transparency="10">
    </telerik:RadAjaxLoadingPanel>
    </form>
</body>
</html>
