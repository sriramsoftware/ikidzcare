using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace DayCare.UI
{
    public partial class ChildAbsentHistory : System.Web.UI.Page
    {
        RadAjaxManager MasterAjaxManager;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["SchoolId"] == null || Session["CurrentSchoolYearId"] == null || string.IsNullOrEmpty(Request.QueryString["Id"]))
            {
                Response.Redirect("~/Login.aspx");
            }

            if (!IsPostBack)
            {
                try
                {
                    if (!string.IsNullOrEmpty(Request.QueryString["Id"]))// != null && Request.QueryString["Id"]
                    {
                        ViewState["ChildDataId"] = Request.QueryString["Id"].ToString();
                    }
                    Guid ChildDataId = new Guid();
                    Guid ChildSchoolYearId = new Guid();
                    Guid CurrentSchoolYearId = new Guid();
                    if (ViewState["ChildDataId"] != null)
                    {
                        ChildDataId = new Guid(ViewState["ChildDataId"].ToString());
                    }
                    if (Session["CurrentSchoolYearId"] != null)
                    {
                        CurrentSchoolYearId = new Guid(Session["CurrentSchoolYearId"].ToString());
                    }
                    ViewState["ChildSchoolYearId"] = Common.GetChildSchoolYearId(ChildDataId, CurrentSchoolYearId);
                    SetMenuLink();
                }
                catch (Exception ex)
                {
                    DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.ChildAbsentHistory, "Page_Load", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                }
            }

        }

        protected void rgChildAbsentHistory_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                DayCareBAL.ChildAbsentHistoryService proxyChildAbsentHistory = new DayCareBAL.ChildAbsentHistoryService();
                rgChildAbsentHistory.DataSource = proxyChildAbsentHistory.LoadChildAbsentHistory(new Guid(ViewState["ChildSchoolYearId"].ToString()));
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.ChildAbsentHistory, "rgChildAbsentHistory_NeedDataSource", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
            }
        }

        protected void rgChildAbsentHistory_InsertCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            bool isValid = SubmitRecord(sender, e);
            if (isValid == false)
            {
                e.Canceled = true;
            }
            rgChildAbsentHistory.MasterTableView.CurrentPageIndex = 0;
        }

        protected void rgChildAbsentHistory_ItemCreated(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            try
            {
                if (e.Item is GridFilteringItem)
                {
                    GridFilteringItem filteringItem = e.Item as GridFilteringItem;
                    //set dimensions for the filter textbox  
                    TextBox box = filteringItem["AbsentReason"].Controls[0] as TextBox;
                    box.Width = Unit.Pixel(50);
                    TextBox box1 = filteringItem["StartDate"].Controls[0] as TextBox;
                    box1.Width = Unit.Pixel(50);
                    TextBox box2 = filteringItem["EndDate"].Controls[0] as TextBox;
                    box2.Width = Unit.Pixel(50);
                    //RadDatePicker box3 = filteringItem["column4"].Controls[0] as RadDatePicker;
                    //box3.Width = Unit.Pixel(100);
                }

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

                        DropDownList ddlAbsentReson = item["AbsentReason"].FindControl("ddlAbsentReason") as DropDownList;
                        if (ddlAbsentReson != null)
                        {
                            cell = (TableCell)ddlAbsentReson.Parent;
                            validator = new RequiredFieldValidator();
                            if (cell != null)
                            {
                                ddlAbsentReson.ID = "ddlAbsentReson";
                                validator.ControlToValidate = ddlAbsentReson.ID;
                                validator.ErrorMessage = "Please select Absent Reason \n";
                                validator.InitialValue = DayCarePL.Common.GUID_DEFAULT;
                                validator.SetFocusOnError = true;
                                validator.Display = ValidatorDisplay.None;
                                cell.Controls.Add(validator);
                                cell.Controls.Add(validationsum);
                            }
                        }
                        RadDatePicker rdpStartDate = item["StartDate"].FindControl("rdpStartDate") as RadDatePicker;
                        RadDatePicker rdpEndDate = item["EndDate"].FindControl("rdpEndDate") as RadDatePicker;

                        if (rdpStartDate != null)
                        {
                            cell = (TableCell)rdpStartDate.Parent;
                            validator = new RequiredFieldValidator();
                            if (cell != null)
                            {
                                rdpStartDate.ID = "rdpStartDate";
                                rdpEndDate.ID = "rdpEndDate";
                                validator.ControlToValidate = rdpStartDate.ID;
                                validator.ErrorMessage = "Please select Start Date \n";
                                validator.SetFocusOnError = true;
                                validator.Display = ValidatorDisplay.None;
                                cell.Controls.Add(validator);
                                cell.Controls.Add(validationsum);
                            }
                        }

                        if (rdpEndDate != null)
                        {
                            cell = (TableCell)rdpEndDate.Parent;
                            validator = new RequiredFieldValidator();
                            if (cell != null)
                            {
                                rdpEndDate.ID = "rdpEndDate";
                                validator.ControlToValidate = rdpStartDate.ID;
                                validator.ErrorMessage = "Please select End Date \n";
                                validator.SetFocusOnError = true;
                                validator.Display = ValidatorDisplay.None;
                                cell.Controls.Add(validator);
                                cell.Controls.Add(validationsum);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.ChildAbsentHistory, "rgChildAbsentHistory_ItemCreated", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
            }
        }

        protected void rgChildAbsentHistory_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
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

        protected void rgChildAbsentHistory_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == GridItemType.AlternatingItem || e.Item.ItemType == GridItemType.Item)
                {
                    DayCarePL.ChildAbsentHistoryProperties objChildAbsentHistory = e.Item.DataItem as DayCarePL.ChildAbsentHistoryProperties;
                    if (objChildAbsentHistory != null)
                    {
                        Label lblStartDate = e.Item.FindControl("lblStartDate") as Label;
                        Label lblEndDate = e.Item.FindControl("lblEndDate") as Label;
                        if (objChildAbsentHistory.StartDate != null)
                        {
                            //lblStartDate.Text = Convert.ToDateTime(objChildAbsentHistory.StartDate).ToShortDateString().ToString();
                            lblStartDate.Text = string.Format("{0:MM/dd/yy}", objChildAbsentHistory.StartDate);
                        }
                        if (objChildAbsentHistory.EndDate != null)
                        {
                            //lblEndDate.Text = Convert.ToDateTime(objChildAbsentHistory.EndDate).ToShortDateString().ToString();
                            lblEndDate.Text = string.Format("{0:MM/dd/yy}", objChildAbsentHistory.EndDate);
                        }
                        lblChild.Text = "of " + objChildAbsentHistory.ChildFullName;
                    }
                }

                if (e.Item.ItemType == GridItemType.EditItem)
                {
                    DayCarePL.ChildAbsentHistoryProperties objChildAbsentHistory = e.Item.DataItem as DayCarePL.ChildAbsentHistoryProperties;
                    RadDatePicker rdpStartDate = e.Item.FindControl("rdpStartDate") as RadDatePicker;
                    RadDatePicker rdpEndDate = e.Item.FindControl("rdpEndDate") as RadDatePicker;
                    DropDownList ddlAbsentReason = e.Item.FindControl("ddlAbsentReson") as DropDownList;
                    Guid SchooId = new Guid();
                    if (Session["SchoolId"] != null)
                    {
                        SchooId = new Guid(Session["SchoolId"].ToString());
                    }
                    Common.BindAbsentReson(ddlAbsentReason, SchooId);
                    if (objChildAbsentHistory != null)
                    {
                        if (ddlAbsentReason.Items != null && ddlAbsentReason.Items.Count > 0)
                        {
                            ddlAbsentReason.SelectedValue = objChildAbsentHistory.AbsentReasonId.ToString();
                        }
                        if (objChildAbsentHistory.StartDate != null)
                        {
                            rdpStartDate.SelectedDate = objChildAbsentHistory.StartDate;
                        }
                        if (objChildAbsentHistory.EndDate != null)
                        {
                            rdpEndDate.SelectedDate = objChildAbsentHistory.EndDate;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.ChildAbsentHistory, "rgChildAbsentHistory_ItemDataBound", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
            }
        }

        protected void rgChildAbsentHistory_EditCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            GridDataItem item = (GridDataItem)e.Item;
            //hdnName.Value = (item["Year"].FindControl("lblYear") as Label).Text;
        }

        protected void rgChildAbsentHistory_UpdateCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            bool isValid = SubmitRecord(sender, e);
            if (isValid == false)
            {
                e.Canceled = true;
            }
        }

        protected void rgChildAbsentHistory_DeleteCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {

        }

        public bool SubmitRecord(object sender, GridCommandEventArgs e)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.ChildAbsentHistory, "SubmitRecord", "Submit record method called", DayCarePL.Common.GUID_DEFAULT);
            bool result = false;

            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.ChildAbsentHistory, "SubmitRecord", "Debug Submit Record Of SchoolYear", DayCarePL.Common.GUID_DEFAULT);
                DayCareBAL.ChildAbsentHistoryService proxyChildAbsentHistory = new DayCareBAL.ChildAbsentHistoryService();
                DayCarePL.ChildAbsentHistoryProperties objChildAbsentHistory = new DayCarePL.ChildAbsentHistoryProperties();

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
                                    case "AbsentReason":
                                        {
                                            objChildAbsentHistory.AbsentReasonId = new Guid((item["AbsentReason"].Controls[1] as DropDownList).SelectedValue);
                                            break;
                                        }
                                    case "StartDate":
                                        {
                                            if ((e.Item.FindControl("rdpStartDate") as RadDatePicker).SelectedDate != null)
                                            {
                                                objChildAbsentHistory.StartDate = (e.Item.FindControl("rdpStartDate") as RadDatePicker).SelectedDate.Value;
                                            }
                                            break;
                                        }
                                    case "EndDate":
                                        {
                                            if ((e.Item.FindControl("rdpEndDate") as RadDatePicker).SelectedDate != null)
                                            {
                                                objChildAbsentHistory.EndDate = (e.Item.FindControl("rdpEndDate") as RadDatePicker).SelectedDate.Value;
                                            }

                                            break;
                                        }
                                    case "Comment":
                                        {
                                            objChildAbsentHistory.Comments = (e.Item.FindControl("txtComment") as TextBox).Text;
                                            break;
                                        }
                                }
                            }
                        }
                    }
                    if (objChildAbsentHistory.StartDate != null && objChildAbsentHistory.EndDate != null)
                    {
                        if (objChildAbsentHistory.StartDate > objChildAbsentHistory.EndDate)
                        {
                            MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                            MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Start Date must be less than End Date.", "false"));
                            return false;
                        }
                    }
                    if (ViewState["ChildSchoolYearId"] != null)
                    {
                        objChildAbsentHistory.ChildSchoolYearId = new Guid(ViewState["ChildSchoolYearId"].ToString());
                    }
                    if (e.CommandName != "PerformInsert")
                    {
                        if (Session["StaffId"] != null)
                        {
                            objChildAbsentHistory.LastModifiedById = new Guid(Session["StaffId"].ToString());
                        }
                        objChildAbsentHistory.Id = new Guid(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Id"].ToString());
                    }
                    else
                    {
                        if (Session["StaffId"] != null)
                        {
                            objChildAbsentHistory.CreatedById = new Guid(Session["StaffId"].ToString());
                        }
                    }
                    result = proxyChildAbsentHistory.Save(objChildAbsentHistory);
                    if (result)
                    {
                        MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                        MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Saved Successfully", "false"));
                    }
                }
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.ChildAbsentHistory, "SubmitRecord", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                result = false;
            }
            return result;
        }

        public void SetMenuLink()
        {
            try
            {
                string str = "";
                List<DayCarePL.MenuLink> lstMenu = new List<DayCarePL.MenuLink>();
                DayCarePL.MenuLink objMenu;
                if (Session["ChildFamilyName"] != null)
                {
                    objMenu = new DayCarePL.MenuLink();
                    str = Session["ChildFamilyName"].ToString();
                    objMenu.Name = "Family" + str;
                    objMenu.Url = Convert.ToString(Session["ChildFamilyUrl"]);
                    lstMenu.Add(objMenu);
                }

                objMenu = new DayCarePL.MenuLink();
                objMenu.Name = "Child: " + Common.GetChildName(new Guid(ViewState["ChildDataId"].ToString()));
                //objMenu.Url = "~/UI/ChildData.aspx?ChildFamilyId=" + new Guid(Session["ChildFamilyId"].ToString());
                if (Request.UrlReferrer != null)
                {
                    objMenu.Url = "~" + Request.UrlReferrer.PathAndQuery;
                }
                else
                {
                    objMenu.Url = "";
                }
                lstMenu.Add(objMenu);

                objMenu = new DayCarePL.MenuLink();
                objMenu.Name = "Child Absent History of" + Common.GetChildName(new Guid(ViewState["ChildDataId"].ToString()));
                objMenu.Url = "";
                lstMenu.Add(objMenu);
                usrMenuLink.SetMenuLink(lstMenu);
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.ChildAbsentHistory, "SetMenuLink:ChildDataId=" + ViewState["ChildDataId"] + " ChildFamilyUrl" + Session["ChildFamilyUrl"], ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
            }
        }
    }
}
