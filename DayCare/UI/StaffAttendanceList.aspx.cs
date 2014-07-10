using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Telerik.Web.UI;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DayCare.UI
{
    public partial class StaffAttendanceList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["SchoolId"] == null && Session["CurrentSchoolYearId"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }

        }

        protected void rgStaffAttendanceList_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DayCareBAL.StaffAttendanceHistoryListService proxyLoad = new DayCareBAL.StaffAttendanceHistoryListService();
            rgStaffAttendanceList.DataSource = proxyLoad.LoadStaffList(new Guid(Session["CurrentSchoolYearId"].ToString()));
        }

        protected void rgStaffAttendanceList_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
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
                GridDataItem itm = e.Item as GridDataItem;
                if (itm != null)
                {
                   
                    //string Lname = itm["LastName"].Text.Trim();
                    Session["FullName"] = itm["FullName"].Text.Trim();
                }
                if (e.CommandName == "Edit")
                {
                    GridEditableItem dataItem = (GridEditableItem)e.Item;
                    Response.Redirect("StaffAttendanceHistoryList.aspx?StaffSchoolYearId=" + dataItem.GetDataKeyValue("StaffSchoolYearId").ToString());
                }
            }
        }

        protected void rgStaffAttendanceList_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item.ItemType == GridItemType.AlternatingItem || e.Item.ItemType == GridItemType.Item)
            {
                DayCarePL.StaffProperties objStaff = e.Item.DataItem as DayCarePL.StaffProperties;
                Image imgStaff = e.Item.FindControl("imgStaff") as Image;
                if (!string.IsNullOrEmpty(objStaff.Photo))
                {
                    imgStaff.ImageUrl = "../StaffImages/" + objStaff.Photo;
                }
                else
                {
                    if (objStaff.Gender == true)//true= Male
                    {
                        imgStaff.ImageUrl = "../StaffImages/male_photo.png";
                    }
                    else
                    {
                        imgStaff.ImageUrl = "../StaffImages/female_photo.png";
                    }
                }
            }
        }

        protected void rgStaffAttendanceList_ItemCreated(object sender, Telerik.Web.UI.GridItemEventArgs e)
        { 
        }
    }
}
