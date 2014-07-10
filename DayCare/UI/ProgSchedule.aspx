<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="ProgSchedule.aspx.cs"
    Inherits="DayCare.UI.ProgSchedule" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <link href="../css/DayCare.css" rel="stylesheet" type="text/css" />
    <script src="../JavaScript/common.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server" AsyncPostBackErrorMessage="We cannot serve your request right now. Try again later."
        ScriptMode="Release" AsyncPostBackTimeout="1800" EnablePageMethods="True">
    </telerik:RadScriptManager>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" EnableAJAX="true" EnableEmbeddedScripts="true">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="rgProgSchedule">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgProgSchedule" LoadingPanelID="RAPL1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <div class="clear">
    </div>
    <div class="clear">
    </div>
    <telerik:RadGrid ID="rgProgSchedule" runat="server" AutoGenerateColumns="false" AllowFilteringByColumn="false"
        AllowPaging="true" AllowSorting="true" EnableLinqExpression="false" EnableEmbeddedSkins="true"
        EnableAjaxSkinRendering="true" ReorderColumnOnClient="true" GridLines="None"
        CssClass="RemoveBorders" ShowGroupPanel="false" allowColumnReorder="True" Width="100%"
        Height="600px" BorderWidth="0px" OnNeedDataSource="rgProgSchedule_NeedDataSource"
        OnEditCommand="rgProgSchedule_EditCommand" OnInsertCommand="rgProgSchedule_InsertCommand"
        OnItemCreated="rgProgSchedule_ItemCreated" OnItemDataBound="rgProgSchedule_ItemDataBound"
        OnUpdateCommand="rgProgSchedule_UpdateCommand" OnItemCommand="rgProgSchedule_ItemCommand">
        <ItemStyle Wrap="true" />
        <PagerStyle AlwaysVisible="true" Mode="NextPrevAndNumeric" ShowPagerText="true" />
        <MasterTableView DataKeyNames="Id" PagerStyle-AlwaysVisible="true" EditMode="InPlace"
            CommandItemDisplay="TopAndBottom" InsertItemDisplay="Top" TableLayout="Auto"
            AllowFilteringByColumn="true" Width="100%">
            <CommandItemSettings AddNewRecordText="Add New Prog. Schedule" />
            <Columns>
                <telerik:GridEditCommandColumn ButtonType="ImageButton" HeaderStyle-Width="5%" Reorderable="true"
                    HeaderText="Edit" UpdateText="Edit" UniqueName="Edit">
                </telerik:GridEditCommandColumn>
                <telerik:GridTemplateColumn HeaderText="SchoolProgram" AllowFiltering="true" Visible="false"
                    UniqueName="SchoolProgram">
                    <ItemTemplate>
                        <asp:Label ID="lblSchoolProgram" runat="server" Text='<%# Eval("SchoolProgramTitle") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:Label ID="lblSchoolProgram" runat="server"></asp:Label>
                    </EditItemTemplate>
                </telerik:GridTemplateColumn>
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
                <telerik:GridTemplateColumn HeaderText="Begin Time" UniqueName="BeginTime" AllowFiltering="true">
                    <ItemTemplate>
                        <%# Eval("BeginTime") != null ? Convert.ToDateTime(Eval("BeginTime")).ToShortTimeString() : ""%>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <telerik:RadTimePicker ID="rdBiginTp" runat="server" ShowPopupOnFocus="true" ToolTip="Open the time view popup.">
                            <TimeView Interval="0:30:0" Columns="4">
                            </TimeView>
                        </telerik:RadTimePicker>
                    </EditItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderText="End Time" UniqueName="CloseTime" AllowFiltering="true">
                    <ItemTemplate>
                        <%# Eval("EndTime") != null ? Convert.ToDateTime(Eval("EndTime")).ToShortTimeString() : ""%>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <telerik:RadTimePicker ID="rdCloseTp" runat="server" ShowPopupOnFocus="true" ToolTip="Open the time view popup.">
                            <TimeView Interval="0:30:0" Columns="4">
                            </TimeView>
                        </telerik:RadTimePicker>
                    </EditItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridCheckBoxColumn DataField="Active" HeaderText="Active" SortExpression="Active"
                    UniqueName="Active">
                </telerik:GridCheckBoxColumn>
                <telerik:GridTemplateColumn HeaderText="Class Room" UniqueName="ProgClassRoom">
                    <ItemTemplate>
                        <asp:Label ID="lblClassRoom" runat="server" Text='<%# Eval("ClassRoomName")%>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddlProgClassRoom" runat="server">
                        </asp:DropDownList>
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
            <telerik:AjaxSetting AjaxControlID="rgProgSchedule">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgProgSchedule" LoadingPanelID="RAPL1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxLoadingPanel ID="RAPL1" runat="server" Transparency="10">
    </telerik:RadAjaxLoadingPanel>
    </form>
</body>
</html>
