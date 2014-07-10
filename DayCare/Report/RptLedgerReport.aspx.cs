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
    public partial class RptLedgerReport : System.Web.UI.Page
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

            if (!string.IsNullOrEmpty(Request.QueryString["ChildFamilyID"]))
            {

            }

        }

    }

}
