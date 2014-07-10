using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using Telerik.Web.UI;
using System.Web.UI.WebControls;

namespace DayCare.UI
{
    public partial class ChildEnrollmentStatus : System.Web.UI.Page
    {
        RadAjaxManager MasterAjaxManager;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["SchoolId"] == null || Session["CurrentSchoolYearId"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            if (!string.IsNullOrEmpty(Request.QueryString["Id"]))
            {
                ViewState["ChildDataId"] = new Guid(Request.QueryString["Id"].ToString());

                ViewState["SchoolYearId"] = Session["CurrentSchoolYearId"].ToString();
                SetMenuLink();
            }

        }

        protected void rgChildEnrollmentStatus_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {

            DayCareBAL.ChildEnrollmentStatusService proxyChildEnrollment = new DayCareBAL.ChildEnrollmentStatusService();

            Guid ChildSchoolYearId = Common.GetChildSchoolYearId(new Guid(ViewState["ChildDataId"].ToString()), new Guid(Session["CurrentSchoolYearId"].ToString()));

            rgChildEnrollmentStatus.DataSource = proxyChildEnrollment.LoadChildEnrollmentStatus(new Guid(Session["SchoolId"].ToString()), ChildSchoolYearId);
        }

        protected void rgChildEnrollmentStatus_EditCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            GridDataItem item = (GridDataItem)e.Item;
            hdnName.Value = item["Enrollmentstatus"].Text;
        }

