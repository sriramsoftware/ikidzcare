using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using Telerik.Web.UI;
using System.IO;
using System.Web.UI.WebControls;


namespace DayCare.UI
{
    public partial class AddEditChild : System.Web.UI.Page
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

                    chkActive.Checked = true;
                    Common.BindProgramName(ddlProg, new Guid(Session["CurrentSchoolYearId"].ToString()));
                    btnSave.Attributes.Add("onclick", "return ValidationOnformSubmit()");
                    Common.BindEnrollmentStatus(ddlEnrollmentStatus, new Guid(Session["SchoolId"].ToString()));
                    ddlFeesPeriod.Items.Insert(0, new ListItem("--Select--", DayCarePL.Common.GUID_DEFAULT));
                    ddlChildProgClassRoom.Items.Insert(0, "--Select--");
                    ddlChildProgClassRoom1.Items.Insert(0, "--Select--");
                    ddlChildProgClassRoom2.Items.Insert(0, "--Select--");
                    ddlChildProgClassRoom3.Items.Insert(0, "--Select--");
                    ddlChildProgClassRoom4.Items.Insert(0, "--Select--");

                    if (!string.IsNullOrEmpty(Request.QueryString["ChildFamilyId"]))
                    {
                        ViewState["ChildFamilyId"] = new Guid(Request.QueryString["ChildFamilyId"].ToString());
                        //ViewState["ChildSchoolYearId"] = new Guid(Request.QueryString["ChildFamilyId"].ToString());
                    }
                    if (!string.IsNullOrEmpty(Request.QueryString["ChildDataId"]))
                    {

                        ViewState["ChildDataId"] = new Guid(Request.QueryString["ChildDataId"].ToString());
                        BindAddEditProgram(new Guid(ViewState["ChildDataId"].ToString()), new Guid(Session["SchoolId"].ToString()), new Guid(DayCarePL.Common.GUID_DEFAULT));
                    }
                    SetMenuLink();
                    if (!string.IsNullOrEmpty(Request.QueryString["ChildDataId"]))
                    {
                        //hlAdditionalNotes.Enabled = true;
                        ViewState["ChildDataId"] = Request.QueryString["ChildDataId"].ToString();
                        //hlAdditionalNotes.Attributes.Add("onClick", "ShowLateFee('" + ViewState["ChildDataId"].ToString() + "'); return false;");
                    }
                    try
                    {
                        DayCareBAL.SchoolYearService ProxySchoolyear = new DayCareBAL.SchoolYearService();
                        DayCarePL.SchoolYearProperties obj = ProxySchoolyear.LoadSchoolYearDtail(new Guid(Session["SchoolId"].ToString()), new Guid(Session["CurrentSchoolYearId"].ToString()));
                        if (obj != null)
                        {
                            hdnSchoolYearStartDate.Value = obj.StartDate.ToString();
                            hdnSchoolYearEndDate.Value = obj.EndDate.ToString();
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                }
                catch (Exception ex)
                {
                    DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.AddEditChild, "Page_Load", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                }
            }
            if (ViewState["ChildDataId"] != null)
            {
                Guid ChildSchoolYearId = Common.GetChildSchoolYearId(new Guid(ViewState["ChildDataId"].ToString()), new Guid(Session["CurrentSchoolYearId"].ToString()));
                DayCareBAL.AddEditChildService proxyGetIsprimary = new DayCareBAL.AddEditChildService();
                List<DayCarePL.ChildProgEnrollmentProperties> objAddeEdit = proxyGetIsprimary.GetIsPrimarySchoolProgram(ChildSchoolYearId);
                if (objAddeEdit.Count > 0)
                {
                    btnSecondary.Enabled = true;
                    hlAdditionalNotes.Enabled = true;
                    hlAdditionalNotes.Attributes.Add("onClick", "ShowLateFee('" + ViewState["ChildDataId"].ToString() + "'); return false;");
                }
                else
                {
                    btnSecondary.Enabled = false;
                    hlAdditionalNotes.Enabled = false;
                }
            }
            EnableDisableControlWhenOtherYear(true);
            if (!Common.IsCurrentYear(GetCurrentSchoolYearId(), GetSchoolId()))
            {
                //EnableDisableControlWhenOtherYear(false);
            }
            this.Form.DefaultButton = btnSave.UniqueID;
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

