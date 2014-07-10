using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DayCarePL
{
    public class ProgClassCategoryProperties : CommonProperties
    {
        public Guid SchoolProgramId
        {
            get;
            set;
        }
        public Guid ClassCategoryId
        {
            get;
            set;
        }

        public string ShcoolTitle
        {
            get;
            set;
        }
        public string ClassCategoryName
        {
            get;
            set;
        }
        public bool Active
        {
            get;
            set;
        }
        public bool pActive
        {
            get;
            set;
        }
        public Guid Assign
        {
            get;
            set;
        }
    }
}
