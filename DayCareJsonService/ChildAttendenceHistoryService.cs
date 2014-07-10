using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace DayCareJsonService
{
    // NOTE: If you change the class name "ChildAttendenceHistoryService" here, you must also update the reference to "ChildAttendenceHistoryService" in App.config.
    public class ChildAttendenceHistoryService : IChildAttendenceHistoryService
    {
        public DayCarePL.Result Save(Guid ChildSchoolYearId, bool CheckInCheckOut, string CheckInCheckOutDateTime)
        {
            return DayCareDAL.clChildAttendanceHistory.Save(ChildSchoolYearId, CheckInCheckOut, CheckInCheckOutDateTime);
        }
    }
}
