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
    public partial class ViewLedgerOfFamilyReport : System.Web.UI.Page
    {
        RadAjaxManager MasterAjaxManager;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["CurrentSchoolYearId"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }

            if (!Page.IsPostBack)
            {
                //rdpStartDate.SelectedDate = DateTime.Now;
                //rdpEndDate.SelectedDate = DateTime.Now;
              
            }
            this.Form.DefaultButton = btnSave.UniqueID;
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            DateTime? dt = rdpStartDate.SelectedDate;
            DateTime? dtEndDate = rdpEndDate.SelectedDate;



            if (dt == null && dtEndDate != null)
            {
                //dt = DateTime.Now.Date;
                MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Please select Start date", "false"));
                return;
            }
            if (dtEndDate == null && dt != null)
            {
                //dtEndDate = DateTime.Now.Date;
                MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Please select End date", "false"));
                return;
            }
            if (dt != null && dtEndDate != null)
            {
                if (dt > dtEndDate)
                {
                    //StringBuilder jscript = new StringBuilder();
                    //jscript.Append("<script>alert('Start date can not greater than End date')<script>");
                    //Page.RegisterStartupScript("OpenWindows", jscript.ToString());
                    MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                    MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Start date can not greater than End date", "false"));
                    return;
                }
            }
            if (!String.IsNullOrEmpty(Request.QueryString["ChildFamilyId"]))
            {
                StringBuilder jscript = new StringBuilder();
                jscript.Append("<script>window.open('");
                jscript.Append("RptLedgerOfFamilyReport.aspx?StartDate=" + dt + "&EndDate=" + dtEndDate + "&ChildFamilyId=" + Request.QueryString["ChildFamilyId"]);
                jscript.Append("');</script>");
                Page.RegisterStartupScript("OpenWindows", jscript.ToString());
            }

        }
    }
}
