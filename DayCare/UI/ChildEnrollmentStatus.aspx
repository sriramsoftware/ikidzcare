<%@ Page Title="" Language="C#" MasterPageFile="~/UI/DayCareIndex.Master" AutoEventWireup="true"
    CodeBehind="ChildEnrollmentStatus.aspx.cs" Inherits="DayCare.UI.ChildEnrollmentStatus" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/UI/UserControls/MenuLink.ascx" TagName="UCMenuLink" TagPrefix="ucl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h3 class="title">
        <ucl:UCMenuLink id="usrMenuLink" runat="server" />
    </h3>

    <telerik:RadGrid ID="rgChildEnrollmentStatus" runat="server" AutoGenerateColumns="false"
        AllowFilteringByColumn="false" AllowPaging="true" AllowSorting="true" EnableLinqExpression="false"
        EnableEmbeddedSkins="true" EnableAjaxSkinRendering="true" ReorderColumnOnClient="true"
        GridLines="None" CssClass="RemoveBorders" ShowGroupPanel="false" allowColumnReorder="True"
        Width="100%" Height="430px" BorderWidth="0px" OnNeedDataSource="rgChildEnrollmentStatus_NeedDataSource"
        OnEditCommand="rgChildEnrollmentStatus_EditCommand" OnInsertCommand="rgChildEnrollmentStatus_InsertCommand"
        OnItemCreated="rgChildEnrollmentStatus_ItemCreated" OnItemDataBound="rgChildEnrollmentStatus_ItemDataBound"
        OnUpdateCommand="rgChildEnrollmentStatus_UpdateCommand" OnItemCommand="rgChildEnrollmentStatus_ItemCommand">
        <ItemStyle Wrap="true" />
        <PagerStyle AlwaysVisible="true" Mode="NextPrevAndNumeric" ShowPagerText="true" />
        <MasterTableView DataKeyNames="Id" PagerStyle-AlwaysVisible="true" EditMode="InPlace"
            CommandItemDisplay="TopAndBottom" InsertItemDisplay="Top" TableLayout="Auto"
            AllowFilteringByColumn="true">
            <CommandItemSettings AddNewRecordText="Add New Child Enrollment Status" />
            <Columns>
                <telerik:GridEditCommandColumn ButtonType="ImageButton" HeaderStyle-Width="5%" Reorderable="true"
                    HeaderText="Edit" UpdateText="Edit" UniqueName="Edit">
                </telerik:GridEditCommandColumn>
                <telerik:GridTemplateColumn HeaderText="Enrollment Status" UniqueName="Enrollmentstatus"
                    AllowFiltering="false">
                    <ItemTemplate>
                        <asp:Label ID="lblChildEnrollmentStatus" runat="server" Text='<%# Eval("EnrollmentStatus") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddlEnrollmentStatus" runat="server">
                        </asp:DropDownList>
                    </EditItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderText="Enrollment Date" UniqueName="EnrollmentDate"
                    AllowFiltering="false">
                    <ItemTemplate>
                        <asp:Label ID="lblEnrollmentDate" runat="server" Text='<%# Eval("EnrollmentDate") != null ? Convert.ToDateTime(Eval("EnrollmentDate").ToString()).ToShortDateString() : ""%>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <telerik:RadDatePicker ID="rdpEnrollmentDate" runat="server">
                        </telerik:RadDatePicker>
                    </EditItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderText="Comments" UniqueName="Comments">
                    <ItemTemplate>
                        <asp:Label ID="lblComments" runat="server" Text='<%# Eval("Comments") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtComments" runat="server" Text='<%# Eval("Comments") %>' TextMode="MultiLine"></asp:TextBox>
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
            <telerik:AjaxSetting AjaxControlID="rgChildEnrollmentStatus">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgChildEnrollmentStatus" LoadingPanelID="RAPL1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <telerik:RadAjaxLoadingPanel ID="RAPL1" runat="server" Transparency="10">
    </telerik:RadAjaxLoadingPanel>
</asp:Content>
