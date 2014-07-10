<%@ Page Title="" Language="C#" MasterPageFile="~/UI/DayCareIndex.Master" AutoEventWireup="true"
    CodeBehind="SchoolYear.aspx.cs" Inherits="DayCare.UI.SchoolYear" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="clear">
    </div>
    <h3 class="title">
        <img src="../images/arrow.png" />&nbsp; School Year</h3>
    <div class="clear">
    </div>
    <telerik:RadGrid ID="rgSchoolYear" CssClass="RemoveBorders" runat="server" Width="100%"
        Height="430px" AllowPaging="true" AllowSorting="true" AutoGenerateColumns="false"
        BorderWidth="0px" OnNeedDataSource="rgSchoolYear_NeedDataSource" PagerStyle-AlwaysVisible="true"
        AllowFilteringByColumn="True" GridLines="None" OnDeleteCommand="rgSchoolYear_DeleteCommand"
        OnEditCommand="rgSchoolYear_EditCommand" OnInsertCommand="rgSchoolYear_InsertCommand"
        OnItemCommand="rgSchoolYear_ItemCommand" OnItemCreated="rgSchoolYear_ItemCreated"
        OnItemDataBound="rgSchoolYear_ItemDataBound" OnUpdateCommand="rgSchoolYear_UpdateCommand">
        <GroupingSettings CaseSensitive="false" />
        <HeaderContextMenu EnableEmbeddedSkins="true" />
        <PagerStyle AlwaysVisible="True" ShowPagerText="true" Mode="NextPrevAndNumeric"  />
        <MasterTableView CommandItemDisplay="TopAndBottom" CommandItemSettings-AddNewRecordText="Add New School Year"
            com HorizontalAlign="NotSet" EditMode="InPlace" DataKeyNames="Id" Width="100%">
            <Columns>
                <telerik:GridEditCommandColumn HeaderText="" ButtonType="ImageButton" Reorderable="False"
                    UniqueName="Edit" ItemStyle-BorderStyle="None">
                    <HeaderStyle Width="5%"></HeaderStyle>
                    <ItemStyle BorderStyle="None"></ItemStyle>
                </telerik:GridEditCommandColumn>
                <%--<telerik:GridTemplateColumn HeaderText="School Name" UniqueName="SchoolName">
                    <ItemTemplate>
                        <asp:Label ID="lblSchoolName" runat="server" Text='<%# Eval("SchoolName") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                       <asp:Label ID="lblSName" runat="server" Text='<%# Eval("SchoolName") %>'></asp:Label>
                    </EditItemTemplate>
                </telerik:GridTemplateColumn>--%>
                <telerik:GridTemplateColumn HeaderText="Year" UniqueName="Year">
                    <ItemTemplate>
                        <asp:Label ID="lblYear" runat="server" Text='<%# Eval("Year") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddlYear" runat="server">
                        </asp:DropDownList>
                    </EditItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderText="Start Date" UniqueName="StartDate">
                    <ItemTemplate>
                        <asp:Label ID="lblStartDate" runat="server"></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <telerik:RadDatePicker ID="rdpStartDate" runat="server" Width="100px"  >
                            <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x" >
                            </Calendar>
                            <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                            <DateInput DisplayDateFormat="M/d/yyyy" DateFormat="M/d/yyyy"  >
                            </DateInput>
                        </telerik:RadDatePicker>
                        <%-- <asp:CompareValidator ID="cmp" ControlToValidate="rdpStartDate" runat="server"></asp:CompareValidator>--%>
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
                <%--<telerik:GridTemplateColumn HeaderText="CurrentId" UniqueName="CurrentId" AllowFiltering="true">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkItemCurrentYear" Enabled="false" runat="server" Checked='<%# Eval("CurrentId") %>' />
                    </ItemTemplate>
                    <EditItemTemplate>
                       <asp:CheckBox ID="chkItemCurrentYear" Enabled="false" runat="server" Checked='<%# Eval("CurrentId") %>' />
                    </EditItemTemplate>
                </telerik:GridTemplateColumn>--%>
                <telerik:GridCheckBoxColumn UniqueName="CurrentId" DataField="CurrentId" HeaderText="Current Year (&lt;b&gt;?&lt;/b&gt;)"
                    HeaderTooltip="checked - consider as a current running year.">
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
            <telerik:AjaxSetting AjaxControlID="rgSchoolYear">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgSchoolYear" LoadingPanelID="RAPL1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <telerik:RadAjaxLoadingPanel ID="RAPL1" runat="server"  Transparency="10">
    </telerik:RadAjaxLoadingPanel>
</asp:Content>
