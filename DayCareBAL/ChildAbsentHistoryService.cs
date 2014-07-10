using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;

namespace DayCareBAL
{
    // NOTE: If you change the class name "ChildAbsentHistoryService" here, you must also update the reference to "ChildAbsentHistoryService" in App.config.
    public class ChildAbsentHistoryService : IChildAbsentHistoryService
    {
        public List<DayCarePL.ChildAbsentHistoryProperties> LoadChildAbsentHistory(Guid ChildSchoolYearId)
        {
            return DayCareDAL.clChildAbsentHistory.LoadChildAbsentHistory(ChildSchoolYearId);
        }

        public bool Save(DayCarePL.ChildAbsentHistoryProperties objChildAbsentHistory)
        {
            return DayCareDAL.clChildAbsentHistory.Save(objChildAbsentHistory);
        }
    }
}
