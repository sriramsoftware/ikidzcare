using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace DayCareJsonService
{
    // NOTE: If you change the class name "ChildService" here, you must also update the reference to "ChildService" in App.config.
    public class ChildService : IChildService
    {
        public List<DayCarePL.iChildDataProperties> LoadChildListByClassRoomId(Guid ClassRoomId, Guid SchoolYearId)
        {
            return DayCareDAL.clStaff.LoadChildListByClassRoomId(ClassRoomId, SchoolYearId);
        }

        public List<DayCarePL.iChildDataProperties> LoadActiveChildList(Guid SchoolYearId)
        {
            return DayCareDAL.clStaff.LoadActiveChildList(SchoolYearId);
        }
    }
}
