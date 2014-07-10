using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace DayCareBAL
{
    // NOTE: If you change the interface name "IAbsentReasonService" here, you must also update the reference to "IAbsentReasonService" in App.config.
    [ServiceContract]
    public interface IAbsentReasonService
    {
        [OperationContract]
        bool Save(DayCarePL.AbsentResonProperties objAbsentReason);
        [OperationContract]
        DayCarePL.AbsentResonProperties[] LoadAbsentReason(Guid SchoolId);
        [OperationContract]
        bool CheckDuplicateAbsentReason(string AbsentReason, Guid AbsentReasonId, Guid SchoolId);
    }
}
