using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace DayCare.UI
{
    public partial class ChildAttendanceHistoryList : System.Web.UI.Page
    {
        RadAjaxManager MasterAjaxManager;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["SchoolId"] == null && Session["CurrentSchoolYearId"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }

            if (Request.QueryString["ChildSchoolYearId"] != null)
            {
                ViewState["ChildSchoolYearId"] = Request.QueryString["ChildSchoolYearId"].ToString();
            }
            SetMenuLink();
        }

        protected void rgChildAttendanceHistory_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            if (Request.QueryString["ChildSchoolYearId"] != null)
            {
                ViewState["ChildSchoolYearId"] = Request.QueryString["ChildSchoolYearId"].ToString();
            }
            DayCareBAL.ChildAttendanceHistoryListService proxyLoadChildAttendanceHistory = new DayCareBAL.ChildAttendanceHistoryListService();
            rgChildAttendanceHistory.DataSource = proxyLoadChildAttendanceHistory.LoadChildAttendanceHistoryList(new Guid(ViewState["ChildSchoolYearId"].ToString()));
        }

        protected void rgChildAttendanceHistory_EditCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
        }

        protected void rgChildAttendanceHistory_InsertCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            bool isValid = SubmitRecord(source, e);
            if (isValid == false)
            {
                e.Canceled = true;
            }
            rgChildAttendanceHistory.MasterTableView.CurrentPageIndex = 0;
        }


        protected void rgChildAttendanceHistory_ItemCreated(object sender, Telerik.Web.UI.GridItemEventArgs e)
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

        protected void rgChildAttendanceHistory_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item.ItemType == GridItemType.EditItem)
            {
                DayCarePL.ChildAttendenceHistoryProperties objChildAttendance = e.Item.DataItem as DayCarePL.ChildAttendenceHistoryProperties;
                RadDatePicker rdpCheckInCheckOutDateTime = e.Item.FindControl("rdpCheckInCheckOutDateTime") as RadDatePicker;
                RadTimePicker rtpCheckInTime = e.Item.FindControl("rtpCheckInTime") as RadTimePicker;
                RadTimePicker rtpCheckOutTime = e.Item.FindControl("rtpCheckOutTime") as RadTimePicker;
                if (objChildAttendance != null)
                {
                    rdpCheckInCheckOutDateTime.SelectedDate = objChildAttendance.CheckInCheckOutDateTime;
                    rtpCheckInTime.SelectedDate = objChildAttendance.CheckInTime;
                    rtpCheckOutTime.SelectedDate = objChildAttendance.CheckOutTime;
                }
            }
        
        }

        protected void rgChildAttendanceHistory_UpdateCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            bool isvalid = SubmitRecord(source, e);
            if (isvalid == false)
            {
                e.Canceled = true;
            }
            e.Item.Expanded = false;
            rgChildAttendanceHistory.MasterTableView.Rebind();
            rgChildAttendanceHistory.MasterTableView.CurrentPageIndex = 0;
        }

        protected void rgChildAttendanceHistory_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
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
                DayCareBAL.ChildAttendanceHistoryListService proxySave = new DayCareBAL.ChildAttendanceHistoryListService();
                DayCarePL.ChildAttendenceHistoryProperties objAttendance = new DayCarePL.ChildAttendenceHistoryProperties();

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
                                            objAttendance.CheckInCheckOutDateTime = Convert.ToDateTime((e.Item.FindControl("rdpCheckInCheckOutDateTime") as RadDatePicker).SelectedDate);
                                            break;
                                        }
                                    case "CheckInTime":
                                        {
                                            if ((e.Item.FindControl("rtpCheckInTime") as RadDatePicker).SelectedDate != null)

                                                objAttendance.CheckInTime = Convert.ToDateTime((e.Item.FindControl("rtpCheckInTime") as RadDatePicker).SelectedDate);
                                                
                                            break;
                                        }
                                    case "CheckOutTime":
                                        {
                                            if ((e.Item.FindControl("rtpCheckOutTime") as RadDatePicker).SelectedDate != null)

                                                objAttendance.CheckOutTime = Convert.ToDateTime((e.Item.FindControl("rtpCheckOutTime") as RadDatePicker).SelectedDate);
                                                
                                            break;
                                        }
                                }
                            }
                        }
                    }
                    if (objAttendance.CheckInTime != null)
                    {
                        objAttendance.CheckInCheckOutDateTime = objAttendance.CheckInCheckOutDateTime +objAttendance.CheckInTime.Value.TimeOfDay;
                        objAttendance.CheckInCheckOut = true;
                    }
                    if (objAttendance.CheckOutTime != null)
                    {
                        objAttendance.CheckInCheckOutDateTime = objAttendance.CheckInCheckOutDateTime + objAttendance.CheckOutTime.Value.TimeOfDay;
                        objAttendance.CheckInCheckOut = false;
                    }
                    if (objAttendance.CheckInTime == null && objAttendance.CheckOutTime == null)
                    {
                        MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                        MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Please select check in or check out time", "false"));
                        result = false;
                    }
                    else if (objAttendance.CheckInTime != null && objAttendance.CheckOutTime != null)
                    {
                        MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                        MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Please select check in or check out time", "false"));
                        result = false;
                    }
                    else
                    {
                        if (ViewState["ChildSchoolYearId"] != null)
                        {
                            objAttendance.ChildSchoolYearId = new Guid(ViewState["ChildSchoolYearId"].ToString());
                        }
                        if (e.CommandName != "PerformInsert")
                        {
                            objAttendance.Id = new Guid(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Id"].ToString());
                        }
                        if (proxySave.Save(objAttendance))
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
                if (ViewState["ChildSchoolYearId"] == null)
                {
                    Session.Remove("ChildName");
                }
                if (Session["ChildName"] != null)
                {
                    objMenu = new DayCarePL.MenuLink();
                    str = Session["ChildName"].ToString();

                    objMenu.Name = "Child Name " + str;
                    objMenu.Url = "~/UI/ChildAttendanceList.aspx";
                    lstMenu.Add(objMenu);
                }
                objMenu = new DayCarePL.MenuLink();
                objMenu.Name = "Child Attendance List";
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
