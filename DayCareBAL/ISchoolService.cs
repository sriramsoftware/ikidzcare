using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace DayCareBAL
{
    // NOTE: If you change the interface name "ISchoolService" here, you must also update the reference to "ISchoolService" in App.config.
    [ServiceContract]
    public interface ISchoolService
    {
        [OperationContract]
        List<DayCarePL.SchoolProperties> LoadAllSchool();

        [OperationContract]
        DayCarePL.SchoolProperties LoadSchoolInfo(Guid SchoolId);

        [OperationContract]
        bool Save(DayCarePL.SchoolProperties objSchool);

        [OperationContract]
        decimal GetLateFeeAmount(Guid SchoolId);
    }
}
