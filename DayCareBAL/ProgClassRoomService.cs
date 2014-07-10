using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace DayCareBAL
{
    // NOTE: If you change the class name "ProgClassRoomService" here, you must also update the reference to "ProgClassRoomService" in App.config.
    public class ProgClassRoomService : IProgClassRoomService
    {
        public List<DayCarePL.ProgClassRoomProperties> LoadProgClassRoom(Guid SchoolId, Guid SchoolProgramId)
        {
            return DayCareDAL.clProgClassRoom.LoadProgClassRoom(SchoolId, SchoolProgramId);
        }

        public List<DayCarePL.ProgClassRoomProperties> LoadProgClassRoom(Guid SchoolId, Guid SchoolProgramId, Guid SchoolYearId)
        {
            return DayCareDAL.clProgClassRoom.LoadProgClassRoom(SchoolId, SchoolProgramId, SchoolYearId);
        }

        public bool Save(DayCarePL.ProgClassRoomProperties objProgClassRoom)
        {
            return DayCareDAL.clProgClassRoom.Save(objProgClassRoom);
        }

        public List<DayCarePL.StaffProperties> LoadStaffBySchoolProgramId(Guid SchoolProgramId)
        {
            return DayCareDAL.clProgClassRoom.LoadStaffBySchoolProgramId(SchoolProgramId);
        }

        public List<DayCarePL.ProgClassRoomProperties> LoadProgClassRoomForChildSchedule(Guid SchoolId, Guid SchoolProgramId)
        {
            return DayCareDAL.clProgClassRoom.LoadProgClassRoomForChildSchedule(SchoolId, SchoolProgramId);
        }
    }
}
