<%@ Page Title="" Language="C#" MasterPageFile="~/UI/DayCareIndex.Master" AutoEventWireup="true"
    CodeBehind="childfamily.aspx.cs" Inherits="DayCare.UI.childfamily" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/UI/UserControls/MenuLink.ascx" TagName="UCMenuLink" TagPrefix="ucl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h3 class="title">
        <ucl:UCMenuLink ID="usrMenuLink" runat="server" />
    </h3>
    <div class="clear">
    </div>
    <asp:ValidationSummary ID="valSum" runat="server" ValidationGroup="Familyvalidate"
        ShowMessageBox="true" ShowSummary="false" />
    <telerik:RadGrid ID="rgChildFamily" runat="server" CssClass="RemoveBorders" Width="100%"
        Height="700px" AllowPaging="true" AllowSorting="true" AutoGenerateColumns="false"
        OnNeedDataSource="rgChildFamily_NeedDataSource" PagerStyle-AlwaysVisible="true"
        ClientSettings-AllowDragToGroup="false" EnableLinqExpressions="false" ClientSettings-AllowAutoScrollOnDragDrop="false"
        AutoGenerateEditColumn="false" PageSize="50" BorderWidth="0" ClientSettings-Scrolling-AllowScroll="true"
        AllowFilteringByColumn="True" OnEditCommand="rgChildFamily_EditCommand" GridLines="None"
        OnItemCommand="rgChildFamily_ItemCommand" OnItemDataBound="rgChildFamily_ItemDataBound"
        OnItemCreated="rgChildFamily_ItemCreated" OnPreRender="rgChildFamily_PreRender">
        <GroupingSettings CaseSensitive="false" />
        <ItemStyle Wrap="true" />
        <HeaderContextMenu EnableEmbeddedSkins="true" />
        <PagerStyle AlwaysVisible="True" ShowPagerText="true" Mode="NextPrevAndNumeric" Position="Top" />
        <MasterTableView CommandItemDisplay="Top" TableLayout="Auto" CommandItemSettings-AddNewRecordText="Add New Family"
            DataKeyNames="Id" EditFormSettings-EditColumn-Display="false" AllowAutomaticInserts="false"
            Width="100%" AllowAutomaticUpdates="false">
            <Columns>
                <telerik:GridEditCommandColumn HeaderStyle-Width="5%" HeaderText="Edit" ButtonType="ImageButton"
                    Reorderable="False" UniqueName="Edit" ItemStyle-BorderStyle="None">
                    <HeaderStyle Width="5%"></HeaderStyle>
                </telerik:GridEditCommandColumn>
                <telerik:GridBoundColumn DataField="FamilyTitle" UniqueName="FamilyTitle" AllowFiltering="true"
                    AllowSorting="true" SortExpression="FamilyTitle" HeaderText="Family">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="UserName" UniqueName="UserName" AllowFiltering="true"
                    AllowSorting="true" HeaderText="Username" SortExpression="UserName">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Code" AllowFiltering="false" AllowSorting="false"
                    HeaderText="CheckIn/Out Code" Visible="false">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Email" AllowFiltering="false" AllowSorting="false"
                    HeaderText="Email">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="HomePhone" AllowFiltering="false" AllowSorting="false"
                    HeaderText="Phone">
                </telerik:GridBoundColumn>
                <telerik:GridCheckBoxColumn HeaderText="Status" UniqueName="Status" DataField="Active"
                    AllowFiltering="true" DataType="System.Boolean" CurrentFilterFunction="EqualTo"
                    CurrentFilterValue="True">
                </telerik:GridCheckBoxColumn>
                <%-- <telerik:GridBoundColumn DataField="MsgDisplayed" AllowFiltering="false" AllowSorting="true"
                    SortExpression="true" HeaderText="Message Displayed">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="MsgStartDate" AllowFiltering="false" AllowSorting="true"
                    SortExpression="true" DataFormatString="{0:dd-MM-yyyy}" HeaderText="Message Start Date">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="MsgEndDate" AllowFiltering="false" AllowSorting="true"
                    SortExpression="true" DataFormatString="{0:dd-MM-yyyy}" HeaderText="Message End Date">
                </telerik:GridBoundColumn>
                <telerik:GridCheckBoxColumn DataField="MsgActive" AllowFiltering="false" AllowSorting="true"
                    SortExpression="true" HeaderText="Message Active">
                </telerik:GridCheckBoxColumn>
                <telerik:GridTemplateColumn UniqueName="Family" AllowFiltering="false" HeaderText="Family">
                    <ItemTemplate>
                        <asp:HyperLink ID="hlFamily" runat="server"><img src="../images/BrowseIcon.gif" height="16" width="16" /></asp:HyperLink>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="Child" AllowFiltering="false" HeaderText="Child">
                    <ItemTemplate>
                        <asp:HyperLink ID="hlChild" runat="server"><img src="../images/BrowseIcon.gif" height="16" width="16" /></asp:HyperLink>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>--%>
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
    <telerik:RadAjaxLoadingPanel ID="RAPL1" runat="server" Transparency="10">
    </telerik:RadAjaxLoadingPanel>
    <asp:HiddenField ID="hdnName" runat="server" />
</asp:Content>
