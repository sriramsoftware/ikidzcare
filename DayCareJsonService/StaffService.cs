using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace DayCareJsonService
{
    // NOTE: If you change the class name "StaffService" here, you must also update the reference to "StaffService" in App.config.
    public class StaffService : IStaffService
    {
        public List<DayCarePL.iStaffDetailProperties> LoadListOfStaff(Guid SchoolYearId)
        {
            return DayCareDAL.clStaff.LoadListOfStaff(SchoolYearId);
        }

        public List<DayCarePL.iClassRoomWithStaffProperties> LoadClassRoomWithAssignedStaff(Guid SchoolId, Guid SchoolYearId)
        {
            return DayCareDAL.clStaff.LoadClassRoomWithAssignedStaff(SchoolId, SchoolYearId);
        }
    }
}
