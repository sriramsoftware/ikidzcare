using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;

namespace DayCareBAL
{
    // NOTE: If you change the interface name "IChildDataService" here, you must also update the reference to "IChildDataService" in App.config.
    [ServiceContract]
    public interface IChildDataService
    {
        [OperationContract]
        Guid Save(DayCarePL.ChildDataProperties objChildData);
        [OperationContract]
        List<DayCarePL.ChildDataProperties> LoadChildData(Guid SchoolId, Guid SchoolYearId, Guid ChildFamilyId);
        [OperationContract]
        DayCarePL.ChildDataProperties LoadChildDataId(Guid ChildDataId, Guid SchoolId, Guid SchoolYearId);
    }
}
