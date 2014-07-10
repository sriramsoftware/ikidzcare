using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;

namespace DayCareBAL
{
    // NOTE: If you change the class name "ChildDataService" here, you must also update the reference to "ChildDataService" in App.config.
    public class ChildDataService : IChildDataService
    {
        public Guid Save(DayCarePL.ChildDataProperties objChildData)
        {
            return DayCareDAL.clChilData.Save(objChildData);
        }
        public List<DayCarePL.ChildDataProperties> LoadChildData(Guid SchoolId, Guid SchoolYearId, Guid ChildFamilyId)
        {
            return DayCareDAL.clChilData.LoadChildData(SchoolId, SchoolYearId, ChildFamilyId);
        }
        public DayCarePL.ChildDataProperties LoadChildDataId(Guid ChildDataId, Guid SchoolId, Guid SchoolYearId)
        {
            return DayCareDAL.clChilData.LoadChildDataId(ChildDataId, SchoolId, SchoolYearId);
        }

    }
}
