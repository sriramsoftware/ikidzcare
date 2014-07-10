using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DayCarePL
{
    public class TimeZoneProperties : CommonProperties
    {
        public string TimeZoneUniqueName
        {
            get;
            set;
        }
        public string TimeZone
        {
            get;
            set;
        }
        public string StandardName
        {
            get;
            set;
        }
        public string DaylightName
        {
            get;
            set;
        }
        public float? TimeZoneValue
        {
            get;
            set;
        }
        public bool? SupportDaylightSavingTime
        {
            get;
            set;
        }
        public string Description
        {
            get;
            set;
        }

    }
}
