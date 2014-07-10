using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace DayCareDAL
{
    public class clProgClassRoom
    {
        #region "Load Prog. ClassRoom, Dt: 12-Aug-2011, DB: A"
        public static List<DayCarePL.ProgClassRoomProperties> LoadProgClassRoom(Guid SchoolId, Guid SchoolProgramId)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clProgClassRoom, "LoadProgClassRoom", "Execute LoadProgClassRoom Method", DayCarePL.Common.GUID_DEFAULT);
            SqlConnection conn = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["daycareConnectionString"].ToString());
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clProgClassRoom, "LoadProgClassRoom", "Debug LoadProgClassRoom Method", DayCarePL.Common.GUID_DEFAULT);
                List<DayCarePL.ProgClassRoomProperties> lstProgClassRoom = new List<DayCarePL.ProgClassRoomProperties>();
                DayCarePL.ProgClassRoomProperties objProgClassRoom;
                if (conn.State == System.Data.ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "spGetClassRoomSchoolProgram";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = conn;
                cmd.Parameters.Add(new SqlParameter("@SchoolId", SqlDbType.UniqueIdentifier)).Value = SchoolId;
                cmd.Parameters.Add(new SqlParameter("@SchoolProgramId", SqlDbType.UniqueIdentifier)).Value = SchoolProgramId;
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    objProgClassRoom = new DayCarePL.ProgClassRoomProperties();
                    objProgClassRoom.Id = new Guid(dr["ProgClassRoom_Id"].ToString());
                    //objProgClassRoom.ProgClassRoom_Id = new Guid(dr["ClassRoomId"].ToString());
                    objProgClassRoom.SchoolProgramId = new Guid(dr["SchoolProgramId"].ToString());
                    objProgClassRoom.ClassRoomId = new Guid(dr["ClassRoomId"].ToString());
                    objProgClassRoom.ClassRoomName = dr["ClassRoomName"].ToString();
                    objProgClassRoom.Active = Convert.ToBoolean(dr["Active"].ToString());
                    objProgClassRoom.ClassRoom_Active = Convert.ToBoolean(dr["ClassRoom_Active"].ToString());
                    if (dr["ProgClassRoom_CreatedById"] != DBNull.Value)
                    {
                        objProgClassRoom.CreatedById = new Guid(dr["ProgClassRoom_CreatedById"].ToString());
                    }
                    if (dr["ProgClassRoom_CreatedDateTime"] != DBNull.Value)
                    {
                        objProgClassRoom.CreatedDateTime = Convert.ToDateTime(dr["ProgClassRoom_CreatedDateTime"].ToString());
                    }
                    if (dr["ProgClassRoom_LastModifiedById"] != DBNull.Value)
                    {
                        objProgClassRoom.LastModifiedById = new Guid(dr["ProgClassRoom_LastModifiedById"].ToString());
                    }
                    if (dr["ProgClassRoom_LastModifiedDatetime"] != DBNull.Value)
                    {
                        objProgClassRoom.LastModifiedDatetime = Convert.ToDateTime(dr["ProgClassRoom_LastModifiedDatetime"].ToString());
                    }
                    objProgClassRoom.Active = Convert.ToBoolean(dr["Active"].ToString());
                    lstProgClassRoom.Add(objProgClassRoom);
                }
                return lstProgClassRoom;
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clProgClassRoom, "LoadProgClassRoom", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                return null;
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }
        #endregion

        #region "Load Prog. ClassRoom, Dt: 12-Aug-2011, DB: A"
        public static List<DayCarePL.ProgClassRoomProperties> LoadProgClassRoom(Guid SchoolId, Guid SchoolProgramId, Guid SchoolYearId)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clProgClassRoom, "LoadProgClassRoom", "Execute LoadProgClassRoom Method", DayCarePL.Common.GUID_DEFAULT);
            SqlConnection conn = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["daycareConnectionString"].ToString());
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clProgClassRoom, "LoadProgClassRoom", "Debug LoadProgClassRoom Method", DayCarePL.Common.GUID_DEFAULT);
                List<DayCarePL.ProgClassRoomProperties> lstProgClassRoom = new List<DayCarePL.ProgClassRoomProperties>();
                DayCarePL.ProgClassRoomProperties objProgClassRoom;
                if (conn.State == System.Data.ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "spGetClassRoomSchoolProgram";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = conn;
                cmd.Parameters.Add(new SqlParameter("@SchoolId", SqlDbType.UniqueIdentifier)).Value = SchoolId;
                cmd.Parameters.Add(new SqlParameter("@SchoolProgramId", SqlDbType.UniqueIdentifier)).Value = SchoolProgramId;
                cmd.Parameters.Add(new SqlParameter("@SchoolYearId", SqlDbType.UniqueIdentifier)).Value = SchoolYearId;
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    objProgClassRoom = new DayCarePL.ProgClassRoomProperties();
                    objProgClassRoom.Id = new Guid(dr["ProgClassRoom_Id"].ToString());
                    //objProgClassRoom.ProgClassRoom_Id = new Guid(dr["ClassRoomId"].ToString());
                    objProgClassRoom.SchoolProgramId = new Guid(dr["SchoolProgramId"].ToString());
                    objProgClassRoom.ClassRoomId = new Guid(dr["ClassRoomId"].ToString());
                    objProgClassRoom.ClassRoomName = dr["ClassRoomName"].ToString();
                    objProgClassRoom.Active = Convert.ToBoolean(dr["Active"].ToString());
                    objProgClassRoom.ClassRoom_Active = Convert.ToBoolean(dr["ClassRoom_Active"].ToString());
                    if (dr["ProgClassRoom_CreatedById"] != DBNull.Value)
                    {
                        objProgClassRoom.CreatedById = new Guid(dr["ProgClassRoom_CreatedById"].ToString());
                    }
                    if (dr["ProgClassRoom_CreatedDateTime"] != DBNull.Value)
                    {
                        objProgClassRoom.CreatedDateTime = Convert.ToDateTime(dr["ProgClassRoom_CreatedDateTime"].ToString());
                    }
                    if (dr["ProgClassRoom_LastModifiedById"] != DBNull.Value)
                    {
                        objProgClassRoom.LastModifiedById = new Guid(dr["ProgClassRoom_LastModifiedById"].ToString());
                    }
                    if (dr["ProgClassRoom_LastModifiedDatetime"] != DBNull.Value)
                    {
                        objProgClassRoom.LastModifiedDatetime = Convert.ToDateTime(dr["ProgClassRoom_LastModifiedDatetime"].ToString());
                    }
                    objProgClassRoom.Active = Convert.ToBoolean(dr["Active"].ToString());
                    lstProgClassRoom.Add(objProgClassRoom);
                }
                return lstProgClassRoom;
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clProgClassRoom, "LoadProgClassRoom", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                return null;
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }
        #endregion

        #region "Load Staff By Program Id, Dt: 12-Aug-2011, DB: A"
        public static List<DayCarePL.StaffProperties> LoadStaffBySchoolProgramId(Guid SchoolProgramId)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clProgClassRoom, "LoadStaffBySchoolProgramId", "Execute LoadStaffBySchoolProgramId Method", DayCarePL.Common.GUID_DEFAULT);
            SqlConnection conn = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["daycareConnectionString"].ToString());
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clProgClassRoom, "LoadStaffBySchoolProgramId", "Debug LoadStaffBySchoolProgramId Method", DayCarePL.Common.GUID_DEFAULT);
                List<DayCarePL.StaffProperties> lstProgClassRoom = new List<DayCarePL.StaffProperties>();
                DayCarePL.StaffProperties objProgClassRoom;
                if (conn.State == System.Data.ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "spGetStaffBySchoolProgramId";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = conn;
                cmd.Parameters.Add(new SqlParameter("@SchoolProgramId", SqlDbType.UniqueIdentifier)).Value = SchoolProgramId;

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    objProgClassRoom = new DayCarePL.StaffProperties();
                    objProgClassRoom.Id = new Guid(dr["Id"].ToString());
                    objProgClassRoom.FullName = dr["FullName"].ToString();
                    lstProgClassRoom.Add(objProgClassRoom);
                }
                return lstProgClassRoom;
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clProgClassRoom, "LoadStaffBySchoolProgramId", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                return null;
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }
        #endregion

        #region "Load Prog. Class Room For Child Schedule, Dt: 24-Aug-2011, DB: A"
        public static List<DayCarePL.ProgClassRoomProperties> LoadProgClassRoomForChildSchedule(Guid SchoolId, Guid SchoolProgramId)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clProgClassRoom, "LoadProgClassRoomForChildSchedule", "Execute LoadProgClassRoomForChildSchedule Method", DayCarePL.Common.GUID_DEFAULT);
            SqlConnection conn = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["daycareConnectionString"].ToString());
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clProgClassRoom, "LoadProgClassRoomForChildSchedule", "Debug LoadProgClassRoomForChildSchedule Method", DayCarePL.Common.GUID_DEFAULT);
                List<DayCarePL.ProgClassRoomProperties> lstProgClassRoom = new List<DayCarePL.ProgClassRoomProperties>();
                DayCarePL.ProgClassRoomProperties objProgClassRoom;
                if (conn.State == System.Data.ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "spGetProgClassRoomForChildSchedule";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = conn;
                //cmd.Parameters.Add(new SqlParameter("@SchoolId", SqlDbType.UniqueIdentifier)).Value = SchoolId;
                cmd.Parameters.Add(new SqlParameter("@SchoolProgramId", SqlDbType.UniqueIdentifier)).Value = SchoolProgramId;
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    objProgClassRoom = new DayCarePL.ProgClassRoomProperties();
                    objProgClassRoom.Id = new Guid(dr["Id"].ToString());
                    objProgClassRoom.ClassRoomName = dr["ClassRoomName"].ToString();
                    lstProgClassRoom.Add(objProgClassRoom);
                }
                return lstProgClassRoom;
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clProgClassRoom, "LoadProgClassRoomForChildSchedule", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                return null;
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }
        #endregion

        #region "Save, Dt: 12-Aug-2011, DB: A"
        public static bool Save(DayCarePL.ProgClassRoomProperties objProgClassRoom)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clProgClassRoom, "Save", "Execute Save Method", DayCarePL.Common.GUID_DEFAULT);
            SqlConnection conn = clConnection.CreateConnection();// new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["daycareConnectionString"].ToString());
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clProgClassRoom, "Save", "Debug Save Method", DayCarePL.Common.GUID_DEFAULT);
                //var data = db.spGetProgStaffList(SchoolId);
                clConnection.OpenConnection(conn);
                SqlCommand cmd;
                if (objProgClassRoom.Id.ToString().Equals(DayCarePL.Common.GUID_DEFAULT))
                {
                    cmd = clConnection.CreateCommand("spAddProgClassRoom", conn);
                    cmd.Parameters.Add(clConnection.GetInputParameter("@CreatedById", objProgClassRoom.CreatedById));
                    cmd.Parameters.Add(clConnection.GetInputParameter("@CreatedDateTime", DateTime.Now));
                }
                else
                {
                    cmd = clConnection.CreateCommand("spUpdateProgClassRoom", conn);
                }
                if (!objProgClassRoom.Id.ToString().Equals(DayCarePL.Common.GUID_DEFAULT))
                {
                    cmd.Parameters.Add(clConnection.GetInputParameter("@Id", objProgClassRoom.Id));
                }
                cmd.Parameters.Add(clConnection.GetInputParameter("@SchoolProgramId", objProgClassRoom.SchoolProgramId));
                cmd.Parameters.Add(clConnection.GetInputParameter("@ClassRoomId", objProgClassRoom.ClassRoomId));
                //cmd.Parameters.Add(clConnection.GetInputParameter("@ProgStaffId", objProgClassRoom.ProgStaffId));
                cmd.Parameters.Add(clConnection.GetInputParameter("@Active", objProgClassRoom.Active));
                cmd.Parameters.Add(clConnection.GetInputParameter("@LastModifiedDatetime", DateTime.Now));
                cmd.Parameters.Add(clConnection.GetInputParameter("@LastModifiedById", objProgClassRoom.LastModifiedById));
                cmd.Parameters.Add(clConnection.GetOutputParameter("@status", SqlDbType.Bit));
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
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clProgClassRoom, "Save", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                return false;
            }
            finally
            {
                clConnection.CloseConnection(conn);
            }
        }
        #endregion
    }
}
