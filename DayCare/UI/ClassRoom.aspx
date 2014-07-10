<%@ Page Title="" Language="C#" MasterPageFile="~/UI/DayCareIndex.Master" AutoEventWireup="true"
    CodeBehind="ClassRoom.aspx.cs" Inherits="DayCare.UI.ClassRoom" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script language="javascript">
        function IsDecimal(ctrl, Evt) {
            Evt = (Evt) ? Evt : window.event
            var Kcode = (Evt.which) ? Evt.which : Evt.keyCode

            if (Kcode == 32)//If Shift is pressed then cancel it
            {
                if (Evt.which) {
                    Evt.which = 505;
                }
                else {
                    Evt.keyCode = 505;
                }
                Kcode = 505;
            }

            if (Kcode >= 65 && Kcode <= 90 ||
              (Kcode == 505 || Kcode == 16 || Kcode == 189 || Kcode == 187 ||
               Kcode == 220 || Kcode == 111 || Kcode == 106 || Kcode == 109 || Kcode == 107 || Kcode == 191 ||
               Kcode == 188 || Kcode == 192)
          )
                return false;

            var str = ctrl.value;

            if (Kcode == 110 || Kcode == 190) {
                str = str + '.';
            }

            var dotcount = str.split('.');
            var cnt = dotcount.length;
            if (cnt > 2)
                return false;
        }
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;

            return true;
        }

    </script>

    <div class="clear">
    </div>
    <h3 class="title">
        <img src="../images/arrow.png" />&nbsp; Class Room</h3>
    <div class="clear">
    </div>
    <telerik:RadGrid ID="rgClassRoom" runat="server" AutoGenerateColumns="false" AllowFilteringByColumn="false"
        AllowPaging="true" AllowSorting="true" EnableLinqExpression="false" EnableEmbeddedSkins="true"
        EnableAjaxSkinRendering="true" ReorderColumnOnClient="true" GridLines="None"
        CssClass="RemoveBorders" ShowGroupPanel="false" allowColumnReorder="True" Width="100%"
        Height="430px" BorderWidth="0px" OnNeedDataSource="rgClassRoom_NeedDataSource"
        OnEditCommand="rgClassRoom_EditCommand" OnInsertCommand="rgClassRoom_InsertCommand"
        OnItemCreated="rgClassRoom_ItemCreated" OnItemDataBound="rgClassRoom_ItemDataBound"
        OnUpdateCommand="rgClassRoom_UpdateCommand" OnItemCommand="rgClassRoom_ItemCommand"
        OnDeleteCommand="rgClassRoom_DeleteCommand">
        <ItemStyle Wrap="true" />
        <PagerStyle AlwaysVisible="true" Mode="NextPrevAndNumeric" ShowPagerText="true" />
        <MasterTableView DataKeyNames="Id" PagerStyle-AlwaysVisible="true" EditMode="InPlace"
            CommandItemDisplay="TopAndBottom" InsertItemDisplay="Top" TableLayout="Auto"
            AllowFilteringByColumn="true" Width="100%">
            <CommandItemSettings AddNewRecordText="Add New Class Room" />
            <Columns>
                <telerik:GridEditCommandColumn ButtonType="ImageButton" HeaderStyle-Width="5%" Reorderable="true"
                    HeaderText="Edit" UpdateText="Edit" UniqueName="Edit">
                </telerik:GridEditCommandColumn>
                <telerik:GridButtonColumn CommandName="Delete" HeaderText="Delete" UniqueName="DeleteColumn"
                    ButtonType="ImageButton" Reorderable="false" ItemStyle-BorderStyle="None" ConfirmText="Do you want to delete this class room?"
                    ConfirmTitle="Delete Record" ConfirmDialogType="RadWindow">
                    <HeaderStyle Width="5%"></HeaderStyle>
                </telerik:GridButtonColumn>
                <telerik:GridBoundColumn DataField="Name" HeaderText="Name" SortExpression="Name"
                    AllowFiltering="true" UniqueName="Name">
                </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn HeaderText="Max Size" UniqueName="MaxSize" AllowFiltering="true">
                    <ItemTemplate>
                        <asp:Label ID="lblMaxSize" runat="server" Text='<%# Eval("MaxSize") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtMaxSize" runat="server" Text='<%# Eval("MaxSize") %>'></asp:TextBox>
                    </EditItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderText="Staff Name" UniqueName="FullName" AllowFiltering="true">
                    <ItemTemplate>
                        <asp:Label ID="lblStaffName" runat="server" Text='<%# Eval("FullName") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddlStaff" runat="server">
                        </asp:DropDownList>
                    </EditItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridCheckBoxColumn DataField="Active" HeaderText="Active" SortExpression="Active"
                    UniqueName="Active" HeaderTooltip="Checked True Then Active">
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
            <telerik:AjaxSetting AjaxControlID="rgClassRoom">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgClassRoom" LoadingPanelID="RAPL1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <telerik:RadAjaxLoadingPanel ID="RAPL1" runat="server" Transparency="10">
    </telerik:RadAjaxLoadingPanel>
</asp:Content>
