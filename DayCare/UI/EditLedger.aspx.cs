using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace DayCare.UI
{
    public partial class EditLedger : System.Web.UI.Page
    {
        RadAjaxManager MasterAjaxManager;
        decimal Balance = 0;
        decimal YTDPay = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["SchoolId"] == null || Session["CurrentSchoolYearId"] == null || string.IsNullOrEmpty(Request.QueryString["ChildFamilyId"]))
            {
                Response.Redirect("~/Login.aspx");
            }

            try
            {
                if (!IsPostBack)
                {
                    //
                    if (!string.IsNullOrEmpty(Request.QueryString["ChildFamilyId"]))
                    {
                        ViewState["ChildFamilyId"] = Request.QueryString["ChildFamilyId"].ToString();
                        SetMenuLink();
                        //GetChildProgEnrollmentFeeDetail(new Guid(Session["CurrentSchoolYearId"].ToString()));
                    }
                    else
                    {
                        Response.Redirect("Ledger.aspx");
                    }
                }
                this.Form.DefaultButton = btnTopEditAll.UniqueID;
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.Ledger, "Page_Load", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
            }
        }

        protected void rgLedger_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DayCareBAL.LedgerService proxyLedger = new DayCareBAL.LedgerService();
            List<DayCarePL.LedgerProperties> lstLeger = proxyLedger.LoadLedgerDetail(new Guid(Session["CurrentSchoolYearId"].ToString()), new Guid(ViewState["ChildFamilyId"].ToString()));
            if (lstLeger != null)
            {
                rgLedger.DataSource = lstLeger;
                txtBalance.Text = (lstLeger.Sum(i => i.Debit) - lstLeger.Sum(i => i.Credit)).ToString();
                txtYTDPay.Text = lstLeger.Sum(i => i.Credit).ToString();

            }
        }

        protected void rgLedger_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item.ItemType == GridItemType.Header)
            {
                Balance = 0;
            }
            if (e.Item.ItemType == GridItemType.Item || e.Item.ItemType == GridItemType.AlternatingItem)
            {
                DayCarePL.LedgerProperties objLedger = e.Item.DataItem as DayCarePL.LedgerProperties;
                Label lblBalance = e.Item.FindControl("lblBalance") as Label;
                TextBox txtDebit = e.Item.FindControl("txtDebit") as TextBox;
                TextBox txtCredit = e.Item.FindControl("txtCredit") as TextBox;
                Label lblOperation = e.Item.FindControl("lblOperation") as Label;
                Label lblPaymentLabel = e.Item.FindControl("lblPaymentLabel") as Label;
                Label lblChargesLable = e.Item.FindControl("lblChargesLable") as Label;
                DropDownList ddlPayment = e.Item.FindControl("ddlPayment") as DropDownList;
                DropDownList ddlCategory = e.Item.FindControl("ddlCategory") as DropDownList;
                //For Edit
                RadDatePicker rdpDate = e.Item.FindControl("rdpDate") as RadDatePicker;
                Common.BindChargeCode(ddlCategory);
                RadioButton rdbPayment = e.Item.FindControl("rdbPayment") as RadioButton;
                RadioButton rdbDiscount = e.Item.FindControl("rdbDiscount") as RadioButton;
                //Label lblTransactionDate1=e.Item.FindControl("lblTransactionDate1") as Label;
                //GridEditableColumn edSelect1 = e.Item.OwnerTableView.Columns[12] as GridEditableColumn;
                //end
                GridEditableColumn edSelect = e.Item.OwnerTableView.Columns[1] as GridEditableColumn;
                edSelect.Display = true;
                if (objLedger != null)
                {
                    lblPaymentLabel.Visible = true;
                    lblChargesLable.Visible = true;
                    ViewState["ChildFamilyId"] = objLedger.ChildFamilyId;

                    if (objLedger.TransactionDate != null)
                    {
                        rdpDate.SelectedDate = objLedger.TransactionDate;
                    }
                    if (objLedger.AllowEdit)
                    {
                        if (objLedger.PaymentId != null)
                        {
                            //edSelect = e.Item.OwnerTableView.Columns[1] as GridEditableColumn;
                            lblPaymentLabel.Visible = false;
                            lblChargesLable.Visible = false;
                            //edSelect.Display = false;
                            rdbPayment.Style.Add("display", "none");
                            rdbDiscount.Style.Add("display", "none");
                            ddlPayment.SelectedValue = objLedger.PaymentMethodId.ToString();
                        }
                        else if (objLedger.PaymentMethodId != null)
                        {
                            rdbPayment.Checked = true;
                            rdbDiscount.Checked = false;
                            ddlPayment.SelectedValue = objLedger.PaymentMethodId.ToString();
                        }
                        else if (objLedger.ChargeCodeCategoryId != null)
                        {
                            rdbPayment.Checked = false;
                            rdbDiscount.Checked = true;
                            ddlCategory.SelectedValue = objLedger.ChargeCodeCategoryId.ToString();
                        }
                        else
                        {
                            rdbPayment.Checked = true;
                            rdbDiscount.Checked = false;
                        }

                        if (rdbPayment.Checked)
                        {

                            //edSelect.Display = true;
                            ddlPayment.Style.Add("display", "block");
                            ddlCategory.Style.Add("display", "none");
                            txtDebit.Enabled = false;

                            //itm["Select"].Visible = true;
                            //itm["Select"].Controls[1].Visible = true;
                            // itm["operation"].Visible = true;
                        }
                        else
                        {

                            //edSelect.Display = true;
                            ddlCategory.Style.Add("display", "block");
                            ddlPayment.Style.Add("display", "none");
                        }
                    }
                    else
                    {
                        //edSelect = e.Item.OwnerTableView.Columns[1] as GridEditableColumn;
                        lblPaymentLabel.Visible = false;
                        lblChargesLable.Visible = false;
                        //edSelect.Display = false;
                        rdpDate.Enabled = false;
                        if (objLedger.TransactionDate != null)
                        {
                            rdpDate.SelectedDate = objLedger.TransactionDate;
                        }
                        ddlCategory.Visible = false;
                        ddlPayment.Visible = false;
                        rdbPayment.Visible = false;
                        rdbDiscount.Visible = false;
                        //itm["Select"].Visible = false;
                        // itm["operation"].Visible = false;
                    }

                    //if (objLedger.PaymentMethodId != null)
                    //{
                    //    switch (objLedger.PaymentMethodId)
                    //    {
                    //        case 0:
                    //            {

                    //                ddlPayment.SelectedValue = "Cash";
                    //                break;
                    //            }
                    //        case 1:
                    //            {
                    //                ddlPayment.SelectedValue = "Check";
                    //                break;
                    //            }
                    //        case 2:
                    //            {
                    //                ddlPayment.SelectedValue = "Credit";
                    //                break;
                    //            }
                    //    }
                    //}
                    //else if (objLedger.ChargeCodeCategoryId != null)
                    //{
                    //    ddlCategory.SelectedValue = objLedger.ChargeCodeCategoryId.ToString();
                    //}
                    if (objLedger.Debit > 0)
                    {

                        Balance += objLedger.Debit;

                    }
                    else
                    {

                        Balance -= objLedger.Credit;
                        YTDPay += objLedger.Credit;

                    }
                    lblBalance.Text = Balance.ToString();
                    //txtBalance.Text = Balance.ToString();
                    //txtYTDPay.Text = YTDPay.ToString();
                }
            }
            if (e.Item.ItemType == GridItemType.EditItem)
            {
                GridEditableItem itm = e.Item as GridEditableItem;
                DayCarePL.LedgerProperties objLedger = e.Item.DataItem as DayCarePL.LedgerProperties;
                RadDatePicker rdpDate = e.Item.FindControl("rdpDate") as RadDatePicker;
                DropDownList ddlPayment = e.Item.FindControl("ddlPayment") as DropDownList;
                DropDownList ddlCategory = e.Item.FindControl("ddlCategory") as DropDownList;
                Common.BindChargeCode(ddlCategory);
                RadioButton rdbPayment = e.Item.FindControl("rdbPayment") as RadioButton;
                RadioButton rdbDiscount = e.Item.FindControl("rdbDiscount") as RadioButton;
                TextBox txtDebit = e.Item.FindControl("txtDebit") as TextBox;
                //Label lblTransactionDate1=e.Item.FindControl("lblTransactionDate1") as Label;
                GridEditableColumn edSelect1 = e.Item.OwnerTableView.Columns[12] as GridEditableColumn;
                //edSelect1.Display = false;
                if (objLedger != null)
                {
                    if (objLedger.TransactionDate != null)
                    {
                        rdpDate.SelectedDate = objLedger.TransactionDate;
                    }
                    if (objLedger.AllowEdit)
                    {
                        if (objLedger.PaymentId != null)
                        {
                            GridEditableColumn edSelect = e.Item.OwnerTableView.Columns[3] as GridEditableColumn;
                            edSelect.Display = false;
                            rdbPayment.Style.Add("display", "none");
                            rdbDiscount.Style.Add("display", "none");
                            ddlPayment.SelectedValue = objLedger.PaymentMethodId.ToString();
                        }
                        else if (objLedger.PaymentMethodId != null)
                        {
                            rdbPayment.Checked = true;
                            rdbDiscount.Checked = false;
                            ddlPayment.SelectedValue = objLedger.PaymentMethodId.ToString();
                        }
                        else if (objLedger.ChargeCodeCategoryId != null)
                        {
                            rdbPayment.Checked = false;
                            rdbDiscount.Checked = true;
                            ddlCategory.SelectedValue = objLedger.ChargeCodeCategoryId.ToString();
                        }
                        else
                        {
                            rdbPayment.Checked = true;
                            rdbDiscount.Checked = false;
                        }

                        if (rdbPayment.Checked)
                        {
                            ddlPayment.Style.Add("display", "block");
                            ddlCategory.Style.Add("display", "none");
                            txtDebit.Enabled = false;

                            //itm["Select"].Visible = true;
                            //itm["Select"].Controls[1].Visible = true;
                            // itm["operation"].Visible = true;
                        }
                        else
                        {
                            ddlCategory.Style.Add("display", "block");
                            ddlPayment.Style.Add("display", "none");
                        }
                    }
                    else
                    {
                        GridEditableColumn edSelect = e.Item.OwnerTableView.Columns[3] as GridEditableColumn;
                        edSelect.Display = false;
                        rdpDate.Enabled = false;
                        if (objLedger.TransactionDate != null)
                        {
                            rdpDate.SelectedDate = objLedger.TransactionDate;
                        }
                        ddlCategory.Visible = false;
                        ddlPayment.Visible = false;
                        rdbPayment.Visible = false;
                        rdbDiscount.Visible = false;
                        //itm["Select"].Visible = false;
                        // itm["operation"].Visible = false;
                    }
                }
                else
                {
                    if (rdbPayment.Checked)
                    {
                        ddlCategory.Style.Add("display", "none");
                        txtDebit.Enabled = false;
                        rdpDate.SelectedDate = DateTime.Now;
                        rdbPayment.TextAlign = TextAlign.Right;
                    }
                }
            }
        }

        public void SetMenuLink()
        {
            try
            {
                List<DayCarePL.MenuLink> lstMenu = new List<DayCarePL.MenuLink>();
                DayCarePL.MenuLink objMenu;
                objMenu = new DayCarePL.MenuLink();
                objMenu.Name = "Family: " + Common.GetFamilyName(new Guid(ViewState["ChildFamilyId"].ToString()));
                objMenu.Name += ": " + Common.GetFamilyChildName(new Guid(ViewState["ChildFamilyId"].ToString()), new Guid(Session["CurrentSchoolYearId"].ToString()));
                objMenu.Url = "~/UI/LedgerofFamily.aspx";
                lstMenu.Add(objMenu);
                objMenu = new DayCarePL.MenuLink();
                objMenu.Name = "Ledger";
                objMenu.Url = "";
                lstMenu.Add(objMenu);
                usrMenuLink.SetMenuLink(lstMenu);
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.AddEditChild, "SetMenuLink:ChildFamilyId=" + ViewState["ChildFamilyId"], ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
            }
        }

        protected void btnEditAll_Click(object sender, EventArgs e)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.Ledger, "btnDeleteAll_Click", "btnDeleteAll_Click called", DayCarePL.Common.GUID_DEFAULT);
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.Ledger, "btnDeleteAll_Click", "Debug btnDeleteAll_Click called", DayCarePL.Common.GUID_DEFAULT);
                DayCareBAL.LedgerService proxySave = new DayCareBAL.LedgerService();
                DayCarePL.LedgerProperties objLedger = new DayCarePL.LedgerProperties();
                List<DayCarePL.LedgerProperties> lstLeger = new List<DayCarePL.LedgerProperties>();
                foreach (GridDataItem e1 in rgLedger.MasterTableView.Items)
                {

                    CheckBox chkDelete = e1.FindControl("chkDelete") as CheckBox;
                    if (chkDelete != null)
                    {
                        if (chkDelete.Checked)
                        {
                            objLedger = new DayCarePL.LedgerProperties();
                            objLedger.Id = new Guid(e1.GetDataKeyValue("Id").ToString());
                            objLedger.TransactionDate = Convert.ToDateTime((e1.FindControl("rdpDate") as RadDatePicker).SelectedDate);
                            objLedger.AllowEdit = (e1.FindControl("rdpDate") as RadDatePicker).Enabled;
                            objLedger.FamilyName = (e1.FindControl("txtFamilyName") as TextBox).Text;
                            if (!string.IsNullOrEmpty((e1.FindControl("txtDebit") as TextBox).Text))
                            {
                                objLedger.Debit = Convert.ToDecimal((e1.FindControl("txtDebit") as TextBox).Text);
                            }
                            else
                            {
                                objLedger.Debit = 0;
                            }
                            if (!string.IsNullOrEmpty((e1.FindControl("txtCredit") as TextBox).Text))
                            {
                                objLedger.Credit = Convert.ToDecimal((e1.FindControl("txtCredit") as TextBox).Text);
                            }
                            else
                            {
                                objLedger.Credit = 0;
                            }
                            objLedger.Comment = (e1.FindControl("txtComment") as TextBox).Text;
                            if ((e1.FindControl("rdbPayment") as RadioButton).Visible)
                            {
                                if ((e1.FindControl("rdbPayment") as RadioButton).Checked)
                                {
                                    objLedger.PaymentMethodId = Convert.ToInt16((e1.FindControl("ddlPayment") as DropDownList).SelectedValue);
                                }
                                else
                                {
                                    objLedger.ChargeCodeCategoryId = new Guid((e1.FindControl("ddlCategory") as DropDownList).SelectedValue);
                                }
                            }
                            if (ViewState["ChildFamilyId"] != null)
                            {
                                objLedger.ChildFamilyId = new Guid(ViewState["ChildFamilyId"].ToString());
                            }
                            if (Session["CurrentSchoolYearId"] != null)
                            {
                                objLedger.SchoolYearId = new Guid(Session["CurrentSchoolYearId"].ToString());
                            }
                            if (Session["StaffId"] != null)
                            {
                                objLedger.LastModifiedById = new Guid(Session["StaffId"].ToString());
                            }

                            lstLeger.Add(objLedger);
                        }
                    }
                }
                if (lstLeger != null && lstLeger.Count > 0)
                {
                    DayCareBAL.LedgerService proxyLedger = new DayCareBAL.LedgerService();
                    if (proxyLedger.EditAllLedger(lstLeger))
                    {
                        MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                        MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Saved Successfully", "false"));
                    }

                    rgLedger.MasterTableView.Rebind();
                    rgLedger.MasterTableView.CurrentPageIndex = 0;
                }
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.Ledger, "btnDeleteAll_Click", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
            }

        }

        protected void btnTopBack_Click(object sender, EventArgs e)
        {
            if (ViewState["ChildFamilyId"] != null)
            {
                Response.Redirect("Ledger.aspx?ChildFamilyId=" + ViewState["ChildFamilyId"]);
            }
            else
            {
                Response.Redirect("LedgerOfFamily.aspx");
            }
        }

    }
}
