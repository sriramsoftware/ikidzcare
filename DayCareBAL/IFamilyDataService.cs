using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace DayCareBAL
{
    // NOTE: If you change the interface name "IFamilyDataService" here, you must also update the reference to "IFamilyDataService" in App.config.
    [ServiceContract]
    public interface IFamilyDataService
    {
        //[OperationContract]
        //Guid Save(DayCarePL.ChildFamilyProperties objChildFamily);

        [OperationContract]
        bool CheckCodeRequire(Guid UserGroupId);

        [OperationContract]
        bool CheckDuplicateUserName(string UserName, Guid SchoolId);

        [OperationContract]
        bool CheckDuplicateCode(string Code, Guid SchoolId);

        [OperationContract]
        DayCarePL.FamilyDataProperties LoadFamilyDataById(Guid FamilyDataId, Guid SchoolId);

        [OperationContract]
        List<DayCarePL.FamilyDataProperties> LoadFamilyData(Guid ChildFamilyId, Guid SchoolId);
    }
}
