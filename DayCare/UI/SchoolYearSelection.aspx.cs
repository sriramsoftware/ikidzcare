using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DayCare.UI
{
    public partial class SchoolYearSelection : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["SchoolId"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            if (!IsPostBack)
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

                if (ddlSchoolYear.Items != null && ddlSchoolYear.Items.Count > 0)
                {
                    ddlSchoolYear.SelectedValue = Convert.ToString(proxySchoolYear.GetCurrentSchoolYear(SchoolId));
                }
            }
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            Session["CurrentSchoolYearId"] = ddlSchoolYear.SelectedValue;
            Response.Redirect("StaffList.aspx");
        }
    }
}
