<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="DayCare.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Welcome to iKidzCare</title>
    <link href="css/DayCare.css" rel="stylesheet" type="text/css" />
    <link href="css/logincss.css" rel="stylesheet" type="text/css" />
    <link rel="shortcut icon" type="image/x-icon" href="../images/favicon.ico">

    <script type="text/javascript" language="javascript">
        function OnLeave(cname, data) {
            if (cname.value.length == 0) {
                cname.value = data;

                cname.style.color = 'gray';
            }
        }
        function OnEnter(cname, data) {
            if (cname.value == data) {
                cname.value = "";
                cname.style.color = 'black';
            }
        }

        if (window != window.top) window.top.navigate(window.location.href);
        function mOver(btnid) {
            btnid.style.color = "#c00";
        }

        function mOut(btnid) {
            btnid.style.color = "Black";
        }
        function mOver(btnid) {
            btnid.style.color = "#c00";
        }
    </script>

</head>
<body class="loginbody">
    <form id="form1" runat="server">
    <div id="main">
    <div class="logo2"><img src="images/logo.png" /></div>
        <div class="main">
        <div class="login_cont">
        
            <div style="padding-left: 39px; ">
                School:
                <asp:DropDownList ID="ddlSchool" runat="server" Style="width: 200px; height: 22px;">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlSchool"
                    ErrorMessage="Please select school." ToolTip="Please select school." InitialValue="00000000-0000-0000-0000-000000000000"
                    ValidationGroup="loginUser"></asp:RequiredFieldValidator>
            </div>
            <div style="padding-left: 20px;">
                Username:
                <asp:TextBox ID="txtLoginName" runat="server" CssClass="textfilds" Text="" Style="width: 180px;"></asp:TextBox><asp:RequiredFieldValidator
                    ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtLoginName"
                    ErrorMessage="User Name is required." ToolTip="User Name is required." ValidationGroup="loginUser"></asp:RequiredFieldValidator></div>
            <div style="padding-left: 20px;">
                Password:&nbsp;
                <asp:TextBox ID="txtLoginPassword" runat="server" TextMode="Password" CssClass="textfilds"
                    Style="width: 180px;"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator2"
                        runat="server" ControlToValidate="txtLoginPassword" ErrorMessage="Password is required."
                        ToolTip="Password is required." ValidationGroup="loginUser"></asp:RequiredFieldValidator></div>
            <div style="height: 5px;">
            </div>
            <div class="clear">
            </div>
            <div style="width: 180px; float: left;">
                <div style="padding-bottom: 10px; color: #d3d3d3;">
                    <asp:CheckBox ID="RememberMe" Checked="true" Text="Remember my Username" runat="server" /></div>
                <div>
                    <a href="#" style="color: #d3d3d3;">Forgot&nbsp;my&nbsp;Username/Password</a></div>
            </div>
            <div style="float: right; width: 63px;">
                <asp:Button  ID="btnLogin" ValidationGroup="loginUser" CssClass="loginbtn"
                    runat="server" OnClick="btnLogin_Click" /></div>
            <div>
                <asp:Label ID="FailureText" runat="server" ForeColor="Red"></asp:Label></div>
        </div>
        </div>
        <div class="blue_table"></div>
    </div>
    </form>
    
</body>
</html>
