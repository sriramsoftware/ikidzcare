using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace DayCare.UI
{
    public partial class SchoolProgram : System.Web.UI.Page
    {
        RadAjaxManager MasterAjaxManager;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["SchoolId"] == null && Session["CurrentSchoolYearId"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
        }

        protected void rgSchoolProgram_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DayCareBAL.SchoolProgramService proxySchoolProgram = new DayCareBAL.SchoolProgramService();
            //Guid SchoolId = new Guid();
            //if (Session["SchoolId"] != null)
            // {
            //     SchoolId = new Guid(Session["SchoolId"].ToString());
            // }
            //Guid CurrentSchoolYearId = new Guid();
            //if (Session["CurrentSchoolYearId"] != null)
            //{
            //    CurrentSchoolYearId = new Guid(Session["CurrentSchoolYearId"].ToString());
            // }
            rgSchoolProgram.DataSource = proxySchoolProgram.LoadSchoolProgram(GetSchoolId(), GetCurrentSchoolYearId());
        }

        protected void rgSchoolProgram_EditCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            GridDataItem item = (GridDataItem)e.Item;

            lblName.Text = (item["Title"].FindControl("lblTitle") as Label).Text;
            //hdnName.Value = (item["Title"].Controls[0] as Label).Text ;
        }

        protected void rgSchoolProgram_InsertCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            bool isValid = SubmitRecord(source, e);
            {
                if (isValid == false)
                {
                    e.Canceled = true;
                }
            }
            rgSchoolProgram.MasterTableView.CurrentPageIndex = 0;
        }

        protected void rgSchoolProgram_ItemCreated(object sender, Telerik.Web.UI.GridItemEventArgs e)
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
                        DropDownList ddlFeesPeriodName = item["FeesPeriodName"].FindControl("ddlFeesPeriodName") as DropDownList;
                        if (ddlFeesPeriodName != null)
                        {
                            TableCell cell = (TableCell)ddlFeesPeriodName.Parent;
                            validator = new RequiredFieldValidator();
                            if (cell != null)
                            {
                                ddlFeesPeriodName.ID = "ddlFeesPeriodName";
                                validator.ControlToValidate = ddlFeesPeriodName.ID;
                                validator.ErrorMessage = "Please select Day.\n";
                                validator.SetFocusOnError = true;
                                validator.InitialValue = DayCarePL.Common.GUID_DEFAULT;
                                validator.Display = ValidatorDisplay.None;
                                cell.Controls.Add(validator);
                                cell.Controls.Add(validationsum);
                            }
                        }
                        TextBox txtTitle = item["Title"].FindControl("txtTitle") as TextBox;
                        if (txtTitle != null)
                        {
                            TableCell cell = (TableCell)txtTitle.Parent;
                            validator = new RequiredFieldValidator();
                            if (cell != null)
                            {
                                txtTitle.ID = "txtTitle";
                                validator.ControlToValidate = txtTitle.ID;
                                validator.ErrorMessage = "Please Enter Title.\n";
                                validator.SetFocusOnError = true;
                                validator.Display = ValidatorDisplay.None;
                                cell.Controls.Add(validator);
                                cell.Controls.Add(validationsum);
                            }
                        }
                        TextBox txtFees = item["Fees"].FindControl("txtFees") as TextBox;
                        if (txtFees != null)
                        {
                            TableCell cell = (TableCell)txtFees.Parent;
                            validator = new RequiredFieldValidator();
                            if (cell != null)
                            {
                                txtFees.ID = "txtFees";
                                validator.ControlToValidate = txtFees.ID;
                                validator.ErrorMessage = "Please Enter Fees.\n";
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

        protected void rgSchoolProgram_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {

            if (e.Item.ItemType == GridItemType.AlternatingItem || e.Item.ItemType == GridItemType.Item)
            {
                DayCarePL.SchoolProgramProperties objSchoolProgram = e.Item.DataItem as DayCarePL.SchoolProgramProperties;
                HyperLink hlProgClassCategory = e.Item.FindControl("hlProgClassCategory") as HyperLink;
                HyperLink hlProgSchedule = e.Item.FindControl("hlProgSchedule") as HyperLink;
                HyperLink hlProgStaff = e.Item.FindControl("hlProgStaff") as HyperLink;
                HyperLink hlProgClassRoom = e.Item.FindControl("hlProgClassRoom") as HyperLink;

                string PageName = "";
                //hlProgClassCategory.Attributes.Add("onclick", "ShowProgramClassCategory('" + objSchoolProgram.Id + "','" + objSchoolProgram.SchoolYearId + "','" + objSchoolProgram.IsPrimary + "','" + objSchoolProgram.Title + "'); return false;");
                //hlProgSchedule.Attributes.Add("onclick", "ShowProgSchedule('" + objSchoolProgram.Id + "','" + objSchoolProgram.SchoolYearId + "','" + objSchoolProgram.IsPrimary + "','" + objSchoolProgram.Title + "'); return false;");
                //hlProgStaff.Attributes.Add("onclick", "ShowProgramStaff('" + objSchoolProgram.Id + "','" + objSchoolProgram.SchoolYearId + "','" + objSchoolProgram.IsPrimary + "','" + objSchoolProgram.Title + "'); return false;");
                PageName = "ProgramClassRoom.aspx?SchoolProgramId='" + objSchoolProgram.Id + "'&SchoolYearId='" + objSchoolProgram.SchoolYearId + "'&IsPrimary='" + objSchoolProgram.IsPrimary + "'";
                hlProgClassRoom.Attributes.Add("onclick", "ShowProgramClassRoom('" + objSchoolProgram.Id + "','" + objSchoolProgram.SchoolYearId + "','" + objSchoolProgram.IsPrimary + "','" + objSchoolProgram.Title + "'); return false;");
                //hlProgClassRoom.Attributes.Add("onclick", "ShowShortcut('" + PageName + "'); return false;");
            }
            if (e.Item.ItemType == GridItemType.EditItem)
            {
                GridEditableItem Itms = e.Item as GridEditableItem;
                DayCarePL.SchoolProgramProperties objSchoolProgram = e.Item.DataItem as DayCarePL.SchoolProgramProperties;
                DayCareBAL.SchoolProgramService proxySchoolProgram = new DayCareBAL.SchoolProgramService();
                DropDownList ddlFeesPeriodName = e.Item.FindControl("ddlFeesPeriodName") as DropDownList;
                CheckBox chkIsPrimary = Itms["IsPrimary"].Controls[0] as CheckBox;
                Common.BindFeesPeriod(ddlFeesPeriodName);
                if (objSchoolProgram != null)
                {
                    if (ddlFeesPeriodName != null && ddlFeesPeriodName.Items.Count > 0)
                    {
                        ddlFeesPeriodName.SelectedValue = Convert.ToString(objSchoolProgram.FeesPeriodId);
                    }
                    if (objSchoolProgram.IsPrimary)
                    {
                        if (proxySchoolProgram.CheckSchoolProgramInChildSchedule(GetSchoolId(), objSchoolProgram.Id))
                        {
                            chkIsPrimary.Enabled = false;
                            chkIsPrimary.ToolTip = "You can not change, It is used in Child Schedule";
                        }
                    }
                }
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

        protected void rgSchoolProgram_UpdateCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            bool isValid = SubmitRecord(source, e);
            {
                if (isValid == false)
                {
                    e.Canceled = true;
                }
            }
            e.Item.Expanded = false;
            rgSchoolProgram.MasterTableView.Rebind();
            rgSchoolProgram.MasterTableView.CurrentPageIndex = 0;
        }

        public bool SubmitRecord(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.ClassRoom, "SubmitRecord", "Submit record method called", DayCarePL.Common.GUID_DEFAULT);
            bool result = false;
            Guid Id;
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.ClassRoom, "SubmitRecord", " Debug Submit record method called of ClassRoom", DayCarePL.Common.GUID_DEFAULT);

                DayCareBAL.SchoolProgramService proxySchoolProgram = new DayCareBAL.SchoolProgramService();
                DayCarePL.SchoolProgramProperties objSchoolProgram = new DayCarePL.SchoolProgramProperties();

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
                                    case "Title":
                                        {
                                            objSchoolProgram.Title = (e.Item.FindControl("txtTitle") as TextBox).Text;
                                            break;
                                        }
                                    //case "Fees":
                                    //    {
                                    //        objSchoolProgram.Fees = Convert.ToDouble((e.Item.FindControl("txtFees") as TextBox).Text);
                                    //        break;
                                    //    }
                                    //case "FeesPeriodName":
                                    //    {

                                    //        objSchoolProgram.FeesPeriodId = new Guid((e.Item.FindControl("ddlFeesPeriodName") as DropDownList).SelectedValue);
                                    //        break;
                                    //    }
                                    case "Comments":
                                        {

                                            objSchoolProgram.Comments = (e.Item.FindControl("txtComments") as TextBox).Text;
                                            break;
                                        }
                                    case "Active":
                                        {
                                            objSchoolProgram.Active = (editor as GridCheckBoxColumnEditor).Value;
                                            break;
                                        }
                                    case "IsPrimary":
                                        {
                                            objSchoolProgram.IsPrimary = (editor as GridCheckBoxColumnEditor).Value;
                                            break;
                                        }
                                }
                            }
                        }
                    }

                    if (Session["CurrentSchoolYearId"] != null)
                    {
                        objSchoolProgram.SchoolYearId = new Guid(Session["CurrentSchoolYearId"].ToString());
                    }
                    if (e.CommandName != "PerformInsert")
                    {
                        if (Session["StaffId"] != null)
                        {
                            objSchoolProgram.LastModifiedById = new Guid(Session["StaffId"].ToString());
                        }
                        objSchoolProgram.Id = new Guid(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Id"].ToString());
                    }
                    else
                    {
                        if (Session["StaffId"] != null)
                        {
                            objSchoolProgram.CreatedById = new Guid(Session["StaffId"].ToString());
                        }
                    }
                    if (!lblName.Text.ToLower().Equals(objSchoolProgram.Title.ToLower()))
                    {
                        Guid SchoolId = new Guid();
                        if (Session["SchoolId"] != null)
                        {
                            SchoolId = new Guid(Session["SchoolId"].ToString());
                        }

                        if (proxySchoolProgram.CheckDuplicateSchoolProgramName(objSchoolProgram.Title, SchoolId))
                        {
                            MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                            MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Title already exists", "false"));
                            return false;
                        }
                    }
                    lblName.Text = "";
                    //result = proxySchoolProgram.Save(objSchoolProgram);
                    Id = proxySchoolProgram.Save(objSchoolProgram);
                    if (!Id.ToString().Equals(DayCarePL.Common.GUID_DEFAULT))
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

        protected void rgSchoolProgram_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
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
            //else
            //{
            //    if (e.CommandName == "ProgClassCategory")
            //    {
            //        Response.Redirect("");
            //    }
            //    else if (e.CommandName == "ProgSchedule")
            //    {

            //    }
            //    else if (e.CommandName == "ProgStaff")
            //    {

            //    }
            //    else if (e.CommandName == "ProgClassRoom")
            //    {

            //    }
            //}
        }

        public Guid GetSchoolId()
        {
            if (Session["SchoolId"] != null)
            {
                return new Guid(Session["SchoolId"].ToString());
            }
            else
            {
                return new Guid();
            }
        }

        public Guid GetCurrentSchoolYearId()
        {
            if (Session["CurrentSchoolYearId"] != null)
            {
                return new Guid(Session["CurrentSchoolYearId"].ToString());
            }
            else
            {
                return new Guid();
            }
        }
    }
}
