using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace DayCareBAL
{
    // NOTE: If you change the class name "EnrollmentStausService" here, you must also update the reference to "EnrollmentStausService" in App.config.
    public class EnrollmentStausService : IEnrollmentStausService
    {
        public bool Save(DayCarePL.EnrollmentStatusProperties objEnrollment)
        {
            return DayCareDAL.clEnrollmentStatus.Save(objEnrollment);
        }
        public DayCarePL.EnrollmentStatusProperties[] LoadEnrollmentStatus(Guid SchoolId)
        {
            return DayCareDAL.clEnrollmentStatus.LoadEnrollmentStatus(SchoolId);
        }
        public bool CheckDuplicateEnrollmentStatusName(string EnrollmentStatusName, Guid EnrollmentStatusId, Guid SchoolId)
        {
            return DayCareDAL.clEnrollmentStatus.CheckDuplicateEnrollmentStatusName(EnrollmentStatusName,EnrollmentStatusId,SchoolId);
        }

       
    }
}
