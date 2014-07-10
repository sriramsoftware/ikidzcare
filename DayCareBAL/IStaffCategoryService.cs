using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace DayCareBAL
{
    // NOTE: If you change the interface name "IStaffCategoryService" here, you must also update the reference to "IStaffCategoryService" in App.config.
    [ServiceContract]
    public interface IStaffCategoryService
    {
        [OperationContract]
        bool Save(DayCarePL.StaffCategoryProperties objStaffCat);
        [OperationContract]
        List<DayCarePL.StaffCategoryProperties> loadStaffCategory(Guid SchoolId);

        [OperationContract]
        bool CheckDuplicateStaffCategoryName(string StaffCategoryName, Guid StaffCategoryId, Guid? SchoolId);
    }
}
