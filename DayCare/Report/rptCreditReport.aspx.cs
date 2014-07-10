using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace DayCare.Report
{
    public partial class rptCreditReport : System.Web.UI.Page
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
                if (Session["CurrentSchoolYearId"] == null)
                {
                    Response.Redirect("~/Login.aspx");
                }

                crdata.Report = rpt;
                rpt1 = crdata.ReportDocument;

                DataSet dsReport = new xmlClassWiseStudentAttendTime();
                DataSet ds = new DataSet();

                if (!string.IsNullOrEmpty(Request.QueryString["StartDate"]))
                {
                    rpt.FileName = Server.MapPath("rptCreditsReport.rpt");
                    DayCareBAL.LedgerService proxy = new DayCareBAL.LedgerService();
                    //ds = proxy.GetAccountReceiable(new Guid(Session["CurrentSchoolYearId"].ToString(), new Guid(Request.QueryString["StartDate"].ToString())));
                    
                    CrystalDecisions.CrystalReports.Engine.TextObject titleTextSchool = (CrystalDecisions.CrystalReports.Engine.TextObject)rpt1.ReportDefinition.ReportObjects["Text5"];
                    CrystalDecisions.CrystalReports.Engine.TextObject footer = (CrystalDecisions.CrystalReports.Engine.TextObject)rpt1.ReportDefinition.ReportObjects["txtfooter"];
                    footer.Text = Common.GetSchoolWiseAddress(new Guid(Session["SchoolId"].ToString()));
                    titleTextSchool.Text = Session["SchoolName"].ToString().ToUpper();
                    ds = proxy.GetAccountReceiable(new Guid(Session["CurrentSchoolYearId"].ToString()), Convert.ToDateTime(Request.QueryString["StartDate"].ToString()), "Credit");
                    dsReport.Tables["dtAccountReceiable_Credit"].Merge(ds.Tables[0]);
                    rpt1.SetDataSource(dsReport.Tables["dtAccountReceiable_Credit"]);
                }

                crp.DisplayGroupTree = false;
                crp.ReportSource = rpt1;
                crp.RefreshReport();

                crp.DataBind();
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.Ledger, "rptAccountReciableReport Page_Load", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
            }
        }
    }
}
