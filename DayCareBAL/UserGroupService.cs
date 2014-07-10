using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace DayCareBAL
{
    // NOTE: If you change the class name "UserGroupService" here, you must also update the reference to "UserGroupService" in App.config.
    public class UserGroupService : IUserGroupService
    {
        public List<DayCarePL.UserGroupProperties> LoadUserGroup(Guid SchoolId)
        {
            return DayCareDAL.clUserGroup.LoadUserGroup(SchoolId);
        }
        public bool Save(DayCarePL.UserGroupProperties objUserGroup)
        {
            return DayCareDAL.clUserGroup.Save(objUserGroup);
        }
        public bool CheckDuplicateUserGroupTitle(string UserGroupTitle, Guid UserGroupId, Guid? SchoolId)
        {
            return DayCareDAL.clUserGroup.CheckDuplicateUserGroupTitle(UserGroupTitle, UserGroupId, SchoolId);
        }
    }
}
