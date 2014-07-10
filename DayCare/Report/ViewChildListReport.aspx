<%@ Page Title="" Language="C#" MasterPageFile="~/UI/DayCareIndex.Master" AutoEventWireup="true"
    CodeBehind="ViewChildListReport.aspx.cs" Inherits="DayCare.Report.ViewChildListReport" %>

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
            if (ctrl.id == 'ctl00_ContentPlaceHolder1_rcbchildList_i0_CheckBox') {
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
            var combo = $find('<%= rcbchildList.ClientID %>');
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
            var combo = $find('<%= rcbchildList.ClientID %>');
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
                    if (checkbox.id != 'ctl00_ContentPlaceHolder1_rcbchildList_i0_CheckBox') {
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
            var combo = $find('<%= rcbchildList.ClientID %>');
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
        <img src="../images/arrow.png" alt="" />&nbsp;ChildList Report
    </h3>
    <div class="clear" style="height: 8px;">
    </div>
    <fieldset>
        <legend><strong>Criteria</strong></legend>
        <div class="fieldDiv">
            <div class="box pndR15 ">
                <span class="red">*</span> Child:
                <br />
                <telerik:RadComboBox ID="rcbchildList" runat="server" AllowCustomText="true" HighlightTemplatedItems="true"
                    OnClientDropDownOpening="OnClientDropDownOpening" OnClientDropDownClosing="OnClientDropDownClosing"
                    OnClientSelectedIndexChanging="OnClientSelectedIndexChanging" OnClientBlur="OnClientBlur" Width="200px">
                    <ItemTemplate>
                        <asp:CheckBox runat="server" ID="CheckBox" onclick="checkboxClick(this);" Text="" /><%# DataBinder.Eval(Container, "Text") %>
                    </ItemTemplate>
                </telerik:RadComboBox>
                <%--<asp:DropDownList ID="ddlFamily" CssClass="fildbox" runat="server">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvfamily" runat="server" ControlToValidate="ddlFamily"
                    SetFocusOnError="true" Font-Size="0" ErrorMessage="Please select family." InitialValue="00000000-0000-0000-0000-000000000000"
                    Text="*" ValidationGroup="SchoolValidate"></asp:RequiredFieldValidator>--%>
            </div>
            <input type="hidden" id="comboValue" value="" runat="server" />
            <input type="hidden" id="chkSelectAll" value="" />
            <div class="box" style="margin-top: -8px;">
                <br />
                <%--<asp:Button ID="btnGrid" runat="server" Text="View In Grid" CssClass="btn" OnClick="btnGrid_Click" />--%>
                <asp:Button ID="btnSave" runat="server" Text="View Report" CssClass="viewReport"
                    OnClick="btnSave_Click" ValidationGroup="SchoolValidate" />
            </div>
        </div>
    </fieldset>
</asp:Content>
