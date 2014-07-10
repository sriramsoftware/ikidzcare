using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Web;

namespace DayCareJsonService
{
    // NOTE: If you change the interface name "IStaffAttendanceHistoryService" here, you must also update the reference to "IStaffAttendanceHistoryService" in App.config.
    [ServiceContract]
    public interface IStaffAttendanceHistoryService
    {
        [OperationContract]
        [WebGet(
              BodyStyle = WebMessageBodyStyle.Bare,
              RequestFormat = WebMessageFormat.Json,
              ResponseFormat = WebMessageFormat.Json,
              UriTemplate = "SaveCheckInCheckOutTime?StaffSchoolYearId={StaffSchoolYearId}&CheckInCheckOut={CheckInCheckOut}&CheckInCheckOutDateTime={CheckInCheckOutDateTime}"
              )]
        DayCarePL.Result SaveCheckInCheckOutTime(Guid StaffSchoolYearId, bool CheckInCheckOut, string CheckInCheckOutDateTime);
    }
}
