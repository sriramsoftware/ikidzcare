using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace DayCareDAL
{
    public class clProgSchedule
    {
        #region "Load Prog Schedule, Dt: 10-Aug-2011, DB: A"
        public static List<DayCarePL.ProgScheduleProperties> LoadProgSchedule(Guid SchoolId,Guid SchoolProgramId)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clProgSchedule, "LoadProgSchedule", "Execute LoadProgSchedule Method", DayCarePL.Common.GUID_DEFAULT);
            SqlConnection conn = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["daycareConnectionString"].ToString());

            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clProgSchedule, "LoadProgSchedule", "Debug LoadProgSchedule Method", DayCarePL.Common.GUID_DEFAULT);


                List<DayCarePL.ProgScheduleProperties> lstProgSchedule = new List<DayCarePL.ProgScheduleProperties>();
                DayCarePL.ProgScheduleProperties objProgSchedule;
                if (conn.State == System.Data.ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "spGetProgScheduleList";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = conn;
                cmd.Parameters.Add(new SqlParameter("@SchoolId", SqlDbType.UniqueIdentifier)).Value = SchoolId;
                cmd.Parameters.Add(new SqlParameter("@SchoolProgramId", SqlDbType.UniqueIdentifier)).Value =SchoolProgramId;

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    objProgSchedule = new DayCarePL.ProgScheduleProperties();
                    objProgSchedule.Id = new Guid(dr["Id"].ToString());
                    objProgSchedule.SchoolProgramId = new Guid(dr["SchoolProgramId"].ToString());
                    objProgSchedule.SchoolProgramTitle = dr["Title"].ToString();
                    objProgSchedule.Day = dr["Day"].ToString();
                    objProgSchedule.DayIndex = Convert.ToInt16(dr["DayIndex"].ToString());
                    objProgSchedule.BeginTime = Convert.ToDateTime(dr["BeginTime"].ToString());
                    objProgSchedule.ClassRoomName = Convert.ToString(dr["ClasroomTitle"]);
                    objProgSchedule.ProgClassRoomId = new Guid(dr["ProgClassRoomId"].ToString());
                    
                    if(dr["EndTime"]!=DBNull.Value)
                    {
                        objProgSchedule.EndTime = Convert.ToDateTime(dr["EndTime"].ToString());
                    }
                    objProgSchedule.Active = Convert.ToBoolean(dr["Active"].ToString());
                    if (dr["CreatedById"] != DBNull.Value)
                    {
                        objProgSchedule.CreatedById = new Guid(dr["CreatedById"].ToString());
                    }
                    if (dr["CreatedDateTime"] != DBNull.Value)
                    {
                        objProgSchedule.CreatedDateTime = Convert.ToDateTime(dr["CreatedDateTime"].ToString());
                    }
                    if (dr["LastModifiedById"] != DBNull.Value)
                    {
                        objProgSchedule.LastModifiedById = new Guid(dr["LastModifiedById"].ToString());
                    }
                    if (dr["LastModifiedDatetime"] != DBNull.Value)
                    {
                        objProgSchedule.LastModifiedDatetime = Convert.ToDateTime(dr["LastModifiedDatetime"].ToString());
                    }
                    lstProgSchedule.Add(objProgSchedule);
                }
                return lstProgSchedule;
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clProgSchedule, "LoadProgSchedule", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                return null;
            }
        }
        #endregion

        #region"Save Prog Schedule, Dt:17-Aug-2011, Db:V"
        public static bool Save(DayCarePL.ProgScheduleProperties objProgSchedule)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clProgSchedule, "Save", "Execute Save Method", DayCarePL.Common.GUID_DEFAULT);
            SqlConnection conn=clConnection.CreateConnection();
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clProgSchedule, "Save", "Debug Save Method", DayCarePL.Common.GUID_DEFAULT);
                clConnection.OpenConnection(conn);
                SqlCommand cmd;
                if (objProgSchedule.Id.ToString().Equals(DayCarePL.Common.GUID_DEFAULT))
                {
                    cmd = clConnection.CreateCommand("spAddProgSchedule", conn);
                    cmd.Parameters.Add(clConnection.GetInputParameter("@CreatedDateTime", DateTime.Now));
                    cmd.Parameters.Add(clConnection.GetInputParameter("@CreatedById", objProgSchedule.CreatedById));
                }
                else
                {
                    cmd = clConnection.CreateCommand("spUpdateProgSchedule", conn);
                }
                if (!objProgSchedule.Id.ToString().Equals(DayCarePL.Common.GUID_DEFAULT))
                {
                    cmd.Parameters.Add(clConnection.GetInputParameter("@Id", objProgSchedule.Id));
                }
                cmd.Parameters.Add(clConnection.GetInputParameter("@ProgClassRoomId", objProgSchedule.ProgClassRoomId));
                cmd.Parameters.Add(clConnection.GetInputParameter("@SchoolProgramId", objProgSchedule.SchoolProgramId));
                cmd.Parameters.Add(clConnection.GetInputParameter("@Day", objProgSchedule.Day));
                cmd.Parameters.Add(clConnection.GetInputParameter("@BeginTime", objProgSchedule.BeginTime));
                cmd.Parameters.Add(clConnection.GetInputParameter("@EndTime", objProgSchedule.EndTime));
                cmd.Parameters.Add(clConnection.GetInputParameter("@Active", objProgSchedule.Active));
                cmd.Parameters.Add(clConnection.GetInputParameter("@DayIndex", objProgSchedule.DayIndex));
                cmd.Parameters.Add(clConnection.GetInputParameter("@LastModifiedById", objProgSchedule.LastModifiedById));
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
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clProgSchedule, "Save", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                return false;
            }
            finally
            {
                clConnection.CloseConnection(conn);
            }
        }
        #endregion

        #region "Load ProgClassRoom ID and Name Display,Dt:24-Aug-2011,Db:V"
        public static List<DayCarePL.ProgScheduleProperties> LoadProgClassRoom(Guid SchoolProgramId)
        {
            DayCareDataContext db = new DayCareDataContext();
            List<DayCarePL.ProgScheduleProperties> lstProgSchedule = new List<DayCarePL.ProgScheduleProperties>();
            DayCarePL.ProgScheduleProperties objProgSchedule = null;
            try
            {
                var data = db.spGetProgClassRoomIdAndClassRoomName(SchoolProgramId);
                foreach (var d in data)
                {
                    objProgSchedule = new DayCarePL.ProgScheduleProperties();
                    objProgSchedule.Id = d.Id;
                    objProgSchedule.ProgClassRoomTitle = d.ClassRoomTitle;
                    objProgSchedule.ClassRoomId = d.ClassRoomId;
                    lstProgSchedule.Add(objProgSchedule);
                    //objProgSchedule = null;
                }
            }
            catch (Exception ex)
            { 
            
            }
            return lstProgSchedule;
        }
        #endregion

        #region "Load Prog Schedule For Child Schedule, Dt: 24-Aug-2011, DB: A"
        public static List<DayCarePL.ProgScheduleProperties> LoadProgScheduleForChildSchedule(Guid SchoolId, Guid SchoolProgramId)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clProgSchedule, "LoadProgScheduleForChildSchedule", "Execute LoadProgScheduleForChildSchedule Method", DayCarePL.Common.GUID_DEFAULT);
            clConnection.DoConnection();
            DayCareDataContext db = new DayCareDataContext();
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clProgSchedule, "LoadProgScheduleForChildSchedule", "Debug LoadProgScheduleForChildSchedule Method", DayCarePL.Common.GUID_DEFAULT);
                List<DayCarePL.ProgScheduleProperties> lstProgSchedule = new List<DayCarePL.ProgScheduleProperties>();
                DayCarePL.ProgScheduleProperties objProgSchedule;
                var data = db.spGetProgScheduleForChildSchedule(SchoolProgramId);
                foreach (var d in data)
                {
                    objProgSchedule = new DayCarePL.ProgScheduleProperties();
                    objProgSchedule.Id = d.Id;
                    objProgSchedule.Day = d.Day;
                    objProgSchedule.BeginTime = d.BeginTime;
                    if (d.EndTime != null)
                    {
                        objProgSchedule.EndTime = d.EndTime;
                    }
                    objProgSchedule.ClassRoomName = d.ClassRoomName;
                    objProgSchedule.FullName = d.FullName;
                    objProgSchedule.ProgClassRoomId = d.ProgClassRoomId.Value;
                    lstProgSchedule.Add(objProgSchedule);
                }

                return lstProgSchedule;
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clProgSchedule, "LoadProgScheduleForChildSchedule", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                return null;
            }
        }
        #endregion

        #region "Check Duplicate ProgClassRoomId And BeginTime,EndTime Should be Unique"        
        public static DayCarePL.ProgScheduleProperties CheckDuplicateProgClassRoom(Guid ClassRoomId, DateTime BeginTime, DateTime EndTime, Int32 DayIndex,Guid Id)
        {
            bool result = false;
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clProgSchedule, "CheckDuplicateProgClassRoom", "Execute CheckDuplicateProgClassRoom Method", DayCarePL.Common.GUID_DEFAULT);
            clConnection.DoConnection();
            DayCarePL.ProgScheduleProperties objProgSchedule = null;
            DayCareDataContext db = new DayCareDataContext();
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clProgSchedule, "CheckDuplicateProgClassRoom", "Debug CheckDuplicateProgClassRoom Method", DayCarePL.Common.GUID_DEFAULT);

                var data = db.spGetCheckDuplicateProgClassRoomId(ClassRoomId, BeginTime, EndTime, DayIndex, Id);
               
                foreach (var c in data)
                {
                    result = true;  
                    objProgSchedule = new DayCarePL.ProgScheduleProperties();
                    objProgSchedule.SchoolProgramTitle = c.Program;
                    objProgSchedule.ProgClassRoomTitle = c.ClassRoomTitle;
                    objProgSchedule.BeginTime = c.BeginTime;
                    objProgSchedule.EndTime = c.EndTime;
                    objProgSchedule.Day = c.Day;
                    
                    break;
                }
                return objProgSchedule;
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clProgSchedule, "CheckDuplicateProgClassRoom", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                return null;
            }
        }
        #endregion

        #region "Check ProgSchedule In BeginTime And EndTime Should be Check on Hours Of Opration OpenTime And CloseTime, Dt:30-Aug-2011, Db:V"
        public static DayCarePL.ProgScheduleProperties CheckBeginTimeAndEndTime(Guid SchoolId, Int32 DayIndex,DateTime BeginTime,DateTime EndTime)
        {
            bool result = false;
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clProgSchedule, "CheckBeginTimeAndEndTime", "Execute CheckBeginTimeAndEndTime Method", DayCarePL.Common.GUID_DEFAULT);
            clConnection.DoConnection();
            DayCarePL.ProgScheduleProperties objProgSchedule = null;
            DayCareDataContext db = new DayCareDataContext();
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clProgSchedule, "CheckBeginTimeAndEndTime", "Debug CheckBeginTimeAndEndTime Method", DayCarePL.Common.GUID_DEFAULT);
                var data = db.spGetCheckBeginTimeAndEndTimeInHoursOfOpration(DayIndex, SchoolId, BeginTime, EndTime);
                foreach (var c in data)
                {
                    result = true;
                    objProgSchedule = new DayCarePL.ProgScheduleProperties();
                    break;
                
                }
                return objProgSchedule;
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clProgSchedule, "CheckBeginTimeAndEndTime", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                return null;
            }
        }
        #endregion
    }
}
