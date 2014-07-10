using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace DayCare.Report
{
    public partial class rptFamilyWiseLateFeesReport : System.Web.UI.Page
    {
        CrystalDecisions.Web.Report rpt = new CrystalDecisions.Web.Report();
        CrystalDecisions.CrystalReports.Engine.ReportDocument rpt1;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Page_Init(object sender, EventArgs e)
        {
            try
            {
                if (Session["SchoolId"] == null || Session["CurrentSchoolYearId"] == null)
                {
                    Response.Redirect("~/Login.aspx");
                }
                crdata.Report = rpt;
                rpt1 = crdata.ReportDocument;
                string SearchText = "";
                Guid ChildFamilyId = new Guid();
                DataSet dsReport = new xmlClassWiseStudentAttendTime();
                DataSet ds = new DataSet();

                if (!String.IsNullOrEmpty(Request.QueryString["ChildFamilyId"]))
                {
                    ChildFamilyId = new Guid(Request.QueryString["ChildFamilyId"].ToString());
                }
                if (!String.IsNullOrEmpty(Request.QueryString["StartDate"]) && String.IsNullOrEmpty(Request.QueryString["EndDate"]))
                {
                    SearchText += " transactiondate>='" + Request.QueryString["StartDate"].ToString() + "'";

                }
                if (String.IsNullOrEmpty(Request.QueryString["StartDate"]) && !String.IsNullOrEmpty(Request.QueryString["EndDate"]))
                {
                    SearchText += " transactiondate<='" + Request.QueryString["EndDate"].ToString().Replace("00:00:00 AM", "11:59:58 PM") + " '";

                }
                else if (!String.IsNullOrEmpty(Request.QueryString["EndDate"]) && !String.IsNullOrEmpty(Request.QueryString["StartDate"]))
                {
                    SearchText += "  transactiondate>='" + Request.QueryString["StartDate"].ToString() + "' and transactiondate<='" + Request.QueryString["EndDate"].ToString().Replace("00:00:00 AM", "11:59:58 PM") + "'";

                }

                rpt.FileName = Server.MapPath("rptLateFeesReport.rpt");
                DayCareBAL.LedgerService proxy = new DayCareBAL.LedgerService();
                string StartDate = string.Empty;
                string EndDate = string.Empty;
                if (Request.QueryString["StartDate"] !="")
                {
                    StartDate = Convert.ToDateTime(Request.QueryString["StartDate"].ToString()).ToString("MM/dd/yyyy");
                }
                if (Request.QueryString["EndDate"] != "")
                {
                    EndDate = Convert.ToDateTime(Request.QueryString["EndDate"].ToString()).ToString("MM/dd/yyyy");
                }
                CrystalDecisions.CrystalReports.Engine.TextObject titleText = (CrystalDecisions.CrystalReports.Engine.TextObject)rpt1.ReportDefinition.ReportObjects["Text4"];
                CrystalDecisions.CrystalReports.Engine.TextObject titleTextSchool = (CrystalDecisions.CrystalReports.Engine.TextObject)rpt1.ReportDefinition.ReportObjects["Text3"];
                CrystalDecisions.CrystalReports.Engine.TextObject footer = (CrystalDecisions.CrystalReports.Engine.TextObject)rpt1.ReportDefinition.ReportObjects["txtfooter"];
                footer.Text = Common.GetSchoolWiseAddress(new Guid(Session["SchoolId"].ToString()));
                titleText.Text = "Late Fee Report From " + StartDate + " To " + EndDate;
                titleTextSchool.Text = Session["SchoolName"].ToString().ToUpper();
                ds = proxy.GetFamilyWiseLateFeesReport(new Guid(Session["SchoolId"].ToString()), new Guid(Session["CurrentSchoolYearId"].ToString()), ChildFamilyId, SearchText);
                dsReport.Tables["dtLateFees"].Merge(ds.Tables[0]);
                rpt1.SetDataSource(dsReport.Tables["dtLateFees"]);

                crp.DisplayGroupTree = false;
                crp.ReportSource = rpt1;
                crp.RefreshReport();

                crp.DataBind();
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.rptFamilyWiseLateFeesReport, "Page_Init", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
            }
        }


    }
}
