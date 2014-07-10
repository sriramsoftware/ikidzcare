using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace DayCareBAL
{
    // NOTE: If you change the interface name "IStaffAttendanceHistoryListService" here, you must also update the reference to "IStaffAttendanceHistoryListService" in App.config.
    [ServiceContract]
    public interface IStaffAttendanceHistoryListService
    {
        [OperationContract]
        List<DayCarePL.StaffAttendenceHistoryProperties> LoadStaffAttendanceHistoryList(Guid StaffSchoolYearId, Guid SchoolYearId);
        [OperationContract]
        List<DayCarePL.StaffProperties> LoadStaffList(Guid SchoolYearId);
        [OperationContract]
        bool Save(DayCarePL.StaffAttendenceHistoryProperties objStaffAttendance);
    }
}
