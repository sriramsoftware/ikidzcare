using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace DayCare.UI
{
    public partial class Test : System.Web.UI.Page
    {
        RadAjaxManager MasterAjaxManager;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["SchoolId"] == null && Session["CurrentSchoolYearId"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            if (!IsPostBack)
            {
                Common.BindChargeCode(ddlCategory);
            }
        }

        protected void rgTest_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DayCareBAL.TestService proxyTest = new DayCareBAL.TestService();
            rgTest.DataSource = proxyTest.LoadLedgerDetail(new Guid(Session["CurrentSchoolYearId"].ToString()));
        }

        protected void rgTest_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {

        }

        protected void rgTest_EditCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {

        }

        protected void rgTest_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item.ItemType == GridItemType.Item || e.Item.ItemType == GridItemType.AlternatingItem)
            {
                DayCareBAL.TestService proxyLedgerdelete = new DayCareBAL.TestService();
                DayCarePL.LedgerProperties objTest = e.Item.DataItem as DayCarePL.LedgerProperties;
                //List<DayCarePL.LedgerProperties> lstTest = proxyTest.LoadLedgerDetail(new Guid(Session["CurrentSchoolYearId"].ToString()));
                if (objTest != null)
                {
                    lblFamilyTitle.Text = objTest.FamilyName;
                    ViewState["ChildFamilyId"] = objTest.ChildFamilyId;
                    if (objTest.AllowEdit == false)
                    {
                        if (e.Item is GridDataItem)
                        {
                            GridEditableItem dataItem = e.Item as Telerik.Web.UI.GridEditableItem;
                            // ImageButton editButton = (ImageButton)item.FindControl("DeleteColumn");
                            ImageButton deleteButton = dataItem["DeleteColumn"].Controls[0] as ImageButton;
                            deleteButton.Visible = false;
                        }
                    }
                    if (objTest.AllowEdit == true)
                    {
                        if (e.Item is GridDataItem)
                        {
                            GridEditableItem dataItem = e.Item as Telerik.Web.UI.GridEditableItem;
                            // ImageButton editButton = (ImageButton)item.FindControl("DeleteColumn");
                            ImageButton deleteButton = dataItem["DeleteColumn"].Controls[0] as ImageButton;
                            deleteButton.Visible = true;
                        }

                    }

                }
            }



        }

        protected void rgTest_ItemCreated(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            rdpDate.SelectedDate = null;
            ddlCategory.SelectedValue = null;
            txtAmount.Text = "";
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.ChildData, "btnSave_Click", "Submit btnSave_Click called", DayCarePL.Common.GUID_DEFAULT);
            try
            {
                string Amount = "";
                bool result = false;
                DayCareBAL.TestService proxySaveLedger = new DayCareBAL.TestService();
                DayCarePL.LedgerProperties objLedger = new DayCarePL.LedgerProperties();
                if (Session["StaffId"] != null)
                {

                    objLedger.CreatedById = new Guid(Session["StaffId"].ToString());
                }
                objLedger.TransactionDate = Convert.ToDateTime(rdpDate.SelectedDate);

                Amount = txtAmount.Text.Trim();
                if (!string.IsNullOrEmpty(Amount))
                {
                    decimal amountresult = 0;
                    decimal.TryParse(Amount, out amountresult);
                    if (amountresult == 0)
                    {
                        objLedger.Debit = amountresult;
                        MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                        MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Pease enter valid Fees.", "false"));
                        rgTest.MasterTableView.Rebind();
                    }
                    else
                    {
                        objLedger.Debit = amountresult;
                    }

                }
                objLedger.Detail = ddlCategory.SelectedItem.ToString();
                if (ViewState["ChildFamilyId"] != null)
                {
                    objLedger.ChildFamilyId = new Guid(ViewState["ChildFamilyId"].ToString());
                }
                if (Session["CurrentSchoolYearId"] != null)
                {
                    objLedger.SchoolYearId = new Guid(Session["CurrentSchoolYearId"].ToString());
                }
                objLedger.AllowEdit = true;
                if (proxySaveLedger.SaveLedger(objLedger))
                {
                    MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                    MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Saved Successfully", "false"));
                    result = true;
                    rdpDate.SelectedDate = null;
                    ddlCategory.SelectedValue = null;
                    txtAmount.Text = "";
                }

            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.ChildData, "btnSave_Click", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Internal Error,Please try again.", "false"));
            }
        }

        protected void rgTest_DeleteCommand(object source, GridCommandEventArgs e)
        {
            DayCareBAL.TestService proxyLedgerdelete = new DayCareBAL.TestService();
            //DayCarePL.LedgerProperties objTest = e.Item.DataItem as DayCarePL.LedgerProperties;
            Guid Id = new Guid(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Id"].ToString());

            if (proxyLedgerdelete.DeleteLedger(Id))
            {
                MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Deleted Successfully", "false"));
            }

        }
    }
}
