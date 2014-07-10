using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace DayCareBAL
{
    // NOTE: If you change the class name "ChildAttendanceHistoryListService" here, you must also update the reference to "ChildAttendanceHistoryListService" in App.config.
    public class ChildAttendanceHistoryListService : IChildAttendanceHistoryListService
    {
        public List<DayCarePL.ChildAttendenceHistoryProperties> LoadChildAttendanceHistoryList(Guid ChildSchoolYearId)
        {
            return DayCareDAL.clChildAttendanceHistory.LoadChildAttendanceHistoryList(ChildSchoolYearId);
        }
        public List<DayCarePL.ChildDataProperties> GetChildList(Guid SchoolYearId)
        {
            return DayCareDAL.clChildAttendanceHistory.GetChildList(SchoolYearId);
        }
        public bool Save(DayCarePL.ChildAttendenceHistoryProperties objChildAttendanceHistory)
        {
            return DayCareDAL.clChildAttendanceHistory.Save(objChildAttendanceHistory);
        }
    }
}
