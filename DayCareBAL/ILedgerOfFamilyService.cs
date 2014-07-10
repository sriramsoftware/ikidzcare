using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace DayCareBAL
{
    // NOTE: If you change the interface name "ILedgerOfFamily" here, you must also update the reference to "ILedgerOfFamily" in App.config.
    [ServiceContract]
    public interface ILedgerOfFamilyService
    {
        [OperationContract]
        List<DayCarePL.ChildFamilyProperties> LoadChildFamily(Guid SchoolId, Guid SchoolYearId);

        [OperationContract]
        DayCarePL.LedgerProperties LoadChildFamilyWiseTranDateAmount(Guid ChildFamilyId);

        [OperationContract]
        bool Save(DayCarePL.LedgerProperties objLedger);

        [OperationContract]
        DayCarePL.LedgerProperties GetLastUpdateLedgerDate();
    }
}
