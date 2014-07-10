using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using Telerik.Web.UI;

namespace DayCare.Report
{
    public partial class rptAttendance1 : System.Web.UI.Page
    {
        RadAjaxManager MasterAjaxManager;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["SchoolId"] == null || Session["CurrentSchoolYearId"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            if (!Page.IsPostBack)
            {
                rdpStartDate.SelectedDate = DateTime.Now;
                rdpEndDate.SelectedDate = DateTime.Now;
                rcbStudentList.Visible = false;
                FillCombo();
            }
            this.Form.DefaultButton = btnSave.UniqueID;

        }

        protected void FillCombo()
        {
            DayCareBAL.ChildAttendanceHistoryListService proxyChildList = new DayCareBAL.ChildAttendanceHistoryListService();
            DayCarePL.ChildDataProperties[] result = proxyChildList.GetChildList(new Guid(Session["CurrentSchoolYearId"].ToString())).ToArray();
            List<DayCarePL.ChildDataProperties> data = new List<DayCarePL.ChildDataProperties>();
            DayCarePL.ChildDataProperties Rec = new DayCarePL.ChildDataProperties();
            Rec.Id = new Guid("00000000-0000-0000-0000-000000000000");
            Rec.FullName = "--Select All--";
            foreach (DayCarePL.ChildDataProperties d in result)
            {
                data.Add(d);
            }
            data.Insert(0, Rec);
            rcbStudentList.Items.Clear();
            rcbStudentList.DataSource = data;
            rcbStudentList.DataTextField = "FullName";
            rcbStudentList.DataBind();
            rcbStudentList.EmptyMessage = "---Select---";
            //rcbStudentList.DataTextField = "FullName";
            //rcbStudentList.DataSource = proxyChildList.GetChildList(new Guid(Session["CurrentSchoolYearId"].ToString()));
            //rcbStudentList.DataBind();


        }
        protected void FillCoboofStaff()
        {
            DayCareBAL.StaffAttendanceHistoryListService proxystaff = new DayCareBAL.StaffAttendanceHistoryListService();
            DayCarePL.StaffProperties[] result = proxystaff.LoadStaffList(new Guid(Session["CurrentSchoolYearId"].ToString())).ToArray();
            List<DayCarePL.StaffProperties> data = new List<DayCarePL.StaffProperties>();
            DayCarePL.StaffProperties Rec = new DayCarePL.StaffProperties();
            Rec.Id = new Guid("00000000-0000-0000-0000-000000000000");
            Rec.FullName = "--Select All--";
            foreach (DayCarePL.StaffProperties d in result)
            {
                data.Add(d);
            }
            data.Insert(0, Rec);
            rcbStudentList.Items.Clear();
            rcbStudentList.DataSource = data;
            rcbStudentList.DataTextField = "FullName";
            rcbStudentList.DataBind();
            rcbStudentList.EmptyMessage = "---Select---";
        }
        protected void ddlReportFor_SelectedIndexChanged(object sender, EventArgs e)
        {
            rcbStudentList.Visible = false;
            if (ddlReportFor.SelectedValue.Equals("Student"))
            {
                rcbStudentList.Visible = true;
                rcbStudentList.DataSource = null;
                rcbStudentList.Text = "";
                FillCombo();
            }
            if (ddlReportFor.SelectedValue.Equals("Staff"))
            {
                rcbStudentList.Visible = true;
                rcbStudentList.DataSource = null;
                rcbStudentList.Text = "";
                FillCoboofStaff();
            }
        }
        protected void ddlReportFor_DataBound(object sender, EventArgs e)
        {


        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string data = string.Empty;
            string result = string.Empty;
            foreach (RadComboBoxItem item in rcbStudentList.Items)
            {
                CheckBox chkName = (CheckBox)item.FindControl("CheckBox");
                if (chkName.Checked == true)
                {
                    data = item.Text.Trim();
                    result += "'" + data + "',";

                }
            }
            if (result.Length > 0)
            {
                result = result.Substring(0, result.LastIndexOf(','));
            }
            if (ddlReportFor.SelectedIndex > 0)
            {

                if (ddlReportFor.SelectedValue.Equals("Staff"))
                {
                    string StartDate = string.Empty;
                    string EndDate = string.Empty;
                    if (rdpStartDate.SelectedDate != null)
                    {
                        StartDate = Convert.ToDateTime(rdpStartDate.SelectedDate.ToString()).ToString("yyyy/MM/dd HH:mm:ss") + " AM";
                    }
                    if (rdpEndDate.SelectedDate != null)
                    {
                        EndDate = Convert.ToDateTime(rdpEndDate.SelectedDate.ToString()).ToString("yyyy/MM/dd HH:mm:ss") + " PM";
                    }
                    Session["StaffResult"] = result;
                    StringBuilder jscript = new StringBuilder();
                    jscript.Append("<script>window.open('");
                    jscript.Append("ViewAttendanceReport.aspx?RepId=" + ddlReportFor.SelectedValue + "&StartDate=" + StartDate + "&EndDate=" + EndDate);
                    jscript.Append("');</script>");
                    Page.RegisterStartupScript("OpenWindows", jscript.ToString());
                }
                else if (ddlReportFor.SelectedValue.Equals("Student"))
                {
                    string StartDate = string.Empty;
                    string EndDate = string.Empty;
                    if (rdpStartDate.SelectedDate != null)
                    {
                        StartDate = Convert.ToDateTime(rdpStartDate.SelectedDate.ToString()).ToString("yyyy/MM/dd HH:mm:ss") + " AM"; 
                    }
                    if (rdpEndDate.SelectedDate != null)
                    {
                        EndDate = Convert.ToDateTime(rdpEndDate.SelectedDate.ToString()).ToString("yyyy/MM/dd HH:mm:ss") + " PM";
                    }
                    Session["Result"] = result;
                    StringBuilder jscript = new StringBuilder();
                    jscript.Append("<script>window.open('");
                    jscript.Append("ViewAttendanceReport.aspx?RepId=" + ddlReportFor.SelectedValue + "&StartDate=" + StartDate + "&EndDate=" + EndDate + "&SearchString=" + "");
                    jscript.Append("');</script>");
                    Page.RegisterStartupScript("OpenWindows", jscript.ToString());
                }
                if (ddlReportFor.SelectedValue.Equals("StaffDate"))
                {
                    string StartDate = string.Empty;
                    string EndDate = string.Empty;
                    if (rdpStartDate.SelectedDate != null)
                    {
                        StartDate = Convert.ToDateTime(rdpStartDate.SelectedDate.ToString()).ToString("yyyy/MM/dd HH:mm:ss") + " AM";
                    }
                    if (rdpEndDate.SelectedDate != null)
                    {
                        EndDate = Convert.ToDateTime(rdpEndDate.SelectedDate.ToString()).ToString("yyyy/MM/dd HH:mm:ss") + " PM";
                    }
                    StringBuilder jscript = new StringBuilder();
                    jscript.Append("<script>window.open('");
                    jscript.Append("ViewAttendanceReport.aspx?RepId=" + ddlReportFor.SelectedValue + "&StartDate=" + StartDate + "&EndDate=" + EndDate);
                    jscript.Append("');</script>");
                    Page.RegisterStartupScript("OpenWindows", jscript.ToString());
                }

                if (ddlReportFor.SelectedValue.Equals("StudentDate"))
                {
                    string StartDate = string.Empty;
                    string EndDate = string.Empty;
                    if (rdpStartDate.SelectedDate != null)
                    {
                        StartDate = Convert.ToDateTime(rdpStartDate.SelectedDate.ToString()).ToString("yyyy/MM/dd HH:mm:ss") + " AM"; 
                    }
                    if (rdpEndDate.SelectedDate != null)
                    {
                        EndDate = Convert.ToDateTime(rdpEndDate.SelectedDate.ToString()).ToString("yyyy/MM/dd HH:mm:ss") + " PM"; 
                    }
                    StringBuilder jscript = new StringBuilder();
                    jscript.Append("<script>window.open('");
                    jscript.Append("ViewAttendanceReport.aspx?RepId=" + ddlReportFor.SelectedValue + "&StartDate=" + StartDate + "&EndDate=" + EndDate);
                    jscript.Append("');</script>");
                    Page.RegisterStartupScript("OpenWindows", jscript.ToString());
                }
                if (ddlReportFor.SelectedValue.Equals("StudentClass"))
                {
                    string StartDate = string.Empty;
                    string EndDate = string.Empty;
                    if (rdpStartDate.SelectedDate != null)
                    {
                        StartDate = Convert.ToDateTime(rdpStartDate.SelectedDate.ToString()).ToString("yyyy/MM/dd HH:mm:ss") + " AM"; 
                    }
                    if (rdpEndDate.SelectedDate != null)
                    {
                        EndDate = Convert.ToDateTime(rdpEndDate.SelectedDate.ToString()).ToString("yyyy/MM/dd HH:mm:ss") + " PM"; 
                    }
                    StringBuilder jscript = new StringBuilder();
                    jscript.Append("<script>window.open('");
                    jscript.Append("ViewAttendanceReport.aspx?RepId=" + ddlReportFor.SelectedValue + "&StartDate=" + StartDate+ "&EndDate=" + EndDate);
                    jscript.Append("');</script>");
                    Page.RegisterStartupScript("OpenWindows", jscript.ToString());
                }
            }
        }

    }
}
