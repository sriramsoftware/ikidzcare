﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewAttendanceReport.aspx.cs"
    Inherits="DayCare.Report.ViewAttendanceReport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="width: 15%; float: left;">
        &nbsp;
    </div>
    <center>
        <div style="text-align: left; width: 1000px; float: left;">
            <CR:CrystalReportViewer ID="crp" runat="server" AutoDataBind="true" />
            <CR:CrystalReportSource ID="crdata" runat="server">
            </CR:CrystalReportSource>
        </div>
        <div style="width: 15%;">
            &nbsp;
        </div>
    </center>
    </form>
</body>
</html>
