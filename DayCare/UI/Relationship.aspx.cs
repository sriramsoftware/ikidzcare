using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace DayCare.UI
{
    public partial class Relationship : System.Web.UI.Page
    {
        RadAjaxManager MasterAjaxManager;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["SchoolId"] == null || Session["CurrentSchoolYearId"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
        }

        protected void rgRelationship_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DayCareBAL.RelationshipService proxyRelationship = new DayCareBAL.RelationshipService();
            Guid SchoolId = new Guid();
            if (Session["SchoolId"] != null)
            {
                SchoolId = new Guid(Session["SchoolId"].ToString());
            }
            rgRelationship.DataSource = proxyRelationship.LoadRelationship(SchoolId);
        }

        protected void rgRelationship_InsertCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            bool isValid = SubmitRecord(sender, e);
            if (isValid == false)
            {
                e.Canceled = true;
            }
            rgRelationship.MasterTableView.CurrentPageIndex = 0;
        }

        protected void rgRelationship_ItemCreated(object sender, Telerik.Web.UI.GridItemEventArgs e)
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

                    TextBox txtName = item["Name"].FindControl("txtName") as TextBox;
                    if (txtName != null)
                    {
                        cell = (TableCell)txtName.Parent;
                        validator = new RequiredFieldValidator();
                        if (cell != null)
                        {
                            txtName.ID = "txtName";
                            validator.ControlToValidate = txtName.ID;
                            validator.ErrorMessage = "Please enter Relationship name \n";
                            validator.SetFocusOnError = true;
                            validator.Display = ValidatorDisplay.None;
                            cell.Controls.Add(validator);
                            cell.Controls.Add(validationsum);
                        }
                    }
                }
            }
        }

        protected void rgRelationship_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
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

        protected void rgRelationship_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
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

        protected void rgRelationship_EditCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            GridDataItem item = (GridDataItem)e.Item;
            hdnName.Value = (item["Name"].FindControl("lblName") as Label).Text;
        }

        protected void rgRelationship_UpdateCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            bool isValid = SubmitRecord(sender, e);
            if (isValid == false)
            {
                e.Canceled = true;
            }
        }

        protected void rgRelationship_DeleteCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {

        }

        public bool SubmitRecord(object sender, GridCommandEventArgs e)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.SchoolYear, "SubmitRecord", "Submit record method called", DayCarePL.Common.GUID_DEFAULT);
            bool result = false;

            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.SchoolYear, "SubmitRecord", "Debug Submit Record Of SchoolYear", DayCarePL.Common.GUID_DEFAULT);
                DayCareBAL.RelationshipService proxyRelationship = new DayCareBAL.RelationshipService();
                DayCarePL.RelationshipProperties objRelationship = new DayCarePL.RelationshipProperties();

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
                                            objRelationship.Name = (e.Item.FindControl("txtName") as TextBox).Text;
                                            break;
                                        }
                                    case "Comments":
                                        {
                                            objRelationship.Comments = (e.Item.FindControl("txtComments") as TextBox).Text;
                                            break;
                                        }
                                    case "Active":
                                        {
                                            objRelationship.Active = (editor as GridCheckBoxColumnEditor).Value;
                                            break;
                                        }
                                }
                            }
                        }
                    }

                    if (Session["SchoolId"] != null)
                    {
                        objRelationship.SchoolId = new Guid(Session["SchoolId"].ToString());
                    }

                    if (e.CommandName != "PerformInsert")
                    {
                        objRelationship.Id = new Guid(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Id"].ToString());

                        bool ans = proxyRelationship.CheckDuplicateRelationshipName(objRelationship.Name, objRelationship.Id, objRelationship.SchoolId);
                        if (ans)
                        {
                            MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                            MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Relationship name Already Exist", "false"));
                            return false;
                        }

                        if (Session["StaffId"] != null)
                        {
                            objRelationship.LastModifiedById = new Guid(Session["StaffId"].ToString());
                        }
                    }
                    else
                    {
                        bool ans = proxyRelationship.CheckDuplicateRelationshipName(objRelationship.Name, objRelationship.Id, objRelationship.SchoolId);
                        if (ans)
                        {
                            MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                            MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Relationship name Already Exist", "false"));
                            return false;
                        }
                    }
                    hdnName.Value = "";

                    result = proxyRelationship.Save(objRelationship);
                    if (result)
                    {
                        MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                        MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Saved Successfully", "false"));
                    }


                }
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.Relationship, "SubmitRecord", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                result = false;
            }
            return result;
        }
    }
}
