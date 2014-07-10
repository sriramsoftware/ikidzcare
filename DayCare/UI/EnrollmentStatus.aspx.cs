using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Telerik.Web.UI;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DayCare.UI
{
    public partial class EnrollmentStatus : System.Web.UI.Page
    {
        RadAjaxManager MasterAjaxManager;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["SchoolId"] == null || Session["CurrentSchoolYearId"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
        }
        protected void rgEnrollmentStatus_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                DayCareBAL.EnrollmentStausService Loadproxy = new DayCareBAL.EnrollmentStausService();
                Guid SchoolId = new Guid();
                if (Session["SchoolId"] != null)
                {
                    SchoolId = new Guid(Session["SchoolId"].ToString());
                }
                rgEnrollmentStatus.DataSource = Loadproxy.LoadEnrollmentStatus(SchoolId);
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.EnrollmentStatus, "rgEnrollmentStatus_NeedDataSource", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
            }
        }

        protected void rgEnrollmentStatus_EditCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            GridDataItem item = (GridDataItem)e.Item;
            hdnName.Value = item["Status"].Text;
        }

        protected void rgEnrollmentStatus_InsertCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            bool isValid = SubmitRecord(source, e);
            {
                if (isValid == false)
                {
                    e.Canceled = true;
                }
            }
            rgEnrollmentStatus.MasterTableView.CurrentPageIndex = 0;
        }

        protected void rgEnrollmentStatus_ItemCreated(object sender, Telerik.Web.UI.GridItemEventArgs e)
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
                    if (item != null)
                    {
                        GridTextBoxColumnEditor editor = (GridTextBoxColumnEditor)item.EditManager.GetColumnEditor("Status");
                        ImageButton cmdEdit = (ImageButton)item["Edit"].Controls[0];
                        if (editor != null)
                        {
                            TableCell cell = (TableCell)editor.TextBoxControl.Parent;
                            RequiredFieldValidator validator = new RequiredFieldValidator();
                            if (cell != null)
                            {
                                editor.TextBoxControl.ID = "Status";
                                validator.ControlToValidate = editor.TextBoxControl.ID;
                                validator.ErrorMessage = "Please Enter Enrollment Staus\n";
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
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.EnrollmentStatus, "rgEnrollmentStatus_ItemCreated", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
            }
        }

        protected void rgEnrollmentStatus_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item.ItemIndex == -1)
            {
                if (e.Item.Edit == true)
                {
                    GridEditableItem dataItem = e.Item as Telerik.Web.UI.GridEditableItem;
                    CheckBox chkActive = dataItem["Active"].Controls[0] as CheckBox;
                    chkActive.Checked = true;
                }
            }
        }
        protected void rgEnrollmentStatus_UpdateCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            bool isValid = SubmitRecord(source, e);
            {
                if (isValid == false)
                {
                    e.Canceled = true;
                }
            }
            e.Item.Expanded = false;
            rgEnrollmentStatus.MasterTableView.Rebind();
            rgEnrollmentStatus.MasterTableView.CurrentPageIndex = 0;
        }

        public bool SubmitRecord(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.EnrollmentStatus, "SubmitRecord", "Submit record method called", DayCarePL.Common.GUID_DEFAULT);
            bool result = false;
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.EnrollmentStatus, "SubmitRecord", " Debug Submit record method called of Enrollmentstatus", DayCarePL.Common.GUID_DEFAULT);

                DayCareBAL.EnrollmentStausService proxySave = new DayCareBAL.EnrollmentStausService();
                DayCarePL.EnrollmentStatusProperties objEnrollment = new DayCarePL.EnrollmentStatusProperties();

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
                                    case "Status":
                                        {
                                            objEnrollment.Status = (editor as GridTextBoxColumnEditor).Text.Trim().ToString();
                                            ViewState["Staus"] = objEnrollment.Status;
                                            break;
                                        }
                                    case "Active":
                                        {
                                            objEnrollment.Active = (editor as GridCheckBoxColumnEditor).Value;
                                            break;
                                        }
                                    case "Comments":
                                        {
                                            objEnrollment.Comments = (e.Item.FindControl("txtComments") as TextBox).Text.Trim().ToString();
                                            ViewState["Comments"] = objEnrollment.Comments;
                                            break;
                                        }
                                }
                            }
                        }
                    }
                    if (Session["SchoolId"] != null)
                    {
                        objEnrollment.SchoolId = new Guid(Session["SchoolId"].ToString());
                    }
                    if (e.CommandName != "PerformInsert")
                    {
                        objEnrollment.Id = new Guid(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Id"].ToString());
                        if (!objEnrollment.Id.ToString().Equals(hdnName.Value.Trim()))
                        {
                            bool ans = proxySave.CheckDuplicateEnrollmentStatusName(objEnrollment.Status, objEnrollment.Id, objEnrollment.SchoolId);
                            if (ans)
                            {
                                MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                                MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Already Exist", "false"));
                                return false;
                            }
                        }

                    }
                    else
                    {
                        bool ans = proxySave.CheckDuplicateEnrollmentStatusName(objEnrollment.Status, objEnrollment.Id, objEnrollment.SchoolId);
                        if (ans)
                        {
                            MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                            MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Already Exist", "false"));
                            return false;
                        }
                    }
                    hdnName.Value = "";
                    result = proxySave.Save(objEnrollment);
                    if (result == true)
                    {
                        MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                        MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Saved Successfully", "false"));
                    }
                }
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.EnrollmentStatus, "SubmitRecord", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                result = false;
            }
            return result;
        }

        protected void rgEnrollmentStatus_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
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
