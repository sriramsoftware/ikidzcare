using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace DayCareBAL
{
    // NOTE: If you change the class name "LedgerOfFamily" here, you must also update the reference to "LedgerOfFamily" in App.config.
    public class LedgerOfFamilyService : ILedgerOfFamilyService
    {
        public List<DayCarePL.ChildFamilyProperties> LoadChildFamily(Guid SchoolId, Guid SchoolYearId)
        {
            return DayCareDAL.clLedgerOfFamily.LoadChildFamily(SchoolId, SchoolYearId);
        }

        public DayCarePL.LedgerProperties LoadChildFamilyWiseTranDateAmount(Guid ChildFamilyId)
        {
            return DayCareDAL.clLedgerOfFamily.LoadChildFamilyWiseTranDateAmount(ChildFamilyId);
        }

        public bool Save(DayCarePL.LedgerProperties objLedger)
        {
            return DayCareDAL.clLedgerOfFamily.Save(objLedger);
        }

        public DayCarePL.LedgerProperties GetLastUpdateLedgerDate()
        {
            return DayCareDAL.clLedgerOfFamily.GetLastUpdateLedgerDate();
        }
    }
}
