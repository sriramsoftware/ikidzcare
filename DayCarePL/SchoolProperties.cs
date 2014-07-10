using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DayCarePL
{
    public class SchoolProperties : CommonProperties
    {
        public string Name
        {
            get;
            set;
        }
        public string Address1
        {
            get;
            set;
        }
        public string Address2
        {
            get;
            set;
        }
        public string City
        {
            get;
            set;
        }
        public string Zip
        {
            get;
            set;
        }
        public Guid? StateId
        {
            get;
            set;
        }
        public Guid? CountryId
        {
            get;
            set;
        }
        public string MainPhone
        {
            get;
            set;
        }
        public string SecondaryPhone
        {
            get;
            set;
        }
        public string Fax
        {
            get;
            set;
        }
        public string Email
        {
            get;
            set;
        }
        public string WebSite
        {
            get;
            set;
        }
        public Guid? TimeZoneId
        {
            get;
            set;
        }
        public bool CodeRequired
        {
            get;
            set;
        }
        public decimal LateFeeAmount
        {
            get;
            set;
        }//Overdue amount
        public string Comments
        {
            get;
            set;
        }
        public string iPadHeader
        {
            get;
            set;
        }
        public string iPadHeaderFont
        {
            get;
            set;
        }
        public int? iPadHeaderFontSize
        {
            get;
            set;
        }
        public string iPadHeaderColor
        {
            get;
            set;
        }
        public string iPadMessage
        {
            get;
            set;
        }
        public string iPadMessageFont
        {
            get;
            set;
        }
        public int? iPadMessageFontSize
        {
            get;
            set;
        }
        public string iPadBackgroundImage
        {
            get;
            set;
        }
        public string iPadMessageColor
        {
            get;
            set;
        }
        
        public string CountryName
        {
            get;
            set;
        }
        public string StateName
        {
            get;
            set;
        }
    }
}
