using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace DayCareBAL
{
    // NOTE: If you change the class name "ProgStaffService" here, you must also update the reference to "ProgStaffService" in App.config.
    public class ProgStaffService : IProgStaffService
    {
        public List<DayCarePL.ProgStaffProperties> LoadStaffBySchoolProgram(Guid SchoolId, Guid SchoolYearId,Guid SchoolProgramId, bool IsPrimary)
        {
            return DayCareDAL.clProgStaff.LoadStaffBySchoolProgram(SchoolId, SchoolYearId,SchoolProgramId, IsPrimary);
        }

        public bool Save(DayCarePL.ProgStaffProperties objProgStaff)
        {
            return DayCareDAL.clProgStaff.Save(objProgStaff);
        }

        public List<DayCarePL.ProgStaffProperties> GetStaffFromProgStaffBySchoolProgram(Guid SchoolProgramId)
        {
            return DayCareDAL.clProgStaff.GetStaffFromProgStaffBySchoolProgram(SchoolProgramId);
        }
    }
}
