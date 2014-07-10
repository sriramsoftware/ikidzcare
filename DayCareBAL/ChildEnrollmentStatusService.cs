using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;

namespace DayCareBAL
{
    // NOTE: If you change the class name "ChildEnrollmentStatusService" here, you must also update the reference to "ChildEnrollmentStatusService" in App.config.
    public class ChildEnrollmentStatusService : IChildEnrollmentStatusService
    {
        public bool Save(DayCarePL.ChildEnrollmentStatusProperties objChildEnrollment)
        {
            return DayCareDAL.clChildEnrollmentStatus.Save(objChildEnrollment);
        }
        public List<DayCarePL.ChildEnrollmentStatusProperties> LoadChildEnrollmentStatus(Guid SchoolId, Guid ChildSchoolYearId)
        {
            return DayCareDAL.clChildEnrollmentStatus.LoadChildEnrollmentStatus(SchoolId, ChildSchoolYearId);
        }
        public bool CheckDuplicateChildEnrollmentStatus(Guid ChildSchoolYearId, Guid EnrollmentStatusId, DateTime EnrollmentDate, Guid Id)
        {
            return DayCareDAL.clChildEnrollmentStatus.CheckDuplicateChildEnrollmentStatus(ChildSchoolYearId, EnrollmentStatusId, EnrollmentDate,Id);
        }
    }
}
