using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace DayCareBAL
{
    // NOTE: If you change the class name "ChildFamilyService" here, you must also update the reference to "ChildFamilyService" in App.config.
    public class ChildFamilyService : IChildFamilyService
    {
        public Guid Save(DayCarePL.ChildFamilyProperties objChildFamily)
        {
            return DayCareDAL.clChildFamily.Save(objChildFamily);
        }
        public List<DayCarePL.ChildFamilyProperties> LoadChildFamily(Guid SchoolId, Guid SchoolYearId)
        {
            return DayCareDAL.clChildFamily.LoadChildFamily(SchoolId, SchoolYearId);
        }

        public DayCarePL.ChildFamilyProperties LoadChildFamilyById(Guid Id,Guid SchoolYearId)
        {
            return DayCareDAL.clChildFamily.LoadChildFamilyById(Id, SchoolYearId);
        }
        public Guid GetChildDataId(Guid ChildFamilyId)
        {
            return DayCareDAL.clChildFamily.GetChildDataId(ChildFamilyId);
        }

        public List<DayCarePL.ChildFamilyProperties> GetFamiliesWithChild(Guid SchoolId, Guid SchoolYearId)
        {
            return DayCareDAL.clChildFamily.GetFamiliesWithChild(SchoolId, SchoolYearId);
        }
    }
}
