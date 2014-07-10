using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;

namespace DayCareBAL
{
    // NOTE: If you change the class name "ChildListService" here, you must also update the reference to "ChildListService" in App.config.
    public class ChildListService : IChildListService
    {
        public List<DayCarePL.ChildDataProperties> GetAllChildList(Guid SchoolId, Guid SchoolYearId)
        {
            return DayCareDAL.clChildList.GetAllChildList(SchoolId, SchoolYearId);
        }

        public DataSet GetChildList(Guid SchoolId, Guid SchoolYearId, string SearchStr)
        {
            return DayCareDAL.clChildList.GetChildList(SchoolId, SchoolYearId, SearchStr);
        }
    }
}
