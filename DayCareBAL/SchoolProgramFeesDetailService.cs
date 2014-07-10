using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;

namespace DayCareBAL
{
    // NOTE: If you change the class name "SchoolProgramFeesDetailService" here, you must also update the reference to "SchoolProgramFeesDetailService" in App.config.
    public class SchoolProgramFeesDetailService : ISchoolProgramFeesDetailService
    {
        public List<DayCarePL.SchoolProgramFeesDetailProperties> LoadProgram(Guid SchoolYearId)
        {
            return DayCareDAL.clSchoolProgramFeesDetail.LoadProgram(SchoolYearId);
        }
        public DayCarePL.FeesPeriodProperties[] LoadFeesPeriod()
        {
            return DayCareDAL.clSchoolProgramFeesDetail.LoadFeesPeriod();
        }
        public bool Save(DayCarePL.SchoolProgramFeesDetailProperties objSchoolProgramFeesDetail)
        {
            return DayCareDAL.clSchoolProgramFeesDetail.Save(objSchoolProgramFeesDetail);
        }
        public List<DayCarePL.SchoolProgramProperties> LoadSchoolProgram(Guid SchoolId, Guid SchoolYearId)
        {
            return DayCareDAL.clSchoolProgram.LoadSchoolProgram(SchoolId, SchoolYearId);
        }
        public List<DayCarePL.SchoolProgramFeesDetailProperties> LoadSchoolProgramFeesDetail(Guid SchoolProgramId)
        {
            return DayCareDAL.clSchoolProgramFeesDetail.LoadSchoolProgramFeesDetail(SchoolProgramId);
        }
        public bool Delete(Guid Id)
        {
            return DayCareDAL.clSchoolProgramFeesDetail.Delete(Id);
        }
        public bool CheckSchoolProgramIdAndFeesPeriodId(Guid SchoolProgramId, Guid FeesPeriodId)
        {
            return DayCareDAL.clSchoolProgramFeesDetail.CheckSchoolProgramIdAndFeesPeriodId(SchoolProgramId, FeesPeriodId);
        }
        public bool CheckDuplicateEffectiveWeekDay(Guid SchoolProgramFeesDetailId, Guid FeesPeriodId, Guid SchoolProgramId)
        {
            return DayCareDAL.clSchoolProgramFeesDetail.CheckDuplicateEffectiveWeekDay(SchoolProgramFeesDetailId, FeesPeriodId,SchoolProgramId);
        }
       
    }
}
