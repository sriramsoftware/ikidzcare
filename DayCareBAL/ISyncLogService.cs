using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace DayCareBAL
{
    // NOTE: If you change the interface name "ISyncLogService" here, you must also update the reference to "ISyncLogService" in App.config.
    [ServiceContract]
    public interface ISyncLogService
    {
        [OperationContract]
        List<DayCarePL.SyncLogProperties> LoadSyncLoad();
    }
}
