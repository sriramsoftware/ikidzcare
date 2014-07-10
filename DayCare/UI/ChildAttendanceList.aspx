<%@ Page Title="" Language="C#" MasterPageFile="~/UI/DayCareIndex.Master" AutoEventWireup="true"
    CodeBehind="ChildAttendanceList.aspx.cs" Inherits="DayCare.UI.ChildAttendanceList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h3 class="title">
        <img src="../images/arrow.png" />&nbsp; Child List</h3>
    <telerik:RadGrid ID="rgChildAttendsList" runat="server" CssClass="RemoveBorders"
        Width="100%" Height="950px" AllowPaging="true" AllowSorting="true" AutoGenerateColumns="false"
        OnNeedDataSource="rgChildAttendsList_NeedDataSource" PagerStyle-AlwaysVisible="true"
        ClientSettings-AllowDragToGroup="false" ClientSettings-AllowAutoScrollOnDragDrop="false"
        AutoGenerateEditColumn="false" PageSize="50" BorderWidth="0" ClientSettings-Scrolling-AllowScroll="true"
        AllowFilteringByColumn="True" GridLines="None" OnItemCommand="rgChildAttendsList_ItemCommand"
        OnItemDataBound="rgChildAttendsList_ItemDataBound">
        <GroupingSettings CaseSensitive="false" />
        <ItemStyle Wrap="true" />
        <HeaderContextMenu EnableEmbeddedSkins="true" />
        <PagerStyle AlwaysVisible="True" ShowPagerText="true" Mode="NextPrevAndNumeric" Position="Top" />
        <MasterTableView CommandItemDisplay="None" TableLayout="Auto" DataKeyNames="ChildSchoolYearId"
            EditFormSettings-EditColumn-Display="false" AllowAutomaticInserts="false" Width="100%"
            AllowAutomaticUpdates="false">
            <Columns>
                <telerik:GridEditCommandColumn HeaderStyle-Width="5%" HeaderText="Edit" ButtonType="ImageButton"
                    Reorderable="False" UniqueName="Edit" ItemStyle-BorderStyle="None">
                    <HeaderStyle Width="5%"></HeaderStyle>
                </telerik:GridEditCommandColumn>
                <telerik:GridTemplateColumn UniqueName="ChildImage" HeaderText="Image" AllowFiltering="false">
                    <ItemTemplate>
                        <asp:Image ID="imgChild" runat="server" Height="32" Width="32" />
                    </ItemTemplate>
                    <HeaderStyle Width="15%"></HeaderStyle>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="FullName" AllowFiltering="true" AllowSorting="true"
                    SortExpression="true" UniqueName="FullName" HeaderText="Child Name">
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
    <telerik:RadAjaxManagerProxy ID="AjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="rgChildAttendsList">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgChildAttendsList" LoadingPanelID="RAPL1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <telerik:RadAjaxLoadingPanel ID="RAPL1" runat="server" Transparency="10">
    </telerik:RadAjaxLoadingPanel>
    <asp:HiddenField ID="hdnName" runat="server" />
</asp:Content>
