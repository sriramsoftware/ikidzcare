using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DayCarePL
{
    public class SchoolProgramFeesDetailProperties
    {
        public Guid Id
        {
            get;
            set;
        }
        public Guid SchoolProgramId
        {
            get;
            set;
        }
        public decimal Fees
        {
            get;
            set;
        }
        public Guid FeesPeriodId
        {
            get;
            set;
        }
        public string SchoolProgram
        {
            get;
            set;
        }

        public string FeesPeriod
        {
            get;
            set;
        }

        public Guid LastmodifiedById
        {
            get;
            set;
        }
        public Guid CreatedById
        {
            get;
            set;
        }
        public Guid SchoolYearId
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
        public bool IsPrimary
        {
            get;
            set;
        }
        public DateTime? EffectiveYearDate
        {
            get;
            set;
        }
        public int? EffectiveMonthDay
        {
            get;
            set;
        }
        public int EffectiveWeekDay
        {
            get;
            set;
        }
    }
}
