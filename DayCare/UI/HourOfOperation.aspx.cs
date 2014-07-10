using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace DayCare.UI
{
    public partial class HourOfOperation : System.Web.UI.Page
    {
        RadAjaxManager MasterAjaxManager;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["SchoolId"] == null || Session["CurrentSchoolYearId"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
        }

        protected void rgHourOfOperation_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DayCareBAL.HoursOfOperationService proxyHoursOfOperation = new DayCareBAL.HoursOfOperationService();
            Guid SchoolId = new Guid();
            if (Session["SchoolId"] != null)
            {
                SchoolId = new Guid(Session["SchoolId"].ToString());
            }
            rgHourOfOperation.DataSource = proxyHoursOfOperation.LoadHoursOfOperation(SchoolId);
        }

        protected void rgHourOfOperation_EditCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            GridDataItem item = (GridDataItem)e.Item;
            hdnName.Value = item["Day"].Text;
        }

        protected void rgHourOfOperation_InsertCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            bool isValid = SubmitRecord(source, e);
            {
                if (isValid == false)
                {
                    e.Canceled = true;
                }
            }
            rgHourOfOperation.MasterTableView.CurrentPageIndex = 0;
        }

        protected void rgHourOfOperation_ItemCreated(object sender, Telerik.Web.UI.GridItemEventArgs e)
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
                        RadTimePicker rdOpenTp = item["OpenTime"].FindControl("rdOpenTp") as RadTimePicker;
                        if (rdOpenTp != null)
                        {
                            TableCell cell = (TableCell)rdOpenTp.Parent;
                            validator = new RequiredFieldValidator();
                            if (cell != null)
                            {
                                rdOpenTp.ID = "rdOpenTp";
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

        protected void rgHourOfOperation_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item.ItemType == GridItemType.EditItem)
            {
                GridDataItem itm = e.Item as GridDataItem;
                DayCarePL.HoursOfOperationProperties objHoursOfOperation = e.Item.DataItem as DayCarePL.HoursOfOperationProperties;
                DropDownList ddlDay = itm["Day"].Controls[1] as DropDownList;
                Guid SchoolId = new Guid();
                RadTimePicker rdOpenTp = e.Item.FindControl("rdOpenTp") as RadTimePicker;
                RadTimePicker rdCloseTp = e.Item.FindControl("rdCloseTp") as RadTimePicker;

                if (objHoursOfOperation != null)
                {
                    if (ddlDay != null && ddlDay.Items.Count > 0)
                    {
                        ddlDay.SelectedValue = Convert.ToString(objHoursOfOperation.DayIndex);
                    }
                    if (rdOpenTp != null)
                    {
                        rdOpenTp.SelectedDate = objHoursOfOperation.OpenTime;
                    }
                    if (rdCloseTp != null)
                    {
                        rdCloseTp.SelectedDate = objHoursOfOperation.CloseTime;
                    }
                }
            }
        }

        protected void rgHourOfOperation_UpdateCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            bool isValid = SubmitRecord(source, e);
            {
                if (isValid == false)
                {
                    e.Canceled = true;
                }
            }
            e.Item.Expanded = false;
            rgHourOfOperation.MasterTableView.Rebind();
            rgHourOfOperation.MasterTableView.CurrentPageIndex = 0;
        }

        public bool SubmitRecord(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.ClassRoom, "SubmitRecord", "Submit record method called", DayCarePL.Common.GUID_DEFAULT);
            bool result = false;
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.ClassRoom, "SubmitRecord", " Debug Submit record method called of ClassRoom", DayCarePL.Common.GUID_DEFAULT);

                DayCareBAL.HoursOfOperationService proxyHoursOfOperation = new DayCareBAL.HoursOfOperationService();
                DayCarePL.HoursOfOperationProperties objHoursOfOperation = new DayCarePL.HoursOfOperationProperties();

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
                                    case "Day":
                                        {
                                            //objHoursOfOperation.Day = (e.Item.FindControl("ddlDayName") as DropDownList).SelectedItem.Text;
                                            //objHoursOfOperation.DayIndex = Convert.ToInt32((e.Item.FindControl("ddlDayName") as DropDownList).SelectedValue);
                                            objHoursOfOperation.Day = (item["Day"].Controls[1] as DropDownList).SelectedItem.Text;
                                            objHoursOfOperation.DayIndex = Convert.ToInt32((item["Day"].Controls[1] as DropDownList).SelectedValue);
                                            break;
                                        }
                                    case "OpenTime":
                                        {
                                            if ((e.Item.FindControl("rdOpenTp") as RadTimePicker).SelectedDate != null)
                                            {
                                                objHoursOfOperation.OpenTime = Convert.ToDateTime((e.Item.FindControl("rdOpenTp") as RadTimePicker).SelectedDate);
                                            }
                                            break;
                                        }
                                    case "CloseTime":
                                        {
                                            if ((e.Item.FindControl("rdCloseTp") as RadTimePicker).SelectedDate != null)
                                            {
                                                objHoursOfOperation.CloseTime = Convert.ToDateTime((e.Item.FindControl("rdCloseTp") as RadTimePicker).SelectedDate.Value);
                                            }
                                            break;
                                        }
                                    case "Comments":
                                        {
                                            objHoursOfOperation.Comments = (e.Item.FindControl("txtComments") as TextBox).Text;
                                            break;
                                        }
                                }
                            }
                        }
                    }
                    if (objHoursOfOperation.OpenTime != null && objHoursOfOperation.CloseTime != null)
                    {
                        if(objHoursOfOperation.OpenTime.TimeOfDay > objHoursOfOperation.CloseTime.TimeOfDay)                       
                        {
                            MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                            MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Open Time must be less than Close Time.", "false"));
                            return false;
                        }
                    }
                    else
                    {
                        MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                        MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Open Time/Close Time is not valid.", "false"));
                        return false;
                    }


                    if (Session["SchoolId"] != null)
                    {
                        objHoursOfOperation.SchoolId = new Guid(Session["SchoolId"].ToString());
                    }
                    if (e.CommandName != "PerformInsert")
                    {
                        if (Session["StaffId"] != null)
                        {
                            objHoursOfOperation.LastModifiedById = new Guid(Session["StaffId"].ToString());
                        }
                        objHoursOfOperation.Id = new Guid(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Id"].ToString());
                    }

                    hdnName.Value = "";
                    result = proxyHoursOfOperation.Save(objHoursOfOperation);
                    if (result)
                    {
                        MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                        MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Saved Successfully", "false"));
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

        protected void rgHourOfOperation_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
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
            //}
        }
    }
}
