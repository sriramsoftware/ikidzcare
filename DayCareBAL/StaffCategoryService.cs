using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace DayCareBAL
{
    // NOTE: If you change the class name "StaffCategoryService" here, you must also update the reference to "StaffCategoryService" in App.config.
    public class StaffCategoryService : IStaffCategoryService
    {
        public bool Save(DayCarePL.StaffCategoryProperties objStaffCat)
        {
            return DayCareDAL.clStaffCategory.Save(objStaffCat);
        }
        public List<DayCarePL.StaffCategoryProperties> loadStaffCategory(Guid SchoolId)
        {
            return DayCareDAL.clStaffCategory.loadStaffCategory(SchoolId);
        }

        public bool CheckDuplicateStaffCategoryName(string StaffCategoryName, Guid StaffCategoryId, Guid? SchoolId)
        {
            return DayCareDAL.clStaffCategory.CheckDuplicateStaffCategoryName(StaffCategoryName, StaffCategoryId, SchoolId);
        }
    }
}
