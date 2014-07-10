using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DayCare.UI
{
    public partial class AttendanceReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SetMenuLink();
            if (!Page.IsPostBack)
            {
                btnCSV.Visible = false;
            }
            this.Form.DefaultButton = btnSave.UniqueID;
        }

        public void rgAttendanceReport_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            BindGrid();
        }

        public void SetMenuLink()
        {
            try
            {
                List<DayCarePL.MenuLink> lstMenu = new List<DayCarePL.MenuLink>();
                DayCarePL.MenuLink objMenu;
                objMenu = new DayCarePL.MenuLink();
                objMenu.Name = "Attendance Report";
                objMenu.Url = "";
                lstMenu.Add(objMenu);
                usrMenuLink.SetMenuLink(lstMenu);
            }
            catch
            {

            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            rgAttendanceReport.Rebind();
            btnCSV.Visible = true;
        }

        private void BindGrid()
        {
            try
            {
                if (Session["CurrentSchoolYearId"] == null)
                {
                    Response.Redirect("~/Login.aspx");
                }
                DayCareBAL.StaffService proxyAttendance = new DayCareBAL.StaffService();

                string ReportFor = "";

                if (ddlReportFor.SelectedItem.Text.Equals("Staff"))
                {
                    ReportFor = "Staff";
                    string SearchText = "";
                    //SearchText = "ssy.schoolyearid='" + Session["CurrentSchoolYearId"].ToString() +"'";
                    if (rdpStartDate.SelectedDate != null)
                    {
                        SearchText += " sah.checkincheckoutdatetime>='" + rdpStartDate.SelectedDate.Value + "'";
                    }

                    else if (rdpEndDate.SelectedDate != null && rdpStartDate.SelectedDate != null)
                    {
                        SearchText += " AND sah.checkincheckoutdatetime<='" + rdpEndDate.SelectedDate.Value + "'";
                    }
                    else if (rdpEndDate.SelectedDate != null && rdpStartDate.SelectedDate == null)
                    {
                        SearchText += " sah.checkincheckoutdatetime<='" + rdpEndDate.SelectedDate.Value + "'";
                    }
                    rgAttendanceReport.DataSource = proxyAttendance.LoadAttendanceHistory(ReportFor, SearchText).Where(u => u.SchoolYearId.Equals(new Guid(Session["CurrentSchoolYearId"].ToString())));
                }
                else if (ddlReportFor.SelectedItem.Text.Equals("Student"))
                {
                    ReportFor = "Student";
                    string SearchText = "";
                    //SearchText = "csy.schoolyearid='" + Session["CurrentSchoolYearId"].ToString() + "'";
                    if (rdpStartDate.SelectedDate != null)
                    {
                        SearchText += " cah.checkincheckoutdatetime>='" + rdpStartDate.SelectedDate.Value + "'";
                    }
                    else if (rdpEndDate.SelectedDate != null && rdpStartDate.SelectedDate == null)
                    {
                        SearchText += " cah.checkincheckoutdatetime<='" + rdpEndDate.SelectedDate.Value + "'";
                    }
                    else if (rdpEndDate.SelectedDate != null && rdpStartDate.SelectedDate != null)
                    {
                        SearchText += " And cah.checkincheckoutdatetime<='" + rdpEndDate.SelectedDate.Value + "'";
                    }
                    rgAttendanceReport.DataSource = proxyAttendance.LoadAttendanceHistory(ReportFor, SearchText).Where(u => u.SchoolYearId.Equals(new Guid(Session["CurrentSchoolYearId"].ToString())));
                }
                else
                {
                    rgAttendanceReport.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.AttendenceReport, "BindGrid", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
            }
        }

        protected void btnPdf_Click(object sender, EventArgs e)
        {
            rgAttendanceReport.ExportSettings.ExportOnlyData = false;
            rgAttendanceReport.ExportSettings.IgnorePaging = true;
            rgAttendanceReport.ExportSettings.OpenInNewWindow = true;


            rgAttendanceReport.MasterTableView.ExportToPdf();

        }

        protected void btnCSV_Click(object sender, EventArgs e)
        {
            rgAttendanceReport.ExportSettings.ExportOnlyData = false;
            rgAttendanceReport.ExportSettings.IgnorePaging = true;
            rgAttendanceReport.ExportSettings.OpenInNewWindow = true;


            rgAttendanceReport.MasterTableView.ExportToCSV();

        }
    }
}
