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
    public partial class rptPaymentDepositsReport : System.Web.UI.Page
    {
        CrystalDecisions.Web.Report rpt = new CrystalDecisions.Web.Report();
        CrystalDecisions.CrystalReports.Engine.ReportDocument rpt1;

        protected void Page_Load(object sender, EventArgs e)
        {
          
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["CurrentSchoolYearId"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            crdata.Report = rpt;
            rpt1 = crdata.ReportDocument;
            DataSet dsReport = new xmlClassWiseStudentAttendTime();
            DataSet ds = new DataSet();
            string SearchText = "";
            rpt.FileName = Server.MapPath("rptDeposit.rpt");
            DayCareBAL.FamilyPaymentService proxyPayment = new DayCareBAL.FamilyPaymentService();

            if (!String.IsNullOrEmpty(Request.QueryString["StartDate"]) && String.IsNullOrEmpty(Request.QueryString["EndDate"]))
            {
                SearchText += " l.transactiondate>='" + Request.QueryString["StartDate"].ToString() + "'";

            }
            if (String.IsNullOrEmpty(Request.QueryString["StartDate"]) && !String.IsNullOrEmpty(Request.QueryString["EndDate"]))
            {
                SearchText += " l.transactiondate<='" + Request.QueryString["EndDate"].ToString().Replace("00:00:00 AM", "11:59:58 PM") + "'";

            }
            else if (!String.IsNullOrEmpty(Request.QueryString["EndDate"]) && !String.IsNullOrEmpty(Request.QueryString["StartDate"])) 
            {
                SearchText += "  l.transactiondate>='" + Request.QueryString["StartDate"].ToString() + "' and l.transactiondate<='" + Request.QueryString["EndDate"].ToString().Replace("00:00:00 AM", "11:59:58 PM") + "'";

            }
            string StartDate = string.Empty;
            string EndDate = string.Empty;
            if (Request.QueryString["StartDate"] != "")
            {
                StartDate = Convert.ToDateTime(Request.QueryString["StartDate"].ToString()).ToString("MM/dd/yyyy");
            }
            if (Request.QueryString["EndDate"] != "")
            {
                EndDate=Convert.ToDateTime(Request.QueryString["EndDate"].ToString()).ToString("MM/dd/yyyy");
            }
            CrystalDecisions.CrystalReports.Engine.TextObject titleText = (CrystalDecisions.CrystalReports.Engine.TextObject)rpt1.ReportDefinition.ReportObjects["Text14"];
            CrystalDecisions.CrystalReports.Engine.TextObject titleTextSchool = (CrystalDecisions.CrystalReports.Engine.TextObject)rpt1.ReportDefinition.ReportObjects["Text4"];
            CrystalDecisions.CrystalReports.Engine.TextObject footer = (CrystalDecisions.CrystalReports.Engine.TextObject)rpt1.ReportDefinition.ReportObjects["txtfooter"];
            footer.Text = Common.GetSchoolWiseAddress(new Guid(Session["SchoolId"].ToString()));
            titleText.Text = "Deposit Report From " +StartDate+ " To " + EndDate;
            titleTextSchool.Text = Session["SchoolName"].ToString().ToUpper();
            ds = proxyPayment.LoadPaymentDeposits(SearchText, new Guid(Session["CurrentSchoolYearId"].ToString()));
            dsReport.Tables["dtDiposit"].Merge(ds.Tables[0]);
            rpt1.SetDataSource(dsReport.Tables["dtDiposit"]);

            crp.DisplayGroupTree = false;
            crp.ReportSource = rpt1;
            crp.RefreshReport();

            crp.DataBind();
        }
    }
}
