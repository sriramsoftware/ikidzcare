using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace DayCareBAL
{
    // NOTE: If you change the interface name "IFeesPeriodService" here, you must also update the reference to "IFeesPeriodService" in App.config.
    [ServiceContract]
    public interface IFeesPeriodService
    {
        [OperationContract]
        bool Save(DayCarePL.FeesPeriodProperties objFeesPeriod);
        [OperationContract]
        DayCarePL.FeesPeriodProperties[] LoadFeesPeriod();
    }
}
