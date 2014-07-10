using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Text;

namespace DayCare.Report
{
    public partial class ViewLedgerReport : System.Web.UI.Page
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
                //rdpStartDate.SelectedDate = DateTime.Now;

                DayCareBAL.LedgerOfFamilyService proxyGetLastUpdatedLedgerDate = new DayCareBAL.LedgerOfFamilyService();
                DayCarePL.LedgerProperties objLedgerProperties = new DayCarePL.LedgerProperties();
                objLedgerProperties.LastModifiedDatetime = proxyGetLastUpdatedLedgerDate.GetLastUpdateLedgerDate().LastModifiedDatetime;
                //lblLastUpdatedLedger.Text += "The ledger was last updated on " + Convert.ToString(objLedgerProperties.LastModifiedDatetime.ToString("dd-MMMM-yyyy"));
                lblLastUpdatedLedger.Text = String.Format(lblLastUpdatedLedger.Text, Convert.ToString(objLedgerProperties.LastModifiedDatetime.ToString("dd-MMMM-yyyy hh:mm tt")));

                Fillddlfamily();
            }
         

        }

        protected void btnUpdateLedger_OnClick(object sender, EventArgs e)
        {
            GetChildProgEnrollmentFeeDetail(new Guid(Session["CurrentSchoolYearId"].ToString()));
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
        //    DateTime? StartDate = rdpStartDate.SelectedDate;
        //    DateTime? EndDate = rdpEndDate.SelectedDate;
        //    if (StartDate == null)
        //    {
        //        StartDate = DateTime.Now.Date;
        //        EndDate = DateTime.Now.Date;
        //    }
        //    StringBuilder jscript = new StringBuilder();
        //    jscript.Append("<script>window.open('");
        //    jscript.Append("RptLedgerOfFamilyReport.aspx?StartDate=" + StartDate + "&EndDate=" + EndDate + "&ChildFamilyID=" + ddlFamily.SelectedValue);
        //    jscript.Append("');</script>");
        //    Page.RegisterStartupScript("OpenWindows", jscript.ToString());
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
                else
                {
                    StringBuilder jscript = new StringBuilder();
                    jscript.Append("<script>window.open('");
                    jscript.Append("RptLedgerOfFamilyReport.aspx?StartDate=" + dt + "&EndDate=" + dtEndDate + "&ChildFamilyId=" + ddlFamily.SelectedValue);
                    jscript.Append("');</script>");
                    Page.RegisterStartupScript("OpenWindows", jscript.ToString());
                }
            }
            if (dtEndDate == null && dt == null)
            {
                StringBuilder jscript = new StringBuilder();
                jscript.Append("<script>window.open('");
                jscript.Append("RptLedgerOfFamilyReport.aspx?StartDate=" + dt + "&EndDate=" + dtEndDate + "&ChildFamilyId=" + ddlFamily.SelectedValue);
                jscript.Append("');</script>");
                Page.RegisterStartupScript("OpenWindows", jscript.ToString());
            }
        }

        protected void Fillddlfamily()
        {
            DayCareBAL.LedgerOfFamilyService proxyChildFamily = new DayCareBAL.LedgerOfFamilyService();
            List<DayCarePL.ChildFamilyProperties> lstFamily = new List<DayCarePL.ChildFamilyProperties>();
            lstFamily = proxyChildFamily.LoadChildFamily(new Guid(Session["SchoolId"].ToString()), new Guid(Session["CurrentSchoolYearId"].ToString()));
            if (lstFamily.Count > 0)
            {
                ddlFamily.DataSource = lstFamily;
                ddlFamily.DataTextField = "FamilyTitle";
                ddlFamily.DataValueField = "Id";
                ddlFamily.DataBind();
                ddlFamily.Items.Insert(0, new ListItem("--Select--", DayCarePL.Common.GUID_DEFAULT));
            }
            

        }

        #region "private Method Ledger Function"
        public void GetChildProgEnrollmentFeeDetail(Guid SchoolYearId)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.Ledger, "GetChildProgEnrollmentFeeDetail", "GetChildProgEnrollmentFeeDetail method called", DayCarePL.Common.GUID_DEFAULT);
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.Ledger, "GetChildProgEnrollmentFeeDetail", "Debug GetChildProgEnrollmentFeeDetail called", DayCarePL.Common.GUID_DEFAULT);
                DayCareBAL.LedgerService proxyLedger = new DayCareBAL.LedgerService();
                List<DayCarePL.ChildProgEnrollmentProperties> lstChildProgEnrollmentFeeDetail = proxyLedger.GetChildProgEnrollmentFeeDetail(SchoolYearId, new Guid(DayCarePL.Common.GUID_DEFAULT));
                if (lstChildProgEnrollmentFeeDetail != null)
                {
                    bool result = false;
                    List<DayCarePL.LedgerProperties> lstChildEnrollForLedger = new List<DayCarePL.LedgerProperties>();
                    DayCarePL.LedgerProperties objChildEnrollForLedger = null;
                    foreach (DayCarePL.ChildProgEnrollmentProperties objChildProgEnrollmentFeeDetail in lstChildProgEnrollmentFeeDetail)
                    {
                        string strDay = "";
                        DateTime StartDate = DateTime.Now, EndDate = DateTime.Now;
                        if (objChildProgEnrollmentFeeDetail.StartDate != null)
                        {
                            StartDate = objChildProgEnrollmentFeeDetail.StartDate.Value;
                        }
                        if (objChildProgEnrollmentFeeDetail.EndDate != null)
                        {
                            EndDate = objChildProgEnrollmentFeeDetail.EndDate.Value;
                        }
                        objChildEnrollForLedger = new DayCarePL.LedgerProperties();

                        #region Weekly
                        switch (objChildProgEnrollmentFeeDetail.EffectiveWeekDay)
                        {
                            case 1:
                                {
                                    strDay = "Monday";
                                    break;
                                }
                            case 2:
                                {
                                    strDay = "Tuesday";
                                    break;
                                }
                            case 3:
                                {
                                    strDay = "Wednesday";
                                    break;
                                }
                            case 4:
                                {
                                    strDay = "Thursday";
                                    break;
                                }
                            case 5:
                                {
                                    strDay = "Friday";
                                    break;
                                }
                        }
                        if (objChildProgEnrollmentFeeDetail.EffectiveWeekDay != null && objChildProgEnrollmentFeeDetail.EffectiveWeekDay != 0)
                        {
                            DateTime? OldDate = proxyLedger.GetLastTransactionDate(SchoolYearId, objChildProgEnrollmentFeeDetail.ChildFamilyId, objChildProgEnrollmentFeeDetail.ChildDataId, objChildProgEnrollmentFeeDetail.SchoolProgramId);
                            if (OldDate != null)
                            {
                                DateTime LastDate = OldDate.Value;
                                if (LastDate.Equals(new DateTime()))
                                {
                                    LastDate = StartDate;
                                }
                                //else
                                //{
                                //    LastDate = LastDate.AddDays(7);
                                //}
                                while (LastDate.Date <= DateTime.Now.Date && LastDate.Date <= EndDate.Date)
                                {
                                    if (LastDate.DayOfWeek.ToString().ToLower().Equals(strDay.ToLower()))
                                    {
                                        break;
                                    }
                                    LastDate = LastDate.AddDays(1);
                                }
                                DateTime TranDate;
                                if (!LastDate.Equals(new DateTime()))
                                {
                                    while (LastDate.Date <= DateTime.Now.Date && LastDate.Date <= EndDate.Date)
                                    {
                                        DateTime.TryParse(LastDate.Month + "/" + LastDate.Day + "/" + LastDate.Year, out TranDate);
                                        if (TranDate.Equals(new DateTime()))
                                        {
                                            TranDate = new DateTime(LastDate.Year, LastDate.Month, System.DateTime.DaysInMonth(LastDate.Year, LastDate.Month));
                                        }
                                        if (!TranDate.Equals(new DateTime()) && TranDate.Date <= System.DateTime.Now.Date && TranDate.Date <= EndDate.Date && !OldDate.Value.Date.Equals(LastDate.Date) && TranDate.Date >= StartDate.Date)
                                        {
                                            objChildEnrollForLedger = new DayCarePL.LedgerProperties();
                                            objChildEnrollForLedger.TransactionDate = TranDate + DateTime.Now.TimeOfDay;
                                            objChildEnrollForLedger.Comment = objChildProgEnrollmentFeeDetail.ProgramTitle + " " + objChildProgEnrollmentFeeDetail.FeesPeriodName;// +" " + strDay;
                                            //objChildEnrollForLedger.Detail = objChildProgEnrollmentFeeDetail.ProgramTitle + " " + objChildProgEnrollmentFeeDetail.FeesPeriodName + " " + strDay;
                                            SetLegderProperties(SchoolYearId, lstChildEnrollForLedger, objChildEnrollForLedger, objChildProgEnrollmentFeeDetail);
                                        }
                                        LastDate = TranDate.AddDays(7);
                                    }
                                }
                            }
                        }
                        #endregion

                        #region Monthly
                        if (objChildProgEnrollmentFeeDetail.EffectiveMonthDay != null)// && objChildProgEnrollmentFeeDetail.EffectiveMonthDay.Equals(DateTime.Now.Month))
                        {
                            DateTime? OldDate = proxyLedger.GetLastTransactionDate(SchoolYearId, objChildProgEnrollmentFeeDetail.ChildFamilyId, objChildProgEnrollmentFeeDetail.ChildDataId, objChildProgEnrollmentFeeDetail.SchoolProgramId);
                            if (OldDate != null)
                            {
                                DateTime LastDate = OldDate.Value;
                                if (LastDate.Equals(new DateTime()))
                                {
                                    LastDate = StartDate;
                                }
                                //else
                                //{
                                //    LastDate = LastDate.AddMonths(1);
                                //}
                                DateTime TranDate;
                                if (!LastDate.Equals(new DateTime()))
                                {
                                    while (LastDate.Date <= DateTime.Now.Date && LastDate.Date <= EndDate.Date)
                                    {
                                        DateTime.TryParse(LastDate.Month + "/" + objChildProgEnrollmentFeeDetail.EffectiveMonthDay.Value + "/" + LastDate.Year, out TranDate);
                                        if (TranDate.Equals(new DateTime()))
                                        {
                                            TranDate = new DateTime(LastDate.Year, LastDate.Month, System.DateTime.DaysInMonth(LastDate.Year, LastDate.Month));
                                        }
                                        if (!TranDate.Equals(new DateTime()) && TranDate.Date <= System.DateTime.Now.Date && TranDate.Date <= EndDate.Date && !OldDate.Value.Date.Equals(LastDate.Date) && TranDate.Date >= StartDate.Date)
                                        {
                                            objChildEnrollForLedger = new DayCarePL.LedgerProperties();
                                            objChildEnrollForLedger.TransactionDate = TranDate + DateTime.Now.TimeOfDay;
                                            objChildEnrollForLedger.Comment = objChildProgEnrollmentFeeDetail.ProgramTitle + " " + objChildProgEnrollmentFeeDetail.FeesPeriodName;//  + " " + objChildEnrollForLedger.TransactionDate.ToShortDateString();
                                            //objChildEnrollForLedger.Detail = objChildProgEnrollmentFeeDetail.ProgramTitle + " " + objChildProgEnrollmentFeeDetail.FeesPeriodName + " " + objChildEnrollForLedger.TransactionDate.ToShortDateString();
                                            SetLegderProperties(SchoolYearId, lstChildEnrollForLedger, objChildEnrollForLedger, objChildProgEnrollmentFeeDetail);
                                        }
                                        LastDate = TranDate.AddMonths(1);
                                    }
                                }
                            }
                        }
                        #endregion

                        #region Yearly
                        if (objChildProgEnrollmentFeeDetail.EffectiveYearDate != null)// && objChildProgEnrollmentFeeDetail.EffectiveYearDate.Value.
                        {
                            DateTime? OldDate = proxyLedger.GetLastTransactionDate(SchoolYearId, objChildProgEnrollmentFeeDetail.ChildFamilyId, objChildProgEnrollmentFeeDetail.ChildDataId, objChildProgEnrollmentFeeDetail.SchoolProgramId);
                            if (OldDate != null)
                            {
                                DateTime LastDate = OldDate.Value;
                                if (LastDate.Equals(new DateTime()))
                                {
                                    LastDate = StartDate;
                                }
                                //else
                                //{
                                //    LastDate = LastDate.AddYears(1);
                                //}
                                DateTime TranDate;
                                if (!LastDate.Equals(new DateTime()))
                                {
                                    while (LastDate.Date <= System.DateTime.Now.Date && LastDate.Date <= EndDate.Date)
                                    {
                                        DateTime.TryParse(objChildProgEnrollmentFeeDetail.EffectiveYearDate.Value.Month + "/" + objChildProgEnrollmentFeeDetail.EffectiveYearDate.Value.Day + "/" + LastDate.Year, out TranDate);
                                        if (!TranDate.Equals(new DateTime()) && TranDate.Date <= System.DateTime.Now.Date && TranDate.Date <= EndDate.Date && !OldDate.Value.Date.Equals(LastDate.Date) && TranDate.Date >= StartDate.Date)
                                        {
                                            objChildEnrollForLedger = new DayCarePL.LedgerProperties();
                                            objChildEnrollForLedger.TransactionDate = TranDate + DateTime.Now.TimeOfDay;
                                            objChildEnrollForLedger.Comment = objChildProgEnrollmentFeeDetail.ProgramTitle + " " + objChildProgEnrollmentFeeDetail.FeesPeriodName;//  + " " + objChildEnrollForLedger.TransactionDate.ToShortDateString();
                                            //objChildEnrollForLedger.Detail = objChildProgEnrollmentFeeDetail.ProgramTitle + " " + objChildProgEnrollmentFeeDetail.FeesPeriodName + " " + objChildEnrollForLedger.TransactionDate.ToShortDateString();
                                            SetLegderProperties(SchoolYearId, lstChildEnrollForLedger, objChildEnrollForLedger, objChildProgEnrollmentFeeDetail);
                                        }
                                        LastDate = TranDate.AddYears(1);
                                    }
                                }
                            }
                        }
                        #endregion

                        #region One Time
                        if (objChildProgEnrollmentFeeDetail.EffectiveMonthDay == null && (objChildProgEnrollmentFeeDetail.EffectiveWeekDay == null || objChildProgEnrollmentFeeDetail.EffectiveWeekDay == 0) && objChildProgEnrollmentFeeDetail.EffectiveYearDate == null)
                        {
                            DateTime? OldDate = proxyLedger.GetLastTransactionDate(SchoolYearId, objChildProgEnrollmentFeeDetail.ChildFamilyId, objChildProgEnrollmentFeeDetail.ChildDataId, objChildProgEnrollmentFeeDetail.SchoolProgramId);
                            if (OldDate != null)
                            {
                                DateTime LastDate = OldDate.Value;
                                if (LastDate.Equals(new DateTime()))
                                {
                                    LastDate = StartDate;
                                }
                                //else
                                //{
                                //    LastDate = LastDate.AddMonths(1);
                                //}
                                if (!LastDate.Equals(new DateTime()))
                                {
                                    if (LastDate.Date <= DateTime.Now.Date)
                                    {
                                        if (OldDate.Value.Equals(new DateTime()))
                                        {
                                            objChildEnrollForLedger = new DayCarePL.LedgerProperties();
                                            objChildEnrollForLedger.TransactionDate = LastDate.Date + DateTime.Now.TimeOfDay;
                                            objChildEnrollForLedger.Comment = objChildProgEnrollmentFeeDetail.ProgramTitle + " " + objChildProgEnrollmentFeeDetail.FeesPeriodName;//  + " " + objChildEnrollForLedger.TransactionDate.ToShortDateString();
                                            //objChildEnrollForLedger.Detail = objChildProgEnrollmentFeeDetail.ProgramTitle + " " + objChildProgEnrollmentFeeDetail.FeesPeriodName + " " + objChildEnrollForLedger.TransactionDate.ToShortDateString();
                                            SetLegderProperties(SchoolYearId, lstChildEnrollForLedger, objChildEnrollForLedger, objChildProgEnrollmentFeeDetail);
                                        }
                                    }
                                }
                            }
                        }
                        #endregion
                    }
                    if (lstChildEnrollForLedger != null && lstChildEnrollForLedger.Count > 0)
                        result = proxyLedger.Save(lstChildEnrollForLedger);
                }
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.LedgerOfFamily, "GetChildProgEnrollmentFeeDetail", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
            }
        }

        private void SetLegderProperties(Guid SchoolYearId, List<DayCarePL.LedgerProperties> lstChildEnrollForLedger, DayCarePL.LedgerProperties objChildEnrollForLedger, DayCarePL.ChildProgEnrollmentProperties objChildProgEnrollmentFeeDetail)
        {
            objChildEnrollForLedger.ChildFamilyId = objChildProgEnrollmentFeeDetail.ChildFamilyId;
            objChildEnrollForLedger.SchoolYearId = SchoolYearId;
            objChildEnrollForLedger.ChildDataId = objChildProgEnrollmentFeeDetail.ChildDataId;
            objChildEnrollForLedger.Debit = objChildProgEnrollmentFeeDetail.Fees.Value;
            objChildEnrollForLedger.Credit = 0;
            objChildEnrollForLedger.AllowEdit = false;
            objChildEnrollForLedger.PaymentId = null;
            objChildEnrollForLedger.CreatedById = new Guid(Session["StaffId"].ToString());
            objChildEnrollForLedger.CreatedDateTime = DateTime.Now;
            objChildEnrollForLedger.LastModifiedById = new Guid(Session["StaffId"].ToString());
            objChildEnrollForLedger.LastModifiedDatetime = DateTime.Now;
            objChildEnrollForLedger.SchoolProgramId = objChildProgEnrollmentFeeDetail.SchoolProgramId;
            lstChildEnrollForLedger.Add(objChildEnrollForLedger);
        }
        #endregion
    }
}
