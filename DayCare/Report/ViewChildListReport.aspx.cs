using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Text;

namespace DayCare.Report
{
    public partial class ViewChildListReport : System.Web.UI.Page
    {
        RadAjaxManager MasterAjaxManager;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["SchoolId"] == null || Session["CurrentSchoolYearId"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            if (!IsPostBack)
            {
                FillChildlist();
                //Fillddlfamily();
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string data = string.Empty;
            string result = string.Empty;
            foreach (RadComboBoxItem item in rcbchildList.Items)
            {
                CheckBox chkName = (CheckBox)item.FindControl("CheckBox");
                if (chkName.Checked == true)
                {

                    data = item.Value.Trim();
                    result += "'" + data + "',";

                }
            }
            if (result.Length > 0)
            {
                if (result.IndexOf("00000000-0000-0000-0000-000000000000") != -1)
                {
                    Session["ChildList"] = string.Empty;
                }
                else
                {
                    result = result.Substring(0, result.LastIndexOf(','));
                        Session["ChildList"] = result;
                }
            }
            StringBuilder jscript = new StringBuilder();
            jscript.Append("<script>window.open('");
            jscript.Append("RptChildListReport.aspx");
            jscript.Append("');</script>");
            Page.RegisterStartupScript("OpenWindows", jscript.ToString());
        }

        protected void FillChildlist()
        {
            DayCarePL.ChildDataProperties ChildDataProperties = new DayCarePL.ChildDataProperties();
            DayCareBAL.ChildListService proxyChild = new DayCareBAL.ChildListService();
            List<DayCarePL.ChildDataProperties> lstChild = proxyChild.GetAllChildList(new Guid(Session["SchoolId"].ToString()), new Guid(Session["CurrentSchoolYearId"].ToString()));
            ChildDataProperties.ChildDataId = new Guid("00000000-0000-0000-0000-000000000000");
            ChildDataProperties.FullName = "--Select All--";
            lstChild.Insert(0, ChildDataProperties);

            rcbchildList.DataSource = lstChild;
            rcbchildList.DataTextField = "FullName";
            rcbchildList.DataValueField = "ChildDataId";
            rcbchildList.DataBind();
            rcbchildList.EmptyMessage = "---Select---";
        }
    }
}
