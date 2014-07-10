<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LateFee.aspx.cs" Inherits="DayCare.LateFee" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../css/DayCare.css" rel="stylesheet" type="text/css" />
    <link href="../css/style.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        function GetRadWindow() {
            var oWindow = null;
            if (window.radWindow) oWindow = window.radWindow; //Will work in Moz in all cases, including clasic dialog
            else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow; //IE (and Moz az well)
            return oWindow;
        }


        function CloseOnReload() {
            GetRadWindow().Close();
            GetRadWindow().BrowserWindow.location.reload();
        }

        function Error() {
            alert("Internal error");
            return false;
        }

    </script>

</head>
<body>
    <form id="form1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server" AsyncPostBackErrorMessage="We cannot serve your request right now. Try again later."
        ScriptMode="Release" AsyncPostBackTimeout="1800" EnablePageMethods="True">
    </telerik:RadScriptManager>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" EnableAJAX="true" EnableEmbeddedScripts="true">
    </telerik:RadAjaxManager>
    <div class="LateFee">
        <div align="center" style="text-align: center;">
            <table width="100%" align="center">
                <tr>
                    <td height="15">
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblMessage" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td height="30">
                    </td>
                </tr>
                <tr>
                    <td>
                        Late Fee:
                        <asp:TextBox ID="txtLateFee" runat="server" Text="15"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtLateFee"
                            SetFocusOnError="true" ErrorMessage="Please enter Fee." Text="*" Font-Size="0"
                            ValidationGroup="LateFeeValidate"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="rev" runat="server" ControlToValidate="txtLateFee"
                            SetFocusOnError="true" ErrorMessage="Please enter valid Late Fee." Text="*" Font-Size="0"
                            ValidationGroup="LateFeeValidate" ValidationExpression="(^-?\d\d*\.\d*$)|(^-?\d\d*$)|(^-?\.\d\d*$)"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                 <tr>
                    <td height="15">
                    </td>
                </tr>
                <tr>
                    <td align="center" style="text-align: center;">
                        <div style="text-align: center;">
                            <asp:Button ID="btnSave" runat="server" CssClass="btnLateFee" Text="Save" ValidationGroup="LateFeeValidate"
                                OnClick="btnSave_Click" />
                        </div>
                    </td>
                </tr>
            </table>
            <asp:Label ID="lblscript" runat="server"></asp:Label>
            <asp:ValidationSummary ID="valSum" runat="server" ValidationGroup="LateFeeValidate"
                ShowMessageBox="true" ShowSummary="false" />
        </div>
    </div>
    </form>
</body>
</html>
