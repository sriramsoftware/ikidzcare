using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DayCarePL
{
    public class StateProperties
    {
        public Guid Id
        {
            get;
            set;
        }
        public string Name
        {
            get;
            set;
        }
        public string Code
        {
            get;
            set;
        }
        public Guid CountryId
        {
            get;
            set;
        }
        public string CountryName
        {
            get;
            set;
        }
    }
}
