using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace DayCareBAL
{
    // NOTE: If you change the class name "RoleService" here, you must also update the reference to "RoleService" in App.config.
    public class RoleService : IRoleService
    {
        public bool Save(DayCarePL.RoleProperties objRole)
        {
            return DayCareDAL.clRole.Save(objRole);
        }

        public DayCarePL.RoleProperties[] LoadRoles()
        {
            return DayCareDAL.clRole.LoadRoles();
        }
    }
}
