using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;

namespace DayCareBAL
{
    // NOTE: If you change the interface name "IClassRoomService" here, you must also update the reference to "IClassRoomService" in App.config.
    [ServiceContract]
    public interface IClassRoomService
    {
        [OperationContract]
        DayCarePL.ClassRoomProperties[] LoadClassRoom(Guid SchoolId);

        [OperationContract]
        bool Save(DayCarePL.ClassRoomProperties objClassRoom);

        [OperationContract]
        bool CheckDuplicateClassRoomName(string ClassRoomName, Guid ClassRoomId, Guid SchoolId, Guid StaffId, Guid SchoolYearId);

        [OperationContract]
        DataSet GetClassroomWiseStudentWeeklySchedule(Guid ClassRoomId, Guid SchoolYearId);

        [OperationContract]
        DayCarePL.ClassRoomProperties[] LoadClassRoomReport(Guid SchoolId,Guid SchoolYearId);

        [OperationContract]
        bool Delete(Guid Id,Guid SchoolYearID);

        [OperationContract]
        bool CheckClassRoomAssignedSchoolProgramm(Guid ClassRoomId);

        [OperationContract]
        DataSet GetStudentSchedule(Guid ClassRoomId, Guid SchoolYearId, string LastNameFrom, string LastNameTo);
    }
}
