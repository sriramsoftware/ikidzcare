using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DayCareDAL
{
    public class clChildAttendanceHistory
    {
        #region "JSON: Save Child Attendance Histroy"
        public static DayCarePL.Result Save(Guid ChildSchoolYearId, bool CheckInCheckOut, string CheckInCheckOutDateTime)
        {
            clConnection.DoConnection();
            DayCareDataContext db = new DayCareDataContext();
            DayCarePL.Result objResult = new DayCarePL.Result();
            ChildAttendenceHistory DBChildAttendaceHistory = null;
            try
            {
                DBChildAttendaceHistory = new ChildAttendenceHistory();
                DBChildAttendaceHistory.Id = System.Guid.NewGuid();
                DBChildAttendaceHistory.ChildSchoolYearId = ChildSchoolYearId;
                DBChildAttendaceHistory.CheckInCheckOut = CheckInCheckOut;
                DBChildAttendaceHistory.CheckInCheckOutDateTime = Convert.ToDateTime(CheckInCheckOutDateTime);
                db.ChildAttendenceHistories.InsertOnSubmit(DBChildAttendaceHistory);
                db.SubmitChanges();
                objResult.result = "true";
            }
            catch (Exception ex)
            {
                objResult.result = ex.Message.ToString();
            }
            return objResult;
        }
        #endregion

        #region "Get Child Attendance History List,Dt:30-Sep-2011,Db:V"
        public static List<DayCarePL.ChildAttendenceHistoryProperties> LoadChildAttendanceHistoryList(Guid ChildSchoolYearId)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clChildList, "LoadChildAttendanceHistoryList", "LoadChildAttendanceHistoryList method called", DayCarePL.Common.GUID_DEFAULT);
            clConnection.DoConnection();
            DayCareDataContext db = new DayCareDataContext();
            List<DayCarePL.ChildAttendenceHistoryProperties> lstChildAttendance = new List<DayCarePL.ChildAttendenceHistoryProperties>();
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clFamilyPayment, "LoadChildAttendanceHistoryList", "Debug LoadChildAttendanceHistoryList called", DayCarePL.Common.GUID_DEFAULT);
                DayCarePL.ChildAttendenceHistoryProperties objChildAttendanceHistory = null;
                var data = db.spChildAttendanceHistoryList(ChildSchoolYearId);
                foreach (var c in data)
                {
                    objChildAttendanceHistory = new DayCarePL.ChildAttendenceHistoryProperties();
                    objChildAttendanceHistory.Id = c.id;
                    objChildAttendanceHistory.CheckInCheckOutDateTime =Convert.ToDateTime( c.Date);
                    if (c.CheckIn !=null)
                    {
                        objChildAttendanceHistory.CheckInTime = Convert.ToDateTime(c.CheckIn);
                    }
                    if (c.CheckOut != null)
                    {
                        objChildAttendanceHistory.CheckOutTime = Convert.ToDateTime(c.CheckOut);
                    }
                    objChildAttendanceHistory.ChildName = c.Name;
                    lstChildAttendance.Add(objChildAttendanceHistory);
                }
                return lstChildAttendance;

            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clChildProgEnrollment, "Save", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                return null;
            }
        }
        #endregion

        #region " Get Child List, Dt:03-Oct-2011,Db:v"
        public static List<DayCarePL.ChildDataProperties> GetChildList(Guid SchoolYearId)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clChildList, "LoadChildAttendanceHistoryList", "LoadChildAttendanceHistoryList method called", DayCarePL.Common.GUID_DEFAULT);
            clConnection.DoConnection();
            DayCareDataContext db = new DayCareDataContext();
            List<DayCarePL.ChildDataProperties> lstChildList = new List<DayCarePL.ChildDataProperties>();
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clFamilyPayment, "LoadChildAttendanceHistoryList", "Debug LoadChildAttendanceHistoryList called", DayCarePL.Common.GUID_DEFAULT);
                DayCarePL.ChildDataProperties objChildList = null;
                var data = db.spGetChildList(SchoolYearId);
                foreach (var c in data)
                {
                    objChildList = new DayCarePL.ChildDataProperties();
                    objChildList.ChildSchoolYearId = c.Id;
                    objChildList.FullName = c.ChildFullName;
                    objChildList.Photo = c.Photo;
                    lstChildList.Add(objChildList);
                }
                return lstChildList;
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clChildProgEnrollment, "Save", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                return null;
            }
        }
        #endregion

        #region "Save Child Attendance History ,Dt:03-Oct-2011,Db:V"
        public static bool Save(DayCarePL.ChildAttendenceHistoryProperties objChildAttendanceHistory)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clAbsentReason, "Save", "Execute Save Method", DayCarePL.Common.GUID_DEFAULT);
            clConnection.DoConnection();
            bool result = false;
            DayCareDataContext db = new DayCareDataContext();
            ChildAttendenceHistory DBChildAttendanceHistory = null;
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clAbsentReason, "Save", "Debug Save Method", DayCarePL.Common.GUID_DEFAULT);
                if (objChildAttendanceHistory.Id.ToString().Equals(DayCarePL.Common.GUID_DEFAULT))
                {
                    DBChildAttendanceHistory = new ChildAttendenceHistory();
                    DBChildAttendanceHistory.Id = System.Guid.NewGuid();
                }
                else
                {
                    DBChildAttendanceHistory = db.ChildAttendenceHistories.SingleOrDefault(A => A.Id.Equals(objChildAttendanceHistory.Id));
                }
                DBChildAttendanceHistory.ChildSchoolYearId = objChildAttendanceHistory.ChildSchoolYearId;
                DBChildAttendanceHistory.CheckInCheckOutDateTime = objChildAttendanceHistory.CheckInCheckOutDateTime;
                DBChildAttendanceHistory.CheckInCheckOut = objChildAttendanceHistory.CheckInCheckOut;
                if (objChildAttendanceHistory.Id.ToString().Equals(DayCarePL.Common.GUID_DEFAULT))
                {
                    db.ChildAttendenceHistories.InsertOnSubmit(DBChildAttendanceHistory);

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
