using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;

namespace DayCareBAL
{
    // NOTE: If you change the class name "SchoolProgramService" here, you must also update the reference to "SchoolProgramService" in App.config.
    public class SchoolProgramService : ISchoolProgramService
    {
        public List<DayCarePL.SchoolProgramProperties> LoadSchoolProgram(Guid SchoolId, Guid SchoolYearId)
        {
            return DayCareDAL.clSchoolProgram.LoadSchoolProgram(SchoolId, SchoolYearId);
        }

        public List<DayCarePL.SchoolProgramProperties> LoadSchoolProgramForChildSchedule(Guid SchoolId, Guid SchoolYearId)
        {
            return DayCareDAL.clSchoolProgram.LoadSchoolProgramForChildSchedule(SchoolId, SchoolYearId);
        }

        public Guid Save(DayCarePL.SchoolProgramProperties objSchoolProgram)
        {
            //return DayCareDAL.clSchoolProgram.Save(objSchoolProgram);
            return DayCareDAL.clSchoolProgram.Save(objSchoolProgram);
        }

        public bool CheckDuplicateSchoolProgramName(string Title, Guid SchoolId)
        {
            return DayCareDAL.clSchoolProgram.CheckDuplicateSchoolProgramName(Title, SchoolId);
        }

        public bool CheckSchoolProgramInChildSchedule(Guid SchoolId, Guid SchoolProgramId)
        {
            return DayCareDAL.clSchoolProgram.CheckSchoolProgramInChildSchedule(SchoolId, SchoolProgramId);
        }

        public DataSet GetSchoolProgramWiseStudentWeeklySchedule(Guid SchoolYearId, Guid @SchoolProgramId)
        {
            return DayCareDAL.clSchoolProgram.GetSchoolProgramWiseStudentWeeklySchedule(SchoolYearId, SchoolProgramId);
        }
        public DataSet GetAllProgClassRoomList(Guid SchoolId)
        {
            return DayCareDAL.clSchoolProgram.GetAllProgClassRoomList(SchoolId);
        }
        public bool CheckSchoolProgramInChildEnrolled(Guid Id, Guid SchoolYearId)
        {
            return DayCareDAL.clSchoolProgram.CheckSchoolProgramInChildEnrolled(Id, SchoolYearId);
        }

        public bool CheckSchoolProgramInChildEnrolledAndLedger(Guid SchoolProgramId)
        {
            return DayCareDAL.clSchoolProgram.CheckSchoolProgramInChildEnrolledAndLedger(SchoolProgramId);
        }

        public bool DeleteSchoolProgram(Guid Id)
        {
            return DayCareDAL.clSchoolProgram.DeleteSchoolProgram(Id);
        }
    }
}
