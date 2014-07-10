using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace DayCare.Report
{
    public partial class rptStudentScheduleReport : System.Web.UI.Page
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

            String strLastNameFrom = Convert.ToString(Request.QueryString["lastnamefrom"]);
            String strLastNameTo = Convert.ToString(Request.QueryString["lastnameto"]);

            // crp.AfterRender += this.crp_AfterRender;


            rpt.FileName = Server.MapPath("rptStudentSchedule.rpt");
            DayCareBAL.ClassRoomService proxy = new DayCareBAL.ClassRoomService();
            CrystalDecisions.CrystalReports.Engine.TextObject titleText = (CrystalDecisions.CrystalReports.Engine.TextObject)rpt1.ReportDefinition.ReportObjects["Text3"];
            CrystalDecisions.CrystalReports.Engine.TextObject titleTextSchool = (CrystalDecisions.CrystalReports.Engine.TextObject)rpt1.ReportDefinition.ReportObjects["Text1"];
            CrystalDecisions.CrystalReports.Engine.TextObject footer = (CrystalDecisions.CrystalReports.Engine.TextObject)rpt1.ReportDefinition.ReportObjects["txtfooter"];
            footer.Text = Common.GetSchoolWiseAddress(new Guid(Session["SchoolId"].ToString()));
            //CrystalDecisions.CrystalReports.Engine.TextObject titleText4 = (CrystalDecisions.CrystalReports.Engine.TextObject)rpt1.ReportDefinition.ReportObjects["Text4"];
            //titleText4.Text = ViewState["title"].ToString();

            titleText.Text = "Student Schedule";
            titleTextSchool.Text = Session["SchoolName"].ToString().ToUpper();
            ds = proxy.GetStudentSchedule(new Guid(DayCarePL.Common.GUID_DEFAULT), new Guid(Session["CurrentSchoolYearId"].ToString()), strLastNameFrom, strLastNameTo);
            dsReport.Tables["dtClassWiseStudent"].Merge(ds.Tables[0]);
            rpt1.SetDataSource(dsReport.Tables["dtClassWiseStudent"]);

            crp.DisplayGroupTree = false;
            crp.ReportSource = rpt1;
            crp.RefreshReport();

            crp.DataBind();
        }

    }
}
