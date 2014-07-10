using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace DayCareBAL
{
    // NOTE: If you change the class name "AbsentReasonService" here, you must also update the reference to "AbsentReasonService" in App.config.
    public class AbsentReasonService : IAbsentReasonService
    {
        public bool Save(DayCarePL.AbsentResonProperties objAbsentReason)
        {
            return DayCareDAL.clAbsentReason.Save(objAbsentReason);
        }
        public DayCarePL.AbsentResonProperties[] LoadAbsentReason(Guid SchoolId)
        {
            return DayCareDAL.clAbsentReason.LoadAbsentReason(SchoolId);
        }
        public bool CheckDuplicateAbsentReason(string AbsentReason, Guid AbsentReasonId, Guid SchoolId)
        {
            return DayCareDAL.clAbsentReason.CheckDuplicateAbsentReason(AbsentReason, AbsentReasonId, SchoolId);
        }
    }
}
