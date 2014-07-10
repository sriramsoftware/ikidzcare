using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DayCarePL
{
    public class ChildAbsentHistoryProperties : CommonProperties
    {
        public Guid ChildSchoolYearId
        {
            get;
            set;
        }
        public Guid AbsentReasonId
        {
            get;
            set;
        }
        public DateTime StartDate
        {
            get;
            set;
        }
        public DateTime EndDate
        {
            get;
            set;
        }
        public string Comments
        {
            get;
            set;
        }
        public string ChildSchoolYear
        {
            get;
            set;
        }
        public string ReasonOfAbsent
        {
            get;
            set;
        }

        public string ChildFullName
        {
            get;
            set;
        }
        public string AbsentReason
        {
            get;
            set;
        }
    }
}
