using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace DayCareBAL
{
    // NOTE: If you change the interface name "IRoleService" here, you must also update the reference to "IRoleService" in App.config.
    [ServiceContract]
    public interface IRoleService
    {
        [OperationContract]
        bool Save(DayCarePL.RoleProperties objRole);

        [OperationContract]
        DayCarePL.RoleProperties[] LoadRoles();
    }
}
