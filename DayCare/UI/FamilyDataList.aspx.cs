using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace DayCare.UI
{
    public partial class FamilyDataList : System.Web.UI.Page
    {
        Guid SchoolId = new Guid();
        Guid CurrentSchoolYearId = new Guid();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["SchoolId"] == null || Session["CurrentSchoolYearId"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            if (!string.IsNullOrEmpty(Request.QueryString["ChildFamilyId"]))
            {
                ViewState["ChildFamilyId"] = Request.QueryString["ChildFamilyId"];
                Session["ChildFamilyId"] = Request.QueryString["ChildFamilyId"];//use for Menu link use in FamilyData.aspx
            }
            SetMenuLink();
        }

        protected void rgFamilyData_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DayCareBAL.FamilyDataService proxyRole = new DayCareBAL.FamilyDataService();
           // Guid SchoolId = new Guid();
            if (Session["SchoolId"] != null)
            {
                SchoolId = new Guid(Session["SchoolId"].ToString());
            }
           // Guid CurrentSchoolYearId = new Guid();
            if (Session["CurrentSchoolYearId"] != null)
            {
                CurrentSchoolYearId = new Guid(Session["CurrentSchoolYearId"].ToString());
            }
            if (ViewState["ChildFamilyId"] != null)
            {
                List<DayCarePL.FamilyDataProperties> lstFamilyData = proxyRole.LoadFamilyData(new Guid(ViewState["ChildFamilyId"].ToString()), SchoolId);
                if (lstFamilyData != null)
                {
                    rgFamilyData.DataSource = lstFamilyData;
                }
            }
        }

        protected void rgFamilyData_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            
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
                    if (ViewState["ChildFamilyId"] != null)
                    {
                        Response.Redirect("FamilyData.aspx?ChildFamilyId=" + ViewState["ChildFamilyId"].ToString() + "");
                    }
                }
                else if (e.CommandName == "Edit")
                {
                    if (ViewState["ChildFamilyId"] != null)
                    {
                        Response.Redirect("FamilyData.aspx?Id=" + e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Id"].ToString() + "&ChildFamilyId=" + ViewState["ChildFamilyId"].ToString() + "");
                    }
                }
            }
        }

        protected void rgFamilyData_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            //if (e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem || e.Item.ItemType == Telerik.Web.UI.GridItemType.Item)
            //{
            //    GridDataItem dataItem = e.Item as GridDataItem;
            //    Label 
            //}
            if (e.Item.ItemType == GridItemType.AlternatingItem || e.Item.ItemType == GridItemType.Item)
            {
                DayCarePL.FamilyDataProperties objFamilyData = e.Item.DataItem as DayCarePL.FamilyDataProperties;
                Image imgStaff = e.Item.FindControl("imgFamilyData") as Image;
                if (!string.IsNullOrEmpty(objFamilyData.Photo))
                {
                    imgStaff.ImageUrl = "../FamilyImages/" + objFamilyData.Photo;
                }
                else
                {
                    //if (objFamilyData.Gender == true)//true= Male
                    //{
                    //    imgStaff.ImageUrl = "../FamilyImages/male_photo.png";
                    //}
                    //else
                    //{
                    //    imgStaff.ImageUrl = "../FamilyImages/female_photo.png";
                    //}
                }
                HyperLink hlChildData = e.Item.FindControl("hlChildData") as HyperLink;
                if (hlChildData != null)
                {
                    hlChildData.NavigateUrl = "ChildData.aspx?Id=" + objFamilyData.ChildFamilyId;
                }
                if (Session["SchoolId"] != null)
                {
                    SchoolId = new Guid(Session["SchoolId"].ToString());
                }
                // Guid CurrentSchoolYearId = new Guid();
                if (Session["CurrentSchoolYearId"] != null)
                {
                    CurrentSchoolYearId = new Guid(Session["CurrentSchoolYearId"].ToString());
                }
                if (!Common.IsCurrentYear(CurrentSchoolYearId, SchoolId))
                {
                    hlChildData.Enabled = false;
                }
                //Guid SchoolId = new Guid();
                //Guid CurrentSchoolYearId = new Guid();
                //if (Session["SchoolId"] != null)
                //{
                //    SchoolId = new Guid(Session["SchoolId"].ToString());
                //}

                //if (Session["CurrentSchoolYearId"] != null)
                //{
                //    CurrentSchoolYearId = new Guid(Session["CurrentSchoolYearId"].ToString());
                //}

                //if (!Common.IsCurrentYear(CurrentSchoolYearId, SchoolId))
                //{
                //    GridDataItem itm = (GridDataItem)e.Item;
                //    if (itm != null)
                //    {
                //        ImageButton imgbtnEdit = (ImageButton)itm["Edit"].Controls[0];
                //        if (imgbtnEdit != null)
                //        {
                //            imgbtnEdit.ToolTip = "";
                //            imgbtnEdit.Enabled = false;
                //            imgbtnEdit.Style.Value = "cursor:auto";
                //        }
                //        GridCommandItem cmdItem = itm.OwnerTableView.GetItems(GridItemType.CommandItem)[0] as GridCommandItem;
                //        if (cmdItem != null)
                //        {
                //            LinkButton lnkbtnInitInsertButton = (LinkButton)cmdItem.FindControl("InitInsertButton");
                //            lnkbtnInitInsertButton.Enabled = false;
                //            lnkbtnInitInsertButton.Style.Value = "cursor:auto";
                //        }
                //        //rgStaffList.MasterTableView.IsItemInserted = false;
                //    }
                //}
            }
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
                objMenu.Name = "Family Data List";
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
