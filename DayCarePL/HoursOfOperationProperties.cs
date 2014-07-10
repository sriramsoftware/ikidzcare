using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DayCarePL
{
    public class HoursOfOperationProperties
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
        public string Day
        {
            get;
            set;
        }
        public int? DayIndex
        {
            get;
            set;
        }
        public DateTime OpenTime
        {
            get;
            set;
        }
        public DateTime CloseTime
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
