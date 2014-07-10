using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace DayCareBAL
{
    // NOTE: If you change the class name "FamilyDataService" here, you must also update the reference to "FamilyDataService" in App.config.
    public class FamilyDataService : IFamilyDataService
    {
        //public Guid Save(DayCarePL.ChildFamilyProperties objChildFamily)
        //{
        //    return DayCareDAL.clFamilyData.Save(objChildFamily);
        //}

        public bool CheckCodeRequire(Guid UserGroupId)
        {
            return DayCareDAL.clFamilyData.CheckCodeRequire(UserGroupId);
        }

        public bool CheckDuplicateUserName(string UserName, Guid SchoolId)
        {
            return DayCareDAL.clFamilyData.CheckDuplicateUserName(UserName, SchoolId);
        }

        public bool CheckDuplicateCode(string Code, Guid SchoolId)
        {
            return DayCareDAL.clFamilyData.CheckDuplicateCode(Code, SchoolId);
        }

        public DayCarePL.FamilyDataProperties LoadFamilyDataById(Guid FamilyDataId, Guid SchoolId)
        {
            return DayCareDAL.clFamilyData.LoadFamilyDataById(FamilyDataId, SchoolId);
        }

        public List<DayCarePL.FamilyDataProperties> LoadFamilyData(Guid ChildFamilyId, Guid SchoolId)
        {
            return DayCareDAL.clFamilyData.LoadFamilyData(ChildFamilyId, SchoolId);
        }


    }
}
