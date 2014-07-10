<%@ Page Title="" Language="C#" MasterPageFile="~/UI/DayCareIndex.Master" AutoEventWireup="true"
    CodeBehind="SchoolProgramFeesDetail.aspx.cs" Inherits="DayCare.UI.SchoolProgramFeesDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript" language="javascript">


        function ShowProgramClassRoom(Id, SchoolYearId, IsPrimary, Title) {
            var oWnd = radopen('ProgramClassRoom.aspx?SchoolProgramId=' + Id + '&SchoolYearId=' + SchoolYearId + '&IsPrimary=' + IsPrimary);
            setTimeout(function() { oWnd.setActive(true); }, 100);
            oWnd.set_title(Title + ' Program Class Room');
            oWnd.center();
        }
        
    </script>

    <h3 class="title">
        <img src="../images/arrow.png" />&nbsp; School Program</h3>
    <telerik:RadGrid ID="rgSchoolProgramFeesDetail" runat="server" AutoGenerateColumns="false"
        AllowFilteringByColumn="false" AllowPaging="true" AllowSorting="true" EnableLinqExpression="false"
        EnableEmbeddedSkins="true" EnableAjaxSkinRendering="true" ReorderColumnOnClient="true"
        GridLines="None" CssClass="RemoveBorders" ShowGroupPanel="false" allowColumnReorder="True"
        PageSize="50" Width="100%" Height="1200px" BorderWidth="0px" OnNeedDataSource="rgSchoolProgramFeesDetail_NeedDataSource"
        OnEditCommand="rgSchoolProgramFeesDetail_EditCommand" OnItemCommand="rgSchoolProgramFeesDetail_ItemCommand"
        OnItemDataBound="rgSchoolProgramFeesDetail_ItemDataBound" OnDetailTableDataBind="rgSchoolProgramFeesDetail_DetailTableDataBind"
        OnPreRender="rgSchoolProgramFeesDetail_PreRender" OnInsertCommand="rgSchoolProgramFeesDetail_InsertCommand"
        OnUpdateCommand="rgSchoolProgramFeesDetail_UpdateCommand" OnItemCreated="rgSchoolProgramFeesDetail_ItemCreated"
        OnDeleteCommand="rgSchoolProgramFeesDetail_DeleteCommand">
        <GroupingSettings CaseSensitive="false" />
        <ItemStyle Wrap="true" />
        <HeaderContextMenu EnableEmbeddedSkins="true" />
        <PagerStyle AlwaysVisible="True" ShowPagerText="true" Mode="NextPrevAndNumeric" Position="Top" />
        <MasterTableView CommandItemDisplay="Top" TableLayout="Auto" DataKeyNames="Id" EditFormSettings-EditColumn-Display="false"
            EditMode="InPlace" Width="100%" HierarchyLoadMode="Client" AllowAutomaticUpdates="false"
            EditFormSettings-EditColumn-ButtonType="ImageButton" Name="SchoolProgram">
            <CommandItemSettings AddNewRecordText="Add New School Program" />
            <DetailTables>
                <telerik:GridTableView EditMode="InPlace" NoDetailRecordsText="No Fees" NoMasterRecordsText="FeesPeriod Not Added"
                    DataKeyNames="Id" HorizontalAlign="NotSet" Width="100%" AllowPaging="false" DataMember="Fees"
                    Name="rgFees" PagerStyle-AlwaysVisible="true" EditFormSettings-EditColumn-ButtonType="ImageButton"
                    EditFormSettings-EditColumn-FooterStyle-HorizontalAlign="Right" CommandItemDisplay="Top">
                    <ParentTableRelation>
                        <telerik:GridRelationFields DetailKeyField="SchoolProgramId" MasterKeyField="Id" />
                    </ParentTableRelation>
                    <CommandItemSettings AddNewRecordText="Add New Fees Detail" />
                    <Columns>
                        <telerik:GridEditCommandColumn HeaderText="Edit" HeaderStyle-Width="5%" ButtonType="ImageButton"
                            Reorderable="False" ItemStyle-BorderStyle="None" UniqueName="Edit">
                        </telerik:GridEditCommandColumn>
                        <telerik:GridButtonColumn CommandName="Delete" HeaderText="Delete" UniqueName="DeleteColumn"
                            ButtonType="ImageButton" Reorderable="false" ItemStyle-BorderStyle="None" ConfirmText="Do you want to delete Fees?"
                            ConfirmTitle="Delete Record" ConfirmDialogType="RadWindow" ConfirmDialogHeight="30%">
                            <HeaderStyle Width="5%"></HeaderStyle>
                        </telerik:GridButtonColumn>
                        <telerik:GridTemplateColumn HeaderText="Fees" UniqueName="Fees" AllowFiltering="false">
                            <ItemTemplate>
                                <asp:Label ID="lblFees" runat="server" Text='<%# Eval("Fees") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtFees" runat="server" Text='<%# Eval("Fees") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="Fees Period" UniqueName="FeesPeriod" AllowFiltering="false">
                            <ItemTemplate>
                                <asp:Label ID="lblFeesPeriod" runat="server" Text='<%# Eval("FeesPeriod") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList ID="ddlFeesPeriod" runat="server" OnSelectedIndexChanged="ddlFeesPeriod_SelectedIndexChanged"
                                    AutoPostBack="true">
                                </asp:DropDownList>
                            </EditItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="Effective Day of Week" UniqueName="WeekDay"
                            AllowFiltering="false">
                            <ItemTemplate>
                                <asp:Label ID="lblWeekDay" runat="server" Text=""></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList ID="ddlWeekDay" runat="server">
                                    <asp:ListItem Text="--Select--" Value="-1"></asp:ListItem>
                                    <asp:ListItem Text="Monday" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Tuesday" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="Wednesday" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="Thursday" Value="4"></asp:ListItem>
                                    <asp:ListItem Text="Friday" Value="5"></asp:ListItem>
                                </asp:DropDownList>
                            </EditItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="Effective Date of Month" UniqueName="Day"
                            AllowFiltering="false">
                            <ItemTemplate>
                                <asp:Label ID="lblMonthday" runat="server"></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList ID="ddlMonthDay" runat="server">
                                    <asp:ListItem Text="--Select--" Value="-1"></asp:ListItem>
                                    <asp:ListItem Text="1" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="4" Value="4"></asp:ListItem>
                                    <asp:ListItem Text="5" Value="5"></asp:ListItem>
                                    <asp:ListItem Text="6" Value="6"></asp:ListItem>
                                    <asp:ListItem Text="7" Value="7"></asp:ListItem>
                                    <asp:ListItem Text="8" Value="8"></asp:ListItem>
                                    <asp:ListItem Text="9" Value="9"></asp:ListItem>
                                    <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                    <asp:ListItem Text="11" Value="11"></asp:ListItem>
                                    <asp:ListItem Text="12" Value="12"></asp:ListItem>
                                    <asp:ListItem Text="13" Value="13"></asp:ListItem>
                                    <asp:ListItem Text="14" Value="14"></asp:ListItem>
                                    <asp:ListItem Text="15" Value="15"></asp:ListItem>
                                    <asp:ListItem Text="16" Value="16"></asp:ListItem>
                                    <asp:ListItem Text="17" Value="17"></asp:ListItem>
                                    <asp:ListItem Text="18" Value="18"></asp:ListItem>
                                    <asp:ListItem Text="19" Value="19"></asp:ListItem>
                                    <asp:ListItem Text="20" Value="20"></asp:ListItem>
                                    <asp:ListItem Text="21" Value="21"></asp:ListItem>
                                    <asp:ListItem Text="22" Value="22"></asp:ListItem>
                                    <asp:ListItem Text="23" Value="23"></asp:ListItem>
                                    <asp:ListItem Text="24" Value="24"></asp:ListItem>
                                    <asp:ListItem Text="25" Value="25"></asp:ListItem>
                                    <asp:ListItem Text="26" Value="26"></asp:ListItem>
                                    <asp:ListItem Text="27" Value="27"></asp:ListItem>
                                    <asp:ListItem Text="28" Value="28"></asp:ListItem>
                                    <asp:ListItem Text="29" Value="29"></asp:ListItem>
                                    <asp:ListItem Text="30" Value="30"></asp:ListItem>
                                    <asp:ListItem Text="31" Value="31"></asp:ListItem>
                                </asp:DropDownList>
                            </EditItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="Effective Date of Year" UniqueName="YearDate"
                            AllowFiltering="false">
                            <ItemTemplate>
                                <asp:Label ID="lblYearDate" runat="server"></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <telerik:RadDatePicker ID="rdpYearDate" runat="server">
                                </telerik:RadDatePicker>
                            </EditItemTemplate>
                        </telerik:GridTemplateColumn>
                    </Columns>
                </telerik:GridTableView>
            </DetailTables>
            <Columns>
                <telerik:GridEditCommandColumn HeaderText="Edit" HeaderStyle-Width="5%" ButtonType="ImageButton"
                    Reorderable="False" ItemStyle-BorderStyle="None" UniqueName="Edit">
                </telerik:GridEditCommandColumn>
                <telerik:GridButtonColumn CommandName="Delete" HeaderText="Delete" UniqueName="DeleteProgram"
                    ButtonType="ImageButton" Reorderable="false" ItemStyle-BorderStyle="None" ConfirmText="Do you want to delete a Program?"
                    ConfirmTitle="Delete Record" ConfirmDialogType="RadWindow" ConfirmDialogHeight="30%">
                    <HeaderStyle Width="5%"></HeaderStyle>
                </telerik:GridButtonColumn>
                <telerik:GridTemplateColumn HeaderText="Program" UniqueName="Program" AllowFiltering="false">
                    <ItemTemplate>
                        <asp:Label ID="lblSchoolProgram" runat="server" Text='<%# Eval("Title") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtSchoolProgram" runat="server" Text='<%# Eval("Title") %>'></asp:TextBox>
                    </EditItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="Program1" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblschoolProgramId" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:Label ID="lblschoolProgramId" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                    </EditItemTemplate>
                </telerik:GridTemplateColumn>
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
                <telerik:GridTemplateColumn UniqueName="ProgClassRoom" HeaderText="Prog. Class Room"
                    AllowFiltering="false">
                    <ItemTemplate>
                        <asp:HyperLink ID="hlProgClassRoom" runat="server" NavigateUrl=""><img src="../images/BrowseIcon.gif" height="16" width="16" /></asp:HyperLink>
                        <%--<asp:ImageButton
                            ID="imgBtnProgClassRoom" CommandName="ProgClassRoom" runat="server" />--%>
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
    <telerik:RadAjaxManagerProxy ID="AjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="rgSchoolProgramFeesDetail">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgSchoolProgramFeesDetail" LoadingPanelID="RAPL1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <telerik:RadAjaxLoadingPanel ID="RAPL1" runat="server" Transparency="10">
    </telerik:RadAjaxLoadingPanel>
    <asp:HiddenField ID="hdnName" runat="server" />
</asp:Content>
