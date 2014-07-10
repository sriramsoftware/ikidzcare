using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Collections;

namespace DayCareDAL
{
    public class clSchoolProgram
    {
        #region "Load School Program, Dt: 12-Aug-2011, DB: A"
        public static List<DayCarePL.SchoolProgramProperties> LoadSchoolProgram(Guid SchoolId, Guid SchoolYearId)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clSchoolProgram, "LoadSchoolProgram", "Execute LoadSchoolProgram Method", DayCarePL.Common.GUID_DEFAULT);
            clConnection.DoConnection();
            DayCareDataContext db = new DayCareDataContext();
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clSchoolProgram, "LoadSchoolProgram", "Debug LoadSchoolProgram Method", DayCarePL.Common.GUID_DEFAULT);
                List<DayCarePL.SchoolProgramProperties> lstSchoolProgram = new List<DayCarePL.SchoolProgramProperties>();
                DayCarePL.SchoolProgramProperties objSchoolProgram;
                var data = db.spGetSchoolProgram(SchoolId, SchoolYearId);
                foreach (var d in data)
                {
                    objSchoolProgram = new DayCarePL.SchoolProgramProperties();
                    objSchoolProgram.Id = d.Id;
                    objSchoolProgram.SchoolYearId = d.SchoolYearId;
                    objSchoolProgram.Active = d.Active;
                    //objSchoolProgram.Fees = Convert.ToDouble(d.Fees);
                    objSchoolProgram.Title = d.Title;
                    objSchoolProgram.IsPrimary = Convert.ToBoolean(d.IsPrimary);
                    //if (d.FeesPeriodId != null)
                    //{
                    //    objSchoolProgram.FeesPeriodId = d.FeesPeriodId;
                    //}
                    //if (d.FeesPeriodName != null)
                    //{
                    //    objSchoolProgram.FeesPeriodName = d.FeesPeriodName;
                    //}
                    if (d.CreatedById != null)
                    {
                        objSchoolProgram.CreatedById = d.CreatedById;
                    }
                    if (d.CreatedDateTime != null)
                    {
                        objSchoolProgram.CreatedDateTime = d.CreatedDateTime;
                    }
                    if (d.LastModifiedById != null)
                    {
                        objSchoolProgram.LastModifiedById = d.LastModifiedById;
                    }
                    if (d.LastModifiedDatetime != null)
                    {
                        objSchoolProgram.LastModifiedDatetime = d.LastModifiedDatetime;
                    }
                    lstSchoolProgram.Add(objSchoolProgram);
                }
                #region Old Code
                //if (conn.State == System.Data.ConnectionState.Closed)
                //{
                //    conn.Open();
                //}
                //SqlCommand cmd = new SqlCommand();
                //cmd.CommandText = "spGetSchoolProgram";
                //cmd.CommandType = System.Data.CommandType.StoredProcedure;
                //cmd.Connection = conn;
                //cmd.Parameters.Add(new SqlParameter("@SchoolId", SqlDbType.UniqueIdentifier)).Value = SchoolId;
                //cmd.Parameters.Add(new SqlParameter("@SchoolYearId", SqlDbType.UniqueIdentifier)).Value = SchoolYearId;

                //SqlDataReader dr = cmd.ExecuteReader();
                //while (dr.Read())
                //{
                //    objSchoolProgram = new DayCarePL.SchoolProgramProperties();
                //    objSchoolProgram.Id = new Guid(dr["Id"].ToString());
                //    objSchoolProgram.SchoolYearId = new Guid(dr["SchoolYearId"].ToString());
                //    objSchoolProgram.Active = Convert.ToBoolean(dr["Active"].ToString());
                //    objSchoolProgram.Fees = Convert.ToDouble(dr["Fees"].ToString());
                //    objSchoolProgram.Title = dr["Title"].ToString();
                //    objSchoolProgram.IsPrimary = Convert.ToBoolean(dr["IsPrimary"].ToString());
                //    if (dr["FeesPeriodId"] != DBNull.Value)
                //    {
                //        objSchoolProgram.FeesPeriodId = new Guid(dr["FeesPeriodId"].ToString());
                //    }
                //    if (dr["FeesPeriodName"] != DBNull.Value)
                //    {
                //        objSchoolProgram.FeesPeriodName = dr["FeesPeriodName"].ToString();
                //    }
                //    if (dr["CreatedById"] != DBNull.Value)
                //    {
                //        objSchoolProgram.CreatedById = new Guid(dr["CreatedById"].ToString());
                //    }
                //    if (dr["CreatedDateTime"] != DBNull.Value)
                //    {
                //        objSchoolProgram.CreatedDateTime = Convert.ToDateTime(dr["CreatedDateTime"].ToString());
                //    }
                //    if (dr["LastModifiedById"] != DBNull.Value)
                //    {
                //        objSchoolProgram.LastModifiedById = new Guid(dr["LastModifiedById"].ToString());
                //    }
                //    if (dr["LastModifiedDatetime"] != DBNull.Value)
                //    {
                //        objSchoolProgram.LastModifiedDatetime = Convert.ToDateTime(dr["LastModifiedDatetime"].ToString());
                //    }
                //    lstSchoolProgram.Add(objSchoolProgram);
                //}
                #endregion
                return lstSchoolProgram;

            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clSchoolProgram, "LoadSchoolProgram", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                return null;
            }
        }
        #endregion

        #region "Load School Program For Child Schedule, Dt: 24-Aug-2011, DB: A"
        public static List<DayCarePL.SchoolProgramProperties> LoadSchoolProgramForChildSchedule(Guid SchoolId, Guid SchoolYearId)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clSchoolProgram, "LoadSchoolProgramForChildSchedule", "Execute LoadSchoolProgramForChildSchedule Method", DayCarePL.Common.GUID_DEFAULT);
            clConnection.DoConnection();
            DayCareDataContext db = new DayCareDataContext();
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clSchoolProgram, "LoadSchoolProgramForChildSchedule", "Debug LoadSchoolProgramForChildSchedule Method", DayCarePL.Common.GUID_DEFAULT);
                List<DayCarePL.SchoolProgramProperties> lstSchoolProgram = new List<DayCarePL.SchoolProgramProperties>();
                DayCarePL.SchoolProgramProperties objSchoolProgram;
                var data = db.spGetSchoolProgramForSchedule(SchoolId, SchoolYearId);
                foreach (var d in data)
                {
                    objSchoolProgram = new DayCarePL.SchoolProgramProperties();
                    objSchoolProgram.Id = d.Id;
                    objSchoolProgram.Title = d.Title;
                    objSchoolProgram.IsPrimary = Convert.ToBoolean(d.IsPrimary);
                    lstSchoolProgram.Add(objSchoolProgram);
                }

                return lstSchoolProgram;

            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clSchoolProgram, "LoadSchoolProgramForChildSchedule", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                return null;
            }
        }
        #endregion

        #region Check Duplicate School Program Name, Dt: 2-Aug-2011, DB: A"
        public static bool CheckDuplicateSchoolProgramName(string Title, Guid SchoolId)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clSchoolProgram, "CheckDuplicateSchoolProgramName", "Execute CheckDuplicateSchoolProgramName Method", DayCarePL.Common.GUID_DEFAULT);
            SqlConnection conn = clConnection.CreateConnection();
            bool result = false;
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clSchoolProgram, "CheckDuplicateSchoolProgramName", "Debug CheckDuplicateSchoolProgramName Method", DayCarePL.Common.GUID_DEFAULT);
                clConnection.OpenConnection(conn);
                SqlCommand cmd = clConnection.CreateCommand("spCheckDuplicateSchoolProgramName", conn);
                cmd.Parameters.Add(clConnection.GetInputParameter("@Title", Title));
                cmd.Parameters.Add(clConnection.GetInputParameter("@SchoolId", SchoolId));
                cmd.Parameters.Add(clConnection.GetOutputParameter("@status", SqlDbType.Bit));
                cmd.ExecuteNonQuery();
                if (Convert.ToBoolean(cmd.Parameters["@status"].Value))
                {
                    result = true;
                }
                else
                {
                    result = false;
                }

            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clSchoolProgram, "CheckDuplicateSchoolProgramName", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                result = false;
            }
            finally
            {
                clConnection.CloseConnection(conn);
            }
            return result;
        }
        #endregion

        #region Check School Program In Child Schedule, Dt: 30-Aug-2011, DB: A"
        public static bool CheckSchoolProgramInChildSchedule(Guid SchoolId, Guid SchoolProgramId)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clSchoolProgram, "CheckSchoolProgramInChildSchedule", "Execute CheckSchoolProgramInChildSchedule Method", DayCarePL.Common.GUID_DEFAULT);
            SqlConnection conn = clConnection.CreateConnection();
            bool result = false;
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clSchoolProgram, "CheckSchoolProgramInChildSchedule", "Debug CheckSchoolProgramInChildSchedule Method", DayCarePL.Common.GUID_DEFAULT);
                clConnection.OpenConnection(conn);
                SqlCommand cmd = clConnection.CreateCommand("spCheckSchoolProgramInChildSchedule", conn);
                cmd.Parameters.Add(clConnection.GetInputParameter("@SchoolId", SchoolId));
                cmd.Parameters.Add(clConnection.GetInputParameter("@SchoolProgramId", SchoolProgramId));
                cmd.Parameters.Add(clConnection.GetOutputParameter("@status", SqlDbType.Bit));
                cmd.ExecuteNonQuery();
                if (Convert.ToBoolean(cmd.Parameters["@status"].Value))
                {
                    result = true;
                }
                else
                {
                    result = false;
                }

            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clSchoolProgram, "CheckSchoolProgramInChildSchedule", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                result = false;
            }
            finally
            {
                clConnection.CloseConnection(conn);
            }
            return result;
        }
        #endregion

        #region "Save, Dt: 10-Aug-2011, DB: A"
        public static Guid Save(DayCarePL.SchoolProgramProperties objSchoolProgram)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clSchoolProgram, "Save", "Execute Save Method", DayCarePL.Common.GUID_DEFAULT);
            SqlConnection conn = clConnection.CreateConnection();
            Guid result = new Guid();

            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clSchoolProgram, "Save", "Debug Save Method", DayCarePL.Common.GUID_DEFAULT);
                clConnection.OpenConnection(conn);
                SqlCommand cmd;

                if (objSchoolProgram.Id.ToString().Equals(DayCarePL.Common.GUID_DEFAULT))
                {
                    cmd = clConnection.CreateCommand("spAddSchoolProgram", conn);
                }
                else
                {
                    cmd = clConnection.CreateCommand("spUpdateSchoolProgram", conn);
                }
                if (!objSchoolProgram.Id.ToString().Equals(DayCarePL.Common.GUID_DEFAULT))
                {
                    cmd.Parameters.Add(clConnection.GetInputParameter("@Id", objSchoolProgram.Id));
                }
                cmd.Parameters.Add(clConnection.GetInputParameter("@SchoolYearId", objSchoolProgram.SchoolYearId));
                cmd.Parameters.Add(clConnection.GetInputParameter("@Title", objSchoolProgram.Title));
                //cmd.Parameters.Add(clConnection.GetInputParameter("@Fees", 0));
                // cmd.Parameters.Add(clConnection.GetInputParameter("@FeesPeriodId", DayCarePL.Common.GUID_DEFAULT));
                cmd.Parameters.Add(clConnection.GetInputParameter("@Active", objSchoolProgram.Active));
                cmd.Parameters.Add(clConnection.GetInputParameter("@IsPrimary", objSchoolProgram.IsPrimary));
                cmd.Parameters.Add(clConnection.GetInputParameter("@Comments", objSchoolProgram.Comments));
                cmd.Parameters.Add(clConnection.GetInputParameter("@CreatedById", objSchoolProgram.CreatedById));
                cmd.Parameters.Add(clConnection.GetInputParameter("@LastModifiedById", objSchoolProgram.LastModifiedById));

                // cmd.Parameters.Add(clConnection.GetOutputParameter("@Result", SqlDbType.Bit));
                //cmd.Parameters.AddWithValue("@SchoolYearId", objSchoolProgram.SchoolYearId);
                //cmd.Parameters.AddWithValue("@Title", objSchoolProgram.Title);
                //cmd.Parameters.AddWithValue("@Fees", objSchoolProgram.Fees);
                //cmd.Parameters.AddWithValue("@FeesPeriodId", objSchoolProgram.FeesPeriodId);
                //cmd.Parameters.AddWithValue("@Active", objSchoolProgram.Active);
                //cmd.Parameters.AddWithValue("@IsPrimary", objSchoolProgram.IsPrimary);
                //cmd.Parameters.AddWithValue("@Comments", objSchoolProgram.Comments);
                //cmd.Parameters.AddWithValue("@CreatedById", objSchoolProgram.CreatedById);
                //cmd.Parameters.AddWithValue("@LastModifiedById", objSchoolProgram.LastModifiedById);
                // cmd.Parameters.Add(new SqlParameter("@Result", SqlDbType.Bit)).Direction = ParameterDirection.Output;
                //cmd.ExecuteNonQuery();
                object SchoolProgramId = cmd.ExecuteScalar();
                //if (Convert.ToBoolean(cmd.Parameters["@Result"].Value))
                //{
                //    return true;
                //}
                //return false;
                return new Guid(SchoolProgramId.ToString());

            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clSchoolProgram, "Save", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                result = new Guid(DayCarePL.Common.GUID_DEFAULT);
            }
            finally
            {
                clConnection.CloseConnection(conn);
            }
            return result;
        }
        #endregion

        #region "Get schoolprogram wise weeky attendace report"
        public static DataSet GetSchoolProgramWiseStudentWeeklySchedule(Guid SchoolYearId, Guid @SchoolProgramId)
        {
            DataSet ds = new DataSet();
            try
            {
                SortedList sl = new SortedList();
                sl.Add("@SchoolYearId", SchoolYearId);
                sl.Add("@SchoolProgramId", SchoolProgramId);
                ds = clConnection.GetDataSet("spRptGetProgramWiseChildProgEnrollmentReport", sl);
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clSchoolProgram, "GetSchoolProgramWiseStudentWeeklySchedule", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
            }
            return ds;
        }
        #endregion

        #region" Get all Progclass Room ,Dt:27-Sep-2011,Db:V"
        public static DataSet GetAllProgClassRoomList(Guid SchoolId)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clChildProgEnrollment, "Save", "Execute Save Method", DayCarePL.Common.GUID_DEFAULT);
            DataSet ds = new DataSet();
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clChildProgEnrollment, "LoadChildFamily", "Debug LoadChildFamily Method", DayCarePL.Common.GUID_DEFAULT);
                SortedList sl = new SortedList();
                sl.Add("@SchoolId", SchoolId);
                ds = clConnection.GetDataSet("spGetAllClassRoomForPrimarySchoolProgram", sl);
                if (ds != null && ds.Tables.Count > 0)
                {

                }
                return ds;
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clChildData, "LoadChildData", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                return null;
            }
            return ds;
        }
        #endregion

        #region "Check SchoolProgram Is in ChildEnrolled,Dt:29-Sep-2011,Db:V"
        public static bool CheckSchoolProgramInChildEnrolled(Guid Id, Guid SchoolYearId)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clChildProgEnrollment, "Save", "Execute Save Method", DayCarePL.Common.GUID_DEFAULT);
            DataSet ds = new DataSet();
            SqlConnection con = clConnection.CreateConnection();


            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clChildProgEnrollment, "LoadChildFamily", "Debug LoadChildFamily Method", DayCarePL.Common.GUID_DEFAULT);
                //SortedList sl = new SortedList();
                //sl.Add("@SchoolYearId", SchoolYearId);
                //sl.Add("@Id", Id);
                //ds = clConnection.GetDataSet("spCheckSchoolProgramInChildEnrolled", sl);
                //return ds;
                clConnection.OpenConnection(con);
                SqlCommand cmd = clConnection.CreateCommand("spCheckSchoolProgramInChildEnrolled", con);
                cmd.Parameters.Add(clConnection.GetInputParameter("@Id", Id));
                cmd.Parameters.Add(clConnection.GetInputParameter("@SchoolYearId", SchoolYearId));
                object status = cmd.ExecuteScalar();
                if (Convert.ToBoolean(status))
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
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clChildData, "LoadChildData", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                return false;
            }
        }
        #endregion

        #region "Check SchoolProgram Is in ChildEnrolled and ledger,Dt:14 march 2012,Db:A"
        public static bool CheckSchoolProgramInChildEnrolledAndLedger(Guid SchoolProgramId)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clSchoolProgram, "CheckSchoolProgramInChildEnrolledAndLedger", "Execute Save Method", DayCarePL.Common.GUID_DEFAULT);
            DataSet ds = new DataSet();
            SqlConnection con = clConnection.CreateConnection();


            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clSchoolProgram, "CheckSchoolProgramInChildEnrolledAndLedger", "Debug CheckSchoolProgramInChildEnrolledAndLedger Method", DayCarePL.Common.GUID_DEFAULT);
                //SortedList sl = new SortedList();
                //sl.Add("@SchoolYearId", SchoolYearId);
                //sl.Add("@Id", Id);
                //ds = clConnection.GetDataSet("spCheckSchoolProgramInChildEnrolled", sl);
                //return ds;
                clConnection.OpenConnection(con);
                SqlCommand cmd = clConnection.CreateCommand("spIsProgramAssignedtoChild", con);
                cmd.Parameters.Add(clConnection.GetInputParameter("@SchoolProgramId", SchoolProgramId));
                object status = cmd.ExecuteScalar();
                if (Convert.ToInt32(status) > 0)
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
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clSchoolProgram, "CheckSchoolProgramInChildEnrolledAndLedger", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                return false;
            }
        }
        #endregion

        #region "Delete School Program ,Dt:14 March-2012,Db:A"
        public static bool DeleteSchoolProgram(Guid Id)
        {
            bool result = false;
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clFamilyPayment, "Delete", "Delete Method", DayCarePL.Common.GUID_DEFAULT);
            clConnection.DoConnection();
            //DayCareDataContext db = new DayCareDataContext();
            try
            {
                SqlConnection sql = clConnection.CreateConnection();
                clConnection.OpenConnection(sql);
                SqlCommand cmd = clConnection.CreateCommand("spDeleteSchoolProgram", sql);
                cmd.Parameters.Add(clConnection.GetInputParameter("@SchoolProgramId", Id));
                cmd.Parameters.Add(clConnection.GetOutputParameter("@status", SqlDbType.Int));
                cmd.ExecuteNonQuery();
                
                if (Convert.ToBoolean(cmd.Parameters["@status"].Value))
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clFamilyPayment, "Delete", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                result = false;
            }
            return result;
        }
        #endregion
    }
}
