using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace DayCareBAL
{
    // NOTE: If you change the class name "FeesPeriodService" here, you must also update the reference to "FeesPeriodService" in App.config.
    public class FeesPeriodService : IFeesPeriodService
    {
        public bool Save(DayCarePL.FeesPeriodProperties objFeesPeriod)
        {
            return DayCareDAL.clFeesPeriod.Save(objFeesPeriod); 
        }
        public DayCarePL.FeesPeriodProperties[] LoadFeesPeriod()
        {
            return DayCareDAL.clFeesPeriod.LoadFeesPeriod();
        }
    }
}
