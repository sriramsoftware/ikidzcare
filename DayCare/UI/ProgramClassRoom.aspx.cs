using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace DayCare
{
    public partial class ProgramClassRoom : System.Web.UI.Page
    {
        RadAjaxManager MasterAjaxManager;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["SchoolId"] == null || Session["CurrentSchoolYearId"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            if (string.IsNullOrEmpty(Request.QueryString["SchoolProgramId"]) || string.IsNullOrEmpty(Request.QueryString["SchoolYearId"]) || string.IsNullOrEmpty(Request.QueryString["IsPrimary"]))
            {
                Response.Redirect("~/Login.aspx");
            }
            else
            {
                ViewState["SchoolProgramId"] = Request.QueryString["SchoolProgramId"].ToString();
                ViewState["SchoolYearId"] = Request.QueryString["SchoolYearId"].ToString();
                ViewState["IsPrimary"] = Request.QueryString["IsPrimary"].ToString();
            }
        }

        protected void rgProgClassRoom_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DayCareBAL.ProgClassRoomService proxyProgClassRoom = new DayCareBAL.ProgClassRoomService();
            Guid SchoolId = new Guid();
            if (Session["SchoolId"] != null)
            {
                SchoolId = new Guid(Session["SchoolId"].ToString());
            }
            if (ViewState["SchoolProgramId"] != null)
            {
                rgProgClassRoom.DataSource = proxyProgClassRoom.LoadProgClassRoom(SchoolId, new Guid(ViewState["SchoolProgramId"].ToString()), new Guid(Session["CurrentSchoolYearId"].ToString()));
            }
        }

        protected void rgProgClassRoom_EditCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {

        }

        protected void rgProgClassRoom_InsertCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {

        }

        protected void rgProgClassRoom_ItemCreated(object sender, Telerik.Web.UI.GridItemEventArgs e)
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
                        //DropDownList ddlProgStaff = item["ProgStaff"].FindControl("ddlProgStaff") as DropDownList;
                        //if (ddlProgStaff != null)
                        //{
                        //    TableCell cell = (TableCell)ddlProgStaff.Parent;
                        //    validator = new RequiredFieldValidator();
                        //    if (cell != null)
                        //    {
                        //        ddlProgStaff.ID = "Day";
                        //        validator.ControlToValidate = ddlProgStaff.ID;
                        //        validator.ErrorMessage = "Please select Day.\n";
                        //        validator.SetFocusOnError = true;
                        //        validator.InitialValue = "-1";
                        //        validator.Display = ValidatorDisplay.None;
                        //        cell.Controls.Add(validator);
                        //        cell.Controls.Add(validationsum);
                        //    }
                        //}
                    }

                }
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.ProgClassRoom, "rgProgClassRoom_ItemCreated", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
            }
        }

        protected void rgProgClassRoom_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item.ItemType == GridItemType.AlternatingItem || e.Item.ItemType == GridItemType.Item)
            {
                CheckBox chkClassRoom = e.Item.FindControl("chkClassRoom") as CheckBox;
                //CheckBox chkActive = e.Item.FindControl("chkActive") as CheckBox;
                //DropDownList ddlProgStaff = e.Item.FindControl("ddlProgStaff") as DropDownList;

                DayCarePL.ProgClassRoomProperties objProgClassRoom = e.Item.DataItem as DayCarePL.ProgClassRoomProperties;
                if (objProgClassRoom != null)
                {

                    //if (ddlProgStaff != null)
                    //{
                    //Guid SchoolId = new Guid();
                    //if (Session["SchoolId"] != null)
                    //{
                    //    SchoolId = new Guid(Session["SchoolId"].ToString());
                    //}
                    //if (ViewState["SchoolProgramId"] != null)
                    //{
                    //    Common.BindStaffFromProgStaff(ddlProgStaff, new Guid(ViewState["SchoolProgramId"].ToString()));
                    //}
                    if (objProgClassRoom.Active)
                    {
                        chkClassRoom.Checked = true;

                        //if (ddlProgStaff != null && ddlProgStaff.Items.Count > 0)
                        //{
                        //    ddlProgStaff.SelectedValue = Convert.ToString(objProgClassRoom.ProgStaffId);
                        //}
                    }
                    // }
                    //if (objProgStaff.Active == true)
                    //{
                    //    chkActive.Checked = true;
                    //}
                }
            }

        }

        protected void rgProgClassRoom_UpdateCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
        }

        protected void rgProgClassRoom_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {

        }

        protected void chkClassRoom_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                DayCareBAL.ProgClassRoomService proxyProgClassRoom = new DayCareBAL.ProgClassRoomService();
                DayCarePL.ProgClassRoomProperties objProgClassRoom = new DayCarePL.ProgClassRoomProperties();

                CheckBox chkClassRoom = sender as CheckBox;
                GridDataItem item = chkClassRoom.NamingContainer as GridDataItem;
                if (ViewState["SchoolProgramId"] != null)
                {
                    if (item.ItemIndex > -1)
                    {
                        objProgClassRoom.Id = new Guid(item.GetDataKeyValue("Id").ToString());
                        //objProgClassRoom.ProgStaffId = new Guid((item.FindControl("ddlProgStaff") as DropDownList).SelectedValue);
                        objProgClassRoom.SchoolProgramId = new Guid(ViewState["SchoolProgramId"].ToString());
                        objProgClassRoom.Active = chkClassRoom.Checked;
                        objProgClassRoom.ClassRoomId = new Guid((item.FindControl("lblClassRoomId") as Label).Text);
                        if (Session["StaffId"] != null)
                        {
                            objProgClassRoom.CreatedById = new Guid(Session["StaffId"].ToString());
                            objProgClassRoom.LastModifiedById = new Guid(Session["StaffId"].ToString());
                        }
                    }
                    if (proxyProgClassRoom.Save(objProgClassRoom))
                    {
                        //MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                        //MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Saved Successfully.!", "false"));
                        //if (chkClassRoom.Checked == false)
                        //{
                        //    chkClassRoom.Checked = false;
                        //(item.FindControl("ddlProgStaff") as DropDownList).SelectedValue = DayCarePL.Common.GUID_DEFAULT;
                        //}

                        //MasterAjaxManager = this.Page.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                        //MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Saved Successfully", "false"));

                        rgProgClassRoom.Rebind();
                    }
                    else
                    {
                        MasterAjaxManager = this.Page.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                        MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Internal Error!", "false"));
                        chkClassRoom.Checked = false;
                        //(item.FindControl("ddlProgStaff") as DropDownList).SelectedValue = DayCarePL.Common.GUID_DEFAULT;
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.ProgClassRoom, "chkClassRoom_CheckedChanged", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
            }
        }
    }
}
