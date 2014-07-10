using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace DayCare.UI
{
    public partial class ChildSchedule : System.Web.UI.Page
    {
        RadAjaxManager MasterAjaxManager;
        //Guid SchoolId = new Guid();
        //Guid CurrentSchoolYearId = new Guid();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["SchoolId"] == null || Session["CurrentSchoolYearId"] == null || string.IsNullOrEmpty(Request.QueryString["Id"]))
            {
                Response.Redirect("~/Login.aspx");
            }
            Guid SchoolId = GetSchoolId();
            Guid CurrentSchoolYearId = GetCurrentSchoolYearId();
            if (!Common.IsCurrentYear(CurrentSchoolYearId, SchoolId))
            {
                btnSave.Enabled = false;
                btnCancel.Enabled = false;
            }
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["Id"]))// != null && Request.QueryString["Id"]
                {
                    ViewState["ChildDataId"] = Request.QueryString["Id"].ToString();
                }
                GetSchoolProgram(SchoolId, CurrentSchoolYearId);
                //ddlClassCategory.Items.Insert(0, new ListItem("--Select--", DayCarePL.Common.GUID_DEFAULT));
                ddlClassRoom.Items.Insert(0, new ListItem("--Select--", DayCarePL.Common.GUID_DEFAULT));
                ddlProgSchedule.Items.Insert(0, new ListItem("--Select--", DayCarePL.Common.GUID_DEFAULT));

                SetMenuLink();
            }
        }

        protected void rgChildSchedule_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DayCareBAL.ChildScheduleService proxyChildSchedule = new DayCareBAL.ChildScheduleService();
            rgChildSchedule.DataSource = proxyChildSchedule.LoadChildSchedule(GetChildSchoolYearId());
        }

        protected void rgChildSchedule_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
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
                if (e.CommandName == "InitInsert")
                {
                    ClearAll();
                    e.Canceled = true;
                }
                if (e.CommandName == "Edit")
                {
                    if (!string.IsNullOrEmpty(Request.QueryString["Id"]))// != null && Request.QueryString["Id"]
                    {
                        Guid ChildScheduleID = new Guid(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Id"].ToString());
                        if (ChildScheduleID != null)
                        {
                            ViewState["SelectedChildScheduleId"] = ChildScheduleID;
                        }
                        LoadDataById(ChildScheduleID, GetChildSchoolYearId());
                        e.Canceled = true;
                    }
                }
            }

        }

        protected void rgChildSchedule_EditCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            e.Canceled = true;
            rgChildSchedule.MasterTableView.CurrentPageIndex = 0;
        }

        protected void rgChildSchedule_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item.ItemType == GridItemType.AlternatingItem || e.Item.ItemType == GridItemType.Item)
            {
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.ChildSchedule, "btnSave_Click", "btnSave_Click called", DayCarePL.Common.GUID_DEFAULT);
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.ChildSchedule, "btnSave_Click", "Debug btnSave_Click called", DayCarePL.Common.GUID_DEFAULT);
                DayCareBAL.ChildScheduleService proxyChildSchedule = new DayCareBAL.ChildScheduleService();
                DayCarePL.ChildScheduleProperties objChildSchedule = new DayCarePL.ChildScheduleProperties();

                
                //bool IsSelectedProgramPrimary = (lblSchoolProgramId.Text.Equals(ddlProgram.SelectedValue) && lblProgClassCategoryId.Text.Equals(ddlClassCategory.SelectedValue) && lblProgScheduleId.Text.Equals(ddlProgSchedule.SelectedValue));
                bool IsSelectedProgramPrimary = (lblSchoolProgramId.Text.Equals(ddlProgram.SelectedValue) && lblProgScheduleId.Text.Equals(ddlProgSchedule.SelectedValue));
                if (!IsSelectedProgramPrimary)
                {
                    bool IsRoomAvailableForChild = proxyChildSchedule.CheckAvailableClassForChild(GetSchoolId(), new Guid(ddlProgSchedule.SelectedValue), GetChildSchoolYearId(), new Guid(ddlProgram.SelectedValue));
                    if (IsRoomAvailableForChild == false)
                    {
                        MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                        MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Class Room is not availabe", "false"));
                        return;
                    }

                    if (proxyChildSchedule.CheckDupicateChildSchedule(GetChildSchoolYearId(), new Guid(ddlProgram.SelectedValue), new Guid(ddlProgSchedule.SelectedValue)))
                    {// ddlClassCategory.SelectedValue is pass through this function but not check from DB.
                        MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                        MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Child schedule already exists", "false"));
                        return;
                    }
                }
                if (rdpBeginDate.SelectedDate != null && rdpEndDate != null)
                {
                    if (rdpBeginDate.SelectedDate.Value > rdpEndDate.SelectedDate.Value)
                    {
                        MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                        MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Start Date must less than End Date", "false"));
                        return;
                    }
                }
                bool IsPrimaryInDB = proxyChildSchedule.IsPrimaryProgramInChildSchedule(GetChildSchoolYearId());
                bool IsPrimarySchoolProgramInDropDown = proxyChildSchedule.CheckProgramIdPrimaryOrNotForChildSchedule(new Guid(ddlProgram.SelectedValue));
                if (IsPrimaryInDB || IsPrimarySchoolProgramInDropDown)
                {
                    SubmitRecord();
                }
                else
                {
                    MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                    MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Please select Primary subject first", "false"));
                    return;
                }
                #region Old Functinality ( Only one Primary Program and other Secondary)
                //if (IsPrimaryInDB)// || IsPrimarySchoolProgramInDropDown)
                //{
                //    if (IsSelectedProgramPrimary)
                //    {
                //        SubmitRecord();
                //    }
                //    else
                //    {
                //        if (IsPrimarySchoolProgramInDropDown)
                //        {
                //            MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                //            MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Program require only one primary", "false"));
                //            return;
                //        }
                //        else
                //        {
                //            SubmitRecord();
                //        }
                //    }
                //}
                //else
                //{
                //    if (IsPrimarySchoolProgramInDropDown)
                //    {
                //        SubmitRecord();
                //    }
                //    else
                //    {
                //        MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                //        MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Please select Primary subject first", "false"));
                //        return;
                //    }
                //}
                #endregion
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.ChildSchedule, "btnSave_Click", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearAll();
        }

        protected void ddlProgram_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Guid SchoolId = GetSchoolId();
                GetProgClassCategory(SchoolId, new Guid(ddlProgram.SelectedValue));
                GetProgClassRoom(SchoolId, new Guid(ddlProgram.SelectedValue));
                GetProgSchedule(SchoolId, new Guid(ddlProgram.SelectedValue));
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.ChildSchedule, "ddlProgram_SelectedIndexChanged", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
            }
        }

        public void GetSchoolProgram(Guid SchoolId, Guid CurrentSchoolYearId)
        {
            DayCareBAL.SchoolProgramService proxySchoolProgram = new DayCareBAL.SchoolProgramService();
            List<DayCarePL.SchoolProgramProperties> lstSchoolProgram = proxySchoolProgram.LoadSchoolProgramForChildSchedule(SchoolId, CurrentSchoolYearId);
            if (lstSchoolProgram != null && lstSchoolProgram.Count > 0)
            {
                foreach (DayCarePL.SchoolProgramProperties objSchoolProgram in lstSchoolProgram)
                {
                    ddlProgram.Items.Add(new ListItem(objSchoolProgram.Title + " - " + (objSchoolProgram.IsPrimary == true ? "Primary" : "Secendory"), objSchoolProgram.Id.ToString()));
                }
            }
            ddlProgram.Items.Insert(0, new ListItem("--Select--", DayCarePL.Common.GUID_DEFAULT));
        }

        public void GetProgClassCategory(Guid SchoolId, Guid SchoolProgramId)
        {
            //DayCareBAL.ProgClassCategoryService proxyProgClassCategory = new DayCareBAL.ProgClassCategoryService();
            //List<DayCarePL.ProgClassCategoryProperties> lstProgClassCategory = proxyProgClassCategory.LoadProgClassCategoryForChildSchedule(SchoolProgramId, SchoolId);
            //ddlClassCategory.Items.Clear();
            //if (lstProgClassCategory != null && lstProgClassCategory.Count > 0)
            //{
            //    foreach (DayCarePL.ProgClassCategoryProperties objProgClassCategory in lstProgClassCategory)
            //    {
            //        ddlClassCategory.Items.Add(new ListItem(objProgClassCategory.ClassCategoryName, objProgClassCategory.Id.ToString()));
            //    }
            //}
            //ddlClassCategory.Items.Insert(0, new ListItem("--Select--", DayCarePL.Common.GUID_DEFAULT));
        }

        public void GetProgClassRoom(Guid SchoolId, Guid SchoolProgramId)
        {
            DayCareBAL.ProgClassRoomService proxyProgClassRoom = new DayCareBAL.ProgClassRoomService();
            List<DayCarePL.ProgClassRoomProperties> lstProgClassRoom = proxyProgClassRoom.LoadProgClassRoomForChildSchedule(SchoolId, SchoolProgramId);
            ddlClassRoom.Items.Clear();
            if (lstProgClassRoom != null && lstProgClassRoom.Count > 0)
            {
                foreach (DayCarePL.ProgClassRoomProperties objProgClassRoom in lstProgClassRoom)
                {
                    ddlClassRoom.Items.Add(new ListItem(objProgClassRoom.ClassRoomName, objProgClassRoom.Id.ToString()));
                }
            }
            ddlClassRoom.Items.Insert(0, new ListItem("--Select--", DayCarePL.Common.GUID_DEFAULT));
        }

        public void GetProgSchedule(Guid SchoolId, Guid SchoolProgramId)
        {
            DayCareBAL.ProgScheduleService proxyProgClassCategory = new DayCareBAL.ProgScheduleService();
            List<DayCarePL.ProgScheduleProperties> lstProgClassCategory = proxyProgClassCategory.LoadProgScheduleForChildSchedule(SchoolId, SchoolProgramId);
            ddlProgSchedule.Items.Clear();
            if (lstProgClassCategory != null && lstProgClassCategory.Count > 0)
            {
                foreach (DayCarePL.ProgScheduleProperties objProgSchedule in lstProgClassCategory)
                {
                    string strSchedule = objProgSchedule.Day + " - " + (objProgSchedule.BeginTime != null ? Convert.ToDateTime(objProgSchedule.BeginTime).ToShortTimeString() : "") + " - " + (objProgSchedule.EndTime != null ? Convert.ToDateTime(objProgSchedule.EndTime).ToShortTimeString() : "") + " - " + objProgSchedule.ClassRoomName + " - " + objProgSchedule.FullName;
                    ddlProgSchedule.Items.Add(new ListItem(strSchedule, objProgSchedule.Id.ToString()));
                }
            }
            ddlProgSchedule.Items.Insert(0, new ListItem("--Select--", DayCarePL.Common.GUID_DEFAULT));
        }

        protected void LoadDataById(Guid ChildScheduleId, Guid ChildSchoolYearId)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.ChildSchedule, "LoadDataById", "LoadDataById method called", DayCarePL.Common.GUID_DEFAULT);
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.ChildSchedule, "LoadDataById", "Debug LoadDataById called", DayCarePL.Common.GUID_DEFAULT);
                DayCareBAL.ChildScheduleService proxyChildSchedule = new DayCareBAL.ChildScheduleService();

                Guid SchoolId = new Guid(Session["SchoolId"].ToString());
                DayCarePL.ChildScheduleProperties objChildSchedule = proxyChildSchedule.LoadChildScheduleById(ChildScheduleId, ChildSchoolYearId);
                if (objChildSchedule != null)
                {
                    ddlProgram.SelectedValue = objChildSchedule.SchoolProgramId.ToString();
                    lblSchoolProgramId.Text = objChildSchedule.SchoolProgramId.ToString();
                    GetProgClassCategory(SchoolId, new Guid(ddlProgram.SelectedValue));
                    GetProgClassRoom(SchoolId, new Guid(ddlProgram.SelectedValue));
                    GetProgSchedule(SchoolId, new Guid(ddlProgram.SelectedValue));
                    //if (objChildSchedule.ProgClassCategoryId != null)
                    //{
                    //    ddlClassCategory.SelectedValue = objChildSchedule.ProgClassCategoryId.ToString();
                    //    lblProgClassCategoryId.Text = objChildSchedule.ProgClassCategoryId.ToString();
                    //}
                    if (objChildSchedule.ProgClassRoomId != null)
                    {
                        ddlClassRoom.SelectedValue = objChildSchedule.ProgClassRoomId.ToString();
                    }
                    if (objChildSchedule.ProgScheduleId != null)
                    {
                        ddlProgSchedule.SelectedValue = objChildSchedule.ProgScheduleId.ToString();
                        lblProgScheduleId.Text = objChildSchedule.ProgScheduleId.ToString();
                    }
                    if (objChildSchedule.BeginDate != null)
                    {
                        rdpBeginDate.SelectedDate = objChildSchedule.BeginDate;
                    }
                    if (objChildSchedule.EndDate != null)
                    {
                        rdpEndDate.SelectedDate = objChildSchedule.EndDate;
                    }
                    txtDiscount.Text = objChildSchedule.Discount.ToString();
                }
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.ChildSchedule, "LoadDataById", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
            }

        }

        public Guid GetChildSchoolYearId()
        {
            Guid ChildDataId = new Guid();
            if (ViewState["ChildDataId"] != null)
            {
                ChildDataId = new Guid(ViewState["ChildDataId"].ToString());
            }
            return Common.GetChildSchoolYearId(ChildDataId, GetCurrentSchoolYearId());
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

        public bool SubmitRecord()
        {
            bool result = false;
            try
            {
                DayCareBAL.ChildScheduleService proxyChildSchedule = new DayCareBAL.ChildScheduleService();
                DayCarePL.ChildScheduleProperties objChildSchedule = new DayCarePL.ChildScheduleProperties();

                objChildSchedule.ChildSchoolYearId = GetChildSchoolYearId();
                objChildSchedule.SchoolProgramId = new Guid(ddlProgram.SelectedValue);
                //objChildSchedule.ProgClassCategoryId = new Guid(ddlClassCategory.SelectedValue);
                DayCareBAL.ProgScheduleService proxyProgClassCategory = new DayCareBAL.ProgScheduleService();
                List<DayCarePL.ProgScheduleProperties> lstProgClassCategory = proxyProgClassCategory.LoadProgScheduleForChildSchedule(GetSchoolId(), new Guid(ddlProgram.SelectedValue));
                if (lstProgClassCategory != null && lstProgClassCategory.Count > 0)
                {
                    objChildSchedule.ProgClassRoomId = lstProgClassCategory.Find(cr => cr.Id.Equals(new Guid(ddlProgSchedule.SelectedValue))).ProgClassRoomId;
                }
                objChildSchedule.ProgScheduleId = new Guid(ddlProgSchedule.SelectedValue);
                if (rdpBeginDate.SelectedDate != null)
                {
                    objChildSchedule.BeginDate = rdpBeginDate.SelectedDate.Value;
                }
                if (rdpEndDate.SelectedDate != null)
                {
                    objChildSchedule.EndDate = rdpEndDate.SelectedDate.Value;
                }
                if (!string.IsNullOrEmpty(txtDiscount.Text.Trim()))
                {
                    objChildSchedule.Discount = Convert.ToDouble(txtDiscount.Text.Trim());
                }
                else
                {
                    objChildSchedule.Discount = 0;
                }
                if (ViewState["SelectedChildScheduleId"] != null)
                {
                    objChildSchedule.Id = new Guid(ViewState["SelectedChildScheduleId"].ToString());
                    int CountChildData = proxyChildSchedule.GetCountChildData(GetChildSchoolYearId());
                    if (CountChildData == 1)
                    {
                        if (!proxyChildSchedule.CheckProgramIdPrimaryOrNotForChildSchedule(new Guid(ddlProgram.SelectedValue)))
                        {
                            MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                            MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Please select Primary subject first", "false"));
                            return false;
                        }
                    }
                }
                else
                {
                    if (Session["StaffId"] != null)
                    {
                        objChildSchedule.CreatedById = new Guid(Session["StaffId"].ToString());
                    }
                }
                if (Session["StaffId"] != null)
                {
                    objChildSchedule.LastModifiedById = new Guid(Session["StaffId"].ToString());
                }
                if (proxyChildSchedule.Save(objChildSchedule))
                {
                    ClearAll();
                    rgChildSchedule.MasterTableView.Rebind();
                    MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                    MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Saved Successfully", "false"));
                    rgChildSchedule.MasterTableView.CurrentPageIndex = 0;
                    result = true;
                }
                else
                {
                    MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                    MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Internal Error", "false"));
                    result = false;
                }
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }

        public void ClearAll()
        {
            ViewState["SelectedChildScheduleId"] = null;
            ddlProgram.SelectedValue = DayCarePL.Common.GUID_DEFAULT;
            //ddlClassCategory.SelectedValue = DayCarePL.Common.GUID_DEFAULT;
            ddlClassRoom.SelectedValue = DayCarePL.Common.GUID_DEFAULT;
            ddlProgSchedule.SelectedValue = DayCarePL.Common.GUID_DEFAULT;
            rdpBeginDate.SelectedDate = null;
            rdpEndDate.SelectedDate = null;
            txtDiscount.Text = "";
        }

        public void SetMenuLink()
        {
            try
            {
                List<DayCarePL.MenuLink> lstMenu = new List<DayCarePL.MenuLink>();
                DayCarePL.MenuLink objMenu;
                objMenu = new DayCarePL.MenuLink();
                objMenu.Name = "Child Family";
                objMenu.Url = "~/UI/ChildFamily.aspx";
                lstMenu.Add(objMenu);
                objMenu = new DayCarePL.MenuLink();
                objMenu.Name = "Child Data";
                objMenu.Url = "~/UI/ChildData.aspx?ChildFamilyId=" + new Guid(Session["ChildFamilyId"].ToString());
                lstMenu.Add(objMenu);
                objMenu = new DayCarePL.MenuLink();
                objMenu.Name = "Child Schedule";
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
