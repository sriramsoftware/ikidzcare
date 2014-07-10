using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace DayCareBAL
{
    // NOTE: If you change the interface name "IChildFamilyService" here, you must also update the reference to "IChildFamilyService" in App.config.
    [ServiceContract]
    public interface IChildFamilyService
    {
        [OperationContract]
        Guid Save(DayCarePL.ChildFamilyProperties objChildFamily);
        [OperationContract]
        List<DayCarePL.ChildFamilyProperties> LoadChildFamily(Guid SchoolId, Guid SchoolYearId);

        [OperationContract]
        DayCarePL.ChildFamilyProperties LoadChildFamilyById(Guid Id, Guid SchoolYearId);

        [OperationContract]
        Guid GetChildDataId(Guid ChildFamilyId);

        [OperationContract]
        List<DayCarePL.ChildFamilyProperties> GetFamiliesWithChild(Guid SchoolId, Guid SchoolYearId);
    }
}
