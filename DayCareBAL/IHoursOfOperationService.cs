﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace DayCareBAL
{
    // NOTE: If you change the interface name "IHoursOfOperationService" here, you must also update the reference to "IHoursOfOperationService" in App.config.
    [ServiceContract]
    public interface IHoursOfOperationService
    {
        [OperationContract]
        List<DayCarePL.HoursOfOperationProperties> LoadHoursOfOperation(Guid SchoolId);

        [OperationContract]
        bool Save(DayCarePL.HoursOfOperationProperties objHourOfOperation);
    }
}
