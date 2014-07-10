<%@ Page Title="" Language="C#" MasterPageFile="~/UI/DayCareIndex.Master" AutoEventWireup="true"
    CodeBehind="FamilyPayment.aspx.cs" Inherits="DayCare.UI.FamilyPayment" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript">

        function Check() {
            // var btnSave = document.getElementById('ctl00_ContentPlaceHolder1_btnSave');
            //alert(document.getElementById("ctl00_ContentPlaceHolder1_btnSave").disabled);
            
           // btnSave.disabled = true;
            var input = document.getElementsByTagName("input");
            var date = 0;
            var paymentmethod = 0;
            var amount = 0;
            for (var k = 0; k < input.length; k++) {
                if (input[k].type == "text") {
                    if ((input[k].id.indexOf("rdpPostDate") > 0) && input[k].value != "") {
                        date++;
                    }
                    if ((input[k].id.indexOf("txtAmount") > 0) && input[k].value != "") {
                        amount++;
                    }
                }
            }
            var input = document.getElementsByTagName("select");
            for (var k = 0; k < input.length; k++) {
                if (input[k].type == "select" || input[k].type == "select-one") {
                    if ((input[k].id.indexOf("ddlPaymentMethod") > 0) && input[k].selectedIndex > 0) {
                        paymentmethod++;
                    }
                }
            }
            if (date > 0 && amount > 0 && paymentmethod > 0) {
                document.getElementById("ctl00_ContentPlaceHolder1_btnSave").style.display = "none";
                return true;
            }
            else {
                document.getElementById("ctl00_ContentPlaceHolder1_btnSave").style.display = "block";
                alert("Please enter valid details at least a family");
                return false;
            }
        }
            
        
    </script>

    <h3 class="title">
        <img src="../images/arrow.png" />&nbsp; Payment</h3>
    <div class="clear">
    </div>
    <asp:ValidationSummary ID="valSum" runat="server" ValidationGroup="Familyvalidate"
        ShowMessageBox="true" ShowSummary="false" />
    <telerik:RadGrid ID="rgFamilyPayment" runat="server" CssClass="RemoveBorders" Width="100%"
        AllowPaging="false" AllowSorting="true" AutoGenerateColumns="false" OnNeedDataSource="rgFamilyPayment_NeedDataSource"
        PagerStyle-AlwaysVisible="true" ClientSettings-AllowDragToGroup="false" ClientSettings-AllowAutoScrollOnDragDrop="false"
        AutoGenerateEditColumn="false" BorderWidth="0" ClientSettings-Scrolling-AllowScroll="true"
        AllowFilteringByColumn="false" OnEditCommand="rgFamilyPayment_EditCommand" GridLines="None"
        OnItemCommand="rgFamilyPayment_ItemCommand" OnPreRender="rgFamilyPayment_PreRender"
        OnItemCreated="rgFamilyPayment_ItemCreated" OnItemDataBound="rgFamilyPayment_ItemDataBound"
        OnInsertCommand="rgFamilyPayment_InsertCommand" OnUpdateCommand="rgFamilyPayment_UpdateCommand"
        OnDeleteCommand="rgFamilyPayment_DeleteCommand">
        <GroupingSettings CaseSensitive="false" />
        <ItemStyle Wrap="true" />
        <HeaderContextMenu EnableEmbeddedSkins="true" />
        <%-- <PagerStyle AlwaysVisible="True" ShowPagerText="true" Mode="NextPrevAndNumeric" />--%>
        <MasterTableView CommandItemDisplay="None" TableLayout="Auto" DataKeyNames="Id" EditFormSettings-EditColumn-Display="false"
            EditMode="InPlace" AllowAutomaticInserts="false" Width="100%" HierarchyLoadMode="Client"
            AllowAutomaticUpdates="false">
            <%--<DetailTables>
                <telerik:GridTableView DataKeyNames="Id" CssClass="RemoveBorders" AutoGenerateColumns="false"
                    Width="100%" AllowPaging="false" PagerStyle-AlwaysVisible="false" DataMember="Payment"
                    EditMode="InPlace" CommandItemDisplay="Top" CommandItemSettings-AddNewRecordText="Add New Payment">
                    <ParentTableRelation>
                        <telerik:GridRelationFields DetailKeyField="ChildFamilyId" MasterKeyField="ChildFamilyId" />
                    </ParentTableRelation>
                    <Columns>
                        <telerik:GridEditCommandColumn HeaderStyle-Width="5%" HeaderText="Edit" ButtonType="ImageButton"
                            Reorderable="False" UniqueName="Edit" ItemStyle-BorderStyle="None">
                            <HeaderStyle Width="5%"></HeaderStyle>
                        </telerik:GridEditCommandColumn>
                        <telerik:GridButtonColumn CommandName="Delete" HeaderText="Delete" UniqueName="DeleteColumn"
                            ButtonType="ImageButton" Reorderable="false" ItemStyle-BorderStyle="None" ConfirmText="Do you wnat to delete Payment?"
                            ConfirmTitle="Delete Record" ConfirmDialogType="RadWindow">
                            <HeaderStyle Width="5%"></HeaderStyle>
                        </telerik:GridButtonColumn>
                        <telerik:GridTemplateColumn HeaderText="Date" AllowFiltering="false" UniqueName="PostDate">
                            <ItemTemplate>
                                <%# Eval("PostDate") != null ? Convert.ToDateTime(Eval("PostDate")).ToString("MM/dd/yy") : ""%>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <telerik:RadDatePicker ID="rdpPostDate" runat="server" Width="100px">
                                    <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x">
                                    </Calendar>
                                    <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                                    <DateInput DisplayDateFormat="M/d/yyyy" DateFormat="M/d/yyyy">
                                    </DateInput>
                                </telerik:RadDatePicker>
                            </EditItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="Payment Method" AllowFiltering="false" UniqueName="PaymentMethod">
                            <ItemTemplate>
                                <asp:Label ID="lblPaymentMethod" runat="server"></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList ID="ddlPaymentMethod" runat="server">
                                    <asp:ListItem Text="--Select--" Value="-1"></asp:ListItem>
                                    <asp:ListItem Text="Cash" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Check" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Credit" Value="2"></asp:ListItem>
                                </asp:DropDownList>
                            </EditItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="Payment Detail" AllowFiltering="false" UniqueName="PaymentDetail">
                            <ItemTemplate>
                                <asp:Label ID="lblPaymentDetail" runat="server" Text='<%# Eval("PaymentDetail") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtPaymentDetail" runat="server" Text='<%# Eval("PaymentDetail") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="Amount" AllowFiltering="false" UniqueName="Amount">
                            <ItemTemplate>
                                <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("Amount") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtAmount" runat="server" Text='<%# Eval("Amount") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="SchoolYearId" AllowFiltering="false" HeaderText="SchoolYearId"
                            UniqueName="SchoolYearId" Visible="false">
                        </telerik:GridBoundColumn>
                        <%--<telerik:GridTemplateColumn HeaderText="ChildFamilyId" AllowFiltering="true" UniqueName="ChildFamilyId" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblChildFamilyId" runat="server" Text='<%# Eval("ChildFamilyId") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:Label ID="txtChildFamilyId" runat="server" Text='<%# Eval("ChildFamilyId") %>'></asp:Label>
                            </EditItemTemplate>
                        </telerik:GridTemplateColumn>
            
            </Columns>
            <NoRecordsTemplate>
                No Payments
            </NoRecordsTemplate>
            </telerik:GridTableView> </DetailTables>--%>
            <Columns>
                <%--<telerik:GridEditCommandColumn HeaderStyle-Width="5%" HeaderText="" ButtonType="ImageButton"
                    Reorderable="False" UniqueName="Edit" ItemStyle-BorderStyle="None">
                    <HeaderStyle Width="5%"></HeaderStyle>
                </telerik:GridEditCommandColumn>--%>
                <telerik:GridTemplateColumn HeaderText="Guardian" AllowFiltering="true" SortExpression="FamilyTitle"
                    UniqueName="FamilyTitle">
                    <ItemTemplate>
                        <asp:Label ID="lblFamilyTitle" runat="server" Text='<%# Eval("FamilyTitle") %>'></asp:Label>
                    </ItemTemplate>
                    <%-- <HeaderStyle Width="20%"></HeaderStyle>--%>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="ChildName" AllowFiltering="false" AllowSorting="false"
                    HeaderText="Child">
                    <%-- <HeaderStyle Width="25%"></HeaderStyle>--%>
                </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn HeaderText="Date" AllowFiltering="false" UniqueName="PostDate">
                    <ItemTemplate>
                        <telerik:RadDatePicker ID="rdpPostDate" runat="server" Width="100px" SelectedDate='<%# DateTime.Now %>'>
                            <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x">
                            </Calendar>
                            <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                            <DateInput DisplayDateFormat="M/d/yyyy" DateFormat="M/d/yyyy">
                            </DateInput>
                        </telerik:RadDatePicker>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderText="Payment Method" AllowFiltering="false" UniqueName="PaymentMethod">
                    <ItemTemplate>
                        <asp:DropDownList ID="ddlPaymentMethod" runat="server">
                            <asp:ListItem Text="--Select--" Value="-1"></asp:ListItem>
                            <asp:ListItem Text="Cash" Value="0"></asp:ListItem>
                            <asp:ListItem Text="Check" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Credit" Value="2"></asp:ListItem>
                        </asp:DropDownList>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderText="Detail" AllowFiltering="false" UniqueName="PaymentDetail">
                    <ItemTemplate>
                        <asp:TextBox ID="txtPaymentDetail" Width="200px" runat="server"></asp:TextBox>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderText="Amount" AllowFiltering="false" UniqueName="Amount">
                    <ItemTemplate>
                        <asp:TextBox ID="txtAmount" runat="server" Width="80px" MaxLength="9"></asp:TextBox>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <%--<telerik:GridBoundColumn DataField="UserName" AllowFiltering="true" AllowSorting="true"
                    HeaderText="Username" SortExpression="true">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Code" AllowFiltering="false" AllowSorting="false"
                    HeaderText="CheckIn/Out Code" Visible="false">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Email" AllowFiltering="false" AllowSorting="false"
                    HeaderText="Email">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="HomePhone" AllowFiltering="false" AllowSorting="false"
                    HeaderText="Phone">
                </telerik:GridBoundColumn>--%>
                <telerik:GridTemplateColumn HeaderText="ChildFamilyId" AllowFiltering="true" UniqueName="ChildFamilyId"
                    Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblChildFamilyId" runat="server" Text='<%# Eval("ChildFamilyId") %>'></asp:Label>
                    </ItemTemplate>
                    <%--<EditItemTemplate>
                        <asp:Label ID="txtChildFamilyId" runat="server" Text='<%# Eval("ChildFamilyId") %>'></asp:Label>
                    </EditItemTemplate>--%>
                </telerik:GridTemplateColumn>
            </Columns>
            <NoRecordsTemplate>
                No Families
            </NoRecordsTemplate>
        </MasterTableView>
        <ClientSettings AllowColumnsReorder="false" AllowDragToGroup="false" ReorderColumnsOnClient="True">
            <Selecting AllowRowSelect="True" />
            <Scrolling AllowScroll="false" UseStaticHeaders="True" />
        </ClientSettings>
        <FilterMenu EnableEmbeddedSkins="true" ExpandAnimation-Type="Linear">
            <ExpandAnimation Type="Linear" />
        </FilterMenu>
    </telerik:RadGrid>
    <br />
    <div class="right" style="padding-top: 0px; padding-bottom: 15px;">
        <div class=" left">
            <%--ValidationGroup="ChildValidate"--%>
            <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" 
                CausesValidation="true" UseSubmitBehavior="true" EnableViewState="false"  CssClass="btn"  /></div>
        <div class=" left" style="padding-left: 15px;">
            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false"
                OnClick="btnCancel_Click" CssClass="btn"  EnableViewState="false" /></div>
    </div>
    <div class="clear">
    </div>
    <div class="clear">
    </div>
    <telerik:RadAjaxManagerProxy ID="AjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="rgFamilyPayment">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgFamilyPayment" LoadingPanelID="RAPL1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <telerik:RadAjaxLoadingPanel ID="RAPL1" runat="server" Transparency="10">
    </telerik:RadAjaxLoadingPanel>
    <asp:HiddenField ID="hdnName" runat="server" />
</asp:Content>
