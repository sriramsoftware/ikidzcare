using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Web;

namespace DayCareJsonService
{
    // NOTE: If you change the interface name "IChildAttendenceHistoryService" here, you must also update the reference to "IChildAttendenceHistoryService" in App.config.
    [ServiceContract]
    public interface IChildAttendenceHistoryService
    {
        [OperationContract]
        [WebGet(
              BodyStyle = WebMessageBodyStyle.Bare,
              RequestFormat = WebMessageFormat.Json,
              ResponseFormat = WebMessageFormat.Json,
              UriTemplate = "Save?ChildSchoolYearId={ChildSchoolYearId}&CheckInCheckOut={CheckInCheckOut}&CheckInCheckOutDateTime={CheckInCheckOutDateTime}"
              )]
        DayCarePL.Result Save(Guid ChildSchoolYearId, bool CheckInCheckOut, string CheckInCheckOutDateTime);
    }
}
