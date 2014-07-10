using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using Telerik.Web.UI;
using System.Web.UI.WebControls;

namespace DayCare.UI
{
    public partial class StaffAttendanceHistoryList : System.Web.UI.Page
    {
        RadAjaxManager MasterAjaxManager;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["SchoolId"] == null && Session["CurrentSchoolYearId"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            if (Request.QueryString["StaffSchoolYearId"] != null)
            {
                ViewState["StaffSchoolYearId"] = Request.QueryString["StaffSchoolYearId"].ToString();
            }
            SetMenuLink();
        }

        protected void rgStaffAttendanceHistory_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            if (ViewState["StaffSchoolYearId"] != null)
            {
                DayCareBAL.StaffAttendanceHistoryListService proxyStaffAttendanceList = new DayCareBAL.StaffAttendanceHistoryListService();
                rgStaffAttendanceHistory.DataSource = proxyStaffAttendanceList.LoadStaffAttendanceHistoryList(new Guid(ViewState["StaffSchoolYearId"].ToString()), new Guid(Session["CurrentSchoolYearId"].ToString()));
            }
        }

        protected void rgStaffAttendanceHistory_EditCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {

        }

        protected void rgStaffAttendanceHistory_InsertCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            bool isValid = SubmitRecord(source, e);
            if (isValid == false)
            {
                e.Canceled = true;
            }
            rgStaffAttendanceHistory.MasterTableView.CurrentPageIndex = 0;
        }

        protected void rgStaffAttendanceHistory_ItemCreated(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item is GridEditableItem && e.Item.IsInEditMode)
            {
                GridEditableItem item = e.Item as GridEditableItem;

                if (item != null)
                {
                    RadDatePicker rdpCheckInCheckOutDateTime = item["CheckInCheckOutDateTime"].FindControl("rdpCheckInCheckOutDateTime") as RadDatePicker;
                    if (rdpCheckInCheckOutDateTime != null)
                    {
                        TableCell cell = (TableCell)rdpCheckInCheckOutDateTime.Parent;
                        RequiredFieldValidator validator = new RequiredFieldValidator();
                        if (cell != null)
                        {

                            rdpCheckInCheckOutDateTime.ID = "rdpCheckInCheckOutDateTime";
                            validator.ControlToValidate = rdpCheckInCheckOutDateTime.ID;
                            validator.ErrorMessage = "Please select Date\n";
                            validator.SetFocusOnError = true;
                            validator.Display = ValidatorDisplay.None;
                        }
                        ValidationSummary validationsum = new ValidationSummary();
                        validationsum.ID = "validationsum1";
                        validationsum.ShowMessageBox = true;
                        validationsum.ShowSummary = false;
                        validationsum.DisplayMode = ValidationSummaryDisplayMode.SingleParagraph;
                        cell.Controls.Add(validator);
                        cell.Controls.Add(validationsum);
                    }

                }
            }

        }

        protected void rgStaffAttendanceHistory_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item.ItemType == GridItemType.EditItem)
            {
                DayCarePL.StaffAttendenceHistoryProperties objStaffAttendance = e.Item.DataItem as DayCarePL.StaffAttendenceHistoryProperties;
                RadDatePicker rdpCheckInCheckOutDateTime = e.Item.FindControl("rdpCheckInCheckOutDateTime") as RadDatePicker;
                RadTimePicker rtpCheckInTime = e.Item.FindControl("rtpCheckInTime") as RadTimePicker;
                RadTimePicker rtpCheckOutTime = e.Item.FindControl("rtpCheckOutTime") as RadTimePicker;
               // TextBox StaffSchoolYearId = e.Item.FindControl("txtStaffSchoolYearId") as TextBox;

                if (objStaffAttendance != null)
                {
                    rdpCheckInCheckOutDateTime.SelectedDate = objStaffAttendance.CheckInCheckOutDateTime;
                    rtpCheckInTime.SelectedDate = objStaffAttendance.CheckInTime;
                    rtpCheckOutTime.SelectedDate = objStaffAttendance.CheckOutTime;
                   // ViewState["StaffSchoolYearId"] = objStaffAttendance.StaffSchoolYearId;
                }
            }
        }

        protected void rgStaffAttendanceHistory_UpdateCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            bool isvalid = SubmitRecord(source, e);
            if (isvalid == false)
            {
                e.Canceled = true;
            }
            e.Item.Expanded = false;
            rgStaffAttendanceHistory.MasterTableView.Rebind();
            rgStaffAttendanceHistory.MasterTableView.CurrentPageIndex = 0;
        }

        protected void rgStaffAttendanceHistory_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
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

            if (!Common.IsCurrentYear(CurrentSchoolYearId, SchoolId))
            {
                if (e.CommandName == "InitInsert")
                {
                    e.Canceled = true;
                }
                else if (e.CommandName == "Edit")
                {
                    e.Canceled = true;
                }
            }
        }

        public bool SubmitRecord(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clAbsentReason, "SubmitRecord", "Submit record method called", DayCarePL.Common.GUID_DEFAULT);
            bool result = false;
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clAbsentReason, "SubmitRecord", "Debug Submit record method called of AbsentReason", DayCarePL.Common.GUID_DEFAULT);
                DayCareBAL.StaffAttendanceHistoryListService proxySave = new DayCareBAL.StaffAttendanceHistoryListService();
                DayCarePL.StaffAttendenceHistoryProperties objStaffAttendance = new DayCarePL.StaffAttendenceHistoryProperties();

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
                                    case "CheckInCheckOutDateTime":
                                        {
                                            objStaffAttendance.CheckInCheckOutDateTime = Convert.ToDateTime((e.Item.FindControl("rdpCheckInCheckOutDateTime") as RadDatePicker).SelectedDate);
                                            break;
                                        }
                                    case "CheckInTime":
                                        {
                                            if ((e.Item.FindControl("rtpCheckInTime") as RadTimePicker).SelectedDate != null)
                                                objStaffAttendance.CheckInTime = Convert.ToDateTime((e.Item.FindControl("rtpCheckInTime") as RadTimePicker).SelectedDate);
                                            break;
                                        }
                                    case "CheckOutTime":
                                        {
                                            if ((e.Item.FindControl("rtpCheckOutTime") as RadTimePicker).SelectedDate != null)
                                                objStaffAttendance.CheckOutTime = Convert.ToDateTime((e.Item.FindControl("rtpCheckOutTime") as RadTimePicker).SelectedDate);
                                            break;
                                        }
                                    //case "StaffSchoolYearId":
                                    //    {
                                    //        objStaffAttendance.StaffSchoolYearId = new Guid((editor as GridTextBoxColumnEditor).Text.Trim());
                                    //        break;
                                    //    }
                                }
                            }
                        }
                    }
                    if (objStaffAttendance.CheckInTime != null)
                    {
                        objStaffAttendance.CheckInCheckOutDateTime = objStaffAttendance.CheckInCheckOutDateTime + objStaffAttendance.CheckInTime.Value.TimeOfDay;
                        objStaffAttendance.CheckInCheckOut = true;
                    }
                    if (objStaffAttendance.CheckOutTime != null)
                    {
                        objStaffAttendance.CheckInCheckOutDateTime = objStaffAttendance.CheckInCheckOutDateTime + objStaffAttendance.CheckOutTime.Value.TimeOfDay;
                        objStaffAttendance.CheckInCheckOut = false;
                    }
                    if (objStaffAttendance.CheckInTime == null && objStaffAttendance.CheckOutTime == null)
                    {
                        MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                        MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Please select check in or check out time", "false"));
                        result = false;
                    }
                    else if (objStaffAttendance.CheckInTime != null && objStaffAttendance.CheckOutTime != null)
                    {
                        MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                        MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Please select check in or check out time", "false"));
                        result = false;
                    }
                    else
                    {
                        if (ViewState["StaffSchoolYearId"] != null)
                        {
                            objStaffAttendance.StaffSchoolYearId = new Guid(ViewState["StaffSchoolYearId"].ToString());
                        }
                        
                        if (e.CommandName != "PerformInsert")
                        {
                            objStaffAttendance.Id = new Guid(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Id"].ToString());
                            
                        }
                        if (proxySave.Save(objStaffAttendance))
                        {
                            MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                            MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Saved Successfully", "false"));
                            result = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.AbsentReason, "SubmitRecord", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                result = false;
            }
            return result;
        }

        public void SetMenuLink()
        {
            try
            {
                string str = "";
                List<DayCarePL.MenuLink> lstMenu = new List<DayCarePL.MenuLink>();
                DayCarePL.MenuLink objMenu;
                if (ViewState["StaffSchoolYearId"] == null)
                {
                    Session.Remove("FullName");
                }
                if (Session["FullName"] != null)
                {
                    objMenu = new DayCarePL.MenuLink();
                    str = Session["FullName"].ToString();

                    objMenu.Name = "Staff " + str;
                    objMenu.Url = "~/UI/StaffAttendanceList.aspx";
                    lstMenu.Add(objMenu);
                }
                objMenu = new DayCarePL.MenuLink();
                objMenu.Name = "Staff Attendance List";
                objMenu.Url = "";
                lstMenu.Add(objMenu);
                usrMenuLink.SetMenuLink(lstMenu);
               
            }
            catch
            {

            }
        }
    }
}
