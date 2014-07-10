using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using Telerik.Web.UI;

namespace DayCare.UI.UserControls
{
    public partial class addeditchild : System.Web.UI.UserControl
    {
        RadAjaxManager MasterAjaxManager;
        protected void Page_Load(object sender, EventArgs e)
        {

            //btnSave.Attributes.Add("onclick",string.Format("realPostBack('{0}', ''); return false;", btnSave.UniqueID));
            
            if (!string.IsNullOrEmpty(Request.QueryString["ChildFamilyId"]))
            {
                ViewState["ChildFamilyId"] = Request.QueryString["ChildFamilyId"].ToString();
            }
            if (DataItem != null)
            {
                if (!(DataItem is Telerik.Web.UI.GridInsertionObject))
                {
                    chkActive.Checked = false;
                    txtFirstName.Text = DataBinder.Eval(DataItem, "FirstName") as string;
                    txtLastName.Text = DataBinder.Eval(DataItem, "LastName  ") as string;
                    txtComments.Text = DataBinder.Eval(DataItem, "Comments") as string;
                    if (DataBinder.Eval(DataItem, "DOB") != null)
                    {
                        rdpDOB.SelectedDate = Convert.ToDateTime(DataBinder.Eval(DataItem, "DOB"));
                    }
                    chkActive.Checked = System.Convert.ToBoolean(DataBinder.Eval(DataItem, "Active"));
                    if (System.Convert.ToBoolean(DataBinder.Eval(DataItem, "Gender")) == true)
                    {
                        rdMale.Checked = true;

                    }
                    else
                    {
                        rdFemale.Checked = true;

                    }
                    if (DataBinder.Eval(DataItem, "Photo") != null)
                    {
                        if (!string.IsNullOrEmpty(DataBinder.Eval(DataItem, "Photo").ToString()))
                        {
                            imgStaff.ImageUrl = "~/ChildImages/" + DataBinder.Eval(DataItem, "Photo").ToString();
                            lblImage.Text = DataBinder.Eval(DataItem, "Photo").ToString();
                        }

                    }
                    ViewState["ChildId"] = new System.Guid(DataBinder.Eval(DataItem, "Id").ToString());

                }
                else 
                {
                    chkActive.Checked = true;
                }

            }
        }

        public object DataItem
        {
            get;
            set;
        }


        protected void btnSave_Click(object sender, EventArgs e)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.ChildData, "btnSave_Click", "Submit btnSave_Click called", DayCarePL.Common.GUID_DEFAULT);
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.ChildData, "btnSave_Click", "Debug btnSave_Click ", DayCarePL.Common.GUID_DEFAULT);
                DayCareBAL.ChildDataService proxyChildData = new DayCareBAL.ChildDataService();
                DayCarePL.ChildDataProperties objChildData = new DayCarePL.ChildDataProperties();
                Guid ChildDataId;
                string str = "";

                if (ViewState["ChildId"] != null)
                {
                    objChildData.Id = new Guid(ViewState["ChildId"].ToString());
                }
                if (fupImage.UploadedFiles.Count > 0)
                {
                    string Extention = Path.GetExtension(fupImage.UploadedFiles[0].FileName).ToLower();
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
                    objChildData.ChildSchoolYearId = new Guid(Session["CurrentSchoolYearId"].ToString());
                    if (Session["StaffId"] != null)
                    {
                        objChildData.LastModifiedById = new Guid(Session["StaffId"].ToString());
                        objChildData.CreatedById = new Guid(Session["StaffId"].ToString());
                    }
                }
                else
                {

                }
                objChildData.FirstName = txtFirstName.Text.Trim();
                objChildData.LastName = txtLastName.Text.Trim();
                if (rdMale.Checked == true)
                {
                    objChildData.Gender = true;
                }
                else
                {
                    objChildData.Gender = false;
                }
                objChildData.DOB = Convert.ToDateTime(rdpDOB.SelectedDate.ToString());
                objChildData.SocSec = txtSocSec.Text.Trim();
                if (fupImage.UploadedFiles.Count > 0)
                {
                    objChildData.Photo = Path.GetExtension(fupImage.UploadedFiles[0].FileName);
                }
                else
                {
                    if (!string.IsNullOrEmpty(lblImage.Text))
                    {
                        objChildData.Photo = Path.GetExtension(lblImage.Text);
                    }
                    else
                    {
                        objChildData.Photo = string.Empty;
                    }
                }
                objChildData.Comments = txtComments.Text.Trim();

                if (chkActive.Checked == true)
                {
                    objChildData.Active = true;
                }
                else
                {
                    objChildData.Active = false;
                }
                //if (proxyChildData.Save(objChildData))
                ChildDataId = proxyChildData.Save(objChildData);
                if (!ChildDataId.ToString().Equals(DayCarePL.Common.GUID_DEFAULT))
                {
                    if (fupImage.UploadedFiles.Count > 0)
                    {
                        string strFile = Server.MapPath("~/ChildImages/" + ChildDataId + Path.GetExtension(fupImage.UploadedFiles[0].FileName));
                        if (System.IO.File.Exists(strFile))
                        {
                            System.IO.File.SetAttributes(strFile, FileAttributes.Normal);
                            System.IO.File.Delete(strFile);
                        }
                        fupImage.UploadedFiles[0].SaveAs(Server.MapPath("~/ChildImages/" + ChildDataId + Path.GetExtension(fupImage.UploadedFiles[0].FileName)));

                    }

                    txtFirstName.Text = "";
                    txtLastName.Text = "";
                    txtComments.Text = "";
                    txtSocSec.Text = "";
                    rdpDOB.SelectedDate = null;
                    MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                    MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Saved Successfully", "false"));
                    Session["IsValid"] = true;
                }
                else
                {
                    MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                    MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Internal Error,Please try again.", "false"));
                }

            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.ChildData, "btnSave_Click", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Internal Error,Please try again.", "false"));
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtComments.Text = "";
            txtSocSec.Text = "";
            rdpDOB.SelectedDate = null;
            chkActive.Checked = false;
        }

    }
}