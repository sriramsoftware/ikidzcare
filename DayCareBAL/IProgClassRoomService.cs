using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace DayCareBAL
{
    // NOTE: If you change the interface name "IProgClassRoomService" here, you must also update the reference to "IProgClassRoomService" in App.config.
    [ServiceContract]
    public interface IProgClassRoomService
    {
        [OperationContract]
        List<DayCarePL.ProgClassRoomProperties> LoadProgClassRoom(Guid SchoolId, Guid SchoolProgramId);

        [OperationContract]
        bool Save(DayCarePL.ProgClassRoomProperties objProgClassRoom);

        [OperationContract]
        List<DayCarePL.StaffProperties> LoadStaffBySchoolProgramId(Guid SchoolProgramId);

        [OperationContract]
        List<DayCarePL.ProgClassRoomProperties> LoadProgClassRoomForChildSchedule(Guid SchoolId, Guid SchoolProgramId);

        [OperationContract]
        List<DayCarePL.ProgClassRoomProperties> LoadProgClassRoom(Guid SchoolId, Guid SchoolProgramId, Guid SchoolYearId);
    }
}
