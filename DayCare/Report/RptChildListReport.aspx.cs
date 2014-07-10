using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions;
using CrystalDecisions.Web;
using CrystalDecisions.Shared;
namespace DayCare.Report
{
    public partial class RptChildListReport : System.Web.UI.Page
    {
        CrystalDecisions.Web.Report rpt = new CrystalDecisions.Web.Report();
        CrystalDecisions.CrystalReports.Engine.ReportDocument rpt1;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Page_Init(object sender, EventArgs e)
        {
            string SearchStr = string.Empty;
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
                if (Session["ChildList"] != null)
                {
                    //rpt.FileName = Server.MapPath("rptFamilyChildListReport.rpt"); 
                    rpt.FileName = Server.MapPath("rptChildDataListReport.rpt");
                    DayCareBAL.ChildListService proxy = new DayCareBAL.ChildListService();

                    SearchStr += Session["ChildList"].ToString();
                    SearchStr = Session["ChildList"].ToString().Replace("'00000000-0000-0000-0000-000000000000',", "");
                    CrystalDecisions.CrystalReports.Engine.TextObject titleTextSchool = (CrystalDecisions.CrystalReports.Engine.TextObject)rpt1.ReportDefinition.ReportObjects["Text1"];
                    CrystalDecisions.CrystalReports.Engine.TextObject footer = (CrystalDecisions.CrystalReports.Engine.TextObject)rpt1.ReportDefinition.ReportObjects["txtfooter"];
                    footer.Text = Common.GetSchoolWiseAddress(new Guid(Session["SchoolId"].ToString()));
                    titleTextSchool.Text = Session["SchoolName"].ToString().ToUpper();
                    //ds = proxy.GetAccountReceiable(new Guid(Session["CurrentSchoolYearId"].ToString(), new Guid(Request.QueryString["StartDate"].ToString())));
                    ds = proxy.GetChildList(new Guid(Session["SchoolId"].ToString()), new Guid(Session["CurrentSchoolYearId"].ToString()), SearchStr);
                    dsReport.Tables["dtChildDataList"].Merge(ds.Tables[0]);
                    rpt1.SetDataSource(dsReport.Tables["dtChildDataList"]);
                }
                crp.DisplayGroupTree = false;
                crp.ReportSource = rpt1;
                crp.RefreshReport();
                crp.DataBind();
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.Ledger, "rptChildDataListReport.rpt Page_Load", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
            }
        }
    }
}
