using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DayCarePL
{
    public class StaffSchoolYearProperties
    {
        public Guid Id
        {
            get;
            set;
        }
        public Guid StaffId
        {
            get;
            set;
        }
        public Guid SchoolYearId
        {
            get;
            set;
        }

        public string StaffFirstName
        {
            get;
            set;
        }
        public string StaffLastName
        {
            get;
            set;
        }
        public string StaffFullName
        {
            get;
            set;
        }
    }
}
