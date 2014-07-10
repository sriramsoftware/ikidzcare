using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace DayCare.Report
{
    public partial class RptFamilyChildListReport : System.Web.UI.Page
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
                if (Session["FamilyTitle"] != null)
                {
                    //rpt.FileName = Server.MapPath("rptFamilyChildListReport.rpt"); 
                    rpt.FileName = Server.MapPath("rptFamilyChildList1Report.rpt");
                    DayCareBAL.LedgerService proxy = new DayCareBAL.LedgerService();

                    SearchStr += Session["FamilyTitle"].ToString();
                    SearchStr = Session["FamilyTitle"].ToString().Replace("'00000000-0000-0000-0000-000000000000',", "");
                    //string[] str = SearchStr.Remove(SearchStr.Length - 1).Remove(0, 1).Replace("','", "$").Split('$');

                    //string strFinalSearhString = "";

                    //foreach (string s in str)
                    //{
                    //    string s1 = "";
                    //    s1 = s.Replace("'", "");
                    //    s1 = "''" + s1 + "'',";
                    //    strFinalSearhString += s1;
                    //}
                    //strFinalSearhString = strFinalSearhString.Remove(strFinalSearhString.Length - 1);
                    //strFinalSearhString = "'" + strFinalSearhString + "'";

                    CrystalDecisions.CrystalReports.Engine.TextObject titleTextSchool = (CrystalDecisions.CrystalReports.Engine.TextObject)rpt1.ReportDefinition.ReportObjects["Text1"];
                    CrystalDecisions.CrystalReports.Engine.TextObject footer = (CrystalDecisions.CrystalReports.Engine.TextObject)rpt1.ReportDefinition.ReportObjects["txtfooter"];
                    footer.Text = Common.GetSchoolWiseAddress(new Guid(Session["SchoolId"].ToString()));
                    titleTextSchool.Text = Session["SchoolName"].ToString().ToUpper();
                    //ds = proxy.GetAccountReceiable(new Guid(Session["CurrentSchoolYearId"].ToString(), new Guid(Request.QueryString["StartDate"].ToString())));
                    ds = proxy.GetFamilyChildListReport(new Guid(Session["SchoolId"].ToString()), new Guid(Session["CurrentSchoolYearId"].ToString()), SearchStr);
                    dsReport.Tables["dtFamilyChildList"].Merge(ds.Tables[0]);
                    rpt1.SetDataSource(dsReport.Tables["dtFamilyChildList"]);
                }
                crp.DisplayGroupTree = false;
                crp.ReportSource = rpt1;
                crp.RefreshReport();
                crp.DataBind();
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.Ledger, "rptFamilyChildListReport Page_Load", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
            }
        }
    }
}
