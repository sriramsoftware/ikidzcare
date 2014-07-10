<%@ Page Title="" Language="C#" MasterPageFile="~/UI/DayCareIndex.Master" AutoEventWireup="true"
    CodeBehind="ChildProgEnrollment.aspx.cs" Inherits="DayCare.UI.ChildProgEnrollment" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/UI/UserControls/MenuLink.ascx" TagName="UCMenuLink" TagPrefix="ucl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script language="javascript" type="text/javascript">
        function ValidationOnformSubmit() {
            var ErrorMsg = "";
            if (document.getElementById("<%=ddlProg.ClientID %>").selectedIndex > 0) {
                if (document.getElementById("<%=ChkMon.ClientID %>").checked == true) {
                    if (document.getElementById("<%=ddlDayType1.ClientID %>").selectedIndex == 0) {
                        ErrorMsg += "Please select time of Monday\n";
                    }
                    if (document.getElementById("<%=ddlChildProgClassRoom.ClientID %>").selectedIndex == 0) {
                        ErrorMsg += "Please select class-room of Monday\n";
                    }
                }
                if (document.getElementById("<%=ChkTue.ClientID %>").checked == true) {
                    if (document.getElementById("<%=ddlDayType2.ClientID %>").selectedIndex == 0) {
                        ErrorMsg += "Please select time of Tuesday\n";
                    }
                    if (document.getElementById("<%=ddlChildProgClassRoom1.ClientID %>").selectedIndex == 0) {
                        ErrorMsg += "Please select class-room of Tuesday\n";
                    }
                }
                if (document.getElementById("<%=ChkWen.ClientID %>").checked == true) {
                    if (document.getElementById("<%=ddlDayType3.ClientID %>").selectedIndex == 0) {
                        ErrorMsg += "Please select time of Wednesday\n";
                    }
                    if (document.getElementById("<%=ddlChildProgClassRoom2.ClientID %>").selectedIndex == 0) {
                        ErrorMsg += "Please select class-room of Wednesday\n";
                    }
                }
                if (document.getElementById("<%=ChkThus.ClientID %>").checked == true) {
                    if (document.getElementById("<%=ddlDayType4.ClientID %>").selectedIndex == 0) {
                        ErrorMsg += "Please select time of Thursday\n";
                    }
                    if (document.getElementById("<%=ddlChildProgClassRoom3.ClientID %>").selectedIndex == 0) {
                        ErrorMsg += "Please select class-room of Thursday\n";
                    }
                }
                if (document.getElementById("<%=ChkFri.ClientID %>").checked == true) {
                    if (document.getElementById("<%=ddlDayType5.ClientID %>").selectedIndex == 0) {
                        ErrorMsg += "Please select time of Friday\n";
                    }
                    if (document.getElementById("<%=ddlChildProgClassRoom4.ClientID %>").selectedIndex == 0) {
                        ErrorMsg += "Please select class-room of Friday\n";
                    }
                }
            }
            else {
                ErrorMsg = "Please select program";
            }

            if (ErrorMsg != "") {
                alert(ErrorMsg);
                return false;
            }
        }
    </script>

    <h3 class="title">
        <ucl:UCMenuLink ID="usrMenuLink" runat="server" />
    </h3>
    <div class="clear">
    </div>
    <div class="innerMain">
        <div class="content">
            <fieldset>
                <legend><strong>User Details</strong></legend>
                <div class="fieldDiv">
                    <div class="box pndR15">
                        Program:&nbsp;<asp:DropDownList ID="ddlProg" runat="server" OnSelectedIndexChanged="ddlProg_SelectedIndexChanged"
                            AutoPostBack="true">
                        </asp:DropDownList>
                    </div>
                    <div class="box pndR15">
                        Fee:&nbsp;<asp:TextBox ID="txtFee" runat="server" Width="100px" ReadOnly="true"></asp:TextBox>
                    </div>
                    <div class=" clear">
                    </div>
                    <div class="box pndR15">
                        <asp:CheckBox ID="ChkAll" runat="server" AutoPostBack="true" OnCheckedChanged="ChkAll_CheckedChanged" />Check
                        All:
                    </div>
                    <div class="box pndR15">
                        Time:
                    </div>
                    <div class="box pndR15">
                        Class Room:
                    </div>
                    <div class=" clear">
                    </div>
                    <div class="box pndR15">
                        <asp:CheckBox ID="ChkMon" runat="server" />Mon
                    </div>
                    <div class="box pndR15">
                        <asp:DropDownList ID="ddlDayType1" runat="server" CssClass="brdbox">
                            <asp:ListItem Text="--Select--"></asp:ListItem>
                            <asp:ListItem Text="FullDay"></asp:ListItem>
                            <asp:ListItem Text="AM"></asp:ListItem>
                            <asp:ListItem Text="PM"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="box pndR15">
                        <asp:DropDownList ID="ddlChildProgClassRoom" runat="server" CssClass="brdbox">
                        </asp:DropDownList>
                    </div>
                    <div class=" clear">
                    </div>
                    <div class="box pndR15">
                        <asp:CheckBox ID="ChkTue" runat="server" />Tue
                    </div>
                    <div class="box pndR15">
                        <asp:DropDownList ID="ddlDayType2" runat="server" CssClass="brdbox">
                            <asp:ListItem Text="--Select--"></asp:ListItem>
                            <asp:ListItem Text="FullDay"></asp:ListItem>
                            <asp:ListItem Text="AM"></asp:ListItem>
                            <asp:ListItem Text="PM"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="box pndR15">
                        <asp:DropDownList ID="ddlChildProgClassRoom1" runat="server" CssClass="brdbox">
                        </asp:DropDownList>
                    </div>
                    <div class=" clear">
                    </div>
                    <div class=" box pndR15">
                        <asp:CheckBox ID="ChkWen" runat="server" />Wen</div>
                    <div class=" box pndR15">
                        <asp:DropDownList ID="ddlDayType3" runat="server" CssClass="brdbox">
                            <asp:ListItem Text="--Select--"></asp:ListItem>
                            <asp:ListItem Text="FullDay"></asp:ListItem>
                            <asp:ListItem Text="AM"></asp:ListItem>
                            <asp:ListItem Text="PM"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class=" box pndR15">
                        <asp:DropDownList ID="ddlChildProgClassRoom2" runat="server" CssClass="brdbox">
                        </asp:DropDownList>
                    </div>
                    <div class=" clear">
                    </div>
                    <div class=" box pndR15">
                        <asp:CheckBox ID="ChkThus" runat="server" />Thus</div>
                    <div class=" box pndR15">
                        <asp:DropDownList ID="ddlDayType4" runat="server" CssClass="brdbox">
                            <asp:ListItem Text="--Select--"></asp:ListItem>
                            <asp:ListItem Text="FullDay"></asp:ListItem>
                            <asp:ListItem Text="AM"></asp:ListItem>
                            <asp:ListItem Text="PM"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class=" box pndR15">
                        <asp:DropDownList ID="ddlChildProgClassRoom3" runat="server" CssClass="brdbox">
                        </asp:DropDownList>
                    </div>
                    <div class=" clear">
                    </div>
                    <div class="box pndR15">
                        <asp:CheckBox ID="ChkFri" runat="server" />Fri</div>
                    <div class="box pndR15">
                        <asp:DropDownList ID="ddlDayType5" runat="server" CssClass="brdbox">
                            <asp:ListItem Text="--Select--"></asp:ListItem>
                            <asp:ListItem Text="FullDay"></asp:ListItem>
                            <asp:ListItem Text="AM"></asp:ListItem>
                            <asp:ListItem Text="PM"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="box pndR15">
                        <asp:DropDownList ID="ddlChildProgClassRoom4" runat="server" CssClass="brdbox">
                        </asp:DropDownList>
                    </div>
                    <div class=" clear">
                    </div>
                </div>
            </fieldset>
            <div class=" clear">
            </div>
            <div class=" right">
                <div class=" left" style="padding-right: 15px; padding-bottom: 15px;">
                    <asp:Button ID="btnSave" runat="server" Text="Save" CssClass=" btn" OnClick="btnSave_Click" /></div>
                <div class=" left">
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass=" btn" OnClick="btnCancel_Click" /></div>
            </div>
        </div>
        <telerik:RadGrid ID="rgProgramEnrollment" runat="server" CssClass="RemoveBorders"
            ShowGroupPanel="false" Width="100%" PagerStyle-AlwaysVisible="true" PageSize="10"
            BorderWidth="0" ClientSettings-Scrolling-AllowScroll="true" AllowFilteringByColumn="True"
            Height="600px" AllowPaging="true" AllowSorting="true" OnNeedDataSource="rgProgramEnrollment_NeedDataSource"
            OnEditCommand="rgProgramEnrollment_EditCommand" OnItemCommand="rgProgramEnrollment_ItemCommand"
            OnDetailTableDataBind="rgProgramEnrollment_DetailTableDataBind" OnPreRender="rgProgramEnrollment_PreRender">
            <GroupingSettings CaseSensitive="false" />
            <ItemStyle Wrap="true" />
            <HeaderContextMenu EnableEmbeddedSkins="true" />
            <PagerStyle AlwaysVisible="True" ShowPagerText="true" Mode="NextPrevAndNumeric" />
            <MasterTableView AutoGenerateColumns="false" Width="100%" HierarchyLoadMode="Client"
                DataKeyNames="SchoolProgramId">
                <%--  <GroupByExpressions>
                    <telerik:GridGroupByExpression> GroupLoadMode="Client"
                        <SelectFields>
                            <telerik:GridGroupByField FieldAlias="Program" FieldName="ProgramTitle"></telerik:GridGroupByField>
                        </SelectFields>
                        <GroupByFields>
                            <telerik:GridGroupByField FieldName="ProgramTitle"></telerik:GridGroupByField>
                        </GroupByFields>
                    </telerik:GridGroupByExpression>
                </GroupByExpressions>
                <SortExpressions>
                    <telerik:GridSortExpression FieldName="DayIndex" />
                </SortExpressions>--%>
                <Columns>
                    <telerik:GridEditCommandColumn HeaderStyle-Width="5%" HeaderText="Edit" ButtonType="ImageButton"
                        Reorderable="False" UniqueName="Edit" ItemStyle-BorderStyle="None">
                        <HeaderStyle Width="5%"></HeaderStyle>
                    </telerik:GridEditCommandColumn>
                    <telerik:GridBoundColumn DataField="ProgramTitle" HeaderText="Program" UniqueName="ProgramTitle">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="Day" HeaderText="Day" UniqueName="Day" Visible="false">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="DayType" HeaderText="Time" UniqueName="DayType"
                        Visible="false">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="ClassRoom" HeaderText="Class Room" UniqueName="ClassRoom"
                        Visible="false">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="ChildSchoolYearId" HeaderText="ChildSchoolYearId"
                        UniqueName="ChildSchoolYearId" Visible="false">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="SchoolProgramId" HeaderText="SchoolProgramId"
                        UniqueName="SchoolProgramId" Visible="false">
                    </telerik:GridBoundColumn>
                </Columns>
                <DetailTables>
                    <telerik:GridTableView DataKeyNames="Id" AutoGenerateColumns="false" Width="100%"
                        PagerStyle-AlwaysVisible="false">
                        <ParentTableRelation>
                            <telerik:GridRelationFields DetailKeyField="SchoolProgramId" MasterKeyField="SchoolProgramId" />
                        </ParentTableRelation>
                        <Columns>
                            <telerik:GridBoundColumn DataField="Day" HeaderText="Day" UniqueName="Day" AllowFiltering="false"
                                AllowSorting="false" SortExpression="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="DayType" HeaderText="Time" UniqueName="DayType"
                                AllowFiltering="false" AllowSorting="false" SortExpression="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="ClassRoom" HeaderText="Class Room" UniqueName="ClassRoom"
                                AllowFiltering="false" AllowSorting="false" SortExpression="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="ChildSchoolYearId" HeaderText="ChildSchoolYearId"
                                UniqueName="ChildSchoolYearId" Visible="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="SchoolProgramId" HeaderText="SchoolProgramId"
                                UniqueName="SchoolProgramId" Visible="false">
                            </telerik:GridBoundColumn>
                        </Columns>
                    </telerik:GridTableView>
                </DetailTables>
            </MasterTableView>
            <ClientSettings AllowColumnsReorder="True" AllowDragToGroup="false" ReorderColumnsOnClient="True">
                <Selecting AllowRowSelect="True" />
                <Scrolling AllowScroll="True" UseStaticHeaders="True" />
            </ClientSettings>
            <FilterMenu EnableEmbeddedSkins="true" ExpandAnimation-Type="Linear">
                <ExpandAnimation Type="Linear" />
            </FilterMenu>
        </telerik:RadGrid>
        <telerik:RadAjaxLoadingPanel ID="RAPL1" runat="server" Transparency="10">
        </telerik:RadAjaxLoadingPanel>
    </div>
    <asp:HiddenField ID="hdnMon" runat="server" />
    <asp:HiddenField ID="hdnTue" runat="server" />
    <asp:HiddenField ID="hdnThus" runat="server" />
    <asp:HiddenField ID="hdnWen" runat="server" />
    <asp:HiddenField ID="hdnFri" runat="server" />
</asp:Content>
