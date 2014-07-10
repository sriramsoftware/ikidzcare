<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SyncLogDetails.aspx.cs"
    Inherits="DayCare.UI.SyncLogDetails" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../css/DayCare.css" rel="stylesheet" type="text/css" />
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <br />
        <br />
        <div class="innerMain">
            <div class="clear">
            </div>
            <h3 class="title">
                <img src="../images/arrow.png" />&nbsp;Sync Log</h3>
            <div class="clear">
            </div>
            <telerik:RadScriptManager ID="RadScriptManager1" runat="server" AsyncPostBackErrorMessage="We cannot serve your request right now. Try again later."
                ScriptMode="Release" AsyncPostBackTimeout="1800" EnablePageMethods="True">
            </telerik:RadScriptManager>
            <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" EnableAJAX="true" EnableEmbeddedScripts="true">
            </telerik:RadAjaxManager>
            <telerik:RadGrid ID="rgSyncLogList" CssClass="RemoveBorders" runat="server" Width="990px"
                Height="800px" AllowPaging="true" AllowSorting="true" AutoGenerateColumns="false"
                PageSize="30" OnNeedDataSource="rgSyncLogList_NeedDataSource" PagerStyle-AlwaysVisible="true"
                BorderWidth="0" ClientSettings-Scrolling-AllowScroll="true" AllowFilteringByColumn="True"
                GridLines="None">
                <GroupingSettings CaseSensitive="false" />
                <ItemStyle Wrap="true" />
                <HeaderContextMenu EnableEmbeddedSkins="true" />
                <PagerStyle AlwaysVisible="True" ShowPagerText="true" Mode="NextPrevAndNumeric" />
                <MasterTableView CommandItemDisplay="None" TableLayout="Auto" DataKeyNames="Id" Width="100%">
                    <Columns>
                        <telerik:GridTemplateColumn AllowFiltering="false" HeaderStyle-Width="10%">
                            <ItemStyle Width="10%" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn HeaderText="Sync By" UniqueName="FullName" DataField="FullName"
                            HeaderStyle-Width="15%">
                            <ItemStyle Width="15%" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="Date Time" UniqueName="Datetime" DataField="Datetime"
                            AllowSorting="false" AllowFiltering="false" HeaderStyle-Width="15%" >
                        </telerik:GridBoundColumn>
                    </Columns>
                </MasterTableView>
                <ClientSettings AllowColumnsReorder="false" AllowDragToGroup="false" ReorderColumnsOnClient="True">
                    <Selecting AllowRowSelect="True" UseClientSelectColumnOnly="true" />
                    <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="True" />
                </ClientSettings>
                <FilterMenu EnableEmbeddedSkins="true" ExpandAnimation-Type="Linear">
                    <ExpandAnimation Type="Linear" />
                </FilterMenu>
            </telerik:RadGrid>
            <asp:HiddenField ID="hdnName" runat="server" />
            <telerik:RadAjaxManagerProxy ID="AjaxManagerProxy1" runat="server">
                <AjaxSettings>
                    <telerik:AjaxSetting AjaxControlID="rgSyncLogList">
                        <UpdatedControls>
                            <telerik:AjaxUpdatedControl ControlID="rgSyncLogList" LoadingPanelID="RAPL1" />
                        </UpdatedControls>
                    </telerik:AjaxSetting>
                </AjaxSettings>
            </telerik:RadAjaxManagerProxy>
            <telerik:RadAjaxLoadingPanel ID="RAPL1" runat="server" Transparency="10">
            </telerik:RadAjaxLoadingPanel>
        </div>
    </div>
    </form>
</body>
</html>
