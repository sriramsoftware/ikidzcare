using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DayCare.UI
{
    public partial class DayCareIndex : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["StaffFullName"] != null)
            {
                hlName.Text = Session["StaffFullName"].ToString();
            }
            if (!Page.IsPostBack)
            {
                DayCareBAL.SchoolYearService proxySchoolYear = new DayCareBAL.SchoolYearService();
                Guid SchoolId = new Guid();
                if (Session["SchoolId"] != null)
                {
                    SchoolId = new Guid(Session["SchoolId"].ToString());
                }
                ddlSchoolYear.DataSource = proxySchoolYear.LoadAllSchoolYear(SchoolId);
                ddlSchoolYear.DataTextField = "Year";
                ddlSchoolYear.DataValueField = "Id";
                ddlSchoolYear.DataBind();

                if (Session["CurrentSchoolYearId"]!=null)
                {
                    ddlSchoolYear.SelectedValue = Session["CurrentSchoolYearId"].ToString();
                }

                
            }
        }

        protected void lnklogout_Click(object sender, EventArgs e)
        {
            Session.RemoveAll();
            Response.Redirect("~/Login.aspx");
        }

        protected void ddlSchoolYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["CurrentSchoolYearId"] = ddlSchoolYear.SelectedValue;
            Response.Redirect("~/UI/StaffList.aspx");
        }
    }
}
