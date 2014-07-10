using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace DayCare.UI
{
    public partial class FeesPeriod : System.Web.UI.Page
    {
        RadAjaxManager MasterAjaxManager;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["SchoolId"] == null || Session["CurrentSchoolYearId"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
        }
        protected void rgFeesPeriod_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DayCareBAL.FeesPeriodService proxyLoad = new DayCareBAL.FeesPeriodService();
            rgFeesPeriod.DataSource = proxyLoad.LoadFeesPeriod();
        }
        protected void rgFeesPeriod_EditCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            GridDataItem item = (GridDataItem)e.Item;
            hdnName.Value = item["Name"].Text;
        }
        protected void rgFeesPeriod_InsertCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            bool isValid = SubmitRecord(source, e);
            if (isValid == false)
            {
                e.Canceled = true;
            }
            rgFeesPeriod.MasterTableView.CurrentPageIndex = 0;
        }
        protected void rgFeesPeriod_ItemCreated(object sender, Telerik.Web.UI.GridItemEventArgs e)
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
                        GridTextBoxColumnEditor Editor = (GridTextBoxColumnEditor)item.EditManager.GetColumnEditor("Name");
                        ImageButton cmdEdit = (ImageButton)item["Edit"].Controls[0];
                        if (Editor != null)
                        {
                            TableCell cell = (TableCell)Editor.TextBoxControl.Parent;
                            RequiredFieldValidator validatior = new RequiredFieldValidator();
                            if (Editor != null)
                            {
                                if (cell != null)
                                {
                                    Editor.TextBoxControl.ID = "Name";
                                    validatior.ControlToValidate = Editor.TextBoxControl.ID;
                                    validatior.ErrorMessage = "Please Enter FeesPeriod\n";
                                    validatior.SetFocusOnError = true;
                                    validatior.Display = ValidatorDisplay.None;

                                }
                                ValidationSummary validationsum = new ValidationSummary();
                                validationsum.ID = "validationsum1";
                                validationsum.ShowMessageBox = true;
                                validationsum.ShowSummary = false;
                                validationsum.DisplayMode = ValidationSummaryDisplayMode.SingleParagraph;
                                cell.Controls.Add(validatior);
                                cell.Controls.Add(validationsum);

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.FeesPeriod, "rgFeesPeriod_ItemCreated", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
            }
        }
        protected void rgFeesPeriod_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
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
        protected void rgFeesPeriod_UpdateCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            bool isvalid = SubmitRecord(source, e);
            if (isvalid == false)
            {
                e.Canceled = true;
            }
            e.Item.Expanded = false;
            rgFeesPeriod.MasterTableView.Rebind();
            rgFeesPeriod.MasterTableView.CurrentPageIndex = 0;
        }
        protected void rgFeesPeriod_DeleteCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
        }
        public bool SubmitRecord(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.FeesPeriod, "SubmitRecord", "Submit record method called", DayCarePL.Common.GUID_DEFAULT);
            bool result = false;
            try
            {

                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.FeesPeriod, "SubmitRecord", "Debug Submit Record Of FeesPeriod", DayCarePL.Common.GUID_DEFAULT);

                DayCareBAL.FeesPeriodService proxySave = new DayCareBAL.FeesPeriodService();
                DayCarePL.FeesPeriodProperties objFeesPeriod = new DayCarePL.FeesPeriodProperties();

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
                                            objFeesPeriod.Name = (editor as GridTextBoxColumnEditor).Text.Trim().ToString();
                                            ViewState["Name"] = objFeesPeriod.Name;
                                            break;
                                        }
                                    case "Active":
                                        {
                                            objFeesPeriod.Active = (editor as GridCheckBoxColumnEditor).Value;
                                            break;
                                        }

                                }
                            }
                        }
                    }
                    if (e.CommandName != "PerformInsert")
                    {
                        if (Session["StaffId"] != null)
                        {
                            objFeesPeriod.LastModifiedById = new Guid(Session["StaffId"].ToString());
                        }
                        objFeesPeriod.Id = new Guid(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Id"].ToString());
                        if (!objFeesPeriod.Name.Trim().Equals(hdnName.Value.Trim()))
                        {
                            bool ans = Common.CheckDuplicate("FeesPeriod", "Name", objFeesPeriod.Name, "update", objFeesPeriod.Id.ToString());
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
                        bool ans = Common.CheckDuplicate("FeesPeriod", "Name", objFeesPeriod.Name, "insert", objFeesPeriod.Id.ToString());
                        if (ans)
                        {
                            MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                            MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Already Exist", "false"));
                            return false;
                        }
                    }
                    hdnName.Value = "";
                    result = proxySave.Save(objFeesPeriod);
                    if (result == true)
                    {
                        MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                        MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Saved Successfully", "false"));
                    }

                }

            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.FeesPeriod, "SubmitRecord", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                result = false;
            }
            return result;
        }

        protected void rgFeesPeriod_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
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
