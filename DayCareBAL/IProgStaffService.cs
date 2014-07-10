using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace DayCareBAL
{
    // NOTE: If you change the interface name "IProgStaffService" here, you must also update the reference to "IProgStaffService" in App.config.
    [ServiceContract]
    public interface IProgStaffService
    {
        [OperationContract]
        List<DayCarePL.ProgStaffProperties> LoadStaffBySchoolProgram(Guid SchoolId, Guid SchoolYearId, Guid SchoolProgramId, bool IsPrimary);

        [OperationContract]
        bool Save(DayCarePL.ProgStaffProperties objProgStaff);

        [OperationContract]
        List<DayCarePL.ProgStaffProperties> GetStaffFromProgStaffBySchoolProgram(Guid SchoolProgramId);

    }
}
