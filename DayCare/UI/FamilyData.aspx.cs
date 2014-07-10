using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.IO;

namespace DayCare.UI
{
    public partial class FamilyData : System.Web.UI.Page
    {
        RadAjaxManager MasterAjaxManager;
        //protected void Page_Load(object sender, EventArgs e)
        //{
        //    if (Session["SchoolId"] == null || Session["CurrentSchoolYearId"] == null)
        //    {
        //        Response.Redirect("~/Login.aspx");
        //    }
           
        //    if (!IsPostBack)
        //    {
        //        try
        //        {
        //            Common.BindRelationship(ddlRelationship, GetSchoolId());
        //            Common.BindCountryDropDown(ddlCountry);
        //            ddlState.Items.Insert(0, new ListItem("--Select--", "00000000-0000-0000-0000-000000000000"));
        //            if (!string.IsNullOrEmpty(Request.QueryString["ChildFamilyId"]))
        //            {
        //                ViewState["ChildFamilyId"] = Request.QueryString["ChildFamilyId"].ToString();
        //            }
        //            if (!string.IsNullOrEmpty(Request.QueryString["Id"]))// != null && Request.QueryString["Id"]
        //            {
        //                ViewState["SelectedFamilyDataId"] = Request.QueryString["Id"].ToString();
        //                LoadFamilyData(new Guid(ViewState["SelectedFamilyDataId"].ToString()));
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.Staff, "Page_Load", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
        //        }
        //    }
            
        //    if (!Common.IsCurrentYear(GetCurrentSchoolYearId(), GetSchoolId()))
        //    {
        //        btnSave.Enabled = false;
        //        btnCancel.Enabled = false;
        //    }
        //    SetMenuLink();
        //}

        //protected void btnSave_Click(object sender, EventArgs e)
        //{
        //    DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.Staff, "btnSave_Click", "Submit btnSave_Click called", DayCarePL.Common.GUID_DEFAULT);
        //    try
        //    {
        //        DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.Role, "btnSave_Click", "Debug btnSave_Click ", DayCarePL.Common.GUID_DEFAULT);
        //        DayCareBAL.FamilyDataService proxyFamilyData = new DayCareBAL.FamilyDataService();
        //        DayCarePL.FamilyDataProperties objFamilyData = new DayCarePL.FamilyDataProperties();
        //        Guid FamilyDataId;
        //        objFamilyData.SchoolId = GetSchoolId();
        //        if (!string.IsNullOrEmpty(txtUserName.Text.Trim()))
        //        {
        //            if (!hdnName.Value.Equals(txtUserName.Text.Trim()))
        //            {
        //                bool result = proxyFamilyData.CheckDuplicateUserName(txtUserName.Text.Trim(), objFamilyData.SchoolId);
        //                if (result)
        //                {
        //                    MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
        //                    MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "User Name Already Exist", "false"));
        //                    return;
        //                }
        //            }
        //        }
                
        //        bool IsCodeRequire = proxyFamilyData.CheckCodeRequire(objFamilyData.SchoolId);
        //        if (IsCodeRequire)
        //        {
        //            if (string.IsNullOrEmpty(txtCode.Text.Trim()))
        //            {
        //                MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
        //                MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Please enter Code", "false"));
        //                return;
        //            }
        //            if (!txtCode.Text.Trim().Equals(hdnCode.Value))
        //            {
        //                bool IsCodeDuplicate = proxyFamilyData.CheckDuplicateCode(txtCode.Text.Trim(), GetSchoolId());
        //                if (IsCodeDuplicate)
        //                {
        //                    MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
        //                    MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Code Already Exist", "false"));
        //                    return;
        //                }
        //            }
        //        }
        //        if (fupImage.HasFile)
        //        {
        //            string Extention = Path.GetExtension(fupImage.FileName).ToLower();
        //            string[] Ext = { ".jpeg", ".jpg", ".png" };
        //            if (Ext.ToList().FindAll(et => et.Equals(Extention)).Count == 0)
        //            {
        //                MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
        //                MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Please select .JPEG,.PNG file!", "false"));
        //                return;
        //            }
        //        }

        //        if (ViewState["SelectedFamilyDataId"] != null)
        //        {
        //            objFamilyData.Id = new Guid(ViewState["SelectedFamilyDataId"].ToString());
        //            objFamilyData.ChildFamilyId = new Guid(lblChildFamilyId.Text);
        //        }
        //        else
        //        {
        //            if (ViewState["ChildFamilyId"] != null)
        //            {
        //                objFamilyData.ChildFamilyId = new Guid(ViewState["ChildFamilyId"].ToString());
        //            }
        //            if (Session["StaffId"] != null)
        //            {
        //                objFamilyData.CreatedById = new Guid(Session["StaffId"].ToString());
        //            }
        //        }

