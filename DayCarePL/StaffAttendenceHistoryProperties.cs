using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DayCarePL
{
    public class StaffAttendenceHistoryProperties
    {
        public Guid Id
        {
            get;
            set;
        }
        public Guid StaffSchoolYearId
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
        public string StaffName
        {
            get;
            set;
        }

    }
}
