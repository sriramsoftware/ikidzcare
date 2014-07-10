using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Web;

namespace DayCareJsonService
{
    // NOTE: If you change the interface name "ILoginService" here, you must also update the reference to "ILoginService" in App.config.
    [ServiceContract]
    public interface ILoginService
    {       
        [OperationContract]
        [WebGet(
              BodyStyle = WebMessageBodyStyle.Bare,
              RequestFormat = WebMessageFormat.Json,
              ResponseFormat = WebMessageFormat.Json,
              UriTemplate = "ValidateUser?UserName={UserName}&Password={Password}&SchoolId={SchoolId}"
              )]
        DayCarePL.iLoginStaffProperties ValidateUser(string UserName, string Password, Guid SchoolId);

        [OperationContract]
        [WebGet(
              BodyStyle = WebMessageBodyStyle.Bare,
              RequestFormat = WebMessageFormat.Json,
              ResponseFormat = WebMessageFormat.Json,
              UriTemplate = "LoadSchoolList"
              )]
        List<DayCarePL.SchoolListProperties> LoadSchoolList();

        [OperationContract]
        [WebGet(
              BodyStyle = WebMessageBodyStyle.Bare,
              RequestFormat = WebMessageFormat.Json,
              ResponseFormat = WebMessageFormat.Json,
              UriTemplate = "LoadSchoolListWithSchoolYearList"
              )]

        List<DayCarePL.SchoolListWithSchoolYearProperties> LoadSchoolListWithSchoolYearList();
    }
}
