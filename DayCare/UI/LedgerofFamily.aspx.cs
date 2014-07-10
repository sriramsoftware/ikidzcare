using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace DayCare.UI
{
    public partial class LedgerofFamily : System.Web.UI.Page
    {
        RadAjaxManager MasterAjaxManager;
        Guid SchoolId = new Guid();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["SchoolId"] == null || Session["CurrentSchoolYearId"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            if (Session["SchoolId"] != null)
            {
                SchoolId = new Guid(Session["SchoolId"].ToString());
            }
            if (!IsPostBack)
            {
                GetChildProgEnrollmentFeeDetail(new Guid(Session["CurrentSchoolYearId"].ToString()));
            }
            SetMenuLink();
        }

        // Same function is place on Common.cs which is call in LateFee, Payment, Ledger, AddEditchild
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
                                        if (!TranDate.Equals(new DateTime()) && TranDate.Date <= System.DateTime.Now.Date && TranDate.Date <= EndDate.Date && !OldDate.Value.Date.Equals(LastDate.Date)  && TranDate.Date >= StartDate.Date)
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

        protected void rgFamilyLedger_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DayCareBAL.SchoolService proxySchool = new DayCareBAL.SchoolService();
            Guid SchoolId = new Guid();
            if (Session["SchoolId"] != null)
            {
                SchoolId = new Guid(Session["SchoolId"].ToString());
            }
            ViewState["LateFee"] = proxySchool.GetLateFeeAmount(SchoolId);
            Guid CurrentSchoolYearId = new Guid();
            if (Session["CurrentSchoolYearId"] != null)
            {
                CurrentSchoolYearId = new Guid(Session["CurrentSchoolYearId"].ToString());
            }
            DayCareBAL.LedgerOfFamilyService proxyChildFamily = new DayCareBAL.LedgerOfFamilyService();
            List<DayCarePL.ChildFamilyProperties> lstChildData = proxyChildFamily.LoadChildFamily(SchoolId, CurrentSchoolYearId);

            if (lstChildData != null)
            {
                rgFamilyLedger.DataSource = lstChildData;
            }

        }

        protected void rgFamilyLedger_EditCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            GridDataItem item = (GridDataItem)e.Item;
            // hdnName.Value = item["FamilyTitle"].Text;
            e.Canceled = true;
        }

        protected void rgFamilyLedger_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            //Guid SchoolId = new Guid();
            //Guid CurrentSchoolYearId = new Guid();
            //if (Session["SchoolId"] != null)
            //{
            //    SchoolId = new Guid(Session["SchoolId"].ToString());
            //}

            //if (Session["CurrentSchoolYearId"] != null)
            //{
            //    CurrentSchoolYearId = new Guid(Session["CurrentSchoolYearId"].ToString());
            //}

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
            //}
            //else
            //{
            string FamilyName = "";
            GridDataItem itm = e.Item as GridDataItem;
            if (itm != null)
            {
                FamilyName = itm["FamilyTitle"].Text;
            }
            //Session["ChildFamilyUrl"] = "~/UI/Families.aspx";
            if (e.CommandName == "Edit")
            {
                if (!string.IsNullOrEmpty(FamilyName))
                {
                    Session["ChildFamilyName"] = ": " + FamilyName;
                }
                Guid ChildFamilyId = new Guid(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Id"].ToString());
                if (ChildFamilyId != null)
                {
                    ViewState["ChildFamilyId"] = ChildFamilyId;
                }
                //LoadDataById(ChildFamilyId);
                e.Canceled = true;
                Response.Redirect("Ledger.aspx?ChildFamilyId=" + ChildFamilyId);
            }
            if (e.CommandName == "InitInsert")
            {
                //if (objFamily != null)
                if (!string.IsNullOrEmpty(FamilyName))
                {
                    Session["ChildFamilyName"] = ": " + FamilyName;
                }
                Response.Redirect("Ledger.aspx?ChildFamilyId=");
            }
            //}


        }

        protected void rgFamilyLedger_ItemCreated(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
        }

        protected void rgFamilyLedger_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item is GridPagerItem)
            {
                RadComboBox PageSizeCombo = (RadComboBox)e.Item.FindControl("PageSizeComboBox");
                PageSizeCombo.Items.Clear();
                PageSizeCombo.Items.Add(new RadComboBoxItem("25"));
                PageSizeCombo.FindItemByText("25").Attributes.Add("ownerTableViewId", rgFamilyLedger.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("50"));
                PageSizeCombo.FindItemByText("50").Attributes.Add("ownerTableViewId", rgFamilyLedger.MasterTableView.ClientID);
                //PageSizeCombo.Items[0].Text = "25";
                //PageSizeCombo.Items[1].Text = "50";
                PageSizeCombo.FindItemByText(e.Item.OwnerTableView.PageSize.ToString()).Selected = true;
            }
            if (e.Item.ItemType == GridItemType.AlternatingItem || e.Item.ItemType == GridItemType.Item)
            {
                DayCarePL.ChildFamilyProperties objChildFamily = e.Item.DataItem as DayCarePL.ChildFamilyProperties;
                HyperLink hlLateFees = e.Item.FindControl("hlLateFees") as HyperLink;
                hlLateFees.Attributes.Add("onclick", "ShowLateFee('" + objChildFamily.Id + "','" + objChildFamily.FamilyTitle + "'); return false;");
                decimal LateFee = 0;
                if (ViewState["LateFee"] != null)
                {
                    LateFee = Convert.ToDecimal(ViewState["LateFee"].ToString());
                }
                if (objChildFamily.Balance >= LateFee && LateFee > 0)
                {
                    hlLateFees.Visible = true;
                }
                else
                {
                    hlLateFees.Visible = false;
                }
            }
        }

        protected void rgFamilyLedger_PreRender(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //rgStaffList.MasterTableView.FilterExpression = "([Active] = \'True\')";
                //GridColumn column = rgStaffList.MasterTableView.GetColumnSafe("Status");
                //column.CurrentFilterFunction = GridKnownFunction.EqualTo;
                //rgStaffList.MasterTableView.Rebind();

                rgFamilyLedger.MasterTableView.FilterExpression = "([Active] = \'True\') ";
                GridColumn column = rgFamilyLedger.MasterTableView.GetColumnSafe("Active");

                rgFamilyLedger.MasterTableView.Rebind();

            }
        }

        public void SetMenuLink()
        {
            try
            {
                List<DayCarePL.MenuLink> lstMenu = new List<DayCarePL.MenuLink>();
                DayCarePL.MenuLink objMenu;
                objMenu = new DayCarePL.MenuLink();
                objMenu.Name = "Ledger of Families";
                objMenu.Url = "";
                lstMenu.Add(objMenu);
                usrMenuLink.SetMenuLink(lstMenu);
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.LedgerOfFamily, "SetMenuLink", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
            }
        }
    }
}
