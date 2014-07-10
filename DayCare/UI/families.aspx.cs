using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace DayCare.UI
{
    public partial class families : System.Web.UI.Page
    {

        RadAjaxManager MasterAjaxManager;
        Guid SchoolId = new Guid();
        Guid ChildDataId = new Guid();
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
            SetMenuLink();



        }

        protected void rgChildFamily_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
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
                DayCareBAL.ChildFamilyService proxyChildFamily = new DayCareBAL.ChildFamilyService();
                List<DayCarePL.ChildFamilyProperties> lstChildData = proxyChildFamily.LoadChildFamily(SchoolId, CurrentSchoolYearId);

                if (lstChildData != null)
                {
                    rgChildFamily.DataSource = lstChildData;
                }
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.Families, "rgChildFamily_NeedDataSource", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
            }
        }

        protected void rgChildFamily_EditCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            GridDataItem item = (GridDataItem)e.Item;
            // hdnName.Value = item["FamilyTitle"].Text;
            e.Canceled = true;
        }

        protected void rgChildFamily_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
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
            //    if (e.CommandName == "InitInsert")
            //    {
            //        e.Canceled = true;
            //    }
            //    else if (e.CommandName == "Edit")
            //    {
            //        e.Canceled = true;
            //    }
            //}
            //else
            //{
            string FamilyName = "";
            GridDataItem itm = e.Item as GridDataItem;
            if (itm != null)
            {
                FamilyName = itm["FamilyTitle"].Text;
            }
            Session["ChildFamilyUrl"] = "~/UI/Families.aspx";
            if (e.CommandName == "Edit")
            {
                if (!string.IsNullOrEmpty(FamilyName))
                {
                    Session["ChildFamilyName"] = ": " + FamilyName;
                }
                Guid ChildFamilyId = new Guid(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Id"].ToString());
                //GridEditableItem dataItem = (GridEditableItem)e.Item;
                //ChildDataId = new Guid(dataItem.GetDataKeyValue("Id").ToString());
                ////Get ChildDataId
                //DayCareBAL.ChildFamilyService proxyChildFamily = new DayCareBAL.ChildFamilyService();
                //ChildDataId = proxyChildFamily.GetChildDataId(ChildFamilyId);

                if (ChildFamilyId != null)
                {
                    ViewState["ChildFamilyId"] = ChildFamilyId;
                }
                //LoadDataById(ChildFamilyId);
                e.Canceled = true;
                //if (Common.IsCurrentYear(CurrentSchoolYearId, SchoolId))
                //{
                    Response.Redirect("AddEditChild.aspx?ChildFamilyId=" + ChildFamilyId);
                //}
            }
            if (e.CommandName == "InitInsert")
            {
                //if (objFamily != null)
                if (!string.IsNullOrEmpty(FamilyName))
                {
                    Session["ChildFamilyName"] = ": " + FamilyName;
                }
                Response.Redirect("AddEditChild.aspx?ChildFamilyId=");
            }
            //}


        }

        protected void rgChildFamily_ItemCreated(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
        }

        protected void rgChildFamily_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item is GridPagerItem)
            {
                RadComboBox PageSizeCombo = (RadComboBox)e.Item.FindControl("PageSizeComboBox");
                PageSizeCombo.Items.Clear();
                PageSizeCombo.Items.Add(new RadComboBoxItem("25"));
                PageSizeCombo.FindItemByText("25").Attributes.Add("ownerTableViewId", rgChildFamily.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("50"));
                PageSizeCombo.FindItemByText("50").Attributes.Add("ownerTableViewId", rgChildFamily.MasterTableView.ClientID);
                //PageSizeCombo.Items[0].Text = "25";
                //PageSizeCombo.Items[1].Text = "50";
                PageSizeCombo.FindItemByText(e.Item.OwnerTableView.PageSize.ToString()).Selected = true;
            }
            if (e.Item.ItemType == GridItemType.AlternatingItem || e.Item.ItemType == GridItemType.Item)
            {

            }
        }

        public void SetMenuLink()
        {
            try
            {
                List<DayCarePL.MenuLink> lstMenu = new List<DayCarePL.MenuLink>();
                DayCarePL.MenuLink objMenu;
                objMenu = new DayCarePL.MenuLink();
                objMenu.Name = "Families";
                objMenu.Url = "";
                lstMenu.Add(objMenu);
                usrMenuLink.SetMenuLink(lstMenu);
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.Families, "LoadDataById", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
            }
        }
    }
}
