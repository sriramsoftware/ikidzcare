using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace DayCareBAL
{
    // NOTE: If you change the interface name "IStateService" here, you must also update the reference to "IStateService" in App.config.
    [ServiceContract]
    public interface IStateService
    {
        [OperationContract]
        List<DayCarePL.StateProperties> LoadStates();

        [OperationContract]
        bool Save(DayCarePL.StateProperties objState);
    }
}
