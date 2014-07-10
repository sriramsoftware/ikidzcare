using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DayCarePL
{
    public class SchoolListProperties
    {
        public Guid SchoolId
        {
            get;
            set;
        }
        public string SchoolName
        {
            get;
            set;
        }
        public Guid CurrentSchoolYearId
        {
            get;
            set;
        }
    }

    public class SchoolListWithSchoolYearProperties
    {
        public Guid SchoolId
        {
            get;
            set;
        }
        public string SchoolName
        {
            get;
            set;
        }
        public List<SchoolYearListProperties> SchoolYearList
        {
            get;
            set;
        }
    }

    public class SchoolYearListProperties
    {
        public Guid SchoolYearId
        {
            get;
            set;
        }
        public string Year
        {
            get;
            set;
        }
    }
}
