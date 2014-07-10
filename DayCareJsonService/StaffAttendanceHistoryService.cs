using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace DayCareJsonService
{
    // NOTE: If you change the class name "StaffAttendanceHistoryService" here, you must also update the reference to "StaffAttendanceHistoryService" in App.config.
    public class StaffAttendanceHistoryService : IStaffAttendanceHistoryService
    {
        public DayCarePL.Result SaveCheckInCheckOutTime(Guid StaffSchoolYearId, bool CheckInCheckOut, string CheckInCheckOutDateTime)
        {
            return DayCareDAL.clStaffAttendenceHistory.SaveCheckInCheckOutTime(StaffSchoolYearId,CheckInCheckOut,CheckInCheckOutDateTime);
        }
    }
}
