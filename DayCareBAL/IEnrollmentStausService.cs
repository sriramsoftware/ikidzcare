using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace DayCareBAL
{
    // NOTE: If you change the interface name "IEnrollmentStausService" here, you must also update the reference to "IEnrollmentStausService" in App.config.
    [ServiceContract]
    public interface IEnrollmentStausService
    {
        [OperationContract]
        bool Save(DayCarePL.EnrollmentStatusProperties objEnrollment);
        [OperationContract]
        DayCarePL.EnrollmentStatusProperties[] LoadEnrollmentStatus(Guid SchoolId);
        [OperationContract]
       bool CheckDuplicateEnrollmentStatusName(string EnrollmentStatusName, Guid EnrollmentStatusId, Guid SchoolId);
    }
}
