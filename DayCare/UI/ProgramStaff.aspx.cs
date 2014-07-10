using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace DayCare.UI
{
    public partial class ProgramStaff : System.Web.UI.Page
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
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.ProgStaff, "Page_Load", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);

            }
        }

        protected void rgProgramStaff_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                DayCareBAL.ProgStaffService proxyProgStaff = new DayCareBAL.ProgStaffService();
                Guid SchoolId = new Guid();
                if (Session["SchoolId"] != null)
                {
                    SchoolId = new Guid(Session["SchoolId"].ToString());
                }
                if (ViewState["SchoolProgramId"] != null && ViewState["SchoolYearId"] != null && ViewState["IsPrimary"] != null)
                {
                    List<DayCarePL.ProgStaffProperties> lstStaff= proxyProgStaff.LoadStaffBySchoolProgram(SchoolId, new Guid(ViewState["SchoolYearId"].ToString()), new Guid(ViewState["SchoolProgramId"].ToString()), Convert.ToBoolean(ViewState["IsPrimary"].ToString()));
                    if(lstStaff!=null && lstStaff.Count>0)
                    {
                        if(Convert.ToBoolean(ViewState["IsPrimary"].ToString()))
                        {
                            lstStaff=lstStaff.FindAll(p=>p.IsPrimary.Equals(true));
                        }
                        rgProgramStaff.DataSource = lstStaff.FindAll(title => !title.GroupTitle.Equals(DayCarePL.Common.SCHOOL_ADMINISTRATOR)); ;
                    }
                }
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.ProgStaff, "rgProgramStaff_NeedDataSource", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
            }
        }

        protected void rgProgramStaff_InsertCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {

        }

        protected void rgProgramStaff_ItemCreated(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {

        }

        protected void rgProgramStaff_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {

        }

        protected void rgProgramStaff_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item.ItemType == GridItemType.AlternatingItem || e.Item.ItemType == GridItemType.Item)
            {
                CheckBox chkStaff = e.Item.FindControl("chkStaff") as CheckBox;
                CheckBox chkActive = e.Item.FindControl("chkActive") as CheckBox;

                DayCarePL.ProgStaffProperties objProgStaff = e.Item.DataItem as DayCarePL.ProgStaffProperties;
                if (objProgStaff != null)
                {
                    if (objProgStaff.Active)
                    {
                        chkStaff.Checked = true;
                    }
                    if (objProgStaff.Active == true)
                    {
                        chkActive.Checked = true;
                    }
                }
            }
        }

        protected void rgProgramStaff_EditCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {

        }

        protected void rgProgramStaff_UpdateCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {

        }

        protected void rgProgramStaff_DeleteCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {

        }

        public bool SubmitRecord(object sender, GridCommandEventArgs e)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.SchoolYear, "SubmitRecord", "Submit record method called", DayCarePL.Common.GUID_DEFAULT);
            bool result = false;

            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.SchoolYear, "SubmitRecord", "Debug Submit Record Of SchoolYear", DayCarePL.Common.GUID_DEFAULT);
                DayCareBAL.ProgStaffService proxyProgStaff = new DayCareBAL.ProgStaffService();
                DayCarePL.ProgStaffProperties objProgStaff = new DayCarePL.ProgStaffProperties();

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
                                            objProgStaff.StaffId = new Guid((e.Item.FindControl("lblStaffId") as Label).Text);
                                            break;
                                        }
                                    case "Active":
                                        {
                                            objProgStaff.Active = (e.Item.FindControl("chkActive") as CheckBox).Checked;
                                            break;
                                        }
                                }
                            }
                        }
                    }



                    if (e.CommandName != "PerformInsert")
                    {
                        objProgStaff.Id = new Guid(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Id"].ToString());

                        if (Session["StaffId"] != null)
                        {
                            objProgStaff.LastModifiedById = new Guid(Session["StaffId"].ToString());
                        }
                    }
                    else
                    {

                    }
                    hdnName.Value = "";

                    //result = proxyProgStaff.Save(objProgStaff);
                    //MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                    //MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Saved Successfully!", "false"));


                }
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.Relationship, "SubmitRecord", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                result = false;
            }
            return result;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                DayCareBAL.ProgStaffService proxyProgStaff = new DayCareBAL.ProgStaffService();
                DayCarePL.ProgStaffProperties objProgStaff = new DayCarePL.ProgStaffProperties();
                List<DayCarePL.ProgStaffProperties> lstProgStaff = new List<DayCarePL.ProgStaffProperties>();

                if (ViewState["SchoolProgramId"] != null)
                {
                    foreach (GridItem itm in rgProgramStaff.Items)
                    {
                        GridDataItem items = itm as GridDataItem;
                        CheckBox chkStaff = items["SelectCheckStaff"].FindControl("chkStaff") as CheckBox;
                        //CheckBox chkActive = items["Active"].FindControl("chkActive") as CheckBox;
                        Label lblStaffId = items["StaffId"].FindControl("lblStaffId") as Label;

                        if (chkStaff.Checked)
                        {
                            objProgStaff = new DayCarePL.ProgStaffProperties();
                            objProgStaff.StaffId = new Guid(lblStaffId.Text);

                            objProgStaff.Active = true;// chkActive.Checked;
                            if (Session["StaffId"] != null)
                            {
                                objProgStaff.CreatedById = new Guid(Session["StaffId"].ToString());
                                objProgStaff.LastModifiedById = new Guid(Session["StaffId"].ToString());
                            }
                            objProgStaff.SchoolProgramId = new Guid(ViewState["SchoolProgramId"].ToString());
                            lstProgStaff.Add(objProgStaff);
                        }
                    }


                }

            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.ProgStaff, "btnSave_Click", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
            }

        }

        protected void chkStaff_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                DayCareBAL.ProgStaffService proxyProgStaff = new DayCareBAL.ProgStaffService();
                DayCarePL.ProgStaffProperties objProgStaff = new DayCarePL.ProgStaffProperties();
                List<DayCarePL.ProgStaffProperties> lstProgStaff = new List<DayCarePL.ProgStaffProperties>();

                CheckBox chkStaff = sender as CheckBox;
                GridDataItem item = chkStaff.NamingContainer as GridDataItem;
                if (ViewState["SchoolProgramId"] != null)
                {
                    if (item.ItemIndex > -1)
                    {
                        objProgStaff.Id = new Guid(item.GetDataKeyValue("Id").ToString());
                        objProgStaff.StaffId = new Guid((item.FindControl("lblStaffId") as Label).Text);
                        objProgStaff.SchoolProgramId = new Guid(ViewState["SchoolProgramId"].ToString());
                        objProgStaff.Active = chkStaff.Checked;
                        if (Session["StaffId"] != null)
                        {
                            objProgStaff.CreatedById = new Guid(Session["StaffId"].ToString());
                            objProgStaff.LastModifiedById = new Guid(Session["StaffId"].ToString());
                        }
                    }

                    if (proxyProgStaff.Save(objProgStaff))
                    {
                        MasterAjaxManager = this.Page.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                        MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Saved Successfully", "false"));
                        rgProgramStaff.Rebind();
                    }
                    else
                    {
                        MasterAjaxManager = this.Page.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                        MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Internal Error!", "false"));
                        return;
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
