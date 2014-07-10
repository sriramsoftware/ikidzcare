using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;

namespace DayCareDAL
{
    public class clStaffAttendenceHistory
    {
        #region "Staff Attendance, Check in Check Out"
        public static DayCarePL.Result SaveCheckInCheckOutTime(Guid StaffSchoolYearId, bool CheckInCheckOut, string CheckInCheckOutDateTime)
        {
            clConnection.DoConnection();
            DayCarePL.Result objResult = new DayCarePL.Result();
            objResult.result = "true";
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clStaffAttendenceHistory, "SaveCheckInCheckOutTime", "Execute SaveCheckInCheckOutTime Method", DayCarePL.Common.GUID_DEFAULT);
            DayCareDataContext db = new DayCareDataContext();
            StaffAttendenceHistory DBStaffAttendenceHistory = null;
            try
            {

                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clStaffAttendenceHistory, "SaveCheckInCheckOutTime", "", StaffSchoolYearId.ToString());
                DBStaffAttendenceHistory = new StaffAttendenceHistory();
                DBStaffAttendenceHistory.Id = Guid.NewGuid();
                DBStaffAttendenceHistory.StaffSchoolYearId = StaffSchoolYearId;
                DBStaffAttendenceHistory.CheckInCheckOut = CheckInCheckOut;
                DBStaffAttendenceHistory.CheckInCheckOutDateTime =Convert.ToDateTime(CheckInCheckOutDateTime);
                DBStaffAttendenceHistory.CreatedDateTime = DateTime.Now;
                db.StaffAttendenceHistories.InsertOnSubmit(DBStaffAttendenceHistory);
                db.SubmitChanges();
                objResult.result = "true";
            }
            catch (Exception ex)
            {
                objResult.result = "false";
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clStaffAttendenceHistory, "SaveCheckInCheckOutTime", ex.Message.ToString(), StaffSchoolYearId.ToString());
            }
            return objResult;
        }
        #endregion

        #region "Load Attendance History"
        public static List<DayCarePL.AttendanceHistoryProperties> LoadAttendanceHistory(string ReportFor,string SearchText)
        {
            clConnection.DoConnection();
            List<DayCarePL.AttendanceHistoryProperties> lstAttendanceHistory = new List<DayCarePL.AttendanceHistoryProperties>();
            DayCarePL.AttendanceHistoryProperties objAttendanceHistory = null;
            DayCareDataContext db = new DayCareDataContext();
            try
            {
                SortedList sl = new SortedList();
                sl.Add("@ReportFor", ReportFor);
                sl.Add("@SearchText", SearchText);
                DataSet ds = clConnection.GetDataSet("spAttendanceReport", sl);
                if (ds != null && ds.Tables.Count > 0)
                {
                    for (int iRow = 0; iRow < ds.Tables[0].Rows.Count; iRow++)
                    {
                        objAttendanceHistory = new DayCarePL.AttendanceHistoryProperties();
                        objAttendanceHistory.Name = Convert.ToString(ds.Tables[0].Rows[iRow]["Name"]);
                        objAttendanceHistory.CheckInCheckOutDateTime = Convert.ToDateTime(ds.Tables[0].Rows[iRow]["checkincheckoutdatetime"]);
                        objAttendanceHistory.CheckIn= Convert.ToBoolean(ds.Tables[0].Rows[iRow]["CheckIn"]);
                        objAttendanceHistory.CheckOut = Convert.ToBoolean(ds.Tables[0].Rows[iRow]["CheckOut"]);
                        objAttendanceHistory.Id = new Guid(ds.Tables[0].Rows[iRow]["Id"].ToString());
                        objAttendanceHistory.SchoolYearId = new Guid(ds.Tables[0].Rows[iRow]["schoolyearid"].ToString());
                        lstAttendanceHistory.Add(objAttendanceHistory);
                        objAttendanceHistory = null;
                    }
                }                
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clStaffAttendenceHistory, "LoadAttendanceHistory", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
            }
            return lstAttendanceHistory;
        }
        #endregion

        #region "Load Report Staff Attendance History"
        public static DataSet LoadAttendanceHistory1(string SearchText,string SearchStr, Guid SchoolYear)
        {
            clConnection.DoConnection();
            DayCareDataContext db = new DayCareDataContext();
            DataSet ds = new DataSet();
            try
            {

                SortedList sl = new SortedList();
                sl.Add("@SearchText", SearchText);
                sl.Add("@SearchStr", SearchStr);
                sl.Add("@SchoolYearId", SchoolYear);
                ds = clConnection.GetDataSet("spRptStaffAttendanceHistory", sl);
                if (ds != null && ds.Tables.Count > 0)
                { 
                
                }
            }
            catch (Exception ex)
            { 
            
            }
            return ds;
        }
        #endregion

        #region "Load Report Child Attendance History"
        public static DataSet LoadChildAttendanceHistory(string SearchText, string SearchStr, Guid SchoolYearId)
        {
            clConnection.DoConnection();
            DayCareDataContext db = new DayCareDataContext();
            DataSet ds = new DataSet();
            try
            {

                SortedList sl = new SortedList();
                sl.Add("@SearchText", SearchText);
                sl.Add("@SearchStr", SearchStr);
                sl.Add("@SchoolYearId", SchoolYearId);
                ds = clConnection.GetDataSet("spRptChildAttendanceHistory", sl);
                if (ds != null && ds.Tables.Count > 0)
                {

                }
            }
            catch (Exception ex)
            {

            }
            return ds;
        }
        #endregion
    }
}
