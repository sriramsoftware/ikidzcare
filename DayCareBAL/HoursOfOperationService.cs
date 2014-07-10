using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace DayCareBAL
{
    // NOTE: If you change the class name "HoursOfOperationService" here, you must also update the reference to "HoursOfOperationService" in App.config.
    public class HoursOfOperationService : IHoursOfOperationService
    {
        public List<DayCarePL.HoursOfOperationProperties> LoadHoursOfOperation(Guid SchoolId)
        {
            return DayCareDAL.clHoursOfOperation.LoadHoursOfOperation(SchoolId);
        }

        public bool Save(DayCarePL.HoursOfOperationProperties objHourOfOperation)
        {
            return DayCareDAL.clHoursOfOperation.Save(objHourOfOperation);
        }
    }
}
