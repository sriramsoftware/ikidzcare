<%@ Page Title="" Language="C#" MasterPageFile="~/UI/DayCareIndex.Master" AutoEventWireup="true"
    CodeBehind="HourOfOperation.aspx.cs" Inherits="DayCare.UI.HourOfOperation" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="clear"></div>
<h3 class="title">
        <img src="../images/arrow.png" />&nbsp;
        Hours of Opration</h3>
        <div class="clear"></div>
    <telerik:RadGrid ID="rgHourOfOperation" runat="server" AutoGenerateColumns="false" 
        AllowFilteringByColumn="false" AllowPaging="true" AllowSorting="true" EnableLinqExpression="false"
        EnableEmbeddedSkins="true" EnableAjaxSkinRendering="true" ReorderColumnOnClient="true"
        GridLines="None" CssClass="RemoveBorders" ShowGroupPanel="false" allowColumnReorder="True"
        Width="100%" Height="430px" BorderWidth="0px" OnNeedDataSource="rgHourOfOperation_NeedDataSource"
        OnEditCommand="rgHourOfOperation_EditCommand" OnInsertCommand="rgHourOfOperation_InsertCommand"
        OnItemCreated="rgHourOfOperation_ItemCreated" OnItemDataBound="rgHourOfOperation_ItemDataBound"
        OnUpdateCommand="rgHourOfOperation_UpdateCommand" OnItemCommand="rgHourOfOperation_ItemCommand">
        <ItemStyle Wrap="true" />
        <PagerStyle AlwaysVisible="true" Mode="NextPrevAndNumeric" ShowPagerText="true" />
        <MasterTableView DataKeyNames="Id" PagerStyle-AlwaysVisible="true" EditMode="InPlace"
            CommandItemDisplay="TopAndBottom" InsertItemDisplay="Top" TableLayout="Auto"
            AllowFilteringByColumn="true" Width="100%">
            <CommandItemSettings AddNewRecordText="Add New Hours of Operation" />
            <Columns>
                <telerik:GridEditCommandColumn ButtonType="ImageButton" HeaderStyle-Width="5%" Reorderable="true"
                    HeaderText="Edit" UpdateText="Edit" UniqueName="Edit">
                </telerik:GridEditCommandColumn>
                <telerik:GridTemplateColumn HeaderText="Day" AllowFiltering="true" UniqueName="Day">
                    <ItemTemplate>
                        <asp:Label ID="lblDay" runat="server" Text='<%# Eval("Day") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddlDayName" runat="server">
                            <asp:ListItem Text="--Select--" Value="-1"></asp:ListItem>
                            <asp:ListItem Text="Monday" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Tuesday" Value="2"></asp:ListItem>
                            <asp:ListItem Text="Wednesday" Value="3"></asp:ListItem>
                            <asp:ListItem Text="Thursday" Value="4"></asp:ListItem>
                            <asp:ListItem Text="Friday" Value="5"></asp:ListItem>
                            <asp:ListItem Text="Saturday" Value="6"></asp:ListItem>
                            <asp:ListItem Text="Sunday" Value="7"></asp:ListItem>
                        </asp:DropDownList>
                    </EditItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderText="Open Time" UniqueName="OpenTime" AllowFiltering="true">
                    <ItemTemplate>
                        <%# Eval("OpenTime")!=null?Convert.ToDateTime(Eval("OpenTime")).ToShortTimeString():"" %>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <telerik:RadTimePicker ID="rdOpenTp" runat="server" ShowPopupOnFocus="true" ToolTip="Open the time view popup.">
                            <TimeView Interval="0:30:0" Columns="4">
                            </TimeView>
                        </telerik:RadTimePicker>
                    </EditItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderText="Close Time" UniqueName="CloseTime" AllowFiltering="true">
                    <ItemTemplate>
                        <%# Eval("CloseTime")!=null?Convert.ToDateTime(Eval("CloseTime")).ToShortTimeString():"" %>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <telerik:RadTimePicker ID="rdCloseTp" runat="server" ShowPopupOnFocus="true" ToolTip="Open the time view popup.">
                            <TimeView Interval="0:30:0" Columns="4">
                            </TimeView>
                        </telerik:RadTimePicker>
                    </EditItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="Comments" HeaderText="Comment" AllowFiltering="false">
                    <ItemTemplate>
                        <asp:Label ID="lblComment" runat="server" Text='<%# Eval("Comments") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtComments" runat="server" TextMode="MultiLine" Text='<%# Eval("Comments") %>'></asp:TextBox>
                    </EditItemTemplate>
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
            <telerik:AjaxSetting AjaxControlID="rgHourOfOperation">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgHourOfOperation" LoadingPanelID="RAPL1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <telerik:RadAjaxLoadingPanel ID="RAPL1" runat="server" Transparency="10">
    </telerik:RadAjaxLoadingPanel>
</asp:Content>
