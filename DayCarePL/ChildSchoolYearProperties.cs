using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DayCarePL
{
    public class ChildSchoolYearProperties
    {
        public Guid Id
        {
            get;
            set;
        }
        public Guid? ChildDataId
        {
            get;
            set;
        }
        public Guid? SchoolYearId
        {
            get;
            set;
        }

        public string SchoolYear
        {
            get;
            set;
        }
        public string ChildFirstName
        {
            get;
            set;
        }
        public string ChildLastName
        {
            get;
            set;
        }
        public string ChildFullName
        {
            get;
            set;
        }

    }
}
