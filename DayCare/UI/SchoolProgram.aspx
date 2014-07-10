<%@ Page Title="" Language="C#" MasterPageFile="~/UI/DayCareIndex.Master" AutoEventWireup="true"
    CodeBehind="SchoolProgram.aspx.cs" Inherits="DayCare.UI.SchoolProgram" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript" language="javascript">
        function ChkFees(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode == 46 || charCode == 8 || (charCode >= 48 && charCode <= 57)) {

                return true;
            }
            else {
                return false;
            }
        }

        function ShowProgramClassRoom(Id, SchoolYearId, IsPrimary, Title) {
            var oWnd = radopen('ProgramClassRoom.aspx?SchoolProgramId=' + Id + '&SchoolYearId=' + SchoolYearId + '&IsPrimary=' + IsPrimary);
            setTimeout(function() { oWnd.setActive(true); }, 100);
            oWnd.set_title(Title + ' Program Class Room');
            oWnd.center();
        }
        function ShowProgramClassCategory(Id, SchoolYearId, IsPrimary, Title) {
            var oWnd = radopen('ProgClassCategory.aspx?SchoolProgramId=' + Id + '&SchoolYearId=' + SchoolYearId + '&IsPrimary=' + IsPrimary);
            setTimeout(function() { oWnd.setActive(true); }, 100);
            oWnd.set_title(Title + ' Program Class Category');
            oWnd.center();
        }
        function ShowProgSchedule(Id, SchoolYearId, IsPrimary, Title) {
            var oWnd = radopen('ProgSchedule.aspx?SchoolProgramId=' + Id + '&SchoolYearId=' + SchoolYearId);
            setTimeout(function() { oWnd.setActive(true); }, 100);
            oWnd.set_title(Title + ' Program Schedule');
            oWnd.center();
        }
        function ShowProgramStaff(Id, SchoolYearId, IsPrimary, Title) {
            var oWnd = radopen('ProgramStaff.aspx?SchoolProgramId=' + Id + '&SchoolYearId=' + SchoolYearId + '&IsPrimary=' + IsPrimary);
            setTimeout(function() { oWnd.setActive(true); }, 100);
            oWnd.set_title(Title + ' Program Staff');
            oWnd.center();
        }
    </script>

    <div class="clear">
    </div>
    <h3 class="title">
        <img src="../images/arrow.png" />&nbsp; School Program</h3>
    <div class="clear">
    </div>
    <telerik:RadGrid ID="rgSchoolProgram" runat="server" AutoGenerateColumns="false"
        AllowFilteringByColumn="false" AllowPaging="true" AllowSorting="true" EnableLinqExpression="false"
        EnableEmbeddedSkins="true" EnableAjaxSkinRendering="true" ReorderColumnOnClient="true"
        GridLines="None" CssClass="RemoveBorders" ShowGroupPanel="false" allowColumnReorder="True"
        PageSize="20" Width="100%" Height="600px" BorderWidth="0px" OnNeedDataSource="rgSchoolProgram_NeedDataSource"
        OnEditCommand="rgSchoolProgram_EditCommand" OnInsertCommand="rgSchoolProgram_InsertCommand"
        OnItemCreated="rgSchoolProgram_ItemCreated" OnItemDataBound="rgSchoolProgram_ItemDataBound"
        OnUpdateCommand="rgSchoolProgram_UpdateCommand" OnItemCommand="rgSchoolProgram_ItemCommand">
        <ItemStyle Wrap="true" />
        <PagerStyle AlwaysVisible="true" Mode="NextPrevAndNumeric" ShowPagerText="true" />
        <MasterTableView DataKeyNames="Id" PagerStyle-AlwaysVisible="true" EditMode="InPlace"
            CommandItemDisplay="TopAndBottom" InsertItemDisplay="Top" TableLayout="Auto"
            Width="100%" AllowFilteringByColumn="true">
            <CommandItemSettings AddNewRecordText="Add New School Program" />
            <Columns>
                <telerik:GridEditCommandColumn ButtonType="ImageButton" HeaderStyle-Width="5%" Reorderable="true"
                    HeaderText="Edit" UpdateText="Edit" UniqueName="Edit">
                </telerik:GridEditCommandColumn>
                <telerik:GridTemplateColumn HeaderText="Title" AllowFiltering="true" UniqueName="Title"
                    DataField="Title" HeaderStyle-Width="15%">
                    <ItemTemplate>
                        <asp:Label ID="lblTitle" runat="server" Text='<%# Eval("Title") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtTitle" runat="server" Text='<%# Eval("Title") %>'></asp:TextBox>
                    </EditItemTemplate>
                </telerik:GridTemplateColumn>
                <%-- <telerik:GridTemplateColumn HeaderText="Fees" AllowFiltering="false" UniqueName="Fees" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblFees" runat="server" Text='<%# Eval("Fees") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtFees" runat="server" Text='<%# Eval("Fees") %>' Width="80px"
                            onkeypress="javascript:return ChkFees(event);"></asp:TextBox>
                    </EditItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderText="Fees Period" UniqueName="FeesPeriodName"
                    AllowFiltering="false" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblFeesPeriodName" runat="server" Text='<%# Eval("FeesPeriodName") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddlFeesPeriodName" runat="server">
                        </asp:DropDownList>
                    </EditItemTemplate>
                </telerik:GridTemplateColumn>--%>
                <telerik:GridTemplateColumn HeaderText="" UniqueName="Comments" Visible="false" AllowFiltering="false">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtComments" runat="server" TextMode="MultiLine" Text='<%# Eval("Comments") %>'></asp:TextBox>
                    </EditItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridCheckBoxColumn DataField="Active" HeaderText="Active" SortExpression="Active"
                    UniqueName="Active">
                </telerik:GridCheckBoxColumn>
                <telerik:GridCheckBoxColumn DataField="IsPrimary" HeaderText="Primary" AllowFiltering="true"
                    UniqueName="IsPrimary">
                </telerik:GridCheckBoxColumn>
                <telerik:GridTemplateColumn UniqueName="ProgClassCategory" HeaderText="Prog. Class Category"
                    Visible="false" AllowFiltering="false">
                    <ItemTemplate>
                        <asp:HyperLink ID="hlProgClassCategory" runat="server"><img src="../images/BrowseIcon.gif" height="16" width="16" /></asp:HyperLink>
                        <%--<asp:ImageButton ID="imgBtnProgClassCategory" CommandName="ProgClassCategory" runat="server" />--%>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="ProgStaff" HeaderText="Prog. Staff" AllowFiltering="false"
                    Visible="false">
                    <ItemTemplate>
                        <asp:HyperLink ID="hlProgStaff" runat="server" NavigateUrl=""><img src="../images/BrowseIcon.gif" height="16" width="16" /></asp:HyperLink>
                        <%-- <asp:ImageButton ID="imgBtnProgStaff" CommandName="ProgStaff" runat="server" />--%>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="ProgClassRoom" HeaderText="Prog. Class Room"
                    AllowFiltering="false">
                    <ItemTemplate>
                        <asp:HyperLink ID="hlProgClassRoom" runat="server" NavigateUrl=""><img src="../images/BrowseIcon.gif" height="16" width="16" /></asp:HyperLink>
                        <%--<asp:ImageButton
                            ID="imgBtnProgClassRoom" CommandName="ProgClassRoom" runat="server" />--%>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="ProgSchedule" HeaderText="Prog. Schedule"
                    Visible="false" AllowFiltering="false">
                    <ItemTemplate>
                        <asp:HyperLink ID="hlProgSchedule" runat="server" NavigateUrl=""><img src="../images/BrowseIcon.gif" height="16" width="16" /></asp:HyperLink>
                        <%--  <asp:ImageButton ID="imgBtnProgSchedule" CommandName="ProgSchedule" runat="server" />--%>
                    </ItemTemplate>
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
    <telerik:RadWindowManager ID="rwm1" runat="server" DestroyOnClose="false" InitialBehaviors="Resize"
        Modal="true" KeepInScreenBounds="true" ReloadOnShow="true" Title="Shortcuts"
        AutoSize="false" Behaviors="Close" Width="1000px" Height="370px" Skin="Office2007"
        Overlay="true" ShowContentDuringLoad="false" Animation="Fade" VisibleStatusbar="false">
    </telerik:RadWindowManager>
    <asp:Label ID="lblName" runat="server" Visible="false" />
    <telerik:RadAjaxManagerProxy ID="AjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="rgSchoolProgram">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgSchoolProgram" LoadingPanelID="RAPL1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <telerik:RadAjaxLoadingPanel ID="RAPL1" runat="server" Transparency="10">
    </telerik:RadAjaxLoadingPanel>
</asp:Content>
