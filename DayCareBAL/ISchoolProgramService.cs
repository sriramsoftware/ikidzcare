using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;

namespace DayCareBAL
{
    // NOTE: If you change the interface name "ISchoolProgramService" here, you must also update the reference to "ISchoolProgramService" in App.config.
    [ServiceContract]
    public interface ISchoolProgramService
    {
        [OperationContract]
        List<DayCarePL.SchoolProgramProperties> LoadSchoolProgram(Guid SchoolId, Guid SchoolYearId);

        [OperationContract]
        List<DayCarePL.SchoolProgramProperties> LoadSchoolProgramForChildSchedule(Guid SchoolId, Guid SchoolYearId);

        [OperationContract]
        Guid Save(DayCarePL.SchoolProgramProperties objSchoolProgram);

        [OperationContract]
        bool CheckDuplicateSchoolProgramName(string Title, Guid SchoolId);

        [OperationContract]
        bool CheckSchoolProgramInChildSchedule(Guid SchoolId, Guid SchoolProgramId);

        [OperationContract]
        DataSet GetSchoolProgramWiseStudentWeeklySchedule(Guid SchoolYearId, Guid @SchoolProgramId);
        [OperationContract]
        DataSet GetAllProgClassRoomList(Guid SchoolId);
        [OperationContract]
        bool CheckSchoolProgramInChildEnrolled(Guid Id, Guid SchoolYearId);

        [OperationContract]
        bool DeleteSchoolProgram(Guid Id);
    }
}
