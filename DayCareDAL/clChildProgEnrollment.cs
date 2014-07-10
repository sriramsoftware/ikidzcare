using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace DayCareDAL
{
    public class clChildProgEnrollment
    {
        #region "Load ProgClassRoom ID and Name Display,Dt:24-Aug-2011,Db:V"
        public static List<DayCarePL.ChildProgEnrollmentProperties> LoadProgClassRoom(Guid SchoolProgramId)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clChildProgEnrollment, "LoadProgClassRoom", "Execute LoadProgClassRoom Method", DayCarePL.Common.GUID_DEFAULT);
            clConnection.DoConnection();
            DayCareDataContext db = new DayCareDataContext();
            List<DayCarePL.ChildProgEnrollmentProperties> lstProgSchedule = new List<DayCarePL.ChildProgEnrollmentProperties>();
            DayCarePL.ChildProgEnrollmentProperties objProgSchedule = null;
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clChildProgEnrollment, "LoadProgClassRoom", "Debug LoadProgClassRoom Method", DayCarePL.Common.GUID_DEFAULT);
                var data = db.spGetProgClassRoomIdAndClassRoomName(SchoolProgramId);
                foreach (var d in data)
                {
                    objProgSchedule = new DayCarePL.ChildProgEnrollmentProperties();
                    objProgSchedule.Id = d.Id;
                    objProgSchedule.ProgClassRoomTitle = d.ClassRoomTitle;
                    objProgSchedule.ClassRoomId = d.ClassRoomId;
                    lstProgSchedule.Add(objProgSchedule);
                    //objProgSchedule = null;
                }
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clChildProgEnrollment, "LoadProgClassRoom", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
            }
            return lstProgSchedule;
        }
        #endregion

        #region "Load Program SchoolProgramId and ProgramName, Dt:3-Sep-2011, Db:V"
        public static List<DayCarePL.ChildProgEnrollmentProperties> LoadProgram()
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clChildProgEnrollment, "LoadProgram", "Execute LoadProgram Method", DayCarePL.Common.GUID_DEFAULT);
            clConnection.DoConnection();
            DayCareDataContext db = new DayCareDataContext();
            List<DayCarePL.ChildProgEnrollmentProperties> lstChildProgEnrollment = new List<DayCarePL.ChildProgEnrollmentProperties>();
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clChildProgEnrollment, "LoadProgram", "Debug LoadProgram Method", DayCarePL.Common.GUID_DEFAULT);
                DayCarePL.ChildProgEnrollmentProperties objChildProgEnrollment = null;
                SortedList sl = new SortedList();
                var data = db.spGetSchoolProgramIdAndName();
                foreach (var c in data)
                {

                    objChildProgEnrollment = new DayCarePL.ChildProgEnrollmentProperties();
                    objChildProgEnrollment.ProgramTitle = c.IsPrimary;
                    objChildProgEnrollment.SchoolProgramId = c.Id;
                    lstChildProgEnrollment.Add(objChildProgEnrollment);

                }
                return lstChildProgEnrollment;

            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clChildProgEnrollment, "LoadProgram", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                return null;
            }
        }
        #endregion

        #region "Get Fees From SchoolProgram by Id,Dt:3-Sep-2011,Db:V"
        public static decimal GetFees(Guid SchoolProgramId)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clChildProgEnrollment, "GetFees", "Execute LoadChildProgEnrollment Method", DayCarePL.Common.GUID_DEFAULT);
            clConnection.DoConnection();
            DayCareDataContext db = new DayCareDataContext();
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clChildProgEnrollment, "GetFees", "Debug GetFees Method", DayCarePL.Common.GUID_DEFAULT);
                //var data = (from c in db.SchoolPrograms
                //            where c.Id.Equals(SchoolProgramId)
                //            select c.Fees).SingleOrDefault();
                return 0;
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clChildProgEnrollment, "GetFees", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                return 0;
            }
        }
        #endregion



        #region "Save ChildProgEnrollment,Dt:3-Sep-2011,Db:V"
        public static Guid Save(DayCarePL.ChildProgEnrollmentProperties objChildProgEnrollment)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clChildProgEnrollment, "Save", "Execute Save Method", DayCarePL.Common.GUID_DEFAULT);
            SqlConnection conn = clConnection.CreateConnection();
            Guid result = new Guid();
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clChildProgEnrollment, "Save", "Debug Save Method", DayCarePL.Common.GUID_DEFAULT);
                clConnection.OpenConnection(conn);
                SqlCommand cmd;
                if (objChildProgEnrollment.Id.ToString().Equals(DayCarePL.Common.GUID_DEFAULT))
                {
                    cmd = clConnection.CreateCommand("spAddChildProgEnrollment", conn);
                    cmd.Parameters.Add(clConnection.GetInputParameter("@CreatedById", objChildProgEnrollment.CreatedById));
                }
                else
                {
                    cmd = clConnection.CreateCommand("spUpdateChildProgEnrollment", conn);
                    cmd.Parameters.Add(clConnection.GetInputParameter("@Id", objChildProgEnrollment.Id));
                }


                cmd.Parameters.Add(clConnection.GetInputParameter("@ChildSchoolYearId", objChildProgEnrollment.ChildSchoolYearId));
                cmd.Parameters.Add(clConnection.GetInputParameter("@SchoolProgramId", objChildProgEnrollment.SchoolProgramId));
                cmd.Parameters.Add(clConnection.GetInputParameter("@ProgClassRoomId", objChildProgEnrollment.ProgClassRoomId));
                cmd.Parameters.Add(clConnection.GetInputParameter("@DayIndex", objChildProgEnrollment.DayIndex));
                cmd.Parameters.Add(clConnection.GetInputParameter("@DayType", objChildProgEnrollment.DayType));
                cmd.Parameters.Add(clConnection.GetInputParameter("@LastModifiedById", objChildProgEnrollment.LastModifiedById));
                if (objChildProgEnrollment.Id.ToString().Equals(DayCarePL.Common.GUID_DEFAULT))
                {
                    //cmd.Parameters.Add(clConnection.GetOutputParameter("@Id", SqlDbType.UniqueIdentifier));
                }
                /*Changes Remaing to Executenonquery inseted executescalar */
                object Id = cmd.ExecuteScalar();

                return new Guid(Id.ToString());

            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clChildProgEnrollment, "Save", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                result = new Guid(DayCarePL.Common.GUID_DEFAULT);
            }
            finally
            {
                clConnection.CloseConnection(conn);
            }
            return result;
        }
        #endregion

        #region "Delete Child Program Enrollment"
        public static int Delete(Guid Id)
        {
            int result = 0;
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clChildProgEnrollment, "Delete", "Delete Method", DayCarePL.Common.GUID_DEFAULT);
            SqlConnection conn = clConnection.CreateConnection();
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clChildProgEnrollment, "Delete", "Debug Delete Method", DayCarePL.Common.GUID_DEFAULT);
                clConnection.OpenConnection(conn);
                SqlCommand cmd;
                cmd = clConnection.CreateCommand("spDeleteChildProgEnrollment", conn);
                cmd.Parameters.Add(clConnection.GetInputParameter("@Id", Id));
                object Result = cmd.ExecuteScalar();
                result = Convert.ToInt32(Result);
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clChildProgEnrollment, "Delete", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                result = 0;
            }
            return result;
        }
        #endregion

        #region "Get School Program Wise Enrollment"
        public static List<DayCarePL.ChildProgEnrollmentProperties> LoadProgEnrollment(Guid ChildSchoolYearId, Guid SchoolProgramId)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clChildProgEnrollment, "LoadProgEnrollment", "LoadProgEnrollment method called", DayCarePL.Common.GUID_DEFAULT);
            clConnection.DoConnection();
            DayCareDataContext db = new DayCareDataContext();
            List<DayCarePL.ChildProgEnrollmentProperties> lstChilProgEnrollment = new List<DayCarePL.ChildProgEnrollmentProperties>();
            DayCarePL.ChildProgEnrollmentProperties objChildProgEnrollment = null;
            try
            {

                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clChildProgEnrollment, "LoadProgEnrollment", "Debug LoadProgEnrollment called", DayCarePL.Common.GUID_DEFAULT);
                var data = db.spGetChildProgEnrollment(ChildSchoolYearId, SchoolProgramId);

                foreach (var d in data)
                {
                    objChildProgEnrollment = new DayCarePL.ChildProgEnrollmentProperties();
                    objChildProgEnrollment.Id = d.Id;
                    objChildProgEnrollment.ChildSchoolYearId = d.ChildSchoolYearId;
                    objChildProgEnrollment.ProgClassRoomId = d.ProgClassRoomId;
                    objChildProgEnrollment.CreatedById = d.CreatedById;
                    objChildProgEnrollment.DayIndex = d.DayIndex;
                    objChildProgEnrollment.DayType = d.DayType;
                    objChildProgEnrollment.SchoolProgramId = d.SchoolProgramId;
                    lstChilProgEnrollment.Add(objChildProgEnrollment);
                    objChildProgEnrollment = null;
                }
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clChildProgEnrollment, "LoadProgEnrollment", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
            }
            return lstChilProgEnrollment;
        }
        #endregion

        #region "Gell All Program Lists by ChildSchoolYearId"
        public static List<DayCarePL.ChildProgEnrollmentProperties> LoadAllProgEnrolled(Guid ChildSchoolYearId)
        {
            clConnection.DoConnection();
            DayCareDataContext db = new DayCareDataContext();
            List<DayCarePL.ChildProgEnrollmentProperties> lstChildProgEnrollment = new List<DayCarePL.ChildProgEnrollmentProperties>();
            DayCarePL.ChildProgEnrollmentProperties objChildProgEnrollment = null;
            try
            {
                var data = db.spGetChildProgEnrollmentAll(ChildSchoolYearId);
                foreach (var d in data)
                {
                    objChildProgEnrollment = new DayCarePL.ChildProgEnrollmentProperties();
                    objChildProgEnrollment.Id = d.Id;
                    objChildProgEnrollment.LastModifiedById = d.LastModifiedById;
                    objChildProgEnrollment.ProgClassRoomId = d.ProgClassRoomId;
                    objChildProgEnrollment.ChildSchoolYearId = d.ChildSchoolYearId;
                    objChildProgEnrollment.SchoolProgramId = d.SchoolProgramId;
                    objChildProgEnrollment.DayIndex = d.DayIndex;
                    objChildProgEnrollment.Day = d.Day;
                    objChildProgEnrollment.DayType = d.DayType;
                    objChildProgEnrollment.CreatedById = d.CreatedById;
                    objChildProgEnrollment.CreateDateTime = d.CreatedDateTime;
                    objChildProgEnrollment.LastModifiedDateTime = d.LastModifiedDateTime;
                    objChildProgEnrollment.ProgramTitle = d.Program;
                    objChildProgEnrollment.ClassRoom = d.ClassRoomTitle;
                    lstChildProgEnrollment.Add(objChildProgEnrollment);
                    objChildProgEnrollment = null;
                }

            }
            catch (Exception ex)
            {
            }
            return lstChildProgEnrollment;
        }
        #endregion
    }
}
