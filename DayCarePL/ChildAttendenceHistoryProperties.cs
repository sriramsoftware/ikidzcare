using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DayCarePL
{
    public class ChildAttendenceHistoryProperties
    {
        public Guid Id
        {
            get;
            set;
        }
        public Guid ChildSchoolYearId
        {
            get;
            set;
        }
        public bool CheckInCheckOut
        {
            get;
            set;
        }
        public DateTime CheckInCheckOutDateTime
        {
            get;
            set;
        }
        public DateTime? CheckInTime
        {
            get;
            set;
        }
        public DateTime? CheckOutTime
        {
            get;
            set;
        }
        public string ChildName
        {
            get;
            set;
        }
    }
}
