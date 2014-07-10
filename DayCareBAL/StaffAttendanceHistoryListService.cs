using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace DayCareBAL
{
    // NOTE: If you change the class name "StaffAttendanceHistoryListService" here, you must also update the reference to "StaffAttendanceHistoryListService" in App.config.
    public class StaffAttendanceHistoryListService : IStaffAttendanceHistoryListService
    {
        public List<DayCarePL.StaffAttendenceHistoryProperties> LoadStaffAttendanceHistoryList(Guid StaffSchoolYearId, Guid SchoolYearId)
        {
            return DayCareDAL.clStaffAttendanceHistoryList.LoadStaffAttendanceHistoryList(StaffSchoolYearId,SchoolYearId);
        }
        public List<DayCarePL.StaffProperties> LoadStaffList(Guid SchoolYearId)
        {
            return DayCareDAL.clStaffAttendanceHistoryList.LoadStaffList(SchoolYearId);
        }
        public bool Save(DayCarePL.StaffAttendenceHistoryProperties objStaffAttendance)
        {
            return DayCareDAL.clStaffAttendanceHistoryList.Save(objStaffAttendance);
        }
    }
}
