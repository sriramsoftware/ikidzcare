using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Web;

namespace DayCareJsonService
{
    // NOTE: If you change the interface name "IStaffService" here, you must also update the reference to "IStaffService" in App.config.
    [ServiceContract]
    public interface IStaffService
    {
        [OperationContract]
        [WebGet(
              BodyStyle = WebMessageBodyStyle.Bare,
              RequestFormat = WebMessageFormat.Json,
              ResponseFormat = WebMessageFormat.Json,
              UriTemplate = "LoadListOfStaff?SchoolYearId={SchoolYearId}"
              )]
        List<DayCarePL.iStaffDetailProperties> LoadListOfStaff(Guid SchoolYearId);

        [OperationContract]
        [WebGet(
              BodyStyle = WebMessageBodyStyle.Bare,
              RequestFormat = WebMessageFormat.Json,
              ResponseFormat = WebMessageFormat.Json,
              UriTemplate = "LoadClassRoomWithAssignedStaff?SchoolId={SchoolId}&SchoolYearId={SchoolYearId}"
              )]
        List<DayCarePL.iClassRoomWithStaffProperties> LoadClassRoomWithAssignedStaff(Guid SchoolId, Guid SchoolYearId);
    }
}
