using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace DayCare.UI
{
    public partial class FamilyPayment : System.Web.UI.Page
    {
        RadAjaxManager MasterAjaxManager;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["SchoolId"] == null || Session["CurrentSchoolYearId"] == null)
            {
                Response.Redirect("~/Login.aspx");
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
                btnSave.Enabled = false;
                btnCancel.Enabled = false;
            }
            this.Form.DefaultButton = btnSave.UniqueID;

            btnSave.Attributes.Add("onclick", "return Check();");
        }

        public void rgFamilyPayment_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (!e.IsFromDetailTable)
                {
                    Guid SchoolId = new Guid();
                    if (Session["SchoolId"] != null)
                    {
                        SchoolId = new Guid(Session["SchoolId"].ToString());
                    }
                    Guid CurrentSchoolYearId = new Guid();
                    if (Session["CurrentSchoolYearId"] != null)
                    {
                        CurrentSchoolYearId = new Guid(Session["CurrentSchoolYearId"].ToString());
                    }
                    DayCareBAL.ChildFamilyService proxyChildFamily = new DayCareBAL.ChildFamilyService();
                    List<DayCarePL.ChildFamilyProperties> lstChildFamily = proxyChildFamily.LoadChildFamily(SchoolId, CurrentSchoolYearId);

                    if (lstChildFamily != null)
                    {

                        rgFamilyPayment.DataSource = lstChildFamily.FindAll(i => i.Active.Equals(true));// proxyChildFamily.LoadChildFamily(new Guid(Session["SchoolId"].ToString()));
                    }
                    else
                    {
                        rgFamilyPayment.DataSource = new List<DayCarePL.ChildFamilyProperties>();
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void rgFamilyPayment_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            //ClearFields();
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
                    btnSave.Enabled = false;
                    btnCancel.Enabled = false;
                }
            }
            else
            {
                if (e.CommandName == "Edit")
                {

                    //GridEditableItem dataItem = (GridEditableItem)e.Item;
                    //Guid SchoolProgramId = new Guid(dataItem["SchoolProgramId"].Text);
                    //BindEditProgramEnrollment(new Guid(Request.QueryString["ChildDataId"].ToString()), SchoolProgramId);
                    //ViewState["SchoolProgramId"] = SchoolProgramId;
                    //e.Canceled = true;

                }
                if (e.CommandName == "InitInsert")
                {
                    ViewState["IsPreRenderCall"] = false;
                }
                if (e.CommandName == "Cancel")
                {
                    ViewState["IsPreRenderCall"] = null;
                }
            }
        }

        protected void rgFamilyPayment_EditCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            GridDataItem dataItem = (GridDataItem)e.Item.DataItem;
            //Guid SchoolProgramId = new Guid(dataItem["SchoolProgramId"].Text);
            //e.Canceled = true;
        }

        protected void rgFamilyPayment_DetailTableDataBind(object sender, Telerik.Web.UI.GridDetailTableDataBindEventArgs e)
        {
            try
            {
                GridDataItem dataItem = (GridDataItem)e.DetailTableView.ParentItem;
                switch (e.DetailTableView.DataMember)
                {
                    case "Payment":
                        {
                            DayCareBAL.FamilyPaymentService proxyFamilyPayment = new DayCareBAL.FamilyPaymentService();
                            List<DayCarePL.FamilyPaymentProperties> lstFamilyPayment = proxyFamilyPayment.GetFamilyWisePayment(new Guid(dataItem.GetDataKeyValue("ChildFamilyId").ToString()));
                            if (lstFamilyPayment != null)
                            {
                                if (lstFamilyPayment != null)
                                {
                                    if (lstFamilyPayment.Count > 0)
                                    {
                                        e.DetailTableView.DataSource = lstFamilyPayment.OrderByDescending(i => i.PostDate);
                                    }
                                    else
                                    {
                                        e.DetailTableView.DataSource = new List<DayCarePL.FamilyPaymentProperties>();
                                    }
                                }

                            }
                            break;
                        }
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void rgFamilyPayment_PreRender(object sender, EventArgs e)
        {
            //if (ViewState["IsPreRenderCall"] == null)
            //{
            //    if (rgFamilyPayment.MasterTableView.Items.Count > 0)
            //    {
            //        for (int cnt = 0; cnt < rgFamilyPayment.MasterTableView.Items.Count; cnt++)
            //        {
            //            rgFamilyPayment.MasterTableView.Items[cnt].Expanded = true;
            //            if (rgFamilyPayment.MasterTableView.Items[cnt].ChildItem.NestedTableViews[0].Items.Count > 0)
            //            {
            //                rgFamilyPayment.MasterTableView.Items[cnt].ChildItem.NestedTableViews[0].Items[0].Expanded = true;
            //            }
            //            else
            //            {
            //                //rgFamilyPayment.MasterTableView.Items[0].ChildItem.NestedTableViews[0].Items[0].Expanded = false;
            //                rgFamilyPayment.MasterTableView.Items[cnt].Expanded = false;
            //            }
            //        }
            //    }
            //}
        }

        protected void rgFamilyPayment_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item.ItemType == GridItemType.AlternatingItem || e.Item.ItemType == GridItemType.Item)
            {
                DayCarePL.FamilyPaymentProperties objFamilyPayment = e.Item.DataItem as DayCarePL.FamilyPaymentProperties;
                if (objFamilyPayment != null)
                {
                    Label lblPaymentMethod = e.Item.FindControl("lblPaymentMethod") as Label;
                    if (!string.IsNullOrEmpty(objFamilyPayment.PaymentMethod))
                    {
                        string paymentmethod = "";
                        switch (objFamilyPayment.PaymentMethod)
                        {
                            case "0":
                                paymentmethod = "Cash";
                                break;
                            case "1":
                                paymentmethod = "Check";
                                break;
                            case "2":
                                paymentmethod = "Credit";
                                break;
                        }
                        lblPaymentMethod.Text = paymentmethod;
                    }
                }

            }
            if (e.Item.ItemType == GridItemType.EditItem)
            {
                GridEditableItem itm = e.Item as GridEditableItem;
                DayCarePL.FamilyPaymentProperties objFamilyPayment = e.Item.DataItem as DayCarePL.FamilyPaymentProperties;
                RadDatePicker rdpPostDate = e.Item.FindControl("rdpPostDate") as RadDatePicker;
                DropDownList ddlPaymentMethod = e.Item.FindControl("ddlPaymentMethod") as DropDownList;
                if (objFamilyPayment != null)
                {
                    if (objFamilyPayment.PostDate != null)
                    {
                        rdpPostDate.SelectedDate = objFamilyPayment.PostDate;
                    }
                    ddlPaymentMethod.SelectedValue = objFamilyPayment.PaymentMethod;
                }
            }
        }

        protected void rgFamilyPayment_ItemCreated(object sender, Telerik.Web.UI.GridItemEventArgs e)
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
                    //validationsum.DisplayMode = ValidationSummaryDisplayMode.SingleParagraph;

                    RadDatePicker rdpPostDate = item["PostDate"].FindControl("rdpPostDate") as RadDatePicker;

                    if (rdpPostDate != null)
                    {
                        cell = (TableCell)rdpPostDate.Parent;
                        validator = new RequiredFieldValidator();
                        if (cell != null)
                        {
                            rdpPostDate.ID = "rdpPostDate";
                            validator.ControlToValidate = rdpPostDate.ID;
                            validator.ErrorMessage = "Please select Date.";
                            validator.SetFocusOnError = true;
                            validator.Display = ValidatorDisplay.None;
                            cell.Controls.Add(validator);
                            cell.Controls.Add(validationsum);
                        }
                    }

                    DropDownList ddlPaymentMethod = item["PaymentMethod"].FindControl("ddlPaymentMethod") as DropDownList;
                    if (ddlPaymentMethod != null)
                    {
                        cell = (TableCell)ddlPaymentMethod.Parent;
                        validator = new RequiredFieldValidator();
                        if (cell != null)
                        {
                            ddlPaymentMethod.ID = "ddlPaymentMethod";
                            validator.ControlToValidate = ddlPaymentMethod.ID;
                            validator.ErrorMessage = "Please select Payment method.";
                            validator.InitialValue = "-1";

                            validator.SetFocusOnError = true;
                            validator.Display = ValidatorDisplay.None;
                            cell.Controls.Add(validator);
                            cell.Controls.Add(validationsum);
                        }
                    }

                    TextBox txtAmount = item["Amount"].FindControl("txtAmount") as TextBox;
                    if (txtAmount != null)
                    {
                        cell = (TableCell)txtAmount.Parent;
                        validator = new RequiredFieldValidator();
                        if (cell != null)
                        {
                            txtAmount.ID = "txtAmount";
                            validator.ControlToValidate = txtAmount.ID;
                            validator.ErrorMessage = "Please enter Amount.";
                            validator.SetFocusOnError = true;
                            validator.Display = ValidatorDisplay.None;
                            cell.Controls.AddAt(0, validator);

                            RegularExpressionValidator val = new RegularExpressionValidator();
                            txtAmount.ID = "txtAmount";
                            val.ControlToValidate = txtAmount.ID;
                            val.ErrorMessage = "Please enter valid Amount.";
                            val.ValidationExpression = @"(^-?\d\d*\.\d*$)|(^-?\d\d*$)|(^-?\.\d\d*$)";
                            val.SetFocusOnError = true;
                            val.Display = ValidatorDisplay.None;
                            cell.Controls.AddAt(1, val);
                            cell.Controls.Add(validationsum);

                        }
                    }
                }
            }
        }

        public bool SubmitRecord(object sender, GridCommandEventArgs e)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.SchoolYear, "SubmitRecord", "Submit record method called", DayCarePL.Common.GUID_DEFAULT);
            bool result = false;
            string Amount = "";
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.SchoolYear, "SubmitRecord", "Debug Submit Record Of SchoolYear", DayCarePL.Common.GUID_DEFAULT);
                DayCareBAL.FamilyPaymentService proxyFamilyPayment = new DayCareBAL.FamilyPaymentService();
                DayCarePL.FamilyPaymentProperties objFamilyPayment = new DayCarePL.FamilyPaymentProperties();

                foreach (GridDataItem e1 in rgFamilyPayment.Items)
                {
                    if ((e.Item.FindControl("rdpPostDate") as RadDatePicker).SelectedDate != null)
                    {
                        objFamilyPayment.PostDate = (e1.FindControl("rdpPostDate") as RadDatePicker).SelectedDate.Value;
                    }
                    else
                    {
                        objFamilyPayment.PostDate = null;
                    }

                    objFamilyPayment.PaymentMethod = (e1.FindControl("ddlPaymentMethod") as DropDownList).SelectedValue;

                    objFamilyPayment.PaymentDetail = (e1.FindControl("txtPaymentDetail") as TextBox).Text;


                    if (!string.IsNullOrEmpty((e.Item.FindControl("txtAmount") as TextBox).Text))
                    {
                        Amount = (e1.FindControl("txtAmount") as TextBox).Text;
                    }

                    objFamilyPayment.ChildFamilyId = new Guid((e1.FindControl("txtChildFamilyId") as Label).Text);


                    if (!string.IsNullOrEmpty(Amount))
                    {
                        decimal amountresult = 0;
                        decimal.TryParse(Amount, out amountresult);
                        if (amountresult == 0)
                        {
                            objFamilyPayment.Amount = amountresult;
                            MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                            MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Pease enter valid Amount.", "false"));
                            return false;
                        }
                        else
                        {
                            objFamilyPayment.Amount = amountresult;
                        }

                    }
                    if (Session["CurrentSchoolYearId"] != null)
                    {
                        objFamilyPayment.SchoolYearId = new Guid(Session["CurrentSchoolYearId"].ToString());
                    }
                    //if (e.CommandName != "PerformInsert")
                    //{
                    if (Session["StaffId"] != null)
                    {
                        objFamilyPayment.CreatedById = new Guid(Session["StaffId"].ToString());
                        objFamilyPayment.LastModifiedById = new Guid(Session["StaffId"].ToString());
                    }

                    hdnName.Value = "";
                }
                //if (proxyFamilyPayment.Save(objFamilyPayment))
                //{
                //    MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                //    MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Saved Successfully", "false"));
                //    result = true;
                //    ViewState["IsPreRenderCall"] = null;
                //}
                //else
                //{
                //    result = false;
                //}



            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.SchoolYear, "SubmitRecord", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                result = false;
            }
            return result;
        }

        protected void rgFamilyPayment_UpdateCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            bool isValid = SubmitRecord(sender, e);
            if (isValid == false)
            {
                e.Canceled = true;
            }
        }

        protected void rgFamilyPayment_InsertCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            bool isValid = SubmitRecord(sender, e);
            if (isValid == false)
            {
                e.Canceled = true;
            }
            rgFamilyPayment.MasterTableView.CurrentPageIndex = 0;
        }

        protected void rgFamilyPayment_DeleteCommand(object source, GridCommandEventArgs e)
        {
            try
            {

                Guid PaymentId = new Guid(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Id"].ToString());
                DayCareBAL.FamilyPaymentService proxyFamilyPayment = new DayCareBAL.FamilyPaymentService();
                if (proxyFamilyPayment.Delete(PaymentId))
                {
                    rgFamilyPayment.MasterTableView.DetailTables[0].Rebind();
                    MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                    MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Delete Successfully", "false"));
                    return;
                }
                else
                {
                    MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                    MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Internal Error,Please try again", "false"));
                    return;
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.SchoolYear, "SubmitRecord", "Submit record method called", DayCarePL.Common.GUID_DEFAULT);
            bool result = false;
            string Amount = "";
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.SchoolYear, "SubmitRecord", "Debug Submit Record Of SchoolYear", DayCarePL.Common.GUID_DEFAULT);
                DayCareBAL.FamilyPaymentService proxyFamilyPayment = new DayCareBAL.FamilyPaymentService();
                DayCarePL.FamilyPaymentProperties objFamilyPayment = new DayCarePL.FamilyPaymentProperties();
                List<DayCarePL.FamilyPaymentProperties> lstFamilyPayment = new List<DayCarePL.FamilyPaymentProperties>();

                foreach (GridDataItem e1 in rgFamilyPayment.Items)
                {
                    TextBox txtAmount = e1.FindControl("txtAmount") as TextBox;
                    RadDatePicker rdpPostDate = e1.FindControl("rdpPostDate") as RadDatePicker;
                    DropDownList ddlPaymentMethod = e1.FindControl("ddlPaymentMethod") as DropDownList;
                    TextBox txtPaymentDetail = e1.FindControl("txtPaymentDetail") as TextBox;
                    if (txtAmount != null && rdpPostDate != null && ddlPaymentMethod != null && txtPaymentDetail != null && rdpPostDate.SelectedDate != null && !string.IsNullOrEmpty(txtAmount.Text) && ddlPaymentMethod.SelectedIndex > 0)
                    {
                        objFamilyPayment = new DayCarePL.FamilyPaymentProperties();
                        if ((e1.FindControl("rdpPostDate") as RadDatePicker).SelectedDate != null)
                        {
                            objFamilyPayment.PostDate = (e1.FindControl("rdpPostDate") as RadDatePicker).SelectedDate.Value;
                        }
                        else
                        {
                            objFamilyPayment.PostDate = null;
                        }

                        objFamilyPayment.PaymentMethod = (e1.FindControl("ddlPaymentMethod") as DropDownList).SelectedValue;

                        objFamilyPayment.PaymentDetail = (e1.FindControl("txtPaymentDetail") as TextBox).Text;


                        if (!string.IsNullOrEmpty((e1.FindControl("txtAmount") as TextBox).Text))
                        {
                            Amount = (e1.FindControl("txtAmount") as TextBox).Text;
                        }

                        objFamilyPayment.ChildFamilyId = new Guid((e1.FindControl("lblChildFamilyId") as Label).Text);

                        decimal amountresult = 0;
                        if (!string.IsNullOrEmpty(Amount))
                        {

                            decimal.TryParse(Amount, out amountresult);
                            if (amountresult == 0)
                            {
                                //objFamilyPayment.Amount = amountresult;
                                //MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                                //MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Pease enter valid Amount.", "false"));
                                //return;
                            }
                            else
                            {
                                objFamilyPayment.Amount = amountresult;
                            }

                        }
                        if (Session["CurrentSchoolYearId"] != null)
                        {
                            objFamilyPayment.SchoolYearId = new Guid(Session["CurrentSchoolYearId"].ToString());
                        }
                        //if (e.CommandName != "PerformInsert")
                        //{
                        if (Session["StaffId"] != null)
                        {
                            objFamilyPayment.CreatedById = new Guid(Session["StaffId"].ToString());
                            objFamilyPayment.LastModifiedById = new Guid(Session["StaffId"].ToString());
                        }

                        hdnName.Value = "";
                        if (amountresult != 0)
                        {
                            lstFamilyPayment.Add(objFamilyPayment);
                        }

                    }
                }
                if (lstFamilyPayment.Count > 0)
                {
                    if (proxyFamilyPayment.Save(lstFamilyPayment))
                    {
                        btnSave.Enabled = true;
                        btnSave.Attributes.Add("display", "block");
                        MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                        MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Saved Successfully\\nEntries are posted to Ledger", "false"));
                        ViewState["IsPreRenderCall"] = null;
                        rgFamilyPayment.MasterTableView.Rebind();
                    }
                }
                else
                {
                    btnSave.Enabled = true;
                    btnSave.Attributes.Add("display", "block");
                    MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                    MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Please enter valid details at least a family", "false"));

                }


            }
            catch (Exception ex)
            {
                btnSave.Enabled = true;
                btnSave.Attributes.Remove("disabled");
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.SchoolYear, "SubmitRecord", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                result = false;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            rgFamilyPayment.MasterTableView.Rebind();
            //Response.Redirect("FamilyPayment.aspx");
        }
    }
}
