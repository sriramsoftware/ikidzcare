using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace DayCareDAL
{
    public class clChildSchedule
    {
        #region check wether program is primary or not in child schedule, Dt: 25-Aug-2011, DB: A"
        public static bool IsPrimaryProgramInChildSchedule(Guid ChildSchoolYearId)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clChildSchedule, "IsPrimaryProgramInChildSchedule", "Execute IsPrimaryProgramInChildSchedule Method", DayCarePL.Common.GUID_DEFAULT);
            SqlConnection conn = clConnection.CreateConnection();
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clChildSchedule, "IsPrimaryProgramInChildSchedule", "Debug IsPrimaryProgramInChildSchedule Method", DayCarePL.Common.GUID_DEFAULT);
                clConnection.OpenConnection(conn);
                SqlCommand cmd = clConnection.CreateCommand("spIsPrimaryProgramInChildSchedule", conn);
                cmd.Parameters.Add(clConnection.GetInputParameter("@ChildSchoolYearId", ChildSchoolYearId));
                cmd.Parameters.Add(clConnection.GetOutputParameter("@Status", SqlDbType.Bit));
                cmd.ExecuteNonQuery();
                if (Convert.ToBoolean(cmd.Parameters["@Status"].Value))
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
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clChildSchedule, "IsPrimaryProgramInChildSchedule", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                return false;
            }
        }
        #endregion

        #region check selected program is primary or not , Dt: 25-Aug-2011, DB: A"
        public static bool CheckProgramIdPrimaryOrNotForChildSchedule(Guid SchoolProgramId)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clChildSchedule, "CheckProgramIdPrimaryOrNotForChildSchedule", "Execute CheckProgramIdPrimaryOrNotForChildSchedule Method", DayCarePL.Common.GUID_DEFAULT);
            SqlConnection conn = clConnection.CreateConnection();
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clChildSchedule, "CheckProgramIdPrimaryOrNotForChildSchedule", "Debug CheckProgramIdPrimaryOrNotForChildSchedule Method", DayCarePL.Common.GUID_DEFAULT);
                clConnection.OpenConnection(conn);
                SqlCommand cmd = clConnection.CreateCommand("spCheckProgramIdPrimaryOrNotForChildSchedule", conn);
                cmd.Parameters.Add(clConnection.GetInputParameter("@SchoolProgramId", SchoolProgramId));
                cmd.Parameters.Add(clConnection.GetOutputParameter("@Status", SqlDbType.Bit));
                cmd.ExecuteNonQuery();
                if (Convert.ToBoolean(cmd.Parameters["@Status"].Value))
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
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clChildSchedule, "CheckProgramIdPrimaryOrNotForChildSchedule", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                return false;
            }
        }
        #endregion

        #region"Save Child Schedule, Dt: 25-Aug-2011, Db: A"
        public static bool Save(DayCarePL.ChildScheduleProperties objChildSchedule)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clChildSchedule, "Save", "Execute Save Method", DayCarePL.Common.GUID_DEFAULT);
            SqlConnection conn = clConnection.CreateConnection();
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clChildSchedule, "Save", "Debug Save Method", DayCarePL.Common.GUID_DEFAULT);
                clConnection.OpenConnection(conn);
                SqlCommand cmd;
                if (objChildSchedule.Id.ToString().Equals(DayCarePL.Common.GUID_DEFAULT))
                {
                    cmd = clConnection.CreateCommand("spAddChildSchedule", conn);
                    cmd.Parameters.Add(clConnection.GetInputParameter("@CreatedDateTime", DateTime.Now));
                    cmd.Parameters.Add(clConnection.GetInputParameter("@CreatedById", objChildSchedule.CreatedById));
                }
                else
                {
                    cmd = clConnection.CreateCommand("spUpdateChildSchedule", conn);
                }
                if (!objChildSchedule.Id.ToString().Equals(DayCarePL.Common.GUID_DEFAULT))
                {
                    cmd.Parameters.Add(clConnection.GetInputParameter("@Id", objChildSchedule.Id));
                }
                cmd.Parameters.Add(clConnection.GetInputParameter("@ChildSchoolYearId", objChildSchedule.ChildSchoolYearId));
                cmd.Parameters.Add(clConnection.GetInputParameter("@SchoolProgramId", objChildSchedule.SchoolProgramId));
                //cmd.Parameters.Add(clConnection.GetInputParameter("@ProgClassCategoryId", objChildSchedule.ProgClassCategoryId));
                cmd.Parameters.Add(clConnection.GetInputParameter("@ProgClassRoomId", objChildSchedule.ProgClassRoomId));
                cmd.Parameters.Add(clConnection.GetInputParameter("@ProgScheduleId", objChildSchedule.ProgScheduleId));
                cmd.Parameters.Add(clConnection.GetInputParameter("@BeginDate", objChildSchedule.BeginDate));
                cmd.Parameters.Add(clConnection.GetInputParameter("@EndDate", objChildSchedule.EndDate));
                cmd.Parameters.Add(clConnection.GetInputParameter("@Discount", objChildSchedule.Discount));
                cmd.Parameters.Add(clConnection.GetInputParameter("@LastModifiedById", objChildSchedule.LastModifiedById));
                cmd.Parameters.Add(clConnection.GetInputParameter("@LastModifiedDateTime", DateTime.Now));
                cmd.Parameters.Add(clConnection.GetOutputParameter("@status", SqlDbType.Bit));
                cmd.ExecuteNonQuery();
                if (Convert.ToBoolean(cmd.Parameters["@status"].Value))
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clChildSchedule, "Save", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                return false;
            }
            finally
            {
                clConnection.CloseConnection(conn);
            }
        }
        #endregion

        #region check duplicate child schedule , Dt: 25-Aug-2011, DB: A"
        public static bool CheckDupicateChildSchedule(Guid ChildSchoolYearId, Guid SchoolProgramId,  Guid ProgScheduleId)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clChildSchedule, "CheckDupicateChildSchedule", "Execute CheckDupicateChildSchedule Method", DayCarePL.Common.GUID_DEFAULT);
            SqlConnection conn = clConnection.CreateConnection();
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clChildSchedule, "CheckDupicateChildSchedule", "Debug CheckDupicateChildSchedule Method", DayCarePL.Common.GUID_DEFAULT);
                clConnection.OpenConnection(conn);
                SqlCommand cmd = clConnection.CreateCommand("spCheckDuplicateChildSchedule", conn);
                cmd.Parameters.Add(clConnection.GetInputParameter("@ChildSchoolYearId", ChildSchoolYearId));
                cmd.Parameters.Add(clConnection.GetInputParameter("@SchoolProgramId", SchoolProgramId));
                //cmd.Parameters.Add(clConnection.GetInputParameter("@ProgClassCategoryId", ProgClassCategoryId));
                cmd.Parameters.Add(clConnection.GetInputParameter("@ProgScheduleId", ProgScheduleId));
                cmd.Parameters.Add(clConnection.GetOutputParameter("@Status", SqlDbType.Bit));
                cmd.ExecuteNonQuery();
                if (Convert.ToBoolean(cmd.Parameters["@Status"].Value))
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
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clChildSchedule, "CheckDupicateChildSchedule", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                return false;
            }
        }
        #endregion

        #region "Load Child Schedule, Dt: 25-Aug-2011, DB: A"
        public static List<DayCarePL.ChildScheduleProperties> LoadChildSchedule(Guid ChildSchoolYearId)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clChildSchedule, "LoadChildSchedule", "Execute LoadChildSchedule Method", DayCarePL.Common.GUID_DEFAULT);
            clConnection.DoConnection();
            DayCareDataContext db = new DayCareDataContext();
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clChildSchedule, "LoadChildSchedule", "Debug LoadChildSchedule Method", DayCarePL.Common.GUID_DEFAULT);
                List<DayCarePL.ChildScheduleProperties> lstChildSchedule = new List<DayCarePL.ChildScheduleProperties>();
                DayCarePL.ChildScheduleProperties objChildSchedule;
                var data = db.spGetChildScheduleList(ChildSchoolYearId);
                foreach (var d in data)
                {
                    objChildSchedule = new DayCarePL.ChildScheduleProperties();
                    objChildSchedule.Id = d.Id;
                    objChildSchedule.ChildSchoolYearId = d.ChildSchoolYearId;
                    objChildSchedule.SchoolProgramId = d.SchoolProgramId;
                    objChildSchedule.SchoolProgramTitle = d.SchoolProgram;
                    //objChildSchedule.ProgClassCategoryId = d.ProgClassCategoryId;
                    //objChildSchedule.ProgClassCategoryName = d.ProgClassCategory;
                    objChildSchedule.StaffFullName = d.FullName;
                    objChildSchedule.ProgClassRoomId = d.ProgClassRoomId;
                    objChildSchedule.ProgClassRoomName = d.ProgClassRoom;
                    objChildSchedule.Discount = Convert.ToDouble(d.Discount);
                    objChildSchedule.ProgScheduleId = d.ProgScheduleId;
                    objChildSchedule.ProgScheduleDay = d.Day;
                    objChildSchedule.ProgScheduleBeginTime = d.BeginTime;
                    objChildSchedule.ProgScheduleEndTime = d.EndTime.Value;
                    objChildSchedule.BeginDate = d.BeginDate;
                    objChildSchedule.EndDate = d.EndDate.Value;
                    objChildSchedule.CreatedById = d.CreatedById;
                    objChildSchedule.CreatedDateTime = d.CreatedDateTime;
                    objChildSchedule.LastModifiedById = d.LastModifiedById;
                    objChildSchedule.LastModifiedDatetime = d.LastModifiedDatetime;
                    lstChildSchedule.Add(objChildSchedule);
                }
                return lstChildSchedule;
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clChildSchedule, "LoadChildSchedule", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                return null;
            }
        }
        #endregion

        #region "Load Child Schedule By Child Schedule Id, Dt: 12-Aug-2011, DB: A"
        public static DayCarePL.ChildScheduleProperties LoadChildScheduleById(Guid ChildScheduleId, Guid ChildSchoolYearId)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clChildSchedule, "LoadChildScheduleById", "Execute LoadChildScheduleById Method", DayCarePL.Common.GUID_DEFAULT);
            clConnection.DoConnection();
            DayCareDataContext db = new DayCareDataContext();
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clChildSchedule, "LoadChildScheduleById", "Debug LoadChildScheduleById Method", DayCarePL.Common.GUID_DEFAULT);
                List<DayCarePL.ChildScheduleProperties> lstChildSchedule = new List<DayCarePL.ChildScheduleProperties>();
                DayCarePL.ChildScheduleProperties objChildSchedule = null;
                var data = db.spGetChildScheduleById(ChildScheduleId, ChildSchoolYearId);
                foreach (var d in data)
                {
                    objChildSchedule = new DayCarePL.ChildScheduleProperties();
                    objChildSchedule.Id = d.Id;
                    objChildSchedule.ChildSchoolYearId = d.ChildSchoolYearId;
                    objChildSchedule.SchoolProgramId = d.SchoolProgramId;
                    objChildSchedule.SchoolProgramTitle = d.SchoolProgram;
                   // objChildSchedule.ProgClassCategoryId = d.ProgClassCategoryId;
                   // objChildSchedule.ProgClassCategoryName = d.ProgClassCategory;
                    objChildSchedule.ProgClassRoomId = d.ProgClassRoomId;
                    objChildSchedule.ProgClassRoomName = d.ProgClassRoom;
                    objChildSchedule.StaffFullName = d.FullName;
                    objChildSchedule.ProgScheduleId = d.ProgScheduleId;
                    objChildSchedule.ProgScheduleDay = d.Day;
                    objChildSchedule.ProgScheduleBeginTime = d.BeginTime;
                    objChildSchedule.ProgScheduleEndTime = d.EndTime.Value;
                    objChildSchedule.BeginDate = d.BeginDate;
                    objChildSchedule.EndDate = d.EndDate.Value;
                    objChildSchedule.Discount = Convert.ToDouble(d.Discount);
                    objChildSchedule.CreatedById = d.CreatedById;
                    objChildSchedule.CreatedDateTime = d.CreatedDateTime;
                    objChildSchedule.LastModifiedById = d.LastModifiedById;
                    objChildSchedule.LastModifiedDatetime = d.LastModifiedDatetime;
                }
                return objChildSchedule;
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clChildSchedule, "LoadChildScheduleById", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                return null;
            }
        }
        #endregion

        #region Available class for child , Dt: 29-Aug-2011, DB: A"
        public static bool CheckAvailableClassForChild(Guid SchoolId, Guid ProgScheduleId, Guid ChildSchoolYearId, Guid SchoolProgramId)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clChildSchedule, "CheckAvailableClassForChild", "Execute CheckAvailableClassForChild Method", DayCarePL.Common.GUID_DEFAULT);
            clConnection.DoConnection();
            SqlConnection conn = clConnection.CreateConnection();

            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clChildSchedule, "CheckAvailableClassForChild", "Debug CheckAvailableClassForChild Method", DayCarePL.Common.GUID_DEFAULT);
                clConnection.OpenConnection(conn);
                SqlCommand cmd = clConnection.CreateCommand("spCheckAvailableClassForChild", conn);
                cmd.Parameters.Add(clConnection.GetInputParameter("@ProgScheduleId", ProgScheduleId));
                cmd.Parameters.Add(clConnection.GetInputParameter("@SchoolId", SchoolId));
                cmd.Parameters.Add(clConnection.GetInputParameter("@ChildSchoolYearId", ChildSchoolYearId));
                cmd.Parameters.Add(clConnection.GetInputParameter("@SchoolProgramId", SchoolProgramId));
                cmd.Parameters.Add(clConnection.GetOutputParameter("@status", SqlDbType.Int));
                cmd.ExecuteNonQuery();
                if (Convert.ToBoolean(cmd.Parameters["@status"].Value))
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
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clChildSchedule, "CheckAvailableClassForChild", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                return false;
            }
        }
        #endregion

        #region get count child data, Dt: 29-Aug-2011, DB: A"
        public static int GetCountChildData(Guid ChildSchoolYearId)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clChildSchedule, "GetCountChildData", "Execute GetCountChildData Method", DayCarePL.Common.GUID_DEFAULT);
            SqlConnection conn = clConnection.CreateConnection();
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clChildSchedule, "GetCountChildData", "Debug GetCountChildData Method", DayCarePL.Common.GUID_DEFAULT);
                clConnection.OpenConnection(conn);
                SqlCommand cmd = clConnection.CreateCommand("spGetCountChildSchedule", conn);
                cmd.Parameters.Add(clConnection.GetInputParameter("@ChildSchoolYearId", ChildSchoolYearId));
                int result = Convert.ToInt32(cmd.ExecuteScalar());
                return result;
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clChildSchedule, "GetCountChildData", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                return 0;
            }
        }
        #endregion
    }
}
