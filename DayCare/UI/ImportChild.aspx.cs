using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace DayCare.UI
{
    public partial class ImportChild : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["SchoolId"] == null || Session["CurrentSchoolYearId"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }

            if (!IsPostBack)
            {
                GetPreSchoolYearOfSelectedSchoolYear(new Guid(Session["SchoolId"].ToString()), new Guid(Session["CurrentSchoolYearId"].ToString()));
                //BindChildList();
            }
            SetMenuLink();
        }

        public void GetPreSchoolYearOfSelectedSchoolYear(Guid Schoold, Guid CurrentSchoolYearId)
        {
            try
            {
                DayCareBAL.SchoolYearService proxySchoolYear = new DayCareBAL.SchoolYearService();
                List<DayCarePL.SchoolYearListProperties> lstSchoolYear = proxySchoolYear.GetPreviousYearOfSelectedCurrentYear(Schoold, CurrentSchoolYearId);
                if (lstSchoolYear != null)
                {
                    ddlPrevSchoolyearOfSelectedCurrentSchoolYear.DataSource = lstSchoolYear;
                    ddlPrevSchoolyearOfSelectedCurrentSchoolYear.DataTextField = "Year";
                    ddlPrevSchoolyearOfSelectedCurrentSchoolYear.DataValueField = "SchoolYearId";
                    ddlPrevSchoolyearOfSelectedCurrentSchoolYear.DataBind();
                    ddlPrevSchoolyearOfSelectedCurrentSchoolYear.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {

            }
        }

        public void BindChildList()
        {
            DayCareBAL.ChildSchoolYearService proxyChildSchoolYear = new DayCareBAL.ChildSchoolYearService();
            divErrorMsg.Style.Add("display", "none");
            btnImportChild.Visible = true;
            btnImportChildBottom.Visible = true;
            DayCareBAL.ChildSchoolYearService proxyChild = new DayCareBAL.ChildSchoolYearService();
            if (ddlPrevSchoolyearOfSelectedCurrentSchoolYear.SelectedIndex == -1)
            {
                divErrorMsg.Style.Add("display", "block");
                lblSchoolYear.Visible = false;
                ddlPrevSchoolyearOfSelectedCurrentSchoolYear.Visible = false;
                btnImportChild.Visible = false;
                lblErrorMsg.Text = "No previous year found";
                btnImportChildBottom.Visible = false;
            }
            else
            {
                divErrorMsg.Style.Add("display", "none");
                lblSchoolYear.Visible = true;
                ddlPrevSchoolyearOfSelectedCurrentSchoolYear.Visible = true;
                btnImportChild.Visible = true;
                btnImportChildBottom.Visible = true;
                List<DayCarePL.ChildDataProperties> lstChildData = proxyChild.GetAllChildListForImport(new Guid(Session["CurrentSchoolYearId"].ToString()), new Guid(ddlPrevSchoolyearOfSelectedCurrentSchoolYear.SelectedValue), new Guid(Session["SchoolId"].ToString()));
                if (lstChildData != null)
                {
                    rgChildList.DataSource = lstChildData.OrderBy(i => i.FamilyName);
                    //rgChildList.DataBind();
                }
            }
        }

        protected void rgChildList_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                //DayCareBAL.ChildSchoolYearService proxyChildSchoolYear = new DayCareBAL.ChildSchoolYearService();
                #region
                //DayCarePL.SchoolYearProperties ObjSchoolYear = proxyChildSchoolYear.GetPreviousYearOfCurrentYear(new Guid(Session["SchoolId"].ToString()), new Guid(Session["CurrentSchoolYearId"].ToString()));
                //if (ObjSchoolYear == null)
                //{
                //    divErrorMsg.Style.Add("display", "block");
                //    btnImportChild.Visible = false;
                //    lblErrorMsg.Text = "No previous year found";
                //    btnImportChildBottom.Visible = false;
                //}
                //else if (ObjSchoolYear.OldCurrentSchoolYearId == null || ObjSchoolYear.OldCurrentSchoolYearId.Equals(new Guid()))
                //{
                //    divErrorMsg.Style.Add("display", "block");
                //    btnImportChild.Visible = false;
                //    lblErrorMsg.Text = "No previous year found";
                //    btnImportChildBottom.Visible = false;
                //}
                //else if (Common.IsCurrentYear(new Guid(Session["CurrentSchoolYearId"].ToString()), new Guid(Session["SchoolId"].ToString())))
                //{
                //    divErrorMsg.Style.Add("display", "none");
                //    btnImportChild.Visible = true;
                //    btnImportChildBottom.Visible = true;
                //    DayCareBAL.ChildSchoolYearService proxyChild = new DayCareBAL.ChildSchoolYearService();
                //    List<DayCarePL.ChildDataProperties> lstChildData = proxyChild.GetAllActiveChildListForImport(new Guid(Session["CurrentSchoolYearId"].ToString()), new Guid(Session["SchoolId"].ToString()));
                //    if (lstChildData != null)
                //    {
                //        rgChildList.DataSource = lstChildData.OrderBy(i => i.FamilyName);
                //        rgChildList.DataBind();
                //    }

                //}
                //else
                //{
                //    DropDownList ddlSchoolYear = this.Master.FindControl("ddlSchoolYear") as DropDownList;
                //    DayCarePL.SchoolYearProperties ObjSchoolYearMsg = proxyChildSchoolYear.GetPreviousYearOfCurrentYearForMessage(new Guid(Session["SchoolId"].ToString()), new Guid(ddlSchoolYear.SelectedValue.ToString()));
                //    divErrorMsg.Style.Add("display", "block");
                //    if (ObjSchoolYearMsg.OldCurrentSchoolYear != null)
                //    {
                //        if (ObjSchoolYearMsg.OldCurrentSchoolYear.Length > 0)
                //            //lblErrorMsg.Text = "Sorry, You can not import from " + ObjSchoolYearMsg.OldCurrentSchoolYear + ". School's current year is " + ObjSchoolYear.Year + ", so you can only import from the previous year: " + ObjSchoolYear.OldCurrentSchoolYear;
                //            lblErrorMsg.Text = "Sorry, School's current year is " + ObjSchoolYear.Year + ", so you can only import from the previous year: " + ObjSchoolYear.OldCurrentSchoolYear + ". So instead of " + ddlSchoolYear.SelectedItem.Text + ", select " + ObjSchoolYear.Year + " in school year dropdown at top right of the screen.";
                //        else
                //            lblErrorMsg.Text = "No previous year found";
                //    }
                //    else
                //        lblErrorMsg.Text = "No previous year found";
                //    btnImportChild.Visible = false;
                //    btnImportChildBottom.Visible = false;
                //}
                #endregion

                //DayCarePL.SchoolYearProperties ObjSchoolYear = proxyChildSchoolYear.GetPreviousYearOfCurrentYear(new Guid(Session["SchoolId"].ToString()), new Guid(Session["CurrentSchoolYearId"].ToString()));

                //divErrorMsg.Style.Add("display", "none");
                //btnImportChild.Visible = true;
                //btnImportChildBottom.Visible = true;
                //DayCareBAL.ChildSchoolYearService proxyChild = new DayCareBAL.ChildSchoolYearService();
                //List<DayCarePL.ChildDataProperties> lstChildData = proxyChild.GetAllChildListForImport(new Guid(Session["CurrentSchoolYearId"].ToString()), new Guid(ddlPrevSchoolyearOfSelectedCurrentSchoolYear.SelectedValue), new Guid(Session["SchoolId"].ToString()));
                //if (lstChildData != null)
                //{
                //    rgChildList.DataSource = lstChildData.OrderBy(i => i.FamilyName);
                //    rgChildList.DataBind();
                //}
                BindChildList();

            }
            catch (Exception ex)
            {

            }
        }

        protected void rgChildList_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
        }

        protected void rgChildList_EditCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {

        }

        protected void rgChildList_InsertCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {

        }

        protected void rgChildList_UpdateCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {

        }

        protected void rgChildList_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item.ItemType == GridItemType.Item || e.Item.ItemType == GridItemType.AlternatingItem)
            {
                CheckBox chkChildItem = e.Item.FindControl("chkChildItem") as CheckBox;
                DayCarePL.ChildDataProperties objChildList = e.Item.DataItem as DayCarePL.ChildDataProperties;
                if (objChildList.ImportedCount > 0)
                {
                    chkChildItem.Enabled = false;
                    chkChildItem.Checked = true;
                    chkChildItem.ToolTip = "This child is imported!";
                }
            }
        }

        protected void rgChildList_ItemCreated(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {

        }

        protected void rgChildList_DeleteCommand(object source, GridCommandEventArgs e)
        {

        }

        protected void rgChildList_PreRender(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                rgChildList.MasterTableView.FilterExpression = "([Active] = \'True\') ";
                GridColumn column = rgChildList.MasterTableView.GetColumnSafe("Active");
                rgChildList.MasterTableView.Rebind();
            }
        }

        protected void btnImportChild_Click(object sender, EventArgs e)
        {
            if (Session["CurrentSchoolYearId"] == null)
            {
                Response.Redirect("~/Login.aspx");
                return;
            }
            //List<DayCarePL.Child_Family> lstLeger = new List<DayCarePL.Child_Family>();
            //DayCarePL.Child_Family objChild_Family;
            DayCareBAL.ChildSchoolYearService proxyChildSchoolyear = new DayCareBAL.ChildSchoolYearService();
            foreach (GridDataItem e1 in rgChildList.MasterTableView.Items)
            {

                CheckBox chkChildItem = e1.FindControl("chkChildItem") as CheckBox;
                HiddenField ChildFamilyId = e1.FindControl("ChildFamilyId") as HiddenField;
                if (chkChildItem != null)
                {
                    if (chkChildItem.Checked && chkChildItem.Enabled == true)
                    {
                        //objChild_Family = new DayCarePL.Child_Family();
                        //objChild_Family.ChildDataId = new Guid(e1.GetDataKeyValue("ChildDataId").ToString());
                        //objChild_Family.ChildFamilyId = new Guid(ChildFamilyId.Value);
                        //lstLeger.Add(objChild_Family);
                        bool result = proxyChildSchoolyear.ImportAllSelectedChild(new Guid(e1.GetDataKeyValue("ChildDataId").ToString()), new Guid(ChildFamilyId.Value), new Guid(Session["CurrentSchoolYearId"].ToString()));

                    }
                }
            }
            rgChildList.Rebind();

        }

        public void SetMenuLink()
        {
            try
            {
                List<DayCarePL.MenuLink> lstMenu = new List<DayCarePL.MenuLink>();
                DayCarePL.MenuLink objMenu;
                objMenu = new DayCarePL.MenuLink();
                objMenu.Name = "Import Child";
                objMenu.Url = "";
                lstMenu.Add(objMenu);
                usrMenuLink.SetMenuLink(lstMenu);
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.LedgerOfFamily, "SetMenuLink", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
            }
        }

        protected void ddlPrevSchoolyearOfSelectedCurrentSchoolYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            rgChildList.Rebind();
        }
    }
}
