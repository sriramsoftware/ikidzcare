using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using Telerik.Web.UI;

namespace DayCare.UI
{
    public partial class SchoolInfo : System.Web.UI.Page
    {
        RadAjaxManager MasterAjaxManager;
        Guid SchoolId = new Guid();
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
                    Common.BindCountryDropDown(ddlCountry);
                    ddlState.Items.Insert(0, new ListItem("--Select--", "00000000-0000-0000-0000-000000000000"));
                    LoadSchoolInfo(SchoolId);
                }
                catch (Exception ex)
                {

                }
            }
            this.Form.DefaultButton = btnSave.UniqueID;
        }

        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            Common.BindStateDropDown(ddlState, ddlCountry.SelectedValue);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.School, "btnSave_Click", "btnSave_Click called", DayCarePL.Common.GUID_DEFAULT);
            bool result = false;
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.School, "btnSave_Click", "Debug btnSave_Click called", DayCarePL.Common.GUID_DEFAULT);
                DayCareBAL.SchoolService proxySchoolService = new DayCareBAL.SchoolService();
                DayCarePL.SchoolProperties objSchool = new DayCarePL.SchoolProperties();

                if (fupiPadBackgroundImage.HasFile)
                {
                    string Extention = Path.GetExtension(fupiPadBackgroundImage.FileName).ToLower();
                    string[] Ext = { ".jpeg", ".jpg", ".png" };
                    if (Ext.ToList().FindAll(et => et.Equals(Extention)).Count == 0)
                    {
                        MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                        MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Please select .JPEG,.PNG file!", "false"));
                        return;
                    }
                }
                if (Session["SchoolId"] != null)
                {
                    objSchool.Id = new Guid(Session["SchoolId"].ToString());
                    objSchool.Name = txtName.Text;
                    objSchool.Address1 = txtAddress1.Text;
                    objSchool.Address2 = txtAddress2.Text;
                    objSchool.City = txtCity.Text;
                    objSchool.Zip = txtZip.Text;
                    objSchool.CountryId = new Guid(ddlCountry.SelectedValue);
                    objSchool.StateId = new Guid(ddlState.SelectedValue);
                    objSchool.MainPhone = txtMainPhone.Text;
                    objSchool.SecondaryPhone = txtSecondaryPhone.Text;
                    objSchool.Fax = txtFax.Text;
                    objSchool.Email = txtEmail.Text;
                    objSchool.WebSite = txtWebSite.Text;
                    objSchool.CodeRequired = chkCodeRequire.Checked;
                    if (!string.IsNullOrEmpty(txtLateFee.Text.Trim()))
                    {
                        objSchool.LateFeeAmount = Convert.ToDecimal(txtLateFee.Text.Trim());
                    }
                    else
                    {
                        objSchool.LateFeeAmount = 0;
                    }
                    objSchool.iPadHeader = txtiPadHeader.Text;
                    if (ddliPadHeaderFont.SelectedValue != "-1")
                    {
                        objSchool.iPadHeaderFont = ddliPadHeaderFont.SelectedItem.Text;
                    }
                    if (ddliPadHeaderFontSize.SelectedValue != "-1")
                    {
                        objSchool.iPadHeaderFontSize = Convert.ToInt16(ddliPadHeaderFontSize.SelectedItem.Text);
                    }
                    if (rcpiPadHeaderColor.SelectedColor.Name != "0")
                    {
                        objSchool.iPadHeaderColor = "#" + rcpiPadHeaderColor.SelectedColor.Name;
                    }
                    objSchool.iPadMessage = txtiPadMessage.Text;
                    if (ddliPadMessageFont.SelectedValue != "-1")
                    {
                        objSchool.iPadMessageFont = ddliPadMessageFont.SelectedItem.Text;
                    }
                    if (ddliPadMessageFontSize.SelectedValue != "-1")
                    {
                        objSchool.iPadMessageFontSize = Convert.ToInt16(ddliPadMessageFontSize.SelectedItem.Text);
                    }
                    if (fupiPadBackgroundImage.HasFile)
                    {
                        objSchool.iPadBackgroundImage = fupiPadBackgroundImage.FileName;
                    }
                    else
                    {
                        objSchool.iPadBackgroundImage = lbliPadBackgroundImage.Text;
                    }
                    if (rcpiPadMessageColor.SelectedColor.Name != "0")
                    {
                        objSchool.iPadMessageColor = "#" + rcpiPadMessageColor.SelectedColor.Name;
                    }
                    if (Session["StaffId"] != null)
                    {
                        objSchool.CreatedById = new Guid(Session["StaffId"].ToString());
                        objSchool.LastModifiedById = new Guid(Session["StaffId"].ToString());
                    }
                    result = proxySchoolService.Save(objSchool);
                    if (result)
                    {
                        if (fupiPadBackgroundImage.HasFile)
                        {
                            fupiPadBackgroundImage.SaveAs(Server.MapPath("~/StaffImages/" + objSchool.Id + Path.GetExtension(fupiPadBackgroundImage.FileName)));
                        }
                        LoadSchoolInfo(objSchool.Id);
                        MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                        MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Saved Successfully", "false"));
                        return;
                    }
                    else
                    {
                        MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                        MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Internal Error,Please try again.", "false"));
                        return;
                    }
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
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.School, "btnSave_Click", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Internal Error,Please try again.", "false"));
                return;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("StaffList.aspx", false);
            return;
        }

        public void LoadSchoolInfo(Guid SchoolId)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.School, "LoadSchoolInfo", "LoadSchoolInfo called", DayCarePL.Common.GUID_DEFAULT);
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.School, "LoadSchoolInfo", "Debug LoadSchoolInfo Of Role", DayCarePL.Common.GUID_DEFAULT);
                DayCareBAL.SchoolService proxySchoolService = new DayCareBAL.SchoolService();
                DayCarePL.SchoolProperties objSchool = proxySchoolService.LoadSchoolInfo(SchoolId);
                DayCareBAL.FontService proxyFontService = new DayCareBAL.FontService();
                DayCarePL.FontProperties[] lstFont = proxyFontService.LoadFont();

                if (objSchool != null)
                {
                    if (!string.IsNullOrEmpty(objSchool.iPadBackgroundImage))
                    {
                        imgSchholImage.ImageUrl = "../StaffImages/" + objSchool.iPadBackgroundImage;
                    }
                    else
                    {
                        imgSchholImage.ImageUrl = "../StaffImages/Filetype-Blank-Alt-icon.png";
                    }
                    txtName.Text = objSchool.Name;
                    txtAddress1.Text = objSchool.Address1;
                    txtAddress2.Text = objSchool.Address2;
                    txtCity.Text = objSchool.City;
                    txtZip.Text = objSchool.Zip;
                    ddlCountry.SelectedValue = objSchool.CountryId.ToString();
                    if (!objSchool.CountryId.ToString().Equals(DayCarePL.Common.GUID_DEFAULT))
                    {
                        Common.BindStateDropDown(ddlState, objSchool.CountryId.ToString());
                    }
                    if (ddlState.Items != null && ddlState.Items.Count > 0)
                    {
                        ddlState.SelectedValue = objSchool.StateId.ToString();
                    }
                    txtMainPhone.Text = objSchool.MainPhone;
                    txtSecondaryPhone.Text = objSchool.SecondaryPhone;
                    txtFax.Text = objSchool.Fax;
                    txtEmail.Text = objSchool.Email;
                    txtWebSite.Text = objSchool.WebSite;
                    chkCodeRequire.Checked = objSchool.CodeRequired;
                    if (objSchool.LateFeeAmount != null)
                    {
                        txtLateFee.Text = objSchool.LateFeeAmount.ToString();
                    }
                    
                    txtiPadHeader.Text = objSchool.iPadHeader;
                    //txtiPadHeaderFont.Text = objSchool.iPadHeaderFont;
                    //txtiPadHeaderFontSize.Text = objSchool.iPadHeaderFontSize.HasValue ? objSchool.iPadHeaderFontSize.ToString() : "0";
                    lbliPadHeaderColor.BackColor = System.Drawing.ColorTranslator.FromHtml(objSchool.iPadHeaderColor);
                    rcpiPadHeaderColor.SelectedColor = System.Drawing.ColorTranslator.FromHtml(objSchool.iPadHeaderColor);
                    txtiPadMessage.Text = objSchool.iPadMessage;
                    //txtiPadMessageFont.Text = objSchool.iPadMessageFont;
                    //txtiPadMessageFontSize.Text = objSchool.iPadMessageFontSize.HasValue ? objSchool.iPadMessageFontSize.ToString() : "0"; ;
                    lbliPadBackgroundImage.Text = objSchool.iPadBackgroundImage;
                    lbliPadMessageColor.BackColor = System.Drawing.ColorTranslator.FromHtml(objSchool.iPadMessageColor);
                    rcpiPadMessageColor.SelectedColor = System.Drawing.ColorTranslator.FromHtml(objSchool.iPadMessageColor);
                    if (lstFont != null && lstFont.Count() > 0)
                    {
                        ddliPadHeaderFont.Items.Clear();
                        ddliPadMessageFont.Items.Clear();
                        foreach (DayCarePL.FontProperties objFont in lstFont)
                        {

                            ddliPadHeaderFont.Items.Add(new ListItem(objFont.Name, objFont.Name));
                            ddliPadMessageFont.Items.Add(new ListItem(objFont.Name, objFont.Name));
                        }
                        ddliPadHeaderFont.Items.Insert(0, new ListItem("--Select--", "-1"));
                        ddliPadMessageFont.Items.Insert(0, new ListItem("--Select--", "-1"));
                    }
                    if (!string.IsNullOrEmpty(objSchool.iPadHeaderFont))
                    {
                        ddliPadHeaderFont.SelectedValue = objSchool.iPadHeaderFont;
                    }
                    else
                    {
                        ddliPadHeaderFont.SelectedIndex = 0;
                    }

                    if (objSchool.iPadHeaderFontSize != null)
                    {
                        ddliPadHeaderFontSize.SelectedValue = objSchool.iPadHeaderFontSize.ToString();
                    }
                    else
                    {
                        ddliPadHeaderFontSize.SelectedIndex = 0;
                    }
                    if (!string.IsNullOrEmpty(objSchool.iPadMessageFont))
                    {
                        ddliPadMessageFont.SelectedValue = objSchool.iPadMessageFont;
                    }
                    else
                    {
                        ddliPadMessageFont.SelectedIndex = 0;
                    }
                    if (objSchool.iPadMessageFontSize != null)
                    {
                        ddliPadMessageFontSize.SelectedValue = objSchool.iPadMessageFontSize.ToString();
                    }
                    else
                    {
                        ddliPadMessageFontSize.SelectedIndex = 0;
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
