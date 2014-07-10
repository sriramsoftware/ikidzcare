using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace DayCareBAL
{
    // NOTE: If you change the class name "ChildSchoolYearService" here, you must also update the reference to "ChildSchoolYearService" in App.config.
    public class ChildSchoolYearService : IChildSchoolYearService
    {
        public List<DayCarePL.ChildDataProperties> GetAllChildListForImport(Guid CurrentSchoolYearId, Guid PreSchoolYearId, Guid SchoolId)
        {
            return DayCareDAL.clChildSchoolYear.GetAllChildListForImport(CurrentSchoolYearId, PreSchoolYearId, SchoolId);
        }

        public List<DayCarePL.ChildDataProperties> GetAllActiveChildListForImport(Guid CurrentSchoolYearId, Guid SchoolId)
        {
            return DayCareDAL.clChildSchoolYear.GetAllActiveChildListForImport(CurrentSchoolYearId, SchoolId);
        }

        public bool ImportAllSelectedChild(Guid ChildDataId, Guid ChildFamilyId, Guid SchoolYearId)
        {
            return DayCareDAL.clChildSchoolYear.ImportAllSelectedChild(ChildDataId, ChildFamilyId, SchoolYearId);
        }

        public DayCarePL.SchoolYearProperties GetPreviousYearOfCurrentYear(Guid SchoolId, Guid CurrentSchoolYearId)
        {
            return DayCareDAL.clChildSchoolYear.GetPreviousYearOfCurrentYear(SchoolId, CurrentSchoolYearId);
        }

        public DayCarePL.SchoolYearProperties GetPreviousYearOfCurrentYearForMessage(Guid SchoolId, Guid CurrentSchoolYearId)
        {
            return DayCareDAL.clChildSchoolYear.GetPreviousYearOfCurrentYearForMessage(SchoolId, CurrentSchoolYearId);
        }
    }
}
