using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace DayCare.UI
{
    public partial class State : System.Web.UI.Page
    {
        RadAjaxManager MasterAjaxManager;
        protected void Page_Load(object sender, EventArgs e)
        {
            //Session["SchoolId"] = "8CA767A0-5E36-4343-8B1D-5ECC40EB9E1B"; 
            if (Session["SchoolId"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
        }

        protected void rgStates_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DayCareBAL.StateService proxyState = new DayCareBAL.StateService();
            rgStates.DataSource = proxyState.LoadStates();
        }

        protected void rgStates_InsertCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            bool isValid = SubmitRecord(source, e);
            if (isValid == false)
            {
                e.Canceled = true;
            }
            rgStates.MasterTableView.Rebind();
            rgStates.MasterTableView.CurrentPageIndex = 0;
        }

        protected void rgStates_ItemCreated(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            try
            {
                if (e.Item is GridEditableItem && e.Item.IsInEditMode)
                {
                    GridEditableItem item = e.Item as GridEditableItem;
                    if (item != null)
                    {
                        TextBox txtName = item["Name"].FindControl("txtState") as TextBox;
                        DropDownList ddlCountry = item["CountryName"].FindControl("ddlCountry") as DropDownList;
                        if (txtName != null && ddlCountry != null)
                        {
                            TableCell cell = (TableCell)txtName.Parent;
                            TableCell cell1 = (TableCell)ddlCountry.Parent;
                            RequiredFieldValidator validator = new RequiredFieldValidator();
                            RequiredFieldValidator validator1 = new RequiredFieldValidator();

                            if (cell != null)
                            {
                                txtName.ID = "Name";
                                validator.ControlToValidate = txtName.ID;
                                validator.ErrorMessage = "Please enter name of state\n";
                                validator.SetFocusOnError = true;
                                validator.Display = ValidatorDisplay.None;
                            }
                            if (cell1 != null)
                            {
                                ddlCountry.ID = "ddlCountry";
                                validator1.ControlToValidate = ddlCountry.ID;
                                validator1.InitialValue = "00000000-0000-0000-0000-000000000000";
                                validator1.ErrorMessage = "Please select country\n";
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
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.State, "rgStates_ItemCreated", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
            }
        }

        protected void rgStates_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item.ItemType == GridItemType.EditItem)
            {
                DayCarePL.StateProperties objState = e.Item.DataItem as DayCarePL.StateProperties;
                DropDownList ddlCountry = e.Item.FindControl("ddlCountry") as DropDownList; //(DropDownList)gridEditFormItem["Name"].FindControl("ddlCountry");
                Common.BindCountryDropDown(ddlCountry);
                if (ddlCountry.Items != null && ddlCountry.Items.Count > 0)
                {
                    if (objState != null)
                    {
                        ddlCountry.SelectedValue = objState.CountryId.ToString();
                    }
                }
            }
        }

        protected void rgStates_UpdateCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            bool isValid = SubmitRecord(source, e);
            if (isValid == false)
            {
                e.Canceled = true;
            }
            e.Item.Expanded = false;
            rgStates.MasterTableView.Rebind();
            rgStates.MasterTableView.CurrentPageIndex = 0;
        }

        protected void rgStates_EditCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            GridDataItem item = (GridDataItem)e.Item;
            hdnName.Value = (item["Name"].FindControl("lblName") as Label).Text;
        }

        public bool SubmitRecord(object source, GridCommandEventArgs e)
        {
            bool result = false;

            try
            {
                DayCareBAL.StateService proxyState = new DayCareBAL.StateService();
                DayCarePL.StateProperties objState = new DayCarePL.StateProperties();

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
                                            objState.Name = (item["Name"].Controls[1] as TextBox).Text;
                                            break;
                                        }
                                    case "CountryName":
                                        {
                                            objState.CountryId = new Guid((e.Item.FindControl("ddlCountry") as DropDownList).SelectedValue);
                                            break;
                                        }
                                }
                            }
                        }
                    }
                    if (e.CommandName != "PerformInsert")
                    {
                        objState.Id = new Guid(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Id"].ToString());
                        if (!objState.Name.Trim().Equals(hdnName.Value.Trim()))
                        {
                            bool ans = Common.CheckDuplicate("State", "Name", objState.Name, "update", objState.Id.ToString());
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
                        bool ans = Common.CheckDuplicate("State", "Name", objState.Name, "insert", "");
                        if (ans)
                        {
                            MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                            MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Already Exist", "false"));
                            return false;
                        }
                    }
                    hdnName.Value = "";


                    result = proxyState.Save(objState);
                    if (result)
                    {
                        MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                        MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Saved Successfully", "false"));
                    }

                }
            }
            catch(Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.State, "SubmitRecord", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                result = false;
            }
            return result;
        }

        #region "ShowMessage"
        public void ShowMessage(string Msg, System.Web.UI.Page ObjPage)
        {
            char s1 = '"';

            string s = "alert(" + s1 + Msg + s1 + ");";

            ScriptManager.RegisterClientScriptBlock(ObjPage, this.GetType(), "ShowMessage", s, true);
        }
        #endregion
    }
}
