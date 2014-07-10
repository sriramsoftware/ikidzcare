using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace DayCareBAL
{
    // NOTE: If you change the interface name "IProgClassCategoryService" here, you must also update the reference to "IProgClassCategoryService" in App.config.
    [ServiceContract]
    public interface IProgClassCategoryService
    {
        
        [OperationBehavior]
        bool Save(DayCarePL.ProgClassCategoryProperties objProgClassCategory, Guid SchoolProgramId);

        [OperationContract]
        List<DayCarePL.ProgClassCategoryProperties> LoadProgClassCategory(Guid SchoolProgramId, Guid SchoolId);

        [OperationContract]
        List<DayCarePL.ProgClassCategoryProperties> LoadProgClassCategoryForChildSchedule(Guid SchoolProgramId, Guid SchoolId);
    }
}
