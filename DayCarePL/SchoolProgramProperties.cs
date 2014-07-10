using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DayCarePL
{
    public class SchoolProgramProperties : CommonProperties
    {
        public Guid SchoolYearId
        {
            get;
            set;
        }
        public string Title
        {
            get;
            set;
        }
        public double? Fees
        {
            get;
            set;
        }
        public Guid? FeesPeriodId
        {
            get;
            set;
        }
        public bool Active
        {
            get;
            set;
        }
        public string Comments
        {
            get;
            set;
        }

        public string SchoolYearTitle
        {
            get;
            set;
        }
        public string FeesPeriodName
        {
            get;
            set;
        }
        public bool IsPrimary
        {
            get;
            set;
        }
    }
}
