using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace DayCareBAL
{
    // NOTE: If you change the class name "EmploymentStatusService" here, you must also update the reference to "EmploymentStatusService" in App.config.
    public class EmploymentStatusService : IEmploymentStatusService
    {
        public bool Save(DayCarePL.EmploymentStatusProperties objEmployment)
        {
            return DayCareDAL.clEmploymentStatus.Save(objEmployment);
        }
        public DayCarePL.EmploymentStatusProperties[] LoadEmploymentStatus(Guid SchoolId)
        {
            return DayCareDAL.clEmploymentStatus.LoadEmploymentStatus(SchoolId);
        }
        public bool CheckDuplicateEmploymentStatusName(string EmploymentStatusName, Guid EmploymentStatusId, Guid SchoolId)
        {
            return DayCareDAL.clEmploymentStatus.CheckDuplicateEmploymentStatusName(EmploymentStatusName, EmploymentStatusId, SchoolId);
        }
    }
}
