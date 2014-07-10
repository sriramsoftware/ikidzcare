<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SchoolYearSelection.aspx.cs"
    Inherits="DayCare.UI.SchoolYearSelection" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Welcome to iKidzCare</title>
    <link href="../css/DayCare.css" rel="stylesheet" type="text/css" />
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
</head>
<body class="loginbody">
    <form id="form1" runat="server">
    <div align="center" style="padding-top:150px;">
        <div class="nextBg">
            <table align="center">
                <tr>
                    <td>
                        Select School Year:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlSchoolYear" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <br />
                        <asp:Button ID="btnNext" runat="server" Text="Next" CssClass="nextbtn" OnClick="btnNext_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    </form>
</body>
</html>
