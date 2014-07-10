using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DayCarePL
{
    public class iClassRoomWithStaffProperties
    {
        public Guid ClassRoomId
        {
            get;
            set;
        }
        public Guid StaffId
        {
            get;
            set;
        }
        public string ClassRoomTitile
        {
            get;
            set;
        }
        public string StaffName
        {
            get;
            set;
        }
        public string LastModifiedDateTime
        {
            get;
            set;
        }
        public Guid StaffSchoolYearId
        {
            get;
            set;
        }
    }
}
