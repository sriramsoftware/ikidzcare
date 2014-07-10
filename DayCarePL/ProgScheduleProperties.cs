using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DayCarePL
{
    public class ProgScheduleProperties : CommonProperties
    {
        public Guid SchoolProgramId
        {
            get;
            set;
        }
        public Guid ProgClassRoomId
        {
            get;
            set;
        }
        public Guid ClassRoomId
        {
            get;
            set;
        }
        public string Day
        {
            get;
            set;
        }
        public int DayIndex
        {
            get;
            set;
        }
        public DateTime? BeginTime
        {
            get;
            set;
        }
        public DateTime? EndTime
        {
            get;
            set;
        }
        public bool Active
        {
            get;
            set;
        }

        public string SchoolProgramTitle
        {
            get;
            set;
        }
        public string FullName
        {
            get;
            set;
        }
        public string ClassRoomName
        {
            get;
            set;
        }        
    }
}
