using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace DayCare.UI
{
    public partial class ProgSchedule : System.Web.UI.Page
    {
        RadAjaxManager MasterAjaxManager;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["SchoolId"] == null && Session["CurrentSchoolYearId"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            if (string.IsNullOrEmpty(Request.QueryString["SchoolProgramId"]) || string.IsNullOrEmpty(Request.QueryString["SchoolYearId"]))
            {
                Response.Redirect("~/Login.aspx");
            }
            else
            {
                ViewState["SchoolProgramId"] = Request.QueryString["SchoolProgramId"].ToString();
                ViewState["SchoolYearId"] = Request.QueryString["SchoolYearId"].ToString();
            }
        }

        protected void rgProgSchedule_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DayCareBAL.ProgScheduleService proxyProgSchedule = new DayCareBAL.ProgScheduleService();
            Guid SchoolId = new Guid();
            if (Session["SchoolId"] != null)
            {
                SchoolId = new Guid(Session["SchoolId"].ToString());
            }
            rgProgSchedule.DataSource = proxyProgSchedule.LoadProgSchedule(SchoolId, new Guid(ViewState["SchoolProgramId"].ToString()));
        }

        protected void rgProgSchedule_EditCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            GridDataItem item = (GridDataItem)e.Item;
            hdnName.Value = item["Day"].Text;
        }

        protected void rgProgSchedule_InsertCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            bool isValid = SubmitRecord(source, e);
            {
                if (isValid == false)
                {
                    e.Canceled = true;
                }
            }
            rgProgSchedule.MasterTableView.CurrentPageIndex = 0;
        }

        protected void rgProgSchedule_ItemCreated(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item.ItemType == GridItemType.FilteringItem)
            {
                GridFilteringItem fileterItem = (GridFilteringItem)e.Item;
                for (int i = 0; i < fileterItem.Cells.Count; i++)
                {
                    fileterItem.Cells[i].Style.Add("text-align", "left");
                }
            }
            try
            {
                if (e.Item is GridEditableItem && e.Item.IsInEditMode)
                {
                    GridEditableItem item = e.Item as GridEditableItem;
                    RequiredFieldValidator validator;
                    if (item != null)
                    {
                        ValidationSummary validationsum = new ValidationSummary();
                        validationsum.ID = "validationsum1";
                        validationsum.ShowMessageBox = true;
                        validationsum.ShowSummary = false;
                        validationsum.DisplayMode = ValidationSummaryDisplayMode.SingleParagraph;
                        // GridTextBoxColumnEditor editor = (GridTextBoxColumnEditor)item.EditManager.GetColumnEditor("Day");
                        DropDownList ddlSchoolProgram = item["SchoolProgram"].FindControl("ddlSchoolProgram") as DropDownList;
                      
                        if (ddlSchoolProgram != null)
                        {
                            TableCell cell = (TableCell)ddlSchoolProgram.Parent;
                            validator = new RequiredFieldValidator();
                            if (cell != null)
                            {
                                ddlSchoolProgram.ID = "ddlSchoolProgram";
                                validator.ControlToValidate = ddlSchoolProgram.ID;
                                validator.ErrorMessage = "Please select Day.\n";
                                validator.SetFocusOnError = true;
                                validator.InitialValue = DayCarePL.Common.GUID_DEFAULT;
                                validator.Display = ValidatorDisplay.None;
                                cell.Controls.Add(validator);
                                cell.Controls.Add(validationsum);
                            }
                        }
                        DropDownList ddlDay = item["Day"].FindControl("ddlDayName") as DropDownList;
                        if (ddlDay != null)
                        {
                            TableCell cell = (TableCell)ddlDay.Parent;
                            validator = new RequiredFieldValidator();
                            if (cell != null)
                            {
                                ddlDay.ID = "Day";
                                validator.ControlToValidate = ddlDay.ID;
                                validator.ErrorMessage = "Please select Day.\n";
                                validator.SetFocusOnError = true;
                                validator.InitialValue = "-1";
                                validator.Display = ValidatorDisplay.None;
                                cell.Controls.Add(validator);
                                cell.Controls.Add(validationsum);
                            }
                        }
                       

                        RadTimePicker rdOpenTp = item["BeginTime"].FindControl("rdBiginTp") as RadTimePicker;
                        if (rdOpenTp != null)
                        {
                            TableCell cell = (TableCell)rdOpenTp.Parent;
                            validator = new RequiredFieldValidator();
                            if (cell != null)
                            {
                                rdOpenTp.ID = "rdBiginTp";
                                validator.ControlToValidate = rdOpenTp.ID;
                                validator.ErrorMessage = "Please select Open Time.\n";
                                validator.SetFocusOnError = true;
                                validator.Display = ValidatorDisplay.None;
                                cell.Controls.Add(validator);
                                cell.Controls.Add(validationsum);
                            }
                        }

                        RadTimePicker rdCloseTp = item["CloseTime"].FindControl("rdCloseTp") as RadTimePicker;
                        if (rdCloseTp != null)
                        {
                            TableCell cell = (TableCell)rdCloseTp.Parent;
                            validator = new RequiredFieldValidator();
                            if (cell != null)
                            {
                                rdCloseTp.ID = "rdCloseTp";
                                validator.ControlToValidate = rdCloseTp.ID;
                                validator.ErrorMessage = "Please select Close Time.\n";
                                validator.SetFocusOnError = true;
                                validator.Display = ValidatorDisplay.None;
                                cell.Controls.Add(validator);
                                cell.Controls.Add(validationsum);
                            }
                        }
                       
                        
                        
                    }

                }
            }
            catch
            {

            }
        }

        protected void rgProgSchedule_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item.ItemType == GridItemType.AlternatingItem || e.Item.ItemType == GridItemType.Item)
            {
                Label lblSchoolProgram = e.Item.FindControl("lblSchoolProgram") as Label;
            }
            if (e.Item.ItemType == GridItemType.EditItem)
            {
                GridEditableItem itm = e.Item as GridEditableItem;
                DayCarePL.ProgScheduleProperties objProgSchedule = e.Item.DataItem as DayCarePL.ProgScheduleProperties;
                DropDownList ddlDay = itm["Day"].Controls[1] as DropDownList;
                // Label lblay = e.Item.FindControl("lblDay") as Label;
                DropDownList ddlSchoolProgram = e.Item.FindControl("ddlSchoolProgram") as DropDownList;
                Guid SchoolId = new Guid();
                RadTimePicker rdOpenTp = e.Item.FindControl("rdBiginTp") as RadTimePicker;
                RadTimePicker rdCloseTp = e.Item.FindControl("rdCloseTp") as RadTimePicker;
                Label lblSchoolProgram = e.Item.FindControl("lblSchoolProgram") as Label;
                DropDownList ddlProgClassRoom = itm["ProgClassRoom"].Controls[1] as DropDownList;
                if (ddlProgClassRoom != null)
                {
                    Common.BindProgClassRoom(ddlProgClassRoom,new Guid(ViewState["SchoolProgramId"].ToString()));
                    if (e.Item.ItemIndex != -1)
                    {
                        DayCarePL.ProgScheduleProperties dataItem = e.Item.DataItem as DayCarePL.ProgScheduleProperties;

                        ddlProgClassRoom.SelectedValue = dataItem.ProgClassRoomId.ToString();
                    }

                }
                if (ddlSchoolProgram != null)
                {
                    ddlSchoolProgram.Items.Add(new ListItem("--select--", DayCarePL.Common.GUID_DEFAULT));
                }
                if (objProgSchedule != null)
                {
                    if (ddlDay != null && ddlDay.Items.Count > 0)
                    {
                        ddlDay.SelectedValue = objProgSchedule.DayIndex.ToString();
                    }
                    if (rdOpenTp != null)
                    {
                        rdOpenTp.SelectedDate = objProgSchedule.BeginTime;
                    }
                    if (rdCloseTp != null)
                    {
                        rdCloseTp.SelectedDate = objProgSchedule.EndTime;
                    }

                }
            }
        }

        protected void rgProgSchedule_UpdateCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            bool isValid = SubmitRecord(source, e);
            {
                if (isValid == false)
                {
                    e.Canceled = true;
                }
            }
            e.Item.Expanded = false;
            rgProgSchedule.MasterTableView.Rebind();
            rgProgSchedule.MasterTableView.CurrentPageIndex = 0;
        }

        public bool SubmitRecord(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.ClassRoom, "SubmitRecord", "Submit record method called", DayCarePL.Common.GUID_DEFAULT);
            bool result = false;
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.ClassRoom, "SubmitRecord", " Debug Submit record method called of ClassRoom", DayCarePL.Common.GUID_DEFAULT);

                DayCareBAL.ProgScheduleService proxyProgSchedule = new DayCareBAL.ProgScheduleService();
                DayCarePL.ProgScheduleProperties objProgSchedule = new DayCarePL.ProgScheduleProperties();

                GridDataItem item = (GridDataItem)e.Item;
                var InsertItem = e.Item as Telerik.Web.UI.GridEditableItem;

                Telerik.Web.UI.GridEditManager editMan = InsertItem.EditManager;

                if (InsertItem != null)
                {
                    foreach (GridColumn column in e.Item.OwnerTableView.RenderColumns)
                    {
                        if (column is GridEditableColumn)
                        {
                            IGridEditableColumn editTableColumn = (column as IGridEditableColumn);

                            if (editTableColumn.IsEditable)
                            {
                                IGridColumnEditor editor = editMan.GetColumnEditor(editTableColumn);

                                switch (column.UniqueName)
                                {
                                    case "SchoolProgram":
                                        {
                                            objProgSchedule.SchoolProgramId = new Guid(ViewState["SchoolProgramId"].ToString());
                                            break;
                                        }
                                    case "Day":
                                        {
                                            objProgSchedule.Day = (item["Day"].Controls[1] as DropDownList).SelectedItem.ToString();
                                            objProgSchedule.DayIndex = Convert.ToInt32((item["Day"].Controls[1] as DropDownList).SelectedValue);
                                            break;
                                        }
                                    case "BeginTime":
                                        {
                                            if ((e.Item.FindControl("rdBiginTp") as RadTimePicker).SelectedDate != null)
                                            {
                                                objProgSchedule.BeginTime = (e.Item.FindControl("rdBiginTp") as RadTimePicker).SelectedDate;
                                            }
                                            break;
                                        }
                                    case "CloseTime":
                                        {
                                            if ((e.Item.FindControl("rdCloseTp") as RadTimePicker).SelectedDate != null)
                                            {
                                                objProgSchedule.EndTime = (e.Item.FindControl("rdCloseTp") as RadTimePicker).SelectedDate;
                                            }
                                            break;
                                        }
                                    case "Active":
                                        {
                                            objProgSchedule.Active = (editor as GridCheckBoxColumnEditor).Value;
                                            break;
                                        }
                                    case "ProgClassRoom":
                                        {
                                            objProgSchedule.ProgClassRoomId = new Guid((item["ProgClassRoom"].Controls[1] as DropDownList).SelectedValue);
                                            DayCareBAL.ProgClassRoomService proxyClassroom = new DayCareBAL.ProgClassRoomService();
                                            objProgSchedule.ClassRoomId = proxyClassroom.LoadProgClassRoom(new Guid(Session["SchoolId"].ToString()),new Guid(ViewState["SchoolProgramId"].ToString())).FirstOrDefault(u => u.Id.Equals(objProgSchedule.ProgClassRoomId)).ClassRoomId.Value;
                                            break;
                                        }
                                }
                            }
                        }
                    }
                    if (objProgSchedule.BeginTime != null && objProgSchedule.EndTime != null)
                    {
                        if (objProgSchedule.BeginTime > objProgSchedule.BeginTime)
                        {
                            MasterAjaxManager = this.Page.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                            MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Begin Time must be less than End Time.", "false"));
                            return false;
                        }
                    }
                    else
                    {
                        MasterAjaxManager = this.Page.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                        MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Begin Time/End Time is not valid.", "false"));
                        return false;
                    }



                    if (e.CommandName != "PerformInsert")
                    {
                        if (Session["StaffId"] != null)
                        {
                            objProgSchedule.LastModifiedById = new Guid(Session["StaffId"].ToString());
                        }
                        objProgSchedule.Id = new Guid(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Id"].ToString());
                    }
                    else 
                    {
                        if (Session["StaffId"] != null)
                        {
                            objProgSchedule.CreatedById = new Guid(Session["StaffId"].ToString());
                            objProgSchedule.LastModifiedById = new Guid(Session["StaffId"].ToString());
                        }
                    }
                        DropDownList ddlClassRoom = e.Item.FindControl("ddlProgClassRoom") as DropDownList;
                        if (ddlClassRoom.SelectedIndex == 0)
                        {
                            MasterAjaxManager = this.Page.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                            MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Select ClassRoom.", "false"));
                            return false;
                        }
                        else
                        {
                            hdnName.Value = "";
                           DayCarePL.ProgScheduleProperties objCheck = proxyProgSchedule.CheckDuplicateProgClassRoom(objProgSchedule.ClassRoomId, Convert.ToDateTime(objProgSchedule.BeginTime), Convert.ToDateTime(objProgSchedule.EndTime), Convert.ToInt32(objProgSchedule.DayIndex), objProgSchedule.Id);
                           DayCarePL.ProgScheduleProperties objResult = proxyProgSchedule.CheckBeginTimeAndEndTime(new Guid(Session["SchoolId"].ToString()), objProgSchedule.DayIndex, Convert.ToDateTime(objProgSchedule.BeginTime), Convert.ToDateTime(objProgSchedule.EndTime));
                           if (objResult == null)
                           {
                               MasterAjaxManager = this.Page.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                               MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "BeginTime And EndTime Does not Match  Hours Of Opration OpenTime And CloseTime", "false"));
                               return false;
                           }
                           if (objCheck!= null)
                            {
                                MasterAjaxManager = this.Page.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                                MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "" + objCheck.SchoolProgramTitle + " (program) already assigned to " + objCheck.ProgClassRoomTitle + " (class room) from " + Convert.ToDateTime(objCheck.BeginTime).ToShortTimeString() + "  to  " + Convert.ToDateTime(objCheck.EndTime).ToShortTimeString() + ".", "false"));
                                return false;
                            }
                            else
                            {

                                result = proxyProgSchedule.Save(objProgSchedule);
                                if (result == true)
                                {
                                    MasterAjaxManager = this.Page.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                                    MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Saved Successfully", "false"));
                                }
                            }
                           
                        }
                           
                }
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.HoursOfOperation, "SubmitRecord", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                result = false;
            }
            return result;
        }

        protected void rgProgSchedule_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            Guid SchoolId = new Guid();
            Guid CurrentSchoolYearId = new Guid();
            if (Session["SchoolId"] != null)
            {
                SchoolId = new Guid(Session["SchoolId"].ToString());
            }

            if (ViewState["SchoolYearId"] != null)
            {
                CurrentSchoolYearId = new Guid(ViewState["SchoolYearId"].ToString());
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
    }
}