        protected void ChkAll_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkAll.Checked == true)
            {
                ChkMon.Checked = true;
                ChkTue.Checked = true;
                ChkWen.Checked = true;
                ChkThus.Checked = true;
                ChkFri.Checked = true;
            }
            else
            {
                ChkMon.Checked = false;
                ChkTue.Checked = false;
                ChkWen.Checked = false;
                ChkThus.Checked = false;
                ChkFri.Checked = false;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.AddEditChild, "btnSave_Click", "Submit btnSave_Click called", DayCarePL.Common.GUID_DEFAULT);
            try
            {
                bool isPrimary = false;
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.AddEditChild, "btnSave_Click", "Debug btnSave_Click ", DayCarePL.Common.GUID_DEFAULT);
                DayCareBAL.AddEditChildService proxySave = new DayCareBAL.AddEditChildService();

                DayCareBAL.ChildEnrollmentStatusService proxyChildEnrollment = new DayCareBAL.ChildEnrollmentStatusService();

                DayCarePL.ChildProgEnrollmentProperties objChildProgEnrollment = new DayCarePL.ChildProgEnrollmentProperties();

                DayCarePL.AddEdditChildProperties objChildData = new DayCarePL.AddEdditChildProperties();
                objChildData.lstChildProgEnrolment = new List<DayCarePL.ChildProgEnrollmentProperties>();
                DayCarePL.ChildEnrollmentStatusProperties objChildEnrollment = new DayCarePL.ChildEnrollmentStatusProperties();

                /*------------------ChildData SaveCode----------------------------------------------------------------------*/
                #region Child
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
                if (ViewState["ChildFamilyId"] != null)
                {
                    objChildData.ChildFamilyId = new Guid(ViewState["ChildFamilyId"].ToString());
                    objChildData.SchoolYearId = new Guid(Session["CurrentSchoolYearId"].ToString());
                    if (Session["StaffId"] != null)
                    {
                        objChildData.LastModifiedById = new Guid(Session["StaffId"].ToString());
                        objChildData.CreatedById = new Guid(Session["StaffId"].ToString());
                    }
                }
                if (ViewState["ChildDataId"] != null)
                {
                    objChildData.Id = new Guid(ViewState["ChildDataId"].ToString());
                }
                if (ViewState["ChildEnrollmentId"] != null)
                {
                    objChildData.ChildEnrollmentId = new Guid(ViewState["ChildEnrollmentId"].ToString());
                }
                else
                {
                    objChildData.ChildEnrollmentId = new Guid(DayCarePL.Common.GUID_DEFAULT);
                }
                objChildData.FirstName = txtFirstName.Text.Trim();
                objChildData.LastName = txtLastName.Text.Trim();
                if (ddlGender.SelectedValue == "true")
                {
                    objChildData.Gender = true;
                }
                else
                {
                    objChildData.Gender = false;
                }
                if (rdpDOB.SelectedDate != null)
                {
                    objChildData.DOB = Convert.ToDateTime(rdpDOB.SelectedDate.ToString());
                }
                if (fupImage.HasFile)
                {
                    objChildData.Photo = Path.GetExtension(fupImage.FileName);
                }
                else
                {
                    objChildData.Photo = string.Empty;
                }
                objChildData.ChildComments = txtComments.Text.Trim();

                if (chkActive.Checked == true)
                {
                    objChildData.Active = true;
                }
                else
                {
                    objChildData.Active = false;
                }
                if (fupImage.HasFile)
                {
                    objChildData.Photo = fupImage.FileName;
                }
                else
                {
                    objChildData.Photo = lblImage.Text;
                }
                #endregion

                #region "Family is inactive so you are not allowed to active child"
                if (objChildData.Active)
                {
                    DayCareBAL.ChildFamilyService proxyFamily = new DayCareBAL.ChildFamilyService();
                    bool isFamilyActive = proxyFamily.LoadChildFamilyById(objChildData.ChildFamilyId, new Guid(Session["CurrentSchoolYearId"].ToString())).Active;

                    if (!isFamilyActive)
                    {
                        MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                        MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Family is inactive so not allowed to active child", "false"));
                        return;
                    }
                }
                #endregion
                /*------------------ChildProgEnrollment-SaveCode----------------------------------------------------------------------*/
                decimal amountresult = 0;

                //if (!string.IsNullOrEmpty(txtFee.Text))
                //{

                //    decimal.TryParse(txtFee.Text, out amountresult);
                //    if (amountresult == 0)
                //    {
                //        MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                //        MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Pease enter valid Fees.", "false"));
                //        return;
                //    }
                //}
                //else
                //{
                //    amountresult = 0;
                //}
                if (!string.IsNullOrEmpty(txtFee.Text.Trim()))
                {
                    amountresult = Convert.ToDecimal(txtFee.Text.Trim());
                }
                else
                {
                    amountresult = 0;
                }
                #region Monday
                if (ChkMon.Checked == true)
                {
                    objChildProgEnrollment = new DayCarePL.ChildProgEnrollmentProperties();
                    if (!string.IsNullOrEmpty(hdnMon.Value))
                    {
                        objChildProgEnrollment.Id = new Guid(hdnMon.Value);
                    }
                    if (!ddlProg.SelectedValue.Equals(DayCarePL.Common.GUID_DEFAULT))
                    {
                        objChildProgEnrollment.SchoolProgramId = new Guid(ddlProg.SelectedValue);
                    }
                    objChildProgEnrollment.DayIndex = 1;
                    if (!ddlDayType1.SelectedValue.Equals("--Select---"))
                    {
                        objChildProgEnrollment.DayType = ddlDayType1.SelectedValue;
                    }
                    if (!ddlChildProgClassRoom.SelectedValue.Equals(DayCarePL.Common.GUID_DEFAULT))
                    {
                        objChildProgEnrollment.ProgClassRoomId = new Guid(ddlChildProgClassRoom.SelectedValue);
                    }
                    if (!ddlFeesPeriod.SelectedValue.Equals(DayCarePL.Common.GUID_DEFAULT))
                    {
                        objChildProgEnrollment.FeesPeriodId = new Guid(ddlFeesPeriod.SelectedValue);
                    }
                    //if (!string.IsNullOrEmpty(txtFee.Text))
                    //{
                    objChildProgEnrollment.Fees = amountresult;// Convert.ToDecimal(txtFee.Text.Trim());
                    // }
                    if (Session["StaffId"] != null)
                    {
                        objChildProgEnrollment.CreatedById = new Guid(Session["StaffId"].ToString());
                        objChildProgEnrollment.LastModifiedById = new Guid(Session["StaffId"].ToString());
                    }
                    objChildData.lstChildProgEnrolment.Add(objChildProgEnrollment);
                    //ChildProgEnrollmentId = proxySave.ChildProgEnrollmentSave(objChildProgEnrollment);

                }
                else
                {
                    if (hdnMon.Value.Length == 36)
                    {
                        int result = proxySave.ChildProgEnrollmentDelete(new Guid(hdnMon.Value));
                    }
                }
                #endregion

                #region Tuseday
                if (ChkTue.Checked == true)
                {
                    objChildProgEnrollment = new DayCarePL.ChildProgEnrollmentProperties();
                    if (!string.IsNullOrEmpty(hdnTue.Value))
                    {
                        objChildProgEnrollment.Id = new Guid(hdnTue.Value);
                    }
                    if (!ddlProg.SelectedValue.Equals(DayCarePL.Common.GUID_DEFAULT))
                    {
                        objChildProgEnrollment.SchoolProgramId = new Guid(ddlProg.SelectedValue);
                    }
                    objChildProgEnrollment.DayIndex = 2;
                    if (!ddlDayType2.SelectedValue.Equals("--Select---"))
                    {
                        objChildProgEnrollment.DayType = ddlDayType2.SelectedValue;
                    }
                    if (!ddlChildProgClassRoom1.SelectedValue.Equals(DayCarePL.Common.GUID_DEFAULT))
                    {
                        objChildProgEnrollment.ProgClassRoomId = new Guid(ddlChildProgClassRoom1.SelectedValue);
                    }
                    if (!ddlFeesPeriod.SelectedValue.Equals(DayCarePL.Common.GUID_DEFAULT))
                    {
                        objChildProgEnrollment.FeesPeriodId = new Guid(ddlFeesPeriod.SelectedValue);
                    }
                    //if (!string.IsNullOrEmpty(txtFee.Text))
                    //{
                    objChildProgEnrollment.Fees = amountresult;//Convert.ToDecimal(txtFee.Text.Trim());
                    //}
                    if (Session["StaffId"] != null)
                    {
                        objChildProgEnrollment.CreatedById = new Guid(Session["StaffId"].ToString());
                        objChildProgEnrollment.LastModifiedById = new Guid(Session["StaffId"].ToString());
                    }
                    objChildData.lstChildProgEnrolment.Add(objChildProgEnrollment);

                }
                else
                {
                    if (hdnTue.Value.Length == 36)
                    {
                        int result = proxySave.ChildProgEnrollmentDelete(new Guid(hdnTue.Value));
                    }
                }
                #endregion

                #region Wednseday
                if (ChkWen.Checked == true)
                {
                    objChildProgEnrollment = new DayCarePL.ChildProgEnrollmentProperties();
                    if (!string.IsNullOrEmpty(hdnWen.Value))
                    {
                        objChildProgEnrollment.Id = new Guid(hdnWen.Value);
                    }
                    if (!ddlProg.SelectedValue.Equals(DayCarePL.Common.GUID_DEFAULT))
                    {
                        objChildProgEnrollment.SchoolProgramId = new Guid(ddlProg.SelectedValue);
                    }
                    objChildProgEnrollment.DayIndex = 3;
                    if (!ddlDayType3.SelectedValue.Equals("--Select---"))
                    {
                        objChildProgEnrollment.DayType = ddlDayType3.SelectedValue;
                    }
                    if (!ddlChildProgClassRoom2.SelectedValue.Equals(DayCarePL.Common.GUID_DEFAULT))
                    {
                        objChildProgEnrollment.ProgClassRoomId = new Guid(ddlChildProgClassRoom2.SelectedValue);
                    }
                    if (!ddlFeesPeriod.SelectedValue.Equals(DayCarePL.Common.GUID_DEFAULT))
                    {
                        objChildProgEnrollment.FeesPeriodId = new Guid(ddlFeesPeriod.SelectedValue);
                    }
                    //if (!string.IsNullOrEmpty(txtFee.Text))
                    // {
                    objChildProgEnrollment.Fees = amountresult;// Convert.ToDecimal(txtFee.Text.Trim());
                    //}
                    if (Session["StaffId"] != null)
                    {
                        objChildProgEnrollment.CreatedById = new Guid(Session["StaffId"].ToString());
                        objChildProgEnrollment.LastModifiedById = new Guid(Session["StaffId"].ToString());
                    }
                    objChildData.lstChildProgEnrolment.Add(objChildProgEnrollment);

                }
                else
                {
                    if (hdnWen.Value.Length == 36)
                    {
                        int result = proxySave.ChildProgEnrollmentDelete(new Guid(hdnWen.Value));
                    }
                }
                #endregion

                #region Thursday
                if (ChkThus.Checked == true)
                {
                    objChildProgEnrollment = new DayCarePL.ChildProgEnrollmentProperties();
                    if (!string.IsNullOrEmpty(hdnThus.Value))
                    {
                        objChildProgEnrollment.Id = new Guid(hdnThus.Value);
                    }
                    if (!ddlProg.SelectedValue.Equals(DayCarePL.Common.GUID_DEFAULT))
                    {
                        objChildProgEnrollment.SchoolProgramId = new Guid(ddlProg.SelectedValue);
                    }
                    objChildProgEnrollment.DayIndex = 4;
                    if (!ddlDayType4.SelectedValue.Equals("--Select---"))
                    {
                        objChildProgEnrollment.DayType = ddlDayType4.SelectedValue;
                    }
                    if (!ddlChildProgClassRoom3.SelectedValue.Equals(DayCarePL.Common.GUID_DEFAULT))
                    {
                        objChildProgEnrollment.ProgClassRoomId = new Guid(ddlChildProgClassRoom3.SelectedValue);
                    }
                    if (!ddlFeesPeriod.SelectedValue.Equals(DayCarePL.Common.GUID_DEFAULT))
                    {
                        objChildProgEnrollment.FeesPeriodId = new Guid(ddlFeesPeriod.SelectedValue);
                    }
                    //if (!string.IsNullOrEmpty(txtFee.Text))
                    //{
                    objChildProgEnrollment.Fees = amountresult;// Convert.ToDecimal(txtFee.Text.Trim());
                    // }
                    if (Session["StaffId"] != null)
                    {
                        objChildProgEnrollment.CreatedById = new Guid(Session["StaffId"].ToString());
                        objChildProgEnrollment.LastModifiedById = new Guid(Session["StaffId"].ToString());
                    }
                    objChildData.lstChildProgEnrolment.Add(objChildProgEnrollment);

                }
                else
                {
                    if (hdnThus.Value.Length == 36)
                    {
                        int result = proxySave.ChildProgEnrollmentDelete(new Guid(hdnThus.Value));
                    }
                }
                #endregion

                #region Friday
                if (ChkFri.Checked == true)
                {
                    objChildProgEnrollment = new DayCarePL.ChildProgEnrollmentProperties();
                    if (!string.IsNullOrEmpty(hdnFri.Value))
                    {
                        objChildProgEnrollment.Id = new Guid(hdnFri.Value);
                    }
                    if (!ddlProg.SelectedValue.Equals(DayCarePL.Common.GUID_DEFAULT))
                    {
                        objChildProgEnrollment.SchoolProgramId = new Guid(ddlProg.SelectedValue);
                    }
                    objChildProgEnrollment.DayIndex = 5;
                    if (!ddlDayType5.SelectedValue.Equals("--Select---"))
                    {
                        objChildProgEnrollment.DayType = ddlDayType5.SelectedValue;
                    }
                    if (!ddlChildProgClassRoom4.SelectedValue.Equals(DayCarePL.Common.GUID_DEFAULT))
                    {
                        objChildProgEnrollment.ProgClassRoomId = new Guid(ddlChildProgClassRoom4.SelectedValue);
                    }
                    if (!ddlFeesPeriod.SelectedValue.Equals(DayCarePL.Common.GUID_DEFAULT))
                    {
                        objChildProgEnrollment.FeesPeriodId = new Guid(ddlFeesPeriod.SelectedValue);
                    }
                    //if (!string.IsNullOrEmpty(txtFee.Text))
                    //{
                    objChildProgEnrollment.Fees = amountresult;//Convert.ToDecimal(txtFee.Text.Trim());
                    //}
                    if (Session["StaffId"] != null)
                    {
                        objChildProgEnrollment.CreatedById = new Guid(Session["StaffId"].ToString());
                        objChildProgEnrollment.LastModifiedById = new Guid(Session["StaffId"].ToString());
                    }
                    objChildData.lstChildProgEnrolment.Add(objChildProgEnrollment);
                }
                else
                {
                    if (hdnFri.Value.Length == 36)
                    {
                        int result = proxySave.ChildProgEnrollmentDelete(new Guid(hdnFri.Value));
                    }
                }
                #endregion

                if (rdpStartDate.SelectedDate != null)
                {
                    objChildData.StartDate = rdpStartDate.SelectedDate.Value;
                }
                if (rdpEndDate.SelectedDate != null)
                {
                    objChildData.EndDate = rdpEndDate.SelectedDate.Value;
                }
                /*--------------------------ChildEnrollmentStatus-SaveCode------------------------------------------------------------------------------------*/
                #region ChildEnrollment
                objChildData.ChildEnrollmentComments = txtComments.Text.Trim();
                if (rdpEnrollmentDate.SelectedDate != null)
                {
                    objChildData.EnrollmentDate = Convert.ToDateTime(rdpEnrollmentDate.SelectedDate.ToString());
                }
                objChildData.EnrollmentStatusId = new Guid(ddlEnrollmentStatus.SelectedValue);
                objChildData.EnrollmentStatus = ddlEnrollmentStatus.SelectedItem.Text;
                #endregion

                DayCarePL.AddEdditChildProperties objChildDetail = proxySave.ChildSave(objChildData);
                if (objChildDetail != null)
                {
                    int Success = 0;
                    if (!objChildDetail.ChildDataId.ToString().Equals(DayCarePL.Common.GUID_DEFAULT))
                    {
                        if (fupImage.HasFile)
                        {
                            string strFile = Server.MapPath("~/ChildImages/" + objChildDetail.ChildDataId + Path.GetExtension(fupImage.FileName));
                            if (System.IO.File.Exists(strFile))
                            {
                                System.IO.File.SetAttributes(strFile, FileAttributes.Normal);
                                System.IO.File.Delete(strFile);
                            }

                            #region "resize image"
                            // Create a bitmap of the content of the fileUpload control in memory
                            System.Drawing.Bitmap originalBMP = new System.Drawing.Bitmap(fupImage.FileContent);

                            // Calculate the new image dimensions
                            double origWidth = originalBMP.Width;
                            double origHeight = originalBMP.Height;
                            double sngRatio = origWidth / origHeight;
                            int newWidth = 200;
                            int newHeight = (int)(newWidth / sngRatio);

                            // Create a new bitmap which will hold the previous resized bitmap
                            System.Drawing.Bitmap newBMP = new System.Drawing.Bitmap(originalBMP, newWidth, newHeight);
                            // Create a graphic based on the new bitmap
                            System.Drawing.Graphics oGraphics = System.Drawing.Graphics.FromImage(newBMP);

                            // Set the properties for the new graphic file
                            oGraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias; oGraphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                            // Draw the new graphic based on the resized bitmap
                            oGraphics.DrawImage(originalBMP, 0, 0, newWidth, newHeight);

                            // Save the new graphic file to the server
                            newBMP.Save(Server.MapPath("~/ChildImages/" + objChildDetail.ChildDataId + Path.GetExtension(fupImage.FileName)));

                            // Once finished with the bitmap objects, we deallocate them.
                            originalBMP.Dispose();
                            newBMP.Dispose();
                            oGraphics.Dispose();
                            #endregion
                            //fupImage.SaveAs(Server.MapPath("~/ChildImages/" + objChildDetail.ChildDataId + Path.GetExtension(fupImage.FileName)));
                        }
                    }
                    if (objChildData.lstChildProgEnrolment.Count == 0)
                    {
                        Success = 1;
                    }
                    foreach (DayCarePL.ChildProgEnrollmentProperties objChldProgEnrollment in objChildDetail.lstChildProgEnrolment)
                    {
                        isPrimary = objChldProgEnrollment.IsPrimary;
                        if (objChldProgEnrollment.Id.ToString().Equals("11111111-1111-1111-1111-111111111111"))
                        {
                            MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                            MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Child must have to enrolled a Primary program", "false"));
                            return;
                        }
                        if (objChldProgEnrollment.DayIndex == 1)
                        {
                            if (!objChldProgEnrollment.Id.ToString().Equals(DayCarePL.Common.GUID_DEFAULT))
                            {
                                hdnMon.Value = Convert.ToString(objChldProgEnrollment.Id);
                                Success++;
                            }
                        }
                        if (objChldProgEnrollment.DayIndex == 2)
                        {
                            if (!objChldProgEnrollment.Id.ToString().Equals(DayCarePL.Common.GUID_DEFAULT))
                            {
                                hdnTue.Value = Convert.ToString(objChldProgEnrollment.Id);
                                Success++;
                            }
                        }
                        if (objChldProgEnrollment.DayIndex == 3)
                        {
                            if (!objChldProgEnrollment.Id.ToString().Equals(DayCarePL.Common.GUID_DEFAULT))
                            {
                                hdnWen.Value = Convert.ToString(objChldProgEnrollment.Id);
                                Success++;
                            }
                        }
                        if (objChldProgEnrollment.DayIndex == 4)
                        {
                            if (!objChldProgEnrollment.Id.ToString().Equals(DayCarePL.Common.GUID_DEFAULT))
                            {
                                hdnThus.Value = Convert.ToString(objChldProgEnrollment.Id);
                                Success++;
                            }
                        }
                        if (objChldProgEnrollment.DayIndex == 5)
                        {
                            if (!objChldProgEnrollment.Id.ToString().Equals(DayCarePL.Common.GUID_DEFAULT))
                            {
                                hdnFri.Value = Convert.ToString(objChldProgEnrollment.Id);
                                Success++;
                            }
                        }

                    }
                    if (!objChildDetail.ChildDataId.ToString().Equals(DayCarePL.Common.GUID_DEFAULT) && objChildData.ChildEnrollmentId != null && Success > 0)
                    {
                        string strMessage = "Saved Successfully";
                        //if (!string.IsNullOrEmpty(hdSchoolProgram.Value) && !hdSchoolProgram.Value.Equals(ddlProg.SelectedValue) && isPrimary == true)
                        //{
                        //    strMessage = "Saved Successfully\\nYou have changed primary program, Please repost the family ledger";
                        //}
                        //SchoolProgramClearFields();
                        //ChildAndEnrollmentFieldClear();
                        try
                        {
                            Common.GetChildProgEnrollmentFeeDetail(GetSchoolId(), GetCurrentSchoolYearId(), new Guid(Session["StaffId"].ToString()));
                        }
                        catch (Exception ex)
                        {
                            
                            
                        }
                        ViewState["ChildDataId"] = objChildDetail.ChildDataId;
                        BindAddEditProgram(new Guid(ViewState["ChildDataId"].ToString()), new Guid(Session["SchoolId"].ToString()), new Guid(DayCarePL.Common.GUID_DEFAULT));
                        hlAdditionalNotes.Enabled = true;
                        if (ViewState["ChildDataId"] != null)
                        {
                            Guid ChildSchoolYearId = Common.GetChildSchoolYearId(new Guid(ViewState["ChildDataId"].ToString()), new Guid(Session["CurrentSchoolYearId"].ToString()));
                            DayCareBAL.AddEditChildService proxyGetIsprimary = new DayCareBAL.AddEditChildService();
                            List<DayCarePL.ChildProgEnrollmentProperties> objAddeEdit = proxyGetIsprimary.GetIsPrimarySchoolProgram(ChildSchoolYearId);
                            if (objAddeEdit.Count > 0)
                            {
                                btnSecondary.Enabled = true;
                            }
                            else
                            {
                                btnSecondary.Enabled = false;
                            }
                        }
                        //btnSecondary.Enabled = true;
                        rgAddEditChid.MasterTableView.Rebind();

                        if (hlAdditionalNotes.Enabled == false)
                        {
                            hlAdditionalNotes.ToolTip = "You can not add a additional notes until enrolled a primary program";
                        }
                        else
                        {
                            hlAdditionalNotes.ToolTip = "Add/View additional note";
                        }
                        if (btnSecondary.Enabled == false)
                        {
                            btnSecondary.ToolTip = "You can not add a secondary program until enrolled a primary program";
                        }
                        else
                        {
                            btnSecondary.ToolTip = "Add/View secondary program";
                        }

                        MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                        MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", strMessage, "false"));
                        return;

                    }
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
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.AddEditChild, "btnSave_Click", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Internal Error,Please try again.", "false"));
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            SchoolProgramClearFields();
            ChildAndEnrollmentFieldClear();
        }

        protected void btnSecondary_Click(object sender, EventArgs e)
        {
            if (ViewState["ChildFamilyId"] != null && ViewState["ChildDataId"] != null)
            {
                Response.Redirect("SecondaryProgram.aspx?ChildFamilyId=" + ViewState["ChildFamilyId"].ToString() + "&ChildDataId=" + ViewState["ChildDataId"].ToString());
            }
        }

        protected void btnAdditionalNotes_Click(object sender, EventArgs e)
        {
            if (ViewState["ChildDataId"] != null)
            {
                btnAdditionalNotes.Attributes.Add("onClick", "ShowLateFee('" + ViewState["ChildDataId"].ToString() + "'); return false;");
            }
        }

        protected void ddlProg_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["ChildDataId"] != null)
                {
                    BindSchoolProgram(new Guid(ViewState["ChildDataId"].ToString()), new Guid(ddlProg.SelectedValue));
                }
                else
                {
                    DayCareBAL.AddEditChildService proxyFee = new DayCareBAL.AddEditChildService();
                    BindFeesPeriod(new Guid(ddlProg.SelectedValue));
                    //txtFee.Text = Convert.ToString(proxyFee.GetFees(new Guid(ddlProg.SelectedValue)));
                    Common.BindProgChildClassRoom(ddlChildProgClassRoom, new Guid(ddlProg.SelectedValue));
                    Common.BindProgChildClassRoom(ddlChildProgClassRoom1, new Guid(ddlProg.SelectedValue));
                    Common.BindProgChildClassRoom(ddlChildProgClassRoom2, new Guid(ddlProg.SelectedValue));
                    Common.BindProgChildClassRoom(ddlChildProgClassRoom3, new Guid(ddlProg.SelectedValue));
                    Common.BindProgChildClassRoom(ddlChildProgClassRoom4, new Guid(ddlProg.SelectedValue));

                    ChkMon.Checked = false; ChkTue.Checked = false; ChkWen.Checked = false; ChkThus.Checked = false; ChkFri.Checked = false;

                    ddlDayType1.SelectedIndex = 0;
                    ddlDayType2.SelectedIndex = 0;
                    ddlDayType3.SelectedIndex = 0;
                    ddlDayType4.SelectedIndex = 0;
                    ddlDayType5.SelectedIndex = 0;

                    hdnFri.Value = "";
                    hdnMon.Value = "";
                    hdnThus.Value = "";
                    hdnTue.Value = "";
                    hdnWen.Value = "";

                    ddlFeesPeriod.SelectedIndex = 0;
                    txtFee.Text = "";
                    rdpStartDate.SelectedDate = DateTime.Now;
                    //rdpEndDate.SelectedDate = null;
                }

                //BindEditProgramEnrollment(new Guid(Request.QueryString["ChildDataId"].ToString()), new Guid(ddlProg.SelectedValue.ToString()));
                //if (ViewState["SchoolProgramId"] != null)
                //{
                //    if (!ViewState["SchoolProgramId"].ToString().Equals(ddlProg.SelectedValue.ToString()))
                //    {

                //    }
                //}
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.AddEditChild, "ddlProg_SelectedIndexChanged", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
            }
        }

        protected void ddlFeesPeriod_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["SelectedFeesPeriodId"] != null && Convert.ToString(ViewState["SelectedFeesPeriodId"]).Equals(ddlFeesPeriod.SelectedValue))
                {
                    txtFee.Text = Convert.ToString(ViewState["SelectedFee"]);
                }
                else
                {
                    DayCareBAL.AddEditChildService proxyAddEditChild = new DayCareBAL.AddEditChildService();
                    txtFee.Text = Convert.ToString(proxyAddEditChild.GetFees(new Guid(ddlProg.SelectedValue), new Guid(ddlFeesPeriod.SelectedValue)));
                }


            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.AddEditChild, "ddlFeesPeriod_SelectedIndexChanged", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
            }
        }

        protected void rgAddEditChid_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
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
                    DayCareBAL.AddEditChildService proxySave = new DayCareBAL.AddEditChildService();
                    rgAddEditChid.DataSource = proxySave.LoadChildData(SchoolId, new Guid(Session["CurrentSchoolYearId"].ToString()), new Guid(ViewState["ChildFamilyId"].ToString()));

                }
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.AddEditChild, "rgAddEditChid_NeedDataSource", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
            }
        }

        protected void rgAddEditChid_DetailTableDataBind(object sender, Telerik.Web.UI.GridDetailTableDataBindEventArgs e)
        {
            try
            {
                GridDataItem dataItem = e.DetailTableView.ParentItem as GridDataItem;
                switch (e.DetailTableView.DataMember)
                {
                    case "ProgramDetail":
                        {
                            if (Session["CurrentSchoolYearId"] == null)
                                return;
                            Guid ChildSchoolYearId = Common.GetChildSchoolYearId(new Guid(dataItem.GetDataKeyValue("Id").ToString()), new Guid(Session["CurrentSchoolYearId"].ToString()));
                            DayCareBAL.AddEditChildService proxyAddEditChild = new DayCareBAL.AddEditChildService();
                            List<DayCarePL.ChildProgEnrollmentProperties> lstChildProgEnrollment = proxyAddEditChild.LoadAllProgEnrolled(ChildSchoolYearId);
                            //var result= lstChildProgEnrollment.Select(s => s.IsPrimary);
                            List<bool> Isprimary = lstChildProgEnrollment.Select(s => s.IsPrimary).ToList();

                            if (lstChildProgEnrollment != null)
                            {
                                if (lstChildProgEnrollment != null)
                                {
                                    if (lstChildProgEnrollment.Count > 0)
                                    {
                                        e.DetailTableView.DataSource = lstChildProgEnrollment.OrderBy(i => i.ProgramTitle);
                                    }
                                    else
                                    {
                                        e.DetailTableView.DataSource = new List<DayCarePL.ChildProgEnrollmentProperties>();
                                    }
                                }

                            }
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.AddEditChild, "rgAddEditChid_DetailTableDataBind", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
            }
        }

        protected void rgAddEditChid_EditCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {

        }

        protected void rgAddEditChid_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            try
            {
                if (e.Item is GridPagerItem)
                {
                    RadComboBox PageSizeCombo = (RadComboBox)e.Item.FindControl("PageSizeComboBox");
                    PageSizeCombo.Items.Clear();
                    PageSizeCombo.Items.Add(new RadComboBoxItem("25"));
                    PageSizeCombo.FindItemByText("25").Attributes.Add("ownerTableViewId", rgAddEditChid.MasterTableView.ClientID);
                    PageSizeCombo.Items.Add(new RadComboBoxItem("50"));
                    PageSizeCombo.FindItemByText("50").Attributes.Add("ownerTableViewId", rgAddEditChid.MasterTableView.ClientID);
                    //PageSizeCombo.Items[0].Text = "25";
                    //PageSizeCombo.Items[1].Text = "50";
                    PageSizeCombo.FindItemByText(e.Item.OwnerTableView.PageSize.ToString()).Selected = true;
                }

                if (e.Item.ItemType == GridItemType.AlternatingItem || e.Item.ItemType == GridItemType.Item)
                {
                    if (hlAdditionalNotes.Enabled == false)
                    {
                        hlAdditionalNotes.ToolTip = "You can not add a additional notes until enrolled a primary program";
                    }
                    else
                    {
                        hlAdditionalNotes.ToolTip = "Add/View additional note";
                    }

                    if (btnSecondary.Enabled == false)
                    {
                        btnSecondary.ToolTip = "You can not add a secondary program until enrolled a primary program";
                    }
                    else
                    {
                        btnSecondary.ToolTip = "Add/View secondary program";
                    }
                    if (e.Item.OwnerTableView.Name == "Child")
                    {
                        //DayCarePL.ChildProgEnrollmentProperties objChildEnrollment = e.Item.DataItem as DayCarePL.ChildProgEnrollmentProperties;
                        //if (e.Item is GridDataItem)
                        //{
                        //    if (!objChildEnrollment.IsPrimary)
                        //    {
                        //        GridDataItem dataItem = e.Item as GridDataItem;
                        //        ImageButton editButton = dataItem["EditColumn"].Controls[0] as ImageButton;
                        //        editButton.Visible = false;
                        //    }
                        //}
                        //hlAdditionalNotes.Visible = true;
                        //if (ViewState["ChildDataId"] != null)
                        //{
                        //    hlAdditionalNotes.Attributes.Add("onClick", "ShowLateFee('" + ViewState["ChildDataId"].ToString() + "'); return false;");
                        //}
                        DayCarePL.ChildDataProperties objChildData = e.Item.DataItem as DayCarePL.ChildDataProperties;
                        Image imgChild = e.Item.FindControl("imgPhoto") as Image;
                        HyperLink hlChildAbsentHistory = e.Item.FindControl("hlChildAbsentHistory") as HyperLink;
                        //if (!string.IsNullOrEmpty(Request.QueryString["ChildDataId"]) || ViewState["ChildDataId"] != null)
                        //{
                        //    if (objChildData.SchoolProgramId != new Guid(DayCarePL.Common.GUID_DEFAULT))
                        //    {
                        //        btnSecondary.Enabled = true;
                        //    }
                        //    else
                        //    {
                        //        btnSecondary.Enabled = false;
                        //    }
                        //}
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
                        hlChildAbsentHistory.NavigateUrl = "ChildAbsentHistory.aspx?Id=" + objChildData.Id;
                        //HyperLink hlAddNote = e.Item.FindControl("hlAdditionalNotes") as HyperLink;
                        //hlAddNote.Attributes.Add("onclick", "ShowLateFee('" + ViewState["ChildDataId"].ToString() + "'); return false;");
                    }
                    if (e.Item.OwnerTableView.Name == "Program")
                    {
                        DayCarePL.ChildProgEnrollmentProperties objChildEnrollment = e.Item.DataItem as DayCarePL.ChildProgEnrollmentProperties;
                        //if (!string.IsNullOrEmpty(Request.QueryString["ChildDataId"]) || ViewState["ChildDataId"] != null)
                        //{
                        //    if (objChildEnrollment.SchoolProgramId != new Guid(DayCarePL.Common.GUID_DEFAULT))
                        //    {
                        //        btnSecondary.Enabled = true;
                        //    }
                        //    else
                        //    {
                        //        btnSecondary.Enabled = false;
                        //    }
                        //}
                        if (e.Item is GridDataItem)
                        {

                            if (objChildEnrollment.IsPrimary)
                            {
                                GridDataItem dataItem = e.Item as GridDataItem;
                                //GridEditableItem dataItem = e.Item as GridEditableItem;
                                ImageButton deleteButton = dataItem["DeleteColumn"].Controls[0] as ImageButton;
                                deleteButton.Visible = false;
                            }
                            else
                            {
                                //GridDataItem dataItem = e.Item as GridDataItem;
                                GridEditableItem dataItem = e.Item as GridEditableItem;
                                ImageButton editColumn = dataItem["Edit"].Controls[0] as ImageButton;
                                editColumn.Visible = false;
                            }
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.AddEditChild, "rgAddEditChid_ItemDataBound", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
            }
        }

        protected void rgAddEditChid_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {

                //Guid ChildDataID = new Guid(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Id"].ToString());
                if (e.CommandName == "Edit")
                {

                    //  btnAdditionalNotes.Visible = true;
                    if (e.Item.OwnerTableView.Name == "Child")
                    {
                        GridEditableItem dataItem = (GridEditableItem)e.Item;
                        Guid SchoolProgramId = new Guid(dataItem["SchoolProgramId"].Text);
                        ddlProg.SelectedIndex = 0;
                        rdpStartDate.SelectedDate = null;
                        rdpEndDate.SelectedDate = null;
                        rdpEnrollmentDate.SelectedDate = null;
                        BindAddEditProgram(new Guid(dataItem.GetDataKeyValue("Id").ToString()), new Guid(Session["SchoolId"].ToString()), new Guid(DayCarePL.Common.GUID_DEFAULT));
                        txtFirstName.Focus();
                        ViewState["SchoolProgramId"] = SchoolProgramId;
                        ViewState["ChildDataId"] = dataItem.GetDataKeyValue("Id").ToString();
                        ViewState["ChildEnrollmentId"] = new Guid(dataItem["ChildEnrollmentStatusId"].Text);


                        #region"display Secondary Program when school Program is primary"
                        if (SchoolProgramId != new Guid(DayCarePL.Common.GUID_DEFAULT))
                        {
                            btnSecondary.Enabled = true;
                        }
                        else
                        {
                            btnSecondary.Enabled = false;
                        }
                        #endregion

                    }
                    if (e.Item.OwnerTableView.Name == "Program")
                    {
                        GridEditableItem ParentDataItem = e.Item.OwnerTableView.ParentItem as GridEditableItem;
                        GridEditableItem dataItem = (GridEditableItem)e.Item;
                        Guid SchoolProgramId = new Guid(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["SchoolProgramId"].ToString());
                        ddlProg.SelectedIndex = 0;
                        BindAddEditProgram(new Guid(ParentDataItem.GetDataKeyValue("Id").ToString()), new Guid(Session["SchoolId"].ToString()), SchoolProgramId);
                        txtFirstName.Focus();
                        ViewState["SchoolProgramId"] = SchoolProgramId;
                        ViewState["ChildDataId"] = ParentDataItem.GetDataKeyValue("Id").ToString();
                        ViewState["ChildEnrollmentId"] = new Guid(ParentDataItem["ChildEnrollmentStatusId"].Text);
                        //rdpStartDate.SelectedDate = null;
                        //rdpEndDate.SelectedDate = null;
                        //rdpEnrollmentDate.SelectedDate = null;

                        #region"Display Secondary Program When SchoolProgram is Primary"
                        if (SchoolProgramId != new Guid(DayCarePL.Common.GUID_DEFAULT))
                        {
                            btnSecondary.Enabled = true;
                        }
                        else
                        {
                            btnSecondary.Enabled = false;
                        }
                        #endregion
                    }
                    hlAdditionalNotes.Enabled = true;
                    if (ViewState["ChildDataId"] != null)
                    {
                        hlAdditionalNotes.Attributes.Add("onClick", "ShowLateFee('" + ViewState["ChildDataId"].ToString() + "'); return false;");
                    }
                    e.Canceled = true;
                }
                if (e.CommandName == "InitInsert")
                {
                    if (e.Item.OwnerTableView.Name == "Child")
                    {
                        SchoolProgramClearFields();
                        ChildAndEnrollmentFieldClear();
                        txtFirstName.Focus();
                        e.Canceled = true;
                        hlAdditionalNotes.Enabled = false;
                        btnSecondary.Enabled = false;
                    }
                }
                if (hlAdditionalNotes.Enabled == false)
                {
                    hlAdditionalNotes.ToolTip = "You can not add a additional notes until enrolled a primary program";
                }
                else
                {
                    hlAdditionalNotes.ToolTip = "Add/View additional note";
                }
                if (btnSecondary.Enabled == false)
                {
                    btnSecondary.ToolTip = "You can not add a secondary program until enrolled a primary program";
                }
                else
                {
                    btnSecondary.ToolTip = "Add/View secondary program";
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
                EnableDisableControlWhenOtherYear(true);
                if (!Common.IsCurrentYear(CurrentSchoolYearId, SchoolId))
                {
                    if (e.CommandName == "InitInsert")
                    {
                        e.Canceled = true;
                    }
                    else if (e.CommandName == "Edit")
                    {
                        e.Canceled = true;
                    }
                    //EnableDisableControlWhenOtherYear(false);
                }
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.AddEditChild, "rgAddEditChid_ItemCommand", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
            }
        }

        public void EnableDisableControlWhenOtherYear(bool True_False)
        {
            ddlProg.Enabled = True_False;
            ddlFeesPeriod.Enabled = True_False;
            txtFee.Enabled = True_False;
            rdpStartDate.Enabled = True_False;
            rdpEndDate.Enabled = True_False;

            ChkAll.Enabled = True_False;

            ChkMon.Enabled = True_False;
            ddlDayType1.Enabled = True_False;
            ddlChildProgClassRoom.Enabled = True_False;

            ChkTue.Enabled = True_False;
            ddlDayType2.Enabled = True_False;
            ddlChildProgClassRoom1.Enabled = True_False;

            ChkWen.Enabled = True_False;
            ddlDayType3.Enabled = True_False;
            ddlChildProgClassRoom2.Enabled = True_False;

            ChkThus.Enabled = True_False;
            ddlDayType4.Enabled = True_False;
            ddlChildProgClassRoom3.Enabled = True_False;

            ChkFri.Enabled = True_False;
            ddlDayType5.Enabled = True_False;
            ddlChildProgClassRoom4.Enabled = True_False;

            ddlEnrollmentStatus.Enabled = True_False;
            rdpEnrollmentDate.Enabled = True_False;
            txtComments.Enabled = True_False;
            chkActive.Enabled = True_False;

            btnAdditionalNotes.Enabled = True_False;
            //btnSecondary.Enabled = True_False;
        }

        protected void rgAddEditChid_DeleteCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                Guid SchoolProgramId = new Guid(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["SchoolProgramId"].ToString());
                GridDataItem dataItem = (GridDataItem)e.Item;
                if (Session["CurrentSchoolYearId"] == null)
                    return;
                Guid ChildSchoolYearId = Common.GetChildSchoolYearId(new Guid(dataItem["ChildDataId"].Text), new Guid(Session["CurrentSchoolYearId"].ToString()));
                DayCareBAL.AddEditChildService proxyFamilyPayment = new DayCareBAL.AddEditChildService();
                if (proxyFamilyPayment.DeleteSchoolProgramChildProgEnrollment(ChildSchoolYearId, SchoolProgramId))
                {
                    rgAddEditChid.MasterTableView.DetailTables[0].Rebind();
                    string strMessage = "Delete Successfully";//\\nYou have deleted this program, Please repost the family ledger";
                    MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                    MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", strMessage, "false"));
                    ChildAndEnrollmentFieldClear();
                    SchoolProgramClearFields();
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
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.AddEditChild, "rgAddEditChid_DeleteCommand", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
            }
        }

        protected void SchoolProgramClearFields()
        {
            ChkMon.Checked = false;
            ChkTue.Checked = false;
            ChkWen.Checked = false;
            ChkThus.Checked = false;
            ChkFri.Checked = false;
            ddlFeesPeriod.SelectedIndex = 0;
            ddlChildProgClassRoom.SelectedIndex = 0;
            ddlChildProgClassRoom1.SelectedIndex = 0;
            ddlChildProgClassRoom2.SelectedIndex = 0;
            ddlChildProgClassRoom3.SelectedIndex = 0;
            ddlChildProgClassRoom4.SelectedIndex = 0;
            ddlDayType1.SelectedIndex = 0;
            ddlDayType2.SelectedIndex = 0;
            ddlDayType3.SelectedIndex = 0;
            ddlDayType4.SelectedIndex = 0;
            ddlDayType5.SelectedIndex = 0;
            hdnMon.Value = "";
            hdnTue.Value = "";
            hdnWen.Value = "";
            hdnThus.Value = "";
            hdnFri.Value = "";
            txtFee.Text = "";
            rdpStartDate.SelectedDate = DateTime.Now;
            //rdpEndDate.SelectedDate = null;
        }

        protected void BindAddEditProgram(Guid ChildDataId, Guid SchoolId, Guid SelectedSchoolProgramId)
        {
            try
            {
                if (Session["CurrentSchoolYearId"] == null)
                    return;
                DayCareBAL.AddEditChildService proxyChildData = new DayCareBAL.AddEditChildService();
                DayCarePL.ChildDataProperties objChildData = proxyChildData.LoadChildDataId(ChildDataId, SchoolId, new Guid(Session["CurrentSchoolYearId"].ToString()));

                if (objChildData != null)
                {
                    txtFirstName.Text = objChildData.FirstName;
                    txtLastName.Text = objChildData.LastName;
                    if (objChildData.DOB != null)
                    {
                        rdpDOB.SelectedDate = objChildData.DOB;
                    }
                    if (objChildData.Gender == true)
                    {
                        ddlGender.SelectedValue = "true";
                    }
                    else
                    {
                        ddlGender.SelectedValue = "false";
                    }
                    if (!string.IsNullOrEmpty(objChildData.Photo))
                    {
                        imgStaff.ImageUrl = "../ChildImages/" + objChildData.Photo;
                    }
                    else
                    {
                        if (objChildData.Gender == true)
                        {
                            imgStaff.ImageUrl = "../ChildImages/boy.png";
                        }
                        else
                        {
                            imgStaff.ImageUrl = "../ChildImages/girl.png";
                        }
                    }
                    txtComments.Text = objChildData.Comments;
                    chkActive.Checked = objChildData.Active;
                    Common.BindEnrollmentStatus(ddlEnrollmentStatus, SchoolId);
                    if (ddlEnrollmentStatus != null && ddlEnrollmentStatus.Items.Count > 0)
                    {
                        ddlEnrollmentStatus.SelectedValue = objChildData.EnrollmentStatusId.ToString();
                    }
                    if (objChildData.EnrollmentDate != null)
                    {
                        rdpEnrollmentDate.SelectedDate = objChildData.EnrollmentDate;
                    }
                    lblImage.Text = objChildData.Photo;
                }
                if (SelectedSchoolProgramId.ToString().Equals(DayCarePL.Common.GUID_DEFAULT))
                {
                    SelectedSchoolProgramId = objChildData.SchoolProgramId;
                }
                BindSchoolProgram(ChildDataId, SelectedSchoolProgramId);
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.AddEditChild, "LoadDataById", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
            }
        }

        private void BindSchoolProgram(Guid ChildDataId, Guid SchoolProgramId)
        {
            if (Session["CurrentSchoolYearId"] == null)
                return;
            Guid ChildSchoolYearId = Common.GetChildSchoolYearId(ChildDataId, new Guid(Session["CurrentSchoolYearId"].ToString()));

            #region "Display Secondary program button when schoolProgram is primary"
            //if (SchoolProgramId != new Guid(DayCarePL.Common.GUID_DEFAULT))
            //{
            //    btnSecondary.Enabled = true;
            //}
            //else
            //{
            //    btnSecondary.Enabled = false;
            //}
            #endregion
            BindFeesPeriod(SchoolProgramId);

            Common.BindProgChildClassRoom(ddlChildProgClassRoom, SchoolProgramId);
            Common.BindProgChildClassRoom(ddlChildProgClassRoom1, SchoolProgramId);
            Common.BindProgChildClassRoom(ddlChildProgClassRoom2, SchoolProgramId);
            Common.BindProgChildClassRoom(ddlChildProgClassRoom3, SchoolProgramId);
            Common.BindProgChildClassRoom(ddlChildProgClassRoom4, SchoolProgramId);

            DayCareBAL.AddEditChildService proxy = new DayCareBAL.AddEditChildService();
            List<DayCarePL.ChildProgEnrollmentProperties> lstChilProgEnrollment = proxy.LoadProgEnrollment(ChildSchoolYearId, SchoolProgramId);
            if (lstChilProgEnrollment == null || lstChilProgEnrollment.Count == 0)
            {
                SchoolProgramClearFields();
                ddlFeesPeriod.SelectedIndex = 0;
                txtFee.Text = "";
                ViewState["SelectedFeesPeriodId"] = null;
                ViewState["SelectedFee"] = null;
            }


            if (lstChilProgEnrollment != null && lstChilProgEnrollment.Count > 0)
            {
                if (ddlProg.Items.IndexOf(ddlProg.Items.FindByValue(SchoolProgramId.ToString())) != -1)
                {
                    ddlProg.SelectedValue = SchoolProgramId.ToString();
                    hdSchoolProgram.Value = SchoolProgramId.ToString();
                }
                else
                {
                    ddlProg.SelectedIndex = 0;
                    hdSchoolProgram.Value = "0";
                }


                //Classroom and fees bind as per selected "Program"
                DayCareBAL.ChildProgEnrollmentService proxyFee = new DayCareBAL.ChildProgEnrollmentService();
                //txtFee.Text = Convert.ToString(proxyFee.GetFees(new Guid(ddlProg.SelectedValue)));

                //end bing fee and classroom

                SchoolProgramClearFields();
                foreach (DayCarePL.ChildProgEnrollmentProperties objChildProgEnrollment in lstChilProgEnrollment)
                {
                    if (string.IsNullOrEmpty(txtFee.Text))
                    {
                        txtFee.Text = Convert.ToString(objChildProgEnrollment.Fees);
                        if (objChildProgEnrollment.StartDate != null)
                        {
                            rdpStartDate.SelectedDate = objChildProgEnrollment.StartDate;
                        }
                        if (objChildProgEnrollment.EndDate != null)
                        {
                            rdpEndDate.SelectedDate = objChildProgEnrollment.EndDate;
                        }
                        if (objChildProgEnrollment.FeesPeriodId != null)
                        {
                            ViewState["SelectedFeesPeriodId"] = objChildProgEnrollment.FeesPeriodId;
                            ViewState["SelectedFee"] = objChildProgEnrollment.Fees;
                            ddlFeesPeriod.SelectedValue = Convert.ToString(objChildProgEnrollment.FeesPeriodId);
                        }
                    }
                    if (objChildProgEnrollment.DayIndex == 1)
                    {
                        ChkMon.Checked = true;
                        ddlDayType1.SelectedValue = objChildProgEnrollment.DayType;
                        ddlChildProgClassRoom.SelectedValue = objChildProgEnrollment.ProgClassRoomId.ToString();
                        hdnMon.Value = objChildProgEnrollment.Id.ToString();
                    }
                    if (objChildProgEnrollment.DayIndex == 2)
                    {
                        ChkTue.Checked = true;
                        ddlDayType2.SelectedValue = objChildProgEnrollment.DayType;
                        ddlChildProgClassRoom1.SelectedValue = objChildProgEnrollment.ProgClassRoomId.ToString();
                        hdnTue.Value = objChildProgEnrollment.Id.ToString();
                    }
                    if (objChildProgEnrollment.DayIndex == 3)
                    {
                        ChkWen.Checked = true;
                        ddlDayType3.SelectedValue = objChildProgEnrollment.DayType;
                        ddlChildProgClassRoom2.SelectedValue = objChildProgEnrollment.ProgClassRoomId.ToString();
                        hdnWen.Value = objChildProgEnrollment.Id.ToString();
                    }
                    if (objChildProgEnrollment.DayIndex == 4)
                    {
                        ChkThus.Checked = true;
                        ddlDayType4.SelectedValue = objChildProgEnrollment.DayType;
                        ddlChildProgClassRoom3.SelectedValue = objChildProgEnrollment.ProgClassRoomId.ToString();
                        hdnThus.Value = objChildProgEnrollment.Id.ToString();
                    }
                    if (objChildProgEnrollment.DayIndex == 5)
                    {
                        ChkFri.Checked = true;
                        ddlDayType5.SelectedValue = objChildProgEnrollment.DayType;
                        ddlChildProgClassRoom4.SelectedValue = objChildProgEnrollment.ProgClassRoomId.ToString();
                        hdnFri.Value = objChildProgEnrollment.Id.ToString();
                    }
                }
                //DateTime dt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                //string str = dt.DayOfWeek.ToString();
            }
        }

        public void ChildAndEnrollmentFieldClear()
        {
            ddlEnrollmentStatus.SelectedIndex = 0;
            txtComments.Text = "";
            txtFirstName.Text = "";
            txtLastName.Text = "";
            rdpDOB.SelectedDate = null;
            rdpEnrollmentDate.SelectedDate = null;

            ddlGender.SelectedValue = "false";
            ViewState["SchoolProgramId"] = null;
            ViewState["ChildEnrollmentId"] = null;
            ViewState["ChildDataId"] = null;
            ViewState["SelectedFeesPeriodId"] = null;
            ViewState["SelectedFee"] = null;
            ddlProg.SelectedIndex = 0;
            txtFee.Text = "";
            imgStaff.ImageUrl = "../ChildImages/boy.png";
            lblImage.Text = "";
            chkActive.Checked = true;
            rdpStartDate.SelectedDate = null;
            rdpEndDate.SelectedDate = null;
            hdSchoolProgram.Value = "";

            BindFeesPeriod(new Guid(ddlProg.SelectedValue));

            Common.BindProgChildClassRoom(ddlChildProgClassRoom, new Guid(ddlProg.SelectedValue));
            Common.BindProgChildClassRoom(ddlChildProgClassRoom1, new Guid(ddlProg.SelectedValue));
            Common.BindProgChildClassRoom(ddlChildProgClassRoom2, new Guid(ddlProg.SelectedValue));
            Common.BindProgChildClassRoom(ddlChildProgClassRoom3, new Guid(ddlProg.SelectedValue));
            Common.BindProgChildClassRoom(ddlChildProgClassRoom4, new Guid(ddlProg.SelectedValue));
        }

        public void BindFeesPeriod(Guid SchoolProgramId)
        {
            try
            {
                DayCareBAL.AddEditChildService proxyAddEditChild = new DayCareBAL.AddEditChildService();
                ddlFeesPeriod.Items.Clear();
                List<DayCarePL.FeesPeriodProperties> lstFessPeriod = proxyAddEditChild.GetFessPeriodFromSchoolProgramFeesDetail(SchoolProgramId);
                if (lstFessPeriod != null && lstFessPeriod.Count > 0)
                {
                    ddlFeesPeriod.DataSource = lstFessPeriod;
                    ddlFeesPeriod.DataTextField = "Name";
                    ddlFeesPeriod.DataValueField = "Id";
                    ddlFeesPeriod.DataBind();
                }
                ddlFeesPeriod.Items.Insert(0, new ListItem("--Select--", DayCarePL.Common.GUID_DEFAULT));
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.AddEditChild, "BindFeesPeriod", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
            }
        }

        protected void rgAddEditChid_PreRender(object sender, EventArgs e)
        {
            try
            {
                if (rgAddEditChid.MasterTableView.Items.Count > 0)
                {
                    for (int cnt = 0; cnt < rgAddEditChid.MasterTableView.Items.Count; cnt++)
                    {
                        rgAddEditChid.MasterTableView.Items[cnt].Expanded = false;
                        if (rgAddEditChid.MasterTableView.Items[cnt].ChildItem.NestedTableViews[0].Items.Count > 0)
                        {
                            rgAddEditChid.MasterTableView.Items[cnt].ChildItem.NestedTableViews[0].Items[0].Expanded = false;
                        }
                        else
                        {
                            //rgFamilyPayment.MasterTableView.Items[0].ChildItem.NestedTableViews[0].Items[0].Expanded = false;
                            rgAddEditChid.MasterTableView.Items[cnt].Expanded = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.AddEditChild, "rgAddEditChid_PreRender", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
            }
        }

        public void SetMenuLink()
        {
            try
            {
                List<DayCarePL.MenuLink> lstMenu = new List<DayCarePL.MenuLink>();
                DayCarePL.MenuLink objMenu;
                objMenu = new DayCarePL.MenuLink();
                objMenu.Name = "Family: " + Common.GetFamilyName(new Guid(Request.QueryString["ChildFamilyId"]));
                if (Session["ChildFamilyUrl"] != null)
                    objMenu.Url = Convert.ToString(Session["ChildFamilyUrl"]);
                else
                    objMenu.Url = "";
                lstMenu.Add(objMenu);

                objMenu = new DayCarePL.MenuLink();
                objMenu.Name = "Child";
                objMenu.Url = "";
                lstMenu.Add(objMenu);
                usrMenuLink.SetMenuLink(lstMenu);
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.AddEditChild, "SetMenuLink:ChildFamilyId=" + Request.QueryString["ChildFamilyId"] + " ChildFamilyUrl" + Session["ChildFamilyUrl"], ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
            }
        }
    }
}
