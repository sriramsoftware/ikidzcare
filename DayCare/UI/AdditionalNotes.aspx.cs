using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace DayCare.UI
{
    public partial class AdditionalNotes : System.Web.UI.Page
    {
        RadAjaxManager MasterAjaxManager;
        //Guid SchoolId = new Guid();
        Guid CurrentSchoolYearId = new Guid();
        Guid ChildDataId = new Guid();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["SchoolId"] == null || Session["CurrentSchoolYearId"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            if (Request.QueryString["ChildDataId"].ToString() != null)
            {
                ViewState["ChildDataId"] = Request.QueryString["ChildDataId"].ToString();
            }
            if (!IsPostBack)
                radCommentDate.SelectedDate = DateTime.Now;
        }

        protected void rgAdditionalNotes_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            ChildDataId = new Guid(ViewState["ChildDataId"].ToString());
            Guid ChildSchoolYearId = Common.GetChildSchoolYearId(ChildDataId, new Guid(Session["CurrentSchoolYearId"].ToString()));
            DayCareBAL.AdditionalNotesService LoadAdditionalNotes = new DayCareBAL.AdditionalNotesService();
            rgAdditionalNotes.DataSource = LoadAdditionalNotes.LoadAdditionalNotes(ChildSchoolYearId);
        }

        protected void rgAdditionalNotes_EditCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
        }

        protected void rgAdditionalNotes_InsertCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
        }

        protected void rgAdditionalNotes_ItemCreated(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
        }

        protected void rgAdditionalNotes_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
        }

        protected void rgAdditionalNotes_UpdateCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            rgAdditionalNotes.MasterTableView.Rebind();
        }

        protected void rgAdditionalNotes_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {
                //Guid ChildDataID = new Guid(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Id"].ToString());
                if (e.CommandName == "Edit")
                {
                    GridEditableItem dataItem = (GridEditableItem)e.Item;
                    ChildDataId = new Guid(ViewState["ChildDataId"].ToString());
                    ViewState["Id"] = dataItem.GetDataKeyValue("Id").ToString();
                    Guid ChildSchoolYearId = Common.GetChildSchoolYearId(ChildDataId, new Guid(Session["CurrentSchoolYearId"].ToString()));
                    DayCareBAL.AdditionalNotesService proxyAdditionalNotes = new DayCareBAL.AdditionalNotesService();
                    DayCarePL.AdditionalNotesProperties[] objNotes = proxyAdditionalNotes.GetAdditionNoteById(new Guid(dataItem.GetDataKeyValue("Id").ToString()), ChildSchoolYearId);
                    if (objNotes != null)
                    {
                        radCommentDate.SelectedDate = Convert.ToDateTime(objNotes[0].CommentDate);
                        txtComment.Text = objNotes[0].Comments;
                    }
                    e.Canceled = true;

                }
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.AddEditChild, "rgAddEditChid_ItemCommand", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.AdditionalNote, "btnSave_Click", "Submit btnSave_Click called", DayCarePL.Common.GUID_DEFAULT);
            try
            {
                bool result = false;
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.AdditionalNote, "btnSave_Click", "Debug btnSave_Click ", DayCarePL.Common.GUID_DEFAULT);
                DayCareBAL.AdditionalNotesService proxyAdditionalNoteService = new DayCareBAL.AdditionalNotesService();
                DayCarePL.AdditionalNotesProperties objNote = new DayCarePL.AdditionalNotesProperties();
                ChildDataId = new Guid(ViewState["ChildDataId"].ToString());
                Guid ChildSchoolYearId = Common.GetChildSchoolYearId(ChildDataId, new Guid(Session["CurrentSchoolYearId"].ToString()));
                objNote.ChildSchoolYearId = ChildSchoolYearId;
                objNote.CommentDate = Convert.ToDateTime(radCommentDate.SelectedDate.ToString());
                objNote.Comments = txtComment.Text.ToString().Trim();
                if (ViewState["Id"] != null)
                {
                    objNote.Id = new Guid(ViewState["Id"].ToString());
                }
                if (Session["StaffId"] != null)
                {
                    objNote.LastModifiedById = new Guid(Session["StaffId"].ToString());
                    objNote.CreatedById = new Guid(Session["StaffId"].ToString());
                }
                result = proxyAdditionalNoteService.Save(objNote);
                if (result)
                {
                    radCommentDate.SelectedDate = DateTime.Now;
                    txtComment.Text = "";
                    rgAdditionalNotes.MasterTableView.Rebind();
                    ViewState["Id"] = null;
                }
                else
                {
                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void rgAdditionalNotes_DeleteCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                DayCareBAL.AdditionalNotesService proxyAdditionalNotesdelete = new DayCareBAL.AdditionalNotesService();
                //DayCarePL.LedgerProperties objTest = e.Item.DataItem as DayCarePL.LedgerProperties;
                Guid Id = new Guid(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Id"].ToString());

                if (proxyAdditionalNotesdelete.DeleteAdditionalNotes(Id))
                {
                    MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                    MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Deleted Successfully", "false"));
                    //rgLedger.MasterTableView.Rebind();
                }
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.Ledger, "rgLedger_DeleteCommand", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
            }
        }
    }
}
