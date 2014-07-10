using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace DayCareBAL
{
    // NOTE: If you change the class name "ChargeCodeService" here, you must also update the reference to "ChargeCodeService" in App.config.
    public class ChargeCodeService : IChargeCodeService
    {
        public bool Save(DayCarePL.ChargeCodeProperties objChargesCode)
        {
            return DayCareDAL.clChargeCode.Save(objChargesCode);
        }
        public List<DayCarePL.ChargeCodeProperties> LoadChargeCode()
        {
            return DayCareDAL.clChargeCode.LoadChargeCode(); 
        }
    }
 
}
