using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace DayCare
{
    public partial class LateFee : System.Web.UI.Page
    {
        RadAjaxManager MasterAjaxManager;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["SchoolId"] == null || Session["CurrentSchoolYearId"] == null)
            {
                lblscript.Text = "<script>CloseOnReload()</" + "script>";
            }
            if (!IsPostBack)
            {
                try
                {
                    if (!string.IsNullOrEmpty(Request.QueryString["ChildFamilyId"]))
                    {
                        ViewState["ChildFamilyId"] = Request.QueryString["ChildFamilyId"];
                        GetChildFamilyDetails(new Guid(ViewState["ChildFamilyId"].ToString()));
                    }
                }
                catch (Exception ex)
                {
                    DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.LateFee, "Page_Load", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                }
            }
            this.Form.DefaultButton = btnSave.UniqueID;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                DayCareBAL.LedgerOfFamilyService proxyLedger = new DayCareBAL.LedgerOfFamilyService();
                DayCarePL.LedgerProperties objLedger = new DayCarePL.LedgerProperties();
                if (Session["CurrentSchoolYearId"] == null)
                    return;

                objLedger.SchoolYearId = new Guid(Session["CurrentSchoolYearId"].ToString());
                if (ViewState["ChildFamilyId"] != null)
                {
                    objLedger.ChildFamilyId = new Guid(ViewState["ChildFamilyId"].ToString());
                }
                objLedger.TransactionDate = DateTime.Now;
                objLedger.Debit = Convert.ToDecimal(txtLateFee.Text.Trim());
                objLedger.Credit = 0;
                objLedger.Comment = "Late Fee Charged";
                objLedger.LateFee = 1;
                objLedger.CreatedDateTime = DateTime.Now;
                objLedger.LastModifiedDatetime = DateTime.Now;
                if (Session["StaffId"] != null)
                {
                    objLedger.CreatedById = new Guid(Session["StaffId"].ToString());
                    objLedger.LastModifiedById = new Guid(Session["StaffId"].ToString());
                }
                if (proxyLedger.Save(objLedger))
                {
                    lblscript.Text = "<script>CloseOnReload()</" + "script>";
                    //lblscript.Text = "<script>ParentReload()</" + "script>";
                    
                }
                else
                {
                    lblscript.Text = "<script>Error()</" + "script>";

                    return;
                }
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.LateFee, "btnSave_Click", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
            }
        }

        public void GetChildFamilyDetails(Guid ChildFammilyId)
        {
            DayCareBAL.LedgerOfFamilyService ProxyLedger = new DayCareBAL.LedgerOfFamilyService();
            DayCarePL.LedgerProperties objLedger = ProxyLedger.LoadChildFamilyWiseTranDateAmount(ChildFammilyId);
            if (objLedger != null)
            {
                lblMessage.Text = "Late fee charged on " + objLedger.TransactionDate.ToShortDateString() + ", $" + objLedger.Debit;
            }
            else
            {
                lblMessage.Text = "No late fee charge found";
            }
        }
    }
}
