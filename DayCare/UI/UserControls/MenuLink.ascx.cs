using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DayCare.UI.UserControls
{
    public partial class MenuLink : System.Web.UI.UserControl
    {
        public static List<DayCarePL.MenuLink> lstMenu = new List<DayCarePL.MenuLink>();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void dlMenuLink_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                DayCarePL.MenuLink objMenu = e.Item.DataItem as DayCarePL.MenuLink;
                HyperLink hlMenuLink = e.Item.FindControl("hlMenuLink") as HyperLink;
                hlMenuLink.Text = objMenu.Name;
                hlMenuLink.NavigateUrl = objMenu.Url;
            }
        }

        public void SetMenuLink(List<DayCarePL.MenuLink> lstMenu)
        {
            dlMenuLink.RepeatColumns = lstMenu.Count;
            dlMenuLink.DataSource = lstMenu;
            dlMenuLink.DataBind();
        }
    }
}