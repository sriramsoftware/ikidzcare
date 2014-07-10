using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace DayCare.UI
{
    public partial class Staff : System.Web.UI.Page
    {
        RadAjaxManager MasterAjaxManager;
        Guid SchoolId = new Guid();
        Guid CurrentSchoolYearId = new Guid();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["SchoolId"] == null || Session["CurrentSchoolYearId"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            if (Session["SchoolId"] != null)
            {
                SchoolId = new Guid(Session["SchoolId"].ToString());
            }
            if (!IsPostBack)
            {
                try
                {
                    this.Form.DefaultButton = btnSave.UniqueID;
                    Common.BindUserGroupDropDown(ddlUserGroup, SchoolId);
                    Common.BindStaffCategoryDropDown(ddlStaffCategory, SchoolId);
                    Common.BindCountryDropDown(ddlCountry);

                    //ddlState.Items.Insert(0, new ListItem("--Select--", "00000000-0000-0000-0000-000000000000"));
                    if (!string.IsNullOrEmpty(Request.QueryString["Id"]))// != null && Request.QueryString["Id"]
                    {
                        ViewState["SelectedStaffId"] = Request.QueryString["Id"].ToString();
                        LoadStaffDetails(new Guid(ViewState["SelectedStaffId"].ToString()), new Guid(Session["CurrentSchoolYearId"].ToString()));
                    }
                    else
                    {
                        DayCareBAL.SchoolService proxySchool = new DayCareBAL.SchoolService();
                        DayCarePL.SchoolProperties objSchool = proxySchool.LoadSchoolInfo(new Guid(Session["SchoolId"].ToString()));
                        if (objSchool != null)
                        {
                            if (ddlCountry != null && ddlCountry.Items.Count > 0)
                            {
                                if (ddlCountry.Items.IndexOf(ddlCountry.Items.FindByValue(objSchool.CountryId.ToString())) != -1)
                                {
                                    ddlCountry.SelectedValue = objSchool.CountryId.ToString();
                                }
                            }
                            Common.BindStateDropDown(ddlState, ddlCountry.SelectedValue);
                            if (ddlState != null && ddlState.Items.Count > 0)
                            {
                                if (ddlState.Items.IndexOf(ddlState.Items.FindByValue(objSchool.StateId.ToString())) != -1)
                                {
                                    ddlState.SelectedValue = objSchool.StateId.ToString();
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.Staff, "Page_Load", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                }
            }

            if (Session["CurrentSchoolYearId"] != null)
            {
                CurrentSchoolYearId = new Guid(Session["CurrentSchoolYearId"].ToString());
            }

            //if (!Common.IsCurrentYear(CurrentSchoolYearId, SchoolId))
            //{
            //    btnSave.Enabled = false;
            //    btnCancel.Enabled = false;
            //}
            SetMenuLink();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.Staff, "btnSave_Click", "Submit btnSave_Click called", DayCarePL.Common.GUID_DEFAULT);
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.Role, "btnSave_Click", "Debug btnSave_Click ", DayCarePL.Common.GUID_DEFAULT);
                DayCareBAL.StaffService proxyStaffService = new DayCareBAL.StaffService();
                DayCarePL.StaffProperties objStaff = new DayCarePL.StaffProperties();
                Guid StaffId;
                txtPassword.Attributes.Add("value", txtPassword.Text);
                txtCode.Attributes.Add("value", txtCode.Text);
                if (fupImage.HasFile)
                {
                    string Extention = Path.GetExtension(fupImage.FileName).ToLower();
                    string[] Ext = { ".jpeg", ".jpg", ".png" };
                    if (Ext.ToList().FindAll(et => et.Equals(Extention)).Count == 0)
                    {
                        MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                        MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Please select .JPEG,.PNG file!", "false"));
                        return;
                    }
                }
                if (ViewState["SelectedStaffId"] != null)
                {
                    objStaff.Id = new Guid(ViewState["SelectedStaffId"].ToString());
                }
                else
                {
                    if (Session["StaffId"] != null)
                    {
                        objStaff.CreatedById = new Guid(Session["StaffId"].ToString());
                    }
                }
                Guid SchoolId = new Guid();
                if (Session["StaffId"] != null)
                {
                    objStaff.LastModifiedById = new Guid(Session["StaffId"].ToString());
                }
                if (!string.IsNullOrEmpty(txtUserName.Text.Trim()))
                {

                    if (Session["SchoolId"] != null)
                    {
                        SchoolId = new Guid(Session["SchoolId"].ToString());
                        objStaff.SchoolId = SchoolId;
                        objStaff.ScoolYearId = CurrentSchoolYearId;
                    }
                    bool result = proxyStaffService.CheckDuplicateUserName(txtUserName.Text.Trim(), objStaff.Id, SchoolId);
                    if (result)
                    {
                        MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                        MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "User Name Already Exist", "false"));
                        return;
                    }
                }

                if (Session["SchoolId"] != null)
                {
                    SchoolId = new Guid(Session["SchoolId"].ToString());
                    objStaff.SchoolId = SchoolId;
                    objStaff.ScoolYearId = CurrentSchoolYearId;
                }
                bool IsCodeRequire = proxyStaffService.CheckCodeRequire(objStaff.SchoolId);
                if (IsCodeRequire)
                {
                    if (string.IsNullOrEmpty(txtCode.Text.Trim()))
                    {
                        MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                        MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Please enter Passcode", "false"));
                        txtCode.Focus();
                        return;
                    }
                }
                //bool result = proxyStaffService.CheckDuplicateCode(txtCode.Text.Trim(), objStaff.Id, SchoolId);
                //if (result)
                //{
                //    MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                //    MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Code Already Exist", "false"));
                //    return;
                //}


                objStaff.UserGroupId = new Guid(ddlUserGroup.SelectedValue);
                objStaff.StaffCategoryId = new Guid(ddlStaffCategory.SelectedValue);
                objStaff.FirstName = txtFirstName.Text.Trim();
                objStaff.LastName = txtLastName.Text.Trim();
                objStaff.Address1 = txtAddress1.Text.Trim();
                objStaff.Address2 = txtAddress2.Text.Trim();
                objStaff.City = txtCity.Text.Trim();
                objStaff.Zip = txtZip.Text.Trim();
                objStaff.CountryId = new Guid(ddlCountry.SelectedValue);
                objStaff.StateId = new Guid(ddlState.SelectedValue);
                objStaff.MainPhone = txtMainPhone.Text.Trim();
                objStaff.SecondaryPhone = txtSecondaryPhone.Text.Trim();
                objStaff.Email = txtEmail.Text.Trim();
                objStaff.UserName = txtUserName.Text.Trim();
                objStaff.Password = txtPassword.Text.Trim();

                objStaff.Code = txtCode.Text.Trim();
                //if (rdMale.Checked == true)
                //{
                //    objStaff.Gender = true;
                //}
                //else
                //{
                //    objStaff.Gender = false;
                //}
                if (ddlGender.SelectedValue == "true")
                {
                    objStaff.Gender = true;
                }
                else
                {
                    objStaff.Gender = false;
                }
                objStaff.SecurityQuestion = txtSecurityQuestion.Text.Trim();
                objStaff.SecurityAnswer = txtSecurityAnswer.Text.Trim();
                if (fupImage.HasFile)
                {
                    objStaff.Photo = fupImage.FileName;
                }
                else
                {
                    objStaff.Photo = lblImage.Text;
                }
                if (rdActive.Checked == true)
                {
                    objStaff.Active = true;
                }
                else
                {
                    objStaff.Active = false;
                }
                objStaff.Comments = txtComment.Text.Trim();
                objStaff.Message = txtMessage.Text;
                //objStaff.IsPrimary = chkIsPrimary.Checked;
                StaffId = proxyStaffService.Save(objStaff);
                if (!StaffId.ToString().Equals(DayCarePL.Common.GUID_DEFAULT))
                {
                    if (fupImage.HasFile)
                    {
                        #region "resize image"
                        // Create a bitmap of the content of the fileUpload control in memory
                        Bitmap originalBMP = new Bitmap(fupImage.FileContent);

                        // Calculate the new image dimensions
                        double origWidth = originalBMP.Width;
                        double origHeight = originalBMP.Height;
                        double sngRatio = origWidth / origHeight;
                        int newWidth = 200;
                        int newHeight = (int) (newWidth / sngRatio);
                          //int newHeight =0;
                          //if (sngRatio > 0)
                          //    newHeight = newWidth / sngRatio;
                          //else
                          //    newHeight = 200;

                        // Create a new bitmap which will hold the previous resized bitmap
                        Bitmap newBMP = new Bitmap(originalBMP, newWidth, newHeight);
                        // Create a graphic based on the new bitmap
                        Graphics oGraphics = Graphics.FromImage(newBMP);

                        // Set the properties for the new graphic file
                        oGraphics.SmoothingMode = SmoothingMode.AntiAlias; oGraphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        // Draw the new graphic based on the resized bitmap
                        oGraphics.DrawImage(originalBMP, 0, 0, newWidth, newHeight);

                        // Save the new graphic file to the server
                        newBMP.Save(Server.MapPath("~/StaffImages/" + StaffId + Path.GetExtension(fupImage.FileName)));

                        // Once finished with the bitmap objects, we deallocate them.
                        originalBMP.Dispose();
                        newBMP.Dispose();
                        oGraphics.Dispose();                
                        #endregion

                        //fupImage.SaveAs(Server.MapPath("~/StaffImages/" + StaffId + Path.GetExtension(fupImage.FileName)));
                    }

                    //MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                    //MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Saved Successfully", "false"));
                    Response.Redirect("StaffList.aspx", false);
                }
                else
                {
                    MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                    MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Internal Error,Please try again.", "false"));
                    return;
                }
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.Staff, "btnSave_Click", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Internal Error,Please try again", "false"));
                return;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("StaffList.aspx");
        }

        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            Common.BindStateDropDown(ddlState, ddlCountry.SelectedValue);
        }

        public void LoadStaffDetails(Guid StaffId,Guid CurrentSchoolyearId)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.Staff, "SubmitRecord", "Submit record method called", DayCarePL.Common.GUID_DEFAULT);
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.Staff, "SubmitRecord", "Debug Submit Record Of Role", DayCarePL.Common.GUID_DEFAULT);
                DayCareBAL.StaffService proxyStaffService = new DayCareBAL.StaffService();
                DayCarePL.StaffProperties objStaff = proxyStaffService.LoadStaffBystaffId(StaffId, CurrentSchoolyearId);
                if (objStaff != null)
                {
                    if (!string.IsNullOrEmpty(objStaff.Photo))
                    {
                        imgStaff.ImageUrl = "../StaffImages/" + objStaff.Photo;
                    }
                    else
                    {
                        if (objStaff.Gender == true)
                        {
                            imgStaff.ImageUrl = "../StaffImages/male_photo.png";
                        }
                        else
                        {
                            imgStaff.ImageUrl = "../StaffImages/female_photo.png";
                        }
                    }
                    ddlUserGroup.SelectedValue = objStaff.UserGroupId.ToString();
                    ddlStaffCategory.SelectedValue = objStaff.StaffCategoryId.ToString();
                    txtFirstName.Text = objStaff.FirstName;
                    txtLastName.Text = objStaff.LastName;
                    txtAddress1.Text = objStaff.Address1;
                    txtAddress2.Text = objStaff.Address2;
                    txtCity.Text = objStaff.City;
                    txtZip.Text = objStaff.Zip;
                    ddlCountry.SelectedValue = objStaff.CountryId.ToString();
                    if (!objStaff.CountryId.ToString().Equals(DayCarePL.Common.GUID_DEFAULT))
                    {
                        Common.BindStateDropDown(ddlState, objStaff.CountryId.ToString());
                    }
                    if (ddlState.Items != null && ddlState.Items.Count > 0)
                    {
                        ddlState.SelectedValue = objStaff.StateId.ToString();
                    }
                    txtMainPhone.Text = objStaff.MainPhone;
                    txtSecondaryPhone.Text = objStaff.SecondaryPhone;
                    txtEmail.Text = objStaff.Email;
                    txtUserName.Text = objStaff.UserName;
                    txtPassword.Text = objStaff.Password;
                    txtPassword.Attributes.Add("value", txtPassword.Text);
                    txtCode.Text = objStaff.Code;
                    txtCode.Attributes.Add("value", objStaff.Code);
                    if (objStaff.Gender == true)
                    {
                        ddlGender.SelectedValue = "true";
                    }
                    else
                    {
                        ddlGender.SelectedValue = "false";
                    }
                    txtSecurityQuestion.Text = objStaff.SecurityQuestion;
                    txtSecurityAnswer.Text = objStaff.SecurityAnswer;
                    if (objStaff.Active == true)
                    {
                        rdActive.Checked = true;
                    }
                    else
                    {
                        rdInactive.Checked = true;
                    }
                    txtComment.Text = objStaff.Comments;
                    txtMessage.Text = objStaff.Message;
                    //chkIsPrimary.Checked = objStaff.IsPrimary;
                    lblImage.Text = objStaff.Photo;
                }
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.Staff, "LoadStaffDetails", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
            }
        }

        public void SetMenuLink()
        {
            try
            {
                List<DayCarePL.MenuLink> lstMenu = new List<DayCarePL.MenuLink>();
                DayCarePL.MenuLink objMenu;
                objMenu = new DayCarePL.MenuLink();
                objMenu.Name = "Staff List";
                objMenu.Url = "~/UI/StaffList.aspx";
                lstMenu.Add(objMenu);
                objMenu = new DayCarePL.MenuLink();
                objMenu.Name = "Staff Data";
                objMenu.Url = "";
                lstMenu.Add(objMenu);
                usrMenuLink.SetMenuLink(lstMenu);
            }
            catch
            {

            }
        }

        protected void Page_PreRender(Object sender, EventArgs e)
        {
            txtUserName.Attributes.Add("autocomplete", "off");
            txtPassword.Attributes.Add("autocomplete", "off");
        }
    }
}
