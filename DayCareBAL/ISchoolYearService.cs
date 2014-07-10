using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace DayCareBAL
{
    // NOTE: If you change the interface name "ISchoolYearService" here, you must also update the reference to "ISchoolYearService" in App.config.
    [ServiceContract]
    public interface ISchoolYearService
    {
        [OperationContract]
        List<DayCarePL.SchoolYearProperties> LoadSchoolYear(Guid SchoolId);

        [OperationContract]
        Guid Save(DayCarePL.SchoolYearProperties objSchoolYear, Guid OldCurrentSchoolYearId);

        [OperationContract]
        bool CheckDuplicateSchoolYear(string SchoolYear, Guid SchoolYearId, Guid SchoolId);

        [OperationContract]
        List<DayCarePL.SchoolYearProperties> LoadAllSchoolYear(Guid SchoolId);

        [OperationContract]
        Guid GetCurrentSchoolYear(Guid SchoolId);

        [OperationContract]
        DayCarePL.SchoolYearProperties LoadSchoolYearDtail(Guid SchoolId, Guid SchoolyearId);

        [OperationContract]
        bool IsSelectedYear_Current_NextYearORPrevYear(Guid SchoolId, string SelectedSchoolYear);

        [OperationContract]
        Guid GetCurrentOldSchoolYear(Guid SchoolID);
    }
}
