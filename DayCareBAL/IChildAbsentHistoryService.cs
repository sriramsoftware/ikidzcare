using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;

namespace DayCareBAL
{
    // NOTE: If you change the interface name "IChildAbsentHistoryService" here, you must also update the reference to "IChildAbsentHistoryService" in App.config.
    [ServiceContract]
    public interface IChildAbsentHistoryService
    {
        [OperationContract]
        List<DayCarePL.ChildAbsentHistoryProperties> LoadChildAbsentHistory(Guid ChildSchoolYearId);

        [OperationContract]
        bool Save(DayCarePL.ChildAbsentHistoryProperties objChildAbsentHistory);
    }
}
