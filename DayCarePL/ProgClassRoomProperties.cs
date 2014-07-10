using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DayCarePL
{
    public class ProgClassRoomProperties : CommonProperties
    {
        public Guid SchoolProgramId
        {
            get;
            set;
        }
        public Guid? ClassRoomId
        {
            get;
            set;
        }
        public bool Active
        {
            get;
            set;
        }
        public Guid? ProgStaffId
        {
            get;
            set;
        }

        public string SchoolProgramTitle
        {
            get;
            set;
        }
        public string ClassRoomName
        {
            get;
            set;
        }
        public string StaffFullName
        {
            get;
            set;
        }
        public bool ClassRoom_Active
        {
            get;
            set;
        }
    }
}
