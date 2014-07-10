using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace DayCare.UI
{
    public partial class Role : System.Web.UI.Page
    {
        RadAjaxManager MasterAjaxManager;

        protected void Page_Load(object sender, EventArgs e)
        {
            //int result =  Common.CheckDuplicate("state", "Name", "ranka", "insert","");
            if (Session["SchoolId"] == null && Session["CurrentSchoolYearId"] == null && Session["UserGroupTitle"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
        }

        protected void rgRoles_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DayCareBAL.RoleService proxyRole = new DayCareBAL.RoleService();
            DayCarePL.RoleProperties[] Roles = proxyRole.LoadRoles();
            if (Roles != null && Roles.Length > 0)
            {
                rgRoles.DataSource = Roles.ToList().FindAll(name => !name.Name.Equals(DayCarePL.Common.SCHOOL_ADMINISTRATOR));
            }
        }

        protected void rgRoles_InsertCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            bool isValid = SubmitRecord(sender, e);
            if (isValid == false)
            {
                e.Canceled = true;
            }
            rgRoles.MasterTableView.CurrentPageIndex = 0;
        }

        protected void rgRoles_ItemCreated(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item is GridEditableItem && e.Item.IsInEditMode)
            {
                GridEditableItem item = e.Item as GridEditableItem;

                if (item != null)
                {
                    TextBox txtName = item["Name"].FindControl("txtName") as TextBox;
                    if (txtName != null)
                    {
                        TableCell cell = (TableCell)txtName.Parent;
                        RequiredFieldValidator validator = new RequiredFieldValidator();
                        if (cell != null)
                        {
                            txtName.ID = "txtName";
                            validator.ControlToValidate = txtName.ID;
                            validator.ErrorMessage = "Please enter role \n";
                            validator.SetFocusOnError = true;
                            validator.Display = ValidatorDisplay.None;
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

        protected void rgRoles_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
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

            if (Session["UserGroupTitle"] != null)
            {
                if (Convert.ToString(Session["UserGroupTitle"]).Equals(DayCarePL.Common.SCHOOL_ADMINISTRATOR))
                {
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
                else
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
            else
            {
                Response.Redirect("~/Login.aspx");
            }
        }

        protected void rgRoles_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
        }

        protected void rgRoles_EditCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            GridDataItem item = (GridDataItem)e.Item;
            hdnName.Value = (item["Name"].FindControl("lblName") as Label).Text;
        }

        protected void rgRoles_UpdateCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            bool isValid = SubmitRecord(sender, e);
            if (isValid == false)
            {
                e.Canceled = true;
            }
        }

        protected void rgRoles_DeleteCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {

        }

        public bool SubmitRecord(object sender, GridCommandEventArgs e)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.Role, "SubmitRecord", "Submit record method called", DayCarePL.Common.GUID_DEFAULT);
            bool result = false;

            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.Role, "SubmitRecord", "Debug Submit Record Of Role", DayCarePL.Common.GUID_DEFAULT);
                DayCareBAL.RoleService proxyRole = new DayCareBAL.RoleService();
                DayCarePL.RoleProperties objRole = new DayCarePL.RoleProperties();

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
                                    case "Name":
                                        {
                                            objRole.Name = (e.Item.FindControl("txtName") as TextBox).Text;
                                            break;
                                        }
                                    case "Active":
                                        {
                                            objRole.Active = (editor as GridCheckBoxColumnEditor).Value;
                                            break;
                                        }
                                }
                            }
                        }
                    }

                    if (e.CommandName != "PerformInsert")
                    {
                        objRole.Id = new Guid(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Id"].ToString());
                        if (!objRole.Name.Trim().Equals(hdnName.Value.Trim()))
                        {
                            bool ans = Common.CheckDuplicate("Role", "Name", objRole.Name, "update", objRole.Id.ToString());
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
                        bool ans = Common.CheckDuplicate("Role", "Name", objRole.Name, "insert", "");
                        if (ans)
                        {
                            MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                            MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Already Exist", "false"));
                            return false;
                        }
                    }
                    hdnName.Value = "";
                    result = proxyRole.Save(objRole);
                }
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.Role, "SubmitRecord", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                result = false;
            }
            return result;
        }
    }
}
