using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using Telerik.Web.UI;
using System.Web.UI.WebControls;

namespace DayCare.UI
{
    public partial class ChildAttendanceList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["SchoolId"] == null || Session["CurrentSchoolYearId"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
        }

        protected void rgChildAttendsList_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                DayCareBAL.ChildAttendanceHistoryListService proxyChildList = new DayCareBAL.ChildAttendanceHistoryListService();
                rgChildAttendsList.DataSource = proxyChildList.GetChildList(new Guid(Session["CurrentSchoolYearId"].ToString()));
            }
            catch (Exception ex)
            {

            }

        }

        protected void rgChildAttendsList_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item is GridPagerItem)
            {
                RadComboBox PageSizeCombo = (RadComboBox)e.Item.FindControl("PageSizeComboBox");
                PageSizeCombo.Items.Clear();
                PageSizeCombo.Items.Add(new RadComboBoxItem("25"));
                PageSizeCombo.FindItemByText("25").Attributes.Add("ownerTableViewId", rgChildAttendsList.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("50"));
                PageSizeCombo.FindItemByText("50").Attributes.Add("ownerTableViewId", rgChildAttendsList.MasterTableView.ClientID);
                //PageSizeCombo.Items[0].Text = "25";
                //PageSizeCombo.Items[1].Text = "50";
                PageSizeCombo.FindItemByText(e.Item.OwnerTableView.PageSize.ToString()).Selected = true;
            }
            if (e.Item.ItemType == GridItemType.AlternatingItem || e.Item.ItemType == GridItemType.Item)
            {
                DayCarePL.ChildDataProperties objChild = e.Item.DataItem as DayCarePL.ChildDataProperties;
                Image imgChild = e.Item.FindControl("imgChild") as Image;
                if (!string.IsNullOrEmpty(objChild.Photo))
                {
                    imgChild.ImageUrl = "../ChildImages/" + objChild.Photo;
                }
                else
                {
                    if (objChild.Gender == true)//true= Male
                    {
                        imgChild.ImageUrl = "../ChildImages/boy.png";
                    }
                    else
                    {
                        imgChild.ImageUrl = "../ChildImages/girl.png";
                    }
                }
            }
        }

        protected void rgChildAttendsList_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
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
                GridDataItem item = e.Item as GridDataItem;
                if (item != null)
                {
                    Session["ChildName"] = item["FullName"].Text.Trim();
                }
                if (e.CommandName == "Edit")
                {
                    GridEditableItem dataItem = (GridEditableItem)e.Item;
                    Response.Redirect("ChildAttendanceHistoryList.aspx?ChildSchoolYearId=" + dataItem.GetDataKeyValue("ChildSchoolYearId").ToString());
                }
            }
        }
    }
}
