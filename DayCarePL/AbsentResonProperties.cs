using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DayCarePL
{
    public class AbsentResonProperties
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
        public string Reason
        {
            get;
            set;
        }
        public bool BillingAffected
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
        public DateTime LastModifiedDatetime
        {
            get;
            set;
        }
        public Guid LastModifiedById
        {
            get;
            set;
        }
        public string SchoolName
        {
            get;
            set;
        }
    }
}
