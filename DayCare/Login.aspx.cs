using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DayCare
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)  
        {
            if (!Page.IsPostBack)
            {
                //User can set the username password
                Common.BindSchool(ddlSchool);
                if (Request.Cookies["uid@DayCare"] != null)
                {
                    txtLoginName.Text = Request.Cookies["uid@DayCare"].Value.ToString().Trim();
                    RememberMe.Checked = true;                   
                }
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            DayCareBAL.StaffService proxyStaffService = new DayCareBAL.StaffService();
            //Guid SchoolId = new Guid("8CA767A0-5E36-4343-8B1D-5ECC40EB9E1B");
            //Guid SchoolId = new Guid("66E740A1-1202-45C9-9D4F-0493C8746EDD");
            Guid SchoolId = new Guid(ddlSchool.SelectedValue);
            DayCarePL.StaffProperties objStaffDetails = proxyStaffService.LoadStaffDetailsByUserNameAndPassword(txtLoginName.Text.Trim(), txtLoginPassword.Text.Trim(), SchoolId);
            if (objStaffDetails != null)
            {
                Session["StaffId"] = objStaffDetails.Id;
                Session["StaffFullName"] = objStaffDetails.FullName;
                Session["SchoolId"] = objStaffDetails.SchoolId;
                Session["SchoolName"] = objStaffDetails.SchoolName;
                Session["UserGroupTitle"] = objStaffDetails.UserGroupTitle;
                Session["CurrentSchoolYearId"] = objStaffDetails.ScoolYearId;
                Session["Role_Id"] = objStaffDetails.RolId;

                if (RememberMe.Checked)
                {
                    HttpCookie objCookiesUn = new HttpCookie("uid@DayCare", txtLoginName.Text.ToLower().Trim());
                    objCookiesUn.Expires = DateTime.Now.AddYears(100);
                    Response.Cookies.Add(objCookiesUn);
                }
                else
                {
                    if (Request.Cookies["uid@DayCare"] != null)
                    {
                        Response.Cookies["uid@DayCare"].Expires = DateTime.Now.AddYears(-100);
                    }
                }
                Response.Redirect("UI/DashBoard.aspx");
            }
            else
            {
                FailureText.Text = "UserName/Password is wrong please try again";
            }
        }
    }
}
