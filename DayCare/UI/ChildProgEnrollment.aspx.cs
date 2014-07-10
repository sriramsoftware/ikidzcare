﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using Telerik.Web.UI;
using System.Web.UI.WebControls;

namespace DayCare.UI
{
    public partial class ChildProgEnrollment : System.Web.UI.Page
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
                btnSave.Attributes.Add("onclick", "return ValidationOnformSubmit()");
                Common.BindProgramName(ddlProg, new Guid(Session["CurrentSchoolYearId"].ToString()));
                ddlChildProgClassRoom.Items.Insert(0, "--Select--");
                ddlChildProgClassRoom1.Items.Insert(0, "--Select--");
                ddlChildProgClassRoom2.Items.Insert(0, "--Select--");
                ddlChildProgClassRoom3.Items.Insert(0, "--Select--");
                ddlChildProgClassRoom4.Items.Insert(0, "--Select--");
                //BindEditProgramEnrollment(new Guid(Request.QueryString["ChildDataId"].ToString()), new Guid(Request.QueryString["SchoolProgramId"]));
                SetMenuLink();
            }
        }

        protected void ddlProg_SelectedIndexChanged(object sender, EventArgs e)
        {
            //DropDownList ddlProg = Items["ddlProg"] as DropDownList;
            DayCareBAL.ChildProgEnrollmentService proxyFee = new DayCareBAL.ChildProgEnrollmentService();
            txtFee.Text = Convert.ToString(proxyFee.GetFees(new Guid(ddlProg.SelectedValue)));
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

            ddlChildProgClassRoom.SelectedIndex = 0;
            ddlChildProgClassRoom1.SelectedIndex = 0;
            ddlChildProgClassRoom2.SelectedIndex = 0;
            ddlChildProgClassRoom3.SelectedIndex = 0;
            ddlChildProgClassRoom4.SelectedIndex = 0;


            hdnFri.Value = "";
            hdnMon.Value = "";
            hdnThus.Value = "";
            hdnTue.Value = "";
            hdnWen.Value = "";

            BindEditProgramEnrollment(new Guid(Request.QueryString["ChildDataId"].ToString()), new Guid(ddlProg.SelectedValue.ToString()));
            if (ViewState["SchoolProgramId"] != null)
            {
                if (!ViewState["SchoolProgramId"].ToString().Equals(ddlProg.SelectedValue.ToString()))
                {

                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.ChildProgEnrollment, "btnSave_Click", "Submit btnSave_Click called", DayCarePL.Common.GUID_DEFAULT);
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.ChildProgEnrollment, "btnSave_Click", "Debug btnSave_Click ", DayCarePL.Common.GUID_DEFAULT);
                DayCareBAL.ChildProgEnrollmentService proxySave = new DayCareBAL.ChildProgEnrollmentService();
                DayCarePL.ChildProgEnrollmentProperties objChildProgEnrollment = new DayCarePL.ChildProgEnrollmentProperties();
                Guid ChildProgEnrollmentId;

                if (ChkMon.Checked == true)
                {
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
                    objChildProgEnrollment.ChildSchoolYearId = Common.GetChildSchoolYearId(new Guid(Request.QueryString["ChildDataId"]), new Guid(Session["CurrentSchoolYearId"].ToString()));

                    if (Session["StaffId"] != null)
                    {
                        objChildProgEnrollment.CreatedById = new Guid(Session["StaffId"].ToString());
                        objChildProgEnrollment.LastModifiedById = new Guid(Session["StaffId"].ToString());
                    }
                    ChildProgEnrollmentId = proxySave.Save(objChildProgEnrollment);
                    if (!ChildProgEnrollmentId.ToString().Equals(DayCarePL.Common.GUID_DEFAULT))
                    {
                        hdnMon.Value = Convert.ToString(ChildProgEnrollmentId);
                    }
                    if (ChildProgEnrollmentId.ToString().Equals("11111111-1111-1111-1111-111111111111"))
                    {
                        MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                        MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Child must have to enrolled a Primary program", "false"));
                    }
                }
                else
                {
                    if (hdnMon.Value.Length == 36)
                    {
                        int result = proxySave.Delete(new Guid(hdnMon.Value));
                    }
                }
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
                    objChildProgEnrollment.ChildSchoolYearId = Common.GetChildSchoolYearId(new Guid(Request.QueryString["ChildDataId"]), new Guid(Session["CurrentSchoolYearId"].ToString()));
                    if (Session["StaffId"] != null)
                    {
                        objChildProgEnrollment.CreatedById = new Guid(Session["StaffId"].ToString());
                        objChildProgEnrollment.LastModifiedById = new Guid(Session["StaffId"].ToString());
                    }
                    ChildProgEnrollmentId = proxySave.Save(objChildProgEnrollment);
                    if (!ChildProgEnrollmentId.ToString().Equals(DayCarePL.Common.GUID_DEFAULT))
                    {
                        hdnTue.Value = Convert.ToString(ChildProgEnrollmentId);
                    }
                    if (ChildProgEnrollmentId.ToString().Equals("11111111-1111-1111-1111-111111111111"))
                    {
                        MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                        MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Child must have to enrolled a Primary program", "false"));
                    }
                }
                else
                {
                    if (hdnTue.Value.Length == 36)
                    {
                        int result = proxySave.Delete(new Guid(hdnTue.Value));
                    }
                }
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
                    objChildProgEnrollment.ChildSchoolYearId = Common.GetChildSchoolYearId(new Guid(Request.QueryString["ChildDataId"]), new Guid(Session["CurrentSchoolYearId"].ToString()));
                    if (Session["StaffId"] != null)
                    {
                        objChildProgEnrollment.CreatedById = new Guid(Session["StaffId"].ToString());
                        objChildProgEnrollment.LastModifiedById = new Guid(Session["StaffId"].ToString());
                    }
                    ChildProgEnrollmentId = proxySave.Save(objChildProgEnrollment);
                    if (!ChildProgEnrollmentId.ToString().Equals(DayCarePL.Common.GUID_DEFAULT))
                    {
                        hdnWen.Value = Convert.ToString(ChildProgEnrollmentId);
                    }
                    if (ChildProgEnrollmentId.ToString().Equals("11111111-1111-1111-1111-111111111111"))
                    {
                        MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                        MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Child must have to enrolled a Primary program", "false"));
                    }
                }
                else
                {
                    if (hdnWen.Value.Length == 36)
                    {
                        int result = proxySave.Delete(new Guid(hdnWen.Value));
                    }
                }
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
                    objChildProgEnrollment.ChildSchoolYearId = Common.GetChildSchoolYearId(new Guid(Request.QueryString["ChildDataId"]), new Guid(Session["CurrentSchoolYearId"].ToString()));
                    if (Session["StaffId"] != null)
                    {
                        objChildProgEnrollment.CreatedById = new Guid(Session["StaffId"].ToString());
                        objChildProgEnrollment.LastModifiedById = new Guid(Session["StaffId"].ToString());
                    }
                    ChildProgEnrollmentId = proxySave.Save(objChildProgEnrollment);
                    if (!ChildProgEnrollmentId.ToString().Equals(DayCarePL.Common.GUID_DEFAULT))
                    {
                        hdnThus.Value = Convert.ToString(ChildProgEnrollmentId);
                    }
                    if (ChildProgEnrollmentId.ToString().Equals("11111111-1111-1111-1111-111111111111"))
                    {
                        MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                        MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Child must have to enrolled a Primary program", "false"));
                    }
                }
                else
                {
                    if (hdnThus.Value.Length == 36)
                    {
                        int result = proxySave.Delete(new Guid(hdnThus.Value));
                    }
                }
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
                    objChildProgEnrollment.ChildSchoolYearId = Common.GetChildSchoolYearId(new Guid(Request.QueryString["ChildDataId"]), new Guid(Session["CurrentSchoolYearId"].ToString()));
                    if (Session["StaffId"] != null)
                    {
                        objChildProgEnrollment.CreatedById = new Guid(Session["StaffId"].ToString());
                        objChildProgEnrollment.LastModifiedById = new Guid(Session["StaffId"].ToString());
                    }
                    ChildProgEnrollmentId = proxySave.Save(objChildProgEnrollment);
                    if (!ChildProgEnrollmentId.ToString().Equals(DayCarePL.Common.GUID_DEFAULT))
                    {
                        hdnFri.Value = Convert.ToString(ChildProgEnrollmentId);
                    }
                    if (ChildProgEnrollmentId.ToString().Equals("11111111-1111-1111-1111-111111111111"))
                    {
                        MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                        MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Child must have to enrolled a Primary program", "false"));
                    }
                }
                else
                {
                    if (hdnFri.Value.Length == 36)
                    {
                        int result = proxySave.Delete(new Guid(hdnFri.Value));
                    }
                }
                rgProgramEnrollment.Rebind();
                ddlProg.SelectedIndex = 0;
                txtFee.Text = "";
                ClearFields();
                //if (!ChildProgEnrollmentId.ToString().Equals("11111111-1111-1111-1111-111111111111"))
                //{
                //    MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                //    MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Saved Successfully", "false"));
                //}
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.ChildProgEnrollment, "btnSave_Click", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                MasterAjaxManager = this.Page.Master.FindControl("RadAjaxManager1") as Telerik.Web.UI.RadAjaxManager;
                MasterAjaxManager.ResponseScripts.Add(string.Format("ShowMessage('{0}','{1}')", "Internal Error,Please try again.", "false"));
            }

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearFields();
            ddlProg.SelectedIndex = 0;
            txtFee.Text = "";
        }

        public void ClearFields()
        {

            ChkMon.Checked = false;
            ChkTue.Checked = false;
            ChkWen.Checked = false;
            ChkThus.Checked = false;
            ChkFri.Checked = false;
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

        protected void ConditionFuction()
        {
            DayCarePL.ChildProgEnrollmentProperties objChildProgEnrollment = new DayCarePL.ChildProgEnrollmentProperties();
            if (ChkMon.Checked == true)
            {
                objChildProgEnrollment.DayIndex = 1;
            }
            else if (ChkTue.Checked == true)
            {
                objChildProgEnrollment.DayIndex = 2;
            }
            else if (ChkWen.Checked == true)
            {
                objChildProgEnrollment.DayIndex = 3;
            }
            else if (ChkThus.Checked == true)
            {
                objChildProgEnrollment.DayIndex = 4;
            }
            else if (ChkFri.Checked == true)
            {
                objChildProgEnrollment.DayIndex = 5;
            }
            else if (ddlDayType1.SelectedItem.ToString() != "--Select--")
            {
                objChildProgEnrollment.DayType = ddlDayType1.SelectedValue;
            }


        }

        public void BindEditProgramEnrollment(Guid ChildDataId, Guid SchoolProgramId)
        {

            if (Session["CurrentSchoolYearId"] == null)
                return;

            Guid ChildSchoolYearId = Common.GetChildSchoolYearId(ChildDataId, new Guid(Session["CurrentSchoolYearId"].ToString()));

            DayCareBAL.ChildProgEnrollmentService proxy = new DayCareBAL.ChildProgEnrollmentService();
            List<DayCarePL.ChildProgEnrollmentProperties> lstChilProgEnrollment = proxy.LoadProgEnrollment(ChildSchoolYearId, SchoolProgramId);

            if (lstChilProgEnrollment.Count > 0)
            {

                ddlProg.SelectedValue = SchoolProgramId.ToString();

                //Classroom and fees bind as per selected "Program"
                DayCareBAL.ChildProgEnrollmentService proxyFee = new DayCareBAL.ChildProgEnrollmentService();
                txtFee.Text = Convert.ToString(proxyFee.GetFees(new Guid(ddlProg.SelectedValue)));
                Common.BindProgChildClassRoom(ddlChildProgClassRoom, new Guid(ddlProg.SelectedValue));
                Common.BindProgChildClassRoom(ddlChildProgClassRoom1, new Guid(ddlProg.SelectedValue));
                Common.BindProgChildClassRoom(ddlChildProgClassRoom2, new Guid(ddlProg.SelectedValue));
                Common.BindProgChildClassRoom(ddlChildProgClassRoom3, new Guid(ddlProg.SelectedValue));
                Common.BindProgChildClassRoom(ddlChildProgClassRoom4, new Guid(ddlProg.SelectedValue));
                //end bing fee and classroom

                ClearFields();
                foreach (DayCarePL.ChildProgEnrollmentProperties objChildProgEnrollment in lstChilProgEnrollment)
                {
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

            }
        }

        public void rgProgramEnrollment_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (!e.IsFromDetailTable)
                {
                    List<DayCarePL.ChildProgEnrollmentProperties> lstChildProgEnrollment = new List<DayCarePL.ChildProgEnrollmentProperties>();
                    lstChildProgEnrollment = GetChildProgEnrollment();
                    if (lstChildProgEnrollment != null && lstChildProgEnrollment.Count > 0)
                    {
                        rgProgramEnrollment.DataSource = lstChildProgEnrollment.GroupBy(i => i.ProgramTitle).Select(g => g.First()).ToList().OrderBy(i => i.ProgramTitle);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void rgProgramEnrollment_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            ClearFields();
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
                    btnSave.Enabled = false;
                    btnCancel.Enabled = false;
                }
            }
            else
            {
                if (e.CommandName == "Edit")
                {

                    GridEditableItem dataItem = (GridEditableItem)e.Item;
                    Guid SchoolProgramId = new Guid(dataItem["SchoolProgramId"].Text);
                    BindEditProgramEnrollment(new Guid(Request.QueryString["ChildDataId"].ToString()), SchoolProgramId);
                    ViewState["SchoolProgramId"] = SchoolProgramId;
                    e.Canceled = true;

                }
            }
        }

        protected void rgProgramEnrollment_EditCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            GridDataItem dataItem = (GridDataItem)e.Item.DataItem;
            Guid SchoolProgramId = new Guid(dataItem["SchoolProgramId"].Text);
            e.Canceled = true;
        }

        protected void rgProgramEnrollment_DetailTableDataBind(object sender, Telerik.Web.UI.GridDetailTableDataBindEventArgs e)
        {
            try
            {
                GridDataItem dataItem = (GridDataItem)e.DetailTableView.ParentItem;
                List<DayCarePL.ChildProgEnrollmentProperties> lstChildProgEnrollment = new List<DayCarePL.ChildProgEnrollmentProperties>();
                lstChildProgEnrollment = GetChildProgEnrollment();
                if (lstChildProgEnrollment != null && lstChildProgEnrollment.Count > 0)
                {
                    lstChildProgEnrollment = lstChildProgEnrollment.FindAll(d => d.SchoolProgramId == new Guid(dataItem.GetDataKeyValue("SchoolProgramId").ToString()));
                    if (lstChildProgEnrollment != null)
                    {
                        e.DetailTableView.DataSource = lstChildProgEnrollment.OrderBy(i => i.DayIndex);
                    }

                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void rgProgramEnrollment_PreRender(object sender, EventArgs e)
        {
            if (rgProgramEnrollment.MasterTableView.Items.Count > 0)
            {
                for (int cnt = 0; cnt < rgProgramEnrollment.MasterTableView.Items.Count; cnt++)
                {
                    rgProgramEnrollment.MasterTableView.Items[cnt].Expanded = true;
                }
            }
        }


        public List<DayCarePL.ChildProgEnrollmentProperties> GetChildProgEnrollment()
        {
            DayCareBAL.ChildProgEnrollmentService proxy = new DayCareBAL.ChildProgEnrollmentService();
            Guid ChildSchoolYearId = new Guid(DayCarePL.Common.GUID_DEFAULT);

            if (String.IsNullOrEmpty(Request.QueryString["ChildDataId"]) || Session["CurrentSchoolYearId"] == null)
                return null;
            ChildSchoolYearId = Common.GetChildSchoolYearId(new Guid(Request.QueryString["ChildDataId"].ToString()), new Guid(Session["CurrentSchoolYearId"].ToString()));
            return proxy.LoadAllProgEnrolled(ChildSchoolYearId);
        }

        public void SetMenuLink()
        {
            try
            {
                string str = "";
                List<DayCarePL.MenuLink> lstMenu = new List<DayCarePL.MenuLink>();
                DayCarePL.MenuLink objMenu;
                if (Session["ChildFamilyName"] != null)
                {
                    objMenu = new DayCarePL.MenuLink();
                    str = Session["ChildFamilyName"].ToString();
                    objMenu.Name = "Family" + str;
                    objMenu.Url = Convert.ToString(Session["ChildFamilyUrl"]);
                    lstMenu.Add(objMenu);
                }
                objMenu = new DayCarePL.MenuLink();
                objMenu.Name = "Child: " + Common.GetChildName(new Guid(Request.QueryString["ChildDataId"].ToString()));
                //objMenu.Url = "~/UI/ChildData.aspx?ChildFamilyId=" + new Guid(Session["ChildFamilyId"].ToString());
                if (Request.UrlReferrer != null)
                {
                    objMenu.Url = "~" + Request.UrlReferrer.PathAndQuery;
                }
                else
                {
                    objMenu.Url = "";
                }
                lstMenu.Add(objMenu);

                objMenu = new DayCarePL.MenuLink();
                objMenu.Name = "Child Prog. Enrollment";
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
