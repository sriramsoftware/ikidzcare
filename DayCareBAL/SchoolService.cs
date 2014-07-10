using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace DayCareBAL
{
    // NOTE: If you change the class name "SchoolService" here, you must also update the reference to "SchoolService" in App.config.
    public class SchoolService : ISchoolService
    {
        public List<DayCarePL.SchoolProperties> LoadAllSchool()
        {
            return DayCareDAL.clSchool.LoadAllSchool();
        }

        public DayCarePL.SchoolProperties LoadSchoolInfo(Guid SchoolId)
        {
            return DayCareDAL.clSchool.LoadSchoolInfo(SchoolId);
        }

        public bool Save(DayCarePL.SchoolProperties objSchool)
        {
            return DayCareDAL.clSchool.Save(objSchool);
        }

        public decimal GetLateFeeAmount(Guid SchoolId)
        {
            return DayCareDAL.clSchool.GetLateFeeAmount(SchoolId);
        }
    }
}
