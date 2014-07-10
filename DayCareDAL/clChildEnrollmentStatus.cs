using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace DayCareDAL
{
    public class clChildEnrollmentStatus
    {
        #region "Save ChildEnrollmentStatus,Dt:24-Aug-2011,Db:V"
        public static bool Save(DayCarePL.ChildEnrollmentStatusProperties objChildEnrollment)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clChildEnrollmentStatus, "Save", "Execute Save Method", DayCarePL.Common.GUID_DEFAULT);
            SqlConnection conn = clConnection.CreateConnection();
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clChildEnrollmentStatus, "Save", "Debug Save Method", DayCarePL.Common.GUID_DEFAULT);
                clConnection.OpenConnection(conn);
                SqlCommand cmd;
                if (objChildEnrollment.Id.ToString().Equals(DayCarePL.Common.GUID_DEFAULT))
                {
                    cmd = clConnection.CreateCommand("spAddChildEnrollmentStatus", conn);
                    cmd.Parameters.Add(clConnection.GetInputParameter("@CreatedDateTime", DateTime.Now));
                    cmd.Parameters.Add(clConnection.GetInputParameter("@CreatedById", objChildEnrollment.CreatedById));
                }
                else
                {
                    cmd = clConnection.CreateCommand("spUpdateChildEnrollmentStatus", conn);
                    cmd.Parameters.Add(clConnection.GetInputParameter("@Id", objChildEnrollment.Id));
                }
                cmd.Parameters.Add(clConnection.GetInputParameter("@ChildSchoolYearId", objChildEnrollment.ChildSchoolYearId));
                cmd.Parameters.Add(clConnection.GetInputParameter("@EnrollmentStatusId", objChildEnrollment.EnrollmentStatusId));
                cmd.Parameters.Add(clConnection.GetInputParameter("@EnrollmentDate", objChildEnrollment.EnrollmentDate));
                cmd.Parameters.Add(clConnection.GetInputParameter("@Comments", objChildEnrollment.Comments));
                cmd.Parameters.Add(clConnection.GetInputParameter("@LastModifiedDateTime", DateTime.Now));
                cmd.Parameters.Add(clConnection.GetInputParameter("@LastModifiedById", objChildEnrollment.LastModifiedById));
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
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clChildEnrollmentStatus, "Save", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                return false;

            }
            finally
            {
                clConnection.CloseConnection(conn);
            }
        }
        #endregion

        #region "Load ChildEnrollmentStatus Data, Dt:24-Aug-2011,Db:V"
        public static List<DayCarePL.ChildEnrollmentStatusProperties> LoadChildEnrollmentStatus(Guid SchoolId, Guid ChildSchoolYearId)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clChildEnrollmentStatus, "LoadChildEnrollmentStatus", "Execute LoadChildEnrollmentStatus Method", DayCarePL.Common.GUID_DEFAULT);
            DayCareDataContext db = new DayCareDataContext();
            List<DayCarePL.ChildEnrollmentStatusProperties> lstChildEnrollmentStatus = new List<DayCarePL.ChildEnrollmentStatusProperties>();
            DayCarePL.ChildEnrollmentStatusProperties objChildEnrollmentStatus = null;
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clChildEnrollmentStatus, "LoadChildEnrollmentStatus", "Debug LoadChildEnrollmentStatus Method", DayCarePL.Common.GUID_DEFAULT);
                var data = db.spGetChildEnrollmentStatus(SchoolId, ChildSchoolYearId);
                foreach (var d in data)
                {
                    objChildEnrollmentStatus = new DayCarePL.ChildEnrollmentStatusProperties();
                    objChildEnrollmentStatus.Id = d.Id;
                    objChildEnrollmentStatus.EnrollmentStatusId = d.EnrollmentStatusId;
                    objChildEnrollmentStatus.EnrollmentStatus = d.Status;
                    objChildEnrollmentStatus.ChildSchoolYearId = d.ChildSchoolYearId;
                    objChildEnrollmentStatus.EnrollmentDate = d.EnrollmentDate;
                    objChildEnrollmentStatus.Comments = d.Comments;
                    lstChildEnrollmentStatus.Add(objChildEnrollmentStatus);
                    objChildEnrollmentStatus = null;
                }
            }
            catch(Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clChildEnrollmentStatus, "LoadChildEnrollmentStatus", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
            }
            return lstChildEnrollmentStatus;
        }
        #endregion

        #region "Check DuplicateChildEnrollmentStatus, Dt:26-Aug-2011,Db:V"
        public static bool CheckDuplicateChildEnrollmentStatus(Guid ChildSchoolYearId, Guid EnrollmentStatusId, DateTime EnrollmentDate, Guid Id)
        { 
            bool result = false;
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clChildEnrollmentStatus, "CheckDuplicateChildEnrollmentStatus", "Execute CheckDuplicateChildEnrollmentStatus Method", DayCarePL.Common.GUID_DEFAULT);
            clConnection.DoConnection();
            DayCareDataContext db = new DayCareDataContext();
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clChildEnrollmentStatus, "CheckDuplicateChildEnrollmentStatus", "Debug CheckDuplicateChildEnrollmentStatus Method", DayCarePL.Common.GUID_DEFAULT);                

                var data = db.spCheckDuplicateChildEnrollmentStatus(ChildSchoolYearId, EnrollmentStatusId, EnrollmentDate, Id);
                foreach (var c in data)
                {
                    result = true;
                    break;
                }
                return result;
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clChildEnrollmentStatus, "CheckDuplicateChildEnrollmentStatus", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                return false;
            }
        }
        #endregion
    }
}
