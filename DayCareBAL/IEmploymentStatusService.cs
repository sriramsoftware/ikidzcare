using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace DayCareBAL
{
    // NOTE: If you change the interface name "IEmploymentStatusService" here, you must also update the reference to "IEmploymentStatusService" in App.config.
    [ServiceContract]
    public interface IEmploymentStatusService
    {
        [OperationContract]
        bool Save(DayCarePL.EmploymentStatusProperties objEmployment);
        [OperationContract]
        DayCarePL.EmploymentStatusProperties[] LoadEmploymentStatus(Guid SchoolId);
        [OperationContract]
        bool CheckDuplicateEmploymentStatusName(string EmploymentStatusName, Guid EmploymentStatusId, Guid SchoolId);
    }
}
