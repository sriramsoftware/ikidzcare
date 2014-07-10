using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DayCarePL
{
    public class SyncLogProperties
    {
        public Guid Id
        {
            get;
            set;
        }
        public Guid? UserId
        {
            get;
            set;
        }
        public DateTime? Datetime
        {
            get;
            set;
        }
        public string FullName
        {
            get;
            set;
        }
        public string Status { get; set; }
    }
}
