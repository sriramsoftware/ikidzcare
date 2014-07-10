using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace DayCareBAL
{
    // NOTE: If you change the class name "ChildScheduleService" here, you must also update the reference to "ChildScheduleService" in App.config.
    public class ChildScheduleService : IChildScheduleService
    {
        public bool IsPrimaryProgramInChildSchedule(Guid SchoolProgramId)
        {
            return DayCareDAL.clChildSchedule.IsPrimaryProgramInChildSchedule(SchoolProgramId);
        }

        public bool CheckProgramIdPrimaryOrNotForChildSchedule(Guid SchoolProgramId)
        {
            return DayCareDAL.clChildSchedule.CheckProgramIdPrimaryOrNotForChildSchedule(SchoolProgramId);
        }

        public bool Save(DayCarePL.ChildScheduleProperties objChildSchedule)
        {
            return DayCareDAL.clChildSchedule.Save(objChildSchedule);
        }

        public bool CheckDupicateChildSchedule(Guid ChildSchoolYearId, Guid SchoolProgramId, Guid ProgScheduleId)
        {
            return DayCareDAL.clChildSchedule.CheckDupicateChildSchedule(ChildSchoolYearId, SchoolProgramId, ProgScheduleId);
        }

        public List<DayCarePL.ChildScheduleProperties> LoadChildSchedule(Guid ChildSchoolYearId)
        {
            return DayCareDAL.clChildSchedule.LoadChildSchedule(ChildSchoolYearId);
        }

        public DayCarePL.ChildScheduleProperties LoadChildScheduleById(Guid ChildScheduleId, Guid ChildSchoolYearId)
        {
            return DayCareDAL.clChildSchedule.LoadChildScheduleById(ChildScheduleId, ChildSchoolYearId);
        }

        public bool CheckAvailableClassForChild(Guid SchoolId, Guid ProgScheduleId, Guid ChildSchoolYearId, Guid SchoolProgramId)
        {
            return DayCareDAL.clChildSchedule.CheckAvailableClassForChild(SchoolId, ProgScheduleId, ChildSchoolYearId, SchoolProgramId);
        }

        public int GetCountChildData(Guid ChildSchoolYearId)
        {
            return DayCareDAL.clChildSchedule.GetCountChildData(ChildSchoolYearId);
        }
    }
}
