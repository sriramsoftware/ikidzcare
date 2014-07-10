using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace DayCareBAL
{
    // NOTE: If you change the interface name "IProgScheduleService" here, you must also update the reference to "IProgScheduleService" in App.config.
    [ServiceContract]
    public interface IProgScheduleService
    {
        [OperationContract]
        List<DayCarePL.ProgScheduleProperties> LoadProgSchedule(Guid SchoolId, Guid SchoolProgramId);
        [OperationContract]
        bool Save(DayCarePL.ProgScheduleProperties objProgSchedule);
        [OperationContract]
        List<DayCarePL.ProgScheduleProperties> LoadProgClassRoom(Guid SchoolProgramID);

        [OperationContract]
        List<DayCarePL.ProgScheduleProperties> LoadProgScheduleForChildSchedule(Guid SchoolId, Guid SchoolProgramId);
        [OperationContract]
        DayCarePL.ProgScheduleProperties CheckDuplicateProgClassRoom(Guid ProgClassRoomId, DateTime BeginTime, DateTime EndTime, Int32 DayIndex, Guid Id);
        [OperationContract]
        DayCarePL.ProgScheduleProperties CheckBeginTimeAndEndTime(Guid SchoolId, Int32 DayIndex, DateTime BeginTime, DateTime EndTime);
    }
}
