using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using Telerik.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

namespace DayCare.UI
{
    public partial class Font : System.Web.UI.Page
    {
        RadAjaxManager MasterAjaxManager;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["SchoolId"] == null && Session["CurrentSchoolYearId"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
        }

        protected void rgFont_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DayCareBAL.FontService proxyFont = new DayCareBAL.FontService();
            rgFont.DataSource = proxyFont.LoadFont();
        }

        protected void rgFont_EditCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            GridDataItem item = (GridDataItem)e.Item;
            hdnName.Value = item["Name"].Text;
        }

        protected void rgFont_InsertCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            bool isValid = SubmitRecord(source, e);
            {
                if (isValid == false)
                {
                    e.Canceled = true;
                }
            }
            rgFont.MasterTableView.CurrentPageIndex = 0;
        }

        protected void rgFont_ItemCreated(object sender, Telerik.Web.UI.GridItemEventArgs e)
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
                    GridTextBoxColumnEditor editor;
                    if (item != null)
                    {
                        editor = (GridTextBoxColumnEditor)item.EditManager.GetColumnEditor("Name");
                        ImageButton cmdEdit = (ImageButton)item["Edit"].Controls[0];
                        ValidationSummary validationsum = new ValidationSummary();
                        validationsum.ID = "validationsum1";
                        validationsum.ShowMessageBox = true;
                        validationsum.ShowSummary = false;
                        validationsum.DisplayMode = ValidationSummaryDisplayMode.SingleParagraph;
                        if (editor != null)
                        {
                            TableCell cell = (TableCell)editor.TextBoxControl.Parent;
                            validator = new RequiredFieldValidator();
                            if (cell != null)
                            {
                                editor.TextBoxControl.ID = "Name";
                                validator.ControlToValidate = editor.TextBoxControl.ID;
                                validator.ErrorMessage = "Please Enter Name \n";
                                validator.SetFocusOnError = true;
                                validator.Display = ValidatorDisplay.None;
                            }

                            cell.Controls.Add(validator);
                            cell.Controls.Add(validationsum);

                        }
                        TextBox txtSize = e.Item.FindControl("txtSize") as TextBox;
                        if (txtSize != null)
                        {
                            TableCell cell = (TableCell)txtSize.Parent;
                            validator = new RequiredFieldValidator();
                            if (cell != null)
                            {
                                txtSize.ID = "txtSize";
                                validator.ControlToValidate = txtSize.ID;
                                validator.ErrorMessage = "Please Enter Size\n";
                                validator.SetFocusOnError = true;
                                validator.Display = ValidatorDisplay.None;
                            }

                            cell.Controls.Add(validator);
                            cell.Controls.Add(validationsum);

                        }
                        //RadColorPicker rcpColor = e.Item.FindControl("rcpColor") as RadColorPicker;
                        //if (rcpColor != null)
                        //{
                        //    TableCell cell = (TableCell)rcpColor.Parent;
                        //    validator = new RequiredFieldValidator();
                        //    if (cell != null)
                        //    {
                        //        rcpColor.ID = "rcpColor";
                        //        validator.ControlToValidate = rcpColor.ID;
                        //        validator.ErrorMessage = "Please enter size of Font\n";
                        //        validator.InitialValue = "0";
                        //        validator.SetFocusOnError = true;
                        //        validator.Display = ValidatorDisplay.None;
                        //    }

                        //    cell.Controls.Add(validator);
                        //    cell.Controls.Add(validationsum);

                        //}
                    }

                }
            }
            catch
            {

            }
        }

        protected void rgFont_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item.ItemType == GridItemType.EditItem)
            {
                RadColorPicker picker = (RadColorPicker)e.Item.FindControl("rcpColor");
                if (picker != null)
                {
                    DayCarePL.FontProperties objFont = e.Item.DataItem as DayCarePL.FontProperties;
                    if (objFont != null)
                        picker.SelectedColor = ColorTranslator.FromHtml((DataBinder.Eval(objFont, "Color").ToString()));
                }
            }
            if (e.Item.ItemType == GridItemType.Item || e.Item.ItemType == GridItemType.AlternatingItem)
            {
                DayCarePL.FontProperties item = e.Item.DataItem as DayCarePL.FontProperties;
                Label lblColor = e.Item.FindControl("lblColor") as Label;
                lblColor.Height = 25;
                lblColor.Width = 25;
                lblColor.BackColor = System.Drawing.ColorTranslator.FromHtml(item.Color);
            }
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

        protected void rgFont_UpdateCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            bool isValid = SubmitRecord(source, e);
            {
                if (isValid == false)
                {
                    e.Canceled = true;
                }
            }
            e.Item.Expanded = false;
            rgFont.MasterTableView.Rebind();
            rgFont.MasterTableView.CurrentPageIndex = 0;
        }

        public bool SubmitRecord(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.Font, "SubmitRecord", "Submit record method called", DayCarePL.Common.GUID_DEFAULT);
            bool result = false;
            string FontSize = "";
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.Font, "SubmitRecord", " Debug Submit record method called of Font", DayCarePL.Common.GUID_DEFAULT);

                DayCareBAL.FontService proxySave = new DayCareBAL.FontService();
                DayCarePL.FontProperties objFont = new DayCarePL.FontProperties();

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
                                            objFont.Name = (editor as GridTextBoxColumnEditor).Text.Trim().ToString();
                                            ViewState["Name"] = objFont.Name;
                                            break;
                                        }
                                    case "Color":
                                        {
                                            //objFont.Color=(e.Item.FindControl("rcpColor") as RadColorPicker ).
                                            if ((e.Item.FindControl("rcpColor") as RadColorPicker) != null && (e.Item.FindControl("rcpColor") as RadColorPicker).SelectedColor.Name != "0")
                                            {
                                                objFont.Color = "#" + (e.Item.FindControl("rcpColor") as RadColorPicker).SelectedColor.Name;
                                            }

                                            break;
                                        }
                                    case "Size":
                                        {
                                            FontSize = (e.Item.FindControl("txtSize") as TextBox).Text.Trim();
                                            //objFont.Size = Convert.ToInt32((e.Item.FindControl("txtSize") as TextBox).Text.Trim());
                                            ViewState["Size"] = objFont.Size;
                                            break;
                                        }
                                    case "Active":
                                        {
                                            objFont.Active = (editor as GridCheckBoxColumnEditor).Value;
                                            break;
                                        }
                                }
                            }
                        }
                    }
                    int res = 0;
                    Int32.TryParse(FontSize, out res);
                    if (res == 0)
                    {
                        MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                        MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Font size must be numeric.", "false"));
                        return false;
                    }
                    else
                    {
                        objFont.Size = Convert.ToInt32(FontSize);
                    }
                    if (string.IsNullOrEmpty(objFont.Color))
                    {
                        MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                        MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Please select color.", "false"));
                        return false;
                    }
                    if (e.CommandName != "PerformInsert")
                    {
                        //objFont.Id = new Guid(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Id"].ToString());
                        objFont.Id = new Guid(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Id"].ToString());
                        if (!objFont.Name.Trim().Equals(hdnName.Value.Trim()))
                        {
                            bool ans = Common.CheckDuplicate("Font", "Name", objFont.Name, "update", objFont.Id.ToString());
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
                        bool ans = Common.CheckDuplicate("Font", "Name", objFont.Name, "insert", objFont.Id.ToString());
                        if (ans)
                        {
                            MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                            MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Already Exist", "false"));
                            return false;
                        }
                    }
                    hdnName.Value = "";
                    result = proxySave.Save(objFont);
                    if (result == true)
                    {
                        MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                        MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Saved Successfully", "false"));
                    }
                }

            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.Font, "SubmitRecord", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                result = false;
            }
            return result;
        }

        protected void rgFont_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
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
