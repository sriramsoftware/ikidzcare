using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions;
using CrystalDecisions.Web;
using CrystalDecisions.Shared;
using System.Configuration;
using System.Data;
namespace DayCare.Report
{
    public partial class ViewAttendanceReport : System.Web.UI.Page
    {
        CrystalDecisions.Web.Report rpt = new CrystalDecisions.Web.Report();
        CrystalDecisions.CrystalReports.Engine.ReportDocument rpt1;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Page_Init(object sender, EventArgs e)
        {
            #region
            if (Session["CurrentSchoolYearId"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            crdata.Report = rpt;
            rpt1 = crdata.ReportDocument;
            DataSet dsReport = new xmlClassWiseStudentAttendTime();
            DataSet ds = new DataSet();

            if (Request.QueryString["RepId"] == "Staff")
            {
                if (!String.IsNullOrEmpty(Session["StaffResult"].ToString()))
                {
                    rpt.FileName = Server.MapPath("rptStaffAttendanceReport.rpt");
                    DayCareBAL.StaffService proxyAttendance = new DayCareBAL.StaffService();

                    string ReportFor = "";
                    ReportFor = "Staff";
                    string SearchText = "";
                    string SearchStr = "";
                    string SchoolYear = "";

                    if (!String.IsNullOrEmpty(Request.QueryString["StartDate"]) && String.IsNullOrEmpty(Request.QueryString["EndDate"]))
                    {
                        SearchText += " sah.checkincheckoutdatetime>='" + Request.QueryString["StartDate"].ToString() + "'";
                        // SchoolYear += "and ssy.SchoolYearId='" + new Guid(Session["CurrentSchoolYearId"].ToString()) + "'";
                        if (Session["StaffResult"] != null)
                        {
                            SearchStr += "Name in(" + Session["StaffResult"].ToString() + ")";
                        }
                    }

                    else if (String.IsNullOrEmpty(Request.QueryString["StartDate"]) && !String.IsNullOrEmpty(Request.QueryString["EndDate"]))
                    {
                        SearchText += " sah.checkincheckoutdatetime<='" + Request.QueryString["EndDate"].ToString().Replace("00:00:00 PM", "11:59:58") + "'";
                        // SchoolYear += "and ssy.SchoolYearId='" + new Guid(Session["CurrentSchoolYearId"].ToString()) + "'";
                        if (Session["StaffResult"] != null)
                        {
                            SearchStr += "Name in(" + Session["StaffResult"].ToString() + ")";
                        }
                    }
                    else if (!String.IsNullOrEmpty(Request.QueryString["EndDate"]) && !String.IsNullOrEmpty(Request.QueryString["StartDate"]))
                    {
                        SearchText += " sah.checkincheckoutdatetime>='" + Request.QueryString["StartDate"].ToString() + "' and sah.checkincheckoutdatetime<='" + Request.QueryString["EndDate"].ToString().Replace("00:00:00", "11:59:58") + "'";
                        //SchoolYear += "and ssy.SchoolYearId='" + new Guid(Session["CurrentSchoolYearId"].ToString()) + "'";
                        if (!String.IsNullOrEmpty(Session["StaffResult"].ToString()))
                        {
                            SearchStr += "Name in(" + Session["StaffResult"].ToString() + ")";
                        }
                    }
                    if (String.IsNullOrEmpty(Request.QueryString["EndDate"]) && String.IsNullOrEmpty(Request.QueryString["StartDate"]) && !String.IsNullOrEmpty(Session["StaffResult"].ToString()))
                    {
                        SearchStr += "Name in(" + Session["StaffResult"].ToString() + ")";
                    }

                    SearchStr = Session["StaffResult"].ToString().Replace("'--Select All--',", "");

                    string[] str = SearchStr.Remove(SearchStr.Length - 1).Remove(0, 1).Replace("','", "$").Split('$');

                    string strFinalSearhString = "";

                    foreach (string s in str)
                    {
                        string s1 = "";
                        s1 = s.Replace("'", "''");
                        s1 = "'" + s1 + "',";
                        strFinalSearhString += s1;
                    }
                    strFinalSearhString = "Name in(" + strFinalSearhString.Remove(strFinalSearhString.Length - 1) + ")";

                    ds = proxyAttendance.LoadAttendanceHistory1(SearchText, strFinalSearhString, new Guid(Session["CurrentSchoolYearId"].ToString()));
                    dsReport.Tables["dtAttendance"].Merge(ds.Tables[0]);
                }
                else
                {
                    rpt.FileName = Server.MapPath("rptStaffAttendanceReport.rpt");
                    DayCareBAL.StaffService proxyAttendance = new DayCareBAL.StaffService();

                    string ReportFor = "";
                    ReportFor = "Staff";
                    string SearchText = "";
                    string SearchStr = "";
                    string SchoolYear = "";

                    if (!String.IsNullOrEmpty(Request.QueryString["StartDate"]) && String.IsNullOrEmpty(Request.QueryString["EndDate"]))
                    {
                        SearchText += " sah.checkincheckoutdatetime>='" + Request.QueryString["StartDate"].ToString() + "'";
                        // SchoolYear += "and ssy.SchoolYearId='" + new Guid(Session["CurrentSchoolYearId"].ToString()) + "'";

                    }

                    else if (String.IsNullOrEmpty(Request.QueryString["StartDate"]) && !String.IsNullOrEmpty(Request.QueryString["EndDate"]))
                    {
                        SearchText += " sah.checkincheckoutdatetime<='" + Request.QueryString["EndDate"].ToString().Replace("00:00:00 PM", "11:59:58") + "'";
                        // SchoolYear += "and ssy.SchoolYearId='" + new Guid(Session["CurrentSchoolYearId"].ToString()) + "'";
                    }
                    else if (!String.IsNullOrEmpty(Request.QueryString["EndDate"]) && !String.IsNullOrEmpty(Request.QueryString["StartDate"]))
                    {
                        SearchText += " sah.checkincheckoutdatetime>='" + Request.QueryString["StartDate"].ToString() + "' and sah.checkincheckoutdatetime<='" + Request.QueryString["EndDate"].ToString().Replace("00:00:00", "11:59:58") + "'";
                        //SchoolYear += "and ssy.SchoolYearId='" + new Guid(Session["CurrentSchoolYearId"].ToString()) + "'";
                    }


                    ds = proxyAttendance.LoadAttendanceHistory1(SearchText, SearchStr, new Guid(Session["CurrentSchoolYearId"].ToString()));
                    dsReport.Tables["dtAttendance"].Merge(ds.Tables[0]);
                }
                CrystalDecisions.CrystalReports.Engine.TextObject titleText = (CrystalDecisions.CrystalReports.Engine.TextObject)rpt1.ReportDefinition.ReportObjects["Text7"];
                CrystalDecisions.CrystalReports.Engine.TextObject titleTextSchool = (CrystalDecisions.CrystalReports.Engine.TextObject)rpt1.ReportDefinition.ReportObjects["Text8"];
                CrystalDecisions.CrystalReports.Engine.TextObject footer = (CrystalDecisions.CrystalReports.Engine.TextObject)rpt1.ReportDefinition.ReportObjects["txtfooter"];
                footer.Text = Common.GetSchoolWiseAddress(new Guid(Session["SchoolId"].ToString()));
                if (!String.IsNullOrEmpty(Request.QueryString["StartDate"]) && !String.IsNullOrEmpty(Request.QueryString["EndDate"]))
                    titleText.Text = "Staff Attendance Report From " + Convert.ToDateTime(Request.QueryString["StartDate"].ToString()).ToString("MM/dd/yyyy") + " To " + Convert.ToDateTime(Request.QueryString["EndDate"].ToString()).ToString("MM/dd/yyyy");
                titleTextSchool.Text = Session["SchoolName"].ToString().ToUpper();

                if (String.IsNullOrEmpty(Request.QueryString["EndDate"]) && !String.IsNullOrEmpty(Request.QueryString["StartDate"]))
                    titleText.Text = "Staff Attendance Report From " + Convert.ToDateTime(Request.QueryString["StartDate"].ToString()).ToString("MM/dd/yyyy");
                titleTextSchool.Text = Session["SchoolName"].ToString().ToUpper();
                if (!String.IsNullOrEmpty(Request.QueryString["EndDate"]) && String.IsNullOrEmpty(Request.QueryString["StartDate"]))
                    titleText.Text = "Staff Attendance Report To " + Convert.ToDateTime(Request.QueryString["EndDate"].ToString()).ToString("MM/dd/yyyy");
                titleTextSchool.Text = Session["SchoolName"].ToString().ToUpper();
                rpt1.SetDataSource(dsReport.Tables["dtAttendance"]);
            }

            if (Request.QueryString["RepId"] == "Student")
            {

                if (!String.IsNullOrEmpty(Session["Result"].ToString()))
                {
                    rpt.FileName = Server.MapPath("rptStudentAttendanceReport.rpt");
                    DayCareBAL.StaffService proxyAttendance = new DayCareBAL.StaffService();
                    string ReportFor = "";
                    ReportFor = "Student";
                    string SearchText = "";
                    string SearchStr = "";
                    string SchoolYear = "";
                    if (!String.IsNullOrEmpty(Request.QueryString["StartDate"]) && String.IsNullOrEmpty(Request.QueryString["EndDate"]))
                    {
                        SearchText += " cah.checkincheckoutdatetime>='" + Request.QueryString["StartDate"].ToString() + "'";
                        // SchoolYear += "and csy.SchoolYearId='" + new Guid(Session["CurrentSchoolYearId"].ToString()) + "'";
                        if (Session["Result"] != null)
                        {
                            SearchStr += "Name in(" + Session["Result"].ToString() + ")";
                        }
                    }

                    else if (String.IsNullOrEmpty(Request.QueryString["StartDate"]) && !String.IsNullOrEmpty(Request.QueryString["EndDate"]))
                    {
                        SearchText += " cah.checkincheckoutdatetime<='" + Request.QueryString["EndDate"].ToString().Replace("00:00:00 PM", "11:59:58") + "'";
                        // SchoolYear += "and csy.SchoolYearId='" + new Guid(Session["CurrentSchoolYearId"].ToString()) + "'";
                        if (Session["Result"] != null)
                        {
                            SearchStr += "Name in(" + Session["Result"].ToString() + ")";
                        }
                    }
                    else if (!String.IsNullOrEmpty(Request.QueryString["EndDate"]) && !String.IsNullOrEmpty(Request.QueryString["StartDate"]))
                    {
                        SearchText += " cah.checkincheckoutdatetime>='" + Request.QueryString["StartDate"].ToString() + "' and cah.checkincheckoutdatetime<='" + Request.QueryString["EndDate"].ToString().Replace("00:00:00", "11:59:58") + "'";
                        //SchoolYear += "and csy.SchoolYearId='" + new Guid(Session["CurrentSchoolYearId"].ToString()) + "'";
                        if (Session["Result"] != null)
                        {
                            SearchStr += "Name in(" + Session["Result"].ToString() + ")";
                        }
                    }
                    if (String.IsNullOrEmpty(Request.QueryString["EndDate"]) && String.IsNullOrEmpty(Request.QueryString["StartDate"]) && !String.IsNullOrEmpty(Session["Result"].ToString()))
                    {
                        SearchStr += "Name in(" + Session["Result"].ToString() + ")";
                    }

                    SearchStr = Session["Result"].ToString().Replace("'--Select All--',", "");

                    string[] str = SearchStr.Remove(SearchStr.Length - 1).Remove(0,1).Replace("','", "$").Split('$');

                    string strFinalSearhString = "";

                    foreach (string s in str)
                    {
                        string s1 = "";
                        s1 = s.Replace("'", "''");
                        s1 = "'" + s1 + "',";
                        strFinalSearhString += s1;
                    }
                    strFinalSearhString = "Name in(" + strFinalSearhString.Remove(strFinalSearhString.Length-1) + ")";
                    ds = proxyAttendance.LoadChildAttendanceHistory(SearchText, strFinalSearhString, new Guid(Session["CurrentSchoolYearId"].ToString()));
                    if(ds.Tables.Count>0)
                    dsReport.Tables["dtAttendance"].Merge(ds.Tables[0]);

                }
                else
                {
                    rpt.FileName = Server.MapPath("rptStudentAttendanceReport.rpt");
                    DayCareBAL.StaffService proxyAttendance = new DayCareBAL.StaffService();
                    string ReportFor = "";
                    ReportFor = "Student";
                    string SearchText = "";
                    string SearchStr = "";
                    string SchoolYear = "";
                    if (!String.IsNullOrEmpty(Request.QueryString["StartDate"]) && String.IsNullOrEmpty(Request.QueryString["EndDate"]))
                    {
                        SearchText += " cah.checkincheckoutdatetime>='" + Request.QueryString["StartDate"].ToString() + "'";
                        // SchoolYear += "and csy.SchoolYearId='" + new Guid(Session["CurrentSchoolYearId"].ToString()) + "'";
                    }

                    else if (String.IsNullOrEmpty(Request.QueryString["StartDate"]) && !String.IsNullOrEmpty(Request.QueryString["EndDate"]))
                    {
                        SearchText += " cah.checkincheckoutdatetime<='" + Request.QueryString["EndDate"].ToString().Replace("00:00:00 PM", "11:59:58") + "'";
                        // SchoolYear += "and csy.SchoolYearId='" + new Guid(Session["CurrentSchoolYearId"].ToString()) + "'";
                    }
                    else if (!String.IsNullOrEmpty(Request.QueryString["EndDate"]) && !String.IsNullOrEmpty(Request.QueryString["StartDate"]))
                    {
                        SearchText += " cah.checkincheckoutdatetime>='" + Request.QueryString["StartDate"].ToString() + "' and cah.checkincheckoutdatetime<='" + Request.QueryString["EndDate"].ToString().Replace("00:00:00", "11:59:58") + "'";
                        //SchoolYear += "and csy.SchoolYearId='" + new Guid(Session["CurrentSchoolYearId"].ToString()) + "'";
                    }

                    ds = proxyAttendance.LoadChildAttendanceHistory(SearchText, SearchStr, new Guid(Session["CurrentSchoolYearId"].ToString()));
                    dsReport.Tables["dtAttendance"].Merge(ds.Tables[0]);
                }
                CrystalDecisions.CrystalReports.Engine.TextObject titleText = (CrystalDecisions.CrystalReports.Engine.TextObject)rpt1.ReportDefinition.ReportObjects["Text7"];
                CrystalDecisions.CrystalReports.Engine.TextObject titleTextSchool = (CrystalDecisions.CrystalReports.Engine.TextObject)rpt1.ReportDefinition.ReportObjects["Text4"];
                CrystalDecisions.CrystalReports.Engine.TextObject footer = (CrystalDecisions.CrystalReports.Engine.TextObject)rpt1.ReportDefinition.ReportObjects["txtfooter"];
                footer.Text = Common.GetSchoolWiseAddress(new Guid(Session["SchoolId"].ToString()));
                if (!String.IsNullOrEmpty(Request.QueryString["StartDate"]) && !String.IsNullOrEmpty(Request.QueryString["EndDate"]))
                    titleText.Text = "Student Attendance Report From " + Convert.ToDateTime(Request.QueryString["StartDate"].ToString()).ToString("MM/dd/yyyy") + " To " + Convert.ToDateTime(Request.QueryString["EndDate"].ToString()).ToString("MM/dd/yyyy");
                titleTextSchool.Text = Session["SchoolName"].ToString().ToUpper();
                if (String.IsNullOrEmpty(Request.QueryString["EndDate"]) && !String.IsNullOrEmpty(Request.QueryString["StartDate"]))
                    titleText.Text = "Student Attendance Report From " + Convert.ToDateTime(Request.QueryString["StartDate"].ToString()).ToString("MM/dd/yyyy");
                titleTextSchool.Text = Session["SchoolName"].ToString().ToUpper();
                if (!String.IsNullOrEmpty(Request.QueryString["EndDate"]) && String.IsNullOrEmpty(Request.QueryString["StartDate"]))
                    titleText.Text = "Student Attendance Report To " + Convert.ToDateTime(Request.QueryString["EndDate"].ToString()).ToString("MM/dd/yyyy");
                titleTextSchool.Text = Session["SchoolName"].ToString().ToUpper();

                rpt1.SetDataSource(dsReport.Tables["dtAttendance"]);
            }

            if (Request.QueryString["RepId"] == "StudentDate")
            {
                rpt.FileName = Server.MapPath("rptStudentAttendanceByDate.rpt");
                DayCareBAL.StaffService proxyAttendance = new DayCareBAL.StaffService();
                string ReportFor = "";
                ReportFor = "Student";
                string SearchText = "";
                string SearchStr = "";
                string SchoolYear = "";
                if (!String.IsNullOrEmpty(Request.QueryString["StartDate"]) && String.IsNullOrEmpty(Request.QueryString["EndDate"]))
                {
                    SearchText += " cah.checkincheckoutdatetime>='" + Request.QueryString["StartDate"].ToString() + "'";
                    // SchoolYear += "and csy.SchoolYearId='" + new Guid(Session["CurrentSchoolYearId"].ToString()) + "'";
                }

                else if (String.IsNullOrEmpty(Request.QueryString["StartDate"]) && !String.IsNullOrEmpty(Request.QueryString["EndDate"]))
                {
                    SearchText += " cah.checkincheckoutdatetime<='" + Request.QueryString["EndDate"].ToString().Replace("00:00:00 PM", "11:59:58") + "'";
                    // SchoolYear += "and csy.SchoolYearId='" + new Guid(Session["CurrentSchoolYearId"].ToString()) + "'";
                }
                else if (!String.IsNullOrEmpty(Request.QueryString["EndDate"]) && !String.IsNullOrEmpty(Request.QueryString["StartDate"]))
                {
                    SearchText += " cah.checkincheckoutdatetime>='" + Request.QueryString["StartDate"].ToString() + "' and cah.checkincheckoutdatetime<='" + Request.QueryString["EndDate"].ToString().Replace("00:00:00", "11:59:58") + "'";
                    //SchoolYear += "and csy.SchoolYearId='" + new Guid(Session["CurrentSchoolYearId"].ToString()) + "'";
                }

                ds = proxyAttendance.LoadChildAttendanceHistory(SearchText, SearchStr, new Guid(Session["CurrentSchoolYearId"].ToString()));
                dsReport.Tables["dtAttendance"].Merge(ds.Tables[0]);

                CrystalDecisions.CrystalReports.Engine.TextObject titleText = (CrystalDecisions.CrystalReports.Engine.TextObject)rpt1.ReportDefinition.ReportObjects["Text2"];
                CrystalDecisions.CrystalReports.Engine.TextObject titleTextSchool = (CrystalDecisions.CrystalReports.Engine.TextObject)rpt1.ReportDefinition.ReportObjects["Text8"];
                CrystalDecisions.CrystalReports.Engine.TextObject footer = (CrystalDecisions.CrystalReports.Engine.TextObject)rpt1.ReportDefinition.ReportObjects["txtfooter"];
                footer.Text = Common.GetSchoolWiseAddress(new Guid(Session["SchoolId"].ToString()));
                if (!String.IsNullOrEmpty(Request.QueryString["StartDate"]) && !String.IsNullOrEmpty(Request.QueryString["EndDate"]))
                    titleText.Text = "Student Attendance Report From " + Convert.ToDateTime(Request.QueryString["StartDate"].ToString()).ToString("MM/dd/yyyy") + " To " + Convert.ToDateTime(Request.QueryString["EndDate"].ToString()).ToString("MM/dd/yyyy");
                titleTextSchool.Text = Session["SchoolName"].ToString().ToUpper();
                if (String.IsNullOrEmpty(Request.QueryString["EndDate"]) && !String.IsNullOrEmpty(Request.QueryString["StartDate"]))
                    titleText.Text = "Student Attendance Report From " + Convert.ToDateTime(Request.QueryString["StartDate"].ToString()).ToString("MM/dd/yyyy");
                titleTextSchool.Text = Session["SchoolName"].ToString().ToUpper();
                if (!String.IsNullOrEmpty(Request.QueryString["EndDate"]) && String.IsNullOrEmpty(Request.QueryString["StartDate"]))
                    titleText.Text = "Student Attendance Report To " + Convert.ToDateTime(Request.QueryString["EndDate"].ToString()).ToString("MM/dd/yyyy");
                titleTextSchool.Text = Session["SchoolName"].ToString().ToUpper();
                rpt1.SetDataSource(dsReport.Tables["dtAttendance"]);
            }


            if (Request.QueryString["RepId"] == "StudentClass")
            {
                rpt.FileName = Server.MapPath("rptStudentAttendanceByClassName.rpt");
                DayCareBAL.StaffService proxyAttendance = new DayCareBAL.StaffService();
                string ReportFor = "";
                ReportFor = "Student";
                string SearchText = "";
                string SearchStr = "";
                string SchoolYear = "";
                if (!String.IsNullOrEmpty(Request.QueryString["StartDate"]) && String.IsNullOrEmpty(Request.QueryString["EndDate"]))
                {
                    SearchText += " cah.checkincheckoutdatetime>='" + Request.QueryString["StartDate"].ToString() + "'";
                    // SchoolYear += "and csy.SchoolYearId='" + new Guid(Session["CurrentSchoolYearId"].ToString()) + "'";
                }

                else if (String.IsNullOrEmpty(Request.QueryString["StartDate"]) && !String.IsNullOrEmpty(Request.QueryString["EndDate"]))
                {
                    SearchText += " cah.checkincheckoutdatetime<='" + Request.QueryString["EndDate"].ToString().Replace("00:00:00 PM", "11:59:58") + "'";
                    // SchoolYear += "and csy.SchoolYearId='" + new Guid(Session["CurrentSchoolYearId"].ToString()) + "'";
                }
                else if (!String.IsNullOrEmpty(Request.QueryString["EndDate"]) && !String.IsNullOrEmpty(Request.QueryString["StartDate"]))
                {
                    SearchText += " cah.checkincheckoutdatetime>='" + Request.QueryString["StartDate"].ToString() + "' and cah.checkincheckoutdatetime<='" + Request.QueryString["EndDate"].ToString().Replace("00:00:00", "11:59:58") + "'";
                    //SchoolYear += "and csy.SchoolYearId='" + new Guid(Session["CurrentSchoolYearId"].ToString()) + "'";
                }

                ds = proxyAttendance.LoadChildAttendanceHistory(SearchText, SearchStr, new Guid(Session["CurrentSchoolYearId"].ToString()));
                dsReport.Tables["dtAttendance"].Merge(ds.Tables[0]);

                CrystalDecisions.CrystalReports.Engine.TextObject titleText = (CrystalDecisions.CrystalReports.Engine.TextObject)rpt1.ReportDefinition.ReportObjects["Text2"];
                CrystalDecisions.CrystalReports.Engine.TextObject titleTextSchool = (CrystalDecisions.CrystalReports.Engine.TextObject)rpt1.ReportDefinition.ReportObjects["Text9"];
                CrystalDecisions.CrystalReports.Engine.TextObject footer = (CrystalDecisions.CrystalReports.Engine.TextObject)rpt1.ReportDefinition.ReportObjects["txtfooter"];
                footer.Text = Common.GetSchoolWiseAddress(new Guid(Session["SchoolId"].ToString()));
                if (!String.IsNullOrEmpty(Request.QueryString["StartDate"]) && !String.IsNullOrEmpty(Request.QueryString["EndDate"]))
                    titleText.Text = "Student Attendance Report From " + Convert.ToDateTime(Request.QueryString["StartDate"].ToString()).ToString("MM/dd/yyyy") + " To " + Convert.ToDateTime(Request.QueryString["EndDate"].ToString()).ToString("MM/dd/yyyy");
                titleTextSchool.Text = Session["SchoolName"].ToString().ToUpper();
                if (String.IsNullOrEmpty(Request.QueryString["EndDate"]) && !String.IsNullOrEmpty(Request.QueryString["StartDate"]))
                    titleText.Text = "Student Attendance Report From " + Convert.ToDateTime(Request.QueryString["StartDate"].ToString()).ToString("MM/dd/yyyy");
                titleTextSchool.Text = Session["SchoolName"].ToString().ToUpper();
                if (!String.IsNullOrEmpty(Request.QueryString["EndDate"]) && String.IsNullOrEmpty(Request.QueryString["StartDate"]))
                    titleText.Text = "Student Attendance Report To " + Convert.ToDateTime(Request.QueryString["EndDate"].ToString()).ToString("MM/dd/yyyy");
                titleTextSchool.Text = Session["SchoolName"].ToString().ToUpper();

                rpt1.SetDataSource(dsReport.Tables["dtAttendance"]);
            }


            if (Request.QueryString["RepId"] == "StaffDate")
            {
                rpt.FileName = Server.MapPath("rptStaffAttendanceByDate.rpt");
                DayCareBAL.StaffService proxyAttendance = new DayCareBAL.StaffService();

                string ReportFor = "";
                ReportFor = "Staff";
                string SearchText = "";
                string SearchStr = "";
                string SchoolYear = "";
                if (!String.IsNullOrEmpty(Request.QueryString["StartDate"]) && String.IsNullOrEmpty(Request.QueryString["EndDate"]))
                {
                    SearchText += " sah.checkincheckoutdatetime>='" + Request.QueryString["StartDate"].ToString() + "'";
                    // SchoolYear += "and ssy.SchoolYearId='" + new Guid(Session["CurrentSchoolYearId"].ToString()) + "'";
                    //
                }

                else if (String.IsNullOrEmpty(Request.QueryString["StartDate"]) && !String.IsNullOrEmpty(Request.QueryString["EndDate"]))
                {
                    SearchText += " sah.checkincheckoutdatetime<='" + Request.QueryString["EndDate"].ToString().Replace("00:00:00 PM", "11:59:58") + "'";
                    // SchoolYear += "and ssy.SchoolYearId='" + new Guid(Session["CurrentSchoolYearId"].ToString()) + "'";
                }
                else if (!String.IsNullOrEmpty(Request.QueryString["EndDate"]) && !String.IsNullOrEmpty(Request.QueryString["StartDate"]))
                {
                    SearchText += " sah.checkincheckoutdatetime>='" + Request.QueryString["StartDate"].ToString() + "' and sah.checkincheckoutdatetime<='" + Request.QueryString["EndDate"].ToString().Replace("00:00:00", "11:59:58") + "'";
                    //SchoolYear += "and ssy.SchoolYearId='" + new Guid(Session["CurrentSchoolYearId"].ToString()) + "'";
                }

                ds = proxyAttendance.LoadAttendanceHistory1(SearchText, SearchStr, new Guid(Session["CurrentSchoolYearId"].ToString()));
                dsReport.Tables["dtAttendance"].Merge(ds.Tables[0]);

                CrystalDecisions.CrystalReports.Engine.TextObject titleText = (CrystalDecisions.CrystalReports.Engine.TextObject)rpt1.ReportDefinition.ReportObjects["Text7"];
                CrystalDecisions.CrystalReports.Engine.TextObject titleTextSchool = (CrystalDecisions.CrystalReports.Engine.TextObject)rpt1.ReportDefinition.ReportObjects["Text2"];
                CrystalDecisions.CrystalReports.Engine.TextObject footer = (CrystalDecisions.CrystalReports.Engine.TextObject)rpt1.ReportDefinition.ReportObjects["txtfooter"];
                footer.Text = Common.GetSchoolWiseAddress(new Guid(Session["SchoolId"].ToString()));
                if (!String.IsNullOrEmpty(Request.QueryString["StartDate"]) && !String.IsNullOrEmpty(Request.QueryString["EndDate"]))
                    titleText.Text = "Staff Attendance Report From " + Convert.ToDateTime(Request.QueryString["StartDate"].ToString()).ToString("MM/dd/yyyy") + " To " + Convert.ToDateTime(Request.QueryString["EndDate"].ToString()).ToString("MM/dd/yyyy");
                titleTextSchool.Text = Session["SchoolName"].ToString().ToUpper();
                if (String.IsNullOrEmpty(Request.QueryString["EndDate"]) && !String.IsNullOrEmpty(Request.QueryString["StartDate"]))
                    titleText.Text = "Staff Attendance Report From " + Convert.ToDateTime(Request.QueryString["StartDate"].ToString()).ToString("MM/dd/yyyy");
                titleTextSchool.Text = Session["SchoolName"].ToString().ToUpper();
                if (!String.IsNullOrEmpty(Request.QueryString["EndDate"]) && String.IsNullOrEmpty(Request.QueryString["StartDate"]))
                    titleText.Text = "Staff Attendance Report To " + Convert.ToDateTime(Request.QueryString["EndDate"].ToString()).ToString("MM/dd/yyyy");
                titleTextSchool.Text = Session["SchoolName"].ToString().ToUpper();

                rpt1.SetDataSource(dsReport.Tables["dtAttendance"]);


            }
            crp.DisplayGroupTree = false;
            crp.ReportSource = rpt1;
            crp.RefreshReport();

            crp.DataBind();
            #endregion
        }
    }
}
