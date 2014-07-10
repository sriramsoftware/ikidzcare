<%@ Page Title="" Language="C#" MasterPageFile="~/UI/DayCareIndex.Master" AutoEventWireup="true"
    CodeBehind="FamilyDataList.aspx.cs" Inherits="DayCare.UI.FamilyDataList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/UI/UserControls/MenuLink.ascx" TagName="UCMenuLink" TagPrefix="ucl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style type="text/css">
        tr
        {
            background-repeat: repeat-x;
            background-position: top right ;
            padding-right: 17px;
        }
    </style>
    <div class="clear">
    </div>
    <h3 class="title">
        <ucl:UCMenuLink ID="usrMenuLink" runat="server" />
    </h3>
    <div class="clear">
    </div>
    <telerik:RadGrid ID="rgFamilyData" CssClass="RemoveBorders" runat="server" Width="990px" 
        Height="430px" AllowPaging="true" AllowSorting="true" AutoGenerateColumns="false"
        OnNeedDataSource="rgFamilyData_NeedDataSource" PagerStyle-AlwaysVisible="true"
        BorderWidth="0" ClientSettings-Scrolling-AllowScroll="true" AllowFilteringByColumn="True"
        GridLines="None" OnItemCommand="rgFamilyData_ItemCommand" OnItemDataBound="rgFamilyData_ItemDataBound">
        <GroupingSettings CaseSensitive="false" />
        <%--<ItemStyle Wrap="true" />--%>
        <HeaderContextMenu EnableEmbeddedSkins="true" />
        <PagerStyle AlwaysVisible="True" ShowPagerText="true" Mode="NextPrevAndNumeric" />
        <MasterTableView CommandItemDisplay="TopAndBottom" TableLayout="Fixed" CommandItemSettings-AddNewRecordText="Add New Family Data"
            DataKeyNames="Id" Width="990px">
            <Columns>
                <telerik:GridEditCommandColumn HeaderStyle-Width="3%" HeaderText="Edit" ButtonType="ImageButton"
                    Reorderable="False" UniqueName="Edit" ItemStyle-BorderStyle="None">
                    <HeaderStyle Width="3%"></HeaderStyle>
                </telerik:GridEditCommandColumn>
                <telerik:GridTemplateColumn UniqueName="FamilyImage" HeaderText="Image" AllowFiltering="false">
                    <ItemTemplate>
                        <asp:Image ID="imgFamilyData" runat="server" Height="32" Width="32" />
                    </ItemTemplate>
                    <HeaderStyle Width="3%"></HeaderStyle>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn HeaderText="Name" UniqueName="FullName" DataField="FullName"
                    FilterControlWidth="50px">
                    <HeaderStyle Width="10%"></HeaderStyle>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Relation" UniqueName="RelationShipName" DataField="RelationShipName"
                    AllowFiltering="false">
                    <HeaderStyle Width="6%"></HeaderStyle>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="City" UniqueName="City" DataField="City" FilterControlWidth="50px">
                    <HeaderStyle Width="7%"></HeaderStyle>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="State" UniqueName="StateName" DataField="StateName"
                    FilterControlWidth="50px">
                    <HeaderStyle Width="7%"></HeaderStyle>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Phone" UniqueName="" DataField="MainPhone" AllowFiltering="false">
                    <HeaderStyle Width="6%"></HeaderStyle>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Email" UniqueName="Email" DataField="Email"
                    AllowFiltering="false">
                    <HeaderStyle Width="10%"></HeaderStyle>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="User Name" UniqueName="UserName" DataField="UserName"
                    FilterControlWidth="50px">
                    <HeaderStyle Width="7%"></HeaderStyle>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Code" UniqueName="Code" DataField="Code" AllowFiltering="false">
                    <HeaderStyle Width="4%"></HeaderStyle>
                </telerik:GridBoundColumn>
                <%--<telerik:GridTemplateColumn HeaderText="Active" UniqueName="Status" Reorderable="false">
                    <ItemTemplate>
                        <%# Eval("Active").ToString().ToLower()=="true"?"Active":"Inactive" %>
                        
                    </ItemTemplate>
                </telerik:GridTemplateColumn>--%>
                <telerik:GridCheckBoxColumn HeaderText="Active" UniqueName="Status" DataField="Active">
                    <HeaderStyle Width="5%"></HeaderStyle>
                </telerik:GridCheckBoxColumn>
                <telerik:GridTemplateColumn HeaderText="Child Data" Reorderable="false" AllowFiltering="false"
                    Visible="false">
                    <ItemTemplate>
                        <asp:HyperLink ID="hlChildData" runat="server"><img src="../images/BrowseIcon.gif" /></asp:HyperLink>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
            </Columns>
        </MasterTableView>
        <ClientSettings AllowColumnsReorder="True" Selecting-EnableDragToSelectRows="true"
            AllowDragToGroup="True" ReorderColumnsOnClient="True">
            <Resizing AllowColumnResize="true" />
            <Selecting AllowRowSelect="True" />
            <Scrolling AllowScroll="true" UseStaticHeaders="true" />
        </ClientSettings>
        <FilterMenu EnableEmbeddedSkins="true" ExpandAnimation-Type="Linear">
            <ExpandAnimation Type="Linear" />
        </FilterMenu>
    </telerik:RadGrid>
    <asp:HiddenField ID="hdnName" runat="server" />
    <telerik:RadAjaxManagerProxy ID="AjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="rgFamilyData">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgFamilyData" LoadingPanelID="RAPL1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <telerik:RadAjaxLoadingPanel ID="RAPL1" runat="server" Transparency="10">
    </telerik:RadAjaxLoadingPanel>
</asp:Content>
