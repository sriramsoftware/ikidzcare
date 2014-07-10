using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DayCareDAL
{
    public class clStaffAttendanceHistoryList
    {
        #region"Get Staff Attendance Check In Check Out list ,Dt:03-Oct-2011,Db:v"
        public static List<DayCarePL.StaffAttendenceHistoryProperties> LoadStaffAttendanceHistoryList(Guid StaffSchoolYearId, Guid SchoolYearId)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.StaffList, "LoadStaffAttendanceHistoryList", "LoadStaffAttendanceHistoryList method called", DayCarePL.Common.GUID_DEFAULT);
            clConnection.DoConnection();
            DayCareDataContext db = new DayCareDataContext();
            List<DayCarePL.StaffAttendenceHistoryProperties> lstStaffAttendanceList = new List<DayCarePL.StaffAttendenceHistoryProperties>();

            try
            {

                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clFamilyPayment, "LoadStaffAttendanceHistoryList", "Debug LoadStaffAttendanceHistoryList called", DayCarePL.Common.GUID_DEFAULT);
                DayCarePL.StaffAttendenceHistoryProperties objStaffAttendanceList = null;
                var data = db.spStaffAttendanceHistoryList(StaffSchoolYearId, SchoolYearId);
                foreach (var s in data)
                {
                    objStaffAttendanceList = new DayCarePL.StaffAttendenceHistoryProperties();
                    objStaffAttendanceList.Id = s.id;
                    objStaffAttendanceList.CheckInCheckOutDateTime = s.checkincheckoutdatetime;
                    if (s.CheckIn != null)
                    {
                        objStaffAttendanceList.CheckInTime = Convert.ToDateTime(s.CheckIn);
                    }
                    if (s.CheckOut!=null)
                    {
                        objStaffAttendanceList.CheckOutTime = Convert.ToDateTime(s.CheckOut);
                    }
                    objStaffAttendanceList.StaffName = s.Name;
                    objStaffAttendanceList.StaffSchoolYearId = s.StaffSchoolyearId;
                    lstStaffAttendanceList.Add(objStaffAttendanceList);
                }
                return lstStaffAttendanceList;
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clChildProgEnrollment, "Save", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                return null;
            }
        }
        #endregion

        #region "Load StaffList,Dt-Oct-2011,Db:V"
        public static List<DayCarePL.StaffProperties> LoadStaffList(Guid SchoolYearId)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.StaffList, "LoadStaffAttendanceHistoryList", "LoadStaffAttendanceHistoryList method called", DayCarePL.Common.GUID_DEFAULT);
            clConnection.DoConnection();
            DayCareDataContext db = new DayCareDataContext();
            List<DayCarePL.StaffProperties> lstStaffList = new List<DayCarePL.StaffProperties>();
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clFamilyPayment, "LoadStaffAttendanceHistoryList", "Debug LoadStaffAttendanceHistoryList called", DayCarePL.Common.GUID_DEFAULT);
                DayCarePL.StaffProperties objStaffList = null;
                var data = db.spGetStaffList(SchoolYearId);
                foreach (var s in data)
                {
                    objStaffList = new DayCarePL.StaffProperties();
                    objStaffList.FullName = s.LastName + ", " + s.FirstName;
                    objStaffList.StaffSchoolYearId = s.StaffSchoolYearId;
                    objStaffList.Photo = s.Photo;
                    objStaffList.Id = s.Id;
                    lstStaffList.Add(objStaffList);
                    
                }
                return lstStaffList;
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clChildProgEnrollment, "Save", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                return null;
            }
        }
        #endregion

        #region "Save Staff Attendance History List,Dt:04-oct-2011,Db:V"
        public static bool Save(DayCarePL.StaffAttendenceHistoryProperties objStaffAttendance)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clAbsentReason, "Save", "Execute Save Method", DayCarePL.Common.GUID_DEFAULT);
            clConnection.DoConnection();
            bool result = false;
            DayCareDataContext db = new DayCareDataContext();
            StaffAttendenceHistory DBStaffAttendance = null;
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clAbsentReason, "Save", "Debug Save Method", DayCarePL.Common.GUID_DEFAULT);
                if (objStaffAttendance.Id.ToString().Equals(DayCarePL.Common.GUID_DEFAULT))
                {
                    DBStaffAttendance = new StaffAttendenceHistory();
                    DBStaffAttendance.Id = System.Guid.NewGuid();
                    DBStaffAttendance.CreatedDateTime = DateTime.Now;
                }
                else
                {
                    DBStaffAttendance = db.StaffAttendenceHistories.SingleOrDefault(A => A.Id.Equals(objStaffAttendance.Id));
                }
                DBStaffAttendance.StaffSchoolYearId = objStaffAttendance.StaffSchoolYearId;
                DBStaffAttendance.CheckInCheckOutDateTime = objStaffAttendance.CheckInCheckOutDateTime;
                DBStaffAttendance.CheckInCheckOut = objStaffAttendance.CheckInCheckOut;
                if (objStaffAttendance.Id.ToString().Equals(DayCarePL.Common.GUID_DEFAULT))
                {
                    db.StaffAttendenceHistories.InsertOnSubmit(DBStaffAttendance);

                }
                db.SubmitChanges();
                result = true;
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clAbsentReason, "Save", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                result = false;
            }
            return result;
        }
        #endregion
    }
}
