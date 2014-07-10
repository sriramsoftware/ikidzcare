using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DayCarePL
{
    public class EnrollmentStatusProperties
    {
        public Guid Id
        {
            get;
            set;
        }
        public Guid SchoolId
        {
            get;
            set;
        }
        public string Status
        {
            get;
            set;
        }
        public bool Active
        {
            get;
            set;
        }
        public string Comments
        {
            get;
            set;
        }
        public DateTime? LastModifiedDatetime
        {
            get;
            set;
        }
        public Guid? LastModifiedById
        {
            get;
            set;
        }
    }
}
