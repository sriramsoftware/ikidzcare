using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

namespace DayCare.Report
{
    public partial class viewstudentschedule : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void btnSubmit_Click(object sender, EventArgs e)
        {
            StringBuilder jscript = new StringBuilder();
            jscript.Append("<script>window.open('");
            jscript.Append("rptStudentScheduleReport.aspx?lastnamefrom=" + txtLastNameFrom.Text + "&lastnameto=" + txtLastNameTo.Text);
            jscript.Append("');</script>");
            Page.RegisterStartupScript("OpenWindows", jscript.ToString());

           // Response.Redirect("rptStudentScheduleReport.aspx?lastnamefrom=" + txtLastNameFrom.Text + "&lastnameto=" + txtLastNameTo.Text);
        }
    }
}
