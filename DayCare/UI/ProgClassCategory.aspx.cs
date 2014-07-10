using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace DayCare.UI
{
    public partial class ProgClassCategory : System.Web.UI.Page
    {
        RadAjaxManager MasterAjaxManager;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["SchoolId"] == null && Session["CurrentSchoolYearId"] == null)
                {
                    Response.Redirect("~/Login.aspx");
                }
                if (string.IsNullOrEmpty(Request.QueryString["SchoolProgramId"]) && string.IsNullOrEmpty(Request.QueryString["SchoolYearId"]))
                {
                    Response.Redirect("~/Login.aspx");
                }
                else
                {
                    ViewState["SchoolProgramId"] = Request.QueryString["SchoolProgramId"].ToString();
                    ViewState["SchoolYearId"] = Request.QueryString["SchoolYearId"].ToString();
                    
                }
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.ProgClassCategory, "Page_Load", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
            }

           
        }

        protected void rgProgClassCategory_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DayCareBAL.ProgClassCategoryService proxyLoad = new DayCareBAL.ProgClassCategoryService();
            Guid SchoolId = new Guid();
            
            if (Session["SchoolId"] != null)
            {
                SchoolId = new Guid(Session["SchoolId"].ToString());
            }
           rgProgClassCategory.DataSource = proxyLoad.LoadProgClassCategory(new Guid(ViewState["SchoolProgramId"].ToString()),SchoolId);
            
        }

        protected void rgProgClassCategory_EditCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            GridDataItem item = (GridDataItem)e.Item;
            hdnName.Value = item["Title"].Text;
        }

        protected void rgProgClassCategory_InsertCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
        }

        protected void rgProgClassCategory_ItemCreated(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item is GridEditableItem && e.Item.IsInEditMode)
            {
                GridEditableItem item = e.Item as GridEditableItem;

                if (item != null)
                {
                    DropDownList ddlSchool = item["Title"].FindControl("ddlSchoolProgram") as DropDownList;
                    if (ddlSchool != null)
                    {
                        TableCell cell = (TableCell)ddlSchool.Parent;
                        RequiredFieldValidator validator = new RequiredFieldValidator();
                        if (cell != null)
                        {

                            ddlSchool.ID = "txtReason";
                            validator.ControlToValidate = ddlSchool.ID;
                            validator.ErrorMessage = "Please Select SchoolProgram\n";
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

        protected void rgProgClassCategory_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item.ItemType == GridItemType.Item || e.Item.ItemType == GridItemType.AlternatingItem)
            {
                
                DayCarePL.ProgClassCategoryProperties dataOfClassCategory = e.Item.DataItem as DayCarePL.ProgClassCategoryProperties;
                CheckBox chkAssign = e.Item.FindControl("Assign") as CheckBox;
             
                if (dataOfClassCategory != null)
                {
                    //if (!dataOfClassCategory.Assign.ToString().Equals(DayCarePL.Common.GUID_DEFAULT))
                    if(dataOfClassCategory.Active ==true)
                    {
                        chkAssign.Checked = true;
                    
                    }
                    if (dataOfClassCategory.Assign.ToString().Equals(DayCarePL.Common.GUID_DEFAULT))
                    {
                        chkAssign.Enabled = true;
                    }
                }
            }
        }

        protected void rgProgClassCategory_UpdateCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
        }

        /* public bool SubmitRecord(object source, Telerik.Web.UI.GridCommandEventArgs e)
         { 
         }*/

        protected void rgProgClassCategory_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
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
        }


        protected void btnSave_Click(object sender, EventArgs e)
        {
      /*      try
            {
                DayCareBAL.ProgClassCategoryService proxyProgClassCategory = new DayCareBAL.ProgClassCategoryService();
                DayCarePL.ProgClassCategoryProperties objProgClassCategory = new DayCarePL.ProgClassCategoryProperties();
                List<DayCarePL.ProgClassCategoryProperties> lstProgClassCategory = new List<DayCarePL.ProgClassCategoryProperties>();

                if (ViewState["SchoolProgramId"] != null)
                {
                    foreach (GridItem item in rgProgClassCategory.Items)
                    {
                        GridDataItem dataItem = item as GridDataItem;

                        CheckBox chkAssign = dataItem["Assign"].FindControl("Assign") as CheckBox;
                        Label lblId = dataItem["Id"].FindControl("lblClassCategoryId") as Label;

                        if (chkAssign.Checked == true)
                        {
                            objProgClassCategory.ClassCategoryId = new Guid(lblId.Text);
                            if (Session["StaffId"] != null)
                            {
                                objProgClassCategory.CreatedById = new Guid(Session["StaffId"].ToString());
                                objProgClassCategory.LastModifiedById = new Guid(Session["StaffId"].ToString());
                            }
                            objProgClassCategory.SchoolProgramId = new Guid(ViewState["SchoolProgramId"].ToString());
                            lstProgClassCategory.Add(objProgClassCategory);
                        }

                    }
                    if (proxyProgClassCategory.Save(objProgClassCategory, new Guid(ViewState["SchoolProgramId"].ToString())))
                    {
                        //MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                        //MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Saved Successfully.!", "false"));
                        return;
                    }
                    else
                    {
                        MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                        MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Internal Error.!", "false"));
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.ProgClassCategory, "btnSave_Click", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
            } */
        }

        protected void Assign_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                DayCareBAL.ProgClassCategoryService proxyProgClassCategory = new DayCareBAL.ProgClassCategoryService();
                DayCarePL.ProgClassCategoryProperties objProgClassCategory = new DayCarePL.ProgClassCategoryProperties();
                List<DayCarePL.ProgClassCategoryProperties> lstProgClassCategory = new List<DayCarePL.ProgClassCategoryProperties>();
                if (ViewState["SchoolProgramId"] != null)
                {
                     CheckBox chkAssign=(CheckBox)sender;
                     GridDataItem dataItem = (GridDataItem)chkAssign.NamingContainer;
                     chkAssign = dataItem["Assign"].FindControl("Assign") as CheckBox;
                      Label lblId = dataItem["Id"].FindControl("lblClassCategoryId") as Label;
                      Label lblProgClassCatId = dataItem["ProgClassCatId"].FindControl("lblProgClassCatId") as Label;
                      objProgClassCategory.ClassCategoryId = new Guid(lblId.Text);
                         if (Session["StaffId"] != null)
                         {
                             objProgClassCategory.CreatedById = new Guid(Session["StaffId"].ToString());
                             objProgClassCategory.LastModifiedById = new Guid(Session["StaffId"].ToString());
                             objProgClassCategory.Active = chkAssign.Checked;
                             objProgClassCategory.Id = new Guid(lblProgClassCatId.Text);                                 
                         }
                         objProgClassCategory.SchoolProgramId = new Guid(ViewState["SchoolProgramId"].ToString());
                         lstProgClassCategory.Add(objProgClassCategory);
                     
                    
                     if (proxyProgClassCategory.Save(objProgClassCategory, new Guid(ViewState["SchoolProgramId"].ToString())))
                     {
                         MasterAjaxManager = this.Page.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                         MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Saved Successfully", "false"));
                         rgProgClassCategory.MasterTableView.Rebind();
                         return;
                     }
                     else
                     {
                         MasterAjaxManager = this.Page.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                         MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Internal Error.!", "false"));
                         return;
                     }
                     
                }
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.ProgClassCategory, "btnSave_Click", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
            }
        
        }
    }
}
