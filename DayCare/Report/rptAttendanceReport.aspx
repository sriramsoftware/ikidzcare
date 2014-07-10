<%@ Page Title="" Language="C#" MasterPageFile="~/UI/DayCareIndex.Master" AutoEventWireup="true"
    CodeBehind="rptAttendanceReport.aspx.cs" Inherits="DayCare.Report.rptAttendance1" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript">
        var supressDropDownClosing = false;

        function OnClientDropDownClosing(sender, eventArgs) {

            eventArgs.set_cancel(supressDropDownClosing);
        }

        function OnClientSelectedIndexChanging(sender, eventArgs) {

            eventArgs.set_cancel(supressDropDownClosing);
        }

        function OnClientDropDownOpening(sender, eventArgs) {

            supressDropDownClosing = true;
        }

        function OnClientBlur(sender) {

            supressDropDownClosing = false;

            sender.toggleDropDown();
        }
        function checkboxClick(ctrl) {
            // alert("Jay Swaminarayan!");
            //debugger;
            //alert(ctrl.id);
            // alert(ctrl.checked);
            //debugger
            if (ctrl.id == 'ctl00_ContentPlaceHolder1_rcbStudentList_i0_CheckBox') {
                if (ctrl.checked) {
                    selectAllCheckbox();
                }
                else {
                    uncheckAllCheckbox();
                }
            }
            else {
                collectSelectedItems();
            }
        }
        function getItemCheckBox(item) {
            //Get the 'div' representing the current RadComboBox Item.
            var itemDiv = item.get_element();

            //Get the collection of all 'input' elements in the 'div' (which are contained in the Item).
            var inputs = itemDiv.getElementsByTagName("input");

            for (var inputIndex = 0; inputIndex < inputs.length; inputIndex++) {
                var input = inputs[inputIndex];

                //Check the type of the current 'input' element.
                if (input.type == "checkbox") {
                    return input;
                }
            }

            return null;
        }
        function collectSelectedItems() {
            //debugger;
            var combo = $find('<%= rcbStudentList.ClientID %>');
            // alert(combo);
            var items = combo.get_items();

            var selectedItemsTexts = "";
            var selectedItemsValues = "";

            var itemsCount = items.get_count();

            for (var itemIndex = 0; itemIndex < itemsCount; itemIndex++) {
                var item = items.getItem(itemIndex);

                var checkbox = getItemCheckBox(item);

                //Check whether the Item's CheckBox) is checked.
                if (checkbox.checked) {
                    selectedItemsTexts += item.get_text() + ", ";
                    selectedItemsValues += item.get_value() + ", ";
                }
            }

            selectedItemsTexts = selectedItemsTexts.substring(0, selectedItemsTexts.length - 2);
            selectedItemsValues = selectedItemsValues.substring(0, selectedItemsValues.length - 1);

            //Set the text of the RadComboBox with the texts of the selected Items, separated by ','.            
            combo.set_text(selectedItemsTexts);

            //Set the comboValue hidden field value with values of the selected Items, separated by ','.
            document.getElementById('<%= comboValue.ClientID %>').value = selectedItemsValues;

            if (selectedItemsValues == "") {
                combo.clearSelection();
            }
        }
        function selectAllCheckbox() {

            // var combo = $find('rcbStudentList');
            var combo = $find('<%= rcbStudentList.ClientID %>');
            //alert(combo);
            var items = combo.get_items();

            var selectedItemsTexts = "";
            var selectedItemsValues = "";

            var itemsCount = items.get_count();

            for (var itemIndex = 0; itemIndex < itemsCount; itemIndex++) {
                var item = items.getItem(itemIndex);

                var checkbox = getItemCheckBox(item);

                checkbox.checked = true;

                //Check whether the Item's CheckBox) is checked.
                if (checkbox.checked) {
                    if (checkbox.id != 'ctl00_ContentPlaceHolder1_rcbStudentList_i0_CheckBox') {
                        selectedItemsTexts += item.get_text() + ", ";
                        selectedItemsValues += item.get_value() + ", ";
                    }
                }
            }

            selectedItemsTexts = selectedItemsTexts.substring(0, selectedItemsTexts.length - 2);
            selectedItemsValues = selectedItemsValues.substring(0, selectedItemsValues.length - 2);

            //Set the text of the RadComboBox with the texts of the selected Items, separated by ','.   

            combo.set_text(selectedItemsTexts);

            //Set the comboValue hidden field value with values of the selected Items, separated by ','.
            //debugger;
            //document.getElementById("comboValue").value = selectedItemsValues;
            document.getElementById('<%= comboValue.ClientID %>').value = selectedItemsValues;
            if (selectedItemsValues == "") {
                combo.clearSelection();
            }
        }
        function uncheckAllCheckbox() {
            var combo = $find('<%= rcbStudentList.ClientID %>');
            //alert(combo);
            var items = combo.get_items();

            var selectedItemsTexts = "";
            var selectedItemsValues = "";

            var itemsCount = items.get_count();

            for (var itemIndex = 0; itemIndex < itemsCount; itemIndex++) {
                var item = items.getItem(itemIndex);

                var checkbox = getItemCheckBox(item);

                checkbox.checked = false;
            }
            document.getElementById('<%= comboValue.ClientID %>').value = "";
            combo.clearSelection();
        }
    </script>

    <h3 class="title">
        <img src="../images/arrow.png" />&nbsp; Attendance Report</h3>
    <fieldset>
        <legend><strong>Criteria</strong></legend>
        <div class="fieldDiv">
            <div class="box pndR15">
                Select Attendance Report For:
                <br />
                <asp:DropDownList ID="ddlReportFor" runat="server" CssClass="fildbox" OnSelectedIndexChanged="ddlReportFor_SelectedIndexChanged"
                    AutoPostBack="true">
                    <asp:ListItem Text="--Select--" Value="--Select--"></asp:ListItem>
                    <asp:ListItem Text="Staff Attendance by Name" Value="Staff"></asp:ListItem>
                    <asp:ListItem Text="Staff Attendance by Date" Value="StaffDate"></asp:ListItem>
                    <asp:ListItem Text="Student Attendance by Name" Value="Student"></asp:ListItem>
                    <asp:ListItem Text="Student Attendance by Date" Value="StudentDate"></asp:ListItem>
                    <asp:ListItem Text="Student Attendance by Class" Value="StudentClass"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="box pndR15" style="width: 160px;">
                Start Date:
                <br />
                <telerik:RadDatePicker ID="rdpStartDate" runat="server">
                </telerik:RadDatePicker>
            </div>
            <div class="box" style="width: 160px;">
                End Date:
                <br />
                <telerik:RadDatePicker ID="rdpEndDate" runat="server">
                </telerik:RadDatePicker>
            </div>
            <%-- <div class="box" style="padding: 15px 0 0 0;">
                
            </div>--%><div class="box" style="padding: 15px 0 0 0; width: 175px;">
                <telerik:RadComboBox ID="rcbStudentList" runat="server" AllowCustomText="true" HighlightTemplatedItems="true"
                    OnClientDropDownOpening="OnClientDropDownOpening" OnClientDropDownClosing="OnClientDropDownClosing"
                    OnClientSelectedIndexChanging="OnClientSelectedIndexChanging" OnClientBlur="OnClientBlur">
                    <ItemTemplate>
                        <asp:CheckBox runat="server" ID="CheckBox" onclick="checkboxClick(this);" Text="" /><%# DataBinder.Eval(Container, "Text") %>
                    </ItemTemplate>
                </telerik:RadComboBox>
            </div>
            <input type="hidden" id="comboValue" value="" runat="server" />
            <input type="hidden" id="chkSelectAll" value="" />
            <div class="box" style="width: 100px; margin-top: -8px;">
                <br />
                <asp:Button ID="btnSave" runat="server" Text="View Report" CssClass="viewReport"
                    OnClick="btnSave_Click" />
            </div>
        </div>
    </fieldset>
</asp:Content>
