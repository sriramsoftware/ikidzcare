using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace DayCareBAL
{
    // NOTE: If you change the interface name "IClassCategoryService" here, you must also update the reference to "IClassCategoryService" in App.config.
    [ServiceContract]
    public interface IClassCategoryService
    {
        [OperationContract]
        DayCarePL.ClassCategoryProperties[] LoadClassCategory(Guid SchoolId);
        [OperationContract]
        bool Save(DayCarePL.ClassCategoryProperties objClassCategory);
        [OperationContract]
        bool CheckDuplicateClassCategory(string ClassCategoryName, Guid ClassCategoryId, Guid SchoolId);
    }
}
