using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace DayCareJsonService
{
    // NOTE: If you change the class name "LoginService" here, you must also update the reference to "LoginService" in App.config.
    public class LoginService : ILoginService
    {
        public DayCarePL.iLoginStaffProperties ValidateUser(string UserName, string Password, Guid SchoolId)
        {
            return DayCareDAL.clStaff.ValidateUser(UserName, Password, SchoolId);
        }

        public List<DayCarePL.SchoolListProperties> LoadSchoolList()
        {
            return DayCareDAL.clSchool.LoadSchoolList();
        }

        public List<DayCarePL.SchoolListWithSchoolYearProperties> LoadSchoolListWithSchoolYearList()
        {
            return DayCareDAL.clSchool.LoadSchoolListWithSchoolYearList();
        }
    }
}
