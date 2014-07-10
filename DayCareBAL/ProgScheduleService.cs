using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace DayCareBAL
{
    // NOTE: If you change the class name "ProgScheduleService" here, you must also update the reference to "ProgScheduleService" in App.config.
    public class ProgScheduleService : IProgScheduleService
    {
        public List<DayCarePL.ProgScheduleProperties> LoadProgSchedule(Guid SchoolId, Guid SchoolProgramId)
        {
            return DayCareDAL.clProgSchedule.LoadProgSchedule(SchoolId, SchoolProgramId);
        }
        public bool Save(DayCarePL.ProgScheduleProperties objProgSchedule)
        {
            return DayCareDAL.clProgSchedule.Save(objProgSchedule);
        }
        public List<DayCarePL.ProgScheduleProperties> LoadProgClassRoom(Guid SchoolProgramID)
        {
            return DayCareDAL.clProgSchedule.LoadProgClassRoom(SchoolProgramID);
        }

        public List<DayCarePL.ProgScheduleProperties> LoadProgScheduleForChildSchedule(Guid SchoolId, Guid SchoolProgramId)
        {
            return DayCareDAL.clProgSchedule.LoadProgScheduleForChildSchedule(SchoolId, SchoolProgramId);
        }
        public DayCarePL.ProgScheduleProperties CheckDuplicateProgClassRoom(Guid ClassRoomId, DateTime BeginTime, DateTime EndTime, Int32 DayIndex, Guid Id)
        {
            return DayCareDAL.clProgSchedule.CheckDuplicateProgClassRoom(ClassRoomId, BeginTime, EndTime, DayIndex, Id);
        }
        public DayCarePL.ProgScheduleProperties CheckBeginTimeAndEndTime(Guid SchoolId, Int32 DayIndex, DateTime BeginTime, DateTime EndTime)
        {
            return DayCareDAL.clProgSchedule.CheckBeginTimeAndEndTime(SchoolId, DayIndex, BeginTime, EndTime);
        }
    }
}
