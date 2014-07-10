using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace DayCareBAL
{
    // NOTE: If you change the interface name "IUserGroupService" here, you must also update the reference to "IUserGroupService" in App.config.
    [ServiceContract]
    public interface IUserGroupService
    {
        [OperationContract]
        List<DayCarePL.UserGroupProperties> LoadUserGroup(Guid SchoolId);
        [OperationContract]
        bool Save(DayCarePL.UserGroupProperties objUserGroup);

        [OperationContract]
        bool CheckDuplicateUserGroupTitle(string UserGroupTitle, Guid UserGroupId, Guid? SchoolId);
    }
}
