using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DayCarePL
{
    public class AttendanceHistoryProperties
    {
        public string Name
        {
            get;
            set;
        }
        public DateTime CheckInCheckOutDateTime
        {
            get;
            set;
        }
        public bool CheckIn
        {
            get;
            set;
        }
        public bool CheckOut
        {
            get;
            set;
        }
        public Guid Id
        {
            get;
            set;
        }
        public Guid SchoolYearId
        {
            get;
            set;
        }
    }
}
