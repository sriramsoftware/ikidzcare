using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace DayCareBAL
{
    // NOTE: If you change the interface name "IChildScheduleService" here, you must also update the reference to "IChildScheduleService" in App.config.
    [ServiceContract]
    public interface IChildScheduleService
    {
        [OperationContract]
        bool IsPrimaryProgramInChildSchedule(Guid SchoolProgramId);

        [OperationContract]
        bool CheckProgramIdPrimaryOrNotForChildSchedule(Guid SchoolProgramId);

        [OperationContract]
        bool Save(DayCarePL.ChildScheduleProperties objChildSchedule);

        [OperationContract]
        bool CheckDupicateChildSchedule(Guid ChildSchoolYearId, Guid SchoolProgramId, Guid ProgScheduleId);

        [OperationContract]
        List<DayCarePL.ChildScheduleProperties> LoadChildSchedule(Guid ChildSchoolYearId);

        [OperationContract]
        DayCarePL.ChildScheduleProperties LoadChildScheduleById(Guid ChildScheduleId, Guid ChildSchoolYearId);

        [OperationContract]
        bool CheckAvailableClassForChild(Guid SchoolId, Guid ProgScheduleId, Guid ChildSchoolYearId, Guid SchoolProgramId);

        [OperationContract]
        int GetCountChildData(Guid ChildSchoolYearId);
    }
}
