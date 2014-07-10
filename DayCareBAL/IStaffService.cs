using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;

namespace DayCareBAL
{
    // NOTE: If you change the interface name "IStaffService" here, you must also update the reference to "IStaffService" in App.config.
    [ServiceContract]
    public interface IStaffService
    {
        [OperationContract]
        List<DayCarePL.StaffProperties> LoadStaff(Guid SchoolId, Guid SchoolYearId);

        [OperationContract]
        Guid Save(DayCarePL.StaffProperties objStaff);

        [OperationContract]
        bool CheckDuplicateUserName(string UserName, Guid StaffId, Guid UserGroupId);

        [OperationContract]
        bool CheckDuplicateCode(string Code, Guid StaffId, Guid SchoolId);

        [OperationContract]
        bool CheckCodeRequire(Guid SchoolId);

        [OperationContract]
        DayCarePL.StaffProperties LoadStaffBystaffId(Guid StaffId, Guid CurrentSchoolYearId);

        [OperationContract]
        DayCarePL.StaffProperties LoadStaffDetailsByUserNameAndPassword(string UserName, string Password, Guid School);

        [OperationContract]
        List<DayCarePL.AttendanceHistoryProperties> LoadAttendanceHistory(string ReportFor, string SearchText);

        [OperationContract]
        DataSet LoadAttendanceHistory1(string SearchText, string SearchStr, Guid SchoolYear);

        [OperationContract]
        DataSet LoadChildAttendanceHistory(string SearchText, string SearchStr, Guid SchoolYearId);
    }
}
