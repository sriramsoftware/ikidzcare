using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using Telerik.Web.UI;
using System.Web.UI.WebControls;

namespace DayCare.UI
{
    public partial class AbsentReason : System.Web.UI.Page
    {
        RadAjaxManager MasterAjaxManager;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["SchoolId"] == null || Session["CurrentSchoolYearId"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
        }

        protected void rgAbsentReason_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                DayCareBAL.AbsentReasonService LoadProxy = new DayCareBAL.AbsentReasonService();
                Guid SchoolId = new Guid();
                if (Session["SchoolId"] != null)
                {
                    SchoolId = new Guid(Session["SchoolId"].ToString());
                }
                rgAbsentReason.DataSource = LoadProxy.LoadAbsentReason(SchoolId);
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.AbsentReason, "rgAbsentReason_NeedDataSource", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
            }
        }

        protected void rgAbsentReason_EditCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            GridDataItem item = (GridDataItem)e.Item;
            hdnName.Value = item["Reason"].Text;
        }

        protected void rgAbsentReason_InsertCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            bool isValid = SubmitRecord(source, e);
            if (isValid == false)
            {
                e.Canceled = true;
            }
            rgAbsentReason.MasterTableView.CurrentPageIndex = 0;
        }

        protected void rgAbsentReason_ItemCreated(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            try
            {
                if (e.Item is GridEditableItem && e.Item.IsInEditMode)
                {
                    GridEditableItem item = e.Item as GridEditableItem;

                    if (item != null)
                    {
                        TextBox txtName = item["Reason"].FindControl("txtReason") as TextBox;
                        if (txtName != null)
                        {
                            TableCell cell = (TableCell)txtName.Parent;
                            RequiredFieldValidator validator = new RequiredFieldValidator();
                            if (cell != null)
                            {

                                txtName.ID = "txtReason";
                                validator.ControlToValidate = txtName.ID;
                                validator.ErrorMessage = "Please Enter Reason\n";
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
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.AbsentReason, "rgAbsentReason_ItemCreated", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
            }
        }

        protected void rgAbsentReason_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
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

        protected void rgAbsentReason_UpdateCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            bool isvalid = SubmitRecord(source, e);
            if (isvalid == false)
            {
                e.Canceled = true;
            }
            e.Item.Expanded = false;
            rgAbsentReason.MasterTableView.Rebind();
            rgAbsentReason.MasterTableView.CurrentPageIndex = 0; 
        }

        public bool SubmitRecord(object source, Telerik.Web.UI.GridCommandEventArgs e)
        { 
          DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clAbsentReason, "SubmitRecord", "Submit record method called", DayCarePL.Common.GUID_DEFAULT);
            bool result = false;
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clAbsentReason, "SubmitRecord", "Debug Submit record method called of AbsentReason", DayCarePL.Common.GUID_DEFAULT);
                DayCareBAL.AbsentReasonService proxySave = new DayCareBAL.AbsentReasonService();
                DayCarePL.AbsentResonProperties objAbsentReason = new DayCarePL.AbsentResonProperties();

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
                                 case "Reason":
                                     {
                                         objAbsentReason.Reason = (e.Item.FindControl("txtReason") as TextBox).Text;
                                         ViewState["Reason"] = objAbsentReason.Reason;
                                         break;
                                     }
                                 case "BillingAffected":
                                     {
                                         objAbsentReason.BillingAffected = (editor as GridCheckBoxColumnEditor).Value;
                                         break;
                                     }
                                 case "Active":
                                     {
                                         objAbsentReason.Active = (editor as GridCheckBoxColumnEditor).Value;
                                         break;
                                     }
                                 case "Comments":
                                     {
                                         objAbsentReason.Comments = (e.Item.FindControl("txtComments") as TextBox).Text;
                                         ViewState["Comments"] = objAbsentReason.Comments;
                                         break;
                                     }
                             }
                          }
                        }
                    }
                    if (Session["SchoolId"] != null)
                    {
                        objAbsentReason.SchoolId = new Guid(Session["SchoolId"].ToString());
                    }
                    if (e.CommandName != "PerformInsert")
                    {
                        if (Session["StaffId"] != null)
                        {
                            objAbsentReason.LastModifiedById = new Guid(Session["StaffId"].ToString());
                        }
                        
                        objAbsentReason.Id = new Guid(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Id"].ToString());
                        if (!objAbsentReason.Reason.Trim().Equals(hdnName.Value.Trim()))
                        {
                           // bool ans = Common.CheckDuplicate("AbsentReason", "Reason", objAbsentReason.Reason, "update", objAbsentReason.Id.ToString());
                            bool ans = proxySave.CheckDuplicateAbsentReason(objAbsentReason.Reason, objAbsentReason.Id, objAbsentReason.SchoolId);
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
                       // bool ans = Common.CheckDuplicate("AbsentReason", "Reason", objAbsentReason.Reason, "insert", objAbsentReason.Id.ToString());
                        bool ans = proxySave.CheckDuplicateAbsentReason(objAbsentReason.Reason, objAbsentReason.Id, objAbsentReason.SchoolId);
                        if (ans)
                        {
                            MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                            MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Already Exist", "false"));
                            return false;
                        }
                    }
                    hdnName.Value = "";
                    result = proxySave.Save(objAbsentReason);
                    if (result == true)
                    {
                        MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                        MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Saved Successfully", "false"));
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

        protected void rgAbsentReason_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
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
