using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace DayCareBAL
{
    // NOTE: If you change the interface name "IMyAccountService" here, you must also update the reference to "IMyAccountService" in App.config.
    [ServiceContract]
    public interface IMyAccountService
    {
        [OperationContract]
        DayCarePL.StaffProperties LoadMyAccountDetails(Guid StaffId);
        [OperationContract]
        bool Save(DayCarePL.StaffProperties objStaff);
    }
}
