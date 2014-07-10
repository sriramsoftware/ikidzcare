using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace DayCareJsonService
{
    // NOTE: If you change the class name "SyncLogService" here, you must also update the reference to "SyncLogService" in App.config.
    public class SyncLogService : ISyncLogService
    {
        public DayCarePL.ResultStatus Save(string UserId, string DateTime)
        {
            return DayCareDAL.clSyncLog.Save(UserId, DateTime);
        }
    }
}
