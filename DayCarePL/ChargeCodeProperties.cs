using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DayCarePL
{
    public class ChargeCodeProperties : CommonProperties
    {
        public string Name
        {
            get;
            set;
        }
        public string Category
        {
            get;
            set;
        }
        public bool DebitCrdit
        {
            get;
            set;
        }
        public string CategoryName
        {
            get;
            set;
        }
    }
}
