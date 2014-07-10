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
    public partial class FamilyChildListReport : System.Web.UI.Page
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
                Fillddlfamily();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string data = string.Empty;
            string result = string.Empty;
            foreach (RadComboBoxItem item in rcbfamilyList.Items)
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
                    Session["FamilyTitle"] = string.Empty;
                }
                else
                {
                    result = result.Substring(0, result.LastIndexOf(','));
                    Session["FamilyTitle"] = result;
                }
            }
            StringBuilder jscript = new StringBuilder();
            jscript.Append("<script>window.open('");
            jscript.Append("RptFamilyChildListReport.aspx");
            jscript.Append("');</script>");
            Page.RegisterStartupScript("OpenWindows", jscript.ToString());
        }

        protected void Fillddlfamily()
        {
            DayCareBAL.LedgerOfFamilyService proxyChildFamily = new DayCareBAL.LedgerOfFamilyService();
            List<DayCarePL.ChildFamilyProperties> lstFamily = new List<DayCarePL.ChildFamilyProperties>();
            DayCarePL.ChildFamilyProperties ChildFamilyProperties = new DayCarePL.ChildFamilyProperties();
            lstFamily = proxyChildFamily.LoadChildFamily(new Guid(Session["SchoolId"].ToString()), new Guid(Session["CurrentSchoolYearId"].ToString())).Where(c => c.Active.Equals(true)).ToList();
            //if (lstFamily.Count > 0)
            //{
            ChildFamilyProperties.Id = new Guid("00000000-0000-0000-0000-000000000000");
            ChildFamilyProperties.FamilyTitle = "--Select All--";
            lstFamily.Insert(0, ChildFamilyProperties);
            rcbfamilyList.DataSource = lstFamily;
            rcbfamilyList.DataTextField = "FamilyTitle";
            rcbfamilyList.DataValueField = "Id";
            rcbfamilyList.DataBind();
            rcbfamilyList.EmptyMessage = "---Select---";
            //.IsPostBack.rcbfamilyList.Items.Insert(0, new ListItem("--Select--", DayCarePL.Common.GUID_DEFAULT));
            //}


        }
    }
}
