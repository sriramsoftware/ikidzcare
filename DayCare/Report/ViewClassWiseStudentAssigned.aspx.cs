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
    public partial class ViewClassWiseStudentAssigned : System.Web.UI.Page
    {
        RadAjaxManager MasterAjaxManager;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["SchoolId"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            if (!Page.IsPostBack)
            {
                Common.BindClassRoomReport(ddlClassRoom, new Guid(Session["SchoolId"].ToString()), new Guid(Session["CurrentSchoolYearId"].ToString()));
                Common.BindSchoolProgram(ddlProgram, new Guid(Session["CurrentSchoolYearId"].ToString()));
            }
            this.Form.DefaultButton = btnSubmit.UniqueID;
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (ddlReportType.SelectedIndex > 0)
            {
                if (ddlReportType.SelectedValue.Equals("ClassWiseStudent"))
                {
                    StringBuilder jscript = new StringBuilder();
                    jscript.Append("<script>window.open('");
                    jscript.Append("rptClassRoomWiseStudentAttendSchool.aspx?RepId=" + ddlReportType.SelectedValue + "&ClassRoomId=" + ddlClassRoom.SelectedItem.Value);
                    jscript.Append("');</script>");
                    Page.RegisterStartupScript("OpenWindows", jscript.ToString());
                }
                if (ddlReportType.SelectedValue.Equals("ClassWiseStudentWithFee"))
                {
                    StringBuilder jscript = new StringBuilder();
                    jscript.Append("<script>window.open('");
                    jscript.Append("rptClassRoomWiseStudentAttendSchool.aspx?RepId=" + ddlReportType.SelectedValue + "&ClassRoomId=" + ddlClassRoom.SelectedItem.Value);
                    jscript.Append("');</script>");
                    Page.RegisterStartupScript("OpenWindows", jscript.ToString());
                }
                if (ddlReportType.SelectedValue.Equals("ProgramWiseStudent"))
                {
                    StringBuilder jscript = new StringBuilder();
                    jscript.Append("<script>window.open('");
                    jscript.Append("rptClassRoomWiseStudentAttendSchool.aspx?RepId=" + ddlReportType.SelectedValue + "&ProgramId=" + ddlProgram.SelectedItem.Value );
                    jscript.Append("');</script>");
                    Page.RegisterStartupScript("OpenWindows", jscript.ToString());
                }
                if (ddlReportType.SelectedValue.Equals("ProgramWiseStudentWithFee"))
                {
                    StringBuilder jscript = new StringBuilder();
                    jscript.Append("<script>window.open('");
                    jscript.Append("rptClassRoomWiseStudentAttendSchool.aspx?RepId=" + ddlReportType.SelectedValue + "&ProgramId=" + ddlProgram.SelectedItem.Value);
                    jscript.Append("');</script>");
                    Page.RegisterStartupScript("OpenWindows", jscript.ToString());
                }
            }
        }


    }
}
