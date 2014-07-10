using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DayCarePL
{
    public class SchoolYearProperties : CommonProperties
    {
        public Guid SchoolId
        {
            get;
            set;
        }
        public string Year
        {
            get;
            set;
        }
        public DateTime StartDate
        {
            get;
            set;
        }
        public DateTime? EndDate
        {
            get;
            set;
        }
        public string Comments
        {
            get;
            set;
        }
        public bool CurrentId
        {
            get;
            set;
        }
        public Guid OldCurrentSchoolYearId { get; set; }
        public string OldCurrentSchoolYear { get; set; }

        public string SchoolName
        {
            get;
            set;
        }
    }
}
