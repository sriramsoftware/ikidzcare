using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;

namespace DayCareBAL
{
    // NOTE: If you change the class name "ClassRoomService" here, you must also update the reference to "ClassRoomService" in App.config.
    public class ClassRoomService : IClassRoomService
    {
        public DayCarePL.ClassRoomProperties[] LoadClassRoom(Guid SchoolId)
        {
            return DayCareDAL.clClassRoom.LoadClassRoom(SchoolId);
        }
        public DayCarePL.ClassRoomProperties[] LoadClassRoomReport(Guid SchoolId, Guid SchoolYearId)
        {
            return DayCareDAL.clClassRoom.LoadClassRoomReport(SchoolId, SchoolYearId);
        }
        public bool Save(DayCarePL.ClassRoomProperties objClassRoom)
        {
            return DayCareDAL.clClassRoom.Save(objClassRoom);
        }

        public  bool SaveClassRoomYearWise(DayCarePL.ClassRoomProperties objClassRoom, Guid SchoolYearId)
        {
            return DayCareDAL.clClassRoom.SaveClassRoomYearWise(objClassRoom, SchoolYearId);
        }

        public bool CheckDuplicateClassRoomName(string ClassRoomName, Guid ClassRoomId, Guid SchoolId, Guid StaffId, Guid SchoolYearId)
        {
            return DayCareDAL.clClassRoom.CheckDuplicateClassRoomName(ClassRoomName, ClassRoomId, SchoolId, StaffId, SchoolYearId);
        }
        public DataSet GetClassroomWiseStudentWeeklySchedule(Guid ClassRoomId, Guid SchoolYearId)
        {
            return DayCareDAL.clClassRoom.GetClassroomWiseStudentWeeklySchedule(ClassRoomId, SchoolYearId);
        }
        public bool Delete(Guid Id,Guid SchoolYearID)
        {
            return DayCareDAL.clClassRoom.Delete(Id, SchoolYearID);
        }
        public bool CheckClassRoomAssignedSchoolProgramm(Guid ClassRoomId)
        {
            return DayCareDAL.clClassRoom.CheckClassRoomAssignedSchoolProgramm(ClassRoomId);
        }

        public DataSet GetStudentSchedule(Guid ClassRoomId, Guid SchoolYearId, string LastNameFrom, string LastNameTo)
        {
            return DayCareDAL.clClassRoom.GetStudentSchedule(ClassRoomId, SchoolYearId, LastNameFrom, LastNameTo);
        }

        public DayCarePL.ClassRoomProperties[] LoadClassRoom(Guid SchoolId, Guid SchoolYearId)
        {
            return DayCareDAL.clClassRoom.LoadClassRoom(SchoolId, SchoolYearId);
        }
    }
}
