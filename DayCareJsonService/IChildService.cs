using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Web;

namespace DayCareJsonService
{
    // NOTE: If you change the interface name "IChildService" here, you must also update the reference to "IChildService" in App.config.
    [ServiceContract]
    public interface IChildService
    {
        [OperationContract]
        [WebGet(
             BodyStyle = WebMessageBodyStyle.Bare,
             RequestFormat = WebMessageFormat.Json,
             ResponseFormat = WebMessageFormat.Json,
             UriTemplate = "LoadChildListByClassRoomId?ClassRoomId={ClassRoomId}&SchoolYearId={SchoolYearId}"
             )]
        List<DayCarePL.iChildDataProperties> LoadChildListByClassRoomId(Guid ClassRoomId, Guid SchoolYearId);


        [OperationContract]
        [WebGet(
             BodyStyle = WebMessageBodyStyle.Bare,
             RequestFormat = WebMessageFormat.Json,
             ResponseFormat = WebMessageFormat.Json,
             UriTemplate = "LoadActiveChildList?SchoolYearId={SchoolYearId}"
             )]
        List<DayCarePL.iChildDataProperties> LoadActiveChildList(Guid SchoolYearId);
    }
}
