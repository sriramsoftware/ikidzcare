using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace DayCareBAL
{
    // NOTE: If you change the interface name "IChildAttendanceHistoryListService" here, you must also update the reference to "IChildAttendanceHistoryListService" in App.config.
    [ServiceContract]
    public interface IChildAttendanceHistoryListService
    {
        [OperationContract]
        List<DayCarePL.ChildAttendenceHistoryProperties> LoadChildAttendanceHistoryList(Guid ChildSchoolYearId);

        [OperationContract]
        List<DayCarePL.ChildDataProperties> GetChildList(Guid SchoolYearId);
        [OperationContract]
        bool Save(DayCarePL.ChildAttendenceHistoryProperties objChildAttendanceHistory);
    }
}
