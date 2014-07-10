using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Web;

namespace DayCareJsonService
{
    // NOTE: If you change the interface name "ISyncLogService" here, you must also update the reference to "ISyncLogService" in App.config.
    [ServiceContract]
    public interface ISyncLogService
    {
        [OperationContract]
        [WebGet(
              BodyStyle = WebMessageBodyStyle.Bare,
              RequestFormat = WebMessageFormat.Json,
              ResponseFormat = WebMessageFormat.Json,
              UriTemplate = "Save?UserId={UserId}&Datetime={Datetime}"
              )]
        DayCarePL.ResultStatus Save(string UserId, string DateTime);
    }
}
