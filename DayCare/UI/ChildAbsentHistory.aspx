<%@ Page Title="" Language="C#" MasterPageFile="~/UI/DayCareIndex.Master" AutoEventWireup="true"
    CodeBehind="ChildAbsentHistory.aspx.cs" Inherits="DayCare.UI.ChildAbsentHistory" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/UI/UserControls/MenuLink.ascx" TagName="UCMenuLink" TagPrefix="ucl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="clear">
    </div>
    <h3 class="title">
        <ucl:UCMenuLink ID="usrMenuLink" runat="server" />
        <asp:Label ID="lblChild" Visible="false" runat="server"></asp:Label>
    </h3>
    <div class="clear">
    </div>
    <telerik:RadGrid ID="rgChildAbsentHistory" CssClass="RemoveBorders" runat="server"
        Width="100%" Height="430px" AllowPaging="true" AllowSorting="true" AutoGenerateColumns="false"
        BorderWidth="0px" OnNeedDataSource="rgChildAbsentHistory_NeedDataSource" PagerStyle-AlwaysVisible="true"
        AllowFilteringByColumn="True" GridLines="None" OnDeleteCommand="rgChildAbsentHistory_DeleteCommand"
        OnEditCommand="rgChildAbsentHistory_EditCommand" OnInsertCommand="rgChildAbsentHistory_InsertCommand"
        OnItemCommand="rgChildAbsentHistory_ItemCommand" OnItemCreated="rgChildAbsentHistory_ItemCreated"
        OnItemDataBound="rgChildAbsentHistory_ItemDataBound" OnUpdateCommand="rgChildAbsentHistory_UpdateCommand">
        <GroupingSettings CaseSensitive="false" />
        <HeaderContextMenu EnableEmbeddedSkins="true" />
        <PagerStyle AlwaysVisible="True" ShowPagerText="true" Mode="NextPrevAndNumeric" />
        <MasterTableView CommandItemDisplay="TopAndBottom" CommandItemSettings-AddNewRecordText="Add New Child Absent"
            HorizontalAlign="NotSet" EditMode="InPlace" DataKeyNames="Id" Width="100%">
            <Columns>
                <telerik:GridEditCommandColumn HeaderStyle-Width="5%" HeaderText="" ButtonType="ImageButton"
                    Reorderable="False" UniqueName="Edit" ItemStyle-BorderStyle="None">
                    <HeaderStyle Width="2%"></HeaderStyle>
                    <ItemStyle BorderStyle="None"></ItemStyle>
                </telerik:GridEditCommandColumn>
                <telerik:GridTemplateColumn HeaderText="AbsentReason" UniqueName="AbsentReason">
                    <ItemTemplate>
                        <asp:Label ID="lblAbsentReson" runat="server" Text='<%# Eval("AbsentReason") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddlAbsentReason" runat="server">
                        </asp:DropDownList>
                    </EditItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderText="Start Date" UniqueName="StartDate">
                    <ItemTemplate>
                        <asp:Label ID="lblStartDate" runat="server"></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <telerik:RadDatePicker ID="rdpStartDate" runat="server" Width="100px">
                            <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x">
                            </Calendar>
                            <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                            <DateInput DisplayDateFormat="M/d/yyyy" DateFormat="M/d/yyyy">
                            </DateInput>
                        </telerik:RadDatePicker>
                    </EditItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderText="End Date" UniqueName="EndDate">
                    <ItemTemplate>
                        <asp:Label ID="lblEndDate" runat="server"></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <telerik:RadDatePicker ID="rdpEndDate" runat="server" Width="100px">
                            <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x">
                            </Calendar>
                            <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                            <DateInput DisplayDateFormat="M/d/yyyy" DateFormat="M/d/yyyy">
                            </DateInput>
                        </telerik:RadDatePicker>
                    </EditItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderText="Comment" UniqueName="Comment" AllowFiltering="false">
                    <ItemTemplate>
                        <asp:Label ID="lblComment" runat="server" Text='<%# Eval("Comments") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtComment" runat="server" TextMode="MultiLine" Text='<%# Eval("Comments") %>'></asp:TextBox>
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
    <telerik:RadAjaxManagerProxy ID="AjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="rgChildAbsentHistory">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgChildAbsentHistory" LoadingPanelID="RAPL1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <telerik:RadAjaxLoadingPanel ID="RAPL1" runat="server" Transparency="10">
    </telerik:RadAjaxLoadingPanel>
</asp:Content>
