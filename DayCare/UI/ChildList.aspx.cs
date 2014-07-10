using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace DayCare.UI
{
    public partial class ChildList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["SchoolId"] == null || Session["CurrentSchoolYearId"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            if (!IsPostBack)
            {
                SetMenuLink();
            }
        }

        protected void rgChildList_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                DayCareBAL.ChildListService proxyChild = new DayCareBAL.ChildListService();
                List<DayCarePL.ChildDataProperties> lstChild = proxyChild.GetAllChildList(new Guid(Session["SchoolId"].ToString()), new Guid(Session["CurrentSchoolYearId"].ToString()));
                if (lstChild == null)
                {
                    lstChild = new List<DayCarePL.ChildDataProperties>();
                }
                rgChildList.DataSource = lstChild;
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.ChildList, "rgChildFamily_NeedDataSource", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
            }
        }

        protected void rgChildList_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            //if (e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem || e.Item.ItemType == Telerik.Web.UI.GridItemType.Item)
            //{
            //    GridDataItem dataItem = e.Item as GridDataItem;
            //    Label 
            //}
            //if (e.Item.ItemType == GridItemType.AlternatingItem)
            //{

            // }
            if (e.Item is GridPagerItem)
            {
                RadComboBox PageSizeCombo = (RadComboBox)e.Item.FindControl("PageSizeComboBox");
                PageSizeCombo.Items.Clear();
                PageSizeCombo.Items.Add(new RadComboBoxItem("25"));
                PageSizeCombo.FindItemByText("25").Attributes.Add("ownerTableViewId", rgChildList.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("50"));
                PageSizeCombo.FindItemByText("50").Attributes.Add("ownerTableViewId", rgChildList.MasterTableView.ClientID);
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

        protected void rgChildList_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
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

            //if (!Common.IsCurrentYear(CurrentSchoolYearId, SchoolId))
            //{
            //    //if (e.CommandName == "InitInsert")
            //    //{
            //    //    e.Canceled = true;
            //    //}
            //    //else if (e.CommandName == "Edit")
            //    //{
            //    //    e.Canceled = true;
            //    //}
            //}
            //else
            //{
            Session["ChildFamilyUrl"] = "~/UI/ChildList.aspx";
            //if (e.CommandName == "InitInsert")
            //{
            //    GridDataItem dataItem = e.Item as GridDataItem;
            //    e.Item.FindControl("ChildFamilyId");
            //    Response.Redirect("AddEditChild.aspx?ChildFamilyId=" + dataItem["ChildFamilyId"].Text);
            //}
            //else 
            if (e.CommandName == "Edit")
            {
                GridEditableItem dataItem = (GridEditableItem)e.Item;
                Response.Redirect("AddEditChild.aspx?ChildFamilyId=" + dataItem["ChildFamilyId"].Text + "&ChildDataId=" + dataItem.GetDataKeyValue("ChildDataId").ToString());
            }
            //}
        }
        //

        protected void rgChildList_ItemCreated(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {

        }
        public void SetMenuLink()
        {
            try
            {
                List<DayCarePL.MenuLink> lstMenu = new List<DayCarePL.MenuLink>();
                DayCarePL.MenuLink objMenu;
                objMenu = new DayCarePL.MenuLink();
                objMenu.Name = "Children";
                objMenu.Url = "";
                lstMenu.Add(objMenu);
                usrMenuLink.SetMenuLink(lstMenu);
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.ChildList, "SetMenuLink", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
            }
        }


        protected void rgChildList_PreRender(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                rgChildList.MasterTableView.FilterExpression = "([Active] = \'True\') ";
                GridColumn column = rgChildList.MasterTableView.GetColumnSafe("Active");
                rgChildList.MasterTableView.Rebind();
            }
        }
    }
}
