using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using Telerik.Web.UI;
using System.Web.UI.WebControls;

namespace DayCare.UI
{
    public partial class MyAccount : System.Web.UI.Page
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
                if (Session["StaffId"] != null)
                {
                    Guid StaffId;
                    StaffId = new Guid(Session["StaffId"].ToString());
                    loadStaffDetails(StaffId);
                }
            }

        }

        public void loadStaffDetails(Guid StaffId)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.Staff, "SubmitRecord", "Submit record method called", DayCarePL.Common.GUID_DEFAULT);
            try
            {

                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.ChildData, "SubmitRecord", "Debug Submit Record Of MyAccountDetails", DayCarePL.Common.GUID_DEFAULT);
                DayCareBAL.MyAccountService proxyMyAccount = new DayCareBAL.MyAccountService();

                if (Session["StaffId"] != null)
                {

                    StaffId = new Guid(Session["StaffId"].ToString());
                }
                DayCarePL.StaffProperties objstaff = proxyMyAccount.LoadMyAccountDetails(StaffId);
                if (objstaff != null)
                {
                    txtFirstName.Text = objstaff.FirstName;
                    txtLastName.Text = objstaff.LastName;
                    txtUserName.Text = objstaff.UserName;
                    //txtPassword.Text = objstaff.Password;
                    // txtOldPassword .Text= objstaff.Password;
                    //txtCode.Text = objstaff.Code;
                    hdnPassword.Value = objstaff.Password;
                }
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.Staff, "LoadMyAccountData", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
            }
        }

        public void btnSave_Click(object sender, EventArgs e)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.Staff, "btnSave_Click", "Submit btnSave_Click called", DayCarePL.Common.GUID_DEFAULT);

            try
            {

                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.Staff, "btnSave_Click", "Debug btnSave_Click ", DayCarePL.Common.GUID_DEFAULT);
                DayCareBAL.MyAccountService proxyMyAccount = new DayCareBAL.MyAccountService();
                DayCareBAL.StaffService proxystaff = new DayCareBAL.StaffService();
                DayCarePL.StaffProperties objStaff = new DayCarePL.StaffProperties();

                objStaff.FirstName = txtFirstName.Text.Trim();
                objStaff.LastName = txtLastName.Text.Trim();
                objStaff.UserName = txtUserName.Text.Trim();
                objStaff.Password = txtPassword.Text.Trim();

                if (Session["StaffId"] != null)
                {

                    objStaff.Id = new Guid(Session["StaffId"].ToString());
                }
                //Check Duplicate Code
                if (hdnPassword.Value == txtOldPassword.Text.Trim())
                {
                    // bool check = proxystaff.CheckDuplicateCode("", objStaff.Id, new Guid(Session["SchoolId"].ToString()));
                    bool check = false;
                    if (check == false)
                    {
                        proxyMyAccount.Save(objStaff);

                        txtFirstName.Text = "";
                        txtLastName.Text = "";
                        txtPassword.Text = "";
                        txtNewPassword.Text = "";


                        MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                        MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", " Saved Successfully", "false"));
                    }
                    else
                    {
                        MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                        MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", " Code Already Exist", "false"));
                    }

                }
                else
                {
                    MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                    MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Old Password Not Match", "false"));
                }
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.Staff, "btnSave_Click", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);

            }

        }
        public void btnCancel_Click(object sender, EventArgs e)
        {
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtPassword.Text = "";
            txtNewPassword.Text = "";

        }

    }
}
