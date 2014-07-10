using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DayCarePL
{
    public class ChildEnrollmentStatusProperties : CommonProperties
    {
        public Guid ChildSchoolYearId
        {
            get;
            set;
        }
        public Guid? EnrollmentStatusId
        {
            get;
            set;
        }
        public Guid StatusId
        {
            get;
            set;
        }
        public DateTime? EnrollmentDate
        {
            get;
            set;
        }
        public string Comments
        {
            get;
            set;
        }
        
        public string EnrollmentStatus
        {
            get;
            set;
        }

    }
}
