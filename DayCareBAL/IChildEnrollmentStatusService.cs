using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;

namespace DayCareBAL
{
    // NOTE: If you change the interface name "IChildEnrollmentStatusService" here, you must also update the reference to "IChildEnrollmentStatusService" in App.config.
    [ServiceContract]
    public interface IChildEnrollmentStatusService
    {
        [OperationContract]
        bool Save(DayCarePL.ChildEnrollmentStatusProperties objChildEnrollment);
        [OperationContract]
        List<DayCarePL.ChildEnrollmentStatusProperties> LoadChildEnrollmentStatus(Guid SchoolId, Guid ChildSchoolYearId);
        [OperationContract]
        bool CheckDuplicateChildEnrollmentStatus(Guid ChildSchoolYearId, Guid EnrollmentStatusId, DateTime EnrollmentDate, Guid Id);
    }
}
