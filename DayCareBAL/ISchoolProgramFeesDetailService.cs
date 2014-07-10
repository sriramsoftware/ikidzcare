using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;

namespace DayCareBAL
{
    // NOTE: If you change the interface name "ISchoolProgramFeesDetailService" here, you must also update the reference to "ISchoolProgramFeesDetailService" in App.config.
    [ServiceContract]
    public interface ISchoolProgramFeesDetailService
    {
        [OperationContract]
        List<DayCarePL.SchoolProgramFeesDetailProperties> LoadProgram(Guid SchoolYearId);

        [OperationContract]
        DayCarePL.FeesPeriodProperties[] LoadFeesPeriod();

        [OperationContract]
        bool Save(DayCarePL.SchoolProgramFeesDetailProperties objSchoolProgramFeesDetail);

        [OperationContract]
        List<DayCarePL.SchoolProgramProperties> LoadSchoolProgram(Guid SchoolId, Guid SchoolYearId);

        [OperationContract]
        List<DayCarePL.SchoolProgramFeesDetailProperties> LoadSchoolProgramFeesDetail(Guid SchoolProgramId);

        [OperationContract]
        bool Delete(Guid Id);

        [OperationContract]
        bool CheckSchoolProgramIdAndFeesPeriodId(Guid SchoolProgramId, Guid FeesPeriodId);

        [OperationContract]
        bool CheckDuplicateEffectiveWeekDay(Guid SchoolProgramFeesDetailId, Guid FeesPeriodId, Guid SchoolProgramId);

        //[OperationContract]
        //bool CheckSchoolProgramInChildEnrolledAndLedger(Guid SchoolProgramId);
    }
}
