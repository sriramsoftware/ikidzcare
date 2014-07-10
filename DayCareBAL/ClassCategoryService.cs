using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace DayCareBAL
{
    // NOTE: If you change the class name "ClassCategoryService" here, you must also update the reference to "ClassCategoryService" in App.config.
    public class ClassCategoryService : IClassCategoryService
    {
        public DayCarePL.ClassCategoryProperties[] LoadClassCategory(Guid SchoolId)
        {
            return DayCareDAL.clClassCategory.LoadClassCategory(SchoolId);
        }
        public bool Save(DayCarePL.ClassCategoryProperties objClassCategory)
        {
            return DayCareDAL.clClassCategory.Save(objClassCategory);
        }
        public bool CheckDuplicateClassCategory(string ClassCategoryName, Guid ClassCategoryId, Guid SchoolId)
        {
            return DayCareDAL.clClassCategory.CheckDuplicateClassCategory(ClassCategoryName, ClassCategoryId, SchoolId);
        }
    }
}
