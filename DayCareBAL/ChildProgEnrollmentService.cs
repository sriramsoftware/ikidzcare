using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace DayCareBAL
{
    // NOTE: If you change the class name "ChildProgEnrollmentService" here, you must also update the reference to "ChildProgEnrollmentService" in App.config.
    public class ChildProgEnrollmentService : IChildProgEnrollmentService
    {
        public List<DayCarePL.ChildProgEnrollmentProperties> LoadProgClassRoom(Guid SchoolProgramId)
        {
            return DayCareDAL.clChildProgEnrollment.LoadProgClassRoom(SchoolProgramId);
        }
        public List<DayCarePL.ChildProgEnrollmentProperties> LoadProgram()
        {
            return DayCareDAL.clChildProgEnrollment.LoadProgram();
        }
        public decimal GetFees(Guid SchoolProgramId)
        {
            return DayCareDAL.clChildProgEnrollment.GetFees(SchoolProgramId);
        }
        public Guid Save(DayCarePL.ChildProgEnrollmentProperties objChildProgEnrollment)
        {
            return DayCareDAL.clChildProgEnrollment.Save(objChildProgEnrollment);
        }
        public int Delete(Guid Id)
        {
            return DayCareDAL.clChildProgEnrollment.Delete(Id);
        }
        public List<DayCarePL.ChildProgEnrollmentProperties> LoadProgEnrollment(Guid ChildSchoolYearId, Guid SchoolProgramId)
        {
            return DayCareDAL.clChildProgEnrollment.LoadProgEnrollment(ChildSchoolYearId, SchoolProgramId);
        }
        public List<DayCarePL.ChildProgEnrollmentProperties> LoadAllProgEnrolled(Guid ChildSchoolYearId)
        {
            return DayCareDAL.clChildProgEnrollment.LoadAllProgEnrolled(ChildSchoolYearId);
        }
    }
}
