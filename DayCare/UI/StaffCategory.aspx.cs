using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using Telerik.Web.UI;
using System.Web.UI.WebControls;

namespace DayCare.UI
{
    public partial class StaffCategory : System.Web.UI.Page
    {
        RadAjaxManager MasterAjaxManager;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["SchoolId"] == null || Session["CurrentSchoolYearId"] == null || Session["Role_Id"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
        }

        protected void rgStaffCategory_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DayCareBAL.StaffCategoryService proxyLoad = new DayCareBAL.StaffCategoryService();
            Guid SchoolId = new Guid();
            if (Session["SchoolId"] != null)
            {
                SchoolId = new Guid(Session["SchoolId"].ToString());
            }
            rgStaffCategory.DataSource = proxyLoad.loadStaffCategory(SchoolId);
        }

        protected void rgStaffCategory_EditCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            GridDataItem item = (GridDataItem)e.Item;
            hdnName.Value = item["Name"].Text;
        }

        protected void rgStaffCategory_InsertCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            bool isValid = SubmitRecord(source, e);
            if (isValid == false)
            {
                e.Canceled = true;
            }
            rgStaffCategory.MasterTableView.CurrentPageIndex = 0;
        }

        protected void rgStaffCategory_ItemCreated(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item.ItemType == GridItemType.FilteringItem)
            {
                GridFilteringItem FileterItem = (GridFilteringItem)e.Item;
                for (int i = 0; i < FileterItem.Cells.Count; i++)
                {
                    FileterItem.Cells[i].Style.Add("text-align", "left");
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
                            if (editor != null)
                            {
                                if (cell != null)
                                {
                                    editor.TextBoxControl.ID = "Name";
                                    validator.ControlToValidate = editor.TextBoxControl.ID;
                                    validator.ErrorMessage = "Please Enter Staff Category\n";
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
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.staffCategory, "rgStaffCategory_ItemCreated", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
            }
        }

        protected void rgStaffCategory_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
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

        protected void rgStaffCategory_UpdateCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            bool isvalid = SubmitRecord(source, e);
            if (isvalid == false)
            {
                e.Canceled = true;
            }
            e.Item.Expanded = false;
            rgStaffCategory.MasterTableView.Rebind();
            rgStaffCategory.MasterTableView.CurrentPageIndex = 0;
        }

        public bool SubmitRecord(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.staffCategory, "SubmitRecord", "Submit record method called", DayCarePL.Common.GUID_DEFAULT);
            bool result = false;
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.staffCategory, "SubmitRecord", "Debug Submit Record Of StaffCategory", DayCarePL.Common.GUID_DEFAULT);
                DayCareBAL.StaffCategoryService proxySave = new DayCareBAL.StaffCategoryService();
                DayCarePL.StaffCategoryProperties objStaffCategory = new DayCarePL.StaffCategoryProperties();

                Telerik.Web.UI.GridDataItem item = (Telerik.Web.UI.GridDataItem)e.Item;

                var InsertItem = e.Item as Telerik.Web.UI.GridEditableItem;
                Telerik.Web.UI.GridEditManager editMan = InsertItem.EditManager;
                if (InsertItem != null)
                {
                    foreach (GridColumn column in e.Item.OwnerTableView.RenderColumns)
                    {
                        if (column is IGridEditableColumn)
                        {
                            IGridEditableColumn editablecol = (column as IGridEditableColumn);
                            if (editablecol.IsEditable)
                            {
                                IGridColumnEditor editor = editMan.GetColumnEditor(editablecol);
                                switch (column.UniqueName)
                                {
                                    case "Name":
                                        {
                                            objStaffCategory.Name = (editor as GridTextBoxColumnEditor).Text.Trim().ToString();
                                            ViewState["Name"] = objStaffCategory.Name;
                                            break;
                                        }
                                    case "Comments":
                                        {
                                            objStaffCategory.Comments = (e.Item.FindControl("txtComments") as TextBox).Text.Trim().ToString();
                                            ViewState["Comments"] = objStaffCategory.Comments;
                                            break;
                                        }
                                    case "Active":
                                        {
                                            objStaffCategory.Active = (editor as GridCheckBoxColumnEditor).Value;
                                            break;
                                        }
                                }
                            }
                        }
                    }
                    if (Session["SchoolId"] != null)
                    {
                        objStaffCategory.SchoolId = new Guid(Session["SchoolId"].ToString());
                    }
                    if (e.CommandName != "PerformInsert")
                    {
                        objStaffCategory.Id = new Guid(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Id"].ToString());
                        if (!objStaffCategory.Name.Trim().Equals(hdnName.Value.Trim()))
                        {
                            bool ans = proxySave.CheckDuplicateStaffCategoryName(objStaffCategory.Name, objStaffCategory.Id, objStaffCategory.SchoolId);
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
                        bool ans = proxySave.CheckDuplicateStaffCategoryName(objStaffCategory.Name, objStaffCategory.Id, objStaffCategory.SchoolId);
                        if (ans)
                        {
                            MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                            MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Already Exist", "false"));
                            return false;
                        }
                    }
                    hdnName.Value = "";
                    result = proxySave.Save(objStaffCategory);
                    if (result == true)
                    {
                        MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                        MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Saved Successfully", "false"));
                    }
                }
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.staffCategory, "SubmitRecord", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                result = false;
            }
            return result;

        }

        protected void rgStaffCategory_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
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

            if (Session["UserGroupTitle"] != null && Session["Role_Id"] != null)
            {
                //if (Convert.ToString(Session["UserGroupTitle"]).Equals(DayCarePL.Common.SCHOOL_ADMINISTRATOR) || Convert.ToString(Session["Role_Id"]).ToLower().Equals(DayCarePL.Common.ADMIN_ROLE_ID.ToLower()))
                //{
                //    if (!Common.IsCurrentYear(CurrentSchoolYearId, SchoolId))
                //    {
                //        if (e.CommandName == "InitInsert")
                //        {
                //            e.Canceled = true;
                //        }
                //        else if (e.CommandName == "Edit")
                //        {
                //            e.Canceled = true;
                //        }
                //    }
                //}
                //else
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
            else
            {
                Response.Redirect("~/Login.aspx");
            }
        }
    }
}