        protected void rgChildEnrollmentStatus_InsertCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            bool isValid = SubmitRecord(source, e);
            {
                if (isValid == false)
                {
                    e.Canceled = true;
                }
            }
            rgChildEnrollmentStatus.MasterTableView.CurrentPageIndex = 0;
        }

        protected void rgChildEnrollmentStatus_ItemCreated(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item is GridEditableItem && e.Item.IsInEditMode)
            {
                GridEditableItem item = e.Item as GridEditableItem;

                if (item != null)
                {

                    DropDownList ddlEnrollmentStatus = item["Enrollmentstatus"].FindControl("ddlEnrollmentStatus") as DropDownList;
                    RadDatePicker rdpEnrollmentDate = item["EnrollmentDate"].FindControl("rdpEnrollmentDate") as RadDatePicker;
                    if (ddlEnrollmentStatus != null)
                    {

                        TableCell cell1 = (TableCell)ddlEnrollmentStatus.Parent;
                        TableCell cell = (TableCell)rdpEnrollmentDate.Parent;
                        RequiredFieldValidator validator1 = new RequiredFieldValidator();
                        RequiredFieldValidator validator = new RequiredFieldValidator();
                        if (cell1 != null)
                        {
                            ddlEnrollmentStatus.ID = "ddlEnrollmentStatus";
                            validator1.ControlToValidate = ddlEnrollmentStatus.ID;
                            validator1.InitialValue = "00000000-0000-0000-0000-000000000000";
                            validator1.ErrorMessage = "Please Select Enrollment Status\n";
                            validator1.SetFocusOnError = true;
                            validator1.Display = ValidatorDisplay.None;
                        }
                        if (cell != null)
                        {
                            rdpEnrollmentDate.ID = "rdpEnrollmentDate";
                            validator.ControlToValidate = rdpEnrollmentDate.ID;
                            //validator.InitialValue = "00000000-0000-0000-0000-000000000000";
                            validator.ErrorMessage = "Please Select Enrollment Date\n";
                            validator.SetFocusOnError = true;
                            validator.Display = ValidatorDisplay.None;
                        }

                        ValidationSummary validationsum = new ValidationSummary();
                        validationsum.ID = "validationsum1";
                        validationsum.ShowMessageBox = true;
                        validationsum.ShowSummary = false;
                        validationsum.DisplayMode = ValidationSummaryDisplayMode.SingleParagraph;
                        cell1.Controls.Add(validator1);
                        cell1.Controls.Add(validationsum);
                        cell.Controls.Add(validator);
                        cell.Controls.Add(validationsum);

                    }

                }
            }
        }

        protected void rgChildEnrollmentStatus_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item.ItemType == Telerik.Web.UI.GridItemType.EditItem)
            {
                DropDownList ddlEnrollmentStatus = e.Item.FindControl("ddlEnrollmentStatus") as DropDownList;
                RadDatePicker rdpEnrollmentDate = e.Item.FindControl("rdpEnrollmentDate") as RadDatePicker;
                if (ddlEnrollmentStatus != null)
                {
                    Common.BindEnrollmentStatus(ddlEnrollmentStatus, new Guid(Session["SchoolId"].ToString()));
                    if (e.Item.ItemIndex != -1)
                    {
                        DayCarePL.ChildEnrollmentStatusProperties dataItem = e.Item.DataItem as DayCarePL.ChildEnrollmentStatusProperties;
                        ddlEnrollmentStatus.SelectedValue = dataItem.EnrollmentStatusId.ToString();
                    }
                }
                if (rdpEnrollmentDate != null)
                {
                    DayCarePL.ChildEnrollmentStatusProperties dataItem = e.Item.DataItem as DayCarePL.ChildEnrollmentStatusProperties;
                    if (dataItem != null)
                    {
                        rdpEnrollmentDate.SelectedDate = dataItem.EnrollmentDate;
                    }
                }
            }
           

        }

        protected void rgChildEnrollmentStatus_UpdateCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            bool isValid = SubmitRecord(source, e);
            {
                if (isValid == false)
                {
                    e.Canceled = true;
                }
            }
            e.Item.Expanded = false;
            rgChildEnrollmentStatus.MasterTableView.Rebind();
            rgChildEnrollmentStatus.MasterTableView.CurrentPageIndex = 0;
        }

        public bool SubmitRecord(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.ChildEnrollmentStatus, "SubmitRecord", "Submit record method called", DayCarePL.Common.GUID_DEFAULT);
            bool result = false;
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.ChildEnrollmentStatus, "SubmitRecord", " Debug Submit record method called of ChildEnrollmentStatus", DayCarePL.Common.GUID_DEFAULT);
                DayCareBAL.ChildEnrollmentStatusService proxyChildEnrollmentStatus = new DayCareBAL.ChildEnrollmentStatusService();
                DayCarePL.ChildEnrollmentStatusProperties objChildEnrollmentStatus = new DayCarePL.ChildEnrollmentStatusProperties();

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
                                    case "Enrollmentstatus":
                                        {
                                            objChildEnrollmentStatus.EnrollmentStatusId = new Guid((e.Item.FindControl("ddlEnrollmentStatus") as DropDownList).SelectedValue);
                                            break;
                                        }
                                    case "EnrollmentDate":
                                        {
                                            objChildEnrollmentStatus.EnrollmentDate = Convert.ToDateTime((e.Item.FindControl("rdpEnrollmentDate") as RadDatePicker).SelectedDate);
                                            break;
                                        }
                                    case "Comments":
                                        {
                                            objChildEnrollmentStatus.Comments = (e.Item.FindControl("txtComments") as TextBox).Text;
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
                            objChildEnrollmentStatus.LastModifiedById = new Guid(Session["StaffId"].ToString());
                        }
                        objChildEnrollmentStatus.Id = new Guid(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Id"].ToString());
                    }
                    hdnName.Value = "";
                    Guid ChildSchoolYearId = Common.GetChildSchoolYearId(new Guid(ViewState["ChildDataId"].ToString()), new Guid(Session["CurrentSchoolYearId"].ToString()));
                    objChildEnrollmentStatus.ChildSchoolYearId = ChildSchoolYearId;
                    if (Session["StaffId"] != null)
                    {
                        objChildEnrollmentStatus.CreatedById = new Guid(Session["StaffId"].ToString());
                    }
                    if (objChildEnrollmentStatus.EnrollmentStatusId != null && objChildEnrollmentStatus.EnrollmentDate != null)
                    {
                        bool check = proxyChildEnrollmentStatus.CheckDuplicateChildEnrollmentStatus(ChildSchoolYearId, objChildEnrollmentStatus.EnrollmentStatusId.Value, objChildEnrollmentStatus.EnrollmentDate.Value, objChildEnrollmentStatus.Id);
                        if (check == false)
                        {
                            result = proxyChildEnrollmentStatus.Save(objChildEnrollmentStatus);
                            if (result == true)
                            {
                                MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                                MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Saved Successfully", "false"));
                            }
                        }
                        else
                        {
                            MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                            MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Already Assigned", "false"));
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.ChildEnrollmentStatus, "SubmitRecord", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                result = false;
            }
            return result;
        }

        protected void rgChildEnrollmentStatus_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            Guid SchoolId = new Guid();
            Guid CurrentSchoolYearId = new Guid();
            if (Session["SchoolId"] != null)
            {
                SchoolId = new Guid(Session["SchoolId"].ToString());
            }

            if (ViewState["SchoolYearId"] != null)
            {
                CurrentSchoolYearId = new Guid(ViewState["SchoolYearId"].ToString());
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
                objMenu.Name = "Child Enrollment Status";
                objMenu.Url = "";
                lstMenu.Add(objMenu);
                usrMenuLink.SetMenuLink(lstMenu);
            }
            catch
            {

            }
        }
    }
}
