using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace DayCare
{
    public partial class StaffList : System.Web.UI.Page
    {
        CheckBox chkActiveAll = new CheckBox();
        int cnt = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["SchoolId"] == null || Session["CurrentSchoolYearId"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            chkActiveAll.Checked = true;
            
            
        }

        protected void rgStaffList_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            //DayCareBAL.StaffService proxyRole = new DayCareBAL.StaffService();
            //Guid SchoolId = new Guid();
            //if (Session["SchoolId"] != null)
            //{
            //    SchoolId = new Guid(Session["SchoolId"].ToString());
            //}
            //Guid CurrentSchoolYearId = new Guid();
            //if (Session["CurrentSchoolYearId"] != null)
            //{
            //    CurrentSchoolYearId = new Guid(Session["CurrentSchoolYearId"].ToString());
            //}
            //List<DayCarePL.StaffProperties> lstStaff = proxyRole.LoadStaff(SchoolId, CurrentSchoolYearId);
            //if (lstStaff != null)
            //{
            //    rgStaffList.DataSource = lstStaff.FindAll(name => !name.StaffCategoryName.Equals(DayCarePL.Common.SCHOOL_ADMINISTRATOR) && !name.UserGroupTitle.Equals(DayCarePL.Common.SCHOOL_ADMINISTRATOR));
            //}

            GetStaff(true);
        }

        protected void rgStaffList_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
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
            if (e.CommandName == "InitInsert")
            {
                Response.Redirect("Staff.aspx?Id=" + null + "");
            }
            else if (e.CommandName == "Edit")
            {
                Response.Redirect("Staff.aspx?Id=" + e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Id"].ToString() + "");
            }
            //}
        }

        protected void rgStaffList_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            //if (e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem || e.Item.ItemType == Telerik.Web.UI.GridItemType.Item)
            //{
            //    GridDataItem dataItem = e.Item as GridDataItem;
            //    Label 
            //}

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

        protected void rgStaffList_ItemCreated(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item is GridFilteringItem)
            {
                //GridFilteringItem filteringItem = (e.Item as GridFilteringItem);

                //filteringItem["Status"].Controls.Clear();
                ////if (chkActiveAll.Checked == true)
                ////{

                ////}
                //chkActiveAll.AutoPostBack = true;
                //chkActiveAll.CheckedChanged += new System.EventHandler(ddList_SelectedIndexChanged);
                //filteringItem["Status"].Controls.Add(chkActiveAll);
                //////(filteringItem["Status"].Controls[0] as CheckBox).Checked = true;
                ////if (filteringItem["Status"].Controls[0] != null)
                ////{
                ////GetStaff((filteringItem["Status"].Controls[0] as CheckBox).Checked);
                ////}
                ////DropDownList ddList = new DropDownList();
                ////ddList.SelectedIndexChanged += new System.EventHandler(ddList_SelectedIndexChanged);
                ////ddList.AutoPostBack = true;
                ////ddList.Items.Add(new ListItem("Show all checked", "True"));
                ////ddList.Items.Add(new ListItem("Show all unchecked", "False"));
                //if (Session["ddlSelValue"] != null)
                //{
                //    //ddList.Items.FindByValue((string)Session["ddlSelValue"]).Selected = true;
                //    chkActiveAll.Checked = (bool)Session["ddlSelValue"];
                //}
                ////filteringItem["Status"].Controls.Add(ddList);



            }


            if (e.Item is GridFilteringItem)
            {
                //GridFilteringItem filteringItem = e.Item as GridFilteringItem;
                ////set dimensions for the filter textbox  
                //TextBox box = filteringItem["Status"].Controls[0] as TextBox;
                //box.Width = Unit.Pixel(50);
                //TextBox box1 = filteringItem["IsPrimary"].Controls[0] as TextBox;
                //box1.Width = Unit.Pixel(50);
                //TextBox box2 = filteringItem["EndDate"].Controls[0] as TextBox;
                //box2.Width = Unit.Pixel(50);
                //RadDatePicker box3 = filteringItem["column4"].Controls[0] as RadDatePicker;
                //box3.Width = Unit.Pixel(100);
            }
        }

        protected void ddList_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            CheckBox chkActiveAll = (CheckBox)sender;
            //DropDownList ddList = (DropDownList)sender;
            Session["ddlSelValue"] = chkActiveAll.Checked;
            if (chkActiveAll.Checked == false)
            {
                rgStaffList.MasterTableView.FilterExpression = "([Active] = False) ";
                foreach (GridColumn column in rgStaffList.MasterTableView.Columns)
                {
                    column.CurrentFilterFunction = GridKnownFunction.NoFilter;
                    column.CurrentFilterValue = String.Empty;
                }
                rgStaffList.MasterTableView.Rebind();
            }
            else
            {
                rgStaffList.MasterTableView.FilterExpression = "([Active] = True) ";
                foreach (GridColumn column in rgStaffList.MasterTableView.Columns)
                {
                    column.CurrentFilterFunction = GridKnownFunction.NoFilter;
                    column.CurrentFilterValue = String.Empty;
                }
                rgStaffList.MasterTableView.Rebind();
            }
            //GetStaff(chkActiveAll.Checked);
            //rgStaffList.MasterTableView.Rebind(); 
        }

        protected void rgStaffList_PreRender(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //rgStaffList.MasterTableView.FilterExpression = "([Active] = \'True\')";
                //GridColumn column = rgStaffList.MasterTableView.GetColumnSafe("Status");
                //column.CurrentFilterFunction = GridKnownFunction.EqualTo;
                //rgStaffList.MasterTableView.Rebind();

                rgStaffList.MasterTableView.FilterExpression = "([Active] = \'True\') ";
                GridColumn column = rgStaffList.MasterTableView.GetColumnSafe("Active");
               
                rgStaffList.MasterTableView.Rebind();

            }
        }

        public void GetStaff(bool Active)
        {
            DayCareBAL.StaffService proxyRole = new DayCareBAL.StaffService();
            Guid SchoolId = new Guid();
            if (Session["SchoolId"] != null)
            {
                SchoolId = new Guid(Session["SchoolId"].ToString());
            }
            Guid CurrentSchoolYearId = new Guid();
            if (Session["CurrentSchoolYearId"] != null)
            {
                CurrentSchoolYearId = new Guid(Session["CurrentSchoolYearId"].ToString());
            }
            List<DayCarePL.StaffProperties> lstStaff = proxyRole.LoadStaff(SchoolId, CurrentSchoolYearId);
            if (lstStaff != null)
            {
                //rgStaffList.DataSource = lstStaff.FindAll(name => !name.StaffCategoryName.Equals(DayCarePL.Common.SCHOOL_ADMINISTRATOR) && !name.UserGroupTitle.Equals(DayCarePL.Common.SCHOOL_ADMINISTRATOR) && name.Active == Active);
                rgStaffList.DataSource = lstStaff.FindAll(name => !name.StaffCategoryName.Equals(DayCarePL.Common.SCHOOL_ADMINISTRATOR) && !name.UserGroupTitle.Equals(DayCarePL.Common.SCHOOL_ADMINISTRATOR) );
            }
        }
    }
}
