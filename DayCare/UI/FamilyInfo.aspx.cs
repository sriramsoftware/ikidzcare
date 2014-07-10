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
    public partial class FamilyInfo : System.Web.UI.Page
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
                try
                {
                    Common.BindRelationship(ddlRelationshipGuardian1, GetSchoolId());
                    Common.BindRelationship(ddlRelationshipGuardian2, GetSchoolId());
                    DayCareBAL.SchoolService proxy = new DayCareBAL.SchoolService();
                    // Common.BindCountryDropDown(ddlCountry);
                    try
                    {
                        Common.BindStateDropDown(ddlState, proxy.LoadSchoolInfo(new Guid(Session["SchoolId"].ToString())).CountryId.Value.ToString());
                        //ddlState.Items.Insert(0, new ListItem("--Select--", "00000000-0000-0000-0000-000000000000"));
                        Guid Id = proxy.LoadSchoolInfo(new Guid(Session["SchoolId"].ToString())).StateId.Value;

                        ddlState.SelectedValue = Id.ToString();
                    }
                    catch
                    {
                        ddlState.Items.Clear();
                        ddlState.Items.Insert(0, new ListItem("--Select--", "00000000-0000-0000-0000-000000000000"));
                    }




                    if (!string.IsNullOrEmpty(Request.QueryString["ChildFamilyId"]))
                    {
                        ViewState["ChildFamilyId"] = Request.QueryString["ChildFamilyId"].ToString();
                        Session["ChildFamilyId"] = Request.QueryString["ChildFamilyId"].ToString();
                        LoadFamilyData(new Guid(ViewState["ChildFamilyId"].ToString()));
                    }
                    if (Session["FamilyInfoMessage"] != null)
                    {
                        MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                        MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Saved Successfully", "false"));
                        Session.Remove("FamilyInfoMessage");
                    }
                }
                catch (Exception ex)
                {
                    DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.FamilyInfo, "Page_Load", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                }
            }

            //if (!Common.IsCurrentYear(GetCurrentSchoolYearId(), GetSchoolId()))
            //{
            //    btnSave.Enabled = false;
            //    btnCancel.Enabled = false;
            //}
            SetMenuLink();

            this.Form.DefaultButton = btnSave.UniqueID;
        }

        public void LoadFamilyData(Guid ChildFamilyId)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.FamilyInfo, "LoadFamilyData", "LoadFamilyData method called", DayCarePL.Common.GUID_DEFAULT);
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.FamilyInfo, "LoadFamilyData", "Debug LoadFamilyData", DayCarePL.Common.GUID_DEFAULT);
                DayCareBAL.ChildFamilyService proxyChildFamily = new DayCareBAL.ChildFamilyService();

                DayCarePL.ChildFamilyProperties objChildFamily = proxyChildFamily.LoadChildFamilyById(ChildFamilyId, new Guid(Session["CurrentSchoolYearId"].ToString()));
                if (objChildFamily != null)
                {
                    txtFamilyTitle.Text = objChildFamily.FamilyTitle;
                    txtUserName.Text = objChildFamily.UserName;
                    txtPassword.Text = objChildFamily.Password;
                    txtPassword.Attributes.Add("value", txtPassword.Text);
                    txtCode.Text = objChildFamily.Code;
                    txtCode.Attributes.Add("value", objChildFamily.Code);
                    txtAddress1.Text = objChildFamily.Address1;
                    txtAddress2.Text = objChildFamily.Address2;
                    txtCity.Text = objChildFamily.City;
                    txtZip.Text = objChildFamily.Zip;
                    if (ddlState.Items != null && ddlState.Items.Count > 0)
                    {
                        ddlState.SelectedValue = objChildFamily.StateId.ToString();
                    }
                    txtHomePhone.Text = objChildFamily.HomePhone;
                    chkMsgActive.Checked = objChildFamily.MsgActive.Value;
                    if (objChildFamily.MsgStartDate != null)
                    {
                        rdpMsgStartDate.SelectedDate = objChildFamily.MsgStartDate;
                    }
                    if (objChildFamily.MsgEndDate != null)
                    {
                        rdpMsgEndDate.SelectedDate = objChildFamily.MsgEndDate;
                    }
                    txtMessage.Text = objChildFamily.MsgDisplayed;
                    txtComments.Text = objChildFamily.Comments;
                    chkActive.Checked = objChildFamily.Active;
                    foreach (DayCarePL.FamilyDataProperties objFamilyData in objChildFamily.lstFamily)
                    {
                        if (objFamilyData.GuardianIndex == 1)
                        {
                            lblGuardian1FamilyId.Text = objFamilyData.Id.ToString();
                            txtFirstNameGuardian1.Text = objFamilyData.FirstName;
                            txtLastNameGuardian1.Text = objFamilyData.LastName;
                            ddlRelationshipGuardian1.SelectedValue = objFamilyData.RelationShipId.ToString();
                            txtEmailGuardian1.Text = objFamilyData.Email;
                            ddlPhoneType1Guardian1.SelectedValue = objFamilyData.Phone1Type;
                            txtPhone1Guardian1.Text = objFamilyData.Phone1;
                            ddPhoneType2Guardian1.SelectedValue = objFamilyData.Phone2Type;
                            txtPhone2Guardian1.Text = objFamilyData.Phone2;
                            lblImageGuardian1.Text = objFamilyData.Photo;
                            if (!string.IsNullOrEmpty(objFamilyData.Photo))
                            {
                                imgFamilyGuardian1.ImageUrl = "../FamilyImages/" + objFamilyData.Photo;
                            }
                            else
                            {
                                imgFamilyGuardian1.ImageUrl = "../FamilyImages/male_photo.png";
                            }
                        }
                        if (objFamilyData.GuardianIndex == 2)
                        {
                            lblGuardian2FamilyId.Text = objFamilyData.Id.ToString();
                            txtFirstNameGuardian2.Text = objFamilyData.FirstName;
                            txtLastNameGuardian2.Text = objFamilyData.LastName;
                            ddlRelationshipGuardian2.SelectedValue = objFamilyData.RelationShipId.ToString();
                            txtEmailGuardian2.Text = objFamilyData.Email;
                            ddlPhoneType1Guardian2.SelectedValue = objFamilyData.Phone1Type;
                            txtPhone1Guardian2.Text = objFamilyData.Phone1;
                            ddlPhoneType2Guardian2.SelectedValue = objFamilyData.Phone2Type;
                            txtPhone2Guardian2.Text = objFamilyData.Phone2;
                            lblImageGuardian2.Text = objFamilyData.Photo;
                            if (!string.IsNullOrEmpty(objFamilyData.Photo))
                            {
                                imgFamilyGuardian2.ImageUrl = "../FamilyImages/" + objFamilyData.Photo;
                            }
                            else
                            {
                                imgFamilyGuardian2.ImageUrl = "../FamilyImages/male_photo.png";
                            }
                        }
                    }
                    hdnName.Value = objChildFamily.UserName;
                    hdnCode.Value = objChildFamily.Code;

                }
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.FamilyInfo, "LoadFamilyData", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
            }
        }

        public Guid GetSchoolId()
        {
            Guid SchoolId = new Guid();
            if (Session["SchoolId"] != null)
            {
                SchoolId = new Guid(Session["SchoolId"].ToString());
            }
            return SchoolId;
        }

        public Guid GetCurrentSchoolYearId()
        {
            Guid CurrentSchoolYearId = new Guid();
            if (Session["CurrentSchoolYearId"] != null)
            {
                CurrentSchoolYearId = new Guid(Session["CurrentSchoolYearId"].ToString());
            }
            return CurrentSchoolYearId;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.FamilyInfo, "btnSave_Click", "Submit btnSave_Click called", DayCarePL.Common.GUID_DEFAULT);
            try
            {
                string err_msg = string.Empty;
                if (txtFirstNameGuardian1.Text.Trim() == "")
                {
                    err_msg = "- Please enter Guardian 1 First Name.\\n";
                }
                if (txtLastNameGuardian1.Text.Trim() == "")
                {
                    err_msg += "- Please enter Guardian 1 Last Name.";
                }
                if (err_msg.Length > 0)
                {
                    MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                    MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", err_msg, "false"));
                    return;
                }
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.FamilyInfo, "btnSave_Click", "Debug btnSave_Click ", DayCarePL.Common.GUID_DEFAULT);
                DayCareBAL.FamilyDataService proxyFamilyData = new DayCareBAL.FamilyDataService();
                DayCareBAL.ChildFamilyService proxyChildFamily = new DayCareBAL.ChildFamilyService();
                DayCarePL.FamilyDataProperties objFamilyData = null;
                DayCarePL.ChildFamilyProperties objChildFamily = new DayCarePL.ChildFamilyProperties();
                Guid ChildFamilyId;
                objChildFamily.SchoolId = GetSchoolId();
                txtPassword.Attributes.Add("value", txtPassword.Text);
                txtCode.Attributes.Add("value", txtCode.Text);
                if (!string.IsNullOrEmpty(txtUserName.Text.Trim()))
                {
                    if (!hdnName.Value.ToLower().Equals(txtUserName.Text.ToLower().Trim()))
                    {
                        bool result = proxyFamilyData.CheckDuplicateUserName(txtUserName.Text.Trim(), objChildFamily.SchoolId);
                        if (result)
                        {
                            MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                            MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "User Name Already Exist", "false"));
                            return;
                        }
                    }
                }

                bool IsCodeRequire = proxyFamilyData.CheckCodeRequire(objChildFamily.SchoolId);
                if (IsCodeRequire)
                {
                    if (string.IsNullOrEmpty(txtCode.Text.Trim()))
                    {
                        MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                        MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Please enter Passcode", "false"));
                        txtCode.Focus();
                        return;
                    }
                    else
                    {
                        //if (txtCode.Text.Length < 4)
                        //{
                        //    MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                        //    MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Passcode require 4 digit number.", "false"));
                        //    return;
                        //}
                    }
                    //if (!txtCode.Text.Trim().Equals(hdnCode.Value))
                    //{
                    //    bool IsCodeDuplicate = proxyFamilyData.CheckDuplicateCode(txtCode.Text.Trim(), GetSchoolId());
                    //    if (IsCodeDuplicate)
                    //    {
                    //        MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                    //        MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Code Already Exist", "false"));
                    //        return;
                    //    }
                    //}
                }
                if (rdpMsgEndDate.SelectedDate != null)
                {
                    if (rdpMsgStartDate.SelectedDate == null)
                    {
                        MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                        MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Start date require", "false"));
                        return;
                    }
                }
                if (rdpMsgStartDate.SelectedDate != null && rdpMsgEndDate.SelectedDate != null)
                {
                    if (rdpMsgStartDate.SelectedDate > rdpMsgEndDate.SelectedDate)
                    {
                        MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                        MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Start date must be less than End date", "false"));
                        return;
                    }
                }

                if (fupImageGuardian1.HasFile)
                {
                    string Extention = Path.GetExtension(fupImageGuardian1.FileName).ToLower();
                    string[] Ext = { ".jpeg", ".jpg", ".png" };
                    if (Ext.ToList().FindAll(et => et.Equals(Extention)).Count == 0)
                    {
                        MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                        MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Please select .JPEG,.PNG file!", "false"));
                        return;
                    }
                }

                if (fupImageGuardian2.HasFile)
                {
                    string Extention = Path.GetExtension(fupImageGuardian2.FileName).ToLower();
                    string[] Ext = { ".jpeg", ".jpg", ".png" };
                    if (Ext.ToList().FindAll(et => et.Equals(Extention)).Count == 0)
                    {
                        MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                        MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Please select .JPEG,.PNG file!", "false"));
                        return;
                    }
                }
                string Guardian1FamilyId = "", Guardian2FamilyId = "";
                if (ViewState["ChildFamilyId"] != null)
                {
                    objChildFamily.Id = new Guid(ViewState["ChildFamilyId"].ToString());
                    Guardian1FamilyId = lblGuardian1FamilyId.Text;
                    Guardian2FamilyId = lblGuardian2FamilyId.Text;
                }
                else
                {
                    if (Session["StaffId"] != null)
                    {
                        objChildFamily.CreatedById = new Guid(Session["StaffId"].ToString());
                    }
                }


                //Child Family 
                objChildFamily.FamilyTitle = txtLastNameGuardian1.Text + ", " + txtFirstNameGuardian1.Text;//txtFamilyTitle.Text.Trim();
                objChildFamily.UserName = txtUserName.Text.Trim();
                objChildFamily.Password = txtPassword.Text.Trim();
                objChildFamily.Code = txtCode.Text.Trim();
                objChildFamily.Address1 = txtAddress1.Text.Trim();
                objChildFamily.Address2 = txtAddress2.Text.Trim();
                objChildFamily.City = txtCity.Text.Trim();
                objChildFamily.Zip = txtZip.Text.Trim();
                if (!ddlState.SelectedValue.Equals(DayCarePL.Common.GUID_DEFAULT))
                {
                    objChildFamily.StateId = new Guid(ddlState.SelectedValue);
                }
                objChildFamily.HomePhone = txtHomePhone.Text.Trim();
                objChildFamily.MsgActive = chkMsgActive.Checked;
                if (rdpMsgStartDate.SelectedDate != null)
                {
                    objChildFamily.MsgStartDate = rdpMsgStartDate.SelectedDate.Value;
                }
                if (rdpMsgEndDate.SelectedDate != null)
                {
                    objChildFamily.MsgEndDate = rdpMsgEndDate.SelectedDate.Value;
                }
                objChildFamily.MsgDisplayed = txtMessage.Text.Trim();
                objChildFamily.Comments = txtComments.Text.Trim();
                objChildFamily.Active = chkActive.Checked;
                if (Session["StaffId"] != null)
                {
                    objChildFamily.LastModifiedById = new Guid(Session["StaffId"].ToString());
                }

                objChildFamily.lstFamily = new List<DayCarePL.FamilyDataProperties>();

                //Family Data Guardian 1
                objFamilyData = new DayCarePL.FamilyDataProperties();
                if (!string.IsNullOrEmpty(Guardian1FamilyId))
                {
                    objFamilyData.Id = new Guid(Guardian1FamilyId);
                }
                objFamilyData.FirstName = txtFirstNameGuardian1.Text.Trim();
                objFamilyData.LastName = txtLastNameGuardian1.Text.Trim();
                if (!ddlRelationshipGuardian1.SelectedValue.Equals(DayCarePL.Common.GUID_DEFAULT))
                {
                    objFamilyData.RelationShipId = new Guid(ddlRelationshipGuardian1.SelectedValue);
                }
                objFamilyData.Email = txtEmailGuardian1.Text.Trim();
                if (fupImageGuardian1.HasFile)
                {
                    objFamilyData.Photo = Path.GetExtension(fupImageGuardian1.FileName);
                }
                else
                {
                    if (!string.IsNullOrEmpty(lblImageGuardian1.Text))
                    {
                        objFamilyData.Photo = Path.GetExtension(lblImageGuardian1.Text);
                    }
                    else
                    {
                        objFamilyData.Photo = string.Empty;
                    }
                }
                if (!string.IsNullOrEmpty(txtPhone1Guardian1.Text.Trim()))
                {
                    objFamilyData.Phone1Type = ddlPhoneType1Guardian1.SelectedValue;
                    objFamilyData.Phone1 = txtPhone1Guardian1.Text.Trim();
                }
                if (!string.IsNullOrEmpty(txtPhone2Guardian1.Text.Trim()))
                {
                    objFamilyData.Phone2Type = ddPhoneType2Guardian1.SelectedValue;
                    objFamilyData.Phone2 = txtPhone2Guardian1.Text.Trim();
                }
                objFamilyData.GuardianIndex = 1;
                if (Session["StaffId"] != null)
                {
                    objFamilyData.LastModifiedById = new Guid(Session["StaffId"].ToString());
                }
                objChildFamily.lstFamily.Add(objFamilyData);

                //Family Data Guardian 2
                //if (!string.IsNullOrEmpty(txtFirstNameGuardian2.Text.Trim()) && !string.IsNullOrEmpty(txtEmailGuardian2.Text.Trim()) && ddlRelationshipGuardian2.SelectedIndex > 0 && !string.IsNullOrEmpty(txtLastNameGuardian2.Text.Trim()))
                if (!string.IsNullOrEmpty(txtFirstNameGuardian2.Text.Trim()) && !string.IsNullOrEmpty(txtLastNameGuardian2.Text.Trim()))
                {
                    objFamilyData = new DayCarePL.FamilyDataProperties();
                    if (!string.IsNullOrEmpty(Guardian2FamilyId))
                    {
                        objFamilyData.Id = new Guid(Guardian2FamilyId);
                    }
                    objFamilyData.FirstName = txtFirstNameGuardian2.Text.Trim();
                    objFamilyData.LastName = txtLastNameGuardian2.Text.Trim();
                    if (!ddlRelationshipGuardian2.SelectedValue.Equals(DayCarePL.Common.GUID_DEFAULT))
                    {
                        objFamilyData.RelationShipId = new Guid(ddlRelationshipGuardian2.SelectedValue);
                    }
                    objFamilyData.Email = txtEmailGuardian2.Text.Trim();
                    if (fupImageGuardian1.HasFile)
                    {
                        objFamilyData.Photo = Path.GetExtension(fupImageGuardian2.FileName);
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(lblImageGuardian2.Text))
                        {
                            objFamilyData.Photo = Path.GetExtension(lblImageGuardian2.Text);
                        }
                        else
                        {
                            objFamilyData.Photo = string.Empty;
                        }
                    }
                    if (!string.IsNullOrEmpty(txtPhone1Guardian2.Text.Trim()))
                    {
                        objFamilyData.Phone1Type = ddlPhoneType1Guardian2.SelectedValue;
                        objFamilyData.Phone1 = txtPhone1Guardian2.Text.Trim();
                    }
                    if (!string.IsNullOrEmpty(txtPhone2Guardian2.Text.Trim()))
                    {
                        objFamilyData.Phone2Type = ddlPhoneType2Guardian2.SelectedValue;
                        objFamilyData.Phone2 = txtPhone2Guardian2.Text.Trim();
                    }
                    objFamilyData.GuardianIndex = 2;
                    if (Session["StaffId"] != null)
                    {
                        objFamilyData.LastModifiedById = new Guid(Session["StaffId"].ToString());
                    }
                    objChildFamily.lstFamily.Add(objFamilyData);
                }
                else
                {
                    string ErrorMsg = "";
                    if (!string.IsNullOrEmpty(txtFirstNameGuardian2.Text.Trim()))
                    {
                        if (string.IsNullOrEmpty(txtLastNameGuardian2.Text.Trim()))
                        {
                            ErrorMsg = "- Please enter Guardian 2 Last Name\\n";
                            //MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                            //MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Please enter Guardian 2 Last Name", "false"));
                            //return;
                        }
                        //if (ddlRelationshipGuardian2.SelectedIndex == 0)
                        //{
                        //    ErrorMsg += "- Please enter Guardian 2 Relationship\\n";
                        //    //MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                        //    //MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Please enter Guardian 2 Relationship", "false"));
                        //    //return;
                        //}
                        //if (string.IsNullOrEmpty(txtEmailGuardian2.Text))
                        //{
                        //    ErrorMsg += "- Please enter Guardian 2 Email\\n";

                        //}
                        if (!string.IsNullOrEmpty(ErrorMsg))
                        {
                            MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                            MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", ErrorMsg, "false"));
                            return;
                        }
                    }
                }

                objChildFamily.SchoolYearId = new Guid(Session["CurrentSchoolYearId"].ToString());
                ChildFamilyId = proxyChildFamily.Save(objChildFamily);
                if (!ChildFamilyId.ToString().Equals(DayCarePL.Common.GUID_DEFAULT))
                {
                    if (fupImageGuardian1.HasFile)
                    {
                        fupImageGuardian1.SaveAs(Server.MapPath("~/FamilyImages/" + ChildFamilyId + "_1" + Path.GetExtension(fupImageGuardian1.FileName)));
                    }
                    if (fupImageGuardian2.HasFile)
                    {
                        fupImageGuardian2.SaveAs(Server.MapPath("~/FamilyImages/" + ChildFamilyId + "_2" + Path.GetExtension(fupImageGuardian2.FileName)));
                    }
                    MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                    MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Saved Successfully", "false"));

                    if (ViewState["ChildFamilyId"] == null)
                    {
                        Session["FamilyInfoMessage"] = true;
                        Response.Redirect("FamilyInfo.aspx?ChildFamilyId=" + ChildFamilyId + "", false);
                    }
                    else
                    {

                        rgChildData.Rebind();
                        LoadFamilyData(new Guid(ViewState["ChildFamilyId"].ToString()));
                    }
                    //else
                    //{
                    //    Response.Redirect("ChildFamily.aspx", false);
                    //}
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
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.FamilyInfo, "btnSave_Click", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Internal Error,Please try again", "false"));
                return;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("childfamily.aspx", false);
            return;
        }

        protected void rgChildData_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                Guid SchoolId = new Guid();
                if (Session["SchoolId"] != null)
                {
                    SchoolId = new Guid(Session["SchoolId"].ToString());
                }
                DayCareBAL.ChildDataService proxyChildData = new DayCareBAL.ChildDataService();
                if (ViewState["ChildFamilyId"] != null)
                {
                    List<DayCarePL.ChildDataProperties> lstChildData = proxyChildData.LoadChildData(new Guid(Session["SchoolId"].ToString()), new Guid(Session["CurrentSchoolYearId"].ToString()), new Guid(ViewState["ChildFamilyId"].ToString()));
                    if (lstChildData != null)
                    {
                        rgChildData.DataSource = lstChildData;
                    }
                }
                else
                {
                    rgChildData.DataSource = new List<DayCarePL.ChildDataProperties>();
                }

                // rgChildData.DataSource = proxyChildData.LoadChildData(new Guid(ViewState["SchoolId"].ToString()), new Guid(ViewState["SchoolYearId"].ToString()));
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.FamilyInfo, "rgChildData_NeedDataSource", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
            }
        }

        protected void rgChildData_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
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

            //if (!Common.IsCurrentYear(CurrentSchoolYearId, SchoolId))
            //{
            //    if (e.CommandName == "InitInsert")
            //    {
            //        e.Canceled = true;
            //    }
            //    else if (e.CommandName == "Edit")
            //    {
            //        e.Canceled = true;
            //    }
            //}
            //else
            //{
            if (e.CommandName == "Edit")
            {

                Guid ChildDataID = new Guid(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Id"].ToString());
                if (ChildDataID != null)
                {
                    ViewState["ChildId"] = ChildDataID;
                }
                //LoadDataById(ChildDataID, new Guid(ViewState["SchoolId"].ToString()), new Guid(ViewState["SchoolYearId"].ToString()));
                //e.Canceled = true;
                if (ViewState["ChildFamilyId"] != null)
                {
                    //Session["ChildUrl"] = "~/UI/FamilyInfo.aspx?ChildFamilyId=" + ViewState["ChildFamilyId"].ToString();
                    Response.Redirect("AddEditChild.aspx?ChildFamilyId=" + ViewState["ChildFamilyId"].ToString() + "&ChildDataId=" + ChildDataID);
                }
            }
            if (e.CommandName == "InitInsert")
            {
                if (ViewState["ChildFamilyId"] != null)
                {
                    //Session["ChildUrl"] = "~/UI/FamilyInfo.aspx?ChildFamilyId=" + ViewState["ChildFamilyId"].ToString();
                    Response.Redirect("AddEditChild.aspx?ChildFamilyId=" + ViewState["ChildFamilyId"].ToString());
                }
            }
            if (ViewState["ChildFamilyId"] == null)
            {
                if (e.CommandName == "Edit" || e.CommandName == "InitInsert")
                {
                    e.Canceled = true;
                    MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                    MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Please add Family info first", "false"));
                    return;
                }
            }
            //}
        }

        protected void rgChildData_EditCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            // e.Canceled = true;
        }

        protected void rgChildData_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item.ItemType == GridItemType.AlternatingItem || e.Item.ItemType == GridItemType.Item)
            {
                DayCarePL.ChildDataProperties objChildData = e.Item.DataItem as DayCarePL.ChildDataProperties;
                Image imgChild = e.Item.FindControl("imgPhoto") as Image;
                HyperLink hlChildEnrollmentStatus = e.Item.FindControl("hlChildEnrollmentStatus") as HyperLink;
                HyperLink hlChildAbsentHistory = e.Item.FindControl("hlChildAbsentHistory") as HyperLink;
                HyperLink hlChildSchedule = e.Item.FindControl("hlChildSchedule") as HyperLink;

                if (!string.IsNullOrEmpty(objChildData.Photo))
                {
                    imgChild.ImageUrl = "../ChildImages/" + objChildData.Photo;
                }
                else
                {
                    if (objChildData.Gender == true)
                    {
                        imgChild.ImageUrl = "../ChildImages/boy.png";
                    }
                    else
                    {
                        imgChild.ImageUrl = "../ChildImages/girl.png";
                    }
                }

                hlChildEnrollmentStatus.NavigateUrl = "ChildEnrollmentStatus.aspx?Id=" + objChildData.Id;
                hlChildAbsentHistory.NavigateUrl = "ChildAbsentHistory.aspx?Id=" + objChildData.Id;
                hlChildSchedule.NavigateUrl = "ChildProgEnrollment.aspx?ChildDataId=" + objChildData.Id;
            }

            if (e.Item is GridEditableItem && e.Item.IsInEditMode)
            {
                UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
                RadUpload upload = userControl.FindControl("fupImage") as RadUpload;
                MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                MasterAjaxManager.ResponseScripts.Add(string.Format("window['UploadId'] = '{0}';", upload.ClientID));

            }


        }

        protected void rgChildData_InsertCommand(object source, GridCommandEventArgs e)
        {
            if (Session["IsValid"] != null)
            {
                rgChildData.MasterTableView.Rebind();
            }
            else
            {
                e.Canceled = true;
            }
        }

        protected void rgChildData_UpdateCommand(object source, GridCommandEventArgs e)
        {
            if (Session["IsValid"] != null)
            {
                rgChildData.MasterTableView.Rebind();
            }
            else
            {
                e.Canceled = true;
            }

        }

        protected void rgChildData_ItemCreated(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item is GridFilteringItem)
            {
                GridFilteringItem filteringItem = e.Item as GridFilteringItem;
                //set dimensions for the filter textbox  
                TextBox box = filteringItem["FullName"].Controls[0] as TextBox;
                box.Width = Unit.Pixel(50);
                TextBox box1 = filteringItem["Gender"].Controls[0] as TextBox;
                box1.Width = Unit.Pixel(50);
                TextBox box2 = filteringItem["DOB"].Controls[0] as TextBox;
                box2.Width = Unit.Pixel(50);
                //RadDatePicker box3 = filteringItem["column4"].Controls[0] as RadDatePicker;
                //box3.Width = Unit.Pixel(100);
            }
        }

        protected void Page_PreRender(Object sender, EventArgs e)
        {
            txtUserName.Attributes.Add("autocomplete", "off");
            txtPassword.Attributes.Add("autocomplete", "off");
            txtCode.Attributes.Add("autocomplete", "off");
        }

        public void SetMenuLink()
        {
            try
            {
                string str = "";
                List<DayCarePL.MenuLink> lstMenu = new List<DayCarePL.MenuLink>();
                DayCarePL.MenuLink objMenu;
                if (ViewState["ChildFamilyId"] == null)
                {
                    Session.Remove("ChildFamilyName");
                }
                if (Session["ChildFamilyName"] != null)
                {
                    objMenu = new DayCarePL.MenuLink();
                    str = Session["ChildFamilyName"].ToString();

                    objMenu.Name = "Family" + str;
                    objMenu.Url = "~/UI/ChildFamily.aspx";
                    lstMenu.Add(objMenu);
                }
                objMenu = new DayCarePL.MenuLink();
                objMenu.Name = "Family Info";
                objMenu.Url = "";
                lstMenu.Add(objMenu);
                usrMenuLink.SetMenuLink(lstMenu);
            }
            catch
            {

            }
        }
    }
}
