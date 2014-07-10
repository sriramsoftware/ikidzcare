using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using Telerik.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;

namespace DayCare.UI
{
    public partial class SchoolProgramFeesDetail : System.Web.UI.Page
    {
        RadAjaxManager MasterAjaxManager;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["SchoolId"] == null || Session["CurrentSchoolYearId"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            if (!IsPostBack)
            {
                if (Session["ShowErrorMessage"] != null)
                {
                    MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                    MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", Session["ShowErrorMessage"].ToString(), "false"));
                    Session["ShowErrorMessage"] = null;
                }
            }
        }

        public void rgSchoolProgramFeesDetail_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (!e.IsFromDetailTable)
                {
                    Guid SchoolId = new Guid();
                    if (Session["SchoolId"] != null)
                    {
                        SchoolId = new Guid(Session["SchoolId"].ToString());
                    }
                    DayCareBAL.SchoolProgramFeesDetailService ProxySchoolProgram = new DayCareBAL.SchoolProgramFeesDetailService();
                    DayCarePL.SchoolProgramFeesDetailProperties objSchoolProgramFeesDetail = new DayCarePL.SchoolProgramFeesDetailProperties();
                    List<DayCarePL.SchoolProgramProperties> lstSchoolProgram = ProxySchoolProgram.LoadSchoolProgram(SchoolId, new Guid(Session["CurrentSchoolYearId"].ToString()));
                    // rgSchoolProgramFeesDetail.DataSource = ProxySchoolProgram.LoadSchoolProgram(SchoolId, new Guid(Session["CurrentSchoolYearId"].ToString()));
                    if (lstSchoolProgram != null)
                    {
                        rgSchoolProgramFeesDetail.DataSource = lstSchoolProgram;// ProxySchoolProgram.LoadSchoolProgram(SchoolId, new Guid(Session["CurrentSchoolYearId"].ToString()));
                        if (Session["CurrentPageIndex"] != null)
                        {
                            int cnt = 0;
                            int.TryParse(Session["CurrentPageIndex"].ToString(), out cnt);
                            rgSchoolProgramFeesDetail.CurrentPageIndex = cnt;
                            Session["CurrentPageIndex"] = null;
                        }
                    }
                }


            }
            catch (Exception ex)
            {

            }


        }

        protected void rgSchoolProgramFeesDetail_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
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
            //if (e.Item.OwnerTableView.DataMember == "Fees")
            //{
            //    if (e.CommandName == "InitInsert")
            //    {

            //        e.Canceled = true;
            //    }
            //    else if (e.CommandName == "Edit")
            //    {
            //        e.Canceled = true;

            //    }
            //    // }
            //}
            //else
            //{
            if (e.CommandName == "Edit")
            {


            }
            if (e.CommandName == "InitInsert")
            {
                ViewState["IsPreRenderCall"] = false;
            }
            if (e.CommandName == "Cancel")
            {
                ViewState["IsPreRenderCall"] = null;
            }
            //}
        }

        protected void rgSchoolProgramFeesDetail_EditCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
        }

        protected void rgSchoolProgramFeesDetail_ItemCreated(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item is GridEditableItem && e.Item.IsInEditMode)
            {
                GridEditableItem item = e.Item as GridEditableItem;
                TableCell cell;
                RequiredFieldValidator validator;
                if (e.Item.OwnerTableView.Name == "SchoolProgram")
                {
                    if (item != null)
                    {
                        ValidationSummary validationsum = new ValidationSummary();
                        validationsum.ID = "validationsum1";
                        validationsum.ShowMessageBox = true;
                        validationsum.ShowSummary = false;

                        TextBox txtSchoolProgram = item["Program"].FindControl("txtSchoolProgram") as TextBox;
                        if (txtSchoolProgram != null)
                        {
                            cell = (TableCell)txtSchoolProgram.Parent;
                            validator = new RequiredFieldValidator();
                            if (cell != null)
                            {
                                txtSchoolProgram.ID = "txtSchoolProgram";
                                validator.ControlToValidate = txtSchoolProgram.ID;
                                validator.ErrorMessage = "Please enter Program.";
                                validator.SetFocusOnError = true;
                                validator.Display = ValidatorDisplay.None;
                                cell.Controls.Add(validator);
                                cell.Controls.Add(validationsum);
                            }
                        }
                    }
                }
                if (e.Item.OwnerTableView.DataMember == "Fees")
                {
                    try
                    {
                        GridEditableItem item1 = e.Item as GridEditableItem;
                        GridEditableItem item2 = e.Item as GridEditableItem;
                        if (item1 != null)
                        {
                            ValidationSummary validationsum = new ValidationSummary();
                            validationsum.ID = "validationsum1";
                            validationsum.ShowMessageBox = true;
                            validationsum.ShowSummary = false;

                            TextBox txtFees = item1["Fees"].FindControl("txtFees") as TextBox;
                            if (txtFees != null)
                            {
                                cell = (TableCell)txtFees.Parent;
                                validator = new RequiredFieldValidator();
                                if (cell != null)
                                {
                                    txtFees.ID = "txtFees";
                                    validator.ControlToValidate = txtFees.ID;
                                    validator.ErrorMessage = "Please enter Fees.";
                                    validator.SetFocusOnError = true;
                                    validator.Display = ValidatorDisplay.None;
                                    cell.Controls.Add(validator);
                                    cell.Controls.Add(validationsum);
                                }
                            }
                            DropDownList ddlFeesPeriod = item1["FeesPeriod"].FindControl("ddlFeesPeriod") as DropDownList;
                            if (ddlFeesPeriod != null)
                            {
                                cell = (TableCell)ddlFeesPeriod.Parent;
                                validator = new RequiredFieldValidator();
                                if (cell != null)
                                {
                                    ddlFeesPeriod.ID = "ddlFeesPeriod";
                                    validator.ControlToValidate = ddlFeesPeriod.ID;
                                    validator.ErrorMessage = "Please select Fees Period.";
                                    validator.InitialValue = DayCarePL.Common.GUID_DEFAULT;

                                    validator.SetFocusOnError = true;
                                    validator.Display = ValidatorDisplay.None;
                                    cell.Controls.Add(validator);
                                    cell.Controls.Add(validationsum);
                                }
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                    }
                }

            }
        }

        protected void rgSchoolProgramFeesDetail_PreRender(object sender, EventArgs e)
        {
            /*if (ViewState["IsPreRenderCall"] == null)
            {
                if (rgSchoolProgramFeesDetail.MasterTableView.Items.Count > 0)
                {
                    for (int cnt = 0; cnt < rgSchoolProgramFeesDetail.MasterTableView.Items.Count; cnt++)
                    {
                        rgSchoolProgramFeesDetail.MasterTableView.Items[cnt].Expanded = false;
                        if (rgSchoolProgramFeesDetail.MasterTableView.Items[cnt].ChildItem.NestedTableViews[0].Items.Count > 0)
                        {
                            rgSchoolProgramFeesDetail.MasterTableView.Items[cnt].ChildItem.NestedTableViews[0].Items[0].Expanded = false;
                        }
                        else
                        {
                            //rgFamilyPayment.MasterTableView.Items[0].ChildItem.NestedTableViews[0].Items[0].Expanded = false;
                            rgSchoolProgramFeesDetail.MasterTableView.Items[cnt].Expanded = false;
                        }
                    }
                }
            }*/
        }

        protected void rgSchoolProgramFeesDetail_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item is GridPagerItem)
            {
                RadComboBox PageSizeCombo = (RadComboBox)e.Item.FindControl("PageSizeComboBox");
                PageSizeCombo.Items.Clear();
                PageSizeCombo.Items.Add(new RadComboBoxItem("25"));
                PageSizeCombo.FindItemByText("25").Attributes.Add("ownerTableViewId", rgSchoolProgramFeesDetail.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("50"));
                PageSizeCombo.FindItemByText("50").Attributes.Add("ownerTableViewId", rgSchoolProgramFeesDetail.MasterTableView.ClientID);
                //PageSizeCombo.Items[0].Text = "25";
                //PageSizeCombo.Items[1].Text = "50";
                PageSizeCombo.FindItemByText(e.Item.OwnerTableView.PageSize.ToString()).Selected = true;
            }
            if (e.Item.ItemType == GridItemType.Item || e.Item.ItemType == GridItemType.AlternatingItem)
            {
                if (e.Item.OwnerTableView.Name == "SchoolProgram")
                {
                    DayCarePL.SchoolProgramProperties objSchoolProgram = e.Item.DataItem as DayCarePL.SchoolProgramProperties;
                    HyperLink hlProgClassRoom = e.Item.FindControl("hlProgClassRoom") as HyperLink;
                    // string PageName = "";
                    //PageName = "ProgramClassRoom.aspx?SchoolProgramId='" + objSchoolProgram.Id + "'&SchoolYearId='" + objSchoolProgram.SchoolYearId + "'&IsPrimary='" + objSchoolProgram.IsPrimary + "'";
                    hlProgClassRoom.Attributes.Add("onclick", "ShowProgramClassRoom('" + objSchoolProgram.Id + "','" + objSchoolProgram.SchoolYearId + "','" + objSchoolProgram.IsPrimary + "','" + objSchoolProgram.Title + "'); return false;");
                }
                if (e.Item.OwnerTableView.DataMember == "Fees")
                {
                    GridDataItem item = e.Item as GridDataItem;
                    DayCarePL.SchoolProgramFeesDetailProperties objSchoolProgramData = e.Item.DataItem as DayCarePL.SchoolProgramFeesDetailProperties;
                    Label lblWeekDay = item["WeekDay"].FindControl("lblWeekDay") as Label;
                    if (objSchoolProgramData.EffectiveWeekDay == 1)
                    {
                        lblWeekDay.Text = "Monday";
                    }
                    if (objSchoolProgramData.EffectiveWeekDay == 2)
                    {
                        lblWeekDay.Text = "Tuesday";
                    }
                    if (objSchoolProgramData.EffectiveWeekDay == 3)
                    {
                        lblWeekDay.Text = "Wednesday";
                    }
                    if (objSchoolProgramData.EffectiveWeekDay == 4)
                    {
                        lblWeekDay.Text = "Thursday";
                    }
                    if (objSchoolProgramData.EffectiveWeekDay == 5)
                    {
                        lblWeekDay.Text = "Friday";
                    }
                    Label lblMonthday = item["Day"].FindControl("lblMonthday") as Label;
                    lblMonthday.Text = objSchoolProgramData.EffectiveMonthDay == 0 ? "" : objSchoolProgramData.EffectiveMonthDay.ToString();
                    Label lblYearDate = item["YearDate"].FindControl("lblYearDate") as Label;
                    lblYearDate.Text = objSchoolProgramData.EffectiveYearDate == null ? "" : string.Format("{0:MM/dd/yy}", objSchoolProgramData.EffectiveYearDate);
                }
            }

            if (e.Item.ItemType == GridItemType.EditItem)
            {

                if (e.Item.OwnerTableView.DataMember == "Fees")
                {
                    GridEditableItem itm = e.Item as GridEditableItem;
                    DayCarePL.SchoolProgramFeesDetailProperties objSchoolProgramData = e.Item.DataItem as DayCarePL.SchoolProgramFeesDetailProperties;
                    DropDownList ddlFeesPeriod = itm["FeesPeriod"].Controls[1] as DropDownList;

                    if (ddlFeesPeriod != null)
                    {
                        Common.BindFeesPeriod(ddlFeesPeriod);
                    }
                    if (objSchoolProgramData != null)
                    {
                        if (ddlFeesPeriod != null && ddlFeesPeriod.Items.Count > 0)
                        {
                            ddlFeesPeriod.SelectedValue = Convert.ToString(objSchoolProgramData.FeesPeriodId);
                        }
                    }
                    if (objSchoolProgramData != null)
                    {
                        if (objSchoolProgramData.FeesPeriod.ToLower() == "one time")
                        {
                            DropDownList ddlMonthDay = itm["Day"].Controls[1] as DropDownList;
                            ddlMonthDay.Enabled = false;
                            DropDownList ddlWeekDay = itm["WeekDay"].Controls[1] as DropDownList;
                            ddlWeekDay.Enabled = false;

                        }
                        if (objSchoolProgramData.EffectiveMonthDay > 0)
                        {
                            DropDownList ddlMonthDay = itm["Day"].Controls[1] as DropDownList;
                            ddlMonthDay.SelectedValue = objSchoolProgramData.EffectiveMonthDay.ToString();
                            ViewState["Day"] = objSchoolProgramData.EffectiveMonthDay.ToString();
                        }
                        if (objSchoolProgramData.EffectiveWeekDay > 0)
                        {
                            DropDownList ddlWeekDay = itm["WeekDay"].Controls[1] as DropDownList;
                            ddlWeekDay.SelectedValue = objSchoolProgramData.EffectiveWeekDay.ToString();
                            ViewState["Weekly"] = objSchoolProgramData.EffectiveWeekDay.ToString();
                        }
                        if (objSchoolProgramData.EffectiveYearDate != null)
                        {
                            RadDatePicker rdpYearDate = itm["YearDate"].Controls[1] as RadDatePicker;
                            rdpYearDate.SelectedDate = objSchoolProgramData.EffectiveYearDate;
                            ViewState["YearDate"] = objSchoolProgramData.EffectiveYearDate;
                        }
                    }

                }
                if (e.Item.OwnerTableView.Name == "SchoolProgram")
                {
                    GridEditableItem itm = e.Item as GridEditableItem;
                    DayCarePL.SchoolProgramProperties objSchoolYear = e.Item.DataItem as DayCarePL.SchoolProgramProperties;
                    DayCareBAL.SchoolProgramService proxy = new DayCareBAL.SchoolProgramService();
                    CheckBox IsPrimary = itm["IsPrimary"].Controls[0] as CheckBox;
                    CheckBox chkActive = itm["Active"].Controls[0] as CheckBox;
                    if (e.Item.ItemIndex != -1)
                    {
                        if (proxy.CheckSchoolProgramInChildEnrolled(new Guid(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Id"].ToString()), new Guid(Session["CurrentSchoolYearId"].ToString())))
                        {
                            IsPrimary.Enabled = false;
                            IsPrimary.ToolTip = "Program enrolled to child";
                            chkActive.Enabled = false;
                            chkActive.ToolTip = "Program enrolled to child";
                        }
                    }
                }
            }
            if (e.Item.OwnerTableView.Name == "SchoolProgram")
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
        }

        protected void rgSchoolProgramFeesDetail_DetailTableDataBind(object sender, Telerik.Web.UI.GridDetailTableDataBindEventArgs e)
        {

            try
            {
                GridDataItem dataItem = (GridDataItem)e.DetailTableView.ParentItem;
                switch (e.DetailTableView.DataMember)
                {
                    case "Fees":
                        {
                            DayCareBAL.SchoolProgramFeesDetailService proxySchoolProgramFeesDetail = new DayCareBAL.SchoolProgramFeesDetailService();
                            List<DayCarePL.SchoolProgramFeesDetailProperties> lstSchoolProgramFeesDetail = proxySchoolProgramFeesDetail.LoadSchoolProgramFeesDetail(new Guid(dataItem.GetDataKeyValue("Id").ToString()));
                            if (lstSchoolProgramFeesDetail != null)
                            {
                                if (lstSchoolProgramFeesDetail != null)
                                {
                                    if (lstSchoolProgramFeesDetail.Count > 0)
                                    {
                                        e.DetailTableView.DataSource = lstSchoolProgramFeesDetail;
                                    }
                                    else
                                    {
                                        e.DetailTableView.DataSource = new List<DayCarePL.SchoolProgramFeesDetailProperties>();
                                    }
                                }

                            }
                            break;
                        }
                }



            }
            catch (Exception ex)
            {

            }
        }

        protected void rgSchoolProgramFeesDetail_UpdateCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            bool isValid = SubmitRecord(sender, e);
            if (isValid == false)
            {
                Session["ShowErrorMessage"] = "Program already exists";
                e.Canceled = true;
            }
            else
            {
                Session["ShowErrorMessage"] = "Saved Successfully";
                Session["CurrentPageIndex"] = rgSchoolProgramFeesDetail.MasterTableView.CurrentPageIndex;
                Response.Redirect("~/UI/SchoolProgramFeesDetail.aspx");
            }
            ViewState["Weekly"] = null;
            ViewState["Day"] = null;
            ViewState["YearDate"] = null;
            //rgSchoolProgramFeesDetail.MasterTableView.CurrentPageIndex = 0;
            //rgSchoolProgramFeesDetail.Rebind();





        }

        protected void rgSchoolProgramFeesDetail_InsertCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            bool isValid = SubmitRecord(sender, e);
            if (isValid == false)
            {
                e.Canceled = true;
            }
            rgSchoolProgramFeesDetail.MasterTableView.CurrentPageIndex = 0;

        }

        protected void rgSchoolProgramFeesDetail_DeleteCommand(object source, GridCommandEventArgs e)
        {
            try
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
                //    e.Canceled = true;
                //    if (e.CommandName == "InitInsert")
                //    {

                //        e.Canceled = true;
                //    }
                //    else if (e.CommandName == "Edit")
                //    {
                //        e.Canceled = true;
                //    }
                //    else if (e.CommandName == "Delete")
                //    {
                //        e.Canceled = true;

                //    }
                //}
                //else
                if (e.Item.OwnerTableView.Name == "SchoolProgram")
                {
                    DayCareBAL.SchoolProgramService proxySchoolProgram = new DayCareBAL.SchoolProgramService();
                    Guid SchoolProgramId = new Guid(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Id"].ToString());
                    if (proxySchoolProgram.CheckSchoolProgramInChildEnrolledAndLedger(SchoolProgramId))
                    {
                        MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                        MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "This Program cannot be deleted until all kids have been reassigned to another program.", "false"));
                        return;
                    }
                    else
                    {
                        if (proxySchoolProgram.DeleteSchoolProgram(SchoolProgramId))
                        {
                            rgSchoolProgramFeesDetail.Rebind();
                            MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                            MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Deleted Successfully", "false"));
                            return;
                        }
                        else
                        {
                            MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                            MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Internal Error", "false"));
                            return;
                        }
                    }
                }
                else
                {
                    //get schoolprogra id
                    GridDataItem parentItem = e.Item.OwnerTableView.ParentItem;
                    Guid SchoolProgramId = new System.Guid(e.Item.OwnerTableView.ParentItem.OwnerTableView.DataKeyValues[parentItem.ItemIndex]["Id"].ToString());

                    //get school program fees detail and id of this table.
                    Guid Id = new Guid(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Id"].ToString());

                    DayCareBAL.SchoolProgramFeesDetailService proxySF = new DayCareBAL.SchoolProgramFeesDetailService();
                    Guid FeesPeriodId = proxySF.LoadSchoolProgramFeesDetail(SchoolProgramId).FirstOrDefault(u => u.Id.Equals(Id)).FeesPeriodId;

                    bool Check = proxySF.CheckSchoolProgramIdAndFeesPeriodId(SchoolProgramId, FeesPeriodId);

                    if (!Check)
                    {
                        if (proxySF.Delete(Id))
                        {
                            rgSchoolProgramFeesDetail.MasterTableView.DetailTables[0].Rebind();
                            MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                            MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Delete Successfully", "false"));
                            return;
                        }
                    }
                    else
                    {
                        MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                        MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Already assigned Child Program Enrollment you can not delete", "false"));
                        return;
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        public bool SubmitRecord(object sender, GridCommandEventArgs e)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.SchoolProgram, "SubmitRecord", "Submit record method called", DayCarePL.Common.GUID_DEFAULT);
            bool result = false;
            string Amount = "";
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.SchoolProgram, "SubmitRecord", "Debug Submit Record Of SchoolProgramFeesDetail", DayCarePL.Common.GUID_DEFAULT);
                DayCareBAL.SchoolProgramFeesDetailService proxySchoolProgramFeesDetail = new DayCareBAL.SchoolProgramFeesDetailService();
                DayCareBAL.SchoolProgramService proxySchoolProgram = new DayCareBAL.SchoolProgramService();
                DayCarePL.SchoolProgramProperties objSchoolProgram = new DayCarePL.SchoolProgramProperties();
                DayCarePL.SchoolProgramFeesDetailProperties objSchoolProgramFeesDetail = new DayCarePL.SchoolProgramFeesDetailProperties();

                if (e.Item.OwnerTableView.Name == "SchoolProgram")
                {
                    Guid SchoolProgramId;
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
                                        case "Program":
                                            {
                                                objSchoolProgram.Title = (e.Item.FindControl("txtSchoolProgram") as TextBox).Text.Trim();
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
                                        case "Comments":
                                            {

                                                objSchoolProgram.Comments = (e.Item.FindControl("txtComments") as TextBox).Text.Trim();
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
                                objSchoolProgramFeesDetail.LastmodifiedById = new Guid(Session["StaffId"].ToString());
                                objSchoolProgram.LastModifiedById = new Guid(Session["StaffId"].ToString());
                            }
                            objSchoolProgram.Id = new Guid(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Id"].ToString());
                        }
                        else
                        {
                            if (Session["StaffId"] != null)
                            {
                                objSchoolProgramFeesDetail.CreatedById = new Guid(Session["StaffId"].ToString());
                                objSchoolProgramFeesDetail.LastmodifiedById = new Guid(Session["StaffId"].ToString());
                                objSchoolProgram.CreatedById = new Guid(Session["StaffId"].ToString());
                                objSchoolProgram.LastModifiedById = new Guid(Session["StaffId"].ToString());
                            }
                        }
                        hdnName.Value = "";
                        // DataSet ds1 = new DataSet();
                        //ds1 = proxySchoolProgram.CheckSchoolProgramInChildEnrolled(objSchoolProgram.Id, objSchoolProgram.SchoolYearId);
                        //if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
                        //{
                        //    MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                        //    MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Program enrolled in Child.", "false"));
                        //    return false;
                        //}
                        //else
                        //{
                        SchoolProgramId = proxySchoolProgram.Save(objSchoolProgram);

                        if (SchoolProgramId.ToString().Equals("11111111-1111-1111-1111-111111111111"))
                        {
                            MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                            MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Program already exists", "false"));
                            return false;
                        }
                        else if (!SchoolProgramId.ToString().Equals(DayCarePL.Common.GUID_DEFAULT))
                        {
                            if (e.CommandName == "PerformInsert")
                            {
                                if (objSchoolProgram.IsPrimary == true)
                                {
                                    DataSet ds = new DataSet();
                                    ds = proxySchoolProgram.GetAllProgClassRoomList(new Guid(Session["SchoolId"].ToString()));
                                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                                    {
                                        DayCareBAL.ProgClassRoomService ProxyProgClassRoomSave = new DayCareBAL.ProgClassRoomService();
                                        DayCarePL.ProgClassRoomProperties objProgClassRoom = new DayCarePL.ProgClassRoomProperties();
                                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                                        {
                                            objProgClassRoom.SchoolProgramId = SchoolProgramId;
                                            objProgClassRoom.ClassRoomId = new Guid(ds.Tables[0].Rows[i]["Id"].ToString());
                                            objProgClassRoom.Active = true;
                                            objProgClassRoom.CreatedById = objSchoolProgram.CreatedById;
                                            objSchoolProgram.LastModifiedById = objSchoolProgram.CreatedById;
                                            ProxyProgClassRoomSave.Save(objProgClassRoom);
                                        }
                                    }
                                }
                                else if (objSchoolProgram.IsPrimary == false)
                                {
                                    DataSet ds = new DataSet();
                                    ds = proxySchoolProgram.GetAllProgClassRoomList(new Guid(Session["SchoolId"].ToString()));
                                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                                    {
                                        DayCareBAL.ProgClassRoomService ProxyProgClassRoomSave = new DayCareBAL.ProgClassRoomService();
                                        DayCarePL.ProgClassRoomProperties objProgClassRoom = new DayCarePL.ProgClassRoomProperties();
                                        //for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                                        //{
                                        objProgClassRoom.SchoolProgramId = SchoolProgramId;
                                        //objProgClassRoom.ClassRoomId = new Guid(DayCarePL.Common.NOTEAPPLICABLE_CLASSROOMID);
                                        objProgClassRoom.ClassRoomId = new Guid(ConfigurationSettings.AppSettings[Session["SchoolId"].ToString() + "_NA"]);
                                        objProgClassRoom.Active = true;
                                        objProgClassRoom.CreatedById = objSchoolProgram.CreatedById;
                                        objSchoolProgram.LastModifiedById = objSchoolProgram.CreatedById;
                                        ProxyProgClassRoomSave.Save(objProgClassRoom);
                                        //}
                                    }
                                }
                            }
                            result = true;
                        }
                        else
                        {
                            result = false;
                        }


                    }
                    if (result)
                    {
                        MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                        MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Saved Successfully", "false"));
                    }
                    else
                    {
                        MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                        MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Internal Error", "false"));
                    }
                    return result;
                }

                if (e.Item.OwnerTableView.Name == "rgFees")
                {
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
                                        case "Fees":
                                            {
                                                objSchoolProgramFeesDetail.Fees = Convert.ToDecimal((e.Item.FindControl("txtFees") as TextBox).Text.Trim());
                                                break;
                                            }
                                        case "FeesPeriod":
                                            {
                                                GridEditableItem itm = e.Item as GridEditableItem;
                                                DropDownList ddlFeesPeriod = itm["FeesPeriod"].Controls[1] as DropDownList;
                                                objSchoolProgramFeesDetail.FeesPeriodId = new Guid(ddlFeesPeriod.SelectedValue.ToString());
                                                break;
                                            }
                                        case "WeekDay":
                                            {
                                                int wd = Convert.ToInt32((e.Item.FindControl("ddlWeekDay") as DropDownList).SelectedValue);
                                                if (wd > 0)
                                                    objSchoolProgramFeesDetail.EffectiveWeekDay = wd;
                                                break;
                                            }
                                        case "Day":
                                            {
                                                int md = Convert.ToInt32((e.Item.FindControl("ddlMonthDay") as DropDownList).SelectedValue);
                                                if (md > 0)
                                                    objSchoolProgramFeesDetail.EffectiveMonthDay = md;
                                                break;
                                            }
                                        case "YearDate":
                                            {
                                                if ((e.Item.FindControl("rdpYearDate") as RadDatePicker).SelectedDate != null)
                                                    objSchoolProgramFeesDetail.EffectiveYearDate = (e.Item.FindControl("rdpYearDate") as RadDatePicker).SelectedDate;
                                                break;
                                            }
                                    }
                                }
                            }
                        }
                        GridDataItem parentItem = e.Item.OwnerTableView.ParentItem;
                        objSchoolProgramFeesDetail.SchoolProgramId = new System.Guid(e.Item.OwnerTableView.ParentItem.OwnerTableView.DataKeyValues[parentItem.ItemIndex]["Id"].ToString());
                        if (!string.IsNullOrEmpty(Amount))
                        {
                            decimal amountresult = 0;
                            decimal.TryParse(Amount, out amountresult);
                            if (amountresult == 0)
                            {
                                objSchoolProgramFeesDetail.Fees = amountresult;
                                MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                                MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Pease enter valid Fees.", "false"));
                                return false;
                            }
                            else
                            {
                                objSchoolProgramFeesDetail.Fees = amountresult;
                            }
                        }

                        if (e.CommandName != "PerformInsert")
                        {
                            if (Session["StaffId"] != null)
                            {
                                objSchoolProgramFeesDetail.LastmodifiedById = new Guid(Session["StaffId"].ToString());
                            }
                            objSchoolProgramFeesDetail.Id = new Guid(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Id"].ToString());
                        }
                        else
                        {
                            if (Session["StaffId"] != null)
                            {
                                objSchoolProgramFeesDetail.CreatedById = new Guid(Session["StaffId"].ToString());
                                objSchoolProgramFeesDetail.LastmodifiedById = new Guid(Session["StaffId"].ToString());
                            }
                        }

                        DropDownList ddlFeesPeriod1 = item["FeesPeriod"].Controls[1] as DropDownList;
                        DropDownList ddlWeekDay = item["WeekDay"].Controls[1] as DropDownList;
                        RadDatePicker rdpYearDate = item["YearDate"].Controls[1] as RadDatePicker;
                        DropDownList ddlMonthDay = item["Day"].Controls[1] as DropDownList;
                        if (ddlFeesPeriod1 != null)
                        {
                            if (ddlFeesPeriod1.SelectedItem.ToString() == "Weekly")
                            {
                                if (ddlWeekDay.SelectedValue == "-1")
                                {
                                    MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                                    MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Please Select Effective Day of Week", "false"));
                                    result = false;
                                }
                                else
                                {
                                    objSchoolProgramFeesDetail.EffectiveMonthDay = null;
                                    objSchoolProgramFeesDetail.EffectiveYearDate = null;
                                }
                            }
                            if (ddlFeesPeriod1.SelectedItem.ToString() == "Monthly")
                            {
                                if (ddlMonthDay.SelectedValue == "-1")
                                {
                                    MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                                    MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Please Select Effective Date of Month", "false"));
                                    result = false;
                                }
                                else
                                {
                                    objSchoolProgramFeesDetail.EffectiveWeekDay = Convert.ToInt32(null);
                                    objSchoolProgramFeesDetail.EffectiveYearDate = null;
                                }
                            }
                            if (ddlFeesPeriod1.SelectedItem.ToString() == "Yearly")
                            {
                                if (rdpYearDate.SelectedDate == null)
                                {
                                    MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                                    MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Please Enter Effective Date of Year", "false"));
                                    result = false;
                                }
                                else
                                {
                                    objSchoolProgramFeesDetail.EffectiveWeekDay = Convert.ToInt32(null);
                                    objSchoolProgramFeesDetail.EffectiveMonthDay = null;
                                }
                            }
                            if (ddlFeesPeriod1.SelectedItem.ToString().ToLower() == "one time")
                            {
                                objSchoolProgramFeesDetail.EffectiveWeekDay = Convert.ToInt32(null);
                                objSchoolProgramFeesDetail.EffectiveYearDate = null;
                                objSchoolProgramFeesDetail.EffectiveMonthDay = null;
                            }
                        }
                        if (ddlWeekDay.SelectedValue != "-1" || ddlMonthDay.SelectedValue != "-1" || rdpYearDate.SelectedDate != null || ddlFeesPeriod1.SelectedItem.ToString().ToLower() == "one time")
                        {

                            if (proxySchoolProgramFeesDetail.CheckDuplicateEffectiveWeekDay(objSchoolProgramFeesDetail.Id, objSchoolProgramFeesDetail.FeesPeriodId, objSchoolProgramFeesDetail.SchoolProgramId))
                            {
                                MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                                MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Already exists FeesPeriod", "false"));
                                result = false;
                            }
                            else
                            {
                                if (proxySchoolProgramFeesDetail.Save(objSchoolProgramFeesDetail))
                                {
                                    MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                                    MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Saved Successfully", "false"));
                                    result = true;
                                    ViewState["IsPreRenderCall"] = null;
                                }
                                else
                                {
                                    result = false;
                                }
                            }
                        }
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.SchoolProgramFeesDetail, "SubmitRecord", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                return false;
            }
        }

        protected void ddlFeesPeriod_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridEditableItem item = (sender as DropDownList).NamingContainer as GridEditableItem;
            DropDownList ddlFeesPeriod = item["FeesPeriod"].Controls[1] as DropDownList;
            DropDownList ddlWeekDay = item["WeekDay"].Controls[1] as DropDownList;
            RadDatePicker rdpYearDate = item["YearDate"].Controls[1] as RadDatePicker;
            DropDownList ddlMonthDay = item["Day"].Controls[1] as DropDownList;
            if (ddlFeesPeriod != null)
            {
                if (ddlFeesPeriod.SelectedItem.ToString().ToLower() == "one time")
                {
                    ddlMonthDay.Text = null;
                    rdpYearDate.SelectedDate = null;
                    ddlWeekDay.SelectedValue = "-1";

                    ddlWeekDay.Enabled = false;
                    ddlMonthDay.Enabled = false;
                }
                if (ddlFeesPeriod.SelectedItem.ToString() == "Weekly")
                {
                    ddlMonthDay.Text = null;
                    rdpYearDate.SelectedDate = null;
                    ddlWeekDay.SelectedValue = "-1";
                    if (ViewState["Weekly"] != null)
                    {
                        ddlWeekDay.SelectedValue = ViewState["Weekly"].ToString();
                    }
                    ddlWeekDay.Enabled = true;
                    ddlMonthDay.Enabled = false;

                }
                if (ddlFeesPeriod.SelectedItem.ToString() == "Monthly")
                {
                    ddlWeekDay.Text = null;
                    rdpYearDate.SelectedDate = null;
                    ddlMonthDay.SelectedValue = "-1";
                    if (ViewState["Day"] != null)
                    {
                        ddlMonthDay.SelectedValue = ViewState["Day"].ToString();
                    }
                    ddlWeekDay.Enabled = false;
                    ddlMonthDay.Enabled = true;

                }
                if (ddlFeesPeriod.SelectedItem.ToString() == "Yearly")
                {
                    ddlMonthDay.Text = null;

                    ddlWeekDay.Text = null;
                    rdpYearDate.SelectedDate = null;
                    if (ViewState["YearDate"] != null)
                    {
                        rdpYearDate.SelectedDate = Convert.ToDateTime(ViewState["YearDate"].ToString());
                    }
                    ddlWeekDay.Enabled = false;
                    ddlMonthDay.Enabled = false;

                }

            }


        }
    }
}