using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.IO;
using Telerik.Web.UI;
using System.Web.UI.WebControls;

namespace DayCare.UI
{
    public partial class ChildData : System.Web.UI.Page
    {
        RadAjaxManager MasterAjaxManager;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["SchoolId"] == null || Session["CurrentSchoolYearId"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            try
            {
                if (!string.IsNullOrEmpty(Request.QueryString["ChildFamilyId"]))
                {
                    ViewState["ChildFamilyId"] = new Guid(Request.QueryString["ChildFamilyId"].ToString());
                    Session["ChildFamilyId"] = new Guid(Request.QueryString["ChildFamilyId"].ToString());
                    ViewState["SchoolYearId"] = new Guid(Session["CurrentSchoolYearId"].ToString());
                    ViewState["SchoolId"] = new Guid(Session["SchoolId"].ToString());
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
                if (!IsPostBack)
                {
                    SetMenuLink();
                }
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.ChildData, "Page_Load", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
            }
            this.Form.DefaultButton = btnSave.UniqueID;
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
                List<DayCarePL.ChildDataProperties> lstChildData = proxyChildData.LoadChildData(new Guid(Session["SchoolId"].ToString()), new Guid(Session["CurrentSchoolYearId"].ToString()), new Guid(ViewState["ChildFamilyId"].ToString()));

                if (lstChildData != null)
                {
                    rgChildData.DataSource = proxyChildData.LoadChildData(new Guid(Session["SchoolId"].ToString()), new Guid(Session["CurrentSchoolYearId"].ToString()), new Guid(ViewState["ChildFamilyId"].ToString()));
                }

                // rgChildData.DataSource = proxyChildData.LoadChildData(new Guid(ViewState["SchoolId"].ToString()), new Guid(ViewState["SchoolYearId"].ToString()));
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.ChildData, "rgChildData_NeedDataSource", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
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
            }
            else
            {
                //string ChildName = "";
                //GridDataItem itm = e.Item as GridDataItem;
                //if (itm != null)
                //{
                //    ChildName = itm["FullName"].Text;
                //}
                if (e.CommandName == "Edit")
                {
                    //if (!string.IsNullOrEmpty(ChildName))
                    //{
                    //    Session["ChildName"] = ": " + ChildName;
                    //}
                    Guid ChildDataID = new Guid(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Id"].ToString());
                    if (ChildDataID != null)
                    {
                        ViewState["ChildId"] = ChildDataID;
                    }
                    LoadDataById(ChildDataID, new Guid(ViewState["SchoolId"].ToString()), new Guid(ViewState["SchoolYearId"].ToString()));
                    e.Canceled = true;
                }
            }
        }

        protected void rgChildData_EditCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            e.Canceled = true;
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
                // hlChildSchedule.NavigateUrl = "ChildSchedule.aspx?Id=" + objChildData.Id;
                hlChildSchedule.NavigateUrl = "ChildProgEnrollment.aspx?ChildDataId=" + objChildData.Id;
            }
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
                if (ViewState["ChildId"] != null)
                {
                    objChildData.Id = new Guid(ViewState["ChildId"].ToString());
                }
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
                if (fupImage.HasFile)
                {
                    objChildData.Photo = Path.GetExtension(fupImage.FileName);
                }
                else
                {
                    objChildData.Photo = string.Empty;
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
                    if (fupImage.HasFile)
                    {
                        string strFile = Server.MapPath("~/ChildImages/" + ChildDataId + Path.GetExtension(fupImage.FileName));
                        if (System.IO.File.Exists(strFile))
                        {
                            System.IO.File.SetAttributes(strFile, FileAttributes.Normal);
                            System.IO.File.Delete(strFile);
                        }
                        fupImage.SaveAs(Server.MapPath("~/ChildImages/" + ChildDataId + Path.GetExtension(fupImage.FileName)));

                    }
                    rgChildData.MasterTableView.Rebind();
                    txtFirstName.Text = "";
                    txtLastName.Text = "";
                    txtComments.Text = "";
                    txtSocSec.Text = "";
                    rdpDOB.SelectedDate = null;
                    MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                    MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Saved Successfully", "false"));

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

            //rgChildData.MasterTableView.Rebind();

        }

        protected void LoadDataById(Guid ChildDataId, Guid SchoolId, Guid SchoolYearId)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.ChildData, "SubmitRecord", "Submit record method called", DayCarePL.Common.GUID_DEFAULT);
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.ChildData, "SubmitRecord", "Debug Submit Record Of ChildData", DayCarePL.Common.GUID_DEFAULT);
                DayCareBAL.ChildDataService proxyChildData = new DayCareBAL.ChildDataService();
                if (Session["SchoolId"] != null)
                {
                    SchoolId = new Guid(Session["SchoolId"].ToString());
                }

                DayCarePL.ChildDataProperties objChildDataId = proxyChildData.LoadChildDataId(ChildDataId, SchoolId, SchoolYearId);
                if (objChildDataId != null)
                {

                    txtFirstName.Text = objChildDataId.FirstName;
                    txtLastName.Text = objChildDataId.LastName;
                    txtComments.Text = objChildDataId.Comments;
                    txtSocSec.Text = objChildDataId.SocSec;
                    rdpDOB.SelectedDate = objChildDataId.DOB;
                    if (objChildDataId.Gender == true)
                    {
                        rdMale.Checked = true;
                    }
                    if (objChildDataId.Active == true)
                    {
                        chkActive.Checked = true;
                    }

                }
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.ChildData, "LoadChildDataDetails", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
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

        public void SetMenuLink()
        {
            try
            {
                string str = "";
                List<DayCarePL.MenuLink> lstMenu = new List<DayCarePL.MenuLink>();
                DayCarePL.MenuLink objMenu;
                objMenu = new DayCarePL.MenuLink();
                if (Session["ChildFamilyName"] != null)
                {
                    str = Session["ChildFamilyName"].ToString();
                }
                objMenu.Name = "Family" + str;
                objMenu.Url = "~/UI/families.aspx";
                lstMenu.Add(objMenu);
                objMenu = new DayCarePL.MenuLink();
                objMenu.Name = "Child";
                objMenu.Url = "";
                lstMenu.Add(objMenu);
                usrMenuLink.SetMenuLink(lstMenu);
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.ChildData, "SetMenuLink:ChildFamilyName=" + Session["ChildFamilyName"], ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
            }
        }
    }
}
