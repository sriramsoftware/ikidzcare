using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DayCarePL
{
    public class ChildScheduleProperties : CommonProperties
    {
        public Guid ChildSchoolYearId
        {
            get;
            set;
        }
        public Guid SchoolProgramId
        {
            get;
            set;
        }
        public Guid ProgClassCategoryId
        {
            get;
            set;
        }
        public Guid ProgClassRoomId
        {
            get;
            set;
        }
        public Guid? ProgScheduleId
        {
            get;
            set;
        }
        public DateTime BeginDate
        {
            get;
            set;
        }
        public DateTime EndDate
        {
            get;
            set;
        }
        public double Discount
        {
            get;
            set;
        }

        public string SchoolProgramTitle
        {
            get;
            set;
        }
        public string ProgClassCategoryName
        {
            get;
            set;
        }
        public string ProgClassRoomName
        {
            get;
            set;
        }
        public string ProgScheduleDay
        {
            get;
            set;
        }
        public string StaffFullName
        {
            get;
            set;
        }
        public DateTime ProgScheduleBeginTime
        {
            get;
            set;
        }
        public DateTime ProgScheduleEndTime
        {
            get;
            set;
        }

    }
}
