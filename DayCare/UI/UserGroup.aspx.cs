using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using Telerik.Web.UI;
using System.Web.UI.WebControls;

namespace DayCare.UI
{
    public partial class usergroup : System.Web.UI.Page
    {
        RadAjaxManager MasterAjaxManager;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["SchoolId"] == null || Session["CurrentSchoolYearId"] == null || Session["UserGroupTitle"] == null || Session["Role_Id"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
        }

        protected void rgUserGroup_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DayCareBAL.UserGroupService proxyLoad = new DayCareBAL.UserGroupService();
            Guid SchoolId = new Guid();
            if (Session["SchoolId"] != null)
            {
                SchoolId = new Guid(Session["SchoolId"].ToString());
            }
            rgUserGroup.DataSource = proxyLoad.LoadUserGroup(SchoolId);
        }

        protected void rgUserGroup_EditCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            GridDataItem item = (GridDataItem)e.Item;
            hdnName.Value = item["GroupTitle"].Text;
        }

        protected void rgUserGroup_InsertCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            bool isValid = SubmitRecord(source, e);
            if (isValid == false)
            {
                e.Canceled = true;
            }
            rgUserGroup.MasterTableView.CurrentPageIndex = 0;
        }

        protected void rgUserGroup_ItemCreated(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item is GridEditableItem && e.Item.IsInEditMode)
            {
                GridEditableItem item = e.Item as GridEditableItem;

                if (item != null)
                {
                    TextBox txtName = item["GroupTitle"].FindControl("txtGroupTitle") as TextBox;
                    DropDownList ddlRole = item["Role"].FindControl("ddlRole") as DropDownList;
                    if (txtName != null)
                    {
                        TableCell cell = (TableCell)txtName.Parent;
                        TableCell cell1 = (TableCell)ddlRole.Parent;
                        RequiredFieldValidator validator = new RequiredFieldValidator();
                        RequiredFieldValidator validator1 = new RequiredFieldValidator();
                        if (cell != null)
                        {

                            txtName.ID = "txtGroupTitle";
                            validator.ControlToValidate = txtName.ID;
                            validator.ErrorMessage = "Please Enter Group Title\n";
                            validator.SetFocusOnError = true;
                            validator.Display = ValidatorDisplay.None;
                        }
                        if (cell1 != null)
                        {
                            ddlRole.ID = "ddlRole";
                            validator1.ControlToValidate = ddlRole.ID;
                            validator1.InitialValue = "00000000-0000-0000-0000-000000000000";
                            validator1.ErrorMessage = "Please Select Role\n";
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

        protected void rgUserGroup_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item.ItemType == Telerik.Web.UI.GridItemType.EditItem)
            {
                DropDownList ddlRole = e.Item.FindControl("ddlRole") as DropDownList;
                Common.BindRoles(ddlRole);

                DayCarePL.UserGroupProperties dataOfUserGroup = e.Item.DataItem as DayCarePL.UserGroupProperties;
                if (dataOfUserGroup != null)
                {
                    ddlRole.Items.FindByValue(dataOfUserGroup.RoleId.ToString()).Selected = true;
                }
            }
        }

        protected void rgUserGroup_UpdateCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            bool isvalid = SubmitRecord(source, e);
            if (isvalid == false)
            {
                e.Canceled = true;
            }
            e.Item.Expanded = false;
            rgUserGroup.MasterTableView.Rebind();
            rgUserGroup.MasterTableView.CurrentPageIndex = 0;
        }

        public bool SubmitRecord(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clUserGroup, "SubmitRecord", "Submit record method called", DayCarePL.Common.GUID_DEFAULT);
            bool result = false;
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clUserGroup, "SubmitRecord", "Debug Submit record method called of UserGroup", DayCarePL.Common.GUID_DEFAULT);
                DayCareBAL.UserGroupService proxySave = new DayCareBAL.UserGroupService();
                DayCarePL.UserGroupProperties objUserGroup = new DayCarePL.UserGroupProperties();

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
                                    case "GroupTitle":
                                        {
                                            objUserGroup.GroupTitle = (e.Item.FindControl("txtGroupTitle") as TextBox).Text;
                                            ViewState["GroupTitle"] = objUserGroup.GroupTitle;
                                            break;
                                        }
                                    case "Role":
                                        {
                                            objUserGroup.RoleId = new Guid((e.Item.FindControl("ddlRole") as DropDownList).SelectedValue);
                                            ViewState["RoleName"] = objUserGroup.RoleId;
                                            break;
                                        }
                                    case "Comments":
                                        {
                                            objUserGroup.Comments = (e.Item.FindControl("txtComments") as TextBox).Text;
                                            ViewState["Comments"] = objUserGroup.Comments;
                                            break;
                                        }
                                }
                            }
                        }
                    }
                    if (Session["SchoolId"] != null)
                    {
                        objUserGroup.SchoolId = new Guid(Session["SchoolId"].ToString());
                    }
                    if (e.CommandName != "PerformInsert")
                    {
                        if (Session["StaffId"] != null)
                        {
                            objUserGroup.LastModifiedById = new Guid(Session["StaffId"].ToString());
                        }
                        objUserGroup.Id = new Guid(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Id"].ToString());
                        if (!objUserGroup.GroupTitle.Trim().Equals(hdnName.Value.Trim()))
                        {
                            bool ans = proxySave.CheckDuplicateUserGroupTitle(objUserGroup.GroupTitle, objUserGroup.Id, objUserGroup.SchoolId);
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
                        bool ans = proxySave.CheckDuplicateUserGroupTitle(objUserGroup.GroupTitle, objUserGroup.Id, objUserGroup.SchoolId);
                        //bool ans = Common.CheckDuplicate("UserGroup", "Group Title", objUserGroup.GroupTitle, "insert", objUserGroup.Id.ToString());
                        if (ans)
                        {
                            MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                            MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Already Exist", "false"));
                            return false;
                        }
                    }
                    hdnName.Value = "";
                    result = proxySave.Save(objUserGroup);
                    if (result == true)
                    {
                        MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                        MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Saved Successfully", "false"));
                    }
                }
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.UserGroup, "SubmitRecord", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                result = false;
            }
            return result;
        }

        protected void rgUserGroup_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
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

                if (Session["UserGroupTitle"] != null && Session["Role_Id"]!=null)
                {
                    //if (Convert.ToString(Session["UserGroupTitle"]).Equals(DayCarePL.Common.SCHOOL_ADMINISTRATOR) || Convert.ToString(Session["Role_Id"]).ToLower().Equals(DayCarePL.Common.ADMIN_ROLE_ID.ToLower()))
                    //{
                    //    if (!Common.IsCurrentYear(CurrentSchoolYearId, SchoolId))
                    //    {
                    //        if (e.CommandName == "InitInsert")
                    //        {
                    //            e.Canceled = true;
                    //        }
                    //        else if (e.CommandName == "Edit")
                    //        {
                    //            e.Canceled = true;
                    //        }
                    //    }
                    //}
                    //else
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
                else
                {
                    Response.Redirect("~/Login.aspx");
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
