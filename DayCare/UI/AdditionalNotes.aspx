<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdditionalNotes.aspx.cs"
    Inherits="DayCare.UI.AdditionalNotes" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/UI/UserControls/MenuLink.ascx" TagName="UCMenuLink" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../css/DayCare.css" rel="stylesheet" type="text/css" />
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server" />
    <div style="font-family:Arial; font-size:12px;">
    <fieldset style=" width:840px; margin-left:5px;">
    <legend><strong>Child Details</strong></legend>
        <div class="box pndR15" style="padding-left:10px; width:142px;">
            <span class="red">*</span>Date:
            <br />
            <telerik:RadDatePicker ID="radCommentDate" runat="server">
            </telerik:RadDatePicker>
            <%--<asp:TextBox ID="txtFirstName" runat="server" CssClass="fildboxstaff"></asp:TextBox>--%>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="radCommentDate"
                ErrorMessage="Please select date." Text="*" Font-Size="0" ValidationGroup="StaffValidate"></asp:RequiredFieldValidator>
        </div>
        <div class="box pndR15">
            <span class="red">*</span>Notes:
            <br />
            <asp:TextBox ID="txtComment" runat="server" CssClass="fildboxstaff" TextMode="MultiLine"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtComment"
                ErrorMessage="Please enter comment." Text="*" Font-Size="0" ValidationGroup="StaffValidate"></asp:RequiredFieldValidator>
        </div>
        <div class="" style="padding-top:20px;">
        <div class="left" style=" padding-right:15px;">
            <asp:ValidationSummary ID="valSum" runat="server" ValidationGroup="StaffValidate"
                ShowMessageBox="true" ShowSummary="false" />
            <asp:Button ID="btnSave" runat="server" CssClass="btn" Text="Save" OnClick="btnSave_Click"
                ValidationGroup="StaffValidate" /></div><div class="left">
            <asp:Button ID="btnCancel" runat="server" CssClass="btn" Text="Cancel" /></div></div>
         </fieldset>
    </div>
    <div class="clear"></div>
    <telerik:RadGrid ID="rgAdditionalNotes" runat="server" AutoGenerateColumns="false"
        AllowFilteringByColumn="false" AllowPaging="true" AllowSorting="true" EnableLinqExpression="false"
        EnableEmbeddedSkins="true" EnableAjaxSkinRendering="true" ReorderColumnOnClient="true"
        GridLines="None" CssClass="RemoveBorders" ShowGroupPanel="false" allowColumnReorder="True"
        Width="100%" Height="335px" BorderWidth="0px" OnNeedDataSource="rgAdditionalNotes_NeedDataSource"
        OnEditCommand="rgAdditionalNotes_EditCommand" OnInsertCommand="rgAdditionalNotes_InsertCommand"
        OnItemCreated="rgAdditionalNotes_ItemCreated" OnItemDataBound="rgAdditionalNotes_ItemDataBound"
        OnUpdateCommand="rgAdditionalNotes_UpdateCommand" OnItemCommand="rgAdditionalNotes_ItemCommand"
        OnDeleteCommand="rgAdditionalNotes_DeleteCommand">
        <ItemStyle Wrap="true" />
        <PagerStyle AlwaysVisible="true" Mode="NextPrevAndNumeric" ShowPagerText="true" />
        <MasterTableView DataKeyNames="Id" PagerStyle-AlwaysVisible="true" EditMode="InPlace"
            CommandItemDisplay="none" InsertItemDisplay="Top" TableLayout="Auto" AllowFilteringByColumn="true"
            Width="100%">
            <Columns>
                <telerik:GridEditCommandColumn ButtonType="ImageButton" HeaderStyle-Width="5%" Reorderable="true"
                    HeaderText="Edit" UpdateText="Edit" UniqueName="Edit">
                </telerik:GridEditCommandColumn>
                <telerik:GridButtonColumn CommandName="Delete" HeaderText="Delete" UniqueName="DeleteColumn"
                    ButtonType="ImageButton" Reorderable="false" ItemStyle-BorderStyle="None" ConfirmText="Do you want to delete record?"
                    ConfirmTitle="Delete Record" ConfirmDialogType="RadWindow">
                    <HeaderStyle Width="7%"></HeaderStyle>
                </telerik:GridButtonColumn>
                <telerik:GridTemplateColumn HeaderText="Date" UniqueName="CommentsDate" SortExpression="CommentsDate"
                    AllowFiltering="false" HeaderStyle-Width="100" >
                    <ItemTemplate>
                        <asp:Label ID="lblCommentsDate" runat="server" Text='<%# Eval("CommentDate","{0:MM/dd/yyyy}") %>'></asp:Label>
                    </ItemTemplate>
                    <%-- <EditItemTemplate>
                        <asp:TextBox ID="txtReason" runat="server" Text='<%# Eval("Reason") %>' TextMode="MultiLine"></asp:TextBox>
                    </EditItemTemplate>--%>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderText="Notes" UniqueName="Comments" SortExpression="Comments"
                    AllowFiltering="false">
                    <ItemTemplate>
                        <asp:Label ID="lblComments" runat="server" Text='<%# Eval("Comments") %>'></asp:Label>
                    </ItemTemplate>
                    <%-- <EditItemTemplate>
                        <asp:TextBox ID="txtReason" runat="server" Text='<%# Eval("Reason") %>' TextMode="MultiLine"></asp:TextBox>
                    </EditItemTemplate>--%>
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
            <telerik:AjaxSetting AjaxControlID="radCommentDate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="radCommentDate" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <%--<telerik:RadAjaxLoadingPanel ID="RAPL1" runat="server" Transparency="10">
    </telerik:RadAjaxLoadingPanel>--%>
    </form>
</body>
</html>
