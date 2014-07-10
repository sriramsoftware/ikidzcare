using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace DayCare.UI
{
    public partial class ChargeCode : System.Web.UI.Page
    {
        RadAjaxManager MasterAjaxManager;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["SchoolId"] == null || Session["CurrentSchoolYearId"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
        }

        protected void rgChargeCode_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                DayCareBAL.ChargeCodeService proxyLoad = new DayCareBAL.ChargeCodeService();
                rgChargeCode.DataSource = proxyLoad.LoadChargeCode();
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.ChargeCode, "rgChargeCode_NeedDataSource", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
            }
        }

        protected void rgChargeCode_EditCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            GridDataItem item = (GridDataItem)e.Item;
            hdnName.Value = item["Name"].Text;
        }

        protected void rgChargeCode_InsertCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            bool isValid = SubmitRecord(source, e);
            {
                if (isValid == false)
                {
                    e.Canceled = true;
                }
            }
            rgChargeCode.MasterTableView.CurrentPageIndex = 0;
        }

        protected void rgChargeCode_ItemCreated(object sender, Telerik.Web.UI.GridItemEventArgs e)
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
                        //GridTextBoxColumnEditor editor = (GridTextBoxColumnEditor)item.EditManager.GetColumnEditor("Name");
                        TextBox txtName = item["Name"].FindControl("txtChargeCodeName") as TextBox;
                        DropDownList ddlCategory = item["Category"].FindControl("ddlCategory") as DropDownList;
                        ImageButton cmdEdit = (ImageButton)item["Edit"].Controls[0];
                        if (txtName != null)
                        {
                            TableCell cell = (TableCell)txtName.Parent;
                            TableCell cell1 = (TableCell)ddlCategory.Parent;
                            RequiredFieldValidator validator = new RequiredFieldValidator();
                            RequiredFieldValidator validator1 = new RequiredFieldValidator();
                            if (cell != null)
                            {
                                txtName.ID = "Name";
                                validator.ControlToValidate = txtName.ID;
                                validator.ErrorMessage = "Please enter ChargeCode\n";
                                validator.SetFocusOnError = true;
                                validator.Display = ValidatorDisplay.None;
                            }
                            if (cell1 != null)
                            {
                                ddlCategory.ID = "ddlCategory";
                                validator1.ControlToValidate = ddlCategory.ID;
                                validator1.ErrorMessage = "Please select Category\n";
                                validator1.InitialValue = "-1";
                                validator1.SetFocusOnError = true;
                                validator1.Display = ValidatorDisplay.None;
                            }
                            ValidationSummary validationsum = new ValidationSummary();
                            validationsum.ID = "validationsum1";
                            validationsum.ShowMessageBox = true;
                            validationsum.ShowSummary = false;
                            validationsum.DisplayMode = ValidationSummaryDisplayMode.SingleParagraph;
                            cell.Controls.Add(validator);
                            cell.Controls.Add(validationsum);
                            cell1.Controls.Add(validator1);
                            cell1.Controls.Add(validationsum);
                        }
                    }

                }
            }
            catch(Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.ChargeCode, "rgChargeCode_ItemCreated", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
            }
        }

        protected void rgChargeCode_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == GridItemType.AlternatingItem || e.Item.ItemType == GridItemType.Item)
                {
                    DayCarePL.ChargeCodeProperties objChargeCode = e.Item.DataItem as DayCarePL.ChargeCodeProperties;
                    RadioButton rbDebit = e.Item.FindControl("rbDebit") as RadioButton;
                    RadioButton rbCredit = e.Item.FindControl("rbCredit") as RadioButton;
                    Label lblCategory = e.Item.FindControl("lblCategory") as Label;

                    if (objChargeCode != null)
                    {
                        switch (objChargeCode.Category)
                        {
                            case "Fees":
                                {
                                    lblCategory.Text = objChargeCode.Category;
                                    break;
                                }
                            case "Payment":
                                {
                                    lblCategory.Text = objChargeCode.Category;
                                    break;
                                }
                            case "1":
                                {
                                    lblCategory.Text = "Fees";
                                    break;
                                }
                            case "2":
                                {
                                    lblCategory.Text = "Payment";
                                    break;
                                }
                            default:
                                {
                                    lblCategory.Text = objChargeCode.Category;
                                    break;
                                }
                        }

                        if (objChargeCode.DebitCrdit)
                        {
                            rbDebit.Checked = true;
                        }
                        else
                        {
                            rbCredit.Checked = true;
                        }
                    }
                }
                if (e.Item.ItemType == GridItemType.EditItem)
                {
                    GridEditableItem item = e.Item as GridEditableItem;
                    DayCarePL.ChargeCodeProperties objChargeCode = e.Item.DataItem as DayCarePL.ChargeCodeProperties;
                    if (objChargeCode != null)
                    {
                        DropDownList ddlCategory = e.Item.FindControl("ddlCategory") as DropDownList;
                        ddlCategory.SelectedValue = objChargeCode.Category;
                    }
                }
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.ChargeCode, "rgChargeCode_ItemDataBound", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
            }
        }

        protected void rgChargeCode_UpdateCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            bool isValid = SubmitRecord(source, e);
            {
                if (isValid == false)
                {
                    e.Canceled = true;
                }
            }
            e.Item.Expanded = false;
            rgChargeCode.MasterTableView.Rebind();
            rgChargeCode.MasterTableView.CurrentPageIndex = 0;
        }

        public bool SubmitRecord(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.ChargeCode, "SubmitRecord", "Submit record method called", DayCarePL.Common.GUID_DEFAULT);
            bool result = false;
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.ChargeCode, "SubmitRecord", " Debug Submit record method called of ChargeCode", DayCarePL.Common.GUID_DEFAULT);

                DayCareBAL.ChargeCodeService proxySave = new DayCareBAL.ChargeCodeService();
                DayCarePL.ChargeCodeProperties objChargeCode = new DayCarePL.ChargeCodeProperties();

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
                                            objChargeCode.Name = (item["Name"].Controls[1] as TextBox).Text;
                                            ViewState["Name"] = objChargeCode.Name;
                                            break;
                                        }
                                    case "Category":
                                        {
                                            objChargeCode.Category = (e.Item.FindControl("ddlCategory") as DropDownList).SelectedValue.Trim().ToString();
                                            ViewState["Category"] = objChargeCode.Category;
                                            break;
                                        }
                                    case "Debit":
                                        {
                                            if ((e.Item.FindControl("rbDebit") as RadioButton).Checked == true)
                                            {
                                                objChargeCode.DebitCrdit = (e.Item.FindControl("rbDebit") as RadioButton).Checked;
                                            }

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
                            objChargeCode.LastModifiedById = new Guid(Session["StaffId"].ToString());
                            objChargeCode.CreatedById = new Guid(Session["StaffId"].ToString());
                        }
                        objChargeCode.Id = new Guid(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Id"].ToString());
                        if (!objChargeCode.Name.Trim().Equals(hdnName.Value.Trim()))
                        {
                            bool ans = Common.CheckDuplicate("ChargeCode", "Name", objChargeCode.Name, "update", objChargeCode.Id.ToString());
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
                        if (Session["StaffId"] != null)
                        {
                            objChargeCode.LastModifiedById = new Guid(Session["StaffId"].ToString());
                            objChargeCode.CreatedById = new Guid(Session["StaffId"].ToString());
                        }
                        bool ans = Common.CheckDuplicate("ChargeCode", "Name", objChargeCode.Name, "insert", objChargeCode.Id.ToString());
                        if (ans)
                        {
                            MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                            MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Already Exist", "false"));
                            return false;
                        }

                    }
                    hdnName.Value = "";
                    result = proxySave.Save(objChargeCode);
                    if (result == true)
                    {
                        MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                        MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Saved Successfully", "false"));
                    }
                }

            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.ChargeCode, "SubmitRecord", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
            }
            return result;
        }

        protected void rgCharge_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
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
