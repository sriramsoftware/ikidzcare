using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//crystal report usefull namespaces
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions;
using CrystalDecisions.Web;
using CrystalDecisions.Shared;
using System.Configuration;
using System.Data;
//end crystal report support namespaces

namespace DayCare.Report
{
    public partial class rptClassRoomWiseStudentAttendSchool : System.Web.UI.Page
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

           // crp.AfterRender += this.crp_AfterRender;

            if (!String.IsNullOrEmpty(Request.QueryString["ClassRoomId"]) && !String.IsNullOrEmpty(Request.QueryString["RepId"]))
            {
                if (Convert.ToString(Request.QueryString["RepId"]).Equals("ClassWiseStudent"))
                    rpt.FileName = Server.MapPath("rptClassroomWiseStudent.rpt");
                if (Convert.ToString(Request.QueryString["RepId"]).Equals("ClassWiseStudentWithFee"))
                    rpt.FileName = Server.MapPath("rptClassroomWiseStudentWithFee.rpt");
                DayCareBAL.ClassRoomService proxy = new DayCareBAL.ClassRoomService();
                CrystalDecisions.CrystalReports.Engine.TextObject titleText = (CrystalDecisions.CrystalReports.Engine.TextObject)rpt1.ReportDefinition.ReportObjects["Text3"];
                CrystalDecisions.CrystalReports.Engine.TextObject titleTextSchool = (CrystalDecisions.CrystalReports.Engine.TextObject)rpt1.ReportDefinition.ReportObjects["Text1"];
                CrystalDecisions.CrystalReports.Engine.TextObject footer = (CrystalDecisions.CrystalReports.Engine.TextObject)rpt1.ReportDefinition.ReportObjects["txtfooter"];
                footer.Text = Common.GetSchoolWiseAddress(new Guid(Session["SchoolId"].ToString()));
                //CrystalDecisions.CrystalReports.Engine.TextObject titleText4 = (CrystalDecisions.CrystalReports.Engine.TextObject)rpt1.ReportDefinition.ReportObjects["Text4"];
                //titleText4.Text = ViewState["title"].ToString();
                //titleText.Text = "Class room wise student list report";

                if (Convert.ToString(Request.QueryString["RepId"]).Equals("ClassWiseStudent"))
                    titleText.Text = "Student list by class report";
                if (Convert.ToString(Request.QueryString["RepId"]).Equals("ClassWiseStudentWithFee"))
                    titleText.Text = "Student fees by class report";
                titleTextSchool.Text = Session["SchoolName"].ToString().ToUpper();
                ds = proxy.GetClassroomWiseStudentWeeklySchedule(new Guid(Request.QueryString["ClassRoomId"].ToString()), new Guid(Session["CurrentSchoolYearId"].ToString()));
                dsReport.Tables["dtClassWiseStudent"].Merge(ds.Tables[0]);
                rpt1.SetDataSource(dsReport.Tables["dtClassWiseStudent"]);
            }
            if (!String.IsNullOrEmpty(Request.QueryString["ProgramId"]) && !String.IsNullOrEmpty(Request.QueryString["RepId"]) )
            {
                if(Convert.ToString(Request.QueryString["RepId"]).Equals("ProgramWiseStudent"))
                    rpt.FileName = Server.MapPath("rptProgramWiseStudentWeeklySchedule.rpt");
                if (Convert.ToString(Request.QueryString["RepId"]).Equals("ProgramWiseStudentWithFee"))
                    rpt.FileName = Server.MapPath("rptProgramWiseStudentWeeklyScheduleWithFee.rpt");
                DayCareBAL.SchoolProgramService proxy = new DayCareBAL.SchoolProgramService();
                CrystalDecisions.CrystalReports.Engine.TextObject titleText = (CrystalDecisions.CrystalReports.Engine.TextObject)rpt1.ReportDefinition.ReportObjects["Text13"];
                CrystalDecisions.CrystalReports.Engine.TextObject titleTextSchool = (CrystalDecisions.CrystalReports.Engine.TextObject)rpt1.ReportDefinition.ReportObjects["Text10"];
                CrystalDecisions.CrystalReports.Engine.TextObject footer = (CrystalDecisions.CrystalReports.Engine.TextObject)rpt1.ReportDefinition.ReportObjects["txtfooter"];
                footer.Text = Common.GetSchoolWiseAddress(new Guid(Session["SchoolId"].ToString()));
               // titleText.Text = "Program wise student list report";

                if (Convert.ToString(Request.QueryString["RepId"]).Equals("ProgramWiseStudent"))
                    titleText.Text = "Student list by Program report";
                if (Convert.ToString(Request.QueryString["RepId"]).Equals("ProgramWiseStudentWithFee"))
                    titleText.Text = "Student fees by Program report";

                titleTextSchool.Text = Session["SchoolName"].ToString().ToUpper();
                ds = proxy.GetSchoolProgramWiseStudentWeeklySchedule(new Guid(Session["CurrentSchoolYearId"].ToString()), new Guid(Request.QueryString["ProgramId"].ToString()));
                dsReport.Tables["dtProgramWiseStudent"].Merge(ds.Tables[0]);
                rpt1.SetDataSource(dsReport.Tables["dtProgramWiseStudent"]);
            }



            crp.DisplayGroupTree = false;
            crp.ReportSource = rpt1;
            crp.RefreshReport();

            crp.DataBind();
        }

        //protected void crp_AfterRender(object sender, EventArgs e)
        //{
        //    CrystalDecisions.Web.ViewInfo v = this.crp.ViewInfo;

        //    int totalPages = v.LastPageNumber;
        //    int currentPage = v.PageNumber;
        //    CrystalDecisions.CrystalReports.Engine.TextObject titleText4 = (CrystalDecisions.CrystalReports.Engine.TextObject)rpt1.ReportDefinition.ReportObjects["Text4"];
        //    titleText4.Text ="Page"+ currentPage + "Of" + totalPages;
        //    ViewState["title"] = "Page" + currentPage + "Of" + totalPages;
        //}
    }
  
}
