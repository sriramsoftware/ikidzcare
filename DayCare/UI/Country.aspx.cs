using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace DayCare.UI
{
    public partial class Country : System.Web.UI.Page
    {
        RadAjaxManager MasterAjaxManager;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["SchoolId"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
        }

        protected void rgCountries_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DayCareBAL.CountryService proxyCountry = new DayCareBAL.CountryService();
            rgCountries.DataSource = proxyCountry.LoadCountries();
        }

        protected void rgCountries_InsertCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            bool isValid = SubmitRecord(source, e);
            if (isValid == false)
            {
                e.Canceled = true;
            }
            rgCountries.MasterTableView.Rebind();
            rgCountries.MasterTableView.CurrentPageIndex = 0;
            
        }

        protected void rgCountries_ItemCreated(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            try
            {
                if (e.Item is GridEditableItem && e.Item.IsInEditMode)
                {
                    GridEditableItem item = e.Item as GridEditableItem;
                    if (item != null)
                    {
                        GridTextBoxColumnEditor editor = (GridTextBoxColumnEditor)item.EditManager.GetColumnEditor("Name");

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
                                    validator.ErrorMessage = "Please enter name of country\n";
                                    validator.SetFocusOnError = true;
                                    validator.Display = ValidatorDisplay.None;
                                }
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
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.Country, "rgCountries_ItemCreated", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
            }
        }

        protected void rgCountries_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {

        }

        protected void rgCountries_UpdateCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            bool isValid = SubmitRecord(source, e);
            if (isValid == false)
            {
                e.Canceled = true;
            }
            e.Item.Expanded = false;
            rgCountries.MasterTableView.Rebind();
            rgCountries.MasterTableView.CurrentPageIndex = 0;
           

        }

        protected void rgCountries_EditCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            GridDataItem item = (GridDataItem)e.Item;
        }

        public bool SubmitRecord(object source, GridCommandEventArgs e)
        {
            bool result = false;

            try
            {
                DayCareBAL.CountryService proxyCountry = new DayCareBAL.CountryService();
                DayCarePL.CountryProperties objCountry = new DayCarePL.CountryProperties();

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
                                            objCountry.Name = (editor as GridTextColumnEditor).Text.Trim().ToString();
                                            ViewState["Name"] = objCountry.Name;
                                            break;
                                        }
                                }
                            }
                        }
                    }
                    if (e.CommandName != "PerformInsert")
                    {
                        objCountry.Id = new Guid(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Id"].ToString());
                        if (!objCountry.Name.Trim().Equals(hdnName.Value.Trim()))
                        {
                            bool ans = Common.CheckDuplicate("Country", "Name", objCountry.Name, "update", objCountry.Id.ToString());
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
                        bool ans = Common.CheckDuplicate("Country", "Name", objCountry.Name, "insert", "");
                        if (ans)
                        {
                            MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                            MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Already Exist", "false"));
                            return false;
                        }
                    }
                    hdnName.Value = "";
                    

                    result = proxyCountry.Save(objCountry);
                    if (result)
                    {
                        MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                        MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Saved Successfully", "false"));
                    }
                   
                }
            }
            catch(Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.Country, "SubmitRecord", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
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
