<%@ Page Title="" Language="C#" MasterPageFile="~/UI/DayCareIndex.Master" AutoEventWireup="true"
    CodeBehind="ChildData.aspx.cs" Inherits="DayCare.UI.ChildData" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/UI/UserControls/MenuLink.ascx" TagName="UCMenuLink" TagPrefix="ucl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h3 class="title">
        <ucl:UCMenuLink ID="usrMenuLink" runat="server" />
    </h3>
            <fieldset > 
                <legend><strong>Child Details</strong></legend>
                <div class="fieldDiv">
                <div class="box pndR15" style="height: 170px;">
                    <div class="left" style="padding: 8px 65px;">
                        <div class="photo">
                            <asp:Image ID="imgStaff" runat="server" ImageUrl="~/StaffImages/male_photo.png" Width="99"
                                Height="125" />
                        </div>
                    </div>
                    <div class="box ">
                        Image:
                        <br />
                        <asp:FileUpload ID="fupImage" runat="server" />
                        <asp:Label ID="lblImage" runat="server" Visible="false"></asp:Label>
                    </div>
                </div>
                <div class="box pndR15">
                    First Name:
                    <br />
                    <asp:TextBox ID="txtFirstName" runat="server" CssClass="fildboxstaff"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtFirstName"
                        ErrorMessage="Please enter First Name." Text="*" Font-Size="0" ValidationGroup="StaffValidate"></asp:RequiredFieldValidator>
                </div>
                <div class="box pndR15">
                    Last Name:
                    <br />
                    <asp:TextBox ID="txtLastName" runat="server" CssClass="fildboxstaff"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtLastName"
                        ErrorMessage="Please enter Last Name." Text="*" Font-Size="0" ValidationGroup="StaffValidate"></asp:RequiredFieldValidator>
                </div>
                <div class="box ">
                    <div class=" left">
                        Date Of Birth:
                        <br />
                        <telerik:RadDatePicker ID="rdpDOB" runat="server" Width="100px">
                        
                        </telerik:RadDatePicker>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="rdpDOB"
                            ErrorMessage="Please enter Date Of Birth." Text="*" Font-Size="0" ValidationGroup="StaffValidate"></asp:RequiredFieldValidator></div>
                    <div class=" left">
                        Gender:
                        <br />
                        <asp:RadioButton ID="rdMale" runat="server" Checked="true" GroupName="Gender" />Male
                        &nbsp;
                        <asp:RadioButton ID="rdFemale" runat="server" GroupName="Gender" />Female
                    </div>
                </div>
                <div class="box pndR15" style="display:none;">
                    Social Security ?
                    <asp:TextBox ID="txtSocSec" runat="server" CssClass="fildboxstaff"></asp:TextBox>
                </div>
                <div class="box pndR15">
                    Comments:<br />
                    <asp:TextBox ID="txtComments" runat="server" TextMode="MultiLine" CssClass="fildboxstaff"></asp:TextBox>
                </div>
                <div class="box">
                    Active:<br />
                    <asp:CheckBox ID="chkActive" runat="server" ToolTip="Checked True Then Active" />
                    &nbsp;
                </div>
                <div class="right" style="padding-top: 25px;">
                    <div class=" left">
                        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" ValidationGroup="StaffValidate"
                            CausesValidation="true" CssClass="btn" /></div>
                    <div class=" left" style="padding-left: 15px;">
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false"
                            OnClick="btnCancel_Click" CssClass="btn" /></div>
                </div>
                </div>
            </fieldset>
    <div class="clear">
    </div>
    <telerik:RadGrid ID="rgChildData" CssClass="RemoveBorders" runat="server" Width="100%"
        Height="600px" AllowPaging="true" AllowSorting="true" AutoGenerateColumns="false"
        OnNeedDataSource="rgChildData_NeedDataSource" PagerStyle-AlwaysVisible="true"
        AutoGenerateEditColumn="false" PageSize="5" BorderWidth="0" ClientSettings-Scrolling-AllowScroll="true"
        AllowFilteringByColumn="True" OnEditCommand="rgChildData_EditCommand" GridLines="None"
        OnItemCommand="rgChildData_ItemCommand" OnItemDataBound="rgChildData_ItemDataBound"
        OnItemCreated="rgChildData_ItemCreated">
        <GroupingSettings CaseSensitive="false" />
        <ItemStyle Wrap="true" />
        <HeaderContextMenu EnableEmbeddedSkins="true" />
        <PagerStyle AlwaysVisible="True" ShowPagerText="true" Mode="NextPrevAndNumeric" />
        <MasterTableView CommandItemDisplay="None" TableLayout="Auto" CommandItemSettings-AddNewRecordText="Add New Child Data"
            DataKeyNames="Id" EditFormSettings-EditColumn-Display="false" AllowAutomaticInserts="false" Width="100%"
            AllowAutomaticUpdates="false">
            <Columns>
                <telerik:GridEditCommandColumn HeaderStyle-Width="5%" HeaderText="Edit" ButtonType="ImageButton"
                    Reorderable="False" UniqueName="Edit" ItemStyle-BorderStyle="None">
                    <HeaderStyle Width="5%"></HeaderStyle>
                </telerik:GridEditCommandColumn>
                <telerik:GridTemplateColumn HeaderText="Image" UniqueName="Photo" AllowFiltering="false">
                    <ItemTemplate>
                        <asp:Image ID="imgPhoto" runat="server" Height="25px" Width="25px" />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="FullName" UniqueName="FullName" AllowFiltering="true"
                    SortExpression="true" HeaderText="Name">
                </telerik:GridBoundColumn>
                
               
                <telerik:GridTemplateColumn UniqueName="Gender" AllowFiltering="true" HeaderText="Gender">
                    <ItemTemplate>
                        <%# Convert.ToBoolean(Eval("Gender").ToString())==true?"Male":"Female" %>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="DOB" UniqueName="DOB" AllowFiltering="true" SortExpression="true"
                    HeaderText="DOB" DataFormatString="{0:dd-MM-yyyy}">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="SocSec" UniqueName="SocSec" AllowFiltering="true"
                    SortExpression="true" HeaderText="SocSec" Visible="false">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Comments" DataField="Comments" UniqueName="Comments"
                    AllowFiltering="false" >
                </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn UniqueName="Active" AllowFiltering="true" HeaderText="Active">
                    <ItemTemplate>
                        <%# Convert.ToBoolean(Eval("Active").ToString())==true?"Active":"Inactive" %>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="ChildEnrollmentStatus" AllowFiltering="false"
                    HeaderText="Enrollment">
                    <ItemTemplate>
                        <asp:HyperLink ID="hlChildEnrollmentStatus" runat="server"><img src="../images/BrowseIcon.gif" height="16" width="16" /></asp:HyperLink>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="ChildAbsentHistory" AllowFiltering="false"
                    HeaderText="Absent History" >
                    <ItemTemplate>
                        <asp:HyperLink ID="hlChildAbsentHistory" runat="server"><img src="../images/BrowseIcon.gif" height="16" width="16" /></asp:HyperLink>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="ChildSchedule" AllowFiltering="false" HeaderText="Child Schedule">
                    <ItemTemplate>
                        <asp:HyperLink ID="hlChildSchedule" runat="server"><img src="../images/BrowseIcon.gif" height="16" width="16" /></asp:HyperLink>
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
    <telerik:RadAjaxLoadingPanel ID="RAPL1" runat="server" Transparency="10">
    </telerik:RadAjaxLoadingPanel>
    <asp:ValidationSummary ID="valSum" runat="server" ValidationGroup="StaffValidate"
        ShowMessageBox="true" ShowSummary="false" />
</asp:Content>
