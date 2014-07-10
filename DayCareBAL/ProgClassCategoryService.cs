using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace DayCareBAL
{
    // NOTE: If you change the class name "ProgClassCategoryService" here, you must also update the reference to "ProgClassCategoryService" in App.config.
    public class ProgClassCategoryService : IProgClassCategoryService
    {
        
       
        public bool Save(DayCarePL.ProgClassCategoryProperties objProgClassCategory, Guid SchoolProgramId)
        {
            return DayCareDAL.clProgClassCategory.Save(objProgClassCategory, SchoolProgramId);

        }
        public List<DayCarePL.ProgClassCategoryProperties> LoadProgClassCategory(Guid SchoolProgramId, Guid SchoolId)
        {

            return DayCareDAL.clProgClassCategory.LoadProgClassCategory(SchoolProgramId,SchoolId);
        }

        public List<DayCarePL.ProgClassCategoryProperties> LoadProgClassCategoryForChildSchedule(Guid SchoolProgramId, Guid SchoolId)
        {
            return DayCareDAL.clProgClassCategory.LoadProgClassCategoryForChildSchedule(SchoolProgramId, SchoolId);
        }
    }
}
