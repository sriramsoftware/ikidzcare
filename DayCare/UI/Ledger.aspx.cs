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
    public partial class Ledger : System.Web.UI.Page
    {
        RadAjaxManager MasterAjaxManager;
        decimal Balance = 0;
        decimal YTDPay = 0;
        bool IsCurrentYear = true;
        List<DayCarePL.LedgerProperties> lstLeger = new List<DayCarePL.LedgerProperties>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["SchoolId"] == null || Session["CurrentSchoolYearId"] == null || string.IsNullOrEmpty(Request.QueryString["ChildFamilyId"]))
            {
                Response.Redirect("~/Login.aspx");
            }
            try
            {
                if (!IsPostBack)
                {
                    //
                    if (!string.IsNullOrEmpty(Request.QueryString["ChildFamilyId"]))
                    {
                        ViewState["ChildFamilyId"] = Request.QueryString["ChildFamilyId"].ToString();
                        SetMenuLink();
                        //GetChildProgEnrollmentFeeDetail(new Guid(Session["CurrentSchoolYearId"].ToString()));
                    }
                    Common.BindChargeCode(ddlCategory);

                }
                Guid SchoolId = new Guid();
                Guid CurrentSchoolYearId = new Guid();
                if (Session["SchoolId"] != null)
                {
                    SchoolId = new Guid(Session["SchoolId"].ToString());
                }

                if (Session["CurrentSchoolYearId"] != null)
                {
                    CurrentSchoolYearId = new Guid(Session["CurrentSchoolYearId"].ToString());
                }

                //if (!Common.IsCurrentYear(CurrentSchoolYearId, SchoolId))
                //{
                //    btnTopDeleteAll.Enabled = false;
                //    btnTopEditAll.Enabled = false;

                //    btnBottomDeleteAll.Enabled = false;
                //    btnBottomEditAll.Enabled = false;
                //    IsCurrentYear = false;
                //}
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.Ledger, "Page_Load", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
            }
        }

        public void GetChildProgEnrollmentFeeDetail(Guid SchoolYearId)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.Ledger, "GetChildProgEnrollmentFeeDetail", "GetChildProgEnrollmentFeeDetail method called", DayCarePL.Common.GUID_DEFAULT);
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.Ledger, "GetChildProgEnrollmentFeeDetail", "Debug GetChildProgEnrollmentFeeDetail called", DayCarePL.Common.GUID_DEFAULT);
                DayCareBAL.LedgerService proxyLedger = new DayCareBAL.LedgerService();
                List<DayCarePL.ChildProgEnrollmentProperties> lstChildProgEnrollmentFeeDetail = proxyLedger.GetChildProgEnrollmentFeeDetail(SchoolYearId, new Guid(ViewState["ChildFamilyId"].ToString()));
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
                                        if (!TranDate.Equals(new DateTime()) && TranDate.Date <= System.DateTime.Now.Date && TranDate.Date <= EndDate.Date && !OldDate.Value.Date.Equals(LastDate.Date))
                                        {
                                            objChildEnrollForLedger = new DayCarePL.LedgerProperties();
                                            objChildEnrollForLedger.TransactionDate = TranDate + DateTime.Now.TimeOfDay;
                                            objChildEnrollForLedger.Comment = objChildProgEnrollmentFeeDetail.ProgramTitle + " " + objChildProgEnrollmentFeeDetail.FeesPeriodName;//+ " " + strDay;
                                            SetLegderProperties(SchoolYearId, lstChildEnrollForLedger, objChildEnrollForLedger, objChildProgEnrollmentFeeDetail);
                                        }
                                        LastDate = LastDate.AddDays(7);
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
                                        if (!TranDate.Equals(new DateTime()) && TranDate.Date <= System.DateTime.Now.Date && TranDate.Date <= EndDate.Date && !OldDate.Value.Date.Equals(LastDate.Date))
                                        {
                                            objChildEnrollForLedger = new DayCarePL.LedgerProperties();
                                            objChildEnrollForLedger.TransactionDate = TranDate + DateTime.Now.TimeOfDay;
                                            objChildEnrollForLedger.Comment = objChildProgEnrollmentFeeDetail.ProgramTitle + " " + objChildProgEnrollmentFeeDetail.FeesPeriodName; //+ " " + objChildEnrollForLedger.TransactionDate.ToShortDateString();
                                            SetLegderProperties(SchoolYearId, lstChildEnrollForLedger, objChildEnrollForLedger, objChildProgEnrollmentFeeDetail);
                                        }
                                        LastDate = LastDate.AddMonths(1);
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
                                        if (!TranDate.Equals(new DateTime()) && TranDate.Date <= System.DateTime.Now.Date && TranDate.Date <= EndDate.Date && !OldDate.Value.Date.Equals(LastDate.Date))
                                        {
                                            objChildEnrollForLedger = new DayCarePL.LedgerProperties();
                                            objChildEnrollForLedger.TransactionDate = TranDate + DateTime.Now.TimeOfDay;
                                            objChildEnrollForLedger.Comment = objChildProgEnrollmentFeeDetail.ProgramTitle + " " + objChildProgEnrollmentFeeDetail.FeesPeriodName;// +" " + objChildEnrollForLedger.TransactionDate.ToShortDateString();
                                            SetLegderProperties(SchoolYearId, lstChildEnrollForLedger, objChildEnrollForLedger, objChildProgEnrollmentFeeDetail);
                                        }
                                        LastDate = LastDate.AddYears(1);
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
                                DateTime TranDate;
                                if (!LastDate.Equals(new DateTime()))
                                {
                                    if (LastDate.Date <= DateTime.Now.Date)
                                    {
                                        if (OldDate.Value.Equals(new DateTime()))
                                        {
                                            objChildEnrollForLedger = new DayCarePL.LedgerProperties();
                                            objChildEnrollForLedger.TransactionDate = LastDate.Date + DateTime.Now.TimeOfDay;
                                            objChildEnrollForLedger.Comment = objChildProgEnrollmentFeeDetail.ProgramTitle + " " + objChildProgEnrollmentFeeDetail.FeesPeriodName;// +" " + objChildEnrollForLedger.TransactionDate.ToShortDateString();
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
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.Ledger, "GetChildProgEnrollmentFeeDetail", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
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

        protected void rgLedger_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DayCareBAL.LedgerService proxyLedger = new DayCareBAL.LedgerService();
            lstLeger = proxyLedger.LoadLedgerDetail(new Guid(Session["CurrentSchoolYearId"].ToString()), new Guid(ViewState["ChildFamilyId"].ToString()));
            if (lstLeger != null)
            {
                rgLedger.DataSource = lstLeger;
                txtBalance.Text = (lstLeger.Sum(i => i.Debit) - lstLeger.Sum(i => i.Credit)).ToString();
                txtYTDPay.Text = lstLeger.Sum(i => i.Credit).ToString();
            }
            List<DayCarePL.ClosingBalance> lstOpeningBalance = proxyLedger.GetPreviousYearClosingBalance(new Guid(ViewState["ChildFamilyId"].ToString()), new Guid(Session["CurrentSchoolYearId"].ToString()));
            if (lstOpeningBalance != null)
            {
                lblOpeningBalance.Text = lstOpeningBalance.Sum(i => i.ClosingBalanceAmount).ToString();
                txtBalance.Text = (Convert.ToDecimal(txtBalance.Text) + Convert.ToDecimal(lblOpeningBalance.Text)).ToString();
                string str = "     Year          Op.bal.\n";
                if (lblOpeningBalance.Text != "0")
                {
                    foreach (DayCarePL.ClosingBalance objClosing in lstOpeningBalance)
                    {
                        str += objClosing.SchoolYear + " -> " + objClosing.ClosingBalanceAmount + "\n";
                    }
                    str = str + "\n\n";
                    Style st = new Style();

                    lblOpeningBalance.Style.Add("cursor", "pointer");
                    lblOpeningBalance.Font.Underline = true;
                    lblOpeningBalance.ToolTip = "click to show all year opening balance";
                    lblOpeningBalance.Attributes.Add("onclick", "javascript:ShowOpeningBalDiv();");
                    imgArrow.Attributes.Add("onclick", "javascript:HideOpeningBalDiv();");
                    OpBaldiv.Attributes.Add("onclick", "javascript:HideOpeningBalDiv();");
                    rptrOpeningBal.DataSource = lstOpeningBalance;
                    rptrOpeningBal.DataBind();
                }
            }
        }

        protected void rgLedger_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            Guid SchoolId = new Guid();
            Guid CurrentSchoolYearId = new Guid();
            if (Session["SchoolId"] != null)
            {
                SchoolId = new Guid(Session["SchoolId"].ToString());
            }

            if (Session["CurrentSchoolYearId"] != null)
            {
                CurrentSchoolYearId = new Guid(Session["CurrentSchoolYearId"].ToString());
            }

            //if (!Common.IsCurrentYear(CurrentSchoolYearId, SchoolId))
            //{
            //    if (e.CommandName == "InitInsert")
            //    {
            //        e.Canceled = true;
            //    }
            //    else if (e.CommandName == "Edit")
            //    {
            //        e.Canceled = true;
            //    }
            //    btnTopDeleteAll.Enabled = false;
            //    btnTopEditAll.Enabled = false;

            //    btnBottomDeleteAll.Enabled = false;
            //    btnBottomEditAll.Enabled = false;
            //    IsCurrentYear = false;
            //}
            //else
            //{
            if (e.CommandName == "InitInsert")
            {
                GridEditableColumn DeleteAll = e.Item.OwnerTableView.Columns[2] as GridEditableColumn;
                DeleteAll.Display = false;
                GridEditableColumn edSelect = e.Item.OwnerTableView.Columns[3] as GridEditableColumn;
                edSelect.Display = true;
                GridEditableColumn edSelect1 = e.Item.OwnerTableView.Columns[12] as GridEditableColumn;
                edSelect1.Display = false;
                rgLedger.MasterTableView.ClearEditItems();
            }
            if (e.CommandName == "Edit")
            {
                GridEditableColumn DeleteAll = e.Item.OwnerTableView.Columns[2] as GridEditableColumn;
                DeleteAll.Display = true;

                GridEditableColumn edSelect = e.Item.OwnerTableView.Columns[3] as GridEditableColumn;
                edSelect.Display = true;
                rgLedger.MasterTableView.IsItemInserted = false;
                GridEditableColumn edSelect1 = e.Item.OwnerTableView.Columns[12] as GridEditableColumn;
                edSelect1.Display = false;
            }
            if (e.CommandName == "Cancel")
            {
                GridEditableColumn DeleteAll = e.Item.OwnerTableView.Columns[2] as GridEditableColumn;
                DeleteAll.Display = true;
                GridEditableColumn edSelect = e.Item.OwnerTableView.Columns[3] as GridEditableColumn;
                edSelect.Display = false;
                GridEditableColumn edSelect1 = e.Item.OwnerTableView.Columns[12] as GridEditableColumn;
                edSelect1.Display = true;

            }
            //}
        }

        protected void rgLedger_EditCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {

        }

        #region "Created Code By Vimal"
        protected void rgLedger_InsertCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            bool isValid = SubmitRecord(source, e);
            if (isValid == false)
            {
                e.Canceled = true;
                GridEditableColumn DeleteAll = e.Item.OwnerTableView.Columns[2] as GridEditableColumn;
                DeleteAll.Display = false;
                GridEditableColumn edSelect = e.Item.OwnerTableView.Columns[3] as GridEditableColumn;
                edSelect.Display = true;
                GridEditableColumn edSelect1 = e.Item.OwnerTableView.Columns[12] as GridEditableColumn;
                edSelect1.Display = false;
            }
            else
            {
                GridEditableColumn DeleteAll = e.Item.OwnerTableView.Columns[2] as GridEditableColumn;
                DeleteAll.Display = true;
                GridEditableColumn edSelect = e.Item.OwnerTableView.Columns[3] as GridEditableColumn;
                edSelect.Display = false;
                GridEditableColumn edSelect1 = e.Item.OwnerTableView.Columns[12] as GridEditableColumn;
                edSelect1.Display = true;
            }
            rgLedger.MasterTableView.CurrentPageIndex = 0;
        }

        protected void rgLedger_UpdateCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {

            bool isvalid = SubmitRecord(source, e);
            if (isvalid == false)
            {
                e.Canceled = true;
                GridEditableColumn DeleteAll = e.Item.OwnerTableView.Columns[2] as GridEditableColumn;
                DeleteAll.Display = false;
                GridEditableColumn edSelect = e.Item.OwnerTableView.Columns[3] as GridEditableColumn;
                edSelect.Display = true;
                GridEditableColumn edSelect1 = e.Item.OwnerTableView.Columns[12] as GridEditableColumn;
                edSelect1.Display = false;
            }
            else
            {
                GridEditableColumn DeleteAll = e.Item.OwnerTableView.Columns[2] as GridEditableColumn;
                DeleteAll.Display = true;
                GridEditableColumn edSelect = e.Item.OwnerTableView.Columns[3] as GridEditableColumn;
                edSelect.Display = false;
                GridEditableColumn edSelect1 = e.Item.OwnerTableView.Columns[12] as GridEditableColumn;
                edSelect1.Display = true;
            }
            e.Item.Expanded = false;
            rgLedger.MasterTableView.Rebind();
            rgLedger.MasterTableView.CurrentPageIndex = 0;

        }

        public bool SubmitRecord(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clAbsentReason, "SubmitRecord", "Submit record method called", DayCarePL.Common.GUID_DEFAULT);
            bool result = false;
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clAbsentReason, "SubmitRecord", "Debug Submit record method called of AbsentReason", DayCarePL.Common.GUID_DEFAULT);
                DayCareBAL.LedgerService proxySave = new DayCareBAL.LedgerService();
                DayCarePL.LedgerProperties objLedger = new DayCarePL.LedgerProperties();

                Telerik.Web.UI.GridDataItem item = (Telerik.Web.UI.GridDataItem)e.Item;

                var InsertItem = e.Item as Telerik.Web.UI.GridEditableItem;
                Telerik.Web.UI.GridEditManager editMan = InsertItem.EditManager;

                if (InsertItem != null)
                {
                    foreach (GridColumn column in e.Item.OwnerTableView.RenderColumns)
                    {
                        if (column is IGridEditableColumn)
                        {
                            IGridEditableColumn editableCol = (column as IGridEditableColumn);
                            if (editableCol.IsEditable)
                            {
                                IGridColumnEditor editor = editMan.GetColumnEditor(editableCol);
                                switch (column.UniqueName)
                                {
                                    case "TransactionDate":
                                        {
                                            objLedger.TransactionDate = Convert.ToDateTime((e.Item.FindControl("rdpDate") as RadDatePicker).SelectedDate);
                                            objLedger.AllowEdit = (e.Item.FindControl("rdpDate") as RadDatePicker).Enabled;
                                            break;
                                        }
                                    //case "Comment":
                                    //    {
                                    //        objLedger.Comment = (e.Item.FindControl("txtDetail") as TextBox).Text;
                                    //        break;
                                    //    }
                                    case "FamilyName":
                                        {
                                            objLedger.FamilyName = (e.Item.FindControl("txtFamilyName") as TextBox).Text;
                                            break;
                                        }
                                    case "Debit":
                                        {
                                            if (!string.IsNullOrEmpty((e.Item.FindControl("txtDebit") as TextBox).Text))
                                            {
                                                objLedger.Debit = Convert.ToDecimal((e.Item.FindControl("txtDebit") as TextBox).Text);
                                            }
                                            else
                                            {
                                                objLedger.Debit = 0;
                                            }
                                            break;
                                        }
                                    case "Credit":
                                        {
                                            if (!string.IsNullOrEmpty((e.Item.FindControl("txtCredit") as TextBox).Text))
                                            {
                                                objLedger.Credit = Convert.ToDecimal((e.Item.FindControl("txtCredit") as TextBox).Text);
                                            }
                                            else
                                            {
                                                objLedger.Credit = 0;
                                            }
                                            break;
                                        }
                                    case "Comment":
                                        {
                                            objLedger.Comment = (e.Item.FindControl("txtComment") as TextBox).Text;
                                            break;
                                        }
                                    case "operation":
                                        {
                                            if ((e.Item.FindControl("rdbPayment") as RadioButton).Visible)
                                            {
                                                if ((e.Item.FindControl("rdbPayment") as RadioButton).Checked)
                                                {
                                                    objLedger.PaymentMethodId = Convert.ToInt16((e.Item.FindControl("ddlPayment") as DropDownList).SelectedValue);
                                                }
                                                else
                                                {
                                                    objLedger.ChargeCodeCategoryId = new Guid((e.Item.FindControl("ddlCategory") as DropDownList).SelectedValue);
                                                }
                                            }
                                            break;
                                        }

                                }
                            }
                        }
                    }

                    if (ViewState["ChildFamilyId"] != null)
                    {
                        objLedger.ChildFamilyId = new Guid(ViewState["ChildFamilyId"].ToString());
                    }
                    if (Session["CurrentSchoolYearId"] != null)
                    {
                        objLedger.SchoolYearId = new Guid(Session["CurrentSchoolYearId"].ToString());
                    }
                    if (Session["StaffId"] != null)
                    {
                        objLedger.LastModifiedById = new Guid(Session["StaffId"].ToString());
                    }
                    if (e.CommandName != "PerformInsert")
                    {

                        objLedger.Id = new Guid(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Id"].ToString());

                        //if (objLedger.Debit != Convert.ToDecimal("0.00"))
                        //{
                        //    if (objLedger.Debit != Convert.ToDecimal("0.00"))
                        //    {
                        //        objLedger.Credit = Convert.ToDecimal("0.00");
                        //    }
                        //    else
                        //    {
                        //        objLedger.Debit = Convert.ToDecimal("0.00");
                        //    }
                        //}
                    }
                    else
                    {
                        if (Session["StaffId"] != null)
                        {
                            objLedger.CreatedById = new Guid(Session["StaffId"].ToString());
                        }
                    }
                    if (proxySave.UpdateLedger(objLedger))
                    {

                        MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                        MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Saved Successfully", "false"));
                        rgLedger.MasterTableView.Rebind();
                        result = true;
                    }

                }

            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.Ledger, "SubmitRecord", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
            }
            return result;
        }
        #endregion

        protected void rgLedger_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item is GridPagerItem)
            {
                RadComboBox PageSizeCombo = (RadComboBox)e.Item.FindControl("PageSizeComboBox");
                PageSizeCombo.Items.Clear();
                PageSizeCombo.Items.Add(new RadComboBoxItem("25"));
                PageSizeCombo.FindItemByText("25").Attributes.Add("ownerTableViewId", rgLedger.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("50"));
                PageSizeCombo.FindItemByText("50").Attributes.Add("ownerTableViewId", rgLedger.MasterTableView.ClientID);
                //PageSizeCombo.Items[0].Text = "25";
                //PageSizeCombo.Items[1].Text = "50";
                PageSizeCombo.FindItemByText(e.Item.OwnerTableView.PageSize.ToString()).Selected = true;
            }
            if (e.Item.ItemType == GridItemType.Header)
            {
                if (rgLedger.CurrentPageIndex == 0)
                {
                    if (string.IsNullOrEmpty(lblOpeningBalance.Text))
                    {
                        Balance = 0;
                    }
                    else
                    {
                        Balance = Convert.ToDecimal(lblOpeningBalance.Text);
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(lblOpeningBalance.Text))
                    {
                        Balance = 0;
                    }
                    else
                    {
                        Balance = Convert.ToDecimal(lblOpeningBalance.Text);
                    }
                    Balance = Balance + lstLeger.Take(rgLedger.CurrentPageIndex * rgLedger.PageSize).Sum(i => i.Debit) - lstLeger.Take(rgLedger.CurrentPageIndex * rgLedger.PageSize).Sum(i => i.Credit);
                }


            }
            if (e.Item.ItemType == GridItemType.Item || e.Item.ItemType == GridItemType.AlternatingItem)
            {
                DayCarePL.LedgerProperties objLedger = e.Item.DataItem as DayCarePL.LedgerProperties;
                Label lblBalance = e.Item.FindControl("lblBalance") as Label;
                Label txtDebit = e.Item.FindControl("lblDebit") as Label;
                Label txtCredit = e.Item.FindControl("lblDebit") as Label;
                Label lblOperation = e.Item.FindControl("lblOperation") as Label;
                GridEditableColumn edDelete = e.Item.OwnerTableView.Columns[2] as GridEditableColumn;
                edDelete.Visible = true;

                GridDataItem dataItem = e.Item as GridDataItem;
                ImageButton deleteButton = dataItem["DeleteColumn"].Controls[0] as ImageButton;
                ImageButton EditButton = dataItem["Edit"].Controls[0] as ImageButton;
                //if (!IsCurrentYear)
                //{
                //    EditButton.Enabled = false;
                //    deleteButton.Enabled = false;
                //    EditButton.ToolTip = "Editing Restricted. You can only edit current year data";
                //    deleteButton.ToolTip = "Deleting Restricted. You can only delete current year data";
                //}
                //GridEditableColumn edSelect = e.Item.OwnerTableView.Columns[11] as GridEditableColumn;
                //edSelect.Display = true;
                if (objLedger != null)
                {
                    //lblFamilyTitle.Text = objLedger.FamilyName;
                    //#region OpeningBalance
                    //Label lblComment = e.Item.FindControl("lblComment") as Label;
                    //CheckBox chkDelete = e.Item.FindControl("chkDelete") as CheckBox;
                    //Label lblTransactionDate = e.Item.FindControl("lblTransactionDate") as Label;
                    //if (rgLedger.CurrentPageIndex == 0 && e.Item.ItemIndex == 0)
                    //{
                    //    lblComment.Text = "<b>OPENING BALANCE:</b>";
                    //    GridDataItem dataItem = e.Item as GridDataItem;
                    //    //GridEditableItem dataItem = e.Item as GridEditableItem;
                    //    ImageButton deleteButton = dataItem["DeleteColumn"].Controls[0] as ImageButton;
                    //    deleteButton.Visible = false;
                    //    ImageButton Edit = dataItem["Edit"].Controls[0] as ImageButton;
                    //    Edit.Visible = false;
                    //    chkDelete.Visible = false;
                    //    lblTransactionDate.Text = "";
                    //    txtDebit.Text = "";
                    //    txtCredit.Text = "";
                    //    lblBalance.Text = "<b>" + lblOpeningBalance.Text + "</b>";
                    //}
                    //#endregion
                    //else
                    //{


                    ViewState["ChildFamilyId"] = objLedger.ChildFamilyId;
                    //if (objLedger.PaymentId != null)
                    //{
                    //    txtCredit.ReadOnly = true;
                    //}
                    /* if (objLedger.AllowEdit == false)
                     {
                         if (e.Item is GridDataItem)
                         {
                             GridEditableItem dataItem = e.Item as Telerik.Web.UI.GridEditableItem;
                             // ImageButton editButton = (ImageButton)item.FindControl("DeleteColumn");
                             ImageButton deleteButton = dataItem["DeleteColumn"].Controls[0] as ImageButton;
                             deleteButton.Visible = false;
                         }
                     }
                     if (objLedger.AllowEdit == true)
                     {
                         if (e.Item is GridDataItem)
                         {
                             GridEditableItem dataItem = e.Item as Telerik.Web.UI.GridEditableItem;
                             // ImageButton editButton = (ImageButton)item.FindControl("DeleteColumn");
                             ImageButton deleteButton = dataItem["DeleteColumn"].Controls[0] as ImageButton;
                             deleteButton.Visible = true;
                         }
                     }*/



                    if (objLedger.PaymentMethodId != null)
                    {
                        switch (objLedger.PaymentMethodId)
                        {
                            case 0:
                                {
                                    lblOperation.Text = "Cash";
                                    break;
                                }
                            case 1:
                                {
                                    lblOperation.Text = "Check";
                                    break;
                                }
                            case 2:
                                {
                                    lblOperation.Text = "Credit";
                                    break;
                                }
                        }
                    }
                    else if (objLedger.ChargeCodeCategoryId != null)
                    {
                        lblOperation.Text = objLedger.ChargeCodeCategoryName;
                    }
                    if (objLedger.Debit > 0)
                    {

                        Balance += objLedger.Debit;

                    }
                    else
                    {

                        Balance -= objLedger.Credit;
                        YTDPay += objLedger.Credit;

                    }
                    lblBalance.Text = Balance.ToString();
                    //txtBalance.Text = Balance.ToString();
                    //txtYTDPay.Text = YTDPay.ToString();
                    //}
                }
            }
            if (e.Item.ItemType == GridItemType.EditItem)
            {
                GridEditableItem itm = e.Item as GridEditableItem;
                DayCarePL.LedgerProperties objLedger = e.Item.DataItem as DayCarePL.LedgerProperties;
                RadDatePicker rdpDate = e.Item.FindControl("rdpDate") as RadDatePicker;
                DropDownList ddlPayment = e.Item.FindControl("ddlPayment") as DropDownList;
                DropDownList ddlCategory = e.Item.FindControl("ddlCategory") as DropDownList;
                Common.BindChargeCode(ddlCategory);
                RadioButton rdbPayment = e.Item.FindControl("rdbPayment") as RadioButton;
                RadioButton rdbDiscount = e.Item.FindControl("rdbDiscount") as RadioButton;
                TextBox txtDebit = e.Item.FindControl("txtDebit") as TextBox;
                //GridEditableColumn edDelete = e.Item.OwnerTableView.Columns[2] as GridEditableColumn;
                //edDelete.Visible = false;
                //Label lblTransactionDate1=e.Item.FindControl("lblTransactionDate1") as Label;
                GridEditableColumn edSelect1 = e.Item.OwnerTableView.Columns[12] as GridEditableColumn;
                //edSelect1.Display = false;
                if (objLedger != null)
                {
                    if (objLedger.TransactionDate != null)
                    {
                        rdpDate.SelectedDate = objLedger.TransactionDate;
                    }
                    if (objLedger.AllowEdit)
                    {
                        if (objLedger.PaymentId != null)
                        {
                            GridEditableColumn edSelect = e.Item.OwnerTableView.Columns[3] as GridEditableColumn;
                            edSelect.Display = false;
                            rdbPayment.Style.Add("display", "none");
                            rdbDiscount.Style.Add("display", "none");
                            ddlPayment.SelectedValue = objLedger.PaymentMethodId.ToString();
                        }
                        else if (objLedger.PaymentMethodId != null)
                        {
                            rdbPayment.Checked = true;
                            rdbDiscount.Checked = false;
                            ddlPayment.SelectedValue = objLedger.PaymentMethodId.ToString();
                        }
                        else if (objLedger.ChargeCodeCategoryId != null)
                        {
                            rdbPayment.Checked = false;
                            rdbDiscount.Checked = true;
                            ddlCategory.SelectedValue = objLedger.ChargeCodeCategoryId.ToString();
                        }
                        else
                        {
                            rdbPayment.Checked = true;
                            rdbDiscount.Checked = false;
                        }

                        if (rdbPayment.Checked)
                        {
                            ddlPayment.Style.Add("display", "block");
                            ddlCategory.Style.Add("display", "none");
                            txtDebit.Enabled = false;

                            //itm["Select"].Visible = true;
                            //itm["Select"].Controls[1].Visible = true;
                            // itm["operation"].Visible = true;
                        }
                        else
                        {
                            ddlCategory.Style.Add("display", "block");
                            ddlPayment.Style.Add("display", "none");
                        }
                    }
                    else
                    {
                        GridEditableColumn edSelect = e.Item.OwnerTableView.Columns[3] as GridEditableColumn;
                        edSelect.Display = false;
                        rdpDate.Enabled = false;
                        if (objLedger.TransactionDate != null)
                        {
                            rdpDate.SelectedDate = objLedger.TransactionDate;
                        }
                        ddlCategory.Visible = false;
                        ddlPayment.Visible = false;
                        rdbPayment.Visible = false;
                        rdbDiscount.Visible = false;
                        //itm["Select"].Visible = false;
                        // itm["operation"].Visible = false;
                    }
                }
                else
                {
                    if (rdbPayment.Checked)
                    {
                        ddlCategory.Style.Add("display", "none");
                        txtDebit.Enabled = false;
                        rdpDate.SelectedDate = DateTime.Now;
                        rdbPayment.TextAlign = TextAlign.Right;
                    }
                }
            }
        }

        protected void rgLedger_ItemCreated(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item is GridEditableItem && e.Item.IsInEditMode)
            {
                GridEditableItem item = e.Item as GridEditableItem;
                TableCell cell;
                RequiredFieldValidator validator;
                try
                {
                    if (item != null)
                    {
                        ValidationSummary validationsum = new ValidationSummary();
                        validationsum.ID = "validationsum1";
                        validationsum.ShowMessageBox = true;
                        validationsum.ShowSummary = false;
                        //validationsum.DisplayMode = ValidationSummaryDisplayMode.SingleParagraph;

                        ImageButton Edit = item["Edit"].Controls[0] as ImageButton;
                        //ImageButton InitInsert = item["InitInsert"].Controls[0] as ImageButton;
                        if (Edit != null)
                        {
                            Edit.Attributes.Add("onclick", "return Chk(this);");
                        }
                    }
                }
                catch (Exception ex)
                {
                    DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.Ledger, "rgLedger_ItemCreated", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                }
            }
        }

        protected void rgLedger_DeleteCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                DayCareBAL.LedgerService proxyLedgerdelete = new DayCareBAL.LedgerService();
                //DayCarePL.LedgerProperties objTest = e.Item.DataItem as DayCarePL.LedgerProperties;
                Guid Id = new Guid(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Id"].ToString());

                if (proxyLedgerdelete.DeleteLedger(Id))
                {
                    MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                    MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Deleted Successfully", "false"));
                    //rgLedger.MasterTableView.Rebind();
                }
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.Ledger, "rgLedger_DeleteCommand", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
            }
        }

        protected void rgLedger_PreRender(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //{
            //    rgLedger.CurrentPageIndex = rgLedger.PageCount - 1;
            //    rgLedger.MasterTableView.Rebind();
            //}
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            rdpDate.SelectedDate = null;
            ddlCategory.SelectedValue = null;
            txtAmount.Text = "";
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.Ledger, "btnSave_Click", "Submit btnSave_Click called", DayCarePL.Common.GUID_DEFAULT);
            try
            {
                string Amount = "";
                DayCareBAL.LedgerService proxySaveLedger = new DayCareBAL.LedgerService();
                DayCarePL.LedgerProperties objLedger = new DayCarePL.LedgerProperties();
                if (Session["StaffId"] != null)
                {
                    objLedger.CreatedById = new Guid(Session["StaffId"].ToString());
                }
                objLedger.TransactionDate = Convert.ToDateTime(rdpDate.SelectedDate);
                Amount = txtAmount.Text.Trim();
                if (!string.IsNullOrEmpty(Amount))
                {
                    decimal amountresult = 0;
                    decimal.TryParse(Amount, out amountresult);
                    if (amountresult == 0)
                    {
                        objLedger.Debit = amountresult;
                        MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                        MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Pease enter valid Fees.", "false"));
                    }
                    else
                    {
                        if (ddlCategory.SelectedItem.Text.Contains("- Fee"))
                        {
                            objLedger.Debit = amountresult;
                        }
                        else
                        {
                            objLedger.Credit = amountresult;
                        }
                    }
                }
                objLedger.Detail = ddlCategory.SelectedItem.ToString();
                if (ViewState["ChildFamilyId"] != null)
                {
                    objLedger.ChildFamilyId = new Guid(ViewState["ChildFamilyId"].ToString());
                }
                if (Session["CurrentSchoolYearId"] != null)
                {
                    objLedger.SchoolYearId = new Guid(Session["CurrentSchoolYearId"].ToString());
                }
                objLedger.AllowEdit = true;

                if (proxySaveLedger.SaveLedger(objLedger))
                {
                    MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                    MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Saved Successfully", "false"));
                    rgLedger.MasterTableView.Rebind();
                    rdpDate.SelectedDate = null;
                    ddlCategory.SelectedValue = null;
                    txtAmount.Text = "";
                }
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.Ledger, "btnSave_Click", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Internal Error,Please try again.", "false"));
            }
        }

        protected void btnReposting_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["ChildFamilyId"] != null && Session["CurrentSchoolYearId"] != null)
                {
                    DayCareBAL.LedgerService proxyAddEditChildService = new DayCareBAL.LedgerService();
                    int rowcount = proxyAddEditChildService.DeleteLedgerDetailsByChildFamilyIdAndSchoolProgramId(new Guid(ViewState["ChildFamilyId"].ToString()));
                    if (rowcount > 0)
                    {
                        MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                        MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Resposting Successfully done for " + Common.GetFamilyName(new Guid(ViewState["ChildFamilyId"].ToString())), "false"));
                        GetChildProgEnrollmentFeeDetail(new Guid(Session["CurrentSchoolYearId"].ToString()));
                        rgLedger.MasterTableView.Rebind();
                    }
                    else if (rowcount == -1)
                    {
                        MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                        MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Internal Error,Please try again.", "false"));
                    }
                }
                else
                {
                    MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                    MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Internal Error,Please try again.", "false"));
                }
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.Ledger, "btnReposting_Click", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Internal Error,Please try again.", "false"));
            }
        }

        public void SetMenuLink()
        {
            try
            {
                List<DayCarePL.MenuLink> lstMenu = new List<DayCarePL.MenuLink>();
                DayCarePL.MenuLink objMenu;
                objMenu = new DayCarePL.MenuLink();
                objMenu.Name = "Family: " + Common.GetFamilyName(new Guid(ViewState["ChildFamilyId"].ToString()));
                objMenu.Name += ": " + Common.GetFamilyChildName(new Guid(ViewState["ChildFamilyId"].ToString()), new Guid(Session["CurrentSchoolYearId"].ToString()));
                objMenu.Url = "~/UI/LedgerofFamily.aspx";
                lstMenu.Add(objMenu);
                objMenu = new DayCarePL.MenuLink();
                objMenu.Name = "Ledger";
                objMenu.Url = "";
                lstMenu.Add(objMenu);
                usrMenuLink.SetMenuLink(lstMenu);
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.AddEditChild, "SetMenuLink:ChildFamilyId=" + ViewState["ChildFamilyId"], ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
            }
        }

        public void btnPrint_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(ViewState["ChildFamilyId"].ToString()))
            {
                Response.Redirect("../Report/ViewLedgerOfFamilyReport.aspx?ChildFamilyId=" + ViewState["ChildFamilyId"].ToString());
                //StringBuilder jscript = new StringBuilder();
                //jscript.Append("<script>window.open('");
                //jscript.Append("../Report/RptLedgerOfFamilyReport.aspx?ChildFamilyId=" + ViewState["ChildFamilyId"].ToString());
                //jscript.Append("');</script>");
                //Page.RegisterStartupScript("OpenWindows", jscript.ToString());
            }
        }

        protected void btnDeleteAll_Click(object sender, EventArgs e)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.Ledger, "btnDeleteAll_Click", "btnDeleteAll_Click called", DayCarePL.Common.GUID_DEFAULT);
            bool result = false;
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.Ledger, "btnDeleteAll_Click", "Debug btnDeleteAll_Click called", DayCarePL.Common.GUID_DEFAULT);
                DayCareBAL.LedgerService proxySave = new DayCareBAL.LedgerService();
                DayCarePL.LedgerProperties objLedger = new DayCarePL.LedgerProperties();
                List<Guid> lstLeger = new List<Guid>();
                foreach (GridDataItem e1 in rgLedger.MasterTableView.Items)
                {

                    CheckBox chkDelete = e1.FindControl("chkDelete") as CheckBox;
                    if (chkDelete != null)
                    {
                        if (chkDelete.Checked)
                        {
                            lstLeger.Add(new Guid(e1.GetDataKeyValue("Id").ToString()));
                        }
                    }
                }
                if (lstLeger != null && lstLeger.Count > 0)
                {
                    DayCareBAL.LedgerService proxyLedger = new DayCareBAL.LedgerService();
                    if (proxyLedger.DeleteSelectedLedger(lstLeger))
                    {
                        MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                        MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Deleted Successfully", "false"));
                    }

                    rgLedger.MasterTableView.Rebind();
                    rgLedger.MasterTableView.CurrentPageIndex = 0;
                }
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.Ledger, "btnDeleteAll_Click", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
            }


        }

        protected void btnEditAll_Click(object sender, EventArgs e)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.Ledger, "btnDeleteAll_Click", "btnDeleteAll_Click called", DayCarePL.Common.GUID_DEFAULT);
            bool result = false;
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.Ledger, "btnDeleteAll_Click", "Debug btnDeleteAll_Click called", DayCarePL.Common.GUID_DEFAULT);
                //DayCareBAL.LedgerService proxySave = new DayCareBAL.LedgerService();
                //DayCarePL.LedgerProperties objLedger = new DayCarePL.LedgerProperties();
                //List<Guid> lstLeger = new List<Guid>();
                //foreach (GridDataItem e1 in rgLedger.MasterTableView.Items)
                //{

                //    CheckBox chkDelete = e1.FindControl("chkDelete") as CheckBox;
                //    if (chkDelete != null)
                //    {
                //        if (chkDelete.Checked)
                //        {
                //            lstLeger.Add(new Guid(e1.GetDataKeyValue("Id").ToString()));
                //        }
                //    }
                //}
                //if (lstLeger != null && lstLeger.Count > 0)
                //{
                //    Session["SelectedLedgerForEdit"] = lstLeger;
                //DayCareBAL.LedgerService proxyLedger = new DayCareBAL.LedgerService();
                //if (proxyLedger.DeleteSelectedLedger(lstLeger))
                //{
                //    MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                //    MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Delete Successfully", "false"));
                //}

                //rgLedger.MasterTableView.Rebind();
                //rgLedger.MasterTableView.CurrentPageIndex = 0;
                Response.Redirect("EditLedger.aspx?ChildFamilyId=" + ViewState["ChildFamilyId"], false);
                //}
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.Ledger, "btnDeleteAll_Click", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
            }
        }
    }
}
