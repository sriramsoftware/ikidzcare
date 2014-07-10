using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using Telerik.Web.UI;
using System.Web.UI.WebControls;

namespace DayCare.UI
{
    public partial class ClassCategory : System.Web.UI.Page
    {
        RadAjaxManager MasterAjaxManager;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["SchoolId"] == null || Session["CurrentSchoolYearId"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
        }
        protected void rgClassCategory_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                DayCareBAL.ClassCategoryService proxyLoad = new DayCareBAL.ClassCategoryService();
                Guid SchoolId = new Guid();
                if (Session["SchoolId"] != null)
                {
                    SchoolId = new Guid(Session["SchoolId"].ToString());
                }
                rgClassCategory.DataSource = proxyLoad.LoadClassCategory(SchoolId);
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.ClassCategory, "rgClassCategory_NeedDataSource", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
            }
        }
        protected void rgClassCategory_EditCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            GridDataItem item = (GridDataItem)e.Item;
            hdnName.Value = item["Name"].Text;
        }
        protected void rgClassCategory_InsertCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            bool isValid = SubmitRecord(source, e);
            {
                if (isValid == false)
                {
                    e.Canceled = true;
                }
            }
            rgClassCategory.MasterTableView.CurrentPageIndex = 0;
        }
        protected void rgClassCategory_ItemCreated(object sender, Telerik.Web.UI.GridItemEventArgs e)
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
                        GridTextBoxColumnEditor editor = (GridTextBoxColumnEditor)item.EditManager.GetColumnEditor("Name");
                        ImageButton cmdEdit = (ImageButton)item["Edit"].Controls[0];
                        if (editor != null)
                        {
                            TableCell cell = (TableCell)editor.TextBoxControl.Parent;
                            RequiredFieldValidator validator = new RequiredFieldValidator();
                            if (cell != null)
                            {
                                editor.TextBoxControl.ID = "Name";
                                validator.ControlToValidate = editor.TextBoxControl.ID;
                                validator.ErrorMessage = "Please Enter Category\n";
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
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.ClassCategory, "rgClassCategory_ItemCreated", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
            }
        }
        protected void rgClassCategory_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
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
        protected void rgClassCategory_UpdateCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            bool isValid = SubmitRecord(source, e);
            {
                if (isValid == false)
                {
                    e.Canceled = true;
                }
            }
            e.Item.Expanded = false;
            rgClassCategory.MasterTableView.Rebind();
            rgClassCategory.MasterTableView.CurrentPageIndex = 0;
        }
        public bool SubmitRecord(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.ClassCategory, "SubmitRecord", "Submit record method called", DayCarePL.Common.GUID_DEFAULT);
            bool result = false;
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.ClassCategory, "SubmitRecord", " Debug Submit record method called of ClassCategory", DayCarePL.Common.GUID_DEFAULT);

                DayCareBAL.ClassCategoryService proxySave = new DayCareBAL.ClassCategoryService();
                DayCarePL.ClassCategoryProperties objClassCategory = new DayCarePL.ClassCategoryProperties();

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
                                    case "Name":
                                        {
                                            objClassCategory.Name = (editor as GridTextBoxColumnEditor).Text.Trim().ToString();
                                            ViewState["Name"] = objClassCategory.Name;
                                            break;
                                        }
                                    case "Active":
                                        {
                                            objClassCategory.Active = (editor as GridCheckBoxColumnEditor).Value;
                                            break;
                                        }
                                    case "Comments":
                                        {
                                            objClassCategory.Comments = (e.Item.FindControl("txtComments") as TextBox).Text;
                                            ViewState["Comments"] = objClassCategory.Comments;
                                            break;
                                        }
                                }
                            }
                        }
                    }
                    if (Session["SchoolId"] != null)
                    {
                        objClassCategory.SchoolId = new Guid(Session["SchoolId"].ToString());
                    }
                    if (e.CommandName != "PerformInsert")
                    {
                        if (Session["StaffId"] != null)
                        {
                            objClassCategory.LastModifiedById = new Guid(Session["StaffId"].ToString());
                        }
                        objClassCategory.Id = new Guid(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Id"].ToString());
                        if (!objClassCategory.Id.ToString().Equals(hdnName.Value.Trim()))
                        {
                            //bool ans = Common.CheckDuplicate("ClassCategory", "Name", objClassCategory.Name, "update", objClassCategory.Id.ToString());
                            bool ans = proxySave.CheckDuplicateClassCategory(objClassCategory.Name, objClassCategory.Id, objClassCategory.SchoolId);
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
                        //bool ans = Common.CheckDuplicate("ClassCategory", "Name", objClassCategory.Name, "insert", objClassCategory.Id.ToString());
                        bool ans = proxySave.CheckDuplicateClassCategory(objClassCategory.Name, objClassCategory.Id, objClassCategory.SchoolId);
                        if (ans)
                        {
                            MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                            MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Already Exist", "false"));
                            return false;
                        }
                    }
                    hdnName.Value = "";
                    result = proxySave.Save(objClassCategory);
                    if (result == true)
                    {
                        MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                        MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Saved Successfully", "false"));
                    }
                }
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.ClassCategory, "SubmitRecord", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                result = false;
            }
            return result;
        }

        protected void rgClassCategory_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
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
    }
}
