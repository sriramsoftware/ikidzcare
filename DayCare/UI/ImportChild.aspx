<%@ Page Title="" Language="C#" MasterPageFile="~/UI/DayCareIndex.Master" AutoEventWireup="true"
    CodeBehind="ImportChild.aspx.cs" Inherits="DayCare.UI.ImportChild" %>

<%@ Register Src="~/UI/UserControls/MenuLink.ascx" TagName="UCMenuLink" TagPrefix="ucl" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script>
        function ChkAll(obj) {
            var input = document.getElementsByTagName('input');
            for (var cnt = 0; cnt < input.length; cnt++) {
                if (input[cnt].id.indexOf("chkChildHeader") != 0 && input[cnt].id.indexOf("FilterCheckBox") == -1 && input[cnt].type == "checkbox" && input[cnt].disabled == false) {
                    input[cnt].checked = obj.checked;
                }
            }
        }
        function ChkUnchk(obj) {
            var flag = true;
            if (obj.checked == false) {
                flag = false;
            }
            else {
                var input = document.getElementsByTagName('input');
                for (var cnt = 0; cnt < input.length; cnt++) {
                    if (input[cnt].id.indexOf("chkChildHeader") == -1 && input[cnt].type == "checkbox" && input[cnt].checked == false) {
                        flag = false;
                        break;
                    }
                }
            }
            document.getElementById("ctl00_ContentPlaceHolder1_rgChildList_ctl00_ctl02_ctl01_chkChildHeader").checked = flag;
        }

        function isCheck() {
            var input = document.getElementsByTagName('input');
            var flag = false;
            for (var cnt = 0; cnt < input.length; cnt++) {
                if (input[cnt].id.indexOf("chkChildHeader") == -1 && input[cnt].type == "checkbox" && input[cnt].id.indexOf("FilterCheckBox") == -1 && input[cnt].checked == true && input[cnt].disabled == false) {
                    flag = true;
                    break;
                }
            }
            if (flag) {
                //if (confirm("Do you want to delete?")) {
                return true;
                //}
                //else {
                //    return false;
                //}
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
    <div>
        <table style="width: 100%">
            <tr>
                <td align="left" style="padding: 5px 10px 0 0; font-family: Arial, Helvetica, sans-serif;
                    font-size: 13px; color: #333;">
                    <asp:Label ID="lblSchoolYear" runat="server" Text="Select School Year:"></asp:Label>
                    <asp:DropDownList ID="ddlPrevSchoolyearOfSelectedCurrentSchoolYear" runat="server"
                        AutoPostBack="true" OnSelectedIndexChanged="ddlPrevSchoolyearOfSelectedCurrentSchoolYear_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Button ID="btnImportChild" runat="server" Text="Import" OnClientClick="javascript:return isCheck();"
                        CssClass="btn" Style="float: right;" OnClick="btnImportChild_Click" />
                </td>
            </tr>
        </table>
        <%--<div class="left">
            a
        </div>
        <div class=" left" style="padding: 15px">
            <asp:Button ID="btnImportChild" runat="server" Text="Import" OnClientClick="javascript:return isCheck();"
                CssClass="btn" Style="float: right;" OnClick="btnImportChild_Click" />
        </div>--%>
    </div>
    <div class="clear">
    </div>
    <div runat="server" id="divErrorMsg" style="display: none; text-align: center; padding: 5px 10px 0 0;
        font-family: Arial, Helvetica, sans-serif; font-size: 15px;">
        <table width="100%" align="center">
            <tr>
                <td align="center">
                    <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <telerik:RadGrid ID="rgChildList" runat="server" CssClass="RemoveBorders" Width="100%"
        Height="800px" AllowPaging="true" AllowSorting="true" AutoGenerateColumns="false"
        OnNeedDataSource="rgChildList_NeedDataSource" PagerStyle-AlwaysVisible="true"
        EnableLinqExpressions="false" ClientSettings-AllowDragToGroup="false" ClientSettings-AllowAutoScrollOnDragDrop="false"
        AutoGenerateEditColumn="false" PageSize="50" BorderWidth="0" ClientSettings-Scrolling-AllowScroll="true"
        AllowFilteringByColumn="True" GridLines="None" OnItemCommand="rgChildList_ItemCommand"
        OnItemDataBound="rgChildList_ItemDataBound" OnItemCreated="rgChildList_ItemCreated"
        OnPreRender="rgChildList_PreRender">
        <GroupingSettings CaseSensitive="false" />
        <ItemStyle Wrap="true" />
        <HeaderContextMenu EnableEmbeddedSkins="true" />
        <PagerStyle AlwaysVisible="True" ShowPagerText="true" Mode="NextPrevAndNumeric" Position="Top" />
        <MasterTableView CommandItemDisplay="None" TableLayout="Auto" DataKeyNames="ChildDataId"
            EditFormSettings-EditColumn-Display="false" AllowAutomaticInserts="false" Width="100%"
            AllowAutomaticUpdates="false">
            <Columns>
                <telerik:GridTemplateColumn HeaderText="" UniqueName="DeleteAll" AllowFiltering="false">
                    <HeaderTemplate>
                        <asp:CheckBox ID="chkChildHeader" runat="server" onclick="javascript:ChkAll(this);" />
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="chkChildItem" runat="server" onclick="javascript:ChkUnchk(this);" />
                    </ItemTemplate>
                    <EditItemTemplate>
                    </EditItemTemplate>
                    <HeaderStyle Width="150px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridTemplateColumn>
                <%--<telerik:GridTemplateColumn UniqueName="ChildImage" HeaderText="Image" AllowFiltering="false">
                    <ItemTemplate>
                        <asp:Image ID="imgChild" runat="server" Height="32" Width="32" />
                    </ItemTemplate>
                    <HeaderStyle Width="15%"></HeaderStyle>
                </telerik:GridTemplateColumn>--%>
                <telerik:GridBoundColumn DataField="FullName" AllowFiltering="true" AllowSorting="true"
                    SortExpression="FullName" UniqueName="FullName" HeaderText="Child Name">
                    <HeaderStyle Width="300px"></HeaderStyle>
                </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn UniqueName="ChildFamily" AllowFiltering="false" HeaderStyle-HorizontalAlign="Right"
                    ItemStyle-HorizontalAlign="Right" Visible="false">
                    <ItemTemplate>
                        <asp:HiddenField ID="ChildFamilyId" runat="server" Value='<%# Eval("ChildFamilyId") %>' />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="FamilyName" AllowFiltering="true" AllowSorting="true"
                    SortExpression="FamilyName" HeaderText="Family Name">
                </telerik:GridBoundColumn>
                <%-- <telerik:GridBoundColumn DataField="Email" AllowFiltering="false" AllowSorting="false"
                    HeaderText="Email">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="HomePhone" AllowFiltering="false" AllowSorting="false"
                    HeaderText="Phone">
                </telerik:GridBoundColumn>--%>
                <telerik:GridCheckBoxColumn HeaderText="Status" UniqueName="Status" DataField="Active"
                    AllowFiltering="true" DataType="System.Boolean" CurrentFilterFunction="EqualTo"
                    CurrentFilterValue="True">
                </telerik:GridCheckBoxColumn>
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
    <div class="right">
        <div class=" left" style="padding: 15px">
            <asp:Button ID="btnImportChildBottom" runat="server" Text="Import" OnClientClick="javascript:return isCheck();"
                CssClass="btn" Style="float: right;" OnClick="btnImportChild_Click" />
        </div>
    </div>
    <div class="clear">
    </div>
    <telerik:RadAjaxManagerProxy ID="AjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="rgChildList">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgChildList" LoadingPanelID="RAPL1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <telerik:RadAjaxLoadingPanel ID="RAPL1" runat="server" Transparency="10">
    </telerik:RadAjaxLoadingPanel>
</asp:Content>
