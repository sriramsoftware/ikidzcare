using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace DayCare.UI
{
    public partial class SchoolYear : System.Web.UI.Page
    {
        RadAjaxManager MasterAjaxManager;
        DayCarePL.SchoolYearProperties objCurrentYear = new DayCarePL.SchoolYearProperties();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["SchoolId"] == null || Session["CurrentSchoolYearId"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            if (!IsPostBack)
            {
                if (Session["SchoolYearMessage"] != null)
                {
                    MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                    MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Saved Successfully", "false"));
                    Session.Remove("SchoolYearMessage");
                }
            }
        }

        protected void rgSchoolYear_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DayCareBAL.SchoolYearService proxySchoolYear = new DayCareBAL.SchoolYearService();
            Guid SchoolId = new Guid();
            if (Session["SchoolId"] != null)
            {
                SchoolId = new Guid(Session["SchoolId"].ToString());
            }
            List<DayCarePL.SchoolYearProperties> lstSchoolYear = proxySchoolYear.LoadSchoolYear(SchoolId);
            rgSchoolYear.DataSource = lstSchoolYear;
            objCurrentYear = lstSchoolYear.Find(i => i.CurrentId.Equals(true));
        }

        protected void rgSchoolYear_InsertCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            bool isValid = SubmitRecord(sender, e);
            if (isValid == false)
            {
                e.Canceled = true;
            }
            else
            {
                Response.Redirect("SchoolYear.aspx", false);
            }
            rgSchoolYear.MasterTableView.CurrentPageIndex = 0;
        }

        protected void rgSchoolYear_ItemCreated(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item is GridEditableItem && e.Item.IsInEditMode)
            {
                GridEditableItem item = e.Item as GridEditableItem;
                TableCell cell;
                RequiredFieldValidator validator;

                if (item != null)
                {
                    ValidationSummary validationsum = new ValidationSummary();
                    validationsum.ID = "validationsum1";
                    validationsum.ShowMessageBox = true;
                    validationsum.ShowSummary = false;
                    validationsum.DisplayMode = ValidationSummaryDisplayMode.SingleParagraph;

                    DropDownList ddlYear = item["Year"].FindControl("ddlYear") as DropDownList;
                    if (ddlYear != null)
                    {
                        cell = (TableCell)ddlYear.Parent;
                        validator = new RequiredFieldValidator();
                        if (cell != null)
                        {
                            ddlYear.ID = "ddlYear";
                            validator.ControlToValidate = ddlYear.ID;
                            validator.ErrorMessage = "Please select SchoolYear \n";
                            validator.InitialValue = "-1";
                            validator.SetFocusOnError = true;
                            validator.Display = ValidatorDisplay.None;
                            cell.Controls.Add(validator);
                            cell.Controls.Add(validationsum);
                        }
                    }
                    RadDatePicker rdpStartDate = item["StartDate"].FindControl("rdpStartDate") as RadDatePicker;
                    RadDatePicker rdpEndDate = item["EndDate"].FindControl("rdpEndDate") as RadDatePicker;

                    if (Session["SchoolId"] == null || Session["CurrentSchoolYearId"] == null)
                    {
                        Response.Redirect("~/Login.aspx");
                    }

                    DayCareBAL.SchoolYearService proxySchoolYear = new DayCareBAL.SchoolYearService();
                    DayCarePL.SchoolYearProperties ObjSchoolYear = proxySchoolYear.LoadSchoolYearDtail(new Guid(Session["SchoolId"].ToString()), new Guid(Session["CurrentSchoolYearId"].ToString()));



                    if (rdpStartDate != null)
                    {
                        //   rdpStartDate.MinDate = ObjSchoolYear.EndDate.Value.AddDays(1);
                        // rdpEndDate.MinDate = ObjSchoolYear.EndDate.Value.AddDays(1);

                        cell = (TableCell)rdpStartDate.Parent;
                        //TableCell cell1 = (TableCell)rdpStartDate.Parent;
                        validator = new RequiredFieldValidator();
                        //CompareValidator compare = item["EndDate"].FindControl("cmp") as CompareValidator; //new CompareValidator();
                        //CompareValidator compare = new CompareValidator();
                        if (cell != null)
                        {
                            rdpStartDate.ID = "rdpStartDate";
                            rdpEndDate.ID = "rdpEndDate";
                            validator.ControlToValidate = rdpStartDate.ID;
                            validator.ErrorMessage = "Please select Start Date \n";
                            //validator.InitialValue = "-1";
                            validator.SetFocusOnError = true;
                            validator.Display = ValidatorDisplay.None;
                            cell.Controls.Add(validator);
                            cell.Controls.Add(validationsum);

                            //compare.Type = ValidationDataType.Date;
                            //compare.Operator = ValidationCompareOperator.GreaterThan;
                            //compare.ControlToValidate = rdpEndDate.ID;
                            //compare.ControlToCompare = r.ID;
                            //compare.ErrorMessage = "Start Date must less than End Date.";
                            //compare.SetFocusOnError = true;
                            //compare.Display = ValidatorDisplay.None;
                            //cell.Controls.Add(compare);
                            //cell.Controls.Add(validationsum);
                        }
                    }
                }
            }
        }

        protected void rgSchoolYear_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
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
            

            //if (!proxySchoolYear.IsSelectedYear_Current_NextYearORPrevYear(SchoolId, CurrentSchoolYearId))
            //{
            //    //if (e.CommandName == "InitInsert")
            //    //{
            //    //    e.Canceled = true;
            //    //}
            //    //else 
            //    if (e.CommandName == "Edit")
            //    {
            //        e.Canceled = true;
            //    }
            //}
        }

        protected void rgSchoolYear_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item.ItemType == GridItemType.AlternatingItem || e.Item.ItemType == GridItemType.Item)
            {
                DayCarePL.SchoolYearProperties objSchool = e.Item.DataItem as DayCarePL.SchoolYearProperties;
                if (objSchool != null)
                {
                    Label lblStartDate = e.Item.FindControl("lblStartDate") as Label;
                    Label lblEndDate = e.Item.FindControl("lblEndDate") as Label;
                    
                    if (objSchool.StartDate != null)
                    {
                        lblStartDate.Text = Convert.ToDateTime(objSchool.StartDate).ToString("MM/dd/yy");
                    }
                    if (objSchool.EndDate != null)
                    {
                        lblEndDate.Text = Convert.ToDateTime(objSchool.EndDate).ToString("MM/dd/yy");
                    }
                    GridDataItem itm = (GridDataItem)e.Item;
                    ImageButton imgbtnEdit = (ImageButton)itm["Edit"].Controls[0];
                    if (imgbtnEdit != null)
                    {
                        DayCareBAL.SchoolYearService proxySchoolYear = new DayCareBAL.SchoolYearService();
                        if (!proxySchoolYear.IsSelectedYear_Current_NextYearORPrevYear(new Guid(Session["SchoolId"].ToString()), objSchool.Year))
                        {
                            imgbtnEdit.Enabled = false;
                        }
                    }
                }
                //Guid SchoolId = new Guid();
                //Guid CurrentSchoolYearId = new Guid();
                //if (Session["SchoolId"] != null)
                //{
                //    SchoolId = new Guid(Session["SchoolId"].ToString());
                //}

                //if (Session["CurrentSchoolYearId"] != null)
                //{
                //    CurrentSchoolYearId = new Guid(Session["CurrentSchoolYearId"].ToString());
                //}

                //if (!Common.IsCurrentYear(CurrentSchoolYearId, SchoolId))
                //{
                //    GridDataItem itm = (GridDataItem)e.Item;
                //    if (itm != null)
                //    {
                //        ImageButton imgbtnEdit = (ImageButton)itm["Edit"].Controls[0];
                //        if (imgbtnEdit != null)
                //        {
                //            imgbtnEdit.ToolTip = "";
                //            imgbtnEdit.Enabled = false;
                //            imgbtnEdit.Style.Value = "cursor:auto";
                //        }
                //        GridCommandItem cmdItem = itm.OwnerTableView.GetItems(GridItemType.CommandItem)[0] as GridCommandItem;
                //        if (cmdItem != null)
                //        {
                //            LinkButton lnkbtnInitInsertButton = (LinkButton)cmdItem.FindControl("InitInsertButton");
                //            lnkbtnInitInsertButton.Enabled = false;
                //            lnkbtnInitInsertButton.Style.Value = "cursor:auto";
                //        }
                //        rgSchoolYear.MasterTableView.IsItemInserted = false;//d new GridTableItemStyle();
                //    }
                //}
            }

            if (e.Item.ItemType == GridItemType.EditItem)
            {
                GridEditableItem itm = e.Item as GridEditableItem;
                DayCarePL.SchoolYearProperties objSchoolYear = e.Item.DataItem as DayCarePL.SchoolYearProperties;
                RadDatePicker rdpStartDate = e.Item.FindControl("rdpStartDate") as RadDatePicker;
                RadDatePicker rdpEndDate = e.Item.FindControl("rdpEndDate") as RadDatePicker;
                DropDownList ddlYear = e.Item.FindControl("ddlYear") as DropDownList;
                CheckBox chkCurrentId = itm["CurrentId"].Controls[0] as CheckBox;
                Common.BindSchoolYear(ddlYear);

                if (objSchoolYear != null)
                {
                    
                    if (ddlYear.Items != null && ddlYear.Items.Count > 0)
                    {
                        ddlYear.SelectedValue = objSchoolYear.Year.ToString();
                    }
                    if (objSchoolYear.StartDate != null)
                    {
                        rdpStartDate.SelectedDate = objSchoolYear.StartDate;
                    }
                    if (objSchoolYear.EndDate != null)
                    {
                        rdpEndDate.SelectedDate = objSchoolYear.EndDate;
                    }
                    if (objSchoolYear.CurrentId == true)
                    {
                        chkCurrentId.Enabled = false;
                    }
                    chkCurrentId.ToolTip = "";
                    if (objCurrentYear.StartDate > objSchoolYear.StartDate)
                    {
                        chkCurrentId.ToolTip = "Don't allow to make past year as current year";
                        chkCurrentId.Enabled = false;
                    }
                }
            }
        }

        protected void rgSchoolYear_EditCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            GridDataItem item = (GridDataItem)e.Item;
            hdnName.Value = (item["Year"].FindControl("lblYear") as Label).Text;
            
        }

        protected void rgSchoolYear_UpdateCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            bool isValid = SubmitRecord(sender, e);
            if (isValid == false)
            {
                e.Canceled = true;
            }
            else
            {
                Response.Redirect("SchoolYear.aspx");
            }
        }

        protected void rgSchoolYear_DeleteCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {

        }

        public bool SubmitRecord(object sender, GridCommandEventArgs e)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.SchoolYear, "SubmitRecord", "Submit record method called", DayCarePL.Common.GUID_DEFAULT);
            Guid SchoolYearId = new Guid();
            bool result = false;
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.SchoolYear, "SubmitRecord", "Debug Submit Record Of SchoolYear", DayCarePL.Common.GUID_DEFAULT);
                DayCareBAL.SchoolYearService proxySchoolYear = new DayCareBAL.SchoolYearService();
                DayCarePL.SchoolYearProperties objSchoolYear = new DayCarePL.SchoolYearProperties();

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
                                    case "Year":
                                        {
                                            objSchoolYear.Year = (e.Item.FindControl("ddlYear") as DropDownList).SelectedValue;
                                            break;
                                        }
                                    case "StartDate":
                                        {
                                            objSchoolYear.StartDate = (e.Item.FindControl("rdpStartDate") as RadDatePicker).SelectedDate.Value;
                                            break;
                                        }
                                    case "EndDate":
                                        {
                                            if ((e.Item.FindControl("rdpEndDate") as RadDatePicker).SelectedDate != null)
                                            {
                                                objSchoolYear.EndDate = (e.Item.FindControl("rdpEndDate") as RadDatePicker).SelectedDate.Value;
                                            }
                                            else
                                            {
                                                objSchoolYear.EndDate = null;
                                            }
                                            break;
                                        }
                                    case "Comment":
                                        {
                                            objSchoolYear.Comments = (e.Item.FindControl("txtComment") as TextBox).Text;
                                            break;
                                        }
                                    case "CurrentId":
                                        {
                                            objSchoolYear.CurrentId = (editor as GridCheckBoxColumnEditor).Value;
                                            break;
                                        }
                                }
                            }
                        }
                    }
                    if (objSchoolYear.EndDate != null)
                    {
                        if (objSchoolYear.StartDate > objSchoolYear.EndDate)
                        {
                            MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                            MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Start Date must be less than End Date.", "false"));
                            return false;
                        }
                    }
                    if (Session["SchoolId"] != null)
                    {
                        objSchoolYear.SchoolId = new Guid(Session["SchoolId"].ToString());
                    }
                    if (e.CommandName != "PerformInsert")
                    {
                        if (Session["StaffId"] != null)
                        {
                            objSchoolYear.LastModifiedById = new Guid(Session["StaffId"].ToString());
                        }
                        objSchoolYear.Id = new Guid(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Id"].ToString());
                        if (!objSchoolYear.Year.Trim().Equals(hdnName.Value.Trim()))
                        {
                            bool ans = proxySchoolYear.CheckDuplicateSchoolYear(objSchoolYear.Year, objSchoolYear.Id, objSchoolYear.SchoolId);
                            if (ans)
                            {
                                MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                                MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "School Year Already Exist", "false"));
                                return false;
                            }
                        }
                    }
                    else
                    {
                        if (Session["StaffId"] != null)
                        {
                            objSchoolYear.CreatedById = new Guid(Session["StaffId"].ToString());
                        }
                        bool ans = proxySchoolYear.CheckDuplicateSchoolYear(objSchoolYear.Year, objSchoolYear.Id, objSchoolYear.SchoolId);
                        if (ans)
                        {
                            MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                            MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "School Year Already Exist", "false"));
                            return false;
                        }
                    }
                    hdnName.Value = "";

                    SchoolYearId = proxySchoolYear.Save(objSchoolYear, new Guid(Session["CurrentSchoolYearId"].ToString()));
                    if (!SchoolYearId.ToString().Equals(DayCarePL.Common.GUID_DEFAULT))
                    {
                        MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                        MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Saved Successfully", "false"));

                        if (objSchoolYear.CurrentId == true)
                        {
                            Session["CurrentSchoolYearId"] = SchoolYearId;
                        }

                        result = true;
                        Session["SchoolYearMessage"] = true;
                    }
                    else
                    {
                        result = false;
                    }

                }
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.SchoolYear, "SubmitRecord", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                result = false;
            }
            return result;
        }
    }
}
