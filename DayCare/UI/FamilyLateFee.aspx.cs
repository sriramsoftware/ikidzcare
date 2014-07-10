using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace DayCare.UI
{
    public partial class FamilyLateFee : System.Web.UI.Page
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
                List<DayCarePL.ChildFamilyProperties> lstLedger = new List<DayCarePL.ChildFamilyProperties>();
                rgLateFee.DataSource = lstLedger;
                rgLateFee.DataBind();
            }
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
                btnTopSave.Enabled = false;
                btnBottomSave.Enabled = false;
                btnTopSave.Style.Add("cursor", "pointer");
                btnBottomSave.Style.Add("cursor", "pointer");
                btnTopSave.ToolTip = "you can not apply late fee charge,because you have not selected current year";
                btnBottomSave.ToolTip = "you can not apply late fee charge,because you have not selected current year";
            }
            this.Form.DefaultButton = btnOk.UniqueID;
        }

        //protected void rgLateFee_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        //{
        //    //DayCareBAL.LedgerService proxyLedger = new DayCareBAL.LedgerService();
        //    //List<DayCarePL.LedgerProperties> lstLeger = proxyLedger.LoadLedgerDetail(new Guid(Session["CurrentSchoolYearId"].ToString()), new Guid(ViewState["ChildFamilyId"].ToString()));
        //    //if (lstLeger != null)
        //    //{
        //    //    rgLateFee.DataSource = lstLeger;
        //    //}


        //}

        protected void btnOk_Click(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void btnTopSave_Click(object sender, EventArgs e)
        {
            try
            {
                DayCareBAL.LedgerService proxyLedger = new DayCareBAL.LedgerService();
                DayCarePL.LedgerProperties objLedger = new DayCarePL.LedgerProperties();
                List<DayCarePL.LedgerProperties> lstLedger = new List<DayCarePL.LedgerProperties>();
                if (Session["CurrentSchoolYearId"] == null)
                    return;
                foreach (GridDataItem e1 in rgLateFee.MasterTableView.Items)
                {
                    if (!string.IsNullOrEmpty((e1.FindControl("txtFamilyLateFee") as TextBox).Text.Trim()))
                    {
                        objLedger = new DayCarePL.LedgerProperties();
                        objLedger.SchoolYearId = new Guid(Session["CurrentSchoolYearId"].ToString());
                        objLedger.ChildFamilyId = new Guid(e1.GetDataKeyValue("Id").ToString());
                        objLedger.TransactionDate = DateTime.Now;
                        objLedger.Debit = Convert.ToDecimal((e1.FindControl("txtFamilyLateFee") as TextBox).Text.Trim());
                        objLedger.Credit = 0;
                        objLedger.Comment = "Late Fee Charged";
                        objLedger.LateFee = 1;
                        objLedger.CreatedDateTime = DateTime.Now;
                        objLedger.LastModifiedDatetime = DateTime.Now;
                        if (Session["StaffId"] != null)
                        {
                            objLedger.CreatedById = new Guid(Session["StaffId"].ToString());
                            objLedger.LastModifiedById = new Guid(Session["StaffId"].ToString());
                        }
                        lstLedger.Add(objLedger);
                    }
                }
                if (lstLedger != null && lstLedger.Count > 0)
                {
                    if (proxyLedger.SaveLateFeeOfFamily(lstLedger))
                    {
                        MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                        MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Saved Successfully", "false"));
                        txtLateCharge.Text = "";
                        BindGrid();
                    }
                    else
                    {
                        MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                        MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Internal Error", "false"));

                    }
                }
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.LateFee, "btnSave_Click", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
            }
        }

        public void BindGrid()
        {
            if (Session["SchoolId"] == null || Session["CurrentSchoolYearId"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            if (!string.IsNullOrEmpty(txtBlance.Text))
            {
                DayCareBAL.LedgerService proxyLedger = new DayCareBAL.LedgerService();
                List<DayCarePL.ChildFamilyProperties> lstLedger = proxyLedger.GetLateFeeofFamilies(new Guid(Session["SchoolId"].ToString()), new Guid(Session["CurrentSchoolYearId"].ToString()), Convert.ToDecimal(txtBlance.Text));
                if (lstLedger != null)
                {
                    rgLateFee.DataSource = lstLedger;
                    if (lstLedger.Count > 0)
                    {
                        btnTopSave.Visible = true;
                        btnBottomSave.Visible = true;
                    }
                    //txtLateCharge.Text = "";
                }
                rgLateFee.DataBind();
            }
        }

        protected void rgLateFee_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item.ItemType == GridItemType.Item || e.Item.ItemType == GridItemType.AlternatingItem)
            {
                DayCarePL.ChildFamilyProperties objChildFamily = e.Item.DataItem as DayCarePL.ChildFamilyProperties;
                TextBox txtFamilyLateFee = e.Item.FindControl("txtFamilyLateFee") as TextBox;
                txtFamilyLateFee.Text = txtLateCharge.Text;
            }
        }
    }
}
