using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace DayCare
{
    public class Common
    {
        public static void BindSchool(DropDownList ddlSchool)
        {
            try
            {
                DayCareBAL.SchoolService proxySchool = new DayCareBAL.SchoolService();
                List<DayCarePL.SchoolProperties> data = proxySchool.LoadAllSchool();
                if (data.Count > 0)
                {
                    ddlSchool.DataSource = data;
                    ddlSchool.DataTextField = "Name";
                    ddlSchool.DataValueField = "Id";
                    ddlSchool.DataBind();

                }
                ddlSchool.Items.Insert(0, new ListItem("--Select--", DayCarePL.Common.GUID_DEFAULT));
            }
            catch (Exception ex)
            {
            }
        }

        public static void BindCountryDropDown(DropDownList ddlCountry)
        {
            try
            {
                DataSet dsCountry = new DataSet();
                Configuration myConfiguration = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");
                dsCountry.ReadXml(myConfiguration.FilePath.ToLower().Remove(myConfiguration.FilePath.ToLower().IndexOf("web.config")) + "XML\\CountryList.xml");

                ddlCountry.Items.Clear();
                ddlCountry.DataSource = dsCountry;
                ddlCountry.DataTextField = "Name";
                ddlCountry.DataValueField = "Id";
                ddlCountry.DataBind();
                ddlCountry.Items.Insert(0, new ListItem("--Select--", "00000000-0000-0000-0000-000000000000"));
            }
            catch (Exception ex)
            {

            }
        }

        public static void BindStateDropDown(DropDownList ddlState, string CountryId)
        {
            try
            {
                DataSet dsState = new DataSet();
                Configuration myConfiguration = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");
                dsState.ReadXml(myConfiguration.FilePath.ToLower().Remove(myConfiguration.FilePath.ToLower().IndexOf("web.config")) + "XML\\StateList.xml");
                ddlState.Items.Clear();
                DataView dvState = new DataView();
                dvState = dsState.Tables[0].DefaultView;
                dvState.RowFilter = "CountryId='" + CountryId + "'";
                dvState.Sort = "Name";
                ddlState.DataSource = dvState;
                ddlState.DataTextField = "Name";
                ddlState.DataValueField = "Id";
                ddlState.DataBind();
                ddlState.Items.Insert(0, new ListItem("--Select--", "00000000-0000-0000-0000-000000000000"));
            }
            catch (Exception ex)
            {

            }
        }

        public static bool CheckDuplicate(string TableName, string ColumnName, string ColumnValue, string Type, string Id)
        {
            bool result = true;
            SqlConnection conn = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["daycareConnectionString"].ToString());
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "spCheckDuplicate";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TblName", TableName);
                cmd.Parameters.AddWithValue("@ColumnName", ColumnName);
                cmd.Parameters.AddWithValue("@ColumnValue", ColumnValue.Replace("'", "''"));
                cmd.Parameters.AddWithValue("@Type", Type);
                cmd.Parameters.Add(new SqlParameter("@result", SqlDbType.Int)).Direction = ParameterDirection.Output;
                if (!Type.Equals("insert"))
                    cmd.Parameters.AddWithValue("@Id", Id);
                cmd.ExecuteNonQuery();
                result = Convert.ToBoolean(cmd.Parameters["@result"].Value);
            }
            catch (Exception ex)
            {
                result = true;
            }
            finally
            {
                conn.Close();
            }
            return result;
        }

        public static void BindUserGroupDropDown(DropDownList ddlUserGroup, Guid SchoolId)
        {
            try
            {
                DayCareBAL.UserGroupService objUserGroupService = new DayCareBAL.UserGroupService();
                ddlUserGroup.DataSource = objUserGroupService.LoadUserGroup(SchoolId);
                ddlUserGroup.DataTextField = "GroupTitle";
                ddlUserGroup.DataValueField = "Id";
                ddlUserGroup.DataBind();
                ddlUserGroup.Items.Insert(0, new ListItem("--Select--", "00000000-0000-0000-0000-000000000000"));
            }
            catch
            {

            }
        }

        public static void BindChargeCode(DropDownList ddlChargeCode)
        {
            try
            {
                DayCareBAL.LedgerService objLedger = new DayCareBAL.LedgerService();
                ddlChargeCode.DataSource = objLedger.LoadChargeCode();
                ddlChargeCode.DataTextField = "CategoryName";
                ddlChargeCode.DataValueField = "Id";
                ddlChargeCode.DataBind();
                ddlChargeCode.Items.Insert(0, new ListItem("--Select--", "00000000-0000-0000-0000-000000000000"));
            }
            catch (Exception ex)
            {
            }
        }

        public static void BindStaffCategoryDropDown(DropDownList ddlUserGroup, Guid SchoolId)
        {
            try
            {
                DayCareBAL.StaffCategoryService objUserGroupService = new DayCareBAL.StaffCategoryService();
                ddlUserGroup.DataSource = objUserGroupService.loadStaffCategory(SchoolId);
                ddlUserGroup.DataTextField = "Name";
                ddlUserGroup.DataValueField = "Id";
                ddlUserGroup.DataBind();
                ddlUserGroup.Items.Insert(0, new ListItem("--Select--", "00000000-0000-0000-0000-000000000000"));
            }
            catch
            {

            }
        }

        public static void BindRoles(DropDownList ddlRol)
        {
            try
            {
                DayCareBAL.RoleService objRoleService = new DayCareBAL.RoleService();
                DayCarePL.RoleProperties[] Roles = objRoleService.LoadRoles();
                if (Roles != null && Roles.Length > 0)
                {
                    ddlRol.DataSource = Roles.ToList().FindAll(role => !role.Name.Equals(DayCarePL.Common.SCHOOL_ADMINISTRATOR));
                    ddlRol.DataTextField = "Name";
                    ddlRol.DataValueField = "Id";
                    ddlRol.DataBind();
                }
                ddlRol.Items.Insert(0, new ListItem("--Select--", DayCarePL.Common.GUID_DEFAULT));
            }
            catch
            {

            }
        }

        public static void BindSchoolYear(DropDownList ddlYear)
        {
            try
            {
                List<string> lstYears = new List<string>();
                //int Year = DateTime.Now.Year;
                //int PreviouYear = Year - 1;
                //int NextYear = Year + 1;
                //lstYears.Add(PreviouYear + " - " + Year);
                //lstYears.Add(Year + " - " + NextYear);
                //ddlYear.DataSource = lstYears;

                for (int Year = 2012; Year <= DateTime.Now.Year + 1; Year++)
                {
                    int PreviouYear = Year - 1;
                    int NextYear = Year;
                    lstYears.Add(PreviouYear + " - " + Year);
                    //string stryear = PreviouYear + " - " + Year;
                    //ddlYear.Items.Add(new ListItem(stryear, stryear));
                }
                lstYears.Reverse();

                foreach (string stryear in lstYears)
                {
                    ddlYear.Items.Add(new ListItem(stryear, stryear));
                }
                ddlYear.Items.Insert(0, new ListItem("--Select--", "-1"));
            }
            catch
            {

            }
        }

        public static bool IsCurrentYear(Guid CurrentYearId, Guid SchoolId)
        {
            try
            {
                DayCareBAL.SchoolYearService proxySchoolYear = new DayCareBAL.SchoolYearService();

                if (CurrentYearId.Equals(proxySchoolYear.GetCurrentSchoolYear(SchoolId)))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        //if want to get primary staff than pass true and not primary than pass false.but if want all than pass "null"
        public static void BindStaff(DropDownList ddlStaff, Guid SchoolId, Guid SchoolYearId)
        {
            try
            {
                DayCareBAL.StaffService proxyStaff = new DayCareBAL.StaffService();
                List<DayCarePL.StaffProperties> lstStaff = proxyStaff.LoadStaff(SchoolId, SchoolYearId);
                if (lstStaff != null && lstStaff.Count > 0)
                {
                    lstStaff = lstStaff.FindAll(s => s.Active.Equals(true) && !s.StaffCategoryName.Equals(DayCarePL.Common.SCHOOL_ADMINISTRATOR) && !s.UserGroupTitle.Equals(DayCarePL.Common.SCHOOL_ADMINISTRATOR));
                    ddlStaff.DataSource = lstStaff;
                    ddlStaff.DataTextField = "FullName";
                    ddlStaff.DataValueField = "Id";
                    ddlStaff.DataBind();
                }
                ddlStaff.Items.Insert(0, new ListItem("--Select--", DayCarePL.Common.GUID_DEFAULT));
            }
            catch
            {

            }
        }

        public static void BindClassRoom(DropDownList ddlClassRoom, Guid SchoolId)
        {
            try
            {
                DayCareBAL.ClassRoomService proxyClassRoom = new DayCareBAL.ClassRoomService();
                DayCarePL.ClassRoomProperties[] ClassRoom = proxyClassRoom.LoadClassRoom(SchoolId);
                if (ClassRoom != null && ClassRoom.Count() > 0)
                {
                    ddlClassRoom.DataSource = ClassRoom.Where(c => c.Active.Equals(true));
                    ddlClassRoom.DataTextField = "Name";
                    ddlClassRoom.DataValueField = "Id";
                    ddlClassRoom.DataBind();
                    ddlClassRoom.Items.Insert(0, new ListItem("--Select--", DayCarePL.Common.GUID_DEFAULT));
                }
            }
            catch
            {

            }
        }

        public static void BindClassRoomReport(DropDownList ddlClassRoom, Guid SchoolId, Guid SchoolYearId)
        {
            try
            {
                DayCareBAL.ClassRoomService proxyClassRoom = new DayCareBAL.ClassRoomService();
                DayCarePL.ClassRoomProperties[] ClassRoom = proxyClassRoom.LoadClassRoomReport(SchoolId, SchoolYearId);
                if (ClassRoom != null && ClassRoom.Count() > 0)
                {
                    ddlClassRoom.DataSource = ClassRoom.Where(c => c.Active.Equals(true));
                    ddlClassRoom.DataTextField = "Name";
                    ddlClassRoom.DataValueField = "Id";
                    ddlClassRoom.DataBind();
                    ddlClassRoom.Items.Insert(0, new ListItem("--Select--", DayCarePL.Common.GUID_DEFAULT));
                }
            }
            catch
            {

            }
        }


        public static void BindEnrollmentStatus(DropDownList ddlEnrollmentStatus, Guid SchoolId)
        {
            DayCareBAL.EnrollmentStausService proxy = new DayCareBAL.EnrollmentStausService();
            List<DayCarePL.EnrollmentStatusProperties> data = proxy.LoadEnrollmentStatus(SchoolId).ToList();
            ddlEnrollmentStatus.Items.Clear();
            if (data.Count() > 0)
            {
                data = data.FindAll(s => s.Active.Equals(true) && !s.Status.Equals(DayCarePL.Common.SCHOOL_ADMINISTRATOR) && !s.Status.Equals(DayCarePL.Common.SCHOOL_ADMINISTRATOR));

                ddlEnrollmentStatus.DataSource = data;
                ddlEnrollmentStatus.DataTextField = "Status";
                ddlEnrollmentStatus.DataValueField = "Id";
                ddlEnrollmentStatus.DataBind();

            }
            ddlEnrollmentStatus.Items.Insert(0, new ListItem("--Select--", DayCarePL.Common.GUID_DEFAULT));
        }


        public static void BindProgClassRoom(DropDownList ddlProgClassRoom, Guid SchoolProgramId)
        {
            DayCareBAL.ProgScheduleService proxyProgSchedule = new DayCareBAL.ProgScheduleService();
            DayCarePL.ProgScheduleProperties[] data = proxyProgSchedule.LoadProgClassRoom(SchoolProgramId).ToArray();
            if (data.Count() > 0)
            {
                ddlProgClassRoom.Items.Clear();
                ddlProgClassRoom.DataSource = data;
                ddlProgClassRoom.DataTextField = "ProgClassRoomTitle";
                ddlProgClassRoom.DataValueField = "Id";
                ddlProgClassRoom.DataBind();
                ddlProgClassRoom.Items.Insert(0, new ListItem("--Select--", DayCarePL.Common.GUID_DEFAULT));

            }


        }

        public static void BindProgChildClassRoom(DropDownList ddlChildProgClassRoom, Guid SchoolProgramId)
        {
            try
            {
                DayCareBAL.AddEditChildService proxyProgSchedule = new DayCareBAL.AddEditChildService();
                DayCarePL.ChildProgEnrollmentProperties[] data = proxyProgSchedule.LoadProgClassRoom(SchoolProgramId).FindAll(s => !s.ProgClassRoomTitle.Equals("N/A")).ToArray();
                ddlChildProgClassRoom.Items.Clear();
                if (data.Count() > 0)
                {

                    ddlChildProgClassRoom.DataSource = data;
                    ddlChildProgClassRoom.DataTextField = "ProgClassRoomTitle";
                    ddlChildProgClassRoom.DataValueField = "Id";
                    ddlChildProgClassRoom.DataBind();


                }
                ddlChildProgClassRoom.Items.Insert(0, new ListItem("--Select--", DayCarePL.Common.GUID_DEFAULT));
            }
            catch (Exception ex)
            {

            }
        }

        public static void BindProgSecondaryChildClassRoom(DropDownList ddlChildProgClassRoom, Guid SchoolProgramId)
        {
            try
            {
                DayCareBAL.AddEditChildService proxyProgSchedule = new DayCareBAL.AddEditChildService();
                List<DayCarePL.ChildProgEnrollmentProperties> lstChildProgEnrollment = proxyProgSchedule.LoadProgClassRoom(SchoolProgramId);
                ddlChildProgClassRoom.Items.Clear();
                Guid ProgClassRoomIdOfNAClassRoom = new Guid();
                if (lstChildProgEnrollment != null && lstChildProgEnrollment.Count > 0)
                {


                    ProgClassRoomIdOfNAClassRoom = lstChildProgEnrollment.Find(i => i.ProgClassRoomTitle.Equals("N/A")).Id;
                    ddlChildProgClassRoom.Items.Clear();

                    ddlChildProgClassRoom.DataSource = lstChildProgEnrollment.FindAll(s => !s.ProgClassRoomTitle.Equals("N/A"));
                    ddlChildProgClassRoom.DataTextField = "ProgClassRoomTitle";
                    ddlChildProgClassRoom.DataValueField = "Id";
                    ddlChildProgClassRoom.DataBind();



                }
                ddlChildProgClassRoom.Items.Insert(0, new ListItem("N/A", ProgClassRoomIdOfNAClassRoom.ToString()));
            }
            catch (Exception ex)
            {

            }
        }

        public static void BindSchoolProgram(DropDownList ddlSchoolProgram, Guid SchoolYearId)
        {
            try
            {
                DayCareBAL.SchoolProgramFeesDetailService proxyProgram = new DayCareBAL.SchoolProgramFeesDetailService();
                List<DayCarePL.SchoolProgramFeesDetailProperties> data = proxyProgram.LoadProgram(SchoolYearId);
                if (data.Count > 0)
                {
                    ddlSchoolProgram.DataSource = data;
                    ddlSchoolProgram.DataTextField = "SchoolProgram";
                    ddlSchoolProgram.DataValueField = "SchoolProgramId";
                    ddlSchoolProgram.DataBind();
                    ddlSchoolProgram.Items.Insert(0, new ListItem("--Select--", DayCarePL.Common.GUID_DEFAULT));
                }
            }
            catch (Exception ex)
            {
            }
        }

        public static void BindProgramName(DropDownList ddlProgram, Guid SchoolYearId)
        {
            try
            {
                DayCareBAL.AddEditChildService proxyProgram = new DayCareBAL.AddEditChildService();
                List<DayCarePL.ChildProgEnrollmentProperties> data = proxyProgram.LoadProgram(SchoolYearId);
                if (data.Count > 0)
                {
                    ddlProgram.DataSource = data;
                    ddlProgram.DataTextField = "ProgramTitle";
                    ddlProgram.DataValueField = "SchoolProgramId";
                    ddlProgram.DataBind();

                }
                ddlProgram.Items.Insert(0, new ListItem("--Select--", DayCarePL.Common.GUID_DEFAULT));
            }
            catch (Exception ex)
            {
            }
        }

        public static void BindSecondaryProgramName(DropDownList ddlProgram, Guid SchoolYearId)
        {
            try
            {
                DayCareBAL.AddEditChildService proxyProgram = new DayCareBAL.AddEditChildService();
                List<DayCarePL.ChildProgEnrollmentProperties> data = proxyProgram.LoadSecondaryProgram(SchoolYearId);
                if (data.Count > 0)
                {
                    ddlProgram.DataSource = data;
                    ddlProgram.DataTextField = "ProgramTitle";
                    ddlProgram.DataValueField = "SchoolProgramId";
                    ddlProgram.DataBind();
                    ddlProgram.Items.Insert(0, new ListItem("--Select--", DayCarePL.Common.GUID_DEFAULT));
                }
            }
            catch (Exception ex)
            {
            }
        }

        public static void BindFeesPeriod(DropDownList ddlFeesPeriod)
        {
            try
            {
                DayCareBAL.FeesPeriodService proxyClassRoom = new DayCareBAL.FeesPeriodService();
                DayCarePL.FeesPeriodProperties[] FeesPeriod = proxyClassRoom.LoadFeesPeriod();
                if (FeesPeriod != null && FeesPeriod.Count() > 0)
                {
                    ddlFeesPeriod.DataSource = FeesPeriod.ToList().FindAll(c => c.Active.Equals(true));
                    ddlFeesPeriod.DataTextField = "Name";
                    ddlFeesPeriod.DataValueField = "Id";
                    ddlFeesPeriod.DataBind();
                    ddlFeesPeriod.Items.Insert(0, new ListItem("--Select--", DayCarePL.Common.GUID_DEFAULT));
                }
            }
            catch
            {

            }
        }

        public static void BindStaffFromProgStaff(DropDownList ddlStaff, Guid SchoolProgramId)
        {
            try
            {
                DayCareBAL.ProgStaffService proxyProgStaff = new DayCareBAL.ProgStaffService();
                List<DayCarePL.ProgStaffProperties> lstProgStaff = proxyProgStaff.GetStaffFromProgStaffBySchoolProgram(SchoolProgramId);
                if (lstProgStaff != null && lstProgStaff.Count > 0)
                {
                    //lstProgStaff = lstProgStaff.FindAll();
                    ddlStaff.DataSource = lstProgStaff;
                    ddlStaff.DataTextField = "StaffFullName";
                    ddlStaff.DataValueField = "Id";
                    ddlStaff.DataBind();
                }
                ddlStaff.Items.Insert(0, new ListItem("--Select--", DayCarePL.Common.GUID_DEFAULT));
            }
            catch
            {

            }
        }

        public static void BindRelationship(DropDownList ddlRelationship, Guid SchoolId)
        {
            try
            {
                DayCareBAL.RelationshipService proxyRelationship = new DayCareBAL.RelationshipService();
                List<DayCarePL.RelationshipProperties> lstRelationship = proxyRelationship.LoadRelationship(SchoolId);
                ddlRelationship.Items.Clear();
                if (lstRelationship != null && lstRelationship.Count > 0)
                {
                    ddlRelationship.DataSource = lstRelationship.FindAll(r => r.Active == true);
                    ddlRelationship.DataTextField = "Name";
                    ddlRelationship.DataValueField = "Id";
                    ddlRelationship.DataBind();

                }
                ddlRelationship.Items.Insert(0, new ListItem("--Select--", DayCarePL.Common.GUID_DEFAULT));
            }
            catch
            {

            }
        }

        public static void BindAbsentReson(DropDownList ddlAbsentReason, Guid SchoolId)
        {

            try
            {
                DayCareBAL.AbsentReasonService proxyAbsentReason = new DayCareBAL.AbsentReasonService();
                DayCarePL.AbsentResonProperties[] lstRelationship = proxyAbsentReason.LoadAbsentReason(SchoolId);
                if (lstRelationship != null && lstRelationship.Count() > 0)
                {
                    ddlAbsentReason.DataSource = lstRelationship.ToList().FindAll(r => r.Active == true);
                    ddlAbsentReason.DataTextField = "Reason";
                    ddlAbsentReason.DataValueField = "Id";
                    ddlAbsentReason.DataBind();
                }
                ddlAbsentReason.Items.Insert(0, new ListItem("--Select--", DayCarePL.Common.GUID_DEFAULT));
            }
            catch
            {

            }
        }

        public static Guid GetChildSchoolYearId(Guid ChildDataId, Guid SchoolYearId)
        {
            Guid result = new Guid(DayCarePL.Common.GUID_DEFAULT);
            SqlConnection conn = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["daycareConnectionString"].ToString());
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "spGetChildSchoolYearId";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ChildDataId", ChildDataId);
                cmd.Parameters.AddWithValue("@SchoolYearId", SchoolYearId);
                object id = cmd.ExecuteScalar();
                if (id != null)
                {
                    result = new Guid(id.ToString());
                }
            }
            catch (Exception ex)
            {
            }
            return result;
        }

        public static string GetChildName(Guid ChildDataId)
        {
            bool result = true;
            SqlConnection conn = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["daycareConnectionString"].ToString());
            try
            {
                SqlDataReader dr;
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "spGetChildNameById";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ChildDataId", ChildDataId);
                object name = cmd.ExecuteScalar();
                return Convert.ToString(name);
            }
            catch (Exception ex)
            {
                result = true;
            }
            finally
            {
                conn.Close();
            }
            return string.Empty;

        }

        public static string GetFamilyName(Guid ChildFamilyId)
        {
            SqlConnection conn = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["daycareConnectionString"].ToString());
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "spGetFamilyNameById";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ChildFamilyId", ChildFamilyId);
                object name = cmd.ExecuteScalar();
                return Convert.ToString(name);
            }
            catch
            {
                return string.Empty;
            }
            finally
            {
                conn.Close();
            }


        }

        public static string GetFamilyChildName(Guid ChildFamilyId, Guid SchoolYearId)
        {
            SqlConnection conn = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["daycareConnectionString"].ToString());
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "spGetFamilywiseChildName";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ChildFamilyId", ChildFamilyId);
                cmd.Parameters.AddWithValue("@ShcoolYearId", SchoolYearId);
                object name = cmd.ExecuteScalar();
                string ChildName = Convert.ToString(name);
                if (!string.IsNullOrEmpty(ChildName))
                {
                    ChildName = "[ " + ChildName.Substring(0, ChildName.LastIndexOf(", ")) + " ]";
                }
                return Convert.ToString(ChildName);
            }
            catch
            {
                return string.Empty;
            }
            finally
            {
                conn.Close();
            }


        }

        public static void BindFamiliesWithChild(DropDownList ddlFamily, Guid SchoolId, Guid SchoolYearId)
        {
            try
            {
                DayCareBAL.ChildFamilyService proxyChildFamily = new DayCareBAL.ChildFamilyService();
                List<DayCarePL.ChildFamilyProperties> data = proxyChildFamily.GetFamiliesWithChild(SchoolId, SchoolYearId);
                if (data.Count > 0)
                {
                    ddlFamily.DataSource = data;
                    ddlFamily.DataTextField = "FamilyTitle";
                    ddlFamily.DataValueField = "Id";
                    ddlFamily.DataBind();
                    ddlFamily.Items.Insert(0, new ListItem("All", DayCarePL.Common.GUID_DEFAULT));
                }
            }
            catch (Exception ex)
            {

            }
        }

        public static string GetSchoolWiseAddress(Guid SchoolId)
        {
            if (SchoolId == new Guid("8CA767A0-5E36-4343-8B1D-5ECC40EB9E1B"))
            {
                //return "Bright Beginnings Academy, 1600 Chapel Ave. W, Suite 200, Cherry Hill, NJ 08002";
                return "1600 Chapel Ave W, #200, Cherry Hill, NJ  |  (P) 856.438.5321";
            }
            else if (SchoolId == new Guid("FA5476CD-7B7D-4030-8FCB-194E066E34B8"))
            {
                //return "315 Fries Mill Road, Sewell, NJ 08080";
                return "315 Fries Mill Road, Sewell, NJ  |  (P) 856.582.1144";
            }
            return "";
        }

        //Dt: 9May2013 By: A
        public static void GetChildProgEnrollmentFeeDetail(Guid SchoolId, Guid SchoolYearId, Guid StaffId)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.Ledger, "GetChildProgEnrollmentFeeDetail", "GetChildProgEnrollmentFeeDetail method called", DayCarePL.Common.GUID_DEFAULT);
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.Ledger, "GetChildProgEnrollmentFeeDetail", "Debug GetChildProgEnrollmentFeeDetail called", DayCarePL.Common.GUID_DEFAULT);
                DayCareBAL.LedgerService proxyLedger = new DayCareBAL.LedgerService();

                DayCareBAL.SchoolYearService proxySchoolYear = new DayCareBAL.SchoolYearService();
                if (proxySchoolYear.IsSelectedYearPrevYearORNot(SchoolId, SchoolYearId))//only prev year can only allow to edit closing balance. because in current year or in future year closing balance is not genrated in these year
                {

                    List<DayCarePL.ChildProgEnrollmentProperties> lstChildProgEnrollmentFeeDetail = proxyLedger.GetChildProgEnrollmentFeeDetail(SchoolYearId, new Guid(DayCarePL.Common.GUID_DEFAULT));
                    if (lstChildProgEnrollmentFeeDetail != null)
                    {
                        bool result = false;
                        List<DayCarePL.LedgerProperties> lstChildEnrollForLedger = new List<DayCarePL.LedgerProperties>();
                        DayCarePL.LedgerProperties objChildEnrollForLedger = null;
                        foreach (DayCarePL.ChildProgEnrollmentProperties objChildProgEnrollmentFeeDetail in lstChildProgEnrollmentFeeDetail)
                        {
                            string strDay = "";
                            DateTime StartDate = DateTime.Now, EndDate = DateTime.Now;
                            if (objChildProgEnrollmentFeeDetail.StartDate != null)
                            {
                                StartDate = objChildProgEnrollmentFeeDetail.StartDate.Value;
                            }
                            if (objChildProgEnrollmentFeeDetail.EndDate != null)
                            {
                                EndDate = objChildProgEnrollmentFeeDetail.EndDate.Value;
                            }
                            objChildEnrollForLedger = new DayCarePL.LedgerProperties();

                            #region Weekly
                            switch (objChildProgEnrollmentFeeDetail.EffectiveWeekDay)
                            {
                                case 1:
                                    {
                                        strDay = "Monday";
                                        break;
                                    }
                                case 2:
                                    {
                                        strDay = "Tuesday";
                                        break;
                                    }
                                case 3:
                                    {
                                        strDay = "Wednesday";
                                        break;
                                    }
                                case 4:
                                    {
                                        strDay = "Thursday";
                                        break;
                                    }
                                case 5:
                                    {
                                        strDay = "Friday";
                                        break;
                                    }
                            }
                            if (objChildProgEnrollmentFeeDetail.EffectiveWeekDay != null && objChildProgEnrollmentFeeDetail.EffectiveWeekDay != 0)
                            {
                                DateTime? OldDate = proxyLedger.GetLastTransactionDate(SchoolYearId, objChildProgEnrollmentFeeDetail.ChildFamilyId, objChildProgEnrollmentFeeDetail.ChildDataId, objChildProgEnrollmentFeeDetail.SchoolProgramId);
                                if (OldDate != null)
                                {
                                    DateTime LastDate = OldDate.Value;
                                    if (LastDate.Equals(new DateTime()))
                                    {
                                        LastDate = StartDate;
                                    }
                                    //else
                                    //{
                                    //    LastDate = LastDate.AddDays(7);
                                    //}
                                    while (LastDate.Date <= DateTime.Now.Date && LastDate.Date <= EndDate.Date)
                                    {
                                        if (LastDate.DayOfWeek.ToString().ToLower().Equals(strDay.ToLower()))
                                        {
                                            break;
                                        }
                                        LastDate = LastDate.AddDays(1);
                                    }
                                    DateTime TranDate;
                                    if (!LastDate.Equals(new DateTime()))
                                    {
                                        while (LastDate.Date <= DateTime.Now.Date && LastDate.Date <= EndDate.Date)
                                        {
                                            DateTime.TryParse(LastDate.Month + "/" + LastDate.Day + "/" + LastDate.Year, out TranDate);
                                            if (TranDate.Equals(new DateTime()))
                                            {
                                                TranDate = new DateTime(LastDate.Year, LastDate.Month, System.DateTime.DaysInMonth(LastDate.Year, LastDate.Month));
                                            }
                                            if (!TranDate.Equals(new DateTime()) && TranDate.Date <= System.DateTime.Now.Date && TranDate.Date <= EndDate.Date && !OldDate.Value.Date.Equals(LastDate.Date) && TranDate.Date >= StartDate.Date)
                                            {
                                                objChildEnrollForLedger = new DayCarePL.LedgerProperties();
                                                objChildEnrollForLedger.TransactionDate = TranDate + DateTime.Now.TimeOfDay;
                                                objChildEnrollForLedger.Comment = objChildProgEnrollmentFeeDetail.ProgramTitle + " " + objChildProgEnrollmentFeeDetail.FeesPeriodName;// +" " + strDay;
                                                //objChildEnrollForLedger.Detail = objChildProgEnrollmentFeeDetail.ProgramTitle + " " + objChildProgEnrollmentFeeDetail.FeesPeriodName + " " + strDay;
                                                SetLegderProperties(SchoolYearId, lstChildEnrollForLedger, objChildEnrollForLedger, objChildProgEnrollmentFeeDetail, StaffId);
                                            }
                                            LastDate = TranDate.AddDays(7);
                                        }
                                    }
                                }
                            }
                            #endregion

                            #region Monthly
                            if (objChildProgEnrollmentFeeDetail.EffectiveMonthDay != null)// && objChildProgEnrollmentFeeDetail.EffectiveMonthDay.Equals(DateTime.Now.Month))
                            {
                                DateTime? OldDate = proxyLedger.GetLastTransactionDate(SchoolYearId, objChildProgEnrollmentFeeDetail.ChildFamilyId, objChildProgEnrollmentFeeDetail.ChildDataId, objChildProgEnrollmentFeeDetail.SchoolProgramId);
                                if (OldDate != null)
                                {
                                    DateTime LastDate = OldDate.Value;
                                    if (LastDate.Equals(new DateTime()))
                                    {
                                        LastDate = StartDate;
                                    }
                                    //else
                                    //{
                                    //    LastDate = LastDate.AddMonths(1);
                                    //}
                                    DateTime TranDate;
                                    if (!LastDate.Equals(new DateTime()))
                                    {
                                        while (LastDate.Date <= DateTime.Now.Date && LastDate.Date <= EndDate.Date)
                                        {
                                            DateTime.TryParse(LastDate.Month + "/" + objChildProgEnrollmentFeeDetail.EffectiveMonthDay.Value + "/" + LastDate.Year, out TranDate);
                                            if (TranDate.Equals(new DateTime()))
                                            {
                                                TranDate = new DateTime(LastDate.Year, LastDate.Month, System.DateTime.DaysInMonth(LastDate.Year, LastDate.Month));
                                            }
                                            if (!TranDate.Equals(new DateTime()) && TranDate.Date <= System.DateTime.Now.Date && TranDate.Date <= EndDate.Date && !OldDate.Value.Date.Equals(LastDate.Date) && TranDate.Date >= StartDate.Date)
                                            {
                                                objChildEnrollForLedger = new DayCarePL.LedgerProperties();
                                                objChildEnrollForLedger.TransactionDate = TranDate + DateTime.Now.TimeOfDay;
                                                objChildEnrollForLedger.Comment = objChildProgEnrollmentFeeDetail.ProgramTitle + " " + objChildProgEnrollmentFeeDetail.FeesPeriodName;//  + " " + objChildEnrollForLedger.TransactionDate.ToShortDateString();
                                                //objChildEnrollForLedger.Detail = objChildProgEnrollmentFeeDetail.ProgramTitle + " " + objChildProgEnrollmentFeeDetail.FeesPeriodName + " " + objChildEnrollForLedger.TransactionDate.ToShortDateString();
                                                SetLegderProperties(SchoolYearId, lstChildEnrollForLedger, objChildEnrollForLedger, objChildProgEnrollmentFeeDetail, StaffId);
                                            }
                                            LastDate = TranDate.AddMonths(1);
                                        }
                                    }
                                }
                            }
                            #endregion

                            #region Yearly
                            if (objChildProgEnrollmentFeeDetail.EffectiveYearDate != null)// && objChildProgEnrollmentFeeDetail.EffectiveYearDate.Value.
                            {
                                DateTime? OldDate = proxyLedger.GetLastTransactionDate(SchoolYearId, objChildProgEnrollmentFeeDetail.ChildFamilyId, objChildProgEnrollmentFeeDetail.ChildDataId, objChildProgEnrollmentFeeDetail.SchoolProgramId);
                                if (OldDate != null)
                                {
                                    DateTime LastDate = OldDate.Value;
                                    if (LastDate.Equals(new DateTime()))
                                    {
                                        LastDate = StartDate;
                                    }
                                    //else
                                    //{
                                    //    LastDate = LastDate.AddYears(1);
                                    //}
                                    DateTime TranDate;
                                    if (!LastDate.Equals(new DateTime()))
                                    {
                                        while (LastDate.Date <= System.DateTime.Now.Date && LastDate.Date <= EndDate.Date)
                                        {
                                            DateTime.TryParse(objChildProgEnrollmentFeeDetail.EffectiveYearDate.Value.Month + "/" + objChildProgEnrollmentFeeDetail.EffectiveYearDate.Value.Day + "/" + LastDate.Year, out TranDate);
                                            if (!TranDate.Equals(new DateTime()) && TranDate.Date <= System.DateTime.Now.Date && TranDate.Date <= EndDate.Date && !OldDate.Value.Date.Equals(LastDate.Date) && TranDate.Date >= StartDate.Date)
                                            {
                                                objChildEnrollForLedger = new DayCarePL.LedgerProperties();
                                                objChildEnrollForLedger.TransactionDate = TranDate + DateTime.Now.TimeOfDay;
                                                objChildEnrollForLedger.Comment = objChildProgEnrollmentFeeDetail.ProgramTitle + " " + objChildProgEnrollmentFeeDetail.FeesPeriodName;//  + " " + objChildEnrollForLedger.TransactionDate.ToShortDateString();
                                                //objChildEnrollForLedger.Detail = objChildProgEnrollmentFeeDetail.ProgramTitle + " " + objChildProgEnrollmentFeeDetail.FeesPeriodName + " " + objChildEnrollForLedger.TransactionDate.ToShortDateString();
                                                SetLegderProperties(SchoolYearId, lstChildEnrollForLedger, objChildEnrollForLedger, objChildProgEnrollmentFeeDetail, StaffId);
                                            }
                                            LastDate = TranDate.AddYears(1);
                                        }
                                    }
                                }
                            }
                            #endregion

                            #region One Time
                            if (objChildProgEnrollmentFeeDetail.EffectiveMonthDay == null && (objChildProgEnrollmentFeeDetail.EffectiveWeekDay == null || objChildProgEnrollmentFeeDetail.EffectiveWeekDay == 0) && objChildProgEnrollmentFeeDetail.EffectiveYearDate == null)
                            {
                                DateTime? OldDate = proxyLedger.GetLastTransactionDate(SchoolYearId, objChildProgEnrollmentFeeDetail.ChildFamilyId, objChildProgEnrollmentFeeDetail.ChildDataId, objChildProgEnrollmentFeeDetail.SchoolProgramId);
                                if (OldDate != null)
                                {
                                    DateTime LastDate = OldDate.Value;
                                    if (LastDate.Equals(new DateTime()))
                                    {
                                        LastDate = StartDate;
                                    }
                                    //else
                                    //{
                                    //    LastDate = LastDate.AddMonths(1);
                                    //}
                                    if (!LastDate.Equals(new DateTime()))
                                    {
                                        if (LastDate.Date <= DateTime.Now.Date)
                                        {
                                            if (OldDate.Value.Equals(new DateTime()))
                                            {
                                                objChildEnrollForLedger = new DayCarePL.LedgerProperties();
                                                objChildEnrollForLedger.TransactionDate = LastDate.Date + DateTime.Now.TimeOfDay;
                                                objChildEnrollForLedger.Comment = objChildProgEnrollmentFeeDetail.ProgramTitle + " " + objChildProgEnrollmentFeeDetail.FeesPeriodName;//  + " " + objChildEnrollForLedger.TransactionDate.ToShortDateString();
                                                //objChildEnrollForLedger.Detail = objChildProgEnrollmentFeeDetail.ProgramTitle + " " + objChildProgEnrollmentFeeDetail.FeesPeriodName + " " + objChildEnrollForLedger.TransactionDate.ToShortDateString();
                                                SetLegderProperties(SchoolYearId, lstChildEnrollForLedger, objChildEnrollForLedger, objChildProgEnrollmentFeeDetail, StaffId);
                                            }
                                        }
                                    }
                                }
                            }
                            #endregion
                        }
                        if (lstChildEnrollForLedger != null && lstChildEnrollForLedger.Count > 0)
                            result = proxyLedger.Save(lstChildEnrollForLedger);
                    }
                }
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.LedgerOfFamily, "GetChildProgEnrollmentFeeDetail", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
            }
        }
        //call from "GetChildProgEnrollmentFeeDetail" to set necessary properties.
        public static void SetLegderProperties(Guid SchoolYearId, List<DayCarePL.LedgerProperties> lstChildEnrollForLedger, DayCarePL.LedgerProperties objChildEnrollForLedger, DayCarePL.ChildProgEnrollmentProperties objChildProgEnrollmentFeeDetail, Guid StaffId)
        {
            objChildEnrollForLedger.ChildFamilyId = objChildProgEnrollmentFeeDetail.ChildFamilyId;
            objChildEnrollForLedger.SchoolYearId = SchoolYearId;
            objChildEnrollForLedger.ChildDataId = objChildProgEnrollmentFeeDetail.ChildDataId;
            objChildEnrollForLedger.Debit = objChildProgEnrollmentFeeDetail.Fees.Value;
            objChildEnrollForLedger.Credit = 0;
            objChildEnrollForLedger.AllowEdit = false;
            objChildEnrollForLedger.PaymentId = null;
            objChildEnrollForLedger.CreatedById = StaffId;
            objChildEnrollForLedger.CreatedDateTime = DateTime.Now;
            objChildEnrollForLedger.LastModifiedById = StaffId;
            objChildEnrollForLedger.LastModifiedDatetime = DateTime.Now;
            objChildEnrollForLedger.SchoolProgramId = objChildProgEnrollmentFeeDetail.SchoolProgramId;
            lstChildEnrollForLedger.Add(objChildEnrollForLedger);
        }

    }
}
