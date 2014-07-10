using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace DayCareDAL
{
    public class clProgStaff
    {
        #region "Save, Dt: 10-Aug-2011, DB: A"
        public static bool Save(DayCarePL.ProgStaffProperties objProgStaff)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clProgStaff, "Save", "Execute Save Method", DayCarePL.Common.GUID_DEFAULT);
            SqlConnection conn = clConnection.CreateConnection();// new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["daycareConnectionString"].ToString());
            SqlTransaction sqlTran = null;
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clProgStaff, "Save", "Debug Save Method", DayCarePL.Common.GUID_DEFAULT);
                clConnection.OpenConnection(conn);
                SqlCommand cmd;
                bool status = true;
                sqlTran = conn.BeginTransaction();
                if (!objProgStaff.Id.ToString().Equals(DayCarePL.Common.GUID_DEFAULT))
                {
                    cmd = clConnection.CreateCommand("spUpdateProgClassRoomByStaffId", conn);
                    cmd.Transaction = sqlTran;
                    cmd.Parameters.Add(clConnection.GetInputParameter("@SchoolProgramId", objProgStaff.SchoolProgramId));
                    cmd.Parameters.Add(clConnection.GetInputParameter("@ProgStaffId", objProgStaff.Id));
                    cmd.Parameters.Add(clConnection.GetInputParameter("@Active", objProgStaff.Active));
                    cmd.Parameters.Add(clConnection.GetInputParameter("@LastModifiedDatetime", DateTime.Now));
                    cmd.Parameters.Add(clConnection.GetInputParameter("@LastModifiedById", objProgStaff.LastModifiedById));
                    cmd.Parameters.Add(clConnection.GetOutputParameter("@status", SqlDbType.Bit));

                    cmd.ExecuteNonQuery();
                    status = Convert.ToBoolean(cmd.Parameters["@status"].Value);
                }
                if (status)
                {
                    if (objProgStaff.Id.ToString().Equals(DayCarePL.Common.GUID_DEFAULT))
                    {
                        cmd = clConnection.CreateCommand("spAddProgStaff", conn);
                        cmd.Parameters.Add(clConnection.GetInputParameter("@CreatedById", objProgStaff.CreatedById));
                        cmd.Parameters.Add(clConnection.GetInputParameter("@CreatedDateTime", DateTime.Now));
                    }
                    else
                    {
                        cmd = clConnection.CreateCommand("spUpdateProgStaff", conn);
                    }

                    cmd.Transaction = sqlTran;

                    if (!objProgStaff.Id.ToString().Equals(DayCarePL.Common.GUID_DEFAULT))
                    {
                        cmd.Parameters.Add(clConnection.GetInputParameter("@Id", objProgStaff.Id));
                    }
                    cmd.Parameters.Add(clConnection.GetInputParameter("@SchoolProgramId", objProgStaff.SchoolProgramId));
                    cmd.Parameters.Add(clConnection.GetInputParameter("@StaffId", objProgStaff.StaffId));
                    cmd.Parameters.Add(clConnection.GetInputParameter("@Active", objProgStaff.Active));
                    cmd.Parameters.Add(clConnection.GetInputParameter("@LastModifiedDatetime", DateTime.Now));
                    cmd.Parameters.Add(clConnection.GetInputParameter("@LastModifiedById", objProgStaff.LastModifiedById));
                    cmd.Parameters.Add(clConnection.GetOutputParameter("@status", SqlDbType.Bit));
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    sqlTran.Rollback();
                    return false;
                }
                sqlTran.Commit();
                return true;
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clProgStaff, "Save", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                if (sqlTran != null)
                {
                    sqlTran.Rollback();
                }
                return false;
            }
            finally
            {
                clConnection.CloseConnection(conn);
            }
        }
        #endregion

        #region "Load Primary/Secondary Staff By School Program, Dt: 11-Aug-2011, DB: A"
        public static List<DayCarePL.ProgStaffProperties> LoadStaffBySchoolProgram(Guid SchoolId, Guid SchoolYearId, Guid SchoolProgramId, bool IsPrimary)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clProgStaff, "LoadStaffBySchoolProgram", "Execute LoadStaffBySchoolProgram Method", DayCarePL.Common.GUID_DEFAULT);
            SqlConnection conn = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["daycareConnectionString"].ToString());
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clProgStaff, "LoadStaffBySchoolProgram", "Debug LoadStaffBySchoolProgram Method", DayCarePL.Common.GUID_DEFAULT);
                List<DayCarePL.ProgStaffProperties> lstProgStaff = new List<DayCarePL.ProgStaffProperties>();
                DayCarePL.ProgStaffProperties objProgStaff;
                if (conn.State == System.Data.ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "spGetStaffBySchoolProgram";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = conn;
                cmd.Parameters.Add(new SqlParameter("@SchoolId", SqlDbType.UniqueIdentifier)).Value = SchoolId;
                cmd.Parameters.Add(new SqlParameter("@SchoolYearId", SqlDbType.UniqueIdentifier)).Value = SchoolYearId;
                cmd.Parameters.Add(new SqlParameter("@SchoolProgramId", SqlDbType.UniqueIdentifier)).Value = SchoolProgramId;
                //cmd.Parameters.Add(new SqlParameter("@IsPrimary", SqlDbType.Bit)).Value = IsPrimary;

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    objProgStaff = new DayCarePL.ProgStaffProperties();
                    objProgStaff.Id = new Guid(dr["ProgStaff_Id"].ToString());
                    objProgStaff.ProgStaff_StaffId = new Guid(dr["ProgStaff_StaffId"].ToString());
                    objProgStaff.StaffId = new Guid(dr["StaffId"].ToString());
                    objProgStaff.StaffFullName = dr["FullName"].ToString();
                    objProgStaff.StaffUserName = dr["UserName"].ToString();
                    objProgStaff.StaffEmail = dr["Email"].ToString();
                    objProgStaff.StaffCity = dr["City"].ToString();
                    objProgStaff.StaffMainPhone = dr["MainPhone"].ToString();
                    if (dr["ProgStaff_CreatedById"] != DBNull.Value)
                    {
                        objProgStaff.CreatedById = new Guid(dr["ProgStaff_CreatedById"].ToString());
                    }
                    if (dr["ProgStaff_CreatedDateTime"] != DBNull.Value)
                    {
                        objProgStaff.CreatedDateTime = Convert.ToDateTime(dr["ProgStaff_CreatedDateTime"].ToString());
                    }
                    if (dr["ProgStaff_LastModifiedById"] != DBNull.Value)
                    {
                        objProgStaff.LastModifiedById = new Guid(dr["ProgStaff_LastModifiedById"].ToString());
                    }
                    if (dr["ProgStaff_LastModifiedDatetime"] != DBNull.Value)
                    {
                        objProgStaff.LastModifiedDatetime = Convert.ToDateTime(dr["ProgStaff_LastModifiedDatetime"].ToString());
                    }
                    objProgStaff.Active = Convert.ToBoolean(dr["Active"].ToString());
                    if (dr["IsPrimary"] != DBNull.Value)
                    {
                        objProgStaff.IsPrimary = Convert.ToBoolean(dr["IsPrimary"].ToString());
                    }
                    objProgStaff.GroupTitle = dr["GroupTitle"].ToString();
                    lstProgStaff.Add(objProgStaff);
                }
                return lstProgStaff;
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clProgStaff, "LoadStaffBySchoolProgram", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
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

        #region "Load Staff From Prog. Staff By School Program, Dt: 16-Aug-2011, DB: A"
        public static List<DayCarePL.ProgStaffProperties> GetStaffFromProgStaffBySchoolProgram(Guid SchoolProgramId)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clProgStaff, "GetStaffFromProgStaffBySchoolProgram", "Execute GetStaffFromProgStaffBySchoolProgram Method", DayCarePL.Common.GUID_DEFAULT);
            SqlConnection conn = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["daycareConnectionString"].ToString());

            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clProgStaff, "GetStaffFromProgStaffBySchoolProgram", "Debug GetStaffFromProgStaffBySchoolProgram Method", DayCarePL.Common.GUID_DEFAULT);

                List<DayCarePL.ProgStaffProperties> lstSchoolProgram = new List<DayCarePL.ProgStaffProperties>();
                DayCarePL.ProgStaffProperties objSchoolProgram;
                if (conn.State == System.Data.ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "spGetStaffFromProgStaffBySchoolProgram";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = conn;
                cmd.Parameters.Add(new SqlParameter("@SchoolProgramId", SqlDbType.UniqueIdentifier)).Value = SchoolProgramId;

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    objSchoolProgram = new DayCarePL.ProgStaffProperties();
                    objSchoolProgram.Id = new Guid(dr["Id"].ToString());
                    objSchoolProgram.StaffFullName = dr["FullName"].ToString();
                    lstSchoolProgram.Add(objSchoolProgram);
                }
                return lstSchoolProgram;
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clProgStaff, "GetStaffFromProgStaffBySchoolProgram", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                return null;
            }
        }

        #endregion
    }
}
