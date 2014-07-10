using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace DayCareBAL
{
    // NOTE: If you change the interface name "IChildSchoolYearService" here, you must also update the reference to "IChildSchoolYearService" in App.config.
    [ServiceContract]
    public interface IChildSchoolYearService
    {

        [OperationContract]
        List<DayCarePL.ChildDataProperties> GetAllChildListForImport(Guid CurrentSchoolYearId, Guid PreSchoolYearId, Guid SchoolId);

        [OperationContract]
        List<DayCarePL.ChildDataProperties> GetAllActiveChildListForImport(Guid CurrentSchoolYearId, Guid SchoolId);
    }
}
