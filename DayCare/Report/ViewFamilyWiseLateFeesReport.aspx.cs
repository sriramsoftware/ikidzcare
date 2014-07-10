using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

namespace DayCare.Report
{
    public partial class ViewFamilyWiseLateFeesReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["SchoolId"] == null || Session["CurrentSchoolYearId"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            if (!Page.IsPostBack)
            {
                Common.BindFamiliesWithChild(ddlFamilies, new Guid(Session["SchoolId"].ToString()), new Guid(Session["CurrentSchoolYearId"].ToString()));
                rdpStartDate.SelectedDate = DateTime.Now;
                rdpEndDate.SelectedDate = DateTime.Now;
            }
            this.Form.DefaultButton = btnSubmit.UniqueID;
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (Session["SchoolId"] == null || Session["CurrentSchoolYearId"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            string StartDate = string.Empty;
            string EndDate = string.Empty;
            if (rdpStartDate.SelectedDate != null)
            {
                StartDate = Convert.ToDateTime(rdpStartDate.SelectedDate.ToString()).ToString("yyyy/MM/dd HH:mm:ss") + " AM";
            }
            if (rdpEndDate.SelectedDate != null)
            {
                EndDate = Convert.ToDateTime(rdpEndDate.SelectedDate.ToString()).ToString("yyyy/MM/dd HH:mm:ss") + " AM";
            }
            StringBuilder jscript = new StringBuilder();
            jscript.Append("<script>window.open('");
            jscript.Append("rptFamilyWiseLateFeesReport.aspx?ChildFamilyId=" + ddlFamilies.SelectedValue + "&StartDate=" + StartDate + "&EndDate=" + EndDate);
            jscript.Append("');</script>");
            Page.RegisterStartupScript("OpenWindows", jscript.ToString());
        }
    }
}