        //        objFamilyData.LastModifiedById = new Guid(Session["StaffId"].ToString());
        //        objFamilyData.FirstName = txtFirstName.Text.Trim();
        //        objFamilyData.LastName = txtLastName.Text.Trim();
        //        objFamilyData.Address1 = txtAddress1.Text.Trim();
        //        objFamilyData.Address2 = txtAddress2.Text.Trim();
        //        objFamilyData.City = txtCity.Text.Trim();
        //        objFamilyData.Zip = txtZip.Text.Trim();
        //        objFamilyData.Fax = txtFax.Text.Trim();
        //        objFamilyData.IdInfo = txtIdInfo.Text.Trim();
        //        if (!ddlCountry.SelectedValue.Equals(DayCarePL.Common.GUID_DEFAULT))
        //        {
        //            objFamilyData.CountryId = new Guid(ddlCountry.SelectedValue);
        //        }
        //        if (!ddlState.SelectedValue.Equals(DayCarePL.Common.GUID_DEFAULT))
        //        {
        //            objFamilyData.StateId = new Guid(ddlState.SelectedValue);
        //        }
        //        objFamilyData.MainPhone = txtMainPhone.Text.Trim();
        //        objFamilyData.SecondaryPhone = txtSecondaryPhone.Text.Trim();
        //        objFamilyData.Email = txtEmail.Text.Trim();
        //        objFamilyData.UserName = txtUserName.Text.Trim();
        //        objFamilyData.Password = txtPassword.Text.Trim();
        //        objFamilyData.RelationShipId = new Guid(ddlRelationship.SelectedValue);
        //        objFamilyData.Code = txtCode.Text.Trim();
        //        if (rdMale.Checked == true)
        //        {
        //            objFamilyData.Gender = true;
        //        }
        //        else
        //        {
        //            objFamilyData.Gender = false;
        //        }
        //        objFamilyData.SecurityQuestion = txtSecurityQuestion.Text.Trim();
        //        objFamilyData.SecurityAnswer = txtSecurityAnswer.Text.Trim();
        //        if (fupImage.HasFile)
        //        {
        //            objFamilyData.Photo = Path.GetExtension(fupImage.FileName);
        //        }
        //        else
        //        {
        //            if (!string.IsNullOrEmpty(lblImage.Text))
        //            {
        //                objFamilyData.Photo = Path.GetExtension(lblImage.Text);
        //            }
        //            else
        //            {
        //                objFamilyData.Photo = string.Empty;
        //            }
        //        }
        //        if (rdActive.Checked == true)
        //        {
        //            objFamilyData.Active = true;
        //        }
        //        else
        //        {
        //            objFamilyData.Active = false;
        //        }
        //        objFamilyData.FamilyDataComments = txtFamilyDataComment.Text.Trim();

        //        FamilyDataId = proxyFamilyData.Save(objFamilyData);
        //        if (!FamilyDataId.ToString().Equals(DayCarePL.Common.GUID_DEFAULT))
        //        {
        //            if (fupImage.HasFile)
        //            {
        //                fupImage.SaveAs(Server.MapPath("~/FamilyImages/" + FamilyDataId + Path.GetExtension(fupImage.FileName)));
        //            }
        //            if (ViewState["ChildFamilyId"] != null)
        //            {
        //                Response.Redirect("FamilyDataList.aspx?ChildFamilyId=" + ViewState["ChildFamilyId"].ToString() + "", false);
        //            }
        //            else
        //            {
        //                Response.Redirect("ChildFamily.aspx", false);
        //            }
        //        }
        //        else
        //        {
        //            MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
        //            MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Internal Error,Please try again", "false"));
        //            return;
        //        }
        //    }

        //    catch (Exception ex)
        //    {
        //        DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.Staff, "btnSave_Click", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
        //        MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
        //        MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Internal Error,Please try again", "false"));
        //        return;
        //    }
        //}

        //protected void btnCancel_Click(object sender, EventArgs e)
        //{
        //    if (ViewState["ChildFamilyId"] != null)
        //    {
        //        Response.Redirect("FamilyDataList.aspx?ChildFamilyId=" + ViewState["ChildFamilyId"].ToString() + "", false);
        //    }
        //    else
        //    {
        //        Response.Redirect("ChildFamily.aspx", false);
        //    }
        //}

        //protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    Common.BindStateDropDown(ddlState, ddlCountry.SelectedValue);
        //}

        //public void LoadFamilyData(Guid FamilyDataId)
        //{
        //    DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.FamilyData, "SubmitRecord", "Submit record method called", DayCarePL.Common.GUID_DEFAULT);
        //    try
        //    {
        //        DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.FamilyData, "SubmitRecord", "Debug Submit Record Of Role", DayCarePL.Common.GUID_DEFAULT);
        //        DayCareBAL.FamilyDataService proxyFamilyData = new DayCareBAL.FamilyDataService();
                
