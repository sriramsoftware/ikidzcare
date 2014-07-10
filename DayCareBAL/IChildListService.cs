using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;

namespace DayCareBAL
{
    // NOTE: If you change the interface name "IChildListService" here, you must also update the reference to "IChildListService" in App.config.
    [ServiceContract]
    public interface IChildListService
    {
        [OperationContract]
        List<DayCarePL.ChildDataProperties> GetAllChildList(Guid SchoolId, Guid SchoolYearId);
        [OperationContract]
        DataSet GetChildList(Guid SchoolId, Guid SchoolYearId, string SearchStr);
    }
}