        //        DayCarePL.FamilyDataProperties objFamilyData = proxyFamilyData.LoadFamilyDataById(FamilyDataId, GetSchoolId());
        //        if (objFamilyData != null)
        //        {
        //            if (!string.IsNullOrEmpty(objFamilyData.Photo))
        //            {
        //                imgFamily.ImageUrl = "../FamilyImages/" + objFamilyData.Photo;
        //            }
        //            else
        //            {
        //                if (objFamilyData.Gender == true)
        //                {
        //                    imgFamily.ImageUrl = "../FamilyImages/male_photo.png";
        //                }
        //                else
        //                {
        //                    imgFamily.ImageUrl = "../FamilyImages/female_photo.png";
        //                }
        //            }
        //            txtFirstName.Text = objFamilyData.FirstName;
        //            txtLastName.Text = objFamilyData.LastName;
        //            txtAddress1.Text = objFamilyData.Address1;
        //            txtAddress2.Text = objFamilyData.Address2;
        //            txtCity.Text = objFamilyData.City;
        //            txtZip.Text = objFamilyData.Zip;
        //            ddlCountry.SelectedValue = objFamilyData.CountryId.ToString();
        //            if (!objFamilyData.CountryId.ToString().Equals(DayCarePL.Common.GUID_DEFAULT))
        //            {
        //                Common.BindStateDropDown(ddlState, objFamilyData.CountryId.ToString());
        //            }
        //            if (ddlState.Items != null && ddlState.Items.Count > 0)
        //            {
        //                ddlState.SelectedValue = objFamilyData.StateId.ToString();
        //            }
        //            ddlRelationship.SelectedValue = objFamilyData.RelationShipId.ToString();
        //            txtMainPhone.Text = objFamilyData.MainPhone;
        //            txtSecondaryPhone.Text = objFamilyData.SecondaryPhone;
        //            txtEmail.Text = objFamilyData.Email;
        //            txtUserName.Text = objFamilyData.UserName;
        //            txtPassword.Text = objFamilyData.Password;
        //            txtPassword.Attributes.Add("value", txtPassword.Text);
        //            txtCode.Text = objFamilyData.Code;
        //            if (objFamilyData.Gender == true)
        //            {
        //                rdMale.Checked = true;
        //            }
        //            else
        //            {
        //                rdFemale.Checked = true;
        //            }
        //            txtSecurityQuestion.Text = objFamilyData.SecurityQuestion;
        //            txtSecurityAnswer.Text = objFamilyData.SecurityAnswer;
        //            if (objFamilyData.Active == true)
        //            {
        //                rdActive.Checked = true;
        //            }
        //            else
        //            {
        //                rdInactive.Checked = true;
        //            }
        //            lblChildFamilyId.Text = objFamilyData.ChildFamilyId.ToString();
        //            lblImage.Text = objFamilyData.Photo;
        //            hdnName.Value = objFamilyData.UserName;
        //            hdnCode.Value = objFamilyData.Code;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.FamilyData, "LoadStaffDetails", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
        //    }
        //}

        //public void SetMenuLink()
        //{
        //    try
        //    {
        //        List<DayCarePL.MenuLink> lstMenu = new List<DayCarePL.MenuLink>();
        //        DayCarePL.MenuLink objMenu;
        //        objMenu = new DayCarePL.MenuLink();
        //        objMenu.Name = "Child Family";
        //        objMenu.Url = "~/UI/ChildFamily.aspx";
        //        lstMenu.Add(objMenu);
        //        objMenu = new DayCarePL.MenuLink();
        //        objMenu.Name = "Family Data List";
        //        objMenu.Url = "~/UI/FamilyDataList.aspx?ChildFamilyId=" + Session["ChildFamilyId"].ToString();
        //        lstMenu.Add(objMenu);
        //        objMenu = new DayCarePL.MenuLink();
        //        objMenu.Name = "Family Data";
        //        objMenu.Url = "";
        //        lstMenu.Add(objMenu);
        //        usrMenuLink.SetMenuLink(lstMenu);
        //    }
        //    catch
        //    {

        //    }
        //}

        //public Guid GetSchoolId()
        //{
        //    Guid SchoolId = new Guid();
        //    if (Session["SchoolId"] != null)
        //    {
        //        SchoolId = new Guid(Session["SchoolId"].ToString());
        //    }
        //    return SchoolId;
        //}

        //public Guid GetCurrentSchoolYearId()
        //{
        //    Guid CurrentSchoolYearId = new Guid();
        //    if (Session["CurrentSchoolYearId"] != null)
        //    {
        //        CurrentSchoolYearId = new Guid(Session["CurrentSchoolYearId"].ToString());
        //    }
        //    return CurrentSchoolYearId;
        //}
    }
}
